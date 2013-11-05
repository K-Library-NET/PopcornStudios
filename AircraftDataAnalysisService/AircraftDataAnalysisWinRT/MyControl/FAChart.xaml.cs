using AircraftDataAnalysisWinRT.Domain;
using AircraftDataAnalysisWinRT.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Telerik.UI.Xaml.Controls.Chart;
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
    public sealed partial class FAChart : UserControl
    {
        public FAChart()
        {
            this.InitializeComponent();
        }

        private FlightAnalysisViewModel m_viewModel = null;

        public FlightAnalysisViewModel ViewModel
        {
            get
            {
                return m_viewModel;
            }
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
            this.ReBindChartSeries();
        }

        private void ReBindChartSeries()
        {
            this.ucChart.Series.Clear();
            var data = this.m_viewModel.EntityData;

            if (data == null || data.Count() <= 0)
                return;

            var filtered = from one in data
                           where this.m_viewModel.RelatedParameterSelected(one.Key)
                           select one;
            if (filtered == null || filtered.Count() <= 0)
                return;

            foreach (var serie in filtered)
            {
                LineSeries line = new LineSeries()
                {
                    CategoryBinding = new PropertyNameDataPointBinding("Second"),
                    ItemsSource = ToWrapObjs(serie.Value),
                    LegendTitle = this.GetParameterCaption(serie.Key),
                    IsVisibleInLegend = true,
                    IsSelected = true,
                    CombineMode = Telerik.Charting.ChartSeriesCombineMode.Stack,
                    Name = serie.Key,
                    DisplayName = this.GetParameterCaption(serie.Key),
                    ValueBinding = new PropertyNameDataPointBinding("Value")
                };


                this.ucChart.Series.Add(line);
            }
        }

        private ObservableCollection<ParameterRawDataWrap> ToWrapObjs(
             ObservableCollection<FlightDataEntitiesRT.ParameterRawData> rawdatas)
        {
            var result = from one in rawdatas
                         select new ParameterRawDataWrap(one);

            if (result != null && result.Count() > 0)
                return new ObservableCollection<ParameterRawDataWrap>(result);//result.Take(1000));//DEBUG，为了速度

            return new ObservableCollection<ParameterRawDataWrap>();
        }

        class ParameterRawDataWrap
        {
            private FlightDataEntitiesRT.ParameterRawData rawData;

            public ParameterRawDataWrap(FlightDataEntitiesRT.ParameterRawData obj)
            {
                this.rawData = obj;
            }

            public int Second
            {
                get
                {
                    return rawData.Second;
                }
            }

            public float Value
            {
                get
                {
                    return rawData.Values[0];
                }
            }
        }

        private string GetParameterCaption(string p)
        {
            return ApplicationContext.Instance.GetFlightParameterCaption(p);
            // throw new NotImplementedException();
        }
        private void OnViewModelChanged()
        {
            this.BindChartData();
        }

        private void BindChartData()
        {
            this.ReBindChartSeries();
        }
    }
}
