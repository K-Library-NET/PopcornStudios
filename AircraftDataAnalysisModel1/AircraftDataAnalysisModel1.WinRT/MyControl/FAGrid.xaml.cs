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

        private Type m_prevModelType = null;

        public FlightAnalysisViewModel ViewModel
        {
            get { return m_viewModel; }
            set
            {
                //if (m_viewModel != null)
                //    this.m_viewModel.FilterDataChanged -= m_viewModel_FilterDataChanged;

                m_viewModel = value;

                //this.m_viewModel.FilterDataChanged += m_viewModel_FilterDataChanged;

                if (m_prevModelType == null || (m_viewModel != null && m_prevModelType != m_viewModel.GetType()))
                {
                    this.ReBindColumns();
                }
                this.OnViewModelChanged();
            }
        }

        [Obsolete("在这有用吗？")]
        void m_viewModel_FilterDataChanged(object sender, EventArgs e)
        {
            return;
            //this.ReBindColumns();
        }

        private void OnViewModelChanged()
        {
            this.BindGridData();
        }

        public void BindGridData()
        {
            //ReBindColumns();
            //不能运行时加列，需要列全部绑定好，通过Visibility控制？
            this.grdData.ItemsSource = this.ViewModel.RawDatas;
        }

        private void ReBindColumns()
        {
            var result2 = GetFlightParameters();
            var related = from o1 in this.m_viewModel.RelatedParameterCollection
                          select o1.Parameter.ParameterID;
            this.grdData.Columns.Clear();

            if (result2 != null && result2.Count() > 0)
            {
                int i = 0;
                foreach (var one in result2)
                {
                    Syncfusion.UI.Xaml.Grid.GridTextColumn col
                        = new Syncfusion.UI.Xaml.Grid.GridTextColumn()
                        {
                            MappingName = one.ParameterID,
                            HeaderText = one.Caption,
                            AllowSorting = false,
                            MinimumWidth = 80,
                            ColumnSizer = Syncfusion.UI.Xaml.Grid.GridLengthUnitType.Auto,
                            TextAlignment = Windows.UI.Xaml.TextAlignment.Center,
                            HorizontalHeaderContentAlignment = Windows.UI.Xaml.HorizontalAlignment.Center
                        };
                    col.IsHidden = related.Contains(one.ParameterID) ? false : true;
                    this.grdData.Columns.Add(col);
                    i++;
                }
            }

            this.grdData.Columns.Insert(0, new Syncfusion.UI.Xaml.Grid.GridTextColumn()
                        {
                            MappingName = "Second",
                            HeaderText = "秒值",
                            TextAlignment = Windows.UI.Xaml.TextAlignment.Center,
                            HorizontalHeaderContentAlignment = Windows.UI.Xaml.HorizontalAlignment.Center
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
            if (this.ViewModel.AllParameterIDs.Contains(p))
                return true;
            return false;
            //foreach (var dc in this.ViewModel.AllParameterIDs)
            //{
            //    if (dc == p)
            //        return true;
            //}
            //return false;
        }
    }

    class GridLoadingDataCollection
    {

    }
}
