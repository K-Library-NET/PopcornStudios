using AircraftDataAnalysisWinRT.Domain;
using AircraftDataAnalysisWinRT.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
//using Telerik.UI.Xaml.Controls.Chart;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Syncfusion.UI.Xaml.Charts;
using Infragistics.Controls.Charts;

// “用户控件”项模板在 http://go.microsoft.com/fwlink/?LinkId=234236 上提供

namespace AircraftDataAnalysisWinRT.MyControl
{
    public sealed partial class FAChart : UserControl
    {
        public FAChart()
        {
            this.InitializeComponent();

            this.Loaded += FAChart_Loaded;
            this.Unloaded += FAChart_Unloaded;
        }

        private bool m_loaded = false;

        void FAChart_Unloaded(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(this.GetHashCode().ToString() + " FAChart UnLoaded:");
        }

        void FAChart_Loaded(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(this.GetHashCode().ToString() + " FAChart Loaded:");
            if (m_loaded == false)
            {
                if (this.m_viewModel != null)
                {
                    this.OnViewModelChanged();
                    m_loaded = true;
                }
            }
        }

        public FAChartModel m_faChartModel;

        private FlightAnalysisViewModelOld m_viewModel = null;
        private IEnumerable<XamDataChart> m_loadingCharts;

        public FlightAnalysisViewModelOld ViewModel
        {
            get
            {
                return m_viewModel;
            }
            set
            {
                if (m_viewModel != null)
                {
                    this.DetachDoubleClicked();
                    //this.m_viewModel.FilterDataChanged -= m_viewModel_FilterDataChanged;
                }

                m_viewModel = value;
                //this.m_viewModel.FilterDataChanged += m_viewModel_FilterDataChanged;
                //this.AttachDoubleClicked();
                //this.OnViewModelChanged();
            }
        }

        private void AttachDoubleClicked()
        {
            if (this.m_loadingCharts != null)
            {
                foreach (var chart in this.m_loadingCharts)
                {
                    chart.IsDoubleTapEnabled = true;
                    System.Diagnostics.Debug.WriteLine("Chart:+" + chart.GetHashCode().ToString());
                    chart.DoubleTapped += new DoubleTappedEventHandler(this.chart_DoubleTapped);
                    chart.Tapped += chart_Tapped;
                }
            }
        }

        void chart_Tapped(object sender, TappedRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Tapped:+" + sender.GetHashCode().ToString());
            if (sender != null && sender is SfChart)
            {
                SfChart chart = sender as SfChart;
                if (chart.DataContext != null && chart.DataContext is FAChartSubModel)
                {
                    FAChartSubModel subModel = chart.DataContext as FAChartSubModel;
                    FlightAnalysisSubNavigateParameter parameter = new FlightAnalysisSubNavigateParameter()
                    {
                        CurrentStartSecond = ViewModel.CurrentStartSecond,
                        CurrentEndSecond = ViewModel.CurrentEndSecond,
                        HostParameterID = subModel.ParameterID
                    };

                    this.RequestFlightAnalysisSub(subModel, parameter);
                    e.Handled = true;
                }
            }
        }

        private void grdHost_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Double Tapped:+" + sender.GetHashCode().ToString());
            e.Handled = true;
        }

        void chart_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (sender != null && sender is SfChart)
            {
                SfChart chart = sender as SfChart;
                if (chart.DataContext != null && chart.DataContext is FAChartSubModel)
                {
                    FAChartSubModel subModel = chart.DataContext as FAChartSubModel;
                    FlightAnalysisSubNavigateParameter parameter = new FlightAnalysisSubNavigateParameter()
                    {
                        CurrentStartSecond = ViewModel.CurrentStartSecond,
                        CurrentEndSecond = ViewModel.CurrentEndSecond,
                        HostParameterID = subModel.ParameterID
                    };

                    this.RequestFlightAnalysisSub(subModel, parameter);
                    e.Handled = true;
                }
            }
        }

        private void RequestFlightAnalysisSub(FAChartSubModel subModel, FlightAnalysisSubNavigateParameter parameter)
        {
            if (FlightAnalysisSubNavigationRequested != null)
            {
                this.FlightAnalysisSubNavigationRequested(
                    subModel, new FlightAnalysisSubNavigateEventArgs() { Parameter = parameter });
            }
        }

        internal class FlightAnalysisSubNavigateEventArgs : EventArgs
        {
            public FlightAnalysisSubNavigateParameter Parameter
            {
                get;
                set;
            }
        }

        public event EventHandler FlightAnalysisSubNavigationRequested;

        private void DetachDoubleClicked()
        {
            if (this.m_loadingCharts != null)
            {
                foreach (var chart in this.m_loadingCharts)
                {
                    chart.DoubleTapped -= chart_DoubleTapped;
                }
                this.m_loadingCharts = null;
            }
        }

        [Obsolete("在这有用吗？")]
        void m_viewModel_FilterDataChanged(object sender, EventArgs e)
        {
            // this.ReBindChartSeries();
        }

        private void OnViewModelChanged()
        {
            if (this.m_faChartModel == null && this.m_viewModel != null)
            {
                this.m_faChartModel = m_viewModel.GetFAChartModel();
            }

            if (m_faChartModel == null)
                return;

            //先分析几个图，一个、两个、三个的做法不一样
            //一个：全屏Grid
            //两个：二分屏幕上下Grid
            //三个或更多：三到n行Grid，行高固定
            this.grdHost.Children.Clear();
            this.grdHost.RowDefinitions.Clear();
            //只需要清空Children和行，列从没设置过
            IEnumerable<XamDataChart> chartElements = this.CreateElements();
            IEnumerable<Infragistics.Controls.XamDock> wrapDocks = this.CreateDockElements(chartElements);
            if (chartElements != null)
            {
                int items = chartElements.Count();
                if (items == 1)
                {
                    var chart = wrapDocks.First();// chartElements.First();
                    if (chart != null)
                        this.grdHost.Children.Add(chart);
                }
                else if (items == 2)
                {
                    this.grdHost.RowDefinitions.Add(new RowDefinition());
                    this.grdHost.RowDefinitions.Add(new RowDefinition());
                    var chart1 = wrapDocks.ElementAt(0);//chartElements.ElementAt(0);
                    var chart2 = wrapDocks.ElementAt(1);//chartElements.ElementAt(1);
                    if (chart1 != null)
                        this.grdHost.Children.Add(chart1);
                    if (chart2 != null)
                    {
                        Grid.SetRow(chart2, 1);
                        this.grdHost.Children.Add(chart2);
                    }
                }
                else if (items > 2)
                {
                    for (int i = 0; i < items; i++)
                    {
                        this.grdHost.RowDefinitions.Add(
                            new RowDefinition()
                            {
                                Height = new GridLength(BindingChartUIFactory.MultiItemsRowHeight, GridUnitType.Pixel)
                            }
                            );
                        var child = wrapDocks.ElementAt(i);//chartElements.ElementAt(i);
                        Grid.SetRow(child, i);
                        this.grdHost.Children.Add(child);
                    }
                }
                //items == 0 那就return
            }
            this.m_loadingCharts = chartElements;
            this.AttachDoubleClicked();
        }

        private IEnumerable<XamDataChart> CreateElements()
        {
            if (this.m_viewModel != null)
            {
                var ids = from one in m_viewModel.RelatedParameterCollection
                          where //one.IsChecked && 
                          one.Parameter != null && !string.IsNullOrEmpty(one.Parameter.ParameterID)
                          select one.Parameter.ParameterID;

                return BindingChartUIFactory.BuildChartElements(m_viewModel, ids.ToArray(), ref m_faChartModel);
            }

            return null;
        }


        private IEnumerable<Infragistics.Controls.XamDock> CreateDockElements(IEnumerable<XamDataChart> chartElements)
        {
            List<Infragistics.Controls.XamDock> docs = new List<Infragistics.Controls.XamDock>();
            foreach (var c in chartElements)
            {
                Infragistics.Controls.XamDock dc = new Infragistics.Controls.XamDock();
                dc.Children.Add(c);
                docs.Add(dc);
            }
            return docs;
        }
    }
}
