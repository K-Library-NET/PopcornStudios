using AircraftDataAnalysisModel1.WinRT.DataModel;
using AircraftDataAnalysisWinRT.DataModel;
using AircraftDataAnalysisWinRT.Domain;
using Infragistics.Controls.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “用户控件”项模板在 http://go.microsoft.com/fwlink/?LinkId=234236 上提供

namespace AircraftDataAnalysisWinRT.MyControl
{
    public sealed partial class FAChartModel1 : UserControl
    {
        public FAChartModel1()
        {
            this.InitializeComponent();

            this.Loaded += FAChartModel1_Loaded;
            this.Unloaded += FAChartModel1_Unloaded;
        }

        void FAChartModel1_Unloaded(object sender, RoutedEventArgs e)
        {
            //debug
            System.Diagnostics.Debug.WriteLine("FAChartModel1 unloaded...");
        }

        private bool m_loaded = false;

        void FAChartModel1_Loaded(object sender, RoutedEventArgs e)
        {
            //debug
            System.Diagnostics.Debug.WriteLine("FAChartModel1 loaded...");
            if (m_loaded == false)
            {
                this.m_loaded = true;
                if (m_subViewModel != null)
                {//value assigned
                    this.ResetCharts();
                }
            }
        }

        private FlightAnalysisSubViewModel m_subViewModel = null;

        public FlightAnalysisSubViewModel SubViewModel
        {
            get { return m_subViewModel; }
            set
            {
                //if (m_subViewModel != null)
                //{
                //    m_subViewModel.RelatedParameterIDs.CollectionChanged -= RelatedParameterIDs_CollectionChanged;
                //    m_subViewModel = null;
                //}

                //m_subViewModel = value;
                //if (m_subViewModel != null)
                //{
                //    m_subViewModel.RelatedParameterIDs.CollectionChanged += RelatedParameterIDs_CollectionChanged;
                //}

                //if (this.m_loaded)
                //{
                //    this.ResetCharts();
                //}
            }
        }

        private IEnumerable<AxisDataBindingObject> CalculateBindingObjects()
        {
            if (this.m_subViewModel != null)
            {
                //List<string> resultString1 = new List<string>();
                //resultString1.Add(this.m_subViewModel.HostParameterID);
                //if (m_subViewModel.RelatedParameterIDs != null && m_subViewModel.RelatedParameterIDs.Count > 0)
                //    resultString1.AddRange(m_subViewModel.RelatedParameterIDs);
                ////var result1 = from one in this.m_viewModel.RelatedParameterCollection
                ////              //where one.IsChecked
                ////              select one;

                ////KG(开关量)分一组
                ////T6L、T6R分一组
                ////NHL、NHR分一组

                //List<AxisDataBindingObject> objs = new List<AxisDataBindingObject>();

                //Dictionary<string, AxisDataBindingObject> objMap = new Dictionary<string, AxisDataBindingObject>();

                //int item = 0;
                //foreach (var res in resultString1)
                //{
                //    string key = res;
                //    if (res.StartsWith("KG"))
                //    {
                //        key = "KG";
                //    }
                //    else if (res.StartsWith("T6"))
                //    {
                //        key = "T6";
                //    }
                //    else if (res.StartsWith("NH"))
                //    {
                //        key = "NH";
                //    }
                //    if (objMap.ContainsKey(key))
                //    {
                //        objMap[key].AddRelatedParameterID(res);
                //        //objMap[key].Add(res);
                //        continue;
                //    }
                //    else
                //    {
                //        AxisDataBindingObject obj = new AxisDataBindingObject(this.m_subViewModel.ViewModel);
                //        if (key == "KG")
                //        {
                //            obj = new KGAxisDataBindingObject(this.m_subViewModel.ViewModel);
                //        }
                //        else if (key == "T6")
                //        {
                //            obj = new T6AxisDataBindingObject(this.m_subViewModel.ViewModel);
                //        }
                //        else if (key == "NH")
                //        {
                //            obj = new NHAxisDataBindingObject(this.m_subViewModel.ViewModel);
                //        }
                //        obj.ParameterID = key;
                //        obj.Order = item;
                //        item++;
                //        //obj.Add(res);
                //        obj.AddRelatedParameterID(res);
                //        objMap.Add(key, obj);
                //        objs.Add(obj);
                //    }
                //}

                //var result2 = from ob in objs
                //              orderby ob.Order ascending
                //              select ob;

                //return result2;
            }

            return new AxisDataBindingObject[] { };
        }

        private Dictionary<string, XamDataChart> m_chartMap = new Dictionary<string, XamDataChart>();
        //cache Chart, that can show/hide fast;

        private void ResetCharts()
        {
            this.grdHost.RowDefinitions.Clear();

            //先分析几个图，一个、两个、三个的做法不一样
            //一个：全屏Grid
            //两个：二分屏幕上下Grid
            //三个或更多：三到n行Grid，行高固定

            IEnumerable<AxisDataBindingObject> bindingObjs = this.CalculateBindingObjects();

            var axisDataBindingObject = bindingObjs.First();
            //must be at least one
            //if (axisDataBindingObject == null || m_subViewModel == null
            //    || string.IsNullOrEmpty(m_subViewModel.HostParameterID)
            //    || axisDataBindingObject.ParameterID != this.m_subViewModel.HostParameterID)
            //    return;

            //XamDataChart hostChart = this.BuildHostChart(axisDataBindingObject);

            IEnumerable<XamDataChart> chartElements = this.CreateElements(bindingObjs);
            IEnumerable<Infragistics.Controls.XamDock> wrapDocks = this.CreateDockElements(chartElements);
            if (chartElements != null)
            {
                int items = chartElements.Count();
                if (items == 1)
                {
                    var chart = wrapDocks.First();////chartElements.First();
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
                        var child = wrapDocks.ElementAt(i);////chartElements.ElementAt(i);
                        Grid.SetRow(child, i);
                        this.grdHost.Children.Add(child);
                    }
                }
                //items == 0 那就return
            }
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

        private IEnumerable<XamDataChart> CreateElements(IEnumerable<AxisDataBindingObject> bindingObjs)
        {
            List<XamDataChart> charts = new List<XamDataChart>();

            foreach (var bd in bindingObjs)
            {
                if (this.m_chartMap.ContainsKey(bd.ParameterID))
                {
                    charts.Add(m_chartMap[bd.ParameterID]);
                    continue;
                }

                var chartCtrl = BindingChartUIFactory.CreateOneChart(bd, ref m_faChartModel);
                if (chartCtrl != null)
                {
                    charts.Add(chartCtrl);
                    m_chartMap.Add(bd.ParameterID, chartCtrl);

                    //TODO: custom chart event handle:

                }
            }

            return charts;
        }

        //private XamDataChart BuildHostChart(AxisDataBindingObject axisDataBindingObject)
        //{
        //    throw new NotImplementedException();
        //}

        void RelatedParameterIDs_CollectionChanged(object sender,
            System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                //add one chart to tail
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                //remove selected chart
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Move)
            {
                //move chartPositions
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Replace)
            {
                //replace chart
            }
            //else
            //{
            //Reset
            this.ResetCharts();
            //}
        }

        private FAChartModel m_faChartModel;

        //private FlightAnalysisViewModel m_viewModel = null;
        //public FlightAnalysisViewModel ViewModel
        //{
        //    get
        //    {
        //        return m_viewModel;
        //    }
        //    set
        //    {
        //        //viewmodel is no longer implement filter functions
        //        //turn to SubModel

        //        //if (m_viewModel != null)
        //        //    this.m_viewModel.FilterDataChanged -= m_viewModel_FilterDataChanged;

        //        m_viewModel = value;
        //        if (m_viewModel != null)
        //        {
        //            this.m_faChartModel = m_viewModel.GetFAChartModel();
        //        }
        //        else
        //        {
        //            this.m_faChartModel = null;
        //        }
        //        //this.m_viewModel.FilterDataChanged += m_viewModel_FilterDataChanged;

        //        //this.OnViewModelChanged();
        //    }
        //}

        #region old

        [Obsolete("Old")]
        void m_viewModel_FilterDataChanged(object sender, EventArgs e)
        {
            //this.OnViewModelChanged();
        }

        [Obsolete("Old")]
        private string GetParameterCaption(string p)
        {
            return ApplicationContext.Instance.GetFlightParameterCaption(p);
        }

        [Obsolete("Old")]
        private void OnViewModelChanged()
        {
            return;
            #region old
            //this.grdHost.RowDefinitions.Clear();

            ////先分析几个图，一个、两个、三个的做法不一样
            ////一个：全屏Grid
            ////两个：二分屏幕上下Grid
            ////三个或更多：三到n行Grid，行高固定

            //IEnumerable<AxisDataBindingObject> bindingObjs = this.CalculateBindingObjects();

            //var axisDataBindingObject = bindingObjs.First();
            ////must be at least one
            //if (axisDataBindingObject == null || m_subViewModel == null
            //    || string.IsNullOrEmpty(m_subViewModel.HostPanelID)
            //    || axisDataBindingObject.ParameterID != this.m_subViewModel.HostPanelID)
            //    return;

            //XamDataChart hostChart = this.BuildHostChart(axisDataBindingObject);

            //IEnumerable<XamDataChart> chartElements = this.CreateElements(bindingObjs);
            //if (chartElements != null)
            //{
            //    int items = chartElements.Count();
            //    if (items == 1)
            //    {
            //        var chart = chartElements.First();
            //        if (chart != null)
            //            this.grdHost.Children.Add(chart);
            //    }
            //    else if (items == 2)
            //    {
            //        this.grdHost.RowDefinitions.Add(new RowDefinition());
            //        this.grdHost.RowDefinitions.Add(new RowDefinition());
            //        var chart1 = chartElements.ElementAt(0);
            //        var chart2 = chartElements.ElementAt(1);
            //        if (chart1 != null)
            //            this.grdHost.Children.Add(chart1);
            //        if (chart2 != null)
            //        {
            //            Grid.SetRow(chart2, 1);
            //            this.grdHost.Children.Add(chart2);
            //        }
            //    }
            //    else if (items > 2)
            //    {
            //        for (int i = 0; i < items; i++)
            //        {
            //            this.grdHost.RowDefinitions.Add(
            //                new RowDefinition()
            //                {
            //                    Height = new GridLength(BindingChartUIFactory.MultiItemsRowHeight, GridUnitType.Pixel)
            //                }
            //                );
            //            var child = chartElements.ElementAt(i);
            //            Grid.SetRow(child, i);
            //            this.grdHost.Children.Add(child);
            //        }
            //    }
            //    //items == 0 那就return
            //}

            //CategoryXAxis xAxis = this.CreateXAxis(axisDataBindingObject);
            //NumericYAxis[] yAxis = this.CreateYAxis(axisDataBindingObject);
            //LineSeries serie = new LineSeries()
            //{
            //    ItemsSource = TestModelDataSource.Instance, // vm.ViewModel.RawDatas,
            //    XAxis = xAxis,
            //    MarkerType = Infragistics.Controls.Charts.MarkerType.None,
            //    Thickness = 2,
            //    YAxis = this.FindYAxis(yAxis, 1),
            //    ValueMemberPath = "Hp"//vm.Parameter.ParameterID,
            //};
            //this.chartTest.DataContext = TestModelDataSource.Instance;
            //this.chartTest.Axes.Add(xAxis);
            //this.chartTest.Axes.Add(yAxis[0]);
            //this.chartTest.Series.Add(serie);
            //return;

            //debug
            //this.grdHost.RowDefinitions.Clear();
            //this.grdHost.Children.Clear();

            //int count = bindingObjs.Count();
            //if (count >= 3)
            //{

            //    for (int i = 0; i < count; i++)
            //    {
            //        this.grdHost.RowDefinitions.Add(
            //            new RowDefinition()
            //            {
            //                Height = new GridLength(240, GridUnitType.Pixel)
            //            }
            //            ); break;
            //    }
            //    int row = 0;
            //    foreach (var one in bindingObjs)
            //    {
            //        XamDataChart chart3 = this.BindObject(one);
            //        if (chart3 != null)
            //        {
            //            this.grdHost.Children.Add(chart3);
            //            Grid.SetRow(chart3, row);
            //        }
            //        row++; break;
            //    }
            //}
            //else if (count == 2)
            //{
            //    //两个图
            //    XamDataChart chart1 = this.BindObject(bindingObjs.First());
            //    XamDataChart chart2 = this.BindObject(bindingObjs.Last());
            //    this.grdHost.RowDefinitions.Add(new RowDefinition());
            //    this.grdHost.RowDefinitions.Add(new RowDefinition());
            //    if (chart1 != null)
            //        this.grdHost.Children.Add(chart1);
            //    if (chart2 != null)
            //    {
            //        this.grdHost.Children.Add(chart2);
            //        Grid.SetRow(chart2, 1);
            //    }
            //}
            //else
            //{
            //    //单图
            //    XamDataChart chart = this.BindObject(bindingObjs.First());
            //    if (chart != null)
            //        this.grdHost.Children.Add(chart);
            //}
            #endregion old
        }

        [Obsolete("Old")]
        private XamDataChart BindObject(AxisDataBindingObject axisDataBindingObject)
        {
            if (axisDataBindingObject != null)
            {
                XamDataChart chart = new XamDataChart();
                CategoryXAxis xAxis = this.CreateXAxis(axisDataBindingObject);
                NumericYAxis[] yAxis = this.CreateYAxis(axisDataBindingObject);

                int counter = 0;
                foreach (var vm in axisDataBindingObject.RelatedParameterIDs)//.ViewModels)
                {
                    LineSeries serie = new LineSeries()
                    {
                        ItemsSource = TestModelDataSource.Instance, // vm.ViewModel.RawDatas,
                        XAxis = xAxis,
                        MarkerType = Infragistics.Controls.Charts.MarkerType.None,
                        Thickness = 2,
                        YAxis = this.FindYAxis(yAxis, counter),
                        ValueMemberPath = "Hp"//vm.Parameter.ParameterID,
                    };
                    chart.Series.Add(serie);
                    counter++;
                }

                return chart;
            }

            return null;
        }

        [Obsolete("Old")]
        private CategoryXAxis CreateXAxis(AxisDataBindingObject axisDataBindingObject)
        {
            CategoryXAxis xaxis = new CategoryXAxis()
            {
                Label = "{Second}",
                Foreground = new SolidColorBrush(Windows.UI.Colors.Black),
                FontSize = 18,
                LabelSettings = new AxisLabelSettings(),
                ItemsSource = TestModelDataSource.Instance//m_viewModel.RawDatas,
            };

            xaxis.LabelSettings.Extent = 35;
            xaxis.LabelSettings.Location = AxisLabelsLocation.OutsideBottom;

            return xaxis;
        }

        [Obsolete("Old")]
        private NumericYAxis[] CreateYAxis(AxisDataBindingObject axisDataBindingObject)
        {
            NumericYAxis xmYAxis = new NumericYAxis()
            {
                LabelSettings = new AxisLabelSettings(),
                FontSize = 18,
                Foreground = new SolidColorBrush(Windows.UI.Colors.Black),
            };
            xmYAxis.LabelSettings.Extent = 55;
            xmYAxis.LabelSettings.Location = AxisLabelsLocation.OutsideLeft;

            return new NumericYAxis[] { xmYAxis };
        }

        [Obsolete("Old")]
        private NumericYAxis FindYAxis(NumericYAxis[] yAxis, int counter)
        {
            if (yAxis != null && yAxis.Count() > 0)
            {
                if (counter + 1 > yAxis.Count())
                    return yAxis.First();
                else
                {
                    return yAxis.ElementAt(counter);
                }
            }
            return null;
        }

        [Obsolete("Old")]
        private KeyValuePair<string, ObservableCollection<FlightDataEntitiesRT.ParameterRawData>>
            Find(ObservableCollection<KeyValuePair<string,
            ObservableCollection<FlightDataEntitiesRT.ParameterRawData>>> observableCollection,
            FlightDataEntitiesRT.FlightParameter flightParameter)
        {
            foreach (var obv in observableCollection)
            {
                if (obv.Key == flightParameter.ParameterID)
                    return obv;
            }

            return new KeyValuePair<string, ObservableCollection<FlightDataEntitiesRT.ParameterRawData>>();
        }

        #endregion old
    }
}
