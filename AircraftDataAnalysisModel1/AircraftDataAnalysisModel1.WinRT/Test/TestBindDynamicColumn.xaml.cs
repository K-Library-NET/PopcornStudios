using AircraftDataAnalysisWinRT.DataModel;
using AircraftDataAnalysisWinRT.Services;
using FlightDataEntitiesRT;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.IO;
using System.Linq;
//using Telerik.UI.Xaml.Controls.Grid;
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
    public sealed partial class TestBindDynamicColumn : AircraftDataAnalysisWinRT.Common.LayoutAwarePage
    {
        public TestBindDynamicColumn()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.DataContext = null;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(
                new DataColumn() { ColumnName = "Second", Caption = "秒", DataType = typeof(int) });
            dt.Columns.Add(
                new DataColumn() { ColumnName = "1", Caption = "1 col", DataType = typeof(string) });
            dt.Columns.Add(
                new DataColumn() { ColumnName = "2", Caption = "2 列", DataType = typeof(string) });
            dt.Columns.Add(
                new DataColumn() { ColumnName = "3", Caption = "3 set", DataType = typeof(string) });
            dt.Columns.Add(
                new DataColumn() { ColumnName = "4", Caption = "4 collection", DataType = typeof(string) });

            var row = dt.NewRow();
            row.Add("Second", 1);
            row.Add("1", "A"); row.Add("2", "B"); row.Add("3", "C"); row.Add("4", "D");
            dt.Rows.Add(row);

            row = dt.NewRow();
            row.Add("Second", 2);
            row.Add("2", "A"); row.Add("3", "B"); row.Add("4", "C"); row.Add("1", "D");
            dt.Rows.Add(row);

            row = dt.NewRow();
            row.Add("Second", 3);
            row.Add("3", "A"); row.Add("4", "B"); row.Add("1", "C"); row.Add("2", "D");
            dt.Rows.Add(row);

            row = dt.NewRow();
            row.Add("Second", 4);
            row.Add("4", "A"); row.Add("1", "B"); row.Add("2", "C"); row.Add("3", "D");
            dt.Rows.Add(row);



            List<RawDataRowViewModel> models = new List<RawDataRowViewModel>();
            ObservableCollection<IDictionary<string, Object>> dicList = new ObservableCollection<IDictionary<string, object>>();

            //this.grid1.AutoGenerateColumns = false;
            //models.Add(
            //    new RawDataRowViewModel() { Second = 1, Values = (new List<object>(new string[] { "A", "B", "C", "D" })) });
            //models.Add(
            //    new RawDataRowViewModel() { Second = 2, Values = (new List<object>(new string[] { "B", "C", "D", "A" })) });
            //models.Add(
            //    new RawDataRowViewModel() { Second = 3, Values = (new List<object>(new string[] { "C", "D", "A", "B" })) });
            //models.Add(
            //    new RawDataRowViewModel() { Second = 4, Values = (new List<object>(new string[] { "D", "A", "B", "C" })) });

            var dic = new ExpandoObject() as IDictionary<string, object>;// new Dictionary<string, object>();
            dic.Add("Second", 1);
            dic.Add("1", "A"); dic.Add("2", "B"); dic.Add("3", "C"); dic.Add("4", "D");
            dicList.Add(dic);
            dic = new ExpandoObject() as IDictionary<string, object>;//new Dictionary<string, object>();
            dic.Add("Second", 2);
            dic.Add("2", "A"); dic.Add("3", "B"); dic.Add("4", "C"); dic.Add("1", "D");
            dicList.Add(dic);
            dic = new ExpandoObject() as IDictionary<string, object>;//new Dictionary<string, object>();
            dic.Add("Second", 3);
            dic.Add("3", "A"); dic.Add("4", "B"); dic.Add("1", "C"); dic.Add("2", "D");
            dicList.Add(dic);
            dic = new ExpandoObject() as IDictionary<string, object>;//new Dictionary<string, object>();
            dic.Add("Second", 4);
            dic.Add("4", "A"); dic.Add("1", "B"); dic.Add("2", "C"); dic.Add("3", "D");
            dicList.Add(dic);



            //this.grid1.Columns.Clear();
            //this.grid1.Columns.Add(new DataGridTextColumn() { PropertyName = "Second", Name = "colSecond", Header = "秒" });

            //this.grid1.Columns.Add(new DataGridTextColumn() { PropertyName = "1", Name = "colSecond1", Header = "1 col" });
            //this.grid1.Columns.Add(new DataGridTextColumn() { PropertyName = "2", Name = "colSecond2", Header = "2 列" });
            //this.grid1.Columns.Add(new DataGridTextColumn() { PropertyName = "3", Name = "colSecond3", Header = "3 set" });
            //this.grid1.Columns.Add(new DataGridTextColumn() { PropertyName = "4", Name = "colSecond4", Header = "4 collection" });

            //this.grid1.ItemsSource = dt;// dicList;
        }

        private void ExtremumValuebindClick(object sender, RoutedEventArgs e)
        {
            //var extremumPointInfos = ServerHelper.GetExtremumPointInfos(new Flight()
            //{
            //    FlightID = "12110222",
            //    FlightName = "12110222-4.phy",
            //    Aircraft = new AircraftInstance()
            //    {
            //        AircraftModel = ServerHelper.GetCurrentAircraftModel(),
            //        AircraftNumber = "0004",
            //        LastUsed = DateTime.Now
            //    },
            //    StartSecond = 0,
            //    EndSecond = 4430
            //});

            //foreach (var ex in extremumPointInfos)
            //{
            //    System.Diagnostics.Debug.WriteLine(ex);
            //}
        }

        private void CategoryChart_PointerMoved_1(object sender, PointerRoutedEventArgs e)
        {
        }

        void xamDataChart1_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
        }

        private void line1_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {

        }

        private void DataChart_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {

        }
    }
}
