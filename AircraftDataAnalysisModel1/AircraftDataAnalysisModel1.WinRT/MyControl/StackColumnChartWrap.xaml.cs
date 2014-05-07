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
    public sealed partial class StackColumnChartWrap : UserControl
    {
        public StackColumnChartWrap()
        {
            this.InitializeComponent();
        }

        public StackColumnCollection DataSource
        {
            get
            {
                object obj = null;
                if (this.Resources.TryGetValue("datacontext", out obj))
                {
                    if (obj != null && obj is StackColumnCollection)
                        return obj as StackColumnCollection;
                }

                return null;
            }
            set
            {
                object obj = null;
                if (this.Resources.TryGetValue("datacontext", out obj))
                {
                    if (obj != null && obj is StackColumnCollection)
                    {
                        var source = (obj as StackColumnCollection);
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

    public class StackColumnItem
    {
        public double Condition1
        {
            get;
            set;
        }

        public double Condition2
        {
            get;
            set;
        }

        public double Condition3
        {
            get;
            set;
        }

        public double Condition4
        {
            get;
            set;
        }

        public string MonthStr
        {
            get;
            set;
        }
    }

    public class StackColumnCollection : ObservableCollection<StackColumnItem>
    {
        public StackColumnCollection()
        {
            //debug
            //DebugInit();
        }

        private void DebugInit()
        {
            this.Add(new StackColumnItem()
            {
                MonthStr = "201304",
                Condition1 = 480,
                Condition2 = 720,
                Condition3 = 2400,
                Condition4 = 320
            });
            this.Add(new StackColumnItem()
            {
                MonthStr = "201306",
                Condition1 = 180,
                Condition2 = 720,
                Condition3 = 1800,
                Condition4 = 920
            });
            this.Add(new StackColumnItem()
            {
                MonthStr = "201308",
                Condition1 = 620,
                Condition2 = 520,
                Condition3 = 2200,
                Condition4 = 360
            });
            this.Add(new StackColumnItem()
            {
                MonthStr = "201401",
                Condition1 = 680,
                Condition2 = 420,
                Condition3 = 2700,
                Condition4 = 120
            });
        }
    }

}
