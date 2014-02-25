using AircraftDataAnalysisWinRT.Services;
using FlightDataEntitiesRT;
using FlightDataEntitiesRT.Decisions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace AircraftDataAnalysisWinRT.DataModel
{
    public class AddFileViewModel : DataCommon, IAsyncActionWithProgress<int>
    {
        public AddFileViewModel(Flight flight,
            Windows.Storage.StorageFile file, IFlightRawDataExtractor extractor,
            AircraftModel model, FlightParameters parameters)
            : base("addfile", "导入文件", "导入文件", string.Empty, "从磁盘选择一个文件导入")
        {
            this.flight = flight;
            this.file = file;
            this.aircraftModel = model;
            this.extractor = extractor;
            this.parameters = parameters;
        }

        public string FlightID
        {
            get
            {
                if (this.Flight != null)
                {
                    return this.flight.FlightID;
                }
                return string.Empty;
            }
        }

        private string m_flightName = string.Empty;

        public string FlightName
        {
            get { return this.Flight.FlightName; }
            set
            {
                this.SetProperty<string>(ref m_flightName, value);
                this.flight.FlightName = value;
            }
        }

        private int m_endSecond = 0;

        public int EndSecond
        {
            get { return this.Flight.EndSecond; }
            set
            {
                this.SetProperty<int>(ref this.m_endSecond, value);
                this.flight.EndSecond = value;
            }
        }

        private Flight flight;

        public Flight Flight
        {
            get { return flight; }
            set { flight = value; }
        }

        private AircraftModel aircraftModel;

        public AircraftModel AircraftModel
        {
            get { return aircraftModel; }
            set { aircraftModel = value; }
        }

        private IFlightRawDataExtractor extractor;

        public IFlightRawDataExtractor Extractor
        {
            get { return extractor; }
            set { extractor = value; }
        }

        private FlightParameters parameters;

        public FlightParameters Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }

        private Windows.Storage.StorageFile file;

        public Windows.Storage.StorageFile File
        {
            get { return file; }
            set { file = value; }
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
                this.SetProperty<FlightDataEntitiesRT.FlightDataHeader>(ref m_header, value);
            }
        }

        public void InitLoadHeaderAsync()
        {
            Task.Run(new Action(async () =>
            {
                if (this.file != null)
                {
                    IDataReading read = new DataReading(this.extractor, flight, this.Parameters);
                    await read.ReadDataAsync();
                    this.Header = read.Header;
                    if (this.Header != null)
                    {
                        this.flight.EndSecond = this.Header.FlightSeconds;
                        this.EndSecond = this.flight.EndSecond;
                    }
                }
            }));
        }

        public void InitLoadHeader()
        {
            if (this.file != null)
            {
                this.Flight.GlobeDatas = new GlobeData[] { };
                IDataReading read = new DataReading(this.extractor, flight, this.Parameters);
                read.ReadHeader();
                this.Header = read.Header;
                if (this.Header != null)
                {
                    this.flight.EndSecond = this.Header.FlightSeconds;
                    this.EndSecond = this.flight.EndSecond;
                    if (this.Header.Latitudes != null && this.Header.Longitudes != null)
                        this.Flight.GlobeDatas = this.ToGlobeDatas(this.Header.Latitudes, this.Header.Longitudes);
                }
            }
        }

        private GlobeData[] ToGlobeDatas(float[] Latitudes, float[] Longitudes)
        {
            int max = Math.Min(Latitudes.Length, Longitudes.Length);

            List<GlobeData> dts = new List<GlobeData>();

            for (int i = 0; i < max; i++)
            {
                GlobeData dt = new GlobeData() { Latitude = Latitudes[i], Longitude = Longitudes[i] };

                if (dt.Latitude > 90 || dt.Longitude > 180)
                    continue;
                //经度大于180度不可以、纬度不能大于90

                dts.Add(dt);
            }

            return dts.ToArray();
        }

        public AsyncActionWithProgressCompletedHandler<int> Completed
        {
            get;
            set;
        }

        public void GetResults()
        {
            if (this.Status == AsyncStatus.Completed)
                return;

            //m_task.Wait();
        }

        public AsyncActionProgressHandler<int> Progress
        {
            get;
            set;
        }

        public void Cancel()
        {
            this.Status = AsyncStatus.Canceled;
        }

        public void Close()
        {
            this.Status = AsyncStatus.Completed;
        }

        private Exception m_errorCode = null;

        public Exception ErrorCode
        {
            get { return m_errorCode; }
            protected set
            {
                this.SetProperty<Exception>(ref m_errorCode, value);
            }
        }

        public uint Id
        {
            get { return Convert.ToUInt32(this.GetHashCode()); }
        }

        private AsyncStatus m_status = AsyncStatus.Completed;

        public AsyncStatus Status
        {
            get { return m_status; }
            protected set
            {
                this.SetProperty<AsyncStatus>(ref m_status, value);
            }
        }

        public RawDataPointViewModel GetPreviewRawDataModel(int startSecond, int endSecond)
        {
            if (this.Header == null || this.Header.FlightSeconds <= 0)
                return null;

            var parameters = ServerHelper.GetFlightParameters(ServerHelper.GetCurrentAircraftModel());

            var extractor = FlightDataReading.AircraftModel1.FlightRawDataExtractorFactory
                .CreateFlightRawDataExtractor(this.File, parameters);

            IDataReading reading = new DataReading(extractor, this.flight,
                parameters);
            reading.Header = this.Header;
            RawDataPointViewModel viewModel = new RawDataPointViewModel();

            viewModel.GenerateColumns();

            reading.ReadData(startSecond, endSecond, false, viewModel);
            //reading.ReadData();
            return viewModel;
            /*
            IFlightRawDataExtractor extractor = this.CreateRawDataExtractorByAircraftModelName(
                viewModel.CurrentFlightParameters);
            (extractor as FlightDataReading.AircraftModel1.FlightDataReadingHandler).TestSpan1 = new TimeSpan();
            (extractor as FlightDataReading.AircraftModel1.FlightDataReadingHandler).TestSpan2 = new TimeSpan();
            (extractor as FlightDataReading.AircraftModel1.FlightDataReadingHandler).TestSpan3 = new TimeSpan();
            var rowModel = viewModel.RawDataRowViewModel;

            var decisions = ServerHelper.GetDecisions(ServerHelper.GetCurrentAircraftModel());
            // extractor.GetFaultDecisions();

            Dictionary<Decision, Decision> hasHappendMap = new Dictionary<Decision, Decision>();
            List<DecisionRecord> decisionRecords = new List<DecisionRecord>();

            TimeSpan spanBinaryRead = new TimeSpan();
            TimeSpan spanRead = new TimeSpan();
            TimeSpan spanDecision = new TimeSpan();
            DateTime start = DateTime.Now;

            for (int i = startSecond; i < Math.Min(endSecond, this.Header.FlightSeconds); i++)
            {
                var s3 = DateTime.Now;
                ParameterRawData[] datas = extractor.GetDataBySecond(i);
                var s4 = DateTime.Now;
                spanBinaryRead += s4.Subtract(s3);

                rowModel.AddOneSecondValue(i, datas);
                var s5 = DateTime.Now;
                spanRead += s5.Subtract(s4);

                foreach (var de in decisions)
                {
                    var s1 = DateTime.Now;
                    de.AddOneSecondDatas(i, datas);
                    var s2 = DateTime.Now;
                    spanDecision += s2.Subtract(s1);

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

            var end = DateTime.Now;
            TimeSpan totalSpan = end.Subtract(start);

            return viewModel;*/
        }

        public RawDataPointViewModel GetRawDataModel()
        {
            return this.GetPreviewRawDataModel(0, this.Header.FlightSeconds);

            if (this.Header == null || this.Header.FlightSeconds <= 0)
                return null;

            RawDataPointViewModel viewModel = new RawDataPointViewModel();

            viewModel.GenerateColumns();

            IFlightRawDataExtractor extractor = this.CreateRawDataExtractorByAircraftModelName(
                viewModel.CurrentFlightParameters);
            (extractor as FlightDataReading.AircraftModel1.FlightDataReadingHandler).TestSpan1 = new TimeSpan();
            (extractor as FlightDataReading.AircraftModel1.FlightDataReadingHandler).TestSpan2 = new TimeSpan();
            (extractor as FlightDataReading.AircraftModel1.FlightDataReadingHandler).TestSpan3 = new TimeSpan();
            var rowModel = viewModel.RawDataRowViewModel;

            var decisions = ServerHelper.GetDecisions(ServerHelper.GetCurrentAircraftModel());
            // extractor.GetFaultDecisions();

            Dictionary<Decision, Decision> hasHappendMap = new Dictionary<Decision, Decision>();
            List<DecisionRecord> decisionRecords = new List<DecisionRecord>();

            TimeSpan spanBinaryRead = new TimeSpan();
            TimeSpan spanRead = new TimeSpan();
            TimeSpan spanDecision = new TimeSpan();
            DateTime start = DateTime.Now;

            for (int i = 0; i < this.Header.FlightSeconds; i++)
            {
                var s3 = DateTime.Now;
                ParameterRawData[] datas = extractor.GetDataBySecond(i);
                var s4 = DateTime.Now;
                spanBinaryRead += s4.Subtract(s3);

                rowModel.AddOneSecondValue(i, datas);
                var s5 = DateTime.Now;
                spanRead += s5.Subtract(s4);

                foreach (var de in decisions)
                {
                    var s1 = DateTime.Now;
                    de.AddOneSecondDatas(i, datas);
                    var s2 = DateTime.Now;
                    spanDecision += s2.Subtract(s1);

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
                                HappenSecond = de.HappenedSecond,
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

            var end = DateTime.Now;
            TimeSpan totalSpan = end.Subtract(start);

            return viewModel;
        }

        private IFlightRawDataExtractor CreateRawDataExtractorByAircraftModelName(FlightParameters flightParameter)
        {
            AircraftService.AircraftServiceClient client = new AircraftService.AircraftServiceClient();
            var modelTask = client.GetCurrentAircraftModelAsync();
            modelTask.Wait();
            var model = modelTask.Result;

            if (model != null && !string.IsNullOrEmpty(model.ModelName))
            {
                if (model.ModelName == "F4D")
                {
                    var result = FlightDataReading.AircraftModel1.FlightRawDataExtractorFactory
                        .CreateFlightRawDataExtractor(this.file, flightParameter);

                    return result;
                }
            }

            return null;
        }
    }
}
