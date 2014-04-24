using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// “用户控件”项模板在 http://go.microsoft.com/fwlink/?LinkId=234236 上提供

namespace AircraftDataAnalysisModel1.WinRT.MyControl
{
    public sealed partial class PieChartWrap : UserControl
    {
        public PieChartWrap()
        {
            this.InitializeComponent();
        }

        public PieChartDataSource DataSource
        {
            get
            {
                object obj = null;
                if (this.Resources.TryGetValue("datacontext", out obj))
                {
                    if (obj != null && obj is PieChartDataSource)
                        return obj as PieChartDataSource;
                }

                return null;
            }
            set
            {
                object obj = null;
                if (this.Resources.TryGetValue("datacontext", out obj))
                {
                    if (obj != null && obj is PieChartDataSource)
                    {
                        var source = (obj as PieChartDataSource);
                        source.Clear();
                        if (value != null && value.Count > 0)
                        {
                            foreach (var v in value)
                            {
                                source.Add(v);
                            }
                        }
                    }
                }
            }
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

    public class PieChartAmount
    {
        public string CategoryName { get; set; }
        public double Amount { get; set; }
    }

    public class PieChartDataSource : ObservableCollection<PieChartAmount>
    {
        public PieChartDataSource()
        {
            //debug
            DebugInit();
        }

        private void DebugInit()
        {
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
