using AircraftDataAnalysisWinRT.Services;
using FlightDataEntitiesRT;
using FlightDataEntitiesRT.Decisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace AircraftDataAnalysisWinRT.FastDataReadings
{
    public class FastDataReading : AircraftDataAnalysisWinRT.Services.IDataReading
    {
        public FastDataReading(
            IFlightRawDataExtractor rawDataExtractor,
            FlightDataEntitiesRT.Flight flight,
            FlightParameters parameters)
        {
            this.m_rawDataExtractor = rawDataExtractor;
            this.m_flight = flight;
            this.m_parameters = parameters;
        }

        #region interface base

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

        public double PercentCurrent { get; set; }

        #endregion


        #region read

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

        private List<FastDataReadingStepHandler> m_handlers = new List<FastDataReadingStepHandler>();
        private FastDataReadingResourceObject m_resourceObject = null;

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
            m_handlers.Clear();
            FastDataReadingStepHandler deleteHandler = new DeleteExistsHandler(startSecond, endSecond,
                putIntoServer, previewModel, this) { CurrentFlight = this.Flight, Parameters = this.Parameters };
            m_handlers.Add(deleteHandler);
            deleteHandler.DoWorkAsync();//1.

            m_resourceObject = new FastDataReadingResourceObject();
            FastDataReadingStepHandler initHandler = new InitResourceObjectHandler(startSecond, endSecond,
                putIntoServer, previewModel, this, m_resourceObject) { CurrentFlight = this.Flight, Parameters = this.Parameters };
            m_handlers.Add(initHandler);
            initHandler.DoWorkAsync();//1.

            ReadSecondsHandler readSecondsHandler = new ReadSecondsHandler(startSecond, endSecond,
                putIntoServer, previewModel, this) { CurrentFlight = this.Flight, Parameters = this.Parameters };
            m_handlers.Add(readSecondsHandler);

            readSecondsHandler.StepHandlers = new List<StepSecondsFastDataReadingStepHandler>();
            //把所有单步Handler返回来
            GenerateStepHandlers(startSecond, endSecond, putIntoServer, previewModel, readSecondsHandler.StepHandlers);
            //加入总的Handler列表
            m_handlers.AddRange(readSecondsHandler.StepHandlers);


            //最终执行顺序：
            //1.先执行Delete、Init，完成后可以开始所有StepHandler
            //2.启动StepHandlers全部，开始监听所有动作
            //3.启动ReadSecondsHandler，里面对每一步的操作，都要激发StepHandlers的操作
            //4.调用GetResult，等待所有操作完成，每个类的GetResult自行负责Finalize

            //2. 启动StepHandlers全部，开始监听所有动作
            Parallel.ForEach(readSecondsHandler.StepHandlers,
                new Action<StepSecondsFastDataReadingStepHandler>(
                    delegate(StepSecondsFastDataReadingStepHandler stepHandler)
                    {
                        try
                        {
                            stepHandler.DoWorkAsync();
                        }
                        catch (Exception e)
                        {
                            LogHelper.Error(e);
                        }
                    }));

            //3. 启动ReadSecondsHandler，里面对每一步的操作，都要激发StepHandlers的操作
            readSecondsHandler.DoWorkAsync();

            //4.调用GetResult，等待所有操作完成，每个类的GetResult自行负责Finalize
            Parallel.ForEach(m_handlers,
                new Action<FastDataReadingStepHandler>(delegate(FastDataReadingStepHandler handler)
            {
                string result = string.Empty;
                try
                {
                    result = handler.GetResult();
                }
                catch (Exception ee)
                {
                    result = ee.Message + "\r\n" + ee.StackTrace;
                }

                if (!string.IsNullOrEmpty(result))
                {
                    LogHelper.Error(new Exception(result));
                }
            }));

            //所有操作完成
            return;
        }

        private void GenerateStepHandlers(int startSecond, int endSecond, bool putIntoServer, AircraftDataAnalysisWinRT.DataModel.RawDataPointViewModel previewModel, List<StepSecondsFastDataReadingStepHandler> stepHandlers)
        {
            StepSecondsFastDataReadingStepHandler basicStepHandler = new BasicStepSecondsHandler(startSecond, endSecond,
                putIntoServer, previewModel, this)
            {
                CurrentFlight = this.Flight,
                Parameters = this.Parameters,
                ResourceObject = this.m_resourceObject
            };
            stepHandlers.Add(basicStepHandler);
            StepSecondsFastDataReadingStepHandler decisionStepHandler = new DecisionStepSecondsHandler(startSecond, endSecond,
                putIntoServer, previewModel, this)
            {
                CurrentFlight = this.Flight,
                Parameters = this.Parameters,
                ResourceObject = this.m_resourceObject
            };
            stepHandlers.Add(decisionStepHandler);
            StepSecondsFastDataReadingStepHandler levelRecordsStepHandler = new LevelRecordsStepSecondsHandler(startSecond, endSecond,
                putIntoServer, previewModel, this)
            {
                CurrentFlight = this.Flight,
                Parameters = this.Parameters,
                ResourceObject = this.m_resourceObject
            };
            stepHandlers.Add(levelRecordsStepHandler);
            StepSecondsFastDataReadingStepHandler othersStepHandler = new OthersStepSecondsHandler(startSecond, endSecond,
                putIntoServer, previewModel, this)
            {
                CurrentFlight = this.Flight,
                Parameters = this.Parameters,
                ResourceObject = this.m_resourceObject
            };
            stepHandlers.Add(othersStepHandler);
        }

        #endregion

        #region resource init

        internal class FastDataReadingResourceObject
        {

            public Decision[] Decisions { get; set; }

            public Dictionary<Decision, Decision> HasHappendMap { get; set; }

            public List<DecisionRecord> DecisionRecords { get; set; }

            public Dictionary<string, List<ParameterRawData>> StepParameterList { get; set; }

            public Dictionary<string, List<Level2FlightRecord>> Level2RecordMap { get; set; }

            public DataPointReducer DataPointReducer { get; set; }

            public List<FlightRawDataRelationPoint> FlightRawDataRelationPoints { get; set; }

            public Dictionary<int, ParameterRawData[]> DecisionHelperMap { get; set; }
        }

        class InitResourceObjectHandler : FastDataReadingStepHandler
        {
            public InitResourceObjectHandler(int startSecond, int endSecond, bool putIntoServer,
               AircraftDataAnalysisWinRT.DataModel.RawDataPointViewModel previewModel,
               AircraftDataAnalysisWinRT.Services.IDataReading dataReading,
                FastDataReadingResourceObject resourceObject)
                : base(startSecond, endSecond, putIntoServer,
                    previewModel, dataReading)
            {

            }

            private FastDataReadingResourceObject m_resourceObject = null;

            protected override string DoWorkCore()
            {
                FastDataReadingResourceObject resourceObject = m_resourceObject;

                var decisions = ServerHelper.GetDecisions(ApplicationContext.Instance.CurrentAircraftModel);

                foreach (var ds in decisions)
                {
                    ds.ParameterObjects = ApplicationContext.Instance.GetFlightParameters(
                        ApplicationContext.Instance.CurrentAircraftModel).Parameters;
                }

                resourceObject.Decisions = decisions;

                resourceObject.HasHappendMap = new Dictionary<Decision, Decision>();
                resourceObject.DecisionRecords = new List<DecisionRecord>();
                resourceObject.StepParameterList = this.InitializeStepDictionary();
                resourceObject.Level2RecordMap = this.InitializeLevel2Dictionary();
                resourceObject.DataPointReducer = new DataPointReducer();
                resourceObject.FlightRawDataRelationPoints = new List<FlightRawDataRelationPoint>();
                resourceObject.DecisionHelperMap = new Dictionary<int, ParameterRawData[]>();

                return string.Empty;
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
        }

        #endregion
    }
}
