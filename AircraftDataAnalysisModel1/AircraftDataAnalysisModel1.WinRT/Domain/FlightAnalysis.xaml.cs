using AircraftDataAnalysisModel1.WinRT.MyControl;
using AircraftDataAnalysisWinRT;
using AircraftDataAnalysisWinRT.DataModel;
using AircraftDataAnalysisWinRT.Domain;
using Infragistics.Controls.Charts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
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
    public sealed partial class FlightAnalysis : AircraftDataAnalysisWinRT.Common.LayoutAwarePage,
        AircraftDataAnalysisModel1.WinRT.MyControl.ITrackerParent
    {
        public FlightAnalysis()
        {
            this.InitializeComponent();

            m_charts.Add(this.tracker1);
            m_charts.Add(this.tracker2);
            m_charts.Add(this.tracker3);
            m_charts.Add(this.tracker4);
            m_charts.Add(this.tracker5);
            m_charts.Add(this.tracker6);
            m_charts.Add(this.tracker7);
        }

        #region tracking point function

        private List<Infragistics.Controls.Charts.XamDataChart> m_charts
            = new List<Infragistics.Controls.Charts.XamDataChart>();

        public void NotifyOtherTracker(object sender, PointerRoutedEventArgs e)
        {
            foreach (var t in m_charts)
            {
                this.OnOtherTrackerNotify(sender, e, t);
            }
        }

        public void OnOtherTrackerNotify(object sender, PointerRoutedEventArgs e,
            Infragistics.Controls.Charts.XamDataChart targetChart)
        {
            if (sender == targetChart)
                return;

            this.xamDataChart_PointerMoved(sender, e, targetChart);
        }

        void xamDataChart_PointerMoved(object sender, PointerRoutedEventArgs e,
            Infragistics.Controls.Charts.XamDataChart targetChart)
        {
            XamDataChart chart = sender as XamDataChart;
            if (chart == null)
            {
                return;
            }
            //System.Diagnostics.Debug.WriteLine(targetChart.GetHashCode().ToString() + " PointerMoved: " + e.GetCurrentPoint(null).Position.ToString());

            chart = targetChart;
            foreach (var series in chart.Series)
            {
                var seriesPos = e.GetCurrentPoint(series).Position;
                //System.Diagnostics.Debug.WriteLine("TrySelectClosest: " + seriesPos.ToString());
                if (seriesPos.X >= 0 &&
                    seriesPos.X < series.ActualWidth &&
                    (sender != targetChart || (seriesPos.Y >= 0 && seriesPos.Y < series.ActualHeight)))
                {
                    SelectClosest(
                    series, seriesPos);
                }
            }

            if (sender == targetChart)
                CategoryChart_PointerMoved(sender, e, targetChart);

            if (sender == targetChart)
            {
                this.NotifyOtherTracker(sender, e);
            }
        }

        public //static 
            void SelectClosest(Series series, Point point)
        {
            double minDist = double.PositiveInfinity;
            TrackingGrid closest = null;
            FrameworkElement closestContent = null;
            FrameworkElement beforeVisible = null;

            foreach (var grid in TrackingGrid.Items()
                .Where((i) => i.Series == series))
            {
                double left = GetLeft(series, grid.Item as FrameworkElement);
                double dist = System.Math.Abs(point.X - left);
                var content = grid.VisibilityItem;

                if (content != null &&
                    content.Visibility == Visibility.Visible)
                {
                    beforeVisible = content;
                    content.Visibility
                        = Visibility.Collapsed;
                }
                if (dist < minDist)
                {
                    minDist = dist;
                    closest = grid.Item;
                    closestContent = content;
                }
            }

            if (closest != null)
            {
                if (closestContent != null)
                {
                    closestContent.Visibility
                        = Visibility.Visible;
                    //System.Diagnostics.Debug.WriteLine("closestContent: " + closestContent.GetHashCode());
                }
            }
            else
            {
                if (beforeVisible != null)
                {
                    beforeVisible.Visibility
                        = Visibility.Visible;
                }
            }
        }

        private //static 
            double GetLeft(Series series, FrameworkElement item)
        {
            Marker m = FindMarker(item);

            if (m == null)
            {
                return double.NaN;
            }
            if (m.Visibility == Visibility.Collapsed)
            {
                return double.NaN;
            }
            TranslateTransform t = m.RenderTransform as TranslateTransform;
            if (t == null)
            {
                return double.NaN;
            }
            return t.X + m.ActualWidth / 2.0;
        }

        private //static 
            Marker FindMarker(FrameworkElement item)
        {
            while (item != null)
            {
                if (item is Marker)
                {
                    return item as Marker;
                }
                item = VisualTreeHelper.GetParent(item)
                    as FrameworkElement;
            }
            return null;
        }

        private void CategoryChart_PointerMoved(object sender, PointerRoutedEventArgs e,
            Infragistics.Controls.Charts.XamDataChart targetChart)
        {
            var series = targetChart.Series.FirstOrDefault();
            if (series == null) return;

            var position = e.GetCurrentPoint(series).Position;

            // calculate crosshair coordinates on CategoryDateTimeXAxis 
            if (((XamDataChart)series.SeriesViewer).Axes.OfType<CategoryXAxis>().Any())
            {
                var xAxis = ((XamDataChart)series.SeriesViewer).Axes.OfType<CategoryXAxis>().First();
                var yAxis = ((XamDataChart)series.SeriesViewer).Axes.OfType<NumericYAxis>().First();

                var viewport = new Rect(0, 0, xAxis.ActualWidth, yAxis.ActualHeight);
                var window = series.SeriesViewer.WindowRect;

                bool isInverted = xAxis.IsInverted;
                ScalerParams param = new ScalerParams(window, viewport, isInverted);
                var unscaledX = xAxis.GetUnscaledValue(position.X, param);

                isInverted = yAxis.IsInverted;
                param = new ScalerParams(window, viewport, isInverted);
                var unscaledY = yAxis.GetUnscaledValue(position.Y, param);

                //DateTime xDate = new DateTime((long)unscaledX);

                //var x = unscaledX.ToString();//String.Format("{0:T}", xDate);
                //var y = unscaledY.ToString();// String.Format("{0:0.00}", unscaledY);
                this.SetCoordinate(unscaledX, unscaledY, targetChart.DataContext as IEnumerable<SimpleDataPoint>);
            }
        }

        public void SetCoordinate(double unscaledX, double unscaledY,
            IEnumerable<AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint> source)
        {
            var rootViewModel = this.GetRootViewModel();
            int roundedSecond = Convert.ToInt32(Math.Round(unscaledX));
            if (rootViewModel == null || roundedSecond < 0
                || roundedSecond > ApplicationContext.Instance.CurrentFlight.EndSecond)
                return;

            if (rootViewModel.Group1 != null)
            {
                this.SetGroupDisplay(rootViewModel.Group1, roundedSecond);
            }
            if (rootViewModel.Group2 != null)
            {
                this.SetGroupDisplay(rootViewModel.Group2, roundedSecond);
            }
            if (rootViewModel.Group3 != null)
            {
                this.SetGroupDisplay(rootViewModel.Group3, roundedSecond);
            }
            if (rootViewModel.Group4 != null)
            {
                this.SetGroupDisplay(rootViewModel.Group4, roundedSecond);
            }
            if (rootViewModel.Group5 != null)
            {
                this.SetGroupDisplay(rootViewModel.Group5, roundedSecond);
            }
            if (rootViewModel.Group6 != null)
            {
                this.SetGroupDisplay(rootViewModel.Group6, roundedSecond);
            }
            if (rootViewModel.Group7 != null)
            {
                this.SetGroupDisplay(rootViewModel.Group7, roundedSecond);
            }
            rootViewModel.SetCurrentSecondDisplay(roundedSecond);
        }

        private void SetGroupDisplay(AircraftDataAnalysisModel1.WinRT.DataModel.FlightAnalysisChartGroupViewModel
            groupViewModel, int roundedSecond)
        {
            if (groupViewModel == null || groupViewModel.DataSerie == null
                || groupViewModel.DataSerie.Count <= 0)
                return;

            var findedPoint = groupViewModel.DataSerie.First(
                new Func<SimpleDataPoint, bool>(
                    delegate(SimpleDataPoint pt)
                    {
                        if (pt.Second == roundedSecond)
                            return true;
                        return false;
                    }));

            if (findedPoint == null)
                return;

            if (groupViewModel.Serie1Definition != null)
            {
                groupViewModel.Serie1Definition.SetCurrentDisplayPoint(
                    findedPoint.Second, findedPoint.Value1);
            }
            if (groupViewModel.Serie2Definition != null)
            {
                groupViewModel.Serie2Definition.SetCurrentDisplayPoint(
                    findedPoint.Second, findedPoint.Value2);
            }
            if (groupViewModel.Serie3Definition != null)
            {
                groupViewModel.Serie3Definition.SetCurrentDisplayPoint(
                    findedPoint.Second, findedPoint.Value3);
            }
        }

        #endregion tracking point function

        private FlightAnalysisViewModelOld m_viewModel = null;

        private void OnNavigateToHome_Clicked(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private Dictionary<XamDataChart, DateTime> m_prevTabTime = new Dictionary<XamDataChart, DateTime>();

        /// <summary>
        /// 把单击转化为双击，为了适应XamDataChart不支持双击的情况
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void item_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (sender == null || !(sender is XamDataChart))
                return;

            var chart = sender as XamDataChart;
            //System.Diagnostics.Debug.WriteLine("SingleTap request: {0},{1}", DateTime.Now, sender.GetHashCode());
            //return;//
            if (m_prevTabTime.ContainsKey(chart))
            {
                var dt = m_prevTabTime[chart];

                double totalSubtract = DateTime.Now.Subtract(dt).TotalMilliseconds;
                System.Diagnostics.Debug.WriteLine("DoubleClick request: {0}", totalSubtract);
                //if (totalSubtract > 5 * DoubleClickInterval)
                //{
                //    m_prevTabTime[chart] = DateTime.Now;
                //    return;
                //}
                if (totalSubtract <= DoubleClickInterval)
                {
                    this.item_DoubleTapped(sender, e, chart);
                    m_prevTabTime.Remove(chart);
                    return;
                }
                else
                {
                    m_prevTabTime[chart] = DateTime.Now;
                }
            }
            else
            {
                m_prevTabTime.Add(chart, DateTime.Now);
            }
        }

        private readonly int DoubleClickInterval = 2000; //双击间隔的毫秒

        /// <summary>
        /// Chart的双击处理，一般是Navigate到Sub视图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="targetChart"></param>
        void item_DoubleTapped(object sender, TappedRoutedEventArgs e, XamDataChart targetChart)
        {
            System.Diagnostics.Debug.WriteLine("DoubleTapped invoke");

            var datacontext = targetChart.DataContext;
            if (datacontext != null && datacontext is AircraftDataAnalysisModel1.WinRT.DataModel.FlightAnalysisChartGroupViewModel)
            {
                AircraftDataAnalysisModel1.WinRT.DataModel.FlightAnalysisChartGroupViewModel vm = datacontext as AircraftDataAnalysisModel1.WinRT.DataModel.FlightAnalysisChartGroupViewModel;
                if (vm.Serie1Visibility == Windows.UI.Xaml.Visibility.Visible)
                {
                    var parameterIDHost = vm.Serie1Definition.ParameterID;
                    AircraftDataAnalysisWinRT.MyControl.SubEditChartNavigationParameter parameter
                        = new AircraftDataAnalysisWinRT.MyControl.SubEditChartNavigationParameter()
                        {
                            HostParameterID = parameterIDHost,
                            DataLoader = this.GetRootViewModel().DataLoader,
                            HostParameterYAxis = FlightAnalysisSubViewYAxis.LeftYAxis,
                        };

                    List<AircraftDataAnalysisWinRT.MyControl.SubEditChartNavigationParameter.RelatedParameterInfo> infos
                        = new List<AircraftDataAnalysisWinRT.MyControl.SubEditChartNavigationParameter.RelatedParameterInfo>();

                    if (vm.Serie2Visibility == Windows.UI.Xaml.Visibility.Visible)
                    {
                        infos.Add(new AircraftDataAnalysisWinRT.MyControl.SubEditChartNavigationParameter.RelatedParameterInfo()
                        {
                            RelatedParameterID = vm.Serie2Definition.ParameterID,
                            YAxis =
                            (vm.Serie2Definition.ParameterID.StartsWith("T6") || vm.Serie2Definition.ParameterID.StartsWith("NH")) ? FlightAnalysisSubViewYAxis.LeftYAxis : FlightAnalysisSubViewYAxis.RightYAxis
                        });
                    }

                    if (vm.Serie3Visibility == Windows.UI.Xaml.Visibility.Visible)
                    {
                        infos.Add(new AircraftDataAnalysisWinRT.MyControl.SubEditChartNavigationParameter.RelatedParameterInfo()
                        {
                            RelatedParameterID = vm.Serie3Definition.ParameterID,
                            YAxis =
                            (vm.Serie3Definition.ParameterID.StartsWith("T6") || vm.Serie3Definition.ParameterID.StartsWith("NH")) ? FlightAnalysisSubViewYAxis.LeftYAxis : FlightAnalysisSubViewYAxis.RightYAxis
                        });
                    }

                    if (infos.Count > 0)
                    {
                        parameter.RelatedParameterIDs = infos.ToArray();
                    }

                    this.Frame.Navigate(typeof(FlightAnalysisSub), parameter);
                }
            }
        }

        /// <summary>
        /// 导航进来的主方法
        /// </summary>
        /// <param name="e"></param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            System.Diagnostics.Debug.WriteLine(string.Format("Start analysis:{0}", DateTime.Now));
            //return;//debug
            AircraftDataAnalysisModel1.WinRT.DataModel.FlightAnalysisViewModel rootViewModel
                = this.GetRootViewModel();
            if (rootViewModel == null)
                return;

            rootViewModel.UserThreadInvoker = this.Dispatcher;

            FlightAnalysisNavigationParameter navPara = null;
            if (e != null)
            {
                if (e.Parameter != null && e.Parameter is FlightAnalysisNavigationParameter)
                {
                    navPara = e.Parameter as FlightAnalysisNavigationParameter;
                    if (!string.IsNullOrEmpty(navPara.SelectedPanelID))
                    {
                        rootViewModel.SetCurrentPanel(navPara.SelectedPanelID);
                    }
                }
            }
            else
            {
            }

            if (navPara != null && navPara.DataLoader != null
                && navPara.DataLoader.CurrentFlight.FlightID == ApplicationContext.Instance.CurrentFlight.FlightID)
                rootViewModel.DataLoader = navPara.DataLoader;
            else
            {
                rootViewModel.DataLoader = new AircraftDataAnalysisModel1.WinRT.Domain.AircraftAnalysisDataLoader()
                {
                    CurrentFlight = ApplicationContext.Instance.CurrentFlight,
                    CurrentAircraftModel = ApplicationContext.Instance.CurrentAircraftModel
                };
            }

            Task<string> initTask = rootViewModel.InitializeAsync();

            initTask.GetAwaiter().OnCompleted(new Action(delegate()
            {
                System.Diagnostics.Debug.WriteLine("Completed");
            }));
        }

        private AircraftDataAnalysisModel1.WinRT.DataModel.FlightAnalysisViewModel GetRootViewModel()
        {
            object value = null;
            if (this.Resources.TryGetValue("datacontext", out value))
            {
                if (value != null && (value is AircraftDataAnalysisModel1.WinRT.DataModel.FlightAnalysisViewModel))
                    return value as AircraftDataAnalysisModel1.WinRT.DataModel.FlightAnalysisViewModel;
            }
            return null;
        }

        #region repeat

        private void xamDataChart1_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            this.xamDataChart_PointerMoved(sender, e, this.tracker1);
        }

        private void xamDataChart2_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            this.xamDataChart_PointerMoved(sender, e, this.tracker2);
        }

        private void xamDataChart3_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            this.xamDataChart_PointerMoved(sender, e, this.tracker3);
        }

        private void xamDataChart4_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            this.xamDataChart_PointerMoved(sender, e, this.tracker4);
        }

        private void xamDataChart5_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            this.xamDataChart_PointerMoved(sender, e, this.tracker5);
        }

        private void xamDataChart6_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            this.xamDataChart_PointerMoved(sender, e, this.tracker6);
        }

        private void xamDataChart7_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            this.xamDataChart_PointerMoved(sender, e, this.tracker7);
        }

        private void btPanel1_Click(object sender, RoutedEventArgs e)
        {
            var rootViewModel = this.GetRootViewModel();
            if (rootViewModel != null && rootViewModel.DataLoader != null
                && rootViewModel.PanelViewItems.Count > 0)
            {
                FlightAnalysisNavigationParameter parameter = new FlightAnalysisNavigationParameter()
                {
                    DataLoader = rootViewModel.DataLoader,
                    SelectedPanelID = rootViewModel.PanelViewItems[0].PanelID
                };

                this.Frame.Navigate(typeof(FlightAnalysis), parameter);
            }
        }

        private void btPanel2_Click(object sender, RoutedEventArgs e)
        {
            var rootViewModel = this.GetRootViewModel();
            if (rootViewModel != null && rootViewModel.DataLoader != null
                && rootViewModel.PanelViewItems.Count > 1)
            {
                FlightAnalysisNavigationParameter parameter = new FlightAnalysisNavigationParameter()
                {
                    DataLoader = rootViewModel.DataLoader,
                    SelectedPanelID = rootViewModel.PanelViewItems[1].PanelID
                };

                this.Frame.Navigate(typeof(FlightAnalysis), parameter);
            }
        }

        private void btPanel3_Click(object sender, RoutedEventArgs e)
        {
            var rootViewModel = this.GetRootViewModel();
            if (rootViewModel != null && rootViewModel.DataLoader != null
                && rootViewModel.PanelViewItems.Count > 2)
            {
                FlightAnalysisNavigationParameter parameter = new FlightAnalysisNavigationParameter()
                {
                    DataLoader = rootViewModel.DataLoader,
                    SelectedPanelID = rootViewModel.PanelViewItems[2].PanelID
                };

                this.Frame.Navigate(typeof(FlightAnalysis), parameter);
            }
        }

        private void btPanel4_Click(object sender, RoutedEventArgs e)
        {
            var rootViewModel = this.GetRootViewModel();
            if (rootViewModel != null && rootViewModel.DataLoader != null
                && rootViewModel.PanelViewItems.Count > 3)
            {
                FlightAnalysisNavigationParameter parameter = new FlightAnalysisNavigationParameter()
                {
                    DataLoader = rootViewModel.DataLoader,
                    SelectedPanelID = rootViewModel.PanelViewItems[3].PanelID
                };

                this.Frame.Navigate(typeof(FlightAnalysis), parameter);
            }
        }

        private void btPanel5_Click(object sender, RoutedEventArgs e)
        {
            var rootViewModel = this.GetRootViewModel();
            if (rootViewModel != null && rootViewModel.DataLoader != null
                && rootViewModel.PanelViewItems.Count > 4)
            {
                FlightAnalysisNavigationParameter parameter = new FlightAnalysisNavigationParameter()
                {
                    DataLoader = rootViewModel.DataLoader,
                    SelectedPanelID = rootViewModel.PanelViewItems[4].PanelID
                };

                this.Frame.Navigate(typeof(FlightAnalysis), parameter);
            }
        }

        private void btPanel6_Click(object sender, RoutedEventArgs e)
        {
            var rootViewModel = this.GetRootViewModel();
            if (rootViewModel != null && rootViewModel.DataLoader != null
                && rootViewModel.PanelViewItems.Count > 5)
            {
                FlightAnalysisNavigationParameter parameter = new FlightAnalysisNavigationParameter()
                {
                    DataLoader = rootViewModel.DataLoader,
                    SelectedPanelID = rootViewModel.PanelViewItems[5].PanelID
                };

                this.Frame.Navigate(typeof(FlightAnalysis), parameter);
            }
        }

        private void btPanel7_Click(object sender, RoutedEventArgs e)
        {
            var rootViewModel = this.GetRootViewModel();
            if (rootViewModel != null && rootViewModel.DataLoader != null
                && rootViewModel.PanelViewItems.Count > 6)
            {
                FlightAnalysisNavigationParameter parameter = new FlightAnalysisNavigationParameter()
                {
                    DataLoader = rootViewModel.DataLoader,
                    SelectedPanelID = rootViewModel.PanelViewItems[6].PanelID
                };

                this.Frame.Navigate(typeof(FlightAnalysis), parameter);
            }
        }

        private void btPanel8_Click(object sender, RoutedEventArgs e)
        {
            var rootViewModel = this.GetRootViewModel();
            if (rootViewModel != null && rootViewModel.DataLoader != null
                && rootViewModel.PanelViewItems.Count > 7)
            {
                FlightAnalysisNavigationParameter parameter = new FlightAnalysisNavigationParameter()
                {
                    DataLoader = rootViewModel.DataLoader,
                    SelectedPanelID = rootViewModel.PanelViewItems[7].PanelID
                };

                this.Frame.Navigate(typeof(FlightAnalysis), parameter);
            }
        }

        private void btPanel9_Click(object sender, RoutedEventArgs e)
        {
            var rootViewModel = this.GetRootViewModel();
            if (rootViewModel != null && rootViewModel.DataLoader != null
                && rootViewModel.PanelViewItems.Count > 8)
            {
                FlightAnalysisNavigationParameter parameter = new FlightAnalysisNavigationParameter()
                {
                    DataLoader = rootViewModel.DataLoader,
                    SelectedPanelID = rootViewModel.PanelViewItems[8].PanelID
                };

                this.Frame.Navigate(typeof(FlightAnalysis), parameter);
            }
        }

        private void btPanel10_Click(object sender, RoutedEventArgs e)
        {
            var rootViewModel = this.GetRootViewModel();
            if (rootViewModel != null && rootViewModel.DataLoader != null
                && rootViewModel.PanelViewItems.Count > 9)
            {
                FlightAnalysisNavigationParameter parameter = new FlightAnalysisNavigationParameter()
                {
                    DataLoader = rootViewModel.DataLoader,
                    SelectedPanelID = rootViewModel.PanelViewItems[9].PanelID
                };

                this.Frame.Navigate(typeof(FlightAnalysis), parameter);
            }
        }

        private void btPanel11_Click(object sender, RoutedEventArgs e)
        {
            var rootViewModel = this.GetRootViewModel();
            if (rootViewModel != null && rootViewModel.DataLoader != null
                && rootViewModel.PanelViewItems.Count > 10)
            {
                FlightAnalysisNavigationParameter parameter = new FlightAnalysisNavigationParameter()
                {
                    DataLoader = rootViewModel.DataLoader,
                    SelectedPanelID = rootViewModel.PanelViewItems[10].PanelID
                };

                this.Frame.Navigate(typeof(FlightAnalysis), parameter);
            }
        }

        private void btPanel12_Click(object sender, RoutedEventArgs e)
        {
            var rootViewModel = this.GetRootViewModel();
            if (rootViewModel != null && rootViewModel.DataLoader != null
                && rootViewModel.PanelViewItems.Count > 11)
            {
                FlightAnalysisNavigationParameter parameter = new FlightAnalysisNavigationParameter()
                {
                    DataLoader = rootViewModel.DataLoader,
                    SelectedPanelID = rootViewModel.PanelViewItems[11].PanelID
                };

                this.Frame.Navigate(typeof(FlightAnalysis), parameter);
            }
        }

        #endregion repeat

        private void OnSerieDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OnSerieDoubleTapped");
        }

        private void On1DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("DoubleTapped " + DateTime.Now);
        }

        private void On2DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("2 DoubleTapped " + DateTime.Now);

        }

        #region old obsolete

        //    return;//debug

        //    //1. 先设置轮转颜色，每个轴可能有最多3个颜色，所以下面要修改
        //    this.LineBrush1 = AircraftDataAnalysisWinRT.Styles.AircraftDataAnalysisGlobalPallete.Brushes[0];
        //    this.LineBrush2 = AircraftDataAnalysisWinRT.Styles.AircraftDataAnalysisGlobalPallete.Brushes[1];
        //    this.LineBrush3 = AircraftDataAnalysisWinRT.Styles.AircraftDataAnalysisGlobalPallete.Brushes[3];
        //    this.LineBrush4 = AircraftDataAnalysisWinRT.Styles.AircraftDataAnalysisGlobalPallete.Brushes[3];
        //    this.LineBrush5 = AircraftDataAnalysisWinRT.Styles.AircraftDataAnalysisGlobalPallete.Brushes[4];
        //    this.LineBrush6 = AircraftDataAnalysisWinRT.Styles.AircraftDataAnalysisGlobalPallete.Brushes[5];
        //    this.LineBrush7 = AircraftDataAnalysisWinRT.Styles.AircraftDataAnalysisGlobalPallete.Brushes[6];

        //    //debug: 设置测试数据源
        //    this.tracker1.DataContext = new AircraftDataAnalysisWinRT.Test.TestSimpleDataSource();
        //    this.tracker2.DataContext = new AircraftDataAnalysisWinRT.Test.TestSimpleDataSource();
        //    this.tracker3.DataContext = new AircraftDataAnalysisWinRT.Test.TestSimpleDataSource();
        //    this.tracker4.DataContext = new AircraftDataAnalysisWinRT.Test.TestSimpleDataSource();
        //    this.tracker5.DataContext = new AircraftDataAnalysisWinRT.Test.TestSimpleDataSource();
        //    this.tracker6.DataContext = new AircraftDataAnalysisWinRT.Test.TestSimpleDataSource();



        //    var chartPanels = ApplicationContext.Instance.GetChartPanels(ApplicationContext.Instance.CurrentAircraftModel);
        //    IEnumerable<PanelChangedWrap> allPanels = null;

        //    if (chartPanels != null && chartPanels.Count() > 0)
        //    {
        //        var converted = from one in chartPanels
        //                        select new PanelChangedWrap() { SelectedPanel = one };
        //        allPanels = converted;
        //    }

        //    m_viewModel = new FlightAnalysisViewModelOld()
        //    {
        //        CurrentStartSecond = ApplicationContext.Instance.CurrentFlight.StartSecond,
        //        CurrentEndSecond = ApplicationContext.Instance.CurrentFlight.EndSecond
        //    };

        //    PanelChangedWrap wrapPanel = this.GetCurrentPanel(allPanels, e.Parameter);

        //    if (wrapPanel == null)
        //        return;

        //    NavigationToPanelAsync(wrapPanel, allPanels);
        //}

        //public Brush LineBrush1
        //{
        //    get
        //    {

        //        var serie1 = this.tracker1.Series[0];
        //        if (serie1 != null)
        //            return serie1.Brush;

        //        return null;
        //    }
        //    set
        //    {
        //        var serie1 = this.tracker1.Series[0];
        //        if (serie1 != null)
        //            serie1.Brush = value;
        //    }
        //}

        //public Brush LineBrush2
        //{
        //    get
        //    {

        //        var serie1 = this.tracker2.Series[0];
        //        if (serie1 != null)
        //            return serie1.Brush;

        //        return null;
        //    }
        //    set
        //    {
        //        var serie1 = this.tracker2.Series[0];
        //        if (serie1 != null)
        //            serie1.Brush = value;
        //    }
        //}

        //public Brush LineBrush3
        //{
        //    get
        //    {

        //        var serie1 = this.tracker3.Series[0];
        //        if (serie1 != null)
        //            return serie1.Brush;

        //        return null;
        //    }
        //    set
        //    {
        //        var serie1 = this.tracker3.Series[0];
        //        if (serie1 != null)
        //            serie1.Brush = value;
        //    }
        //}

        //public Brush LineBrush4
        //{
        //    get
        //    {

        //        var serie1 = this.tracker4.Series[0];
        //        if (serie1 != null)
        //            return serie1.Brush;

        //        return null;
        //    }
        //    set
        //    {
        //        var serie1 = this.tracker4.Series[0];
        //        if (serie1 != null)
        //            serie1.Brush = value;
        //    }
        //}

        //public Brush LineBrush5
        //{
        //    get
        //    {

        //        var serie1 = this.tracker5.Series[0];
        //        if (serie1 != null)
        //            return serie1.Brush;

        //        return null;
        //    }
        //    set
        //    {
        //        var serie1 = this.tracker5.Series[0];
        //        if (serie1 != null)
        //            serie1.Brush = value;
        //    }
        //}

        //public Brush LineBrush6
        //{
        //    get
        //    {

        //        var serie1 = this.tracker6.Series[0];
        //        if (serie1 != null)
        //            return serie1.Brush;

        //        return null;
        //    }
        //    set
        //    {
        //        var serie1 = this.tracker6.Series[0];
        //        if (serie1 != null)
        //            serie1.Brush = value;
        //    }
        //}

        //public Brush LineBrush7
        //{
        //    get
        //    {

        //        var serie1 = this.tracker7.Series[0];
        //        if (serie1 != null)
        //            return serie1.Brush;

        //        return null;
        //    }
        //    set
        //    {
        //        var serie1 = this.tracker7.Series[0];
        //        if (serie1 != null)
        //            serie1.Brush = value;
        //    }
        //}

        //private PanelChangedWrap GetCurrentPanel(IEnumerable<PanelChangedWrap> allPanels, object navigateParameter)
        //{
        //    var chartPanels = allPanels;
        //    //ApplicationContext.Instance.GetChartPanels(ApplicationContext.Instance.CurrentAircraftModel);

        //    if (chartPanels != null && chartPanels.Count() > 0)
        //    {
        //        if (navigateParameter != null && navigateParameter is FlightAnalysisNavigationParameter)
        //        {
        //            FlightAnalysisNavigationParameter parameter = navigateParameter as FlightAnalysisNavigationParameter;

        //            foreach (var c in chartPanels)
        //            {
        //                string selectedPanelID = parameter.SelectedPanelID;
        //                if (c.SelectedPanel.PanelID == selectedPanelID)
        //                    return c;
        //            }
        //        }

        //        var selected = chartPanels.First();

        //        return selected;
        //    }

        //    return null;
        //}

        //private async Task NavigationToPanelAsync(PanelChangedWrap wrapPanel, IEnumerable<PanelChangedWrap> allPanels)
        //{
        //    if (wrapPanel == null || wrapPanel.SelectedPanel == null
        //        || string.IsNullOrEmpty(wrapPanel.SelectedPanel.PanelID))
        //        return;

        //    SetSelectedPanel(wrapPanel, allPanels);

        //    BindDataCore(wrapPanel, allPanels);
        //}

        //void content_FlightAnalysisSubNavigationRequested(object sender, EventArgs e)
        //{
        //    if (e != null && e is AircraftDataAnalysisWinRT.MyControl.FAChart.FlightAnalysisSubNavigateEventArgs)
        //    {
        //        AircraftDataAnalysisWinRT.MyControl.FAChart.FlightAnalysisSubNavigateEventArgs args = e as AircraftDataAnalysisWinRT.MyControl.FAChart.FlightAnalysisSubNavigateEventArgs;
        //        this.Frame.Navigate(typeof(FlightAnalysisSub), args.Parameter);
        //    }
        //}

        //private void SetSelectedPanel(PanelChangedWrap wrapPanel, IEnumerable<PanelChangedWrap> allPanels)
        //{
        //    var parameters = this.GetFlightParameters(wrapPanel.SelectedPanel.ParameterIDs);

        //    this.m_viewModel.RelatedParameterCollection.Clear();

        //    foreach (var par in parameters)
        //    {
        //        this.m_viewModel.RelatedParameterCollection.Add(
        //            new RelatedParameterViewModel(this.m_viewModel, par)
        //            );
        //    }

        //    m_viewModel.RefreshAndRetriveData();
        //    ApplicationContext.Instance.SetCurrentViewModel(ApplicationContext.Instance.CurrentFlight, m_viewModel);
        //}

        //private void BindDataCore(PanelChangedWrap wrapPanel, IEnumerable<PanelChangedWrap> allPanels)
        //{
        //    //1. m_viewModel绑定左边列表
        //    this.panelParameterListCtrl.DataContext = m_viewModel;

        //    //2. btPanels 绑定Selected Panel
        //    FlightAnalysisCommandViewModel commandViewModel = new FlightAnalysisCommandViewModel(
        //        m_viewModel, allPanels, this.Frame) { SelectedPanel = wrapPanel };

        //    this.btPanel1.DataContext = commandViewModel;
        //    this.btPanel2.DataContext = commandViewModel;
        //    this.btPanel3.DataContext = commandViewModel;
        //    this.btPanel4.DataContext = commandViewModel;
        //    this.btPanel5.DataContext = commandViewModel;
        //    this.btPanel6.DataContext = commandViewModel;
        //    this.btPanel7.DataContext = commandViewModel;
        //    this.btPanel8.DataContext = commandViewModel;
        //    this.btPanel9.DataContext = commandViewModel;
        //    this.btPanel10.DataContext = commandViewModel;
        //    this.btPanel11.DataContext = commandViewModel;
        //    this.btPanel12.DataContext = commandViewModel;

        //    //3. 根据当前Selected的面板加载数据
        //    for (int i = 0; i < this.m_charts.Count; i++)
        //    {
        //        if (i < this.m_viewModel.RelatedParameterCollection.Count)
        //        {
        //            var related = this.m_viewModel.RelatedParameterCollection[i];
        //            System.Collections.ObjectModel.ObservableCollection<SimpleDataPoint> points
        //                = this.GetRelatedData(m_viewModel, related);
        //            this.m_charts[i].DataContext = points;
        //        }
        //        else
        //        {
        //            this.m_charts[i].Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        //            if (this.m_charts[i].Parent != null &&
        //                this.m_charts[i].Parent is UIElement)
        //            {
        //                (this.m_charts[i].Parent as UIElement).Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        //            }
        //        }
        //    }


        //    //debug
        //    return;
        //    //debug

        //    /*
        //    Task.Run(new Action(async delegate()
        //    {
        //        System.Diagnostics.Debug.WriteLine(string.Format("before bind analysis:{0}", DateTime.Now));
        //        m_viewModel.RefreshAndRetriveData();
        //        System.Diagnostics.Debug.WriteLine(string.Format("after data retrieved:{0}", DateTime.Now));
        //        var res3 = this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
        //                new Windows.UI.Core.DispatchedHandler(delegate()
        //                {
        //                    this.DataContext = m_viewModel;
        //                    this.lvParameters.ItemsSource = m_viewModel.RelatedParameterCollection;
        //                }));

        //        var res2 = this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
        //                new Windows.UI.Core.DispatchedHandler(delegate()
        //                {
        //                    this.chartCtrl.ViewModel = m_viewModel;
        //                })
        //        );
        //        //this.grdCtrl.BindGridData();// = m_viewModel;
        //        var res1 = this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
        //                new Windows.UI.Core.DispatchedHandler(delegate()
        //                {
        //                    //this.grdData.Columns.Clear();
        //                    //this.grdData.ItemsSource = m_viewModel.RawDatas;
        //                    this.RebindColumns();
        //                    //this.grdData.ItemsSource = m_viewModel.EntityBindingCollection.RawDataItems;
        //                    //this.grdCtrl.ReBindColumns();
        //                })
        //        );
        //        await res1;
        //        await res2;
        //        await res3;
        //        System.Diagnostics.Debug.WriteLine(string.Format("End analysis:{0}", DateTime.Now));
        //    }));*/
        //}

        //private System.Collections.ObjectModel.ObservableCollection<SimpleDataPoint>
        //    GetRelatedData(FlightAnalysisViewModelOld m_viewModel, RelatedParameterViewModel related)
        //{
        //    throw new NotImplementedException();
        //}

        //private void RebindColumns()
        //{
        //    var result2 = GetFlightParameters(null);
        //    var related = from o1 in this.m_viewModel.RelatedParameterCollection
        //                  select o1.Parameter.ParameterID;

        //    //foreach (var cc in this.grdData.Columns)
        //    //{
        //    //    cc.AllowSorting = false;
        //    //    //cc.MinimumWidth = 80;
        //    //    //cc.ColumnSizer = Syncfusion.UI.Xaml.Grid.GridLengthUnitType.Auto;
        //    //    cc.TextAlignment = Windows.UI.Xaml.TextAlignment.Center;
        //    //    cc.HorizontalHeaderContentAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;

        //    //    if (result2 != null && result2.Count() > 0)
        //    //    {
        //    //        cc.IsHidden = related.Contains(cc.MappingName) ? false : true;
        //    //        //int i = 0;
        //    //        //foreach (var one in result2)
        //    //        //{
        //    //        //    Syncfusion.UI.Xaml.Grid.GridTextColumn col
        //    //        //        = new Syncfusion.UI.Xaml.Grid.GridTextColumn()
        //    //        //        {
        //    //        //            MappingName = one.ParameterID,
        //    //        //            HeaderText = one.Caption,
        //    //        //            AllowSorting = false,
        //    //        //            MinimumWidth = 80,
        //    //        //            ColumnSizer = Syncfusion.UI.Xaml.Grid.GridLengthUnitType.Auto,
        //    //        //            TextAlignment = Windows.UI.Xaml.TextAlignment.Center,
        //    //        //            HorizontalHeaderContentAlignment = Windows.UI.Xaml.HorizontalAlignment.Center
        //    //        //        };
        //    //        //    col.IsHidden = related.Contains(one.ParameterID) ? false : true;
        //    //        //    this.grdData.Columns.Add(col);
        //    //        //    i++;
        //    //        //}
        //    //    }
        //    //}

        //    //this.grdData.Columns["Second"].HeaderText = "秒值";
        //    //this.grdData.Columns["Second"].IsHidden = false;

        //    //this.grdData.Columns["Second"].TextAlignment = Windows.UI.Xaml.TextAlignment.Center;
        //    //this.grdData.Columns["Second"].HorizontalHeaderContentAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
        //}

        //private FlightDataEntitiesRT.FlightParameter[] GetFlightParameters(IEnumerable<string> parameterIDs)
        //{
        //    var flightParameters = ApplicationContext.Instance.GetFlightParameters(
        //        ApplicationContext.Instance.CurrentAircraftModel);

        //    var result = from one in flightParameters.Parameters
        //                 where one.ParameterID != "(NULL)" && this.ExistsParameter(one.ParameterID)
        //                 && (parameterIDs == null || parameterIDs.Count() <= 0 ||
        //                 parameterIDs.Contains(one.ParameterID))
        //                 //m_viewModel.RelatedParameterSelected(one.ParameterID)
        //                 select one;

        //    return result.ToArray();
        //}

        //private bool ExistsParameter(string p)
        //{
        //    if (this.m_viewModel.AllParameterIDs.Contains(p))
        //        return true;
        //    return false;
        //}

        //private int GetPanelSelectedIndex(IEnumerable<FlightDataEntitiesRT.Charts.ChartPanel> panels2, string panelID)
        //{
        //    int i = 0;
        //    foreach (var p in panels2)
        //    {
        //        if (p != null && p.PanelID == panelID)
        //            return i;
        //        i++;
        //    }
        //    return -1;
        //}

        //[Obsolete("重设绑定和消息机制")]
        //private void BindCurrentPanelData()
        //{
        //    m_viewModel.CurrentStartSecond = 0;
        //    m_viewModel.CurrentEndSecond = ApplicationContext.Instance.CurrentFlight.EndSecond;

        //    System.Diagnostics.Debug.WriteLine(string.Format("before bind analysis:{0}", DateTime.Now));
        //    m_viewModel.RefreshAndRetriveData();
        //    System.Diagnostics.Debug.WriteLine(string.Format("after data retrieved:{0}", DateTime.Now));
        //    this.DataContext = m_viewModel;

        //    //this.chartUc1.ViewModel = m_viewModel;

        //    //this.lvParameters.ItemsSource = m_viewModel.RelatedParameterCollection;
        //    ////this.grdCtrl.ViewModel = m_viewModel;
        //    //this.chartCtrl.ViewModel = m_viewModel;
        //}

        ///// <summary>
        ///// 使用在导航过程中传递的内容填充页。在从以前的会话
        ///// 重新创建页时，也会提供任何已保存状态。
        ///// </summary>
        ///// <param name="navigationParameter">最初请求此页时传递给
        ///// <see cref="Frame.Navigate(Type, Object)"/> 的参数值。
        ///// </param>
        ///// <param name="pageState">此页在以前会话期间保留的状态
        ///// 字典。首次访问页面时为 null。</param>
        ////protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        ////{
        ////}

        /////// <summary>
        /////// 保留与此页关联的状态，以防挂起应用程序或
        /////// 从导航缓存中放弃此页。值必须符合
        /////// <see cref="SuspensionManager.SessionState"/> 的序列化要求。
        /////// </summary>
        /////// <param name="pageState">要使用可序列化状态填充的空字典。</param>
        ////protected override void SaveState(Dictionary<String, Object> pageState)
        ////{
        ////}

        //private bool m_switcher = false;

        ////private void cbPanelSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        ////{
        ////    ////重做Navigation吧，所有加载都在Navigation中做
        ////    if (m_switcher)
        ////        return;

        ////    m_switcher = true;
        ////    try
        ////    {
        ////        if (this.tabHost.SelectedItem != null
        ////            && this.tabHost.SelectedItem is SfTabItem)
        ////        {
        ////            var item = this.tabHost.SelectedItem as SfTabItem;
        ////            if (item != null && item.Content != null
        ////                && !string.IsNullOrEmpty(item.Name))
        ////            {
        ////                string panelID = item.Name;

        ////                this.SetSelectedPanel(panelID);
        ////            }
        ////        }

        ////        //    if (this.cbPanelSelect.SelectedItem != null
        ////        //        && this.cbPanelSelect.SelectedItem is FlightDataEntitiesRT.Charts.ChartPanel)
        ////        //    {
        ////        //        var panel = this.cbPanelSelect.SelectedItem as FlightDataEntitiesRT.Charts.ChartPanel;
        ////        //        PanelChangedWrap wrap = new PanelChangedWrap() { SelectedPanel = panel };

        ////        //        Task<IEnumerable<FlightDataEntitiesRT.Charts.ChartPanel>> task = Task.Run<
        ////        //            IEnumerable<FlightDataEntitiesRT.Charts.ChartPanel>>(
        ////        //            new Func<IEnumerable<FlightDataEntitiesRT.Charts.ChartPanel>>(delegate()
        ////        //            {
        ////        //                var panels = ApplicationContext.Instance.GetChartPanels(
        ////        //                    ApplicationContext.Instance.CurrentAircraftModel);
        ////        //                return panels;
        ////        //            }));

        ////        //        //this.Frame.Navigate(typeof(FlightAnalysis), wrap);
        ////        //        this.NavigationToPanel(task, wrap);

        ////        //        //this.m_viewModel.CurrentPanel = this.cbPanelSelect.SelectedItem as FlightDataEntitiesRT.Charts.ChartPanel;
        ////        //        //this.BindCurrentPanelData();
        ////        //    }
        ////    }
        ////    catch (Exception ex) { LogHelper.Error(ex); }
        ////    finally
        ////    {
        ////        m_switcher = false;
        ////    }
        ////}

        //private void tabHost_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        //{

        //}
        #endregion old obsolete
    }

    public class FlightAnalysisNavigationParameter : AircraftDataAnalysisModel1.WinRT.Domain.IDataLoaderNavigator
    {
        public string SelectedPanelID { get; set; }
        public AircraftDataAnalysisModel1.WinRT.Domain.AircraftAnalysisDataLoader DataLoader { get; set; }
    }

    internal class NullVisibilityValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    internal class NullGroupVisibilityValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && value is AircraftDataAnalysisModel1.WinRT.DataModel.FlightAnalysisChartGroupViewModel
                && (value as AircraftDataAnalysisModel1.WinRT.DataModel.FlightAnalysisChartGroupViewModel).GroupVisible == Visibility.Visible)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    [Obsolete]
    public class PanelChangedWrap
    {
        public FlightDataEntitiesRT.Charts.ChartPanel SelectedPanel
        {
            get;
            set;
        }
    }

    [Obsolete]
    public class FlightAnalysisCommandViewModel : AircraftDataAnalysisWinRT.Common.BindableBase
    {
        public FlightAnalysisCommandViewModel(FlightAnalysisViewModelOld viewModel,
            IEnumerable<PanelChangedWrap> wrapPanels, Windows.UI.Xaml.Controls.Frame frame)
        {
            this.m_viewModel = viewModel;
            this.m_wrapPanels = wrapPanels;

            this.m_panel1SelectedCommand = new FlightAnalysisNavCommand(viewModel,
                m_wrapPanels.ElementAt(0).SelectedPanel.PanelID, false, frame);

            this.m_panel2SelectedCommand = new FlightAnalysisNavCommand(viewModel,
                m_wrapPanels.ElementAt(1).SelectedPanel.PanelID, false, frame);

            this.m_panel3SelectedCommand = new FlightAnalysisNavCommand(viewModel,
                m_wrapPanels.ElementAt(2).SelectedPanel.PanelID, false, frame);

            this.m_panel4SelectedCommand = new FlightAnalysisNavCommand(viewModel,
                m_wrapPanels.ElementAt(3).SelectedPanel.PanelID, false, frame);

            this.m_panel5SelectedCommand = new FlightAnalysisNavCommand(viewModel,
                m_wrapPanels.ElementAt(4).SelectedPanel.PanelID, false, frame);

            this.m_panel6SelectedCommand = new FlightAnalysisNavCommand(viewModel,
                m_wrapPanels.ElementAt(5).SelectedPanel.PanelID, false, frame);

            this.m_panel7SelectedCommand = new FlightAnalysisNavCommand(viewModel,
                m_wrapPanels.ElementAt(6).SelectedPanel.PanelID, false, frame);

            this.m_panel8SelectedCommand = new FlightAnalysisNavCommand(viewModel,
                m_wrapPanels.ElementAt(7).SelectedPanel.PanelID, false, frame);

            this.m_panel9SelectedCommand = new FlightAnalysisNavCommand(viewModel,
                m_wrapPanels.ElementAt(8).SelectedPanel.PanelID, false, frame);

            this.m_panel10SelectedCommand = new FlightAnalysisNavCommand(viewModel,
                m_wrapPanels.ElementAt(9).SelectedPanel.PanelID, false, frame);

            this.m_panel11SelectedCommand = new FlightAnalysisNavCommand(viewModel,
                m_wrapPanels.ElementAt(10).SelectedPanel.PanelID, false, frame);

            this.m_panel12SelectedCommand = new FlightAnalysisNavCommand(viewModel,
                m_wrapPanels.ElementAt(11).SelectedPanel.PanelID, false, frame);
        }

        private PanelChangedWrap m_selectedPanel;

        public PanelChangedWrap SelectedPanel
        {
            get { return m_selectedPanel; }
            set
            {
                this.SetProperty<PanelChangedWrap>(ref m_selectedPanel, value);

                int index = this.FindSelectedIndex();

                switch (index)
                {
                    case 0: { m_panel1SelectedCommand.IsPanelSelected = true; break; }
                    case 1: { m_panel2SelectedCommand.IsPanelSelected = true; break; }
                    case 2: { m_panel3SelectedCommand.IsPanelSelected = true; break; }
                    case 3: { m_panel4SelectedCommand.IsPanelSelected = true; break; }
                    case 4: { m_panel5SelectedCommand.IsPanelSelected = true; break; }
                    case 5: { m_panel6SelectedCommand.IsPanelSelected = true; break; }
                    case 6: { m_panel7SelectedCommand.IsPanelSelected = true; break; }
                    case 7: { m_panel8SelectedCommand.IsPanelSelected = true; break; }
                    case 8: { m_panel9SelectedCommand.IsPanelSelected = true; break; }
                    case 9: { m_panel10SelectedCommand.IsPanelSelected = true; break; }
                    case 10: { m_panel11SelectedCommand.IsPanelSelected = true; break; }
                    case 11: { m_panel12SelectedCommand.IsPanelSelected = true; break; }
                    default: break;
                }
            }
        }

        private int FindSelectedIndex()
        {
            int i = 0;
            foreach (var one in m_wrapPanels)
            {
                if (one.SelectedPanel.PanelID == m_selectedPanel.SelectedPanel.PanelID)
                    return i;
                i++;
            }

            return -1;
        }

        private FlightAnalysisNavCommand m_panel1SelectedCommand = null;

        public ICommand Panel1SelectedCommand
        {
            get
            {
                return m_panel1SelectedCommand;
            }
        }

        private FlightAnalysisNavCommand m_panel2SelectedCommand = null;

        public ICommand Panel2SelectedCommand
        {
            get
            {
                return m_panel2SelectedCommand;
            }
        }

        private FlightAnalysisNavCommand m_panel3SelectedCommand = null;

        public ICommand Panel3SelectedCommand
        {
            get
            {
                return m_panel3SelectedCommand;
            }
        }

        private FlightAnalysisNavCommand m_panel4SelectedCommand = null;

        public ICommand Panel4SelectedCommand
        {
            get
            {
                return m_panel4SelectedCommand;
            }
        }

        private FlightAnalysisNavCommand m_panel5SelectedCommand = null;

        public ICommand Panel5SelectedCommand
        {
            get
            {
                return m_panel5SelectedCommand;
            }
        }

        private FlightAnalysisNavCommand m_panel6SelectedCommand = null;

        public ICommand Panel6SelectedCommand
        {
            get
            {
                return m_panel6SelectedCommand;
            }
        }

        private FlightAnalysisNavCommand m_panel7SelectedCommand = null;

        public ICommand Panel7SelectedCommand
        {
            get
            {
                return m_panel7SelectedCommand;
            }
        }

        private FlightAnalysisNavCommand m_panel8SelectedCommand = null;

        public ICommand Panel8SelectedCommand
        {
            get
            {
                return m_panel8SelectedCommand;
            }
        }

        private FlightAnalysisNavCommand m_panel9SelectedCommand = null;

        public ICommand Panel9SelectedCommand
        {
            get
            {
                return m_panel9SelectedCommand;
            }
        }

        private FlightAnalysisNavCommand m_panel10SelectedCommand = null;

        public ICommand Panel10SelectedCommand
        {
            get
            {
                return m_panel10SelectedCommand;
            }
        }

        private FlightAnalysisNavCommand m_panel11SelectedCommand = null;

        public ICommand Panel11SelectedCommand
        {
            get
            {
                return m_panel11SelectedCommand;
            }
        }

        private FlightAnalysisNavCommand m_panel12SelectedCommand = null;

        private FlightAnalysisViewModelOld m_viewModel;
        private IEnumerable<PanelChangedWrap> m_wrapPanels;

        public ICommand Panel12SelectedCommand
        {
            get
            {
                return m_panel12SelectedCommand;
            }
        }
    }

    [Obsolete]
    public class FlightAnalysisNavCommand : ICommand
    {
        private bool m_isPanelSelected;

        public bool IsPanelSelected
        {
            get { return m_isPanelSelected; }
            set
            {
                m_isPanelSelected = value;
                if (CanExecuteChanged != null)
                    CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        private string m_panelID;
        private FlightAnalysisViewModelOld m_viewModel;
        private Windows.UI.Xaml.Controls.Frame m_frame;

        public FlightAnalysisNavCommand(FlightAnalysisViewModelOld viewModel,
            string panelID, bool isPanelSelected, Windows.UI.Xaml.Controls.Frame frame)
        {
            this.m_viewModel = viewModel;
            this.m_panelID = panelID;
            this.m_isPanelSelected = isPanelSelected;
            this.m_frame = frame;
        }

        public bool CanExecute(object parameter)
        {
            if (m_isPanelSelected)
                return false;
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            FlightAnalysisNavigationParameter navParameter
                = new FlightAnalysisNavigationParameter() { SelectedPanelID = this.m_panelID };

            m_frame.Navigate(typeof(FlightAnalysis), navParameter);
        }
    }
}
