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
}
