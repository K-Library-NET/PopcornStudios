using AircraftDataAnalysisWinRT.Domain;
using Infragistics.Controls.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftDataAnalysisWinRT.MyControl
{
    /// <summary>
    /// 创建“能用的”chart控件，没加自定义功能
    /// </summary>
    class BindingChartUIFactory
    {
        public static int YAxisLeftExtend = 65;

        public static int YAxisFontSize = 18;
        public static int XAxisFontSize = 18;

        private static Windows.UI.Xaml.Media.SolidColorBrush m_foregroundBrush = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Black);

        public static Windows.UI.Xaml.Media.Brush XAxisForegroundBrush { get { return m_foregroundBrush; } }

        public static Windows.UI.Xaml.Media.Brush YAxisForegroundBrush { get { return m_foregroundBrush; } }

        public static double MultiItemsRowHeight
        {
            get
            {
                return 240.0;
            }
        }



        internal static IEnumerable<XamDataChart> BuildChartElements(FlightAnalysisViewModel viewModel,
            IEnumerable<string> parameterIDs, ref FAChartModel faChartModel)
        {
            List<XamDataChart> charts = new List<XamDataChart>();
            IEnumerable<AxisDataBindingObject> bindingObjs = CalculateBindingObjects(viewModel, parameterIDs);

            foreach (var bd in bindingObjs)
            {
                var chartCtrl = BindingChartUIFactory.CreateOneChart(bd, ref faChartModel);
                if (chartCtrl != null)
                {
                    if (chartCtrl != null)
                        charts.Add(chartCtrl);

                    //TODO: custom chart event handle:

                }
            }

            return charts;
        }

        private static IEnumerable<AxisDataBindingObject> CalculateBindingObjects(FlightAnalysisViewModel viewModel,
            IEnumerable<string> parameterIds)
        {
            if (parameterIds != null)
            {
                //var result1 = from one in parameterIds
                //              where one.IsChecked
                //              select one;

                //KG(开关量)分一组
                //T6L、T6R分一组
                //NHL、NHR分一组

                List<AxisDataBindingObject> objs = new List<AxisDataBindingObject>();

                Dictionary<string, AxisDataBindingObject> objMap
                    = new Dictionary<string, AxisDataBindingObject>();

                int item = 0;
                foreach (var res in parameterIds)
                {
                    string key = res;
                    if (res.StartsWith("KG"))
                    {
                        key = "KG";
                    }
                    else if (res.StartsWith("T6"))
                    {
                        key = "T6";
                    }
                    else if (res.StartsWith("NH"))
                    {
                        key = "NH";
                    }
                    if (objMap.ContainsKey(key))
                    {
                        objMap[key].AddRelatedParameterID(res);
                        continue;
                    }
                    else
                    {
                        AxisDataBindingObject obj = new AxisDataBindingObject(viewModel);
                        if (key == "KG")
                        {
                            obj = new KGAxisDataBindingObject(viewModel);
                        }
                        else if (key == "T6")
                        {
                            obj = new T6AxisDataBindingObject(viewModel);
                        }
                        else if (key == "NH")
                        {
                            obj = new NHAxisDataBindingObject(viewModel);
                        }
                        obj.ParameterID = key;
                        obj.Order = item;
                        item++;
                        obj.AddRelatedParameterID(res);

                        objMap.Add(key, obj);
                        objs.Add(obj);
                    }
                }

                var result2 = from ob in objs
                              orderby ob.Order ascending
                              select ob;

                return result2;
            }

            return new AxisDataBindingObject[] { };
        }

        public static XamDataChart CreateOneChart(
            AxisDataBindingObject bindingObj, ref FAChartModel faChartModel)
        {
            FAChartSubModel model = bindingObj.ToFAChartSubModel();

            if (faChartModel.SubModels.ContainsKey(bindingObj.ParameterID))
            {
                faChartModel.SubModels[bindingObj.ParameterID] = model;
            }
            else
            {
                faChartModel.SubModels.Add(bindingObj.ParameterID, model);
            }

            Infragistics.Controls.Charts.Legend legend = new Legend()
            {
                Margin = new Windows.UI.Xaml.Thickness(10),
                Opacity = 1,
                VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top,
                HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Right
            };
            Infragistics.Controls.XamDock.SetEdge(legend, Infragistics.Controls.DockEdge.InsideRight);
            XamDataChart chart = new XamDataChart() { Margin = new Windows.UI.Xaml.Thickness(0, 25, 0, 25) };

            chart.Legend = legend;
            chart.DataContext = model;

            return bindingObj.AssignSimpleLineChart(model, chart);
        }
    }
}
