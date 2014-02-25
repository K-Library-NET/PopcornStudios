using AircraftDataAnalysisWinRT.Services;
using FlightDataEntitiesRT;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftDataAnalysisWinRT.DataModel
{
    public class RawDataPointViewModel : AircraftDataAnalysisWinRT.Common.BindableBase
    {
        public RawDataPointViewModel()
        {
            var parameters = this.GetFlightParameters();
            this.RawDataRowViewModel = new RawDataRowViewModel()
            {
                Columns = new ObservableCollection<FlightDataEntitiesRT.DataColumn>(
                  from one in parameters
                  where !string.IsNullOrEmpty(one.ParameterID) && one.ParameterID != "(NULL)"
                  select new FlightDataEntitiesRT.DataColumn()
                  {
                      Caption = one.Caption,
                      ColumnName = one.ParameterID,
                      DataType = FlightDataEntitiesRT.DataTypeConverter.GetType(one.ParameterDataType)
                  })
            };

            if (this.RawDataRowViewModel.Columns.Count > 0)
            {//DEBUG
            }
        }

        public ObservableCollection<AircraftService.FlightParameter> ParameterList { get; set; }

        //public ObservableCollection<Telerik.UI.Xaml.Controls.Grid.DataGridColumn> ColumnCollection { get; set; }
        public ObservableCollection<Syncfusion.UI.Xaml.Grid.GridColumn> ColumnCollection { get; set; }

        internal void GenerateColumns()
        {
            var result2 = GetFlightParameters();

            this.ColumnCollection = new ObservableCollection<Syncfusion.UI.Xaml.Grid.GridColumn>();
            //new ObservableCollection<Telerik.UI.Xaml.Controls.Grid.DataGridColumn>();

            if (result2 != null && result2.Count() > 0)
            {
                int i = 0;
                foreach (var one in result2)
                {
                    //这里才是去掉NULL值
                    if (one.ParameterID == "(NULL)")
                        continue;
                    Syncfusion.UI.Xaml.Grid.GridTextColumn col
                        = new Syncfusion.UI.Xaml.Grid.GridTextColumn()
                        {
                            MappingName = one.ParameterID,
                            AllowEditing = false,
                            TextAlignment = Windows.UI.Xaml.TextAlignment.Center,
                            HorizontalHeaderContentAlignment = Windows.UI.Xaml.HorizontalAlignment.Center,
                            HeaderText = one.Caption
                        };

                    //Telerik.UI.Xaml.Controls.Grid.DataGridColumn col
                    //    = new Telerik.UI.Xaml.Controls.Grid.DataGridTextColumn()
                    //    {
                    //        Name = one.ParameterID,
                    //        PropertyName = one.ParameterID,//"Values[" + i.ToString() + "]",
                    //        CanUserEdit = false,
                    //        Header = one.Caption
                    //    };

                    this.ColumnCollection.Add(col);
                    i++;
                }
            }

            this.RawDataRowViewModel.Columns.Insert(0, new DataColumn() { Caption = "秒值", ColumnName = "Second", DataType = typeof(int) });

            //this.ColumnCollection.Insert(0,
            //    new Telerik.UI.Xaml.Controls.Grid.DataGridTextColumn()
            //    {
            //        Name = "Second",
            //        PropertyName = "Second",
            //        Header = "秒值"
            //    });
            this.ColumnCollection.Insert(0,
                new Syncfusion.UI.Xaml.Grid.GridTextColumn()
                {
                    HeaderText = "秒值",
                    AllowEditing = false,
                    MappingName = "Second",
                    HorizontalHeaderContentAlignment = Windows.UI.Xaml.HorizontalAlignment.Center,
                    TextAlignment = Windows.UI.Xaml.TextAlignment.Center
                });
        }

        private FlightDataEntitiesRT.FlightParameters m_currentFlightParameters = null;

        public FlightDataEntitiesRT.FlightParameters CurrentFlightParameters
        {
            get { return m_currentFlightParameters; }
            set { m_currentFlightParameters = value; }
        }

        public FlightDataEntitiesRT.FlightParameter[] GetFlightParameters()
        {
            if (m_currentFlightParameters != null)
                return m_currentFlightParameters.Parameters;

            m_currentFlightParameters = ServerHelper.GetFlightParameters(ServerHelper.GetCurrentAircraftModel());

            return m_currentFlightParameters.Parameters;
        }

        public RawDataRowViewModel RawDataRowViewModel { get; set; }
    }

    public class RawDataRowViewModel : FlightDataEntitiesRT.DataTable, IEnumerable<FlightDataEntitiesRT.DataRow>
    {
        public void AddOneSecondValue(int i, FlightDataEntitiesRT.ParameterRawData[] datas)
        {
            DataRow row = this.NewRow();

            row["Second"] = i;

            foreach (var dt in datas)
            {
                row[dt.ParameterID] = Convert.ToSingle(Math.Round(dt.Values[0], 2));//写死用第一个值，并且两位小数保留
            }

            this.Rows.Add(row);

            //RawDataRowViewModel model = new RawDataRowViewModel() { Second = i };
            //model.AddValue(i);

            //foreach (var param in ParameterList)
            //{
            //    var value = datas.Single(
            //         new Func<FlightDataEntitiesRT.ParameterRawData, bool>(
            //             delegate(FlightDataEntitiesRT.ParameterRawData data)
            //             {
            //                 if (data != null && data.ParameterID == param.ParameterID)
            //                     return true;
            //                 return false;
            //             }));

            //    if (value != null)
            //    {
            //        model.AddValue(value.Values[0]);
            //    }
            //    else { model.AddValue(string.Empty); }
            //}

            //this.Items.Add(model);
        }

        public new IEnumerator<DataRow> GetEnumerator()
        {
            return this.Rows.GetEnumerator();
        }
    }
}
