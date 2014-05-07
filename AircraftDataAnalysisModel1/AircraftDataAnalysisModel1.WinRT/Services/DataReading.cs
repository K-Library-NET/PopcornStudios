using FlightDataEntitiesRT;
using FlightDataEntitiesRT.Decisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace AircraftDataAnalysisWinRT.Services
{
    /// <summary>
    /// 数据读取处理
    /// </summary>
    public class DataReading : AircraftDataAnalysisWinRT.Services.IDataReading, IAsyncActionWithProgress<int>
    {
        public DataReading(IFlightRawDataExtractor rawDataExtractor,
            FlightDataEntitiesRT.Flight flight,
            FlightParameters parameters)
        {
            this.m_rawDataExtractor = rawDataExtractor;
            this.m_flight = flight;
            this.m_parameters = parameters;
        }

        private int m_dataReductionSecondGap = 8;

        /// <summary>
        /// 数据精简的秒间隔，默认为8
        /// </summary>
        public int DataReductionSecondGap
        {
            get { return m_dataReductionSecondGap; }
            set { m_dataReductionSecondGap = value; }
        }

        private FlightParameters m_parameters = null;

        public FlightParameters Parameters
        {
            get { return m_parameters; }
            set { m_parameters = value; }
        }

        private FlightDataEntitiesRT.Flight m_flight = null;

        public FlightDataEntitiesRT.Flight Flight
        {
            get { return m_flight; }
            set { m_flight = value; }
        }

        private IFlightRawDataExtractor m_rawDataExtractor = null;

        public IFlightRawDataExtractor RawDataExtractor
        {
            get { return m_rawDataExtractor; }
            set { m_rawDataExtractor = value; }
        }

        public AsyncActionWithProgressCompletedHandler<int> Completed
        {
            get;
            set;
        }

        public void GetResults()
        {
            if (m_currentTask != null)
                m_currentTask.Wait();
        }

        public AsyncActionProgressHandler<int> Progress
        {
            get;
            set;
        }

        public void Cancel()
        {
            if (this.Completed != null)
                this.Completed(this, AsyncStatus.Canceled);
        }

        public void Close()
        {
            if (this.Completed != null)
                this.Completed(this, AsyncStatus.Completed);
        }

        public Exception ErrorCode
        {
            get
            {
                if (m_currentTask != null)
                    return m_currentTask.Exception;
                return null;
            }
        }

        public uint Id
        {
            get { throw new NotImplementedException(); }
        }

        public AsyncStatus Status
        {
            get { return AsyncStatus.Completed; }
        }

        private FlightDataEntitiesRT.FlightDataHeader m_header = null;

        public FlightDataEntitiesRT.FlightDataHeader Header
        {
            get
            {
                return m_header;
            }
            set
            {
                m_header = value;
            }
        }

        private Task m_currentTask = null;

        public Task ReadHeaderAsync()
        {
            var task = Task.Run(new Action(this.ReadHeader));

            m_currentTask = task;

            return task;
        }

        public void ReadHeader()
        {
            this.Header = this.m_rawDataExtractor.GetHeader();
        }

        public Task ReadDataAsync()
        {
            var task = Task.Run(new Action(this.ReadData));

            m_currentTask = task;

            return task;
        }

        /// <summary>
        /// 读取数据并入库
        /// </summary>
        public void ReadData()
        {
            this.ReadData(0, this.Header.FlightSeconds, true, null);
            return;
        }

        /// <summary>
        /// 读取数据并入库
        /// </summary>
        /// <param name="startSecond"></param>
        /// <param name="endSecond"></param>
        /// <param name="putIntoServer">是否入库</param>
        /// <param name="previewModel">如果需要返回预览模型则传入空Model，数据会直接写入此对象。传空则表示不需预览</param>
        public void ReadData(int startSecond, int endSecond, bool putIntoServer,
            AircraftDataAnalysisWinRT.DataModel.RawDataPointViewModel previewModel)
        {
            Task<Flight> flightTask = null;

            if (putIntoServer)
            {
                var temp = this.Flight.GlobeDatas;
                this.Flight.GlobeDatas = null;
                DataInputHelper.DeleteExistsData(this.Flight);
                this.Flight.GlobeDatas = temp;
                flightTask = DataInputHelper.AddOrReplaceFlightAsync(this.Flight);//需要先新增或者更新Flight
            }

            var decisions = ServerHelper.GetDecisions(ApplicationContext.Instance.CurrentAircraftModel);
            var flightConditionDecisions = ServerHelper.GetAllFlightConditionDecisions(
                ApplicationContext.Instance.CurrentAircraftModel);

            var parameterObjects = ApplicationContext.Instance.GetFlightParameters(
                    ApplicationContext.Instance.CurrentAircraftModel).Parameters;

            foreach (var ds in decisions)
            {
                ds.ParameterObjects = parameterObjects;
            }
            foreach (var ds in decisions)
            {
                ds.ParameterObjects = parameterObjects;
            }

            string ViRelatedParameterIDs = ServerHelper.GetAppConfigValue(
                "ViRelatedParameterIDs", ApplicationContext.Instance.AircraftServiceURL);
            string[] virelatedParameterArray = ViRelatedParameterIDs.Split(',');

            Dictionary<Decision, Decision> hasHappendMap = new Dictionary<Decision, Decision>();
            Dictionary<FlightConditionDecision, FlightConditionDecision> hasFlightHappendMap
                = new Dictionary<FlightConditionDecision, FlightConditionDecision>();
            List<DecisionRecord> decisionRecords = new List<DecisionRecord>();
            List<DecisionRecord> decisionFlightRecords = new List<DecisionRecord>();
            Dictionary<string, List<ParameterRawData>> stepParameterList = this.InitializeStepDictionary();
            Dictionary<string, List<Level2FlightRecord>> level2RecordMap = this.InitializeLevel2Dictionary();
            DataPointReducer reducer = new DataPointReducer();

            List<FlightRawDataRelationPoint> flightRawDataRelationPoints = new List<FlightRawDataRelationPoint>();

            //DEBUG
            TimeSpan span0 = new TimeSpan();
            TimeSpan span1 = new TimeSpan();
            Dictionary<int, ParameterRawData[]> decisionHelperMap = new Dictionary<int, ParameterRawData[]>();

            var st = DateTime.Now;
            int percentStart = 5;
            int percentBase = 20;

            this.PercentCurrent = 5;

            Dictionary<string, double> prevValueMap = new Dictionary<string, double>();
            //处理Vi=1000的脉冲情况
            int ddtIsVi1000First = int.MaxValue;
            int ddtIsViLast = -1;
            for (int i = startSecond; //i < Math.Min(this.Header.FlightSeconds , endSecond); i++)
                i < this.Header.FlightSeconds; i++)//debug
            {
                ParameterRawData[] datas = m_rawDataExtractor.GetDataBySecond(i);

                bool isVi1000 = this.IsVi1000(datas);
                if (isVi1000)
                {
                    ddtIsVi1000First = Math.Min(ddtIsVi1000First, i);
                    ddtIsViLast = Math.Max(ddtIsViLast, i);
                }
            }

            for (int i = startSecond; i < Math.Min(this.Header.FlightSeconds, endSecond); i++)
            {
                ParameterRawData[] datas = m_rawDataExtractor.GetDataBySecond(i);

                bool isVi1000 = this.IsVi1000(datas);
                //if (isVi1000)
                //{
                //    ddtIsVi1000First = Math.Min(ddtIsVi1000First, i);
                //    ddtIsViLast = Math.Max(ddtIsViLast, i);
                //}

                foreach (var ddt in datas)
                {
                    for (int p = 0; p < ddt.Values.Length; p++)
                    {
                        //if (isVi1000 && prevValueMap.ContainsKey(ddt.ParameterID)) 
                        //    //&& virelatedParameterArray.Contains(ddt.ParameterID))
                        //{
                        //    ddt.Values[p] = (float)prevValueMap[ddt.ParameterID];
                        //}

                        if (isVi1000 && ddt.ParameterID != "Et") //飞行秒数千万别修正
                            // && !prevValueMap.ContainsKey(ddt.ParameterID))
                        {
                            if (ddtIsVi1000First <= 0)
                            {
                                //第一秒
                                ddt.Values[p] = this.TryGetParameterValue(
                                    m_rawDataExtractor.GetDataBySecond(ddtIsViLast + 1), ddt.ParameterID,
                                    ddt.Values[p]);
                            }
                            else
                            {
                                ddt.Values[p] = this.TryGetParameterValue(
                                    m_rawDataExtractor.GetDataBySecond(ddtIsVi1000First - 1), ddt.ParameterID,
                                    ddt.Values[p]);
                            }
                        }

                        if (ddt.Values[p] >= 65535F)
                            ddt.Values[p] = 0;
                        else
                            ddt.Values[p] = Convert.ToSingle(Math.Round(ddt.Values[0], 2));
                    }

                    if (prevValueMap.ContainsKey(ddt.ParameterID))
                    {
                        prevValueMap[ddt.ParameterID] = ddt.Values[0];
                    }
                    else
                    {
                        prevValueMap.Add(ddt.ParameterID, ddt.Values[0]);
                    }
                    //if (ddt.ParameterID == "NHL" && ddt.Second < (this.Header.FlightSeconds / 10)
                    //    && ddt.Values[0] >= 65535F)
                    //{
                    //    ddt.Values[0] = 0;
                    //}//左发高压转速在头10%和末尾10%，有可能出现65535的情况，使得曲线显示有异常
                    //if (ddt.ParameterID == "NHL" && ddt.Second > (9 * this.Header.FlightSeconds / 10)
                    //    && ddt.Values[0] >= 65535F)
                    //{
                    //    ddt.Values[0] = 0;
                    //}//这种情况目前写死去掉
                }

                if (previewModel != null && previewModel.RawDataRowViewModel != null)
                    previewModel.RawDataRowViewModel.AddOneSecondValue(i, datas);
                //DEBUG
                decisionHelperMap.Add(i, datas);

                FlightRawDataRelationPoint relation1 = new FlightRawDataRelationPoint()
                {
                    FlightID = this.Flight.FlightID,
                    FlightDate = this.Header.FlightDate,
                    XAxisParameterID = "Tt",
                    YAxisParameterID = "T6L",
                };

                FlightRawDataRelationPoint relation2 = new FlightRawDataRelationPoint()
                {
                    FlightID = this.Flight.FlightID,
                    FlightDate = this.Header.FlightDate,
                    XAxisParameterID = "Tt",
                    YAxisParameterID = "T6R",
                };

                float ttValue = 0;
                float t6lValue = 0;
                float t6rValue = 0;

                foreach (var d in datas)
                {
                    if (stepParameterList.ContainsKey(d.ParameterID))
                    {
                        if (d.ParameterID == "Tt")
                        {
                            ttValue = d.Values[0];//写死
                        }
                        else if (d.ParameterID == "T6L")
                        {
                            t6lValue = d.Values[0];
                        }
                        else if (d.ParameterID == "T6R")
                        {
                            t6rValue = d.Values[0];
                        }

                        stepParameterList[d.ParameterID].Add(d);
                    }
                }

                relation1.XAxisParameterValue = ttValue;
                relation1.YAxisParameterValue = t6lValue;
                relation2.XAxisParameterValue = ttValue;
                relation2.YAxisParameterValue = t6rValue;

                flightRawDataRelationPoints.Add(relation1);
                flightRawDataRelationPoints.Add(relation2);
                //20140118 liangdawen

                var s1 = DateTime.Now;
                //DoDecisionWithinOneSecond(decisions, hasHappendMap, decisionRecords, i, datas);
                //TODO: DoTrendDecisionWithinOneSecond
                span1 += DateTime.Now.Subtract(s1);

                this.PercentCurrent = (i / (double)this.Header.FlightSeconds) * (percentBase - percentStart);
                if (this.Progress != null)
                    this.Progress(this, (int)this.PercentCurrent);
            }

            percentStart = (int)this.PercentCurrent;
            percentBase = 30;

            //DEBUG
            Task task = Task.Run(new Action(delegate()
            {
                if (decisions == null || decisions.Count() == 0)
                    return;

                int decisionCount = decisions.Count();

                //flight condition decision
                foreach (FlightConditionDecision fde in flightConditionDecisions)
                {
                    for (int j = startSecond; j < Math.Min(this.Header.FlightSeconds, endSecond); j++)
                    {
                        var dts = decisionHelperMap[j];
                        //DEBUG: 写死出现判据
                        //if (de.DecisionID == "050" && dts[18].Values[0] > 630)
                        //    dts[48].Values[0] = 1.0F;
                        //DEBUG
                        fde.AddOneSecondDatas(j, dts);

                        if (fde.HasHappened)
                        {
                            if (!hasFlightHappendMap.ContainsKey(fde))
                                //添加一条准备记录
                                hasFlightHappendMap.Add(fde, fde);
                        }
                        else
                        {
                            if (hasFlightHappendMap.ContainsKey(fde))
                            {//从发生到不发生，应该产生一条记录
                                hasFlightHappendMap.Remove(fde);
                                DecisionRecord record = new DecisionRecord()
                                {
                                    FlightID = Flight.FlightID,
                                    EventLevel = fde.EventLevel,
                                    StartSecond = fde.ActiveStartSecond,
                                    HappenSecond = fde.HappenedSecond,
                                    EndSecond = fde.ActiveEndSecond,
                                    DecisionID = fde.DecisionID,
                                    DecisionName = fde.DecisionName,
                                };
                                record.DecisionDescription = fde.ToDecisionDescriptionString(record);
                                decisionFlightRecords.Add(record);
                            }
                        }
                    }
                }

                foreach (var decisionKey in hasFlightHappendMap.Keys)
                {
                    if (decisionKey.HasHappened)
                    {
                        DecisionRecord record = new DecisionRecord()
                        {
                            FlightID = Flight.FlightID,
                            EventLevel = decisionKey.EventLevel,
                            StartSecond = decisionKey.ActiveStartSecond,
                            HappenSecond = decisionKey.HappenedSecond,
                            EndSecond = Flight.EndSecond, //decisionKey.ActiveEndSecond,
                            DecisionID = decisionKey.DecisionID,
                            DecisionName = decisionKey.DecisionName,
                        };
                        record.DecisionDescription = decisionKey.ToDecisionDescriptionString(record);
                        decisionFlightRecords.Add(record);
                    }
                }

                //Parallel.ForEach(decisions, new ParallelOptions() { MaxDegreeOfParallelism = 4 },
                //    new Action<Decision>(delegate(Decision de)
                //    {
                foreach (Decision de in decisions)
                {
                    for (int j = startSecond; j < Math.Min(this.Header.FlightSeconds, endSecond); j++)
                    {
                        var dts = decisionHelperMap[j];
                        //DEBUG: 写死出现判据
                        //if (de.DecisionID == "050" && dts[18].Values[0] > 630)
                        //    dts[48].Values[0] = 1.0F;
                        //DEBUG
                        de.AddOneSecondDatas(j, dts);

                        if (de.HasHappened)
                        {
                            if (!hasHappendMap.ContainsKey(de))
                                //添加一条准备记录
                                hasHappendMap.Add(de, de);
                        }
                        else
                        {
                            if (hasHappendMap.ContainsKey(de))
                            {//从发生到不发生，应该产生一条记录
                                hasHappendMap.Remove(de);
                                DecisionRecord record = new DecisionRecord()
                                {
                                    FlightID = Flight.FlightID,
                                    EventLevel = de.EventLevel,
                                    StartSecond = de.ActiveStartSecond,
                                    EndSecond = de.ActiveEndSecond,
                                    DecisionID = de.DecisionID,
                                    DecisionName = de.DecisionName,
                                };
                                record.DecisionDescription = de.ToDecisionDescriptionString(record);
                                decisionRecords.Add(record);
                            }
                        }
                    }

                    this.PercentCurrent += percentBase / (double)decisionCount;
                    if (this.Progress != null)
                        this.Progress(this, (int)this.PercentCurrent);

                    if (this.Progress != null)
                        this.Progress(this, (int)this.PercentCurrent);
                }

                foreach (var decisionKey in hasHappendMap.Keys)
                {
                    if (decisionKey.HasHappened)
                    {
                        DecisionRecord record = new DecisionRecord()
                        {
                            FlightID = Flight.FlightID,
                            EventLevel = decisionKey.EventLevel,
                            StartSecond = decisionKey.ActiveStartSecond,
                            EndSecond = decisionKey.ActiveEndSecond,
                            DecisionID = decisionKey.DecisionID,
                            DecisionName = decisionKey.DecisionName,
                        };
                        record.DecisionDescription = decisionKey.ToDecisionDescriptionString(record);
                        decisionRecords.Add(record);
                    }
                }

                if (putIntoServer)
                {
                    DataInputHelper.AddDecisionRecordsBatch(this.Flight, decisionRecords);
                    DataInputHelper.AddFlightConditionDecisionRecordsBatch(this.Flight, decisionFlightRecords);

                    for (int i = 0; i < flightRawDataRelationPoints.Count; i = i + 100)
                    {
                        int range = i;
                        int rangePlus = 100;
                        if (rangePlus + range > flightRawDataRelationPoints.Count)
                            rangePlus = flightRawDataRelationPoints.Count - range;
                        var takeItems = flightRawDataRelationPoints.Skip(range).Take(rangePlus);
                        try
                        {
                            DataInputHelper.AddFlightRawDataRelationPoints(this.Flight, takeItems.ToList());
                        }
                        catch (Exception eeitem)
                        {
                            LogHelper.Error(eeitem);
                        }
                    }
                }

                this.PercentCurrent += 5;
            }));

            var percentBase2 = 45;
            if (stepParameterList.Keys.Count > 0)
            {
                //Parallel.ForEach(stepParameterList.Keys, new ParallelOptions() { MaxDegreeOfParallelism = 4 },
                //    new Action<string>(delegate(string key)
                foreach (string key in stepParameterList.Keys)
                {
                    Level1FlightRecord[] reducedRecords = reducer.ReduceFlightRawDataPoints(
                        key, this.Flight.FlightID, stepParameterList[key], startSecond, endSecond, this.DataReductionSecondGap);
                    Level2FlightRecord[] level2Records = reducer.GenerateLevel2FlightRecord(this.Flight.FlightID, key, reducedRecords);

                    if (level2Records != null)
                    {
                        if (putIntoServer)
                            this.PutInServer(key, reducedRecords);

                        List<Level2FlightRecord> lv2records = level2RecordMap[key];
                        lv2records.AddRange(level2Records);
                    }

                    this.PercentCurrent += percentBase2 / (double)stepParameterList.Keys.Count;
                    if (this.Progress != null)
                        this.Progress(this, (int)this.PercentCurrent);
                }
                //));
            }

            List<LevelTopFlightRecord> topRecords = reducer.GenerateLevelTopFlightRecord(
                this.Flight.FlightID, level2RecordMap, 0, this.Header.FlightSeconds);

            //Level2和LevelTop分别入库？还是只入库LevelTop？
            if (putIntoServer)
            {
                //提取极值
                string[] extremeParameterIDs = new string[]{"Hp","Vi","Tt","KZB","KCB","Ny","Nx",
                    "Nz","T6L","T6R","NHL","NHR"};
                var extreme = from one1 in topRecords
                              where extremeParameterIDs.Contains(one1.ParameterID)
                              select one1.ExtremumPointInfo;

                List<ExtremumPointInfo> tempExtInfos = new List<ExtremumPointInfo>(extreme);

                DataInputHelper.AddOrReplaceFlightExtreme(this.Flight, tempExtInfos.ToArray());

                this.PutInServer(topRecords, this.Flight, level2RecordMap);
            }

            if (flightTask != null)
                flightTask.Wait();
            task.Wait();
            //if (putIntoServer)
            //    DataInputHelper.AddDecisionRecordsBatch(this.Flight, decisionRecords);
            if (this.Progress != null)
                this.Progress(this, 98);
            //DEBUG
            span0 += DateTime.Now.Subtract(st);

            if (this.Completed != null)
                this.Completed(this, AsyncStatus.Completed);
        }

        private float TryGetParameterValue(ParameterRawData[] parameterRawData, string p1, float p2)
        {
            if (parameterRawData != null && parameterRawData.Length > 0)
            {
                foreach (var f in parameterRawData)
                {
                    if (f.ParameterID == p1 && f.Values != null && f.Values.Length > 0)
                    {
                        return f.Values[0];
                    }
                }
            }
            return p2;
        }

        private bool IsVi1000(ParameterRawData[] datas)
        {
            foreach (var f in datas)
            {
                if (f.ParameterID == "Vi")
                {
                    if (f.Values[0] == 1000)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        private void PutInServer(List<LevelTopFlightRecord> topRecords, Flight flight,
            Dictionary<string, List<Level2FlightRecord>> level2RecordMap)
        {
            DataInputHelper.AddLevelTopFlightRecords(flight, topRecords);
        }

        private void PutInServer(string key, Level1FlightRecord[] reducedRecords)
        {
            List<Task<string>> tasks = new List<Task<string>>();

            int step = 8;//8个提交一次
            List<Level1FlightRecord> tempList = new List<Level1FlightRecord>();
            for (int i = 0; i < reducedRecords.Length; i++)
            {
                tempList.Add(reducedRecords[i]);
                if (i >= step && i % step == 0)
                {
                    var task = DataInputHelper.AddOneParameterValueAsync(this.Flight, key, tempList.ToArray());
                    //task.Wait();
                    if (!string.IsNullOrEmpty(task.Result))
                    {
                    }

                    tempList.Clear();
                    tasks.Add(task);
                }
            }

            if (tempList.Count > 0)
            {
                var task2 = DataInputHelper.AddOneParameterValueAsync(this.Flight, key, tempList.ToArray());
                //task2.Wait();
                if (!string.IsNullOrEmpty(task2.Result))
                {
                }

                tempList.Clear();
                tasks.Add(task2);
            }

            Task.WaitAll(tasks.ToArray());
        }

        private Dictionary<string, List<Level2FlightRecord>> InitializeLevel2Dictionary()
        {
            var dic = new Dictionary<string, List<Level2FlightRecord>>();
            if (this.Parameters != null && this.Parameters.Parameters != null)
            {
                foreach (var para in this.Parameters.Parameters)
                {
                    if (!dic.ContainsKey(para.ParameterID))
                        dic.Add(para.ParameterID, new List<Level2FlightRecord>());
                }
            }
            return dic;
        }

        private Dictionary<string, List<ParameterRawData>> InitializeStepDictionary()
        {
            var dic = new Dictionary<string, List<ParameterRawData>>();
            if (this.Parameters != null && this.Parameters.Parameters != null)
            {
                foreach (var para in this.Parameters.Parameters)
                {
                    if (!dic.ContainsKey(para.ParameterID))
                        dic.Add(para.ParameterID, new List<ParameterRawData>());
                }
            }
            return dic;
        }

        private void DoDecisionWithinOneSecond(Decision[] decisions,
            Dictionary<Decision, Decision> hasHappendMap, List<DecisionRecord> decisionRecords,
            int i, ParameterRawData[] datas)
        {
            Parallel.ForEach(decisions, new ParallelOptions() { MaxDegreeOfParallelism = 4 },
                new Action<Decision>(delegate(Decision de)
                    {
                        //foreach (var de in decisions)
                        //{
                        de.AddOneSecondDatas(i, datas);

                        if (de.HasHappened)
                        {
                            if (!hasHappendMap.ContainsKey(de))
                                //添加一条准备记录
                                hasHappendMap.Add(de, de);
                        }
                        else
                        {
                            if (hasHappendMap.ContainsKey(de))
                            {//从发生到不发生，应该产生一条记录
                                hasHappendMap.Remove(de);
                                DecisionRecord record = new DecisionRecord()
                                {
                                    StartSecond = de.ActiveStartSecond,
                                    EndSecond = de.ActiveEndSecond,
                                    DecisionID = de.DecisionID,
                                    DecisionName = de.DecisionName,
                                    DecisionDescription = de.ToString()
                                };
                                decisionRecords.Add(record);
                            }
                        }
                    }));
        }

        public double PercentCurrent { get; set; }
    }
}
