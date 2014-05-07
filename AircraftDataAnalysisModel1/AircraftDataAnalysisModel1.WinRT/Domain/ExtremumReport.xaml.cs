using AircraftDataAnalysisWinRT;
using AircraftDataAnalysisWinRT.DataModel;
using AircraftDataAnalysisWinRT.Services;
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
    public sealed partial class ExtremumReport : AircraftDataAnalysisWinRT.Common.LayoutAwarePage
    {
        public ExtremumReport()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 读取当前架次的极值信息
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            AircraftDataAnalysisWinRT.DataModel.ExtremumReportViewModel viewModel =
                new AircraftDataAnalysisWinRT.DataModel.ExtremumReportViewModel();
            this.DataContext = viewModel;

            //var extremumInfos = ServerHelper.GetExtremumPointInfos(ApplicationContext.Instance.CurrentFlight); 
            //this.rdgList.ItemsSource = extremumInfos;
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

        private void OnNavToPanel(object sender, RoutedEventArgs e)
        {
            if (this.rdgList.SelectedItem != null && this.rdgList.SelectedItem is ExtremumReportItemWrap)
            {
                var wrap = this.rdgList.SelectedItem as ExtremumReportItemWrap;
                AircraftDataAnalysisWinRT.Domain.FASubNavigateParameter param =
                    this.GenerateParam(wrap);
                this.Frame.Navigate(typeof(AircraftDataAnalysisWinRT.Domain.FlightAnalysisSub), param);
            }
        }

        private void OnGridDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (e.OriginalSource != null && e.OriginalSource is Windows.UI.Xaml.FrameworkElement
                && (e.OriginalSource as Windows.UI.Xaml.FrameworkElement).DataContext != null
                && (e.OriginalSource as Windows.UI.Xaml.FrameworkElement).DataContext is ExtremumReportItemWrap)
            {
                ExtremumReportItemWrap wrap = (e.OriginalSource as Windows.UI.Xaml.FrameworkElement).DataContext as ExtremumReportItemWrap;
                //AircraftDataAnalysisWinRT.Domain.FASubNavigateParameter param =
                //    this.GenerateParam(wrap);

                AircraftDataAnalysisWinRT.MyControl.SubEditChartNavigationParameter parameter
                    = new AircraftDataAnalysisWinRT.MyControl.ExtremumReportSubEditChartNavigationParameter()
                    {
                        HostParameterID = wrap.ParameterID,
                        HostParameterYAxis = FlightAnalysisSubViewYAxis.LeftYAxis,
                        MinValueSecond = wrap.MinValueSecond,
                        MaxValueSecond = wrap.MaxValueSecond,
                    };

                this.Frame.Navigate(typeof(AircraftDataAnalysisWinRT.Domain.FlightAnalysisSubLite),
                    //typeof(AircraftDataAnalysisWinRT.Domain.FlightAnalysisSub), 
                    parameter);
            }
        }

        private AircraftDataAnalysisWinRT.Domain.FASubNavigateParameter
            GenerateParam(ExtremumReportItemWrap wrap)
        {
            var param = new AircraftDataAnalysisWinRT.Domain.ExtremumReportFASubNavigateParameter()
            {
                HostParameterID = wrap.ParameterID,
                DataLoader = null,
                MinValueSecond = wrap.MinValueSecond,
                MaxValueSecond = wrap.MaxValueSecond,
                FlightEndSecond = ApplicationContext.Instance.CurrentFlight.EndSecond,
                FlightStartSecond = 0
            };

            return param;
        }

        private void OnRowSelectedChanged(object sender, Syncfusion.UI.Xaml.Grid.GridSelectionChangedEventArgs e)
        {
            if (this.DataContext != null && this.DataContext is ExtremumReportViewModel)
            {
                if (this.rdgList.SelectedItem == null || !(this.rdgList.SelectedItem is ExtremumReportItemWrap))
                {
                    (this.DataContext as ExtremumReportViewModel).IsValueSelected = false;
                }
                else
                {
                    (this.DataContext as ExtremumReportViewModel).IsValueSelected = true;
                }
            }
        }

    }
}
