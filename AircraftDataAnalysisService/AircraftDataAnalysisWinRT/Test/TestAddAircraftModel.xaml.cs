﻿using AircraftDataAnalysisWinRT.Services;
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

namespace AircraftDataAnalysisWinRT.Test
{
    /// <summary>
    /// 基本页，提供大多数应用程序通用的特性。
    /// </summary>
    public sealed partial class TestAddAircraftModel : AircraftDataAnalysisWinRT.Common.LayoutAwarePage
    {
        public TestAddAircraftModel()
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

        /// <summary>
        /// 用ServerHelper读取所有架次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRefreshAircraftClicked(object sender, RoutedEventArgs e)
        {//TODO:
            var flights = ServerHelper.GetAllFlights(ServerHelper.GetCurrentAircraftModel());
            var flightViewModels = from one in flights
                                   select new AircraftDataAnalysisWinRT.DataModel.FlightDataItem(one.FlightID,
                                       one.FlightName, string.Empty, string.Empty, one.Aircraft.AircraftModel.Caption,
                                       string.Format("{0} {1} {2} {3}",
                                       one.FlightID, one.FlightName, one.EndSecond, one.Aircraft.AircraftModel.ModelName),
                                       null);
            this.viewModels.ItemsSource = flightViewModels;
        }

        /// <summary>
        /// 添加或更新一个架次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAddAircraftClicked(object sender, RoutedEventArgs e)
        {//TODO:
            var aircraftModel = ServerHelper.GetCurrentAircraftModel();

            Flight flight = new Flight()
            {
                Aircraft = new AircraftInstance()
                {
                    AircraftModel = aircraftModel,
                    AircraftNumber = "0004",
                    LastUsed = DateTime.Now
                },
                FlightID = "04110325",
                FlightName = "04110325.phy",
                StartSecond = 0,
                EndSecond = 2236,
            };
            flight = Services.DataInputHelper.AddOrReplaceFlight(flight);
        }

        /// <summary>
        /// 读取所有参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnReadParameters(object sender, RoutedEventArgs e)
        {//TODO:
            ServerHelper.GetFlightParameters(ServerHelper.GetCurrentAircraftModel());
        }

        /// <summary>
        /// 读取所有判据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnReadDecisions(object sender, RoutedEventArgs e)
        {//TODO:
            var decisions = ServerHelper.GetDecisions(ServerHelper.GetCurrentAircraftModel());
        }

        /// <summary>
        /// 读取所有面板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnChartPanelRead(object sender, RoutedEventArgs e)
        {//TODO:
            var chartPanels = ServerHelper.GetChartPanels(ServerHelper.GetCurrentAircraftModel());

        }

        /// <summary>
        /// 不用在此添加，背后配置先写死
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Obsolete]
        private void OnCurrentAicraftModelChartsAdded(object sender, RoutedEventArgs e)
        {

        }
    }
}
