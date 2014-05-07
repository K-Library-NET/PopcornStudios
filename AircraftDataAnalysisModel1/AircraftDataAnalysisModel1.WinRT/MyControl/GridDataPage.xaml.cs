using AircraftDataAnalysisModel1.WinRT.Domain;
using AircraftDataAnalysisWinRT.Domain;
using FlightDataReading.AircraftModel1;
using FlightDataReadingModel1.AircraftModel1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
        private FlightAnalysisViewModelOld m_viewModel;
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

            var para = e.Parameter as GridDataDisplayArg;

            base.OnNavigatedTo(e);

            Task.Run(new Action(() =>
            {
                AircraftModel1RawDataCollection collection = this.GenerateDataCollection(para);

                this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                    new Windows.UI.Core.DispatchedHandler(() =>
                    {
                        this.DataContext = collection;
                        this.ProgressBar1.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        this.ProgressBar1.IsIndeterminate = false;
                    }));
            }));
            //this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
            //    new Windows.UI.Core.DispatchedHandler(
            //        delegate()
            //        {
            //            GridDataDisplayArg arg = e.Parameter as GridDataDisplayArg;

            //            this.m_viewModel = arg.ViewModel;
            //            this.ReBindColumns(arg.ParameterIDs);
            //            this.sfDataPager.Source = this.ToSubset(
            //                m_viewModel.RawDatas, arg.StartSecond, arg.EndSecond);
            //        }));
        }

        private AircraftModel1RawDataCollection GenerateDataCollection(GridDataDisplayArg gridDataDisplayArg)
        {
            if (gridDataDisplayArg == null || gridDataDisplayArg.ParameterIDs == null
                || gridDataDisplayArg.ParameterIDs.Length <= 0)
                return null;

            var dataloader = gridDataDisplayArg.DataLoader;

            if (dataloader == null)
                dataloader = this.GetDataLoader();

            Task<string> dataResult = dataloader.LoadRawDataAsync(gridDataDisplayArg.ParameterIDs);

            //dataResult.Wait();

            List<FlightDataEntitiesRT.ParameterRawData> dts = new List<FlightDataEntitiesRT.ParameterRawData>();

            Dictionary<int, AircraftModel1RawData> secondsMap = new Dictionary<int, AircraftModel1RawData>();
            for (int i = 0; i < dataloader.CurrentFlight.EndSecond; i++)
            {
                secondsMap.Add(i, new AircraftModel1RawData() { Second = i });
            }

            AircraftModel1RawDataBuilder builder = new AircraftModel1RawDataBuilder();

            foreach (var p in gridDataDisplayArg.ParameterIDs)
            {
                var rawDt = dataloader.GetRawData(p);

                if (rawDt != null && rawDt.Count() > 0)
                {
                    foreach (var rt in rawDt)
                    {
                        if (!secondsMap.ContainsKey(rt.Second))
                            continue;

                        var rawDataItem = secondsMap[rt.Second];

                        builder.AssignValueExt(rawDataItem, rt);
                    }
                }
                // dts.AddRange(rawDt);
            }

            var changecd = from item in secondsMap
                           orderby item.Key ascending
                           select item.Value;

            return new AircraftModel1RawDataCollection(changecd);

            //dts.Sort(new Comparison<FlightDataEntitiesRT.ParameterRawData>(
            //    delegate(FlightDataEntitiesRT.ParameterRawData a, FlightDataEntitiesRT.ParameterRawData b)
            //    {
            //        if (a != null && b != null)
            //        {
            //            return a.Second - b.Second;
            //        }
            //        if (a == null && b != null)
            //            return -1;
            //        if (a != null && b == null)
            //            return 1;
            //        return 0;
            //    }));

            //return new AircraftModel1RawDataCollection(dts);
        }

        //private IEnumerable<FlightDataReading.AircraftModel1.AircraftModel1RawData> ToSubset(
        //    IEnumerable<FlightDataReading.AircraftModel1.AircraftModel1RawData> collection,
        //    int startSecond, int endSecond)
        //{
        //    var result = from one in collection
        //                 where one.Second >= startSecond && one.Second <= endSecond
        //                 select one;
        //    return result;
        //}

        //private void ReBindColumns(string[] parameterIDs)
        //{
        //    for (int i = 1; i < this.grdData.Columns.Count; i++)
        //    {
        //        var col = this.grdData.Columns[i];
        //        if (!parameterIDs.Contains(col.MappingName))
        //            col.IsHidden = true;
        //    }
        //}

        //private FlightDataEntitiesRT.FlightParameter[] GetFlightParameters()
        //{
        //    var flightParameters = ApplicationContext.Instance.GetFlightParameters(
        //        ApplicationContext.Instance.CurrentAircraftModel);

        //    var result = from one in flightParameters.Parameters
        //                 where one.ParameterID != "(NULL)" && this.ExistsParameter(one.ParameterID)
        //                 && m_viewModel.RelatedParameterSelected(one.ParameterID)
        //                 select one;

        //    return result.ToArray();
        //}

        //private bool ExistsParameter(string p)
        //{
        //    if (this.m_viewModel.AllParameterIDs.Contains(p))
        //        return true;
        //    return false;
        //}

        private AircraftAnalysisDataLoader GetDataLoader()
        {
            if (this.DataLoader == null)
            {
                this.DataLoader = new AircraftAnalysisDataLoader()
                {
                    CurrentFlight = ApplicationContext.Instance.CurrentFlight,
                    CurrentAircraftModel = ApplicationContext.Instance.CurrentAircraftModel
                };
            }
            return this.DataLoader;
        }

        public AircraftDataAnalysisModel1.WinRT.Domain.AircraftAnalysisDataLoader DataLoader
        {
            get;
            set;
        }
    }

    public class GridDataDisplayArg
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

        public AircraftDataAnalysisModel1.WinRT.Domain.AircraftAnalysisDataLoader DataLoader
        {
            get;
            set;
        }
    }
}
