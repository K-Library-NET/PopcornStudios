using AircraftDataAnalysisWinRT.DataModel;
using AircraftDataAnalysisWinRT.Styles;
using Infragistics.Controls.Charts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “基本页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234237 上有介绍

namespace PStudio.WinApp.Aircraft.FDAPlatform.Domain
{
    /// <summary>
    /// 基本页，提供大多数应用程序通用的特性。
    /// </summary>
    public sealed partial class EngineMonitoring : AircraftDataAnalysisWinRT.Common.LayoutAwarePage
    {
        public EngineMonitoring()
        {
            this.InitializeComponent();
        }

        class EngineMonitoringModel : AircraftDataAnalysisWinRT.Common.BindableBase
        {
            public EngineMonitoringModel(EngineMonitoring engineMonitoring)
            {
                // TODO: Complete member initialization
                this.engineMonitoring = engineMonitoring;
            }

            private bool m_ist6lSelected = true;
            private EngineMonitoring engineMonitoring;

            public bool IsT6LSelected
            {
                get { return m_ist6lSelected; }
                set
                {
                    m_ist6lSelected = value;
                    this.OnPropertyChanged(string.Empty);
                }
            }

            public bool IsT6RSelected
            {
                get
                {
                    return !m_ist6lSelected;
                }
                set
                {
                    m_ist6lSelected = !value;
                    this.OnPropertyChanged(string.Empty);
                }
            }
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            EngineMonitorNavigationParameter parameter = this.GetNavigatedParameter(e.Parameter);

            this.m_model = this.GetStaticResourceModel("HostViewModel");

            if (m_switcher)
                return;

            try
            {
                m_switcher = true;
                m_model.IsT6LSelected = parameter.IsT6LSelected;
                SetFlights(parameter);

                int counter = 0;

                foreach (string flightID in parameter.SelectedFlights)
                {
                    if (m_model.ItemsMap.ContainsKey(flightID))
                    {
                        this.AddData(flightID, counter);
                        counter++;

                        continue;
                    }

                    this.Frame.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                        new Windows.UI.Core.DispatchedHandler(
                            delegate()
                            {
                                var points = this.GetFlightRawDataRelationPoints(parameter.IsT6LSelected, flightID);

                                this.m_model.AddItems(points, flightID, m_model.IsT6LSelected);

                                if (!string.IsNullOrEmpty(flightID))
                                {
                                    this.AddData(flightID, counter);
                                    counter++;
                                }
                            }
                        )
                    );
                }

                this.Counter = counter;
            }
            catch (Exception ex)
            {
                AircraftDataAnalysisWinRT.LogHelper.Error(ex);
            }
            finally
            {
                m_switcher = false;
            }
        }

        private IEnumerable<FlightDataEntitiesRT.FlightRawDataRelationPoint>
            GetFlightRawDataRelationPoints(bool isT6LSelected, string flightID)
        {
            string XAxisParameterID = "Tt";
            string YAxisParameterID = isT6LSelected ? "T6L" : "T6R";

            return AircraftDataAnalysisWinRT.Services.ServerHelper.GetFlightRawDataRelationPoints(
                                   AircraftDataAnalysisWinRT.ApplicationContext.Instance.CurrentAircraftModel, flightID,
                                   XAxisParameterID, YAxisParameterID);
        }

        private EngineMonitorNavigationParameter GetNavigatedParameter(object parameter)
        {
            if (parameter != null && parameter is EngineMonitorNavigationParameter)
            {
                return parameter as EngineMonitorNavigationParameter;
            }

            EngineMonitorNavigationParameter param = new EngineMonitorNavigationParameter() { IsT6LSelected = true };

            param.SelectedFlights = new string[] { AircraftDataAnalysisWinRT.ApplicationContext.Instance.CurrentFlight.FlightID };

            return param;
        }

        private void AddData(string flightID, int counter)
        {
            Series serie = this.chartT6L.Series[counter % 4];
            if (serie != null && serie is ScatterSeries)
            {
                ScatterSeries serie1 = serie as ScatterSeries;
                serie1.ItemsSource = m_model.ItemsMap[flightID].T6LViewModel;
                serie1.Title = flightID;
                serie1.MarkerBrush = AircraftDataAnalysisGlobalPallete.Brushes[counter % 10];
                serie1.MarkerType = PalleteMarkerTypes.MarkerTypes[counter % 10];
            }
            else
            {
                ScatterSeries serieT6L = new ScatterSeries()
                {
                    XAxis = numericXAxis1,
                    YAxis = numericYAxis1,
                    XMemberPath = "XValue",
                    YMemberPath = "YValue"
                };
                serieT6L.MarkerBrush = AircraftDataAnalysisGlobalPallete.Brushes[counter % 4];
                serieT6L.MarkerType = PalleteMarkerTypes.MarkerTypes[counter % 4];
                serieT6L.DataContext = m_model.ItemsMap[flightID];
                serieT6L.ItemsSource = m_model.ItemsMap[flightID].T6LViewModel;

                this.chartT6L.Series.Add(serieT6L);
            }

            serie = this.chartT6R.Series[counter % 4];
            if (serie != null && serie is ScatterSeries)
            {
                ScatterSeries serie2 = serie as ScatterSeries;
                serie2.ItemsSource = m_model.ItemsMap[flightID].T6LViewModel;
                serie2.MarkerBrush = AircraftDataAnalysisGlobalPallete.Brushes[(counter % 4) + 4];
                serie2.MarkerType = PalleteMarkerTypes.MarkerTypes[(counter % 4) + 4];
            }
            else
            {
                ScatterSeries serieT6R = new ScatterSeries()
                {
                    XAxis = numericXAxis2,
                    YAxis = numericYAxis2,
                    XMemberPath = "XValue",
                    YMemberPath = "YValue"
                };
                serieT6R.MarkerBrush = AircraftDataAnalysisGlobalPallete.Brushes[(counter % 4) + 4];
                serieT6R.MarkerType = PalleteMarkerTypes.MarkerTypes[(counter % 4) + 4];
                serieT6R.ItemsSource = m_model.ItemsMap[flightID].T6RViewModel;

                this.chartT6R.Series.Add(serieT6R);
            }
        }

        private void RemoveData(string flightID, int counter)
        {
            try
            {
                Series serie = this.chartT6L.Series[counter - 1 % 4];
                if (serie != null && serie is ScatterSeries && serie.Title == flightID)
                {
                    ScatterSeries serie1 = serie as ScatterSeries;
                    serie1.ItemsSource = null;
                    //m_model.ItemsMap[flightID].T6LViewModel;
                    //serie1.Title = flightID;
                    //serie1.MarkerBrush = AircraftDataAnalysisGlobalPallete.Brushes[counter % 10];
                    //serie1.MarkerType = PalleteMarkerTypes.MarkerTypes[counter % 10];
                }
            }
            catch (Exception e)
            {
                AircraftDataAnalysisWinRT.LogHelper.Error(e);
            }
        }

        private IEnumerable<FlightSelectItem> GetSelectedFlight()
        {
            var result = from one in this.m_model.FlightViewModel
                         where one.IsSelected
                         select one;

            return result;
        }

        private void SetFlights(EngineMonitorNavigationParameter parameter)
        {
            var flights = AircraftDataAnalysisWinRT.Services.ServerHelper.GetAllFlights(
                AircraftDataAnalysisWinRT.ApplicationContext.Instance.CurrentAircraftModel,
                AircraftDataAnalysisWinRT.ApplicationContext.Instance.CurrentFlight.Aircraft);

            foreach (var flight in flights)
            {
                this.m_model.FlightViewModel.Add(new FlightSelectItem(flight)
                {
                    IsSelected =
                    parameter.SelectedFlights.Contains(flight.FlightID)
                    //(flight.FlightID == AircraftDataAnalysisWinRT.ApplicationContext.Instance.CurrentFlight.FlightID)
                });
            }

            foreach (var fl in this.m_model.FlightViewModel)
            {
                fl.PropertyChanged += fl_PropertyChanged;
            }
        }

        void fl_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is FlightSelectItem)
            {
                FlightSelectItem item = sender as FlightSelectItem;

                if (item.IsSelected)
                {
                    if (this.m_model.ItemsMap.ContainsKey(item.Flight.FlightID))
                    {
                        this.AddData(item.Flight.FlightID, this.Counter);
                        this.Counter++;
                        return;
                    }

                    var taskPoints = this.GetFlightRawDataRelationPointsAsync(m_model.IsT6LSelected, item.Flight.FlightID);

                    taskPoints.ContinueWith(new Action<Task<FlightDataEntitiesRT.FlightRawDataRelationPoint[]>>(
                        delegate(Task<FlightDataEntitiesRT.FlightRawDataRelationPoint[]> task)
                        {
                            taskPoints.Wait();
                            var points = taskPoints.Result;

                            this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                                new Windows.UI.Core.DispatchedHandler(delegate()
                            {
                                this.m_model.AddItems(points, item.Flight.FlightID, m_model.IsT6LSelected);

                                if (!string.IsNullOrEmpty(item.Flight.FlightID))
                                {
                                    this.AddData(item.Flight.FlightID, this.Counter);
                                    this.Counter++;
                                }
                            }));
                        }));
                }
                else
                {
                    this.RemoveData(item.Flight.FlightID, this.Counter);
                    this.Counter--;
                }
            }
            //throw new NotImplementedException();
        }


        private Task<FlightDataEntitiesRT.FlightRawDataRelationPoint[]>
            GetFlightRawDataRelationPointsAsync(bool isT6LSelected, string flightID)
        {
            string XAxisParameterID = "Tt";
            string YAxisParameterID = isT6LSelected ? "T6L" : "T6R";

            return AircraftDataAnalysisWinRT.Services.ServerHelper.GetFlightRawDataRelationPointsAsync(
                                   AircraftDataAnalysisWinRT.ApplicationContext.Instance.CurrentAircraftModel, flightID,
                                   XAxisParameterID, YAxisParameterID);
        }

        private EngineMonitoringViewModel GetStaticResourceModel(string p)
        {
            object val = null;
            if (this.Resources.TryGetValue(p, out val))
            {
                if (val != null && val is EngineMonitoringViewModel)
                    return val as EngineMonitoringViewModel;
            }

            return new EngineMonitoringViewModel();
        }

        #region old test
        //            XamDataChart chart = new XamDataChart();
        //            var data = new SimpleDataCollection();
        //            chart.DataContext = data;
        //            CategoryXAxis xAxis = this.CreateXAxis(data);
        //            NumericYAxis[] yAxis = this.CreateYAxis(data);

        //            LineSeries serie = new LineSeries()
        //            {
        //                ItemsSource = data, // vm.ViewModel.RawDatas,
        //                XAxis = xAxis,
        //                Brush = new SolidColorBrush(Windows.UI.Colors.Black),
        //                MarkerType = Infragistics.Controls.Charts.MarkerType.None,
        //                Thickness = 2,
        //                YAxis = yAxis[0],
        //                ValueMemberPath = "Value"//vm.Parameter.ParameterID,
        //            };
        //            chart.Axes.Add(xAxis);
        //            chart.Axes.Add(yAxis[0]);
        //            chart.Series.Add(serie);

        ////            this.hostBorder.Child = chart;
        //        }
        //        private CategoryXAxis CreateXAxis(SimpleDataCollection data)
        //        {
        //            CategoryXAxis xaxis = new CategoryXAxis()
        //            {
        //                Label = "{Label}",
        //                Foreground = new SolidColorBrush(Windows.UI.Colors.Black),
        //                FontSize = 18,
        //                LabelSettings = new AxisLabelSettings(),
        //                ItemsSource = data//m_viewModel.RawDatas,
        //            };

        //            xaxis.LabelSettings.Extent = 35;
        //            xaxis.LabelSettings.Location = AxisLabelsLocation.OutsideBottom;

        //            return xaxis;
        //        }

        //        private NumericYAxis[] CreateYAxis(SimpleDataCollection data)
        //        {
        //            NumericYAxis xmYAxis = new NumericYAxis()
        //            {
        //                LabelSettings = new AxisLabelSettings(),
        //                FontSize = 18,
        //                Foreground = new SolidColorBrush(Windows.UI.Colors.Black),
        //            };
        //            xmYAxis.LabelSettings.Extent = 55;
        //            xmYAxis.LabelSettings.Location = AxisLabelsLocation.OutsideLeft;

        //            return new NumericYAxis[] { xmYAxis };
        //        }

        //        private void sfchart_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        //        {

        //            System.Diagnostics.Debug.WriteLine("Double Tapped:+" + sender.GetHashCode().ToString());
        //        }
        #endregion

        private bool m_switcher = false;
        private EngineMonitoringViewModel m_model;

        private void btnT6LSelect_Click(object sender, RoutedEventArgs e)
        {
            if (m_model.IsT6LSelected)
                return;

            EngineMonitorNavigationParameter parameter = this.GenerateParameter(true);
            this.Frame.Navigate(typeof(EngineMonitoring), parameter);
        }

        private EngineMonitorNavigationParameter GenerateParameter(bool t6lSelected)
        {
            EngineMonitorNavigationParameter parameter = new EngineMonitorNavigationParameter()
            {
                IsT6LSelected = t6lSelected
            };

            var flights = this.GetSelectedFlight();
            var result = from one in flights
                         select one.Flight.FlightID;

            parameter.SelectedFlights = result.ToArray();

            return parameter;
        }

        private void btnT6RSelect_Click(object sender, RoutedEventArgs e)
        {
            if (m_model.IsT6RSelected)
                return;

            EngineMonitorNavigationParameter parameter = this.GenerateParameter(false);
            this.Frame.Navigate(typeof(EngineMonitoring), parameter);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        public int Counter { get; set; }
    }

    class BooleanColorBrushConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && Boolean.TrueString.Equals(value.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                return new SolidColorBrush(Windows.UI.Colors.Gold);
            }

            return new SolidColorBrush(Windows.UI.Colors.White);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    class BooleanVisibilityConverter : IValueConverter
    {
        public BooleanVisibilityConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && Boolean.TrueString.Equals(value.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

    }

    class EngineMonitorNavigationParameter
    {
        public string[] SelectedFlights { get; set; }
        public bool IsT6LSelected { get; set; }
    }
}
