using AircraftDataAnalysisModel1.WinRT.DataModel;
using AircraftDataAnalysisModel1.WinRT.MyControl;
using AircraftDataAnalysisWinRT.DataModel;
using AircraftDataAnalysisWinRT.MyControl;
using Infragistics.Controls.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace AircraftDataAnalysisWinRT.Domain
{
    /// <summary>
    /// 基本页，提供大多数应用程序通用的特性。
    /// </summary>
    public sealed partial class FlightAnalysisSubLite : AircraftDataAnalysisWinRT.Common.LayoutAwarePage,
        INotifyPropertyChanged
    {
        public FlightAnalysisSubLite()
        {
            this.InitializeComponent();

            this.Loaded += GridDataPage_Loaded;
        }

        private bool m_loaded = false;
        private object m_syncRoot = new object();

        void GridDataPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (m_loaded)
                return;

            this.LoadParameterAndCreateColumnsAsync();

            m_loaded = true;
        }

        private string m_extremumSuffix = string.Empty;

        public string AppName
        {
            get
            {
                return this.GetResourceString("AppName") + m_extremumSuffix;
            }
        }

        private string GetResourceString(string p)
        {
            if (this.Resources.ContainsKey(p) && this.Resources[p] != null)
            {
                return this.Resources[p].ToString();
            }
            return string.Empty;
        }

        private void LoadParameterAndCreateColumnsAsync()
        {
            //this.cbSelectParameter.DataContext = this.DataContext;

            //if (flightParameters == null || flightParameters.Parameters == null
            //    || flightParameters.Parameters.Count() == 0)
            //{
            //    //this.grdData.Columns.Clear();
            //    this.cbSelectParameter.ItemsSource = this.ColumnWrappers;
            //    return;
            //}
            //this.cbSelectParameter.ItemsSource = this.ColumnWrappers;

            //this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
            //    new Windows.UI.Core.DispatchedHandler(() =>
            // {
            //     lock (m_syncRoot)
            //     {
            //         var flightParameters = ApplicationContext.Instance.GetFlightParameters(
            //             ApplicationContext.Instance.CurrentAircraftModel);

            //         this.FlightParameters = flightParameters;

            //         this.ColumnWrappers = new ObservableCollection<ColumnWrapper>();

            //         if (this.FlightParameters == null || this.FlightParameters.Parameters == null
            //             || this.FlightParameters.Parameters.Count() == 0)
            //         {
            //             //this.grdData.Columns.Clear();
            //             this.cbSelectParameter.ItemsSource = this.ColumnWrappers;
            //             return;
            //         }

            //         foreach (var pm in this.FlightParameters.Parameters)
            //         {
            //             ColumnWrapper wrap = new ColumnWrapper(pm);
            //             //this.grdData.Columns.Add(wrap.GridColumn);
            //             this.ColumnWrappers.Add(wrap);
            //         }
            //         this.cbSelectParameter.ItemsSource = this.ColumnWrappers;
            //     }
            // }
            // ));
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

        public FlightDataEntitiesRT.FlightParameters FlightParameters { get; set; }

        //internal ObservableCollection<AircraftDataAnalysisWinRT.DataModel.ColumnWrapper> ColumnWrappers
        //{
        //    get;
        //    set;
        //}

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("Start analysis sub:{0}", DateTime.Now));
            base.OnNavigatedTo(e);
            if (!(e.Parameter is SubEditChartNavigationParameter))
            {
                return;
            }

            SubEditChartNavigationParameter parameter = e.Parameter as SubEditChartNavigationParameter;

            var dataLoader = parameter.DataLoader;
            if (dataLoader == null)
            {
                dataLoader = new AircraftDataAnalysisModel1.WinRT.Domain.AircraftAnalysisDataLoader()
                    {
                        CurrentAircraftModel = ApplicationContext.Instance.CurrentAircraftModel,
                        CurrentFlight = ApplicationContext.Instance.CurrentFlight
                    };
            }

            if (parameter is ISecondRangeDataLoaderNavigator)
            {
                var secNavParameter = parameter as ISecondRangeDataLoaderNavigator;
                tracker1.Axes["xm1YAxis1"].CrossingValue = secNavParameter.MaxValueSecond;
                tracker1.Axes["xm1YAxis2"].CrossingValue = secNavParameter.MinValueSecond;
            }

            if (parameter is ExtremumReportSubEditChartNavigationParameter)
            {
                ExtremumReportSubEditChartNavigationParameter extParameter = parameter as ExtremumReportSubEditChartNavigationParameter;

                this.m_extremumSuffix = "——" + ApplicationContext.Instance.GetFlightParameterCaption(
                    extParameter.HostParameterID) + "极值";
                this.OnPropertyChanged("AppName");

                this.InitializeExtremumInfo(extParameter, dataLoader);

                return;
            }

            this.InitializeAsync(parameter, dataLoader);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string p)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(p));
        }

        private void InitializeExtremumInfo(ExtremumReportSubEditChartNavigationParameter parameter,
            AircraftDataAnalysisModel1.WinRT.Domain.AircraftAnalysisDataLoader dataLoader)
        {
            var Xaxis1 = this.tracker1.Axes["xm1XAxis1"] as CategoryXAxis;
            var Yaxis1 = this.tracker1.Axes["xm1YAxis1"] as NumericYAxis;
            var Xaxis2 = this.tracker1.Axes["xm1XAxis2"] as CategoryXAxis;
            var Yaxis2 = this.tracker1.Axes["xm1YAxis2"] as NumericYAxis;

            Task.Run(new Action(async () =>
            {
                var viewModel = new AircraftDataAnalysisWinRT.DataModel.FlightAnalysisSubLiteViewModel();
                List<string> parameterIDs = new List<string>();
                GetThisParameterID(parameter, parameterIDs);

                //grouped : 
                var groupedIDs = AircraftDataAnalysisModel1.WinRT.DataModel.FlightAnalysisChartGroupFactory.CalculateBindingGroups(parameterIDs);

                if (groupedIDs == null)
                    return;
                var groupCount = groupedIDs.Count();
                if (groupCount <= 0)
                    return;
                //极值报表只能一个分组，X轴和Y轴，必须两个轴都设置，才能出红蓝线
                dataLoader.LoadRawDataAsync(parameterIDs);

                //第一个分组

                List<Series> series = new List<Series>();

                Dictionary<int, SimpleDataPoint> pointMapper = new Dictionary<int, SimpleDataPoint>();
                for (int i = 0; i < dataLoader.CurrentFlight.EndSecond; i++)
                {
                    pointMapper.Add(i, new SimpleDataPoint() { Second = i });
                }

                var sorted = from one in pointMapper.Values
                             orderby one.Second ascending
                             select one;
                FlightAnalysisChartSerieViewModel chartviewModel = new FlightAnalysisChartSerieViewModel(sorted);

                var group1 = groupedIDs.First();
                int counter = 0;
                int counterStart = 0;
                viewModel.Group1ID = group1.Key;
                AssignPointMapperValue(dataLoader, pointMapper, ref group1, ref counter);

                await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                    new Windows.UI.Core.DispatchedHandler(() =>
                    {
                        AssignSerie(Xaxis1, Yaxis1, series, chartviewModel, counter, counterStart, parameterIDs);
                    }));

                if (true || groupCount > 1)
                {
                    counterStart = 0;//counter;
                    var group2 = groupedIDs.First(); //groupedIDs.ElementAt(1);
                    viewModel.Group2ID = group2.Key;
                    AssignPointMapperValue(dataLoader, pointMapper, ref group2, ref counter);
                    counter = 1;

                    await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                        new Windows.UI.Core.DispatchedHandler(() =>
                        {
                            AssignSerie(Xaxis2, Yaxis2, series, chartviewModel, counter, counterStart, parameterIDs);
                        }));
                }

                viewModel.ChartViewModel = chartviewModel;

                this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                    new Windows.UI.Core.DispatchedHandler(() =>
                    {
                        this.TitleLegend.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        //必须去掉Legend，因为两条线是可能出两个Legend的
                        //加参数的Stack也要去掉
                        this.stackAddParam.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        //Yaxis2.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        Yaxis2.LabelSettings.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                        //this.DataContext = viewModel;
                        //viewModel.SelectedParameterIDChanged += viewModel_SelectedParameterIDChanged;
                    }));

                this.InitFinalizeRunAsync(dataLoader, Xaxis1, Yaxis1, Xaxis2, Yaxis2,
                    viewModel, series, chartviewModel);
            }));
        }

        private void InitializeAsync(SubEditChartNavigationParameter parameter,
            AircraftDataAnalysisModel1.WinRT.Domain.AircraftAnalysisDataLoader dataLoader)
        {
            var Xaxis1 = this.tracker1.Axes["xm1XAxis1"] as CategoryXAxis;
            var Yaxis1 = this.tracker1.Axes["xm1YAxis1"] as NumericYAxis;
            var Xaxis2 = this.tracker1.Axes["xm1XAxis2"] as CategoryXAxis;
            var Yaxis2 = this.tracker1.Axes["xm1YAxis2"] as NumericYAxis;

            Task.Run(new Action(async () =>
            {
                var viewModel = new AircraftDataAnalysisWinRT.DataModel.FlightAnalysisSubLiteViewModel();
                List<string> parameterIDs = new List<string>();
                GetThisParameterID(parameter, parameterIDs);

                //grouped : 
                var groupedIDs = AircraftDataAnalysisModel1.WinRT.DataModel.FlightAnalysisChartGroupFactory.CalculateBindingGroups(parameterIDs);

                if (groupedIDs == null)
                    return;
                var groupCount = groupedIDs.Count();
                if (groupCount <= 0)
                    return;
                //只能两个分组，X轴和Y轴
                dataLoader.LoadRawDataAsync(parameterIDs);

                //第一个分组

                List<Series> series = new List<Series>();

                Dictionary<int, SimpleDataPoint> pointMapper = new Dictionary<int, SimpleDataPoint>();
                for (int i = 0; i < dataLoader.CurrentFlight.EndSecond; i++)
                {
                    pointMapper.Add(i, new SimpleDataPoint() { Second = i });
                }

                var sorted = from one in pointMapper.Values
                             orderby one.Second ascending
                             select one;
                FlightAnalysisChartSerieViewModel chartviewModel = new FlightAnalysisChartSerieViewModel(sorted);

                var group1 = groupedIDs.First();
                int counter = 0;
                int counterStart = 0;
                viewModel.Group1ID = group1.Key;
                AssignPointMapperValue(dataLoader, pointMapper, ref group1, ref counter);

                await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                    new Windows.UI.Core.DispatchedHandler(() =>
                    {
                        AssignSerie(Xaxis1, Yaxis1, series, chartviewModel, counter, counterStart, parameterIDs);
                    }));

                if (groupCount > 1)
                {
                    counterStart = counter;
                    var group2 = groupedIDs.ElementAt(1);
                    viewModel.Group2ID = group2.Key;
                    AssignPointMapperValue(dataLoader, pointMapper, ref group2, ref counter);

                    await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                        new Windows.UI.Core.DispatchedHandler(() =>
                        {
                            AssignSerie(Xaxis2, Yaxis2, series, chartviewModel, counter, counterStart, parameterIDs);
                        }));
                }

                InitFinalizeRunAsync(dataLoader, Xaxis1, Yaxis1, Xaxis2, Yaxis2, viewModel, series, chartviewModel);
            }));
        }

        private void InitFinalizeRunAsync(AircraftDataAnalysisModel1.WinRT.Domain.AircraftAnalysisDataLoader dataLoader, CategoryXAxis Xaxis1, NumericYAxis Yaxis1, CategoryXAxis Xaxis2, NumericYAxis Yaxis2, FlightAnalysisSubLiteViewModel viewModel, List<Series> series, FlightAnalysisChartSerieViewModel chartviewModel)
        {
            this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                new Windows.UI.Core.DispatchedHandler(() =>
                {
                    viewModel.ChartViewModel = chartviewModel;

                    this.DataContext = viewModel;
                    viewModel.SelectedParameterIDChanged += viewModel_SelectedParameterIDChanged;
                    //    }));

                    //this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                    //    new Windows.UI.Core.DispatchedHandler(() =>
                    //{
                    this.DataLoader = dataLoader;

                    Xaxis1.DataContext = chartviewModel;
                    Yaxis1.DataContext = chartviewModel;
                    Xaxis2.DataContext = chartviewModel;
                    Yaxis2.DataContext = chartviewModel;
                    foreach (var se in series)
                    {
                        this.tracker1.Series.Add(se);
                    }

                    this.progbar1.IsIndeterminate = false;
                    this.progbar1.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                }));
        }

        void viewModel_SelectedParameterIDChanged(object sender, EventArgs e)
        {
            FlightAnalysisSubLiteViewModel viewModel = this.GetViewModel();
            if (viewModel == null || viewModel.ChartViewModel == null
                || this.DataLoader == null)
                return;

            var tempArray = new string[] { viewModel.SelectedParameterID };

            this.DataLoader.LoadRawDataAsync(tempArray);
            var tempGroup = AircraftDataAnalysisModel1.WinRT.DataModel.FlightAnalysisChartGroupFactory.CalculateBindingGroups(tempArray);
            //Group must be only one
            var gpName = tempGroup.First().Key;

            var rawDatas = DataLoader.GetRawData(viewModel.SelectedParameterID);
            Dictionary<int, SimpleDataPoint> pointMapper = new Dictionary<int, SimpleDataPoint>();
            foreach (var item in viewModel.ChartViewModel)
            {
                if (!pointMapper.ContainsKey(item.Second))
                {
                    pointMapper.Add(item.Second, item);
                }
            }

            foreach (var raw in rawDatas)
            {
                if (pointMapper.ContainsKey(raw.Second))
                {
                    pointMapper[raw.Second].Value = raw.Values[0];
                }
            }

            var Xaxis1 = this.tracker1.Axes["xm1XAxis1"] as CategoryXAxis;
            var Yaxis1 = this.tracker1.Axes["xm1YAxis1"] as NumericYAxis;
            var Xaxis2 = this.tracker1.Axes["xm1XAxis2"] as CategoryXAxis;
            var Yaxis2 = this.tracker1.Axes["xm1YAxis2"] as NumericYAxis;
            //Left YAxis    
            var xaxis = Xaxis1;
            var yaxis = Yaxis1;

            if (gpName != viewModel.Group1ID)
            {                //Right YAxis
                xaxis = Xaxis2;
                yaxis = Yaxis2;
            }

            Series temp = this.tracker1.Series["addParamSerie"];
            if (temp != null)
                this.tracker1.Series.Remove(temp);

            LineSeries serie = new LineSeries() { Name = "addParamSerie" };
            serie.XAxis = xaxis;
            serie.ItemsSource = viewModel.ChartViewModel;
            serie.SetBinding(LineSeries.VisibilityProperty, new Binding()
            {
                Path = new PropertyPath("CompareParameterSerieVisibility"),
                Source = viewModel
            });
            serie.YAxis = yaxis;
            serie.Title = ApplicationContext.Instance.GetFlightParameterCaption(viewModel.SelectedParameterID);
            serie.Legend = this.TitleLegend;
            serie.MarkerType = MarkerType.None;
            serie.ValueMemberPath = "Value";
            serie.DataContext = viewModel;

            this.tracker1.Series.Add(serie);
        }

        private FlightAnalysisSubLiteViewModel GetViewModel()
        {
            if (this.DataContext != null && this.DataContext is FlightAnalysisSubLiteViewModel)
                return this.DataContext as FlightAnalysisSubLiteViewModel;

            return null;
        }

        private static void GetThisParameterID(SubEditChartNavigationParameter parameter, List<string> parameterIDs)
        {
            parameterIDs.Add(parameter.HostParameterID);
            if (parameter.RelatedParameterIDs != null && parameter.RelatedParameterIDs.Length > 0)
            {
                var sel = from i in parameter.RelatedParameterIDs
                          select i.RelatedParameterID;
                parameterIDs.AddRange(sel);
            }
        }

        private void AssignSerie(CategoryXAxis Xaxis, NumericYAxis Yaxis,
            List<Series> series, FlightAnalysisChartSerieViewModel viewModel, int counter, int counterStart,
            List<string> parameterIDs)
        {
            for (int j = counterStart; j < counter; j++)
            {
                LineSeries serie = new LineSeries()
                {
                    ItemsSource = viewModel,
                    XAxis = Xaxis,
                    Thickness = 3,
                    Title = ApplicationContext.Instance.GetFlightParameterCaption(parameterIDs[j]),
                    YAxis = Yaxis,
                    MarkerType = MarkerType.None,
                    Legend = this.TitleLegend,
                };

                if (j == 0)
                {
                    serie.ValueMemberPath = "Value1";
                }
                else if (j == 1)
                {
                    serie.ValueMemberPath = "Value2";
                }
                else if (j == 2)
                {
                    serie.ValueMemberPath = "Value3";
                }
                else
                {
                    serie.ValueMemberPath = "Value";
                }

                series.Add(serie);
            }
        }

        private void AssignPointMapperValue(AircraftDataAnalysisModel1.WinRT.Domain.AircraftAnalysisDataLoader dataLoader,
            Dictionary<int, SimpleDataPoint> pointMapper, ref KeyValuePair<string, IEnumerable<string>> group1, ref int counter)
        {
            foreach (var id in group1.Value)
            {
                counter++;

                var rawDatas = dataLoader.GetRawData(id);

                foreach (var raw in rawDatas)
                {
                    if (pointMapper.ContainsKey(raw.Second))
                    {
                        if (counter == 1)
                        {
                            pointMapper[raw.Second].Value1 = raw.Values[0];
                        }
                        else if (counter == 2)
                        {
                            pointMapper[raw.Second].Value2 = raw.Values[0];
                        }
                        else if (counter == 3)
                        {
                            pointMapper[raw.Second].Value3 = raw.Values[0];
                        }
                    }
                }
            }
        }

        public AircraftDataAnalysisModel1.WinRT.Domain.AircraftAnalysisDataLoader DataLoader { get; set; }
    }


    [Obsolete("Old")]
    class ParameterRawDataWrap
    {
        private FlightDataEntitiesRT.ParameterRawData rawData;

        public ParameterRawDataWrap(FlightDataEntitiesRT.ParameterRawData obj)
        {
            this.rawData = obj;
        }

        public int Second
        {
            get
            {
                return rawData.Second;
            }
        }

        public double Value
        {
            get
            {
                return Convert.ToDouble(rawData.Values[0]);
            }
        }
    }

    //[Obsolete("Old")]
    //private ObservableCollection<ParameterRawDataWrap> ToWrapObjs(
    //     ObservableCollection<FlightDataEntitiesRT.ParameterRawData> rawdatas)
    //{
    //    var result = from one in rawdatas
    //                 select new ParameterRawDataWrap(one);

    //    if (result != null && result.Count() > 0)
    //        return new ObservableCollection<ParameterRawDataWrap>(result);//result.Take(1000));//DEBUG，为了速度

    //    return new ObservableCollection<ParameterRawDataWrap>();
    //}

}
