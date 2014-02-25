using AircraftDataAnalysisWinRT.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftDataAnalysisWinRT.DataModel
{
    public class ChartPanelViewModel : BindableBase
    {
        public ChartPanelViewModel()
        {
            var panels = AircraftDataAnalysisWinRT.Services.ServerHelper.GetChartPanels(
                ApplicationContext.Instance.CurrentAircraftModel);
            if (panels != null && panels.Count() > 0)
            {
                var tmp = from one in panels
                          select new ChartPanelWrap(one, this);

                this.m_chartPanelCollections = new ObservableCollection<ChartPanelWrap>(tmp);
            }
        }

        private int m_currentIndex = -1;

        public int CurrentIndex
        {
            get { return m_currentIndex; }
            set
            {
                this.SetProperty<int>(ref m_currentIndex, value);
                base.OnPropertyChanged("CurrentPanel");
            }
        }

        private FlightDataEntitiesRT.Charts.ChartPanel m_currentChartPanel = null;

        public FlightDataEntitiesRT.Charts.ChartPanel CurrentPanel
        {
            get
            {
                return m_currentChartPanel;
            }
            set
            {
                this.SetProperty<FlightDataEntitiesRT.Charts.ChartPanel>(
                    ref m_currentChartPanel, value);
                if (m_currentChartPanel != null)
                {
                    var tmp = from one in m_chartPanelCollections
                              where one.Panel.PanelID == m_currentChartPanel.PanelID
                              select one;
                    if (tmp != null && tmp.Count() > 0)
                    {
                        int index = m_chartPanelCollections.IndexOf(tmp.First());
                        this.CurrentIndex = index;
                    }
                }
                else
                {
                }
            }
        }

        private ObservableCollection<ChartPanelWrap> m_chartPanelCollections = new ObservableCollection<ChartPanelWrap>();

        public ObservableCollection<ChartPanelWrap> ChartPanelCollections
        {
            get
            {
                return m_chartPanelCollections;
            }
        }
    }

    public class ChartPanelWrap : BindableBase
    {
        private FlightDataEntitiesRT.Charts.ChartPanel m_panel;
        private ChartPanelViewModel viewModel;

        public FlightDataEntitiesRT.Charts.ChartPanel Panel
        {
            get { return m_panel; }
            set { this.SetProperty<FlightDataEntitiesRT.Charts.ChartPanel>(ref m_panel, value); }
        }

        public ChartPanelWrap(FlightDataEntitiesRT.Charts.ChartPanel panel, ChartPanelViewModel viewModel)
        {
            this.m_panel = panel;
            this.viewModel = viewModel;
        }

        public bool IsCurrent
        {
            get
            {
                if (viewModel.CurrentIndex >= 0 &&
                    this.viewModel.ChartPanelCollections.Count > viewModel.CurrentIndex)
                {
                    if (viewModel.ChartPanelCollections.IndexOf(this) == viewModel.CurrentIndex)
                        return true;
                }

                return false;
            }
            set
            {
                if (viewModel != null)
                {
                    this.viewModel.CurrentIndex = this.viewModel.ChartPanelCollections.IndexOf(this);
                    foreach (var one in this.viewModel.ChartPanelCollections)
                    {
                        one.OnPropertyChanged("IsCurrent");
                    }
                }
            }
        }
    }
}
