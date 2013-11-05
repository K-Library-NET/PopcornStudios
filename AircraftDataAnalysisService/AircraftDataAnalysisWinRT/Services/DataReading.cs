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
    public class DataReading : IAsyncActionWithProgress<int>
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
            if (putIntoServer)
            {
                DataInputHelper.DeleteExistsData(this.Flight);
                DataInputHelper.AddOrReplaceFlightAsync(this.Flight);//需要先新增或者更新Flight
            }
            var decisions = ServerHelper.GetDecisions(ApplicationContext.Instance.CurrentAircraftModel);

            foreach (var ds in decisions)
            {
                ds.ParameterObjects = ApplicationContext.Instance.GetFlightParameters(
                    ApplicationContext.Instance.CurrentAircraftModel).Parameters;
            }

            Dictionary<Decision, Decision> hasHappendMap = new Dictionary<Decision, Decision>();
            List<DecisionRecord> decisionRecords = new List<DecisionRecord>();
            Dictionary<string, List<ParameterRawData>> stepParameterList = this.InitializeStepDictionary();
            Dictionary<string, List<Level2FlightRecord>> level2RecordMap = this.InitializeLevel2Dictionary();
            DataPointReducer reducer = new DataPointReducer();

            //DEBUG
            TimeSpan span0 = new TimeSpan();
            TimeSpan span1 = new TimeSpan();
            Dictionary<int, ParameterRawData[]> decisionHelperMap = new Dictionary<int, ParameterRawData[]>();

            var st = DateTime.Now;
            int percentStart = 5;
            int percentBase = 20;

            this.PercentCurrent = 5;

            for (int i = startSecond; i < Math.Min(this.Header.FlightSeconds, endSecond); i++)
            {
                ParameterRawData[] datas = m_rawDataExtractor.GetDataBySecond(i);
                if (previewModel != null && previewModel.RawDataRowViewModel != null)
                    previewModel.RawDataRowViewModel.AddOneSecondValue(i, datas);
                //DEBUG
                decisionHelperMap.Add(i, datas);

                foreach (var d in datas)
                {
                    if (stepParameterList.ContainsKey(d.ParameterID))
                    {
                        stepParameterList[d.ParameterID].Add(d);
                    }
                }

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

                if (putIntoServer)
                    DataInputHelper.AddDecisionRecordsBatch(this.Flight, decisionRecords);
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
                this.PutInServer(topRecords, this.Flight, level2RecordMap);

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
                    task.Wait();
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
                task2.Wait();
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
