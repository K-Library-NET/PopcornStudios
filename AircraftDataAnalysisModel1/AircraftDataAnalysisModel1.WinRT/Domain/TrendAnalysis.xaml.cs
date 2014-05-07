using AircraftDataAnalysisWinRT;
using AircraftDataAnalysisWinRT.DataModel;
using AircraftDataAnalysisWinRT.Domain;
using AircraftDataAnalysisWinRT.Services;
using FlightDataEntitiesRT;
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

// “基本页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234237 上有介绍

namespace PStudio.WinApp.Aircraft.FDAPlatform.Domain
{
    /// <summary>
    /// 基本页，提供大多数应用程序通用的特性。
    /// </summary>
    public sealed partial class TrendAnalysis : AircraftDataAnalysisWinRT.Common.LayoutAwarePage
    {
        private FlightAnalysisViewModelOld m_viewModel;

        public TrendAnalysis()
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
        //protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        //{
        //}

        ///// <summary>
        ///// 保留与此页关联的状态，以防挂起应用程序或
        ///// 从导航缓存中放弃此页。值必须符合
        ///// <see cref="SuspensionManager.SessionState"/> 的序列化要求。
        ///// </summary>
        ///// <param name="pageState">要使用可序列化状态填充的空字典。</param>
        //protected override void SaveState(Dictionary<String, Object> pageState)
        //{
        //}

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(string.Format("Start trend chart:{0}", DateTime.Now));

            TrendAnalysisViewModel viewModel = this.GetStaticResourceViewModel();
            this.BindData(viewModel);

            System.Diagnostics.Debug.WriteLine(string.Format("end trend chart:{0}", DateTime.Now));
        }

        private void BindData(TrendAnalysisViewModel viewModel)
        {
            BindAircraft(viewModel);

            BindTrendData(viewModel);
        }

        private void BindTrendData(TrendAnalysisViewModel viewModel)
        {
            IEnumerable<AircraftInstance> selectedAircraft = this.GetSelectedAircrafts(viewModel);

            if (selectedAircraft != null && selectedAircraft.Count() > 0)
            {
                foreach (var aircraft in selectedAircraft)
                {
                    if (viewModel.ItemsMap.ContainsKey(aircraft.AircraftNumber))
                    {
                        this.AddData(aircraft, viewModel);
                        continue;
                    }

                    ExtremumPointInfo[] infos = ServerHelper.GetExtremumPointInfos(aircraft);
                    viewModel.ItemsMap.Add(aircraft.AircraftNumber, new List<ExtremumPointInfo>(infos));
                    this.AddData(aircraft, viewModel);
                }
            }
        }

        private void AddData(AircraftInstance aircraft, TrendAnalysisViewModel viewModel)
        {
            var items = viewModel.ItemsMap[aircraft.AircraftNumber];

            var result1 = from one in items
                          where TrendAnalysisItem.Fields.Contains(one.ParameterID)
                          group one by one.FlightID into gp
                          select gp;

            foreach (var gp in result1)
            {
                TrendAnalysisItem trenditem = this.AssignOneItem(gp);

                trenditem.FlightDateTime = gp.First().FlightDate;
                trenditem.AircraftNumber = aircraft.AircraftNumber;

                viewModel.T6Collection.Add(trenditem);
                viewModel.TtCollection.Add(trenditem);
                viewModel.ViCollection.Add(trenditem);
                viewModel.NHCollection.Add(trenditem);
                viewModel.NCollection.Add(trenditem);
                viewModel.KBCollection.Add(trenditem);
                viewModel.HpCollection.Add(trenditem);
            }
        }

        private TrendAnalysisItem AssignOneItem(IGrouping<string, ExtremumPointInfo> gp)
        {
            TrendAnalysisItem item = new TrendAnalysisItem();

            foreach (var g in gp)
            {
                if (g.ParameterID == "Hp")
                    item.Hp = g.MaxValue;
                if (g.ParameterID == "Tt")
                    item.Tt = g.MaxValue;
                if (g.ParameterID == "Vi")
                    item.Vi = g.MaxValue;
                if (g.ParameterID == "T6R")
                    item.T6R = g.MaxValue;
                if (g.ParameterID == "T6L")
                    item.T6L = g.MaxValue;
                if (g.ParameterID == "Nx")
                    item.Nx = g.MaxValue;
                if (g.ParameterID == "Ny")
                    item.Ny = g.MaxValue;
                if (g.ParameterID == "Nz")
                    item.Nz = g.MaxValue;
                if (g.ParameterID == "NHL")
                    item.NHL = g.MaxValue;
                if (g.ParameterID == "NHR")
                    item.NHR = g.MaxValue;
                if (g.ParameterID == "KCB")
                    item.KCB = g.MaxValue;
                if (g.ParameterID == "KZB")
                    item.KZB = g.MaxValue;
            }

            return item;
        }

        private IEnumerable<AircraftInstance> GetSelectedAircrafts(TrendAnalysisViewModel viewModel)
        {
            if (this.listAircrafts.ItemsSource != null && this.listAircrafts.ItemsSource is List<AircraftInstanceBindingClass>)
            {
                List<AircraftInstanceBindingClass> bindingClass = this.listAircrafts.ItemsSource as List<AircraftInstanceBindingClass>;

                var result = from one in bindingClass
                             where one.IsSelected
                             select one.AircraftInstance;

                return result;
            }

            return new AircraftInstance[] { };
            //throw new NotImplementedException();
        }

        private void BindAircraft(TrendAnalysisViewModel viewModel)
        {
            IEnumerable<AircraftInstance> instances = ServerHelper.GetAllAircrafts(
                ApplicationContext.Instance.CurrentAircraftModel);

            viewModel.AircraftInstances = new ObservableCollection<AircraftInstance>(instances);

            List<AircraftInstanceBindingClass> bindingClass = new List<AircraftInstanceBindingClass>();

            foreach (var instance in viewModel.AircraftInstances)
            {
                AircraftInstanceBindingClass binding = new AircraftInstanceBindingClass(instance);
                if (instance.AircraftNumber == ApplicationContext.Instance.CurrentFlight.Aircraft.AircraftNumber)
                {
                    binding.IsSelected = true;
                }
                binding.PropertyChanged += binding_PropertyChanged;
                bindingClass.Add(binding);
            }

            this.listAircrafts.ItemsSource = bindingClass; //viewModel.AircraftInstances;
        }

        void binding_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if ((string.IsNullOrEmpty(e.PropertyName) || e.PropertyName == "IsSelected")
                && sender is AircraftInstanceBindingClass)
            {
                var bindingClass = sender as AircraftInstanceBindingClass;

                TrendAnalysisViewModel vm = this.GetStaticResourceViewModel();
                if (bindingClass.IsSelected)
                {
                    if (vm.ItemsMap.ContainsKey(bindingClass.AircraftInstance.AircraftNumber))
                    {
                        this.AddData(bindingClass.AircraftInstance, vm);
                    }
                    else
                    {
                        ExtremumPointInfo[] infos = ServerHelper.GetExtremumPointInfos(bindingClass.AircraftInstance);
                        vm.ItemsMap.Add(bindingClass.AircraftInstance.AircraftNumber, new List<ExtremumPointInfo>(infos));
                        this.AddData(bindingClass.AircraftInstance, vm);
                    }
                }
                else
                {
                    this.RemoveData(bindingClass.AircraftInstance, vm);
                }
            }
            //throw new NotImplementedException();
        }

        private void RemoveData(AircraftInstance aircraft, TrendAnalysisViewModel viewModel)
        {
            //var items = viewModel.ItemsMap[aircraft.AircraftNumber];

            //var result1 = from one in items
            //              where TrendAnalysisItem.Fields.Contains(one.ParameterID)
            //              group one by one.FlightID into gp
            //              select gp;

            var subset = from one in viewModel.T6Collection
                         where one.AircraftNumber != aircraft.AircraftNumber
                         select one;

            viewModel.T6Collection = new TrendAnalysisSubViewModel(subset); //.Add(trenditem);
            viewModel.TtCollection = new TrendAnalysisSubViewModel(subset); //.Add(trenditem);
            viewModel.ViCollection = new TrendAnalysisSubViewModel(subset); //.Add(trenditem);
            viewModel.NHCollection = new TrendAnalysisSubViewModel(subset); //.Add(trenditem);
            viewModel.NCollection = new TrendAnalysisSubViewModel(subset); //.Add(trenditem);
            viewModel.KBCollection = new TrendAnalysisSubViewModel(subset); //.Add(trenditem);
            viewModel.HpCollection = new TrendAnalysisSubViewModel(subset); //.Add(trenditem);

            //foreach (var gp in result1)
            //{
            //    TrendAnalysisItem trenditem = this.AssignOneItem(gp);

            //    trenditem.FlightDateTime = gp.First().FlightDate;
            //    trenditem.AircraftNumber = aircraft.AircraftNumber;

            //    viewModel.T6Collection.Add(trenditem);
            //    viewModel.TtCollection.Add(trenditem);
            //    viewModel.ViCollection.Add(trenditem);
            //    viewModel.NHCollection.Add(trenditem);
            //    viewModel.NCollection.Add(trenditem);
            //    viewModel.KBCollection.Add(trenditem);
            //    viewModel.HpCollection.Add(trenditem);
            //}
        }

        private TrendAnalysisViewModel GetStaticResourceViewModel()
        {
            object val = null;
            if (this.Resources.TryGetValue("ViewModel", out val))
            {
                if (val != null && val is TrendAnalysisViewModel)
                {
                    return val as TrendAnalysisViewModel;
                }
            }
            return null;
        }

        private XamDataChart CreateTestChart()
        {
            XamDataChart chart = new XamDataChart() { Height = 240 };
            /*
            var data = new TrendAnalysisViewModel();
            chart.DataContext = data;

            // XAxis
            CategoryXAxis xmXAxis = new CategoryXAxis();
            xmXAxis.ItemsSource = data;
            xmXAxis.Label = "{FlightDateTime}";
            xmXAxis.LabelSettings = new AxisLabelSettings();
            xmXAxis.LabelSettings.Extent = 35;
            xmXAxis.LabelSettings.Location = AxisLabelsLocation.OutsideBottom;

            // YAxis
            NumericYAxis xmYAxis = new NumericYAxis();
            xmYAxis.LabelSettings = new AxisLabelSettings();
            xmYAxis.LabelSettings.Extent = 55;
            xmYAxis.LabelSettings.Location = AxisLabelsLocation.OutsideLeft;

            chart.Axes.Add(xmXAxis);
            chart.Axes.Add(xmYAxis);

            SplineAreaSeries series = new SplineAreaSeries();
            series.ValueMemberPath = "Value";
            series.XAxis = xmXAxis;
            series.YAxis = xmYAxis;
            series.ItemsSource = data;
            chart.Series.Add(series);
            */
            return chart;
        }

        //private IEnumerable<TestClass1> GetRelateds2()
        //{
        //    List<TestClass1> list = new List<TestClass1>();

        //    Random rand = new Random();

        //    for (int i = 0; i < 1000; i++)
        //    {
        //        var l = new TestClass1()
        //        {
        //            Hp = (float)rand.NextDouble(),
        //            Vi = (float)rand.NextDouble(),
        //            T6L = rand.Next(0, 2),
        //            T6R = rand.Next(0, 2),
        //            Second = i,
        //        };

        //        list.Add(l);
        //    }

        //    return list;
        //}

        [Obsolete]
        private System.Collections.ObjectModel.ObservableCollection<RelatedParameterViewModel> GetRelateds()
        {
            var flightParameters = ApplicationContext.Instance.GetFlightParameters(
                ApplicationContext.Instance.CurrentAircraftModel);

            string[] testps = new string[] { "Hp", "Vi", "T6L", "T6R" };

            var result = from one in flightParameters.Parameters
                         where testps.Contains(one.ParameterID)
                         select new RelatedParameterViewModel(m_viewModel, one)
                         ;

            return new System.Collections.ObjectModel.ObservableCollection<RelatedParameterViewModel>(result);
        }

        [Obsolete]
        private IEnumerable<FlightDataReading.AircraftModel1.AircraftModel1RawData> GetRawDatas(DataTable dataTable)
        {
            List<FlightDataReading.AircraftModel1.AircraftModel1RawData> list =
                new List<FlightDataReading.AircraftModel1.AircraftModel1RawData>();

            foreach (DataRow r in dataTable.Rows)
            {
                var item = this.GetDataItem(r);
                list.Add(item);
            }

            return list;
        }

        [Obsolete]
        private FlightDataReading.AircraftModel1.AircraftModel1RawData GetDataItem(DataRow r)
        {
            FlightDataReading.AircraftModel1.AircraftModel1RawData dt = new FlightDataReading.AircraftModel1.AircraftModel1RawData();

            dt.aT = Convert.ToSingle(r["aT"]);
            dt.CS = Convert.ToSingle(r["CS"]);
            dt.DR = Convert.ToSingle(r["DR"]);
            dt.Dx = Convert.ToSingle(r["Dx"]);
            dt.Dy = Convert.ToSingle(r["Dy"]);
            dt.Dz = Convert.ToSingle(r["Dz"]);
            dt.Et = Convert.ToSingle(r["Et"]);
            dt.EW = Convert.ToSingle(r["EW"]);
            dt.FY = Convert.ToSingle(r["FY"]);
            dt.GS = Convert.ToSingle(r["GS"]);
            dt.HG = Convert.ToSingle(r["HG"]);
            dt.Hp = Convert.ToSingle(r["Hp"]);
            dt.KCB = Convert.ToSingle(r["KCB"]);
            dt.KZB = Convert.ToSingle(r["KZB"]);
            dt.M = Convert.ToSingle(r["M"]);
            dt.NHL = Convert.ToSingle(r["NHL"]);
            dt.NHR = Convert.ToSingle(r["NHR"]);
            dt.NS = Convert.ToSingle(r["NS"]);
            dt.Nx = Convert.ToSingle(r["Nx"]);
            dt.Ny = Convert.ToSingle(r["Ny"]);
            dt.Nz = Convert.ToSingle(r["Nz"]);
            dt.Second = Convert.ToSingle(r["Second"]);
            dt.T6L = Convert.ToSingle(r["T6L"]);
            dt.T6R = Convert.ToSingle(r["T6R"]);
            dt.Tt = Convert.ToSingle(r["Tt"]);
            dt.Vi = Convert.ToSingle(r["Vi"]);
            dt.Vy = Convert.ToSingle(r["Vy"]);
            dt.Wx = Convert.ToSingle(r["Wx"]);
            dt.Wy = Convert.ToSingle(r["Wy"]);
            dt.Wz = Convert.ToSingle(r["Wz"]);
            dt.ZH = Convert.ToSingle(r["ZH"]);
            dt.ZS = Convert.ToSingle(r["ZS"]);

            return dt;
        }

    }

    public class AircraftInstanceBindingClass : AircraftDataAnalysisWinRT.Common.BindableBase
    {
        public AircraftInstanceBindingClass(AircraftInstance instance)
        {
            this.AircraftInstance = instance;
        }

        private bool m_isSelected = false;

        public bool IsSelected
        {
            get { return m_isSelected; }
            set
            {
                this.SetProperty<bool>(ref m_isSelected, value);
            }
        }

        public string AircraftName
        {
            get
            {
                return this.AircraftInstance.AircraftNumber;
            }
        }

        //public int Second
        //{
        //    get;
        //    set;
        //}

        //public float Hp
        //{
        //    get;
        //    set;
        //}

        //public float Vi
        //{
        //    get;
        //    set;
        //}

        //public int T6L
        //{
        //    get;
        //    set;
        //}

        //public int T6R
        //{
        //    get;
        //    set;
        //}

        public AircraftInstance AircraftInstance { get; set; }
    }
}
