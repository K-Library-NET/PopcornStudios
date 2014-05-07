using AircraftDataAnalysisWinRT.DataModel;
using AircraftDataAnalysisWinRT.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “基本页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234237 上有介绍

namespace AircraftDataAnalysisWinRT.Domain
{
    /// <summary>
    /// 基本页，提供大多数应用程序通用的特性。
    /// </summary>
    public sealed partial class ImportAircraftBatchConfirm : AircraftDataAnalysisWinRT.Common.LayoutAwarePage
    {
        public ImportAircraftBatchConfirm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 使用在导航过程中传递的内容填充页。在从以前的会话
        /// 重新创建页时，也会提供任何已保存状态。
        /// </summary>
        /// <param name="navigationParameter">最初请求此页时传递给
        /// <see cref="Frame.Navigate(Type, Object)"/> 的参数值。
        /// </param>
        /// <param name="pageState">此页在以前会话期间保留的状态
        /// 字典。首次访问页面时为 null。</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// 保留与此页关联的状态，以防挂起应用程序或
        /// 从导航缓存中放弃此页。值必须符合
        /// <see cref="SuspensionManager.SessionState"/> 的序列化要求。
        /// </summary>
        /// <param name="pageState">要使用可序列化状态填充的空字典。</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private ObservableCollection<AddFileViewModel> m_addFileModels = new ObservableCollection<AddFileViewModel>();

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var openPicker = new Windows.Storage.Pickers.FileOpenPicker();
            openPicker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            openPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.ComputerFolder;
            openPicker.FileTypeFilter.Add(".phy");
            IReadOnlyList<StorageFile> files = await openPicker.PickMultipleFilesAsync();
            if (files != null && files.Count() > 0)
            {
                this.m_addFileModels.Clear();

                bool allCorrect = true;

                foreach (var file in files)
                {
                    var aircraftModel = AircraftDataAnalysisWinRT.ApplicationContext.Instance.CurrentAircraftModel;
                    var flightParameter = AircraftDataAnalysisWinRT.ApplicationContext.Instance.GetFlightParameters(
                        ApplicationContext.Instance.CurrentAircraftModel);
                    FlightDataEntitiesRT.IFlightRawDataExtractor extractor = null;
                    FlightDataEntitiesRT.Flight currentFlight = null;

                    bool correct = CreateTempCurrentFlight(file, aircraftModel, flightParameter, ref extractor, ref currentFlight);

                    AddFileViewModel model = new AddFileViewModel(currentFlight, file, extractor,
                        aircraftModel, flightParameter);
                    model.IsTempFlightParseError = !correct;
                    allCorrect = allCorrect & correct;

                    model.InitLoadHeader();
                    if (model.Header != null)
                    {
                        currentFlight.EndSecond = model.Header.FlightSeconds;
                        if (model.Header.Latitudes != null && model.Header.Longitudes != null)
                            currentFlight.GlobeDatas = AddFileViewModel.ToGlobeDatasStatic(model.Header.Latitudes, model.Header.Longitudes);
                    }
                    this.m_addFileModels.Add(model);
                }

                if (allCorrect == false)
                    this.tbMessage1.Visibility = Windows.UI.Xaml.Visibility.Visible;

                rgdItems.ItemsSource = this.m_addFileModels;
            }
            else
            {
                this.Frame.GoBack();
            }
        }

        /// <summary>
        /// 临时的方法，多个机型后要考虑使用别的方式创建，例如反射
        /// </summary>
        /// <param name="file"></param>
        /// <param name="aircraftModel"></param>
        /// <param name="flightParameter"></param>
        /// <param name="extractor"></param>
        /// <param name="currentFlight"></param>
        private bool CreateTempCurrentFlight(StorageFile file, FlightDataEntitiesRT.AircraftModel aircraftModel, FlightDataEntitiesRT.FlightParameters flightParameter, ref FlightDataEntitiesRT.IFlightRawDataExtractor extractor, ref FlightDataEntitiesRT.Flight currentFlight)
        {
            return PStudio.WinApp.Aircraft.FDAPlatform.MainPage.BuildTempFlightByRule(file, aircraftModel,
                flightParameter, ref extractor, ref currentFlight);

            //if (aircraftModel != null && !string.IsNullOrEmpty(aircraftModel.ModelName))
            //{
            //    //if (aircraftModel.ModelName == "F4D")
            //    //{
            //    var result = FlightDataReading.AircraftModel1.FlightRawDataExtractorFactory
            //        .CreateFlightRawDataExtractor(file, flightParameter);
            //    extractor = result;
            //    //}
            //}
            //currentFlight = new FlightDataEntitiesRT.Flight()
            //{
            //    Aircraft = new FlightDataEntitiesRT.AircraftInstance()
            //    {
            //        AircraftModel = aircraftModel,
            //        AircraftNumber = (extractor as FlightDataReading.AircraftModel1.FlightDataReadingHandler).ParseAircraftNumber(file.Name),
            //        LastUsed = DateTime.Now
            //    },
            //    StartSecond = 0,
            //    FlightName = file.Name,
            //    FlightDate = (extractor as FlightDataReading.AircraftModel1.FlightDataReadingHandler).ParseDate(file.Name),
            //    FlightID = this.RemoveIllegalChars(file.DisplayName)
            //};
        }

        private string RemoveIllegalChars(string p)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char c in p)
            {
                if (char.IsNumber(c))
                    builder.Append(c);
            }
            return builder.ToString();
        }

        private void OncancelImportclicked(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.progressBar1.IsIndeterminate = false;
            this.progressBar1.Minimum = 0;
            this.progressBar1.Maximum = this.m_addFileModels.Count;
            this.progressBar1.Value = 0;
            this.progressBar1.Visibility = Windows.UI.Xaml.Visibility.Visible;
            this.rgdItems.IsEnabled = false;
            this.tbMessage.Text = string.Format("当前正在导入第{0}个文件，共{1}个。这可能需要一些时间，请耐心等待......", 0, this.m_addFileModels.Count);

            Task.Run(new Action(async () =>
            {
                int counter = 1;
                foreach (var one in this.m_addFileModels)
                {
                    await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                        new Windows.UI.Core.DispatchedHandler(delegate()
                        {
                            this.tbMessage.Text = string.Format("当前正在导入第{0}个文件，共{1}个。这可能需要一些时间，请耐心等待......", counter, this.m_addFileModels.Count);
                        }));
                    counter++;

                    this.DoImportCore(one);

                    await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                        new Windows.UI.Core.DispatchedHandler(delegate()
                        {
                            this.progressBar1.Value++;
                        }));
                }

                await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                    new Windows.UI.Core.DispatchedHandler(delegate()
                    {
                        this.tbMessage.Text = "导入完成。";
                        this.rgdItems.IsEnabled = true;
                        this.Frame.GoBack();
                        this.Frame.Navigate(typeof(PStudio.WinApp.Aircraft.FDAPlatform.MainPage), null);
                    }));
            }));
        }

        private void DoImportCore(AddFileViewModel model)
        {
            try
            {
                var parameters = ApplicationContext.Instance.GetFlightParameters(
                    ApplicationContext.Instance.CurrentAircraftModel);

                var extractor = FlightDataReading.AircraftModel1.FlightRawDataExtractorFactory
                    .CreateFlightRawDataExtractor(model.File, parameters);

                IDataReading reading = new DataReading(extractor, model.Flight,
                    parameters);

                reading.Header = model.Header;
                reading.ReadData();
            }
            catch (Exception ex)
            {
                //SERVER log?
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}
