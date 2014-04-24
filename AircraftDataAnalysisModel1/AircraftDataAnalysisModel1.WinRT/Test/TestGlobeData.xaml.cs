using FlightDataEntitiesRT;
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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace AircraftDataAnalysisWinRT.Test
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class TestGlobeData : Page
    {
        public TestGlobeData()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            double startY = 23;
            double endY = 36;

            double startX = 115;
            double endX = 137;
            ObservableCollection<GlobeData> datas = new ObservableCollection<GlobeData>();
            Random rand = new Random();

            for (int i = 0; i < 10000; i++)
            {
                var rate = rand.NextDouble();
                var x = startX + (rate * (endX - startX));
                var y = startY + (rate * (endY - startY));

                GlobeData dt = new GlobeData() { Longitude = (float)x, Latitude = (float)y };
                datas.Add(dt);
            }
            this.chart1.ItemsSource = datas;
        }
    }

    internal class TestSimpleDataSource : ObservableCollection<AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint>
    {
        public TestSimpleDataSource()
        {
            this.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Second = 1,
                Value = 3
            });
            this.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Second = 2,
                Value = 4
            });
            this.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Second = 3,
                Value = 2
            });
            this.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Second = 4,
                Value = 1
            });
            this.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Second = 5,
                Value = 5
            });
            this.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Second = 6,
                Value = 3
            });
        }
    }

    public class Labelconvertor : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return String.Format("{0} %", value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
        #endregion


    }

    internal class PieChartAmount
    {
        public string CategoryName { get; set; }
        public double Amount { get; set; }
    }

    internal class PieChartDataSource : ObservableCollection<PieChartAmount>
    {
        public PieChartDataSource()
        {
            //debug
            //this.Add(new PieChartAmount() { CategoryName = "飞行状态1", Amount = 20d });
            //this.Add(new PieChartAmount() { CategoryName = "飞行状态2", Amount = 23d });
            //this.Add(new PieChartAmount() { CategoryName = "飞行状态3", Amount = 12d });
            this.Add(new PieChartAmount() { CategoryName = "飞行状态1", Amount = 45.6 });
            this.Add(new PieChartAmount() { CategoryName = "飞行状态4", Amount = 13.7 });
            this.Add(new PieChartAmount() { CategoryName = "飞行状态3", Amount = 27.2 });
            this.Add(new PieChartAmount() { CategoryName = "飞行状态2", Amount = 13.5 });
        }
    }
}
