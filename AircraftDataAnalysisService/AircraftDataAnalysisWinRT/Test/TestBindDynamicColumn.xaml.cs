﻿using AircraftDataAnalysisWinRT.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using Telerik.UI.Xaml.Controls.Grid;
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
            List<RawDataRowViewModel> models = new List<RawDataRowViewModel>();
            ObservableCollection<IDictionary<string, Object>> dicList = new ObservableCollection<IDictionary<string, object>>();

            this.grid1.AutoGenerateColumns = false;
            models.Add(
                new RawDataRowViewModel() { Second = 1, Values = (new List<object>(new string[] { "A", "B", "C", "D" })) });
            models.Add(
                new RawDataRowViewModel() { Second = 2, Values = (new List<object>(new string[] { "B", "C", "D", "A" })) });
            models.Add(
                new RawDataRowViewModel() { Second = 3, Values = (new List<object>(new string[] { "C", "D", "A", "B" })) });
            models.Add(
                new RawDataRowViewModel() { Second = 4, Values = (new List<object>(new string[] { "D", "A", "B", "C" })) });

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



            this.grid1.Columns.Clear();
            this.grid1.Columns.Add(new DataGridTextColumn() { PropertyName = "Second", Name = "colSecond", Header = "秒" });

            this.grid1.Columns.Add(new DataGridTextColumn() { PropertyName = "1", Name = "colSecond1", Header = "1 col" });
            this.grid1.Columns.Add(new DataGridTextColumn() { PropertyName = "2", Name = "colSecond2", Header = "2 列" });
            this.grid1.Columns.Add(new DataGridTextColumn() { PropertyName = "3", Name = "colSecond3", Header = "3 set" });
            this.grid1.Columns.Add(new DataGridTextColumn() { PropertyName = "4", Name = "colSecond4", Header = "4 collection" });

            this.grid1.ItemsSource = dicList;
        }
    }
}
