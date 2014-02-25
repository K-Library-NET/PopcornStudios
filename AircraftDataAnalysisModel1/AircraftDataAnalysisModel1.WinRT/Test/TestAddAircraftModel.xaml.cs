using AircraftDataAnalysisWinRT.Domain;
using AircraftDataAnalysisWinRT.Services;
using FlightDataEntitiesRT;
using System;
using System.Collections.Generic;
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

namespace AircraftDataAnalysisWinRT.Test
{
    /// <summary>
    /// 基本页，提供大多数应用程序通用的特性。
    /// </summary>
    public sealed partial class TestAddAircraftModel : AircraftDataAnalysisWinRT.Common.LayoutAwarePage
    {
        public TestAddAircraftModel()
        {
            this.InitializeComponent();
        }

        private FlightAnalysisViewModel m_viewModel;

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

        /// <summary>
        /// 用ServerHelper读取所有架次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRefreshAircraftClicked(object sender, RoutedEventArgs e)
        {//TODO:
            //var flights = ServerHelper.GetAllFlights(ServerHelper.GetCurrentAircraftModel());
            //var flightViewModels = from one in flights
            //                       select new AircraftDataAnalysisWinRT.DataModel.FaultDiagnosisViewModel(one.FlightID,
            //                           one.FlightName, string.Empty, string.Empty, one.Aircraft.AircraftModel.Caption,
            //                           string.Format("{0} {1} {2} {3}",
            //                           one.FlightID, one.FlightName, one.EndSecond, one.Aircraft.AircraftModel.ModelName),
            //                           null);
            //this.viewModels.ItemsSource = flightViewModels;
        }

        /// <summary>
        /// 添加或更新一个架次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAddAircraftClicked(object sender, RoutedEventArgs e)
        {//TODO:
            var aircraftModel = ServerHelper.GetCurrentAircraftModel();

            Flight flight = new Flight()
            {
                Aircraft = new AircraftInstance()
                {
                    AircraftModel = aircraftModel,
                    AircraftNumber = "0004",
                    LastUsed = DateTime.Now
                },
                FlightID = "04110325",
                FlightName = "04110325.phy",
                StartSecond = 0,
                EndSecond = 2236,
            };
            flight = Services.DataInputHelper.AddOrReplaceFlight(flight);
        }

        /// <summary>
        /// 读取所有参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnReadParameters(object sender, RoutedEventArgs e)
        {//TODO:
            ServerHelper.GetFlightParameters(ServerHelper.GetCurrentAircraftModel());
        }

        /// <summary>
        /// 读取所有判据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnReadDecisions(object sender, RoutedEventArgs e)
        {//TODO:
            var decisions = ServerHelper.GetDecisions(ServerHelper.GetCurrentAircraftModel());
        }

        /// <summary>
        /// 读取所有面板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnChartPanelRead(object sender, RoutedEventArgs e)
        {//TODO:
            var chartPanels = ServerHelper.GetChartPanels(ServerHelper.GetCurrentAircraftModel());

        }

        /// <summary>
        /// 不用在此添加，背后配置先写死
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Obsolete]
        private void OnCurrentAicraftModelChartsAdded(object sender, RoutedEventArgs e)
        {

        }

        private void OnReadAircraftClicked(object sender, RoutedEventArgs e)
        {
            var dt = ServerHelper.GetDataTable(new Flight()
             {
                 Aircraft = new AircraftInstance()
                 {
                     AircraftModel = ServerHelper.GetCurrentAircraftModel(),
                     AircraftNumber = "0004",
                     LastUsed = DateTime.Now
                 },
                 FlightID = "781102221",
                 FlightName = "781102221-1.phy",
                 StartSecond = 0,
                 EndSecond = 5520
             }, new string[] { "Et", "Hp" }, 0, 200);


            var result2 = new FlightParameter[]{ new FlightParameter(){ ParameterID = "Et", Caption = "飞行时间"},
                 new FlightParameter(){ Caption = "大气压", ParameterID = "Hp"}};
            //GetFlightParameters();

            //this.grdView.AutoGenerateColumns = false;
            //this.grdView.Columns.Clear();
            //this.ColumnCollection = new ObservableCollection<Telerik.UI.Xaml.Controls.Grid.DataGridColumn>();

            if (result2 != null && result2.Count() > 0)
            {
                int i = 0;
                foreach (var one in result2)
                {
                    //这里才是去掉NULL值
                    if (one.ParameterID == "(NULL)")
                        continue;

                    //Telerik.UI.Xaml.Controls.Grid.DataGridColumn col
                    //    = new Telerik.UI.Xaml.Controls.Grid.DataGridTextColumn()
                    //    {
                    //        Name = one.ParameterID,
                    //        PropertyName = one.ParameterID,//"Values[" + i.ToString() + "]",
                    //        CanUserEdit = false,
                    //        Header = one.Caption
                    //    };

                    //this.grdView.Columns.Add(col);
                    i++;
                }
            }

            //this.RawDataRowViewModel.Columns.Insert(0, new DataColumn() { Caption = "秒值", ColumnName = "Second", DataType = typeof(int) });

            //this.grdView.Columns.Insert(0,
            //    new Telerik.UI.Xaml.Controls.Grid.DataGridTextColumn()
            //    {
            //        Name = "Second",
            //        PropertyName = "Second",
            //        Header = "秒值"
            //    });
            //this.grdView.ItemsSource = dt;
        }

        private void OnReadLevelTopClicked(object sender, RoutedEventArgs e)
        {
            // ServerHelper.GetExtremumPointInfos(
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(string.Format("Start trend chart:{0}", DateTime.Now));
            base.OnNavigatedTo(e);

            FlightAnalysisViewModel viewModel = new FlightAnalysisViewModel();
            var panels = ApplicationContext.Instance.GetChartPanels(ApplicationContext.Instance.CurrentAircraftModel);

            viewModel.CurrentStartSecond = 0;
            viewModel.CurrentEndSecond = ApplicationContext.Instance.CurrentFlight.EndSecond;
            m_viewModel = viewModel;
            m_viewModel.RelatedParameterCollection = this.GetRelateds();
            m_viewModel.RefreshAndRetriveData();
            this.DataContext = m_viewModel;

            m_viewModel.RefreshAndRetriveData();
            System.Diagnostics.Debug.WriteLine(string.Format("before trend chart bind:{0}", DateTime.Now));
            //var dts = this.GetRawDatas(m_viewModel.GridData);

            //this.sfchart.Series["Hp"].ItemsSource = dts;
            //this.sfchart.Series["Vi"].ItemsSource = dts;
            //this.sfchart.Series["T6L"].ItemsSource = dts;
            //this.sfchart.Series["T6R"].ItemsSource = dts;

            //this.sfchart.DataContext = m_viewModel.EntityData;
            System.Diagnostics.Debug.WriteLine(string.Format("end trend chart:{0}", DateTime.Now));
        }

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
}
