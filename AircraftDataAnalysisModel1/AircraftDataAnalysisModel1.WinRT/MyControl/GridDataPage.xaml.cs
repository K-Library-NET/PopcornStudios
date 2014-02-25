using AircraftDataAnalysisWinRT.Domain;
using System;
using System.Collections.Generic;
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

namespace AircraftDataAnalysisWinRT.MyControl
{
    /// <summary>
    /// 基本页，提供大多数应用程序通用的特性。
    /// </summary>
    public sealed partial class GridDataPage : AircraftDataAnalysisWinRT.Common.LayoutAwarePage
    {
        private FlightAnalysisViewModel m_viewModel;
        public GridDataPage()
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
            if (e.Parameter == null || !(e.Parameter is GridDataDisplayArg))
            {
                return;
            }

            base.OnNavigatedTo(e);

            this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                new Windows.UI.Core.DispatchedHandler(
                    delegate()
                    {
                        GridDataDisplayArg arg = e.Parameter as GridDataDisplayArg;

                        this.m_viewModel = arg.ViewModel;
                        this.ReBindColumns(arg.ParameterIDs);
                        this.sfDataPager.Source = this.ToSubset(
                            m_viewModel.RawDatas, arg.StartSecond, arg.EndSecond);
                    }));
        }

        private IEnumerable<FlightDataReading.AircraftModel1.AircraftModel1RawData> ToSubset(
            IEnumerable<FlightDataReading.AircraftModel1.AircraftModel1RawData> collection,
            int startSecond, int endSecond)
        {
            var result = from one in collection
                         where one.Second >= startSecond && one.Second <= endSecond
                         select one;
            return result;
        }

        private void ReBindColumns(string[] parameterIDs)
        {
            for (int i = 1; i < this.grdData.Columns.Count; i++)
            {
                var col = this.grdData.Columns[i];
                if (!parameterIDs.Contains(col.MappingName))
                    col.IsHidden = true;
            }
        }

        private FlightDataEntitiesRT.FlightParameter[] GetFlightParameters()
        {
            var flightParameters = ApplicationContext.Instance.GetFlightParameters(
                ApplicationContext.Instance.CurrentAircraftModel);

            var result = from one in flightParameters.Parameters
                         where one.ParameterID != "(NULL)" && this.ExistsParameter(one.ParameterID)
                         && m_viewModel.RelatedParameterSelected(one.ParameterID)
                         select one;

            return result.ToArray();
        }

        private bool ExistsParameter(string p)
        {
            if (this.m_viewModel.AllParameterIDs.Contains(p))
                return true;
            return false;
        }
    }

    internal class GridDataDisplayArg
    {
        public int StartSecond
        {
            get;
            set;
        }

        public int EndSecond
        {
            get;
            set;
        }

        public string[] ParameterIDs
        {
            get;
            set;
        }

        public FlightAnalysisViewModel ViewModel
        {
            get;
            set;
        }
    }
}
