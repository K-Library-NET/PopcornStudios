using AircraftDataAnalysisWinRT.DataModel;
using AircraftDataAnalysisWinRT.Domain;
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

// “用户控件”项模板在 http://go.microsoft.com/fwlink/?LinkId=234236 上提供

namespace AircraftDataAnalysisWinRT.MyControl
{
    public sealed partial class FAGrid : UserControl
    {
        public FAGrid()
        {
            this.InitializeComponent();
        }

        private FlightAnalysisViewModel m_viewModel = null;

        public FlightAnalysisViewModel ViewModel
        {
            get { return m_viewModel; }
            set
            {
                if (m_viewModel != null)
                    this.m_viewModel.FilterDataChanged -= m_viewModel_FilterDataChanged;

                m_viewModel = value;
                this.m_viewModel.FilterDataChanged += m_viewModel_FilterDataChanged;

                this.OnViewModelChanged();
            }
        }

        void m_viewModel_FilterDataChanged(object sender, EventArgs e)
        {
            this.ReBindColumns();
        }

        private void OnViewModelChanged()
        {
            this.BindGridData();
        }

        public void BindGridData()
        {
            ReBindColumns();

            this.grdData.ItemsSource = this.ViewModel.GridData;
        }

        private void ReBindColumns()
        {
            var result2 = GetFlightParameters();

            this.grdData.Columns.Clear();

            if (result2 != null && result2.Count() > 0)
            {
                int i = 0;
                foreach (var one in result2)
                {
                    Telerik.UI.Xaml.Controls.Grid.DataGridColumn col
                        = new Telerik.UI.Xaml.Controls.Grid.DataGridTextColumn()
                        {
                            Name = one.ParameterID,
                            PropertyName = one.ParameterID,
                            CanUserEdit = false,
                            Header = one.Caption
                        };

                    this.grdData.Columns.Add(col);
                    i++;
                }
            }

            this.grdData.Columns.Insert(0,
                new Telerik.UI.Xaml.Controls.Grid.DataGridTextColumn()
                {
                    Name = "Second",
                    PropertyName = "Second",
                    Header = "秒值"
                });
        }

        private FlightDataEntitiesRT.FlightParameter[] GetFlightParameters()
        {
            var flightParameters = ApplicationContext.Instance.GetFlightParameters(
                ApplicationContext.Instance.CurrentAircraftModel);

            var result = from one in flightParameters.Parameters
                         where one.ParameterID != "(NULL)" && this.ExistsParameter(one.ParameterID)
                         && ViewModel.RelatedParameterSelected(one.ParameterID)
                         select one;

            return result.ToArray();
        }

        private bool ExistsParameter(string p)
        {
            if (this.ViewModel.GridData == null || this.ViewModel.GridData.Columns == null)
                return false;
            foreach (var dc in this.ViewModel.GridData.Columns)
            {
                if (dc.ColumnName == p)
                    return true;
            }
            return false;
        }
    }
}
