using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AircraftDataAnalysisModel1.WinRT.MyControl;
using AircraftDataAnalysisWinRT;
using AircraftDataAnalysisWinRT.DataModel;
using AircraftDataAnalysisWinRT.Domain;
using Infragistics.Controls.Charts;
using PStudio.WinApp.Aircraft.FDAPlatform.Domain;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;

// “基本页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234237 上有介绍

namespace AircraftDataAnalysisModel1.WinRT.Domain
{
    /// <summary>
    /// 基本页，提供大多数应用程序通用的特性。
    /// </summary>
    public sealed partial class FaultDiagAnalysis : AircraftDataAnalysisWinRT.Common.LayoutAwarePage,
        AircraftDataAnalysisModel1.WinRT.MyControl.ITrackerParent
    {
        public FaultDiagAnalysis()
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

        private void OnNavigateToHome_Clicked(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PStudio.WinApp.Aircraft.FDAPlatform.MainPage));
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
            System.Diagnostics.Debug.WriteLine(string.Format("Start fault analysis:{0}", DateTime.Now));
            //return;//debug
            AircraftDataAnalysisModel1.WinRT.DataModel.FaultDiagAnalysisViewModel rootViewModel
                = this.GetRootViewModel();
            if (rootViewModel == null)
                return;

            rootViewModel.UserThreadInvoker = this.Dispatcher;

            FaultDiagnosisFASubNavigateParameter navPara = null;
            if (e != null)
            {
                if (e.Parameter != null && e.Parameter is FaultDiagnosisFASubNavigateParameter)
                {
                    navPara = e.Parameter as FaultDiagnosisFASubNavigateParameter;

                    rootViewModel.SetCurrentParameters(navPara.HostParameterID,
                        navPara.RelatedParameterIDs, navPara.FlightStartSecond,
                        navPara.FlightEndSecond, navPara.DecisionStartSecond,
                        navPara.DecisionEndSecond, navPara.DecisionHappenSecond);

                    //if (!string.IsNullOrEmpty(navPara.SelectedPanelID))
                    //{
                    //    rootViewModel.SetCurrentPanel(navPara.SelectedPanelID);
                    //}
                }
            }

            if (navPara == null)
                return;

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

        private AircraftDataAnalysisModel1.WinRT.DataModel.FaultDiagAnalysisViewModel GetRootViewModel()
        {
            object value = null;
            if (this.Resources.TryGetValue("datacontext", out value))
            {
                if (value != null && (value is AircraftDataAnalysisModel1.WinRT.DataModel.FaultDiagAnalysisViewModel))
                    return value as AircraftDataAnalysisModel1.WinRT.DataModel.FaultDiagAnalysisViewModel;
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
    }
}
