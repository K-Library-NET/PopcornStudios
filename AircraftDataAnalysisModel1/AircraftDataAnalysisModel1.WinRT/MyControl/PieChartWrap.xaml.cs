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
            if (value != null && value is double)
            {
                return String.Format("{0} %", ((double)value).ToString("F2"));
            }

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
        private string m_catName = string.Empty;
        public string CategoryName
        {
            get { return m_catName; }
            set { m_catName = value; }
        }

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
            //this.Add(new PieChartAmount() { CategoryName = "停车通电状态", Amount = 0.2 });
            //this.Add(new PieChartAmount() { CategoryName = "发动机地面开车状态", Amount = 8.2 });
            //this.Add(new PieChartAmount() { CategoryName = "正常飞行状态", Amount = 88.7 });
            //this.Add(new PieChartAmount() { CategoryName = "最大军用转速状态", Amount = 2.8 });
        }
    }

    internal class PieChartAmountCategoryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is PieChartAmount)
            {
                return (value as PieChartAmount).CategoryName;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
