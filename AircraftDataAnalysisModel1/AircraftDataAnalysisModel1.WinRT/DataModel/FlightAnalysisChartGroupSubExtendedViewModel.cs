using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftDataAnalysisModel1.WinRT.DataModel
{
    public class FlightAnalysisChartGroupSubExtendedViewModel : FlightAnalysisChartGroupViewModel
    {
        public FlightAnalysisChartGroupSubExtendedViewModel()
        {
        }

        public FlightAnalysisChartGroupSubExtendedViewModel(FlightAnalysisViewModel flightAnalysisViewModel) :
            base(flightAnalysisViewModel)
        {
        }

        private bool m_isTrackMode = true;

        public bool IsTrackMode
        {
            get
            {
                return m_isTrackMode;
            }
            set
            {
                this.SetProperty<bool>(ref m_isTrackMode, value);
                this.OnPropertyChanged("IsZoomMode");
            }
        }

        public bool IsZoomMode
        {
            get
            {
                return m_isTrackMode == false;
            }
            set
            {
                this.SetProperty<bool>(ref m_isTrackMode, !value);
                this.OnPropertyChanged("IsTrackMode");
            }
        }

        private SerieDefinitionViewModel m_Serie4Definition = null;
        private SerieDefinitionViewModel m_Serie5Definition = null;
        private SerieDefinitionViewModel m_Serie6Definition = null;
        private SerieDefinitionViewModel m_Serie7Definition = null;

        private FlightAnalysisViewModel flightAnalysisViewModel;

        public FlightAnalysisViewModel FlightAnalysisViewModel
        {
            get { return flightAnalysisViewModel; }
            set { flightAnalysisViewModel = value; }
        }

        public SerieDefinitionViewModel Serie4Definition
        {
            get { return m_Serie4Definition; }
            set
            {
                this.SetProperty<SerieDefinitionViewModel>(ref m_Serie4Definition, value);
                this.OnPropertyChanged("Serie4Visibility");
                this.OnPropertyChanged("GroupVisible");
            }
        }

        public SerieDefinitionViewModel Serie5Definition
        {
            get { return m_Serie5Definition; }
            set
            {
                this.SetProperty<SerieDefinitionViewModel>(ref m_Serie5Definition, value);
                this.OnPropertyChanged("Serie5Visibility");
            }
        }

        public SerieDefinitionViewModel Serie6Definition
        {
            get { return m_Serie6Definition; }
            set
            {
                this.SetProperty<SerieDefinitionViewModel>(ref m_Serie6Definition, value);
                this.OnPropertyChanged("Serie6Visibility");
            }
        }

        public SerieDefinitionViewModel Serie7Definition
        {
            get { return m_Serie7Definition; }
            set
            {
                this.SetProperty<SerieDefinitionViewModel>(ref m_Serie7Definition, value);
                this.OnPropertyChanged("Serie7Visibility");
            }
        }

        public override Windows.UI.Xaml.Visibility GroupVisible
        {
            get
            {
                return Windows.UI.Xaml.Visibility.Visible;
            }
        }

        public Windows.UI.Xaml.Visibility Serie4Visibility
        {
            get
            {//at least one exists and visible, then visible
                if (this.Serie4Definition != null && this.Serie4Definition.IsSerieVisible)
                {
                    return Windows.UI.Xaml.Visibility.Visible;
                }

                return Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        public Windows.UI.Xaml.Visibility Serie5Visibility
        {
            get
            {//at least one exists and visible, then visible
                if (this.Serie5Definition != null && this.Serie5Definition.IsSerieVisible)
                {
                    return Windows.UI.Xaml.Visibility.Visible;
                }

                return Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        public Windows.UI.Xaml.Visibility Serie6Visibility
        {
            get
            {//at least one exists and visible, then visible
                if (this.Serie6Definition != null && this.Serie6Definition.IsSerieVisible)
                {
                    return Windows.UI.Xaml.Visibility.Visible;
                }

                return Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        public Windows.UI.Xaml.Visibility Serie7Visibility
        {
            get
            {//at least one exists and visible, then visible
                if (this.Serie7Definition != null && this.Serie7Definition.IsSerieVisible)
                {
                    return Windows.UI.Xaml.Visibility.Visible;
                }

                return Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        internal virtual void RequestGroupVisibleChanged()
        {
            this.OnPropertyChanged("Serie1Visibility");
            this.OnPropertyChanged("Serie2Visibility");
            this.OnPropertyChanged("Serie3Visibility");
            this.OnPropertyChanged("Serie4Visibility");
            this.OnPropertyChanged("Serie5Visibility");
            this.OnPropertyChanged("Serie6Visibility");
            this.OnPropertyChanged("Serie7Visibility");

            if (this.flightAnalysisViewModel != null)
            {
                this.flightAnalysisViewModel.RequestGroupVisibleChanged();
            }
        }
    }
}
