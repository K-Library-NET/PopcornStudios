using AircraftDataAnalysisWinRT;
using AircraftDataAnalysisWinRT.Common;
using AircraftDataAnalysisWinRT.DataModel;
using AircraftDataAnalysisWinRT.Services;
using FlightDataEntitiesRT.Decisions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
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
    public sealed partial class FaultDiagnosis : AircraftDataAnalysisWinRT.Common.LayoutAwarePage
    {
        public FaultDiagnosis()
        {
            this.InitializeComponent();

            //rdgList.SelectionChanged += rdgList_SelectionChanged;
        }

        private int selectCount = 0;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            FaultDiagnosisViewModel viewModel = new FaultDiagnosisViewModel();

            this.DataContext = viewModel;
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

        private void OnDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
        }

        private void OnNavigateToPanelClick(object sender, RoutedEventArgs e)
        {
            NavigateToPanel();
        }

        private void NavigateToPanel()
        {
            if (this.rdgList.SelectedItem != null && this.rdgList.SelectedItem is DecisionWrap)
            {
                DecisionWrap wrap = this.rdgList.SelectedItem as DecisionWrap;
                if (wrap.Record == null || wrap.Decision == null)
                    return;

                string parameterStr = this.ToParameters(wrap.Decision.RelatedParameters);
                string seconds = string.Format("second={0}_{1}", wrap.Record.StartSecond, wrap.Record.EndSecond);
                string urlParameters = "?type=custom&" + parameterStr + "&" + seconds;
                //  this.Frame.Navigate(typeof(FlightAnalysis), urlParameters);
                this.Frame.Navigate(typeof(FlightAnalysis), wrap);
            }
        }

        private string ToParameters(string[] parameterIds)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string p1 in parameterIds)
            {
                if (builder.Length == 0)
                    builder.Append("paramids=");
                else
                    builder.Append(',');

                builder.Append(p1);
            }
            return builder.ToString();
        }

        private void OnDoubleTapped2(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (e.OriginalSource != null && e.OriginalSource is Windows.UI.Xaml.FrameworkElement
                && (e.OriginalSource as Windows.UI.Xaml.FrameworkElement).DataContext != null
                && (e.OriginalSource as Windows.UI.Xaml.FrameworkElement).DataContext is DecisionWrap)
            {
                DecisionWrap wrap = (e.OriginalSource as Windows.UI.Xaml.FrameworkElement).DataContext as DecisionWrap;
                if (wrap.Record != null && wrap.Decision != null)
                {
                    this.Frame.Navigate(typeof(FlightAnalysis), wrap);
                }
            }
        }

        private void OnRowSelectedChanged(object sender, Syncfusion.UI.Xaml.Grid.GridSelectionChangedEventArgs e)
        {
            if (this.DataContext != null && this.DataContext is FaultDiagnosisViewModel)
            {
                if (this.rdgList.SelectedItem == null || !(this.rdgList.SelectedItem is DecisionWrap)
                    || (this.rdgList.SelectedItem as DecisionWrap).Record == null)
                {
                    (this.DataContext as FaultDiagnosisViewModel).IsValueSelected = false;
                }
                else
                {
                    (this.DataContext as FaultDiagnosisViewModel).IsValueSelected = true;
                }
            }
        }

    }

    internal class FaultEventLevelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                int eventlevel = -1;
                if (int.TryParse(value.ToString(), out eventlevel))
                {
                    if (eventlevel == 1)
                        return new SolidColorBrush(Windows.UI.Colors.Red);
                    else if (eventlevel == 2)
                        return new SolidColorBrush(Windows.UI.Colors.Blue);
                }
            }

            return new SolidColorBrush(Windows.UI.Colors.Gray);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
