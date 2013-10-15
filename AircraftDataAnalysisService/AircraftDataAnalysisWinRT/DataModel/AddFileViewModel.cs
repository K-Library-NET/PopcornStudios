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
        private Windows.Storage.StorageFile file;

        public AddFileViewModel(Windows.Storage.StorageFile file)
            : base("addfile", "导入文件", "导入文件", string.Empty, "从磁盘选择一个文件导入")
        {
            // TODO: Complete member initialization
            this.file = file;

            //this.CurrentFileName = this.file.Name;
            //不在这里做，读取头是需要时间的，或者还需要有经纬度地图
            //this.InitLoadHeader();
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

        public async void InitLoadHeader()
        {
            if (this.file != null)
            {
                var readFile = await this.file.OpenReadAsync();
                var stream = readFile.AsStreamForRead();

                using (BinaryReader reader = new BinaryReader(stream))
                {
                    FlightDataEntitiesRT.IFlightRawDataExtractor extractor =
                        this.CreateRawDataExtractorByAircraftModelName(null);
                    if (extractor != null)
                    {
                        this.Header = extractor.GetHeader();
                        extractor.Close();
                    }
                    else this.Header = null;
                }
            }
        }

        private string m_description = string.Empty;

        public string Description
        {
            get
            {
                if (this.Header != null)
                    return this.Header.Description;
                return string.Empty;
            }
            set
            {
                this.SetProperty<string>(ref m_description, value);
            }
        }

        private Windows.UI.Xaml.Visibility m_importDataVisibility = Windows.UI.Xaml.Visibility.Collapsed;

        public Windows.UI.Xaml.Visibility ImportDataVisibility
        {
            get { return m_importDataVisibility; }
            set
            {
                this.SetProperty<Windows.UI.Xaml.Visibility>(ref m_importDataVisibility, value);
            }
        }

        //private FlightDataEntitiesRT.PHYHeader m_header = null;

        //private void BindHeader(FlightDataEntitiesRT.PHYHeader header)
        //{
        //    this.m_header = header;

        //    if (m_header != null)
        //    {
        //        StringBuilder builder = new StringBuilder();
        //        builder.AppendLine(m_header.AircrfName);
        //        builder.AppendLine(m_header.AircrfNum);
        //        builder.AppendLine(m_header.BTime.ToString());
        //        builder.AppendLine(m_header.EndTime.ToString());
        //        builder.AppendLine(m_header.FlyPlanAddr.ToString());
        //        builder.AppendLine(m_header.FlySeconds.ToString());
        //        builder.AppendLine(m_header.GPSEndTime.ToString());
        //        builder.AppendLine(m_header.GPSStartTime.ToString());
        //        builder.AppendLine(m_header.OPName);
        //        builder.AppendLine(m_header.ParaListAddr.ToString());
        //        builder.AppendLine(m_header.PhyValueAddr.ToString());
        //        builder.AppendLine(m_header.PhyValueEndAddr.ToString());
        //        builder.AppendLine(m_header.PNum.ToString());
        //        builder.AppendLine(m_header.StartTime.ToString());
        //        builder.AppendLine(m_header.SWNum.ToString());

        //        this.Description = builder.ToString();

        //        this.UniqueId = m_header.AircrfName + "/" + m_header.AircrfNum;
        //        //Example: F4D/0004
        //    }
        //    else
        //    {
        //        this.UniqueId = string.Empty;
        //    }

        //    if (this.m_header != null)
        //        this.ImportDataVisibility = Windows.UI.Xaml.Visibility.Visible;
        //    else this.ImportDataVisibility = Windows.UI.Xaml.Visibility.Collapsed;
        //}

        //private string m_currentFileName = string.Empty;
        //public string CurrentFileName
        //{
        //    get { return this.m_currentFileName; }
        //    set { this.SetProperty(ref this.m_currentFileName, value); }
        //}

        //private Task m_task = null;

        //internal void ImportData()
        //{
        //    this.Status = AsyncStatus.Started;
        //    this.ErrorCode = null;
        //    this.
        //    m_task = new Task(new Action(delegate()
        //    {
        //        this.DoImportData();
        //    }));
        //    m_task.RunSynchronously();
        //}

        //private async void DoImportData()
        //{
        //    int seconds = FlightDataEntitiesRT.PHYHelper.GetFlyParamSeconds(m_header);
        //    var openStreamTask = this.file.OpenStreamForReadAsync();
        //    var getParamsTask = this.GetParametersAsync();

        //    Stream stream = await openStreamTask;
        //    IEnumerable<FlightDataEntitiesRT.FlyParameter> paramRTs = await getParamsTask;

        //    using (BinaryReader reader = new BinaryReader(stream))
        //    {
        //        for (int current = 0; current <= seconds; current++)
        //        {
        //            this.ReadOneSecondAndImport(current, reader, this.m_header, paramRTs);

        //            int progress = Convert.ToInt32(100.0 * (double)current / (double)seconds);

        //            if (this.Progress != null)
        //            {
        //                this.Progress(this, progress);
        //            }
        //        }
        //    }

        //    if (this.Completed != null)
        //    {
        //        this.Completed(this, this.Status);
        //    }
        //}

        //private Task<IEnumerable<FlightDataEntitiesRT.FlyParameter>> GetParametersAsync()
        //{
        //    Task<IEnumerable<FlightDataEntitiesRT.FlyParameter>> task
        //        = new Task<IEnumerable<FlightDataEntitiesRT.FlyParameter>>(
        //        new Func<IEnumerable<FlightDataEntitiesRT.FlyParameter>>(delegate()
        //    {
        //        //debug
        //        AircraftService.AircraftModel model = new AircraftService.AircraftModel()
        //        {
        //            ModelName = "F4D",
        //            Caption = "",
        //            LastUsed = DateTime.Now
        //        };

        //        AircraftService.AircraftServiceClient client = new AircraftService.AircraftServiceClient();
        //        var modelParamsTask = client.GetAllFlightParametersAsync(model.ModelName);
        //        modelParamsTask.Wait();

        //        var result = modelParamsTask.Result;
        //        var resultRT = from one in result
        //                       select new FlightDataEntitiesRT.FlyParameter()
        //                       {
        //                           Index = one.Index,
        //                           SubIndex = one.SubIndex,
        //                           Caption = one.Caption,
        //                           Frequence = one.Frequence,
        //                           Unit = one.Unit
        //                       };

        //        return resultRT;
        //    }));

        //    task.RunSynchronously();

        //    return task;
        //}

        //private void ReadOneSecondAndImport(int current, BinaryReader reader, FlightDataEntitiesRT.PHYHeader header,
        //    IEnumerable<FlightDataEntitiesRT.FlyParameter> paramList)
        //{
        //    foreach (FlightDataEntitiesRT.FlyParameter parameter in paramList)
        //    {
        //        float[] datas = FlightDataEntitiesRT.PHYHelper.ReadFlyParameter(reader, current, header, parameter);

        //    }
        //}

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

        public RawDataPointViewModel GetRawDataModel()
        {
            if (this.Header == null || this.Header.FlightSeconds <= 0)
                return null;

            RawDataPointViewModel viewModel = new RawDataPointViewModel();

            viewModel.GenerateColumns();

            IFlightRawDataExtractor extractor = this.CreateRawDataExtractorByAircraftModelName(
                viewModel.CurrentFlightParameters);

            var rowModel = viewModel.RawDataRowViewModel;

            var decisions = extractor.GetFaultDecisions();

            Dictionary<Decision, Decision> hasHappendMap = new Dictionary<Decision, Decision>();
            List<DecisionRecord> decisionRecords = new List<DecisionRecord>();

            for (int i = 0; i < this.Header.FlightSeconds; i++)
            {
                ParameterRawData[] datas = extractor.GetDataBySecond(i);

                rowModel.AddOneSecondValue(i, datas);

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
