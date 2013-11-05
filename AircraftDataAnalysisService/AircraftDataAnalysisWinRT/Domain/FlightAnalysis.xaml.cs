using AircraftDataAnalysisWinRT;
using AircraftDataAnalysisWinRT.DataModel;
using AircraftDataAnalysisWinRT.Domain;
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
    public sealed partial class FlightAnalysis : AircraftDataAnalysisWinRT.Common.LayoutAwarePage
    {
        public FlightAnalysis()
        {
            this.InitializeComponent();
        }

        private FlightAnalysisViewModel m_viewModel = null;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //base.OnNavigatedTo(e);

            if (e.Parameter != null && e.Parameter is AircraftDataAnalysisWinRT.Common.DecisionWrap)
            {
                DecisionRecordFlightAnalysisViewModel viewModel1 = new DecisionRecordFlightAnalysisViewModel(
                    e.Parameter as AircraftDataAnalysisWinRT.Common.DecisionWrap);
                this.DataContext = viewModel1;
                this.grdCtrl.ViewModel = viewModel1;
                return;
            }
            else if (e.Parameter != null && e.Parameter is ExtremumReportItemWrap)
            {
                ExtremumInfoFlightAnalysisViewModel viewModel2 = new ExtremumInfoFlightAnalysisViewModel(
                    e.Parameter as ExtremumReportItemWrap);
                this.DataContext = viewModel2;
                this.grdCtrl.ViewModel = viewModel2;
                return;
            }

            FlightAnalysisViewModel viewModel = new FlightAnalysisViewModel();
            var panels = ApplicationContext.Instance.GetChartPanels(ApplicationContext.Instance.CurrentAircraftModel);
            if (e.Parameter != null && e.Parameter is FlightDataEntitiesRT.Charts.ChartPanel)
            {
                viewModel.CurrentPanel = e.Parameter as FlightDataEntitiesRT.Charts.ChartPanel;
            }
            else if (panels is IEnumerable<FlightDataEntitiesRT.Charts.ChartPanel>)
            {
                if (panels != null && panels.Count() > 0)
                {
                    viewModel.CurrentPanel = panels.First();
                }
            }
            viewModel.CurrentStartSecond = 0;
            viewModel.CurrentEndSecond = ApplicationContext.Instance.CurrentFlight.EndSecond;
            m_viewModel = viewModel;

            //m_viewModel.RefreshAndRetriveData();
            this.DataContext = m_viewModel;
            this.grdCtrl.ViewModel = m_viewModel;
            this.chartCtrl.ViewModel = m_viewModel;
            ////liangdawen 20131028 如果没有参数，那就默认CurrentAircraft、CurrentFlight还有第一个面板
            ////我只写默认算了，不做Try catch调试

            //if (ApplicationContext.Instance.CurrentFlight == null)
            //    return;//暂定只能选定架次


            //var tops = AircraftDataAnalysisWinRT.Services.ServerHelper.GetLevelTopFlightRecords(
            //    ApplicationContext.Instance.CurrentFlight, null);

            //var table = AircraftDataAnalysisWinRT.Services.ServerHelper.GetData(
            //    ApplicationContext.Instance.CurrentFlight, null, 200, 400);

            //绑定面板，触发第一个参数SelectChange
        }

        private FlightDataEntitiesRT.Charts.ChartPanel ParseCurrentPanel(
            IEnumerable<FlightDataEntitiesRT.Charts.ChartPanel> rtPanels, object parameter)
        {
            //liangdawen debug: 直接就算第一个面板了
            return rtPanels.First();
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

        private void btSelectPanel_Click(object sender, RoutedEventArgs e)
        {
            ChartPanelViewModel viewModel = new ChartPanelViewModel() { CurrentPanel = this.GetCurrentPanel() };

            this.Frame.Navigate(typeof(AircraftDataAnalysisWinRT.MyControl.SwitchChartPanelConfirmPage), viewModel);
        }

        private FlightDataEntitiesRT.Charts.ChartPanel GetCurrentPanel()
        {
            return this.m_viewModel.CurrentPanel;
        }
    }
}
