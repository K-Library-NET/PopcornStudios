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
            AircraftDataAnalysisWinRT.AircraftService.Flight flight,
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

        private AircraftDataAnalysisWinRT.AircraftService.Flight m_flight = null;

        public AircraftDataAnalysisWinRT.AircraftService.Flight Flight
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
            this.m_rawDataExtractor.GetHeader();
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
            DataInputHelper.DeleteExistsData(this.Flight);
            var decisions = m_rawDataExtractor.GetFaultDecisions();
            Dictionary<Decision, Decision> hasHappendMap = new Dictionary<Decision, Decision>();
            List<DecisionRecord> decisionRecords = new List<DecisionRecord>();
            Dictionary<string, List<ParameterRawData>> stepParameterList = this.InitializeStepDictionary();
            DataPointReducer reducer = new DataPointReducer();

            int percentBase = 80;
            for (int i = 0; i < this.Header.FlightSeconds; i++)
            {
                ParameterRawData[] datas = m_rawDataExtractor.GetDataBySecond(i);

                foreach (var d in datas)
                {
                    if (stepParameterList.ContainsKey(d.ParameterID))
                    {
                        stepParameterList[d.ParameterID].Add(d);
                    }
                }

                DoDecisionWithinOneSecond(decisions, hasHappendMap, decisionRecords, i, datas);
                //TODO: DoTrendDecisionWithinOneSecond

                int percent = (i / this.Header.FlightSeconds) * percentBase;
                if (this.Progress != null)
                    this.Progress(this, percent);
            }

            DataInputHelper.AddDecisionRecordsBatch(this.Flight, decisionRecords);
            if (this.Progress != null)
                this.Progress(this, 90);

            foreach (string key in stepParameterList.Keys)
            {
                Level1FlightRecord[] reducedRecords = reducer.ReduceFlightRawDataPoints(
                    key, stepParameterList[key], this.DataReductionSecondGap);
                Level2FlightRecord level2Record = reducer.GenerateLevel2FlightRecord(key, reducedRecords);

                this.PutInServer(key, reducedRecords, level2Record);
            }

            if (this.Progress != null)
                this.Progress(this, 98);

            if (this.Completed != null)
                this.Completed(this, AsyncStatus.Completed);
        }

        private void PutInServer(string key, Level1FlightRecord[] reducedRecords, Level2FlightRecord level2Record)
        {
            DataInputHelper.AddOneParameterValue(this.Flight, key, reducedRecords, level2Record);
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
            foreach (var de in decisions)
            {
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
            }
        }
    }
}
