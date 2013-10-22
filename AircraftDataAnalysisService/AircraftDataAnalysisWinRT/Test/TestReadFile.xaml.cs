using AircraftDataAnalysisWinRT.DataModel;
using AircraftDataAnalysisWinRT.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

namespace AircraftDataAnalysisWinRT.Test
{
    /// <summary>
    /// 基本页，提供大多数应用程序通用的特性。
    /// </summary>
    public sealed partial class TestReadFile : AircraftDataAnalysisWinRT.Common.LayoutAwarePage
    {
        public TestReadFile()
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var openPicker = new Windows.Storage.Pickers.FileOpenPicker();
            openPicker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            openPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.ComputerFolder; 
            openPicker.FileTypeFilter.Add(".phy");// ([".png", ".jpg", ".jpeg"]);
            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                var aircraftModel = ServerHelper.GetCurrentAircraftModel();
                var flightParameter = ServerHelper.GetFlightParameters(aircraftModel);
                FlightDataEntitiesRT.IFlightRawDataExtractor extractor = null;

                if (aircraftModel != null && !string.IsNullOrEmpty(aircraftModel.ModelName))
                {
                    if (aircraftModel.ModelName == "F4D")
                    {
                        var result = FlightDataReading.AircraftModel1.FlightRawDataExtractorFactory
                            .CreateFlightRawDataExtractor(file, flightParameter);
                        extractor = result;
                    }
                }
                this.CurrentFlight = new FlightDataEntitiesRT.Flight()
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

                AddFileViewModel model = new AddFileViewModel(this.CurrentFlight,
                    file, extractor, aircraftModel, flightParameter);
                model.InitLoadHeader();
                this.ViewModel = model;
                if (this.ViewModel != null && this.ViewModel.Header != null)
                {
                    //this.CurrentFlight = new FlightDataEntitiesRT.Flight()
                    //{
                    //    Aircraft = new FlightDataEntitiesRT.AircraftInstance()
                    //    {
                    //        AircraftModel =
                    //            ServerHelper.GetCurrentAircraftModel(),
                    //        AircraftNumber = "1234", //debug
                    //        LastUsed = DateTime.Now
                    //    },
                    //    EndSecond = model.Header.FlightSeconds,
                    //    StartSecond = 0,
                    //    FlightName = file.Name,
                    //    FlightID = this.RemoveIllegalChars(file.DisplayName)
                    //};
                }
                else { this.CurrentFlight = null; }
                this.flightHost.DataContext = this.CurrentFlight;
            }
            else
            {
                this.ViewModel = null;
                this.DataContext = null;
            }

            this.tbMessage.Text = "Command completed " + DateTime.Now.ToString();
        }

        private string RemoveIllegalChars(string p)
        {
            //System.Text.RegularExpressions.Regex regex 
            //    = new System.Text.RegularExpressions.Regex("^[a-zA-Z][a-zA-Z0-9_]*$");
            //var matchCollection = regex.Match(p);
            StringBuilder builder = new StringBuilder();
            foreach (char c in p)
            {
                if (char.IsNumber(c))
                    builder.Append(c);
            }
            //foreach (var m in matchCollection.Groups)
            //{
            //    builder.Append(m);
            //}
            return builder.ToString();
        }

        public AddFileViewModel ViewModel { get; set; }

        private void readToDb_Click(object sender, RoutedEventArgs e)
        {
            if (this.ViewModel == null)
                return;

            this.ViewModel.Progress += new AsyncActionProgressHandler<int>(this.OnProgressChanged);
            this.ViewModel.Completed = new AsyncActionWithProgressCompletedHandler<int>(this.OnCompleted);

            this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                new Windows.UI.Core.DispatchedHandler(
                    delegate()
                    {
                        var parameters = ServerHelper.GetFlightParameters(ServerHelper.GetCurrentAircraftModel());

                        var extractor = FlightDataReading.AircraftModel1.FlightRawDataExtractorFactory
                            .CreateFlightRawDataExtractor(this.ViewModel.File, parameters);

                        DataReading reading = new DataReading(extractor, this.CurrentFlight,
                            parameters);

                        reading.ReadData();

                        //this.gridData.AutoGenerateColumns = false;
                        //this.gridData.Columns.Clear();
                        //var rawDataModel = this.ViewModel.GetRawDataModel();
                        //foreach (var col in rawDataModel.ColumnCollection)
                        //{
                        //    this.gridData.Columns.Add(col);
                        //}
                        //this.gridData.ItemsSource = rawDataModel.RawDataRowViewModel;
                    }));
        }

        private void OnProgressChanged(IAsyncActionWithProgress<int> asyncInfo, int progress)
        {
            if (this.ViewModel.Status == AsyncStatus.Started)
                this.progressBar1.Visibility = Windows.UI.Xaml.Visibility.Visible;
            else
                this.progressBar1.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            if (progress > 30)
            {//可以读取首页
                this.DoDisplayDataAsync();
            }
        }

        private void DoDisplayDataAsync()
        {
            //this.ViewModel.ReadRawData();
        }

        private void OnCompleted(IAsyncActionWithProgress<int> progress, AsyncStatus status)
        {
            this.progressBar1.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            this.ViewModel.Progress -= new AsyncActionProgressHandler<int>(this.OnProgressChanged);
            this.ViewModel.Completed -= new AsyncActionWithProgressCompletedHandler<int>(this.OnCompleted);
        }

        private void AddOrReplaceFlightclicked(object sender, RoutedEventArgs e)
        {
            if (this.CurrentFlight != null)
            {
                DataInputHelper.AddOrReplaceFlight(this.CurrentFlight);
                this.tbMessage.Text = string.Format("Flight Added Or replaced: {0} ", DateTime.Now.ToString());

                this.RefreshFlights();
            }
        }

        private void RefreshFlights()
        {
            var flights = ServerHelper.GetAllFlights(ServerHelper.GetCurrentAircraftModel());
            this.lbFlights.ItemsSource = flights;
            this.tbMessage.Text = string.Format("Flight refreshed: {0} ", DateTime.Now.ToString());
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.RefreshFlights();
        }

        public FlightDataEntitiesRT.Flight CurrentFlight { get; set; }

        private void OnPreViewdataClicked(object sender, RoutedEventArgs e)
        {
            if (this.ViewModel == null)
                return;

            this.ViewModel.Progress += new AsyncActionProgressHandler<int>(this.OnProgressChanged);
            this.ViewModel.Completed = new AsyncActionWithProgressCompletedHandler<int>(this.OnCompleted);

            this.gridData.AutoGenerateColumns = false;
            this.gridData.Columns.Clear();
            int start = 0;
            int end = 200;
            var rawDataModel = this.ViewModel.GetPreviewRawDataModel(start, end);
            foreach (var col in rawDataModel.ColumnCollection)
            {
                this.gridData.Columns.Add(col);
            }
            this.gridData.ItemsSource = rawDataModel.RawDataRowViewModel;
        }
    }
}
