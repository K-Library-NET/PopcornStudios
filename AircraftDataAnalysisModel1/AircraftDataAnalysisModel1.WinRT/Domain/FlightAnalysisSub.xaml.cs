using AircraftDataAnalysisWinRT.Common;
using AircraftDataAnalysisWinRT.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    /// 叶子级别详细页面，包含增加参数、图表联动、缩放等效果
    /// </summary>
    public sealed partial class FlightAnalysisSub : AircraftDataAnalysisWinRT.Common.LayoutAwarePage
    {
        private FlightAnalysisSubViewModel m_viewModel;
        public FlightAnalysisSub()
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

            if (e == null || e.Parameter == null
                || !(e.Parameter is FlightAnalysisSubNavigateParameter))
                return;

            FlightAnalysisSubNavigateParameter parameter = e.Parameter as FlightAnalysisSubNavigateParameter;

            FlightAnalysisSubViewModel viewModel = FlightAnalysisSubViewModelFactory.Create(ApplicationContext.Instance.GetViewModelByCurrentFlight(),
                ApplicationContext.Instance.CurrentFlight,
                parameter.HostParameterID);

            this.m_viewModel = viewModel;
            this.DataContext = m_viewModel;

            this.chartCtrl.DataContext = m_viewModel;
            this.gridCtrl.DataContext = m_viewModel;
            this.chartCtrl.SubViewModel = m_viewModel;
            this.gridCtrl.SubViewModel = m_viewModel;
        }



    }

    class FlightAnalysisSubNavigateParameter
    {
        public string HostParameterID
        {
            get;
            set;
        }

        public int CurrentStartSecond { get; set; }

        public int CurrentEndSecond { get; set; }
    }
}
