using AircraftDataAnalysisWinRT;
using AircraftDataAnalysisWinRT.DataModel;
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

namespace PStudio.WinApp.Aircraft.FDAPlatform.Domain
{
    /// <summary>
    /// 基本页，提供大多数应用程序通用的特性。
    /// </summary>
    public sealed partial class StatReport : AircraftDataAnalysisWinRT.Common.LayoutAwarePage
    {
        public StatReport()
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var context = this.SetDataContext();

            //this.comboYear.SelectedValue = context.SelectModel.Years[0];
            this.comboYear.SelectedValue = context.SelectModel.Years[0];
            this.comboMonth.SelectedValue = context.SelectModel.Months[0];
            //this.comboMonth.DataContext = context;
            //this.DataContext = this.GetDataContext();

            //Old Test
            //System.Diagnostics.Debug.WriteLine(string.Format("Start:{0}", DateTime.Now));
            //FlightAnalysisViewModel viewModel = new FlightAnalysisViewModel();
            //System.Diagnostics.Debug.WriteLine(string.Format("before data retrieve:{0}", DateTime.Now));

            //viewModel.CurrentStartSecond = 0;
            //viewModel.CurrentEndSecond = ApplicationContext.Instance.CurrentFlight.EndSecond;
            //m_viewModel = viewModel;
            //m_viewModel.RelatedParameterCollection = this.GetRelateds();
            //m_viewModel.RefreshAndRetriveData();
            //this.DataContext = m_viewModel;
            //System.Diagnostics.Debug.WriteLine(string.Format("before bind:{0}", DateTime.Now));

            ////this.grdHost.ItemsSource = this.GetRawDatas(
            ////    m_viewModel.GridData);
            //System.Diagnostics.Debug.WriteLine(string.Format("end:{0}", DateTime.Now));
        }

        private StatReportViewModel SetDataContext()
        {
            StatReportViewModel viewModel = this.grdHost.DataContext as StatReportViewModel;
            //new StatReportViewModel();
            viewModel.SelectModel.Years.Clear();
            viewModel.SelectModel.Years.Add(new YearSelectViewModelItem() { Year = 2014, Display = "2014" });
            viewModel.SelectModel.Years.Add(new YearSelectViewModelItem() { Year = 2013, Display = "2013" });

            var flights = ServerHelper.GetAllFlights(ApplicationContext.Instance.CurrentAircraftModel);
            if (flights != null && flights.Count() > 0)
            {
                foreach (var flight in flights)
                {
                    var viewFlight =
                        new FlightSelectViewModelItem(viewModel.SelectModel)
                        {
                            FlightName = flight.FlightName,
                            FlightID = flight.FlightID,
                            IsSelected = false
                        };

                    if (ApplicationContext.Instance.CurrentFlight != null &&
                        flight.FlightID == ApplicationContext.Instance.CurrentFlight.FlightID)
                    {
                        viewFlight.IsSelected = true;
                    }

                    viewModel.SelectModel.Flights.Add(viewFlight);
                }
            }

            if (ApplicationContext.Instance.CurrentFlight == null &&
                viewModel.SelectModel.Flights.Count > 0 &&
                viewModel.SelectModel.Flights[0] is AllFlightSelectViewModelItem)
            {
                (viewModel.SelectModel.Flights[0] as AllFlightSelectViewModelItem).IsSelected = true;
            }

            viewModel.SelectModel.SelectedYear = viewModel.SelectModel.Years[0];
            viewModel.SelectModel.SelectedMonth = viewModel.SelectModel.Months[0];

            return viewModel;
        }

        private System.Collections.ObjectModel.ObservableCollection<RelatedParameterViewModel> GetRelateds()
        {
            var flightParameters = ApplicationContext.Instance.GetFlightParameters(
                ApplicationContext.Instance.CurrentAircraftModel);

            var result = from one in flightParameters.Parameters
                         select new RelatedParameterViewModel(m_viewModel, true, one);

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

        private IEnumerable<ParameterRawData> GetRawDatas(
            System.Collections.ObjectModel.ObservableCollection<KeyValuePair<string,
            System.Collections.ObjectModel.ObservableCollection<FlightDataEntitiesRT.ParameterRawData>>> observableCollection)
        {
            var res1 = from one in observableCollection
                       select one.Value.ToArray();
            List<ParameterRawData> dts = new List<ParameterRawData>();
            foreach (var res in res1)
            {
                dts.AddRange(res);
            }
            return dts;
        }
        private FlightAnalysisViewModel m_viewModel;
    }

    public class Labelconvertor : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return String.Format("{0} %", value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
