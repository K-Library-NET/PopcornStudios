using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AircraftDataAnalysisWinRT.DataModel;
using AircraftDataAnalysisWinRT.MyControl;
using PStudio.WinApp.Aircraft.FDAPlatform.Domain;
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
using AircraftDataAnalysisWinRT.Services;
using AircraftDataAnalysisWinRT;
using System.Text;
using AircraftDataAnalysisWinRT.Domain;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace PStudio.WinApp.Aircraft.FDAPlatform
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。Parameter
        /// 属性通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ////base.OnNavigatedTo(e);
            this.Loaded += MainPage_Loaded;
            this.Unloaded += MainPage_Unloaded;

            //return;
            //liangdawen 20131028
            //Loading方法应该获取最新架次
            AircraftDataAnalysisWinRT.ApplicationContext.Instance.CurrentAircraftModel
                = ServerHelper.GetCurrentAircraftModel();
            var flights = ServerHelper.GetAllFlights(AircraftDataAnalysisWinRT.ApplicationContext.Instance.CurrentAircraftModel);
            this.grdFlights.ItemsSource = flights;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            this.Loaded -= MainPage_Loaded;
            this.Unloaded -= MainPage_Unloaded;
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            SetCurrentFlight();
        }

        private void SetCurrentFlight()
        {
            if (this.grdFlights.ItemsSource != null && this.grdFlights.ItemsSource is IEnumerable<FlightDataEntitiesRT.Flight>)
            {
                var flights = this.grdFlights.ItemsSource as IEnumerable<FlightDataEntitiesRT.Flight>;
                if (flights != null && flights.Count() > 0 && ApplicationContext.Instance.CurrentFlight == null)
                {
                    this.grdFlights.SelectedIndex = 0;
                }
                else if (flights != null && flights.Count() > 0 && ApplicationContext.Instance.CurrentFlight != null)
                {
                    var f = flights.FirstOrDefault(new Func<FlightDataEntitiesRT.Flight, bool>(
                        delegate(FlightDataEntitiesRT.Flight flight)
                        {
                            if (flight.FlightID == ApplicationContext.Instance.CurrentFlight.FlightID)
                                return true;
                            return false;
                        }));

                    this.grdFlights.SelectedItem = f;
                }
            }
        }

        void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        private void btHistory_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btStatReport_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(StatReport), "StatReport");
        }

        private void btTrendAnalysis_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(TrendAnalysis), "TrendAnalysis");
        }

        private void btEngineMonitoring_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EngineMonitoring), "EngineMonitoring");
        }

        private void btExtremumReport_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ExtremumReport), "ExtremumReport");
        }

        private void btFaultDiagnosis_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(FaultDiagnosis), "FaultDiagnosis");
        }

        private void btFlightAnalysis_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(FlightAnalysis), "FlightAnalysis");
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btSelect_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ImportAircraftBatchConfirm), null);
        }

        private async void btImport_Click(object sender, RoutedEventArgs e)
        {
            var openPicker = new Windows.Storage.Pickers.FileOpenPicker();
            openPicker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            openPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.ComputerFolder;
            openPicker.FileTypeFilter.Add(".phy");
            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                //20131029 liangdawen:
                var aircraftModel = AircraftDataAnalysisWinRT.ApplicationContext.Instance.CurrentAircraftModel;
                var flightParameter = AircraftDataAnalysisWinRT.ApplicationContext.Instance.GetFlightParameters(
                    ApplicationContext.Instance.CurrentAircraftModel);
                FlightDataEntitiesRT.IFlightRawDataExtractor extractor = null;
                FlightDataEntitiesRT.Flight currentFlight = null;

                CreateTempCurrentFlight(file, aircraftModel, flightParameter, ref extractor, ref currentFlight);

                AddFileViewModel model = new AddFileViewModel(currentFlight, file, extractor,
                    aircraftModel, flightParameter);

                this.Frame.Navigate(typeof(AircraftDataAnalysisWinRT.Domain.ImportLoadConfirmPage), model);
                //20131029 liangdawen
                //this.SetLoading(true);
                //await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                //     new Windows.UI.Core.DispatchedHandler(
                //     delegate()
                //     {
                //         var t = System.Threading.Tasks.Task.Delay(5000);
                //         t.Wait();
                //         //debug: 模拟需要时间
                //         model.InitLoadHeader();
                //     }));

                //this.SetLoading(false);
                //ImportAircraftConfirm confirm = new ImportAircraftConfirm();
                //confirm.Show(dialogArea, model);
            }
            else
            {
                //this.ViewModel = null;
                //this.DataContext = null;
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
        private void CreateTempCurrentFlight(StorageFile file, FlightDataEntitiesRT.AircraftModel aircraftModel, FlightDataEntitiesRT.FlightParameters flightParameter, ref FlightDataEntitiesRT.IFlightRawDataExtractor extractor, ref FlightDataEntitiesRT.Flight currentFlight)
        {
            if (aircraftModel != null && !string.IsNullOrEmpty(aircraftModel.ModelName))
            {
                if (aircraftModel.ModelName == "F4D")
                {
                    var result = FlightDataReading.AircraftModel1.FlightRawDataExtractorFactory
                        .CreateFlightRawDataExtractor(file, flightParameter);
                    extractor = result;
                }
            }
            currentFlight = new FlightDataEntitiesRT.Flight()
            {
                Aircraft = new FlightDataEntitiesRT.AircraftInstance()
                {
                    AircraftModel = aircraftModel,
                    AircraftNumber = "1234", //debug
                    LastUsed = DateTime.Now
                },
                StartSecond = 0,
                FlightName = file.Name,
                FlightID = this.RemoveIllegalChars(file.DisplayName)
            };
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

        private void OnCurrentFlightChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.grdFlights.SelectedItem != null && this.grdFlights.SelectedItem is FlightDataEntitiesRT.Flight)
            {
                AircraftDataAnalysisWinRT.ApplicationContext.Instance.CurrentFlight
                    = this.grdFlights.SelectedItem as FlightDataEntitiesRT.Flight;
            }
        }
    }
}
