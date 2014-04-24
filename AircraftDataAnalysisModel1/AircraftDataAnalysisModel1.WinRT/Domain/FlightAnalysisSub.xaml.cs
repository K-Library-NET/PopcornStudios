using AircraftDataAnalysisModel1.WinRT.DataModel;
using AircraftDataAnalysisWinRT.Common;
using AircraftDataAnalysisWinRT.DataModel;
using AircraftDataAnalysisWinRT.MyControl;
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

        private AircraftDataAnalysisModel1.WinRT.DataModel.FlightAnalysisSubViewModel GetRootViewModel()
        {
            object value = null;
            if (this.Resources.TryGetValue("datacontext", out value))
            {
                if (value != null && (value is AircraftDataAnalysisModel1.WinRT.DataModel.FlightAnalysisSubViewModel))
                    return value as AircraftDataAnalysisModel1.WinRT.DataModel.FlightAnalysisSubViewModel;
            }
            return null;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            System.Diagnostics.Debug.WriteLine(string.Format("Start analysis sub:{0}", DateTime.Now));

            var rootViewModel = this.GetRootViewModel();
            this.m_viewModel = rootViewModel;
            if (m_viewModel.SerieGroup == null)
                m_viewModel.SerieGroup = new FlightAnalysisChartGroupSubExtendedViewModel();
            if (rootViewModel == null || e.Parameter == null)
                return;

            if (!(e.Parameter is SubEditChartNavigationParameter))
            {
                return;
            }

            SubEditChartNavigationParameter parameter = e.Parameter as SubEditChartNavigationParameter;
            if (parameter.DataLoader != null)
                rootViewModel.DataLoader = parameter.DataLoader;
            else
                rootViewModel.DataLoader = new AircraftDataAnalysisModel1.WinRT.Domain.AircraftAnalysisDataLoader()
                {
                    CurrentAircraftModel = ApplicationContext.Instance.CurrentAircraftModel,
                    CurrentFlight = ApplicationContext.Instance.CurrentFlight
                };
            rootViewModel.UserThreadInvoker = this.Dispatcher;
            rootViewModel.HostParameterID = parameter.HostParameterID;

            if (parameter is ExtremumReportSubEditChartNavigationParameter)
            {
                ExtremumReportSubEditChartNavigationParameter extParameter = parameter as ExtremumReportSubEditChartNavigationParameter;

                tracker1.Axes["xm1YAxis1"].CrossingValue = extParameter.MaxValueSecond;
                tracker1.Axes["xm1YAxis2"].CrossingValue = extParameter.MinValueSecond;
            }

            //TODO: Initialize DataLoader

            //FlightAnalysisNavigationParameter navPara = null;
            //if (e != null)
            //{
            //    if (e.Parameter != null && e.Parameter is FlightAnalysisNavigationParameter)
            //    {
            //        navPara = e.Parameter as FlightAnalysisNavigationParameter;
            //        if (!string.IsNullOrEmpty(navPara.SelectedPanelID))
            //        {
            //            rootViewModel.SetCurrentPanel(navPara.SelectedPanelID);
            //        }
            //    }
            //}
            //else
            //{
            //}

            //if (navPara != null && navPara.DataLoader != null
            //    && navPara.DataLoader.CurrentFlight.FlightID == ApplicationContext.Instance.CurrentFlight.FlightID)
            //    rootViewModel.DataLoader = navPara.DataLoader;
            //else
            //{
            //    rootViewModel.DataLoader = new AircraftDataAnalysisModel1.WinRT.Domain.AircraftAnalysisDataLoader()
            //    {
            //        CurrentFlight = ApplicationContext.Instance.CurrentFlight,
            //        CurrentAircraftModel = ApplicationContext.Instance.CurrentAircraftModel
            //    };
            //}
            rootViewModel.InitializeAsync(parameter);
        }

        private void btnTrackModeSelect_Click(object sender, RoutedEventArgs e)
        {
            this.m_viewModel.SerieGroup.IsTrackMode = true;
        }

        private void btnZoomModeSelect_Click(object sender, RoutedEventArgs e)
        {
            this.m_viewModel.SerieGroup.IsZoomMode = true;
        }

        private void btnEditParameters_Click(object sender, RoutedEventArgs e)
        {
            AircraftDataAnalysisWinRT.MyControl.SubEditChartNavigationParameter parameter = this.GenerateNavigationParameter();
            this.Frame.Navigate(typeof(AircraftDataAnalysisWinRT.MyControl.SwitchChartPanelConfirmPage), parameter);
        }

        private MyControl.SubEditChartNavigationParameter GenerateNavigationParameter()
        {
            throw new NotImplementedException();
        }

        private void OnNavigateToHome_Clicked(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PStudio.WinApp.Aircraft.FDAPlatform.MainPage));
        }

    }

    internal class BooleanRowHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                bool val = true;
                if (bool.TryParse(value.ToString(), out val))
                {
                    if (val)
                        return new GridLength(1, GridUnitType.Star);
                }
            }
            return new GridLength(0, GridUnitType.Pixel);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    [Obsolete]
    class FASubNavigateParameter
    {
        public string HostParameterID
        {
            get;
            set;
        }

        public AircraftDataAnalysisModel1.WinRT.Domain.AircraftAnalysisDataLoader DataLoader { get; set; }

        public int FlightStartSecond { get; set; }

        public int FlightEndSecond { get; set; }
    }

    [Obsolete]
    class ExtremumReportFASubNavigateParameter : FASubNavigateParameter
    {

        public int MinValueSecond { get; set; }

        public int MaxValueSecond { get; set; }
    }

    [Obsolete]
    class FaultDiagnosisFASubNavigateParameter : FASubNavigateParameter
    {
        public string[] RelatedParameterIDs { get; set; }

        public int DecisionStartSecond { get; set; }

        public int DecisionEndSecond { get; set; }

        public int DecisionHappenSecond { get; set; }
    }
}
