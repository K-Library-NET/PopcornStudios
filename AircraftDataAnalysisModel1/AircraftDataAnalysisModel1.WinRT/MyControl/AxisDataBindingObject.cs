using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infragistics.Controls.Charts;
using System.Collections.ObjectModel;
using AircraftDataAnalysisWinRT.Domain;

namespace AircraftDataAnalysisWinRT.MyControl
{
    /// <summary>
    /// 几种不同的对象，分别对NH（高压转速）、T6（排气温度）、KG（开关量）进行差别处理 
    /// </summary>
    class AxisDataBindingObject
    {
        public AxisDataBindingObject(FlightAnalysisViewModelOld viewModel)
        {
            this.viewModel = viewModel;
        }

        internal ObservableCollection<string> RelatedParameterIDs = new ObservableCollection<string>();
        protected FlightAnalysisViewModelOld viewModel;

        public virtual string ParameterID
        {
            get;
            set;
        }

        public virtual int Order
        {
            get;
            set;
        }

        internal virtual FAChartSubModel ToFAChartSubModel()
        {
            var subModel = new FAChartSubModel() { ParameterID = this.ParameterID };

            //TODO: TODO: 
            ObservableCollection<FlightDataEntitiesRT.ParameterRawData> rawDatas = this.FindRawDatas();

            if (rawDatas != null)
            {
                foreach (var rawData in rawDatas)
                {
                    try
                    {
                        FAChartItem item = new FAChartItem()
                        {
                            XValue = rawData.Second,
                            YValue = rawData.Values[0]
                        };

                        subModel.Add(item);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                        continue;
                    }
                }
            }

            return subModel;
        }

        protected virtual ObservableCollection<FlightDataEntitiesRT.ParameterRawData> FindRawDatas()
        {
            foreach (var vmdt in this.viewModel.EntityData)
            {
                if (vmdt.Key == this.ParameterID)
                {
                    return vmdt.Value;
                }
            }

            return new ObservableCollection<FlightDataEntitiesRT.ParameterRawData>();
        }

        /// <summary>
        /// 按照最普通的Binding
        /// </summary>
        /// <param name="model"></param>
        /// <param name="chart"></param>
        /// <returns></returns>
        internal virtual XamDataChart AssignSimpleLineChart(FAChartSubModel model, XamDataChart chart)
        {
            CategoryXAxis xAxis = new CategoryXAxis()
            {
                ItemsSource = model,
                LabelSettings = new AxisLabelSettings()
                {
                    FontSize = BindingChartUIFactory.XAxisFontSize,
                    Foreground = BindingChartUIFactory.XAxisForegroundBrush,
                    Location = AxisLabelsLocation.OutsideBottom,
                },
                Label = "{XValue}"
            };

            NumericYAxis yAxis = new NumericYAxis()
            {
                LabelSettings = new AxisLabelSettings()
                {
                    Location = AxisLabelsLocation.OutsideLeft,
                    Foreground = BindingChartUIFactory.YAxisForegroundBrush,
                    Extent = BindingChartUIFactory.YAxisLeftExtend,
                },
            };

            LineSeries serie = new LineSeries()
            {
                Title = ApplicationContext.Instance.GetFlightParameterCaption(model.ParameterID),
                ItemsSource = model,
                XAxis = xAxis,
                YAxis = yAxis,
                ValueMemberPath = "YValue"
            };

            chart.Axes.Add(xAxis);
            chart.Axes.Add(yAxis);
            chart.Series.Add(serie);

            return chart;
        }

        internal void AddRelatedParameterID(string res)
        {
            //TODO: fix add relatedParameterIDs
            RelatedParameterIDs.Add(res);
        }

        //internal ObservableCollection<RelatedParameterViewModel> ViewModels
        //    = new ObservableCollection<RelatedParameterViewModel>();

        //internal void Add(RelatedParameterViewModel res)
        //{
        //    ViewModels.Add(res);
        //}

    }

    class KGAxisDataBindingObject : AxisDataBindingObject
    {
        public KGAxisDataBindingObject(FlightAnalysisViewModelOld viewModel)
            : base(viewModel)
        {
        }

        public override string ParameterID
        {
            get
            {
                return "KG";
            }
            set
            {
                //
            }
        }

        internal override FAChartSubModel ToFAChartSubModel()
        {
            var subModel = new KGFAChartSubModel() { ParameterID = this.ParameterID };

            var pairs = from data in this.viewModel.EntityData
                        where data.Key.StartsWith(this.ParameterID)
                        select data;

            Dictionary<int, KGFAChartItem> secondMap = new Dictionary<int, KGFAChartItem>();
            foreach (var one in pairs)
            {
                this.AssignOneValue(one, subModel, secondMap);
            }

            var sorted = from one in secondMap
                         orderby one.Key ascending
                         select one.Value;
            foreach (var m in sorted)
            {
                subModel.Add(m);
            }
            return subModel;
        }

        private void AssignOneValue(KeyValuePair<string, ObservableCollection<FlightDataEntitiesRT.ParameterRawData>> one,
            KGFAChartSubModel subModel, Dictionary<int, KGFAChartItem> secondMap)
        {
            foreach (var vals in one.Value)
            {
                KGFAChartItem item = null;
                if (secondMap.ContainsKey(vals.Second))
                {
                    item = secondMap[vals.Second];
                }
                else
                {
                    item = new KGFAChartItem();
                    secondMap.Add(vals.Second, item);
                }

                item.XValue = vals.Second;

                if (one.Key == "KG1")
                {
                    item.KG1 = Convert.ToInt32(vals.Values[0]);
                }
                else if (one.Key == "KG2")
                {
                    item.KG2 = Convert.ToInt32(vals.Values[0]);
                }
                else if (one.Key == "KG3")
                {
                    item.KG3 = Convert.ToInt32(vals.Values[0]);
                }
                else if (one.Key == "KG4")
                {
                    item.KG4 = Convert.ToInt32(vals.Values[0]);
                }
                else if (one.Key == "KG5")
                {
                    item.KG5 = Convert.ToInt32(vals.Values[0]);

                }
                else if (one.Key == "KG6")
                {
                    item.KG6 = Convert.ToInt32(vals.Values[0]);

                }
                else if (one.Key == "KG7")
                {
                    item.KG7 = Convert.ToInt32(vals.Values[0]);

                }
                else if (one.Key == "KG8")
                {
                    item.KG8 = Convert.ToInt32(vals.Values[0]);

                }
                else if (one.Key == "KG9")
                {
                    item.KG9 = Convert.ToInt32(vals.Values[0]);

                }
                else if (one.Key == "KG10")
                {
                    item.KG10 = Convert.ToInt32(vals.Values[0]);

                }
                else if (one.Key == "KG11")
                {
                    item.KG11 = Convert.ToInt32(vals.Values[0]);

                }
                else if (one.Key == "KG12")
                {
                    item.KG12 = Convert.ToInt32(vals.Values[0]);

                }
                else if (one.Key == "KG13")
                {
                    item.KG13 = Convert.ToInt32(vals.Values[0]);
                }
                else if (one.Key == "KG14")
                {
                    item.KG14 = Convert.ToInt32(vals.Values[0]);
                }
                else if (one.Key == "KG15")
                {
                    item.KG15 = Convert.ToInt32(vals.Values[0]);
                }
            }

        }

        internal override XamDataChart AssignSimpleLineChart(FAChartSubModel model, XamDataChart chart)
        {
            CategoryXAxis xAxis = new CategoryXAxis()
            {
                ItemsSource = model,
                LabelSettings = new AxisLabelSettings()
                {
                    FontSize = BindingChartUIFactory.XAxisFontSize,
                    Foreground = BindingChartUIFactory.XAxisForegroundBrush,
                    Location = AxisLabelsLocation.OutsideBottom,
                },
                Label = "{XValue}"
            };

            NumericYAxis yAxis = new NumericYAxis()
            {
                LabelSettings = new AxisLabelSettings()
                {
                    Location = AxisLabelsLocation.OutsideLeft,
                    Foreground = BindingChartUIFactory.YAxisForegroundBrush,
                    Extent = BindingChartUIFactory.YAxisLeftExtend,
                },
            };

            chart.Axes.Add(xAxis);
            chart.Axes.Add(yAxis);

            foreach (var rid in this.RelatedParameterIDs)
            {
                LineSeries serie = new LineSeries()
                {
                    ItemsSource = model,
                    XAxis = xAxis,
                    YAxis = yAxis,
                    ValueMemberPath = rid,//"KG" + i.ToString(),
                };
                chart.Series.Add(serie);

            }

            //for (int i = 1; i <= 15; i++)
            //{
            //    LineSeries serie = new LineSeries()
            //    {
            //        ItemsSource = model,
            //        XAxis = xAxis,
            //        YAxis = yAxis,
            //        ValueMemberPath = "KG" + i.ToString(),
            //    };
            //    chart.Series.Add(serie);
            //}

            return chart;
        }
    }

    class NHAxisDataBindingObject : AxisDataBindingObject
    {
        public NHAxisDataBindingObject(FlightAnalysisViewModelOld viewModel)
            : base(viewModel)
        {
        }

        public override string ParameterID
        {
            get
            {
                return "NH";
            }
            set
            {
                //
            }
        }

        internal override FAChartSubModel ToFAChartSubModel()
        {
            var subModel = new NHFAChartSubModel() { ParameterID = this.ParameterID };

            var pairs = from data in this.viewModel.EntityData
                        where data.Key.StartsWith(this.ParameterID)
                        select data;

            Dictionary<int, NHFAChartItem> secondMap = new Dictionary<int, NHFAChartItem>();
            foreach (var one in pairs)
            {
                this.AssignOneValue(one, subModel, secondMap);
            }

            var sorted = from one in secondMap
                         orderby one.Key ascending
                         select one.Value;

            foreach (var m in sorted)
            {
                subModel.Add(m);
            }
            return subModel;
        }

        protected override ObservableCollection<FlightDataEntitiesRT.ParameterRawData> FindRawDatas()
        {
            foreach (var vmdt in this.viewModel.EntityData)
            {
                if (vmdt.Key.StartsWith(this.ParameterID))
                {
                    return vmdt.Value;
                }
            }

            return new ObservableCollection<FlightDataEntitiesRT.ParameterRawData>();
        }

        private void AssignOneValue(KeyValuePair<string, ObservableCollection<FlightDataEntitiesRT.ParameterRawData>> one,
            NHFAChartSubModel subModel, Dictionary<int, NHFAChartItem> secondMap)
        {
            foreach (var vals in one.Value)
            {
                NHFAChartItem item = null;
                if (secondMap.ContainsKey(vals.Second))
                {
                    item = secondMap[vals.Second];
                }
                else
                {
                    item = new NHFAChartItem();
                    secondMap.Add(vals.Second, item);
                }

                item.XValue = vals.Second;

                if (one.Key == "NHL")
                {
                    item.NHL = vals.Values[0];
                }
                else if (one.Key == "NHR")
                {
                    item.NHR = vals.Values[0];
                }

                //subModel.Add(item);
            }
        }

        internal override XamDataChart AssignSimpleLineChart(FAChartSubModel model, XamDataChart chart)
        {
            CategoryXAxis xAxis = new CategoryXAxis()
            {
                ItemsSource = model,
                LabelSettings = new AxisLabelSettings()
                {
                    FontSize = BindingChartUIFactory.XAxisFontSize,
                    Foreground = BindingChartUIFactory.XAxisForegroundBrush,
                    Location = AxisLabelsLocation.OutsideBottom,
                },
                Label = "{XValue}"
            };

            NumericYAxis yAxis = new NumericYAxis()
            {
                LabelSettings = new AxisLabelSettings()
                {
                    Location = AxisLabelsLocation.OutsideLeft,
                    Foreground = BindingChartUIFactory.YAxisForegroundBrush,
                    Extent = BindingChartUIFactory.YAxisLeftExtend,
                },
            };

            chart.Axes.Add(xAxis);
            chart.Axes.Add(yAxis);

            LineSeries serie1 = new LineSeries()
            {
                ItemsSource = model,
                XAxis = xAxis,
                YAxis = yAxis,
                ValueMemberPath = "NHL",
            };
            chart.Series.Add(serie1);
            LineSeries serie2 = new LineSeries()
            {
                ItemsSource = model,
                XAxis = xAxis,
                YAxis = yAxis,
                ValueMemberPath = "NHR",
            };
            chart.Series.Add(serie2);

            return chart;
        }
    }

    class T6AxisDataBindingObject : AxisDataBindingObject
    {
        public T6AxisDataBindingObject(FlightAnalysisViewModelOld viewModel)
            : base(viewModel)
        {
        }

        public override string ParameterID
        {
            get
            {
                return "T6";
            }
            set
            {
                //
            }
        }

        internal override FAChartSubModel ToFAChartSubModel()
        {
            var subModel = new T6FAChartSubModel() { ParameterID = this.ParameterID };

            var pairs = from data in this.viewModel.EntityData
                        where data.Key.StartsWith(this.ParameterID)
                        select data;

            Dictionary<int, T6FAChartItem> secondMap = new Dictionary<int, T6FAChartItem>();
            foreach (var one in pairs)
            {
                this.AssignOneValue(one, subModel, secondMap);
            }

            var sorted = from one in secondMap
                         orderby one.Key ascending
                         select one.Value;

            foreach (var m in sorted)
            {
                subModel.Add(m);
            }
            return subModel;
        }

        protected override ObservableCollection<FlightDataEntitiesRT.ParameterRawData> FindRawDatas()
        {
            foreach (var vmdt in this.viewModel.EntityData)
            {
                if (vmdt.Key.StartsWith(this.ParameterID))
                {
                    return vmdt.Value;
                }
            }

            return new ObservableCollection<FlightDataEntitiesRT.ParameterRawData>();
        }

        private void AssignOneValue(KeyValuePair<string, ObservableCollection<FlightDataEntitiesRT.ParameterRawData>> one,
            T6FAChartSubModel subModel, Dictionary<int, T6FAChartItem> secondMap)
        {
            foreach (var vals in one.Value)
            {
                T6FAChartItem item = null;
                if (secondMap.ContainsKey(vals.Second))
                {
                    item = secondMap[vals.Second];
                }
                else
                {
                    item = new T6FAChartItem();
                    secondMap.Add(vals.Second, item);
                }

                item.XValue = vals.Second;

                if (one.Key == "T6L")
                {
                    item.T6L = vals.Values[0];
                }
                else if (one.Key == "T6R")
                {
                    item.T6R = vals.Values[0];
                }

                //subModel.Add(item);
            }
        }

        internal override XamDataChart AssignSimpleLineChart(FAChartSubModel model, XamDataChart chart)
        {
            CategoryXAxis xAxis = new CategoryXAxis()
            {
                ItemsSource = model,
                LabelSettings = new AxisLabelSettings()
                {
                    FontSize = BindingChartUIFactory.XAxisFontSize,
                    Foreground = BindingChartUIFactory.XAxisForegroundBrush,
                    Location = AxisLabelsLocation.OutsideBottom,
                },
                Label = "{XValue}"
            };

            NumericYAxis yAxis = new NumericYAxis()
            {
                LabelSettings = new AxisLabelSettings()
                {
                    Location = AxisLabelsLocation.OutsideLeft,
                    Foreground = BindingChartUIFactory.YAxisForegroundBrush,
                    Extent = BindingChartUIFactory.YAxisLeftExtend,
                },
            };

            chart.Axes.Add(xAxis);
            chart.Axes.Add(yAxis);

            LineSeries serie1 = new LineSeries()
            {
                ItemsSource = model,
                XAxis = xAxis,
                YAxis = yAxis,
                ValueMemberPath = "T6L",
            };
            chart.Series.Add(serie1);
            LineSeries serie2 = new LineSeries()
            {
                ItemsSource = model,
                XAxis = xAxis,
                YAxis = yAxis,
                ValueMemberPath = "T6R",
            };
            chart.Series.Add(serie2);

            return chart;
        }
    }
}
