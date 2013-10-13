using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace AircraftDataAnalysisWinRT.DataModel
{
    public class RawDataPointViewModel : AircraftDataAnalysisWinRT.Common.BindableBase
    {
        public RawDataPointViewModel()
        {
            AircraftService.AircraftServiceClient client = new AircraftService.AircraftServiceClient();
            var result = client.GetAllFlightParametersAsync();
            result.Wait();

            this.ParameterList = result.Result;
            this.Items = new ObservableCollection<RawDataRowViewModel>();
        }

        public ObservableCollection<AircraftService.FlightParameter> ParameterList { get; set; }

        public ObservableCollection<Telerik.UI.Xaml.Controls.Grid.DataGridColumn> ColumnCollection { get; set; }

        public ObservableCollection<RawDataRowViewModel> Items { get; set; }

        /// <summary>
        /// 创建RawDataRowViewModel作为一行
        /// </summary>
        /// <param name="i"></param>
        /// <param name="datas"></param>
        public void AddOneSecondValue(int i, FlightDataEntitiesRT.ParameterRawData[] datas)
        {
            RawDataRowViewModel model = new RawDataRowViewModel() { Second = i };
            model.AddValue(i);

            foreach (var param in ParameterList)
            {
                var value = datas.Single(
                     new Func<FlightDataEntitiesRT.ParameterRawData, bool>(
                         delegate(FlightDataEntitiesRT.ParameterRawData data)
                         {
                             if (data != null && data.ParameterID == param.ParameterID)
                                 return true;
                             return false;
                         }));

                if (value != null)
                {
                    model.AddValue(value.Values[0]);
                }
                else { model.AddValue(string.Empty); }
            }

            this.Items.Add(model);
        }

        internal async void GenerateColumns()
        {
            AircraftService.AircraftServiceClient client = new AircraftService.AircraftServiceClient();
            var result = client.GetAllFlightParametersAsync();
            var result2 = await result;

            this.ColumnCollection = new ObservableCollection<Telerik.UI.Xaml.Controls.Grid.DataGridColumn>();

            if (result2 != null && result2.Count > 0)
            {
                int i = 0;
                foreach (var one in result2)
                {
                    Telerik.UI.Xaml.Controls.Grid.DataGridColumn col
                        = new Telerik.UI.Xaml.Controls.Grid.DataGridTextColumn()
                        {
                            Name = one.ParameterID,
                            PropertyName = "Values[" + i.ToString() + "]",
                            Header = one.Caption
                        };
                    
                    this.ColumnCollection.Add(col);
                    i++;
                }
            }

            this.ColumnCollection.Insert(0,
                new Telerik.UI.Xaml.Controls.Grid.DataGridTextColumn()
                {
                    PropertyName = "Second",
                    Header = "秒值"
                });
        }
    }

    public class RawDataRowViewModel : AircraftDataAnalysisWinRT.Common.BindableBase
    {
        private int m_second = 0;

        public int Second
        {
            get { return m_second; }
            set { this.SetProperty<int>(ref m_second, value); }
        }

        private List<object> m_values = new List<object>();

        public List<object> Values
        {
            get { return m_values; }
            set { m_values = value; }
        }

        internal void AddValue(object val)
        {
            m_values.Add(val);
        }
    }
}
