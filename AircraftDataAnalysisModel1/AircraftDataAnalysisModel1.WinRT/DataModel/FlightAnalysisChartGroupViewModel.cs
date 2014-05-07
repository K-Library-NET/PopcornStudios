using AircraftDataAnalysisWinRT.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftDataAnalysisModel1.WinRT.DataModel
{
    /// <summary>
    /// 2014年新修改的Chart：按照Serie分组的方式，每个组目前来看最多三个Serie，
    /// 每个Serie独立设置颜色、独立设置Parameter相关属性和值
    /// </summary>
    public class FlightAnalysisChartGroupViewModel : BindableBase
    {
        public FlightAnalysisChartGroupViewModel()
        {
        }

        public FlightAnalysisChartGroupViewModel(IGroupVisibleObserver groupVisibleObserver)
        {
            // TODO: Complete member initialization
            this.m_groupVisibleObserver = groupVisibleObserver;
        }

        private FlightAnalysisChartSerieViewModel m_dataSerie = null;

        public FlightAnalysisChartSerieViewModel DataSerie
        {
            get { return m_dataSerie; }
            set { this.SetProperty<FlightAnalysisChartSerieViewModel>(ref m_dataSerie, value); }
        }

        private SerieDefinitionViewModel m_Serie1Definition = null;
        private SerieDefinitionViewModel m_Serie2Definition = null;
        private SerieDefinitionViewModel m_Serie3Definition = null;

        private IGroupVisibleObserver m_groupVisibleObserver;

        public IGroupVisibleObserver GroupVisibleObserver
        {
            get { return m_groupVisibleObserver; }
            set { m_groupVisibleObserver = value; }
        }

        public SerieDefinitionViewModel Serie1Definition
        {
            get { return m_Serie1Definition; }
            set
            {
                this.SetProperty<SerieDefinitionViewModel>(ref m_Serie1Definition, value);
                this.OnPropertyChanged("Serie1Visibility");
                this.OnPropertyChanged("GroupVisible");
            }
        }

        public SerieDefinitionViewModel Serie2Definition
        {
            get { return m_Serie2Definition; }
            set
            {
                this.SetProperty<SerieDefinitionViewModel>(ref m_Serie2Definition, value);
                this.OnPropertyChanged("Serie2Visibility");
                this.OnPropertyChanged("GroupVisible");
            }
        }

        public SerieDefinitionViewModel Serie3Definition
        {
            get { return m_Serie3Definition; }
            set
            {
                this.SetProperty<SerieDefinitionViewModel>(ref m_Serie3Definition, value);
                this.OnPropertyChanged("Serie3Visibility");
                this.OnPropertyChanged("GroupVisible");
            }
        }

        public virtual Windows.UI.Xaml.Visibility GroupVisible
        {
            get
            {//at least one exists and visible, then visible
                if (this.Serie1Visibility == Windows.UI.Xaml.Visibility.Visible
                    || this.Serie2Visibility == Windows.UI.Xaml.Visibility.Visible
                    || this.Serie3Visibility == Windows.UI.Xaml.Visibility.Visible)
                {
                    return Windows.UI.Xaml.Visibility.Visible;
                }

                return Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        public Windows.UI.Xaml.Visibility Serie1Visibility
        {
            get
            {//at least one exists and visible, then visible
                if (this.Serie1Definition != null && this.Serie1Definition.IsSerieVisible)
                {
                    return Windows.UI.Xaml.Visibility.Visible;
                }

                return Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        public Windows.UI.Xaml.Visibility Serie2Visibility
        {
            get
            {//at least one exists and visible, then visible
                if (this.Serie2Definition != null && this.Serie2Definition.IsSerieVisible)
                {
                    return Windows.UI.Xaml.Visibility.Visible;
                }

                return Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        public Windows.UI.Xaml.Visibility Serie3Visibility
        {
            get
            {//at least one exists and visible, then visible
                if (this.Serie3Definition != null && this.Serie3Definition.IsSerieVisible)
                {
                    return Windows.UI.Xaml.Visibility.Visible;
                }

                return Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        //private FlightAnalysisChartSerieViewModel m_Serie1 = null;
        //private FlightAnalysisChartSerieViewModel m_Serie2 = null;
        //private FlightAnalysisChartSerieViewModel m_Serie3 = null;

        //public FlightAnalysisChartSerieViewModel Serie1
        //{
        //    get { return m_Serie1; }
        //    set { this.SetProperty<FlightAnalysisChartSerieViewModel>(ref m_Serie1, value); }
        //}

        //public FlightAnalysisChartSerieViewModel Serie2
        //{
        //    get { return m_Serie2; }
        //    set { this.SetProperty<FlightAnalysisChartSerieViewModel>(ref m_Serie2, value); }
        //}

        //public FlightAnalysisChartSerieViewModel Serie3
        //{
        //    get { return m_Serie3; }
        //    set { this.SetProperty<FlightAnalysisChartSerieViewModel>(ref m_Serie3, value); }
        //}

        internal virtual void RequestGroupVisibleChanged()
        {
            this.OnPropertyChanged("Serie1Visibility");
            this.OnPropertyChanged("Serie2Visibility");
            this.OnPropertyChanged("Serie3Visibility");
            this.OnPropertyChanged("GroupVisible");
            if (this.m_groupVisibleObserver != null)
            {
                this.m_groupVisibleObserver.RequestGroupVisibleChanged();
            }
        }
    }

    public class SerieDefinitionViewModel : BindableBase
    {
        public SerieDefinitionViewModel()
        {
        }

        public SerieDefinitionViewModel(FlightAnalysisChartGroupViewModel groupViewModel)
        {
            // TODO: Complete member initialization
            this.groupViewModel = groupViewModel;
        }

        private string m_parameterID = string.Empty;

        public string ParameterID
        {
            get { return m_parameterID; }
            set
            {
                m_parameterID = value;
                this.ParameterCaption = AircraftDataAnalysisWinRT.ApplicationContext.Instance.GetFlightParameterCaption(m_parameterID);
                this.ForegroundBrush = //new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red);
                    FlightAnalysisChartGroupFactory.LoadOneBrushPlus();
                this.NotifyPropertyChanged("ParameterID");
            }
        }

        private string m_parameterCaption = string.Empty;

        public string ParameterCaption
        {
            get { return m_parameterCaption; }
            protected set
            {
                m_parameterCaption = value;
                this.NotifyPropertyChanged("ParameterCaption");
            }
        }

        private Windows.UI.Xaml.Media.Brush m_foregroundBrush = null;

        public Windows.UI.Xaml.Media.Brush ForegroundBrush
        {
            get { return m_foregroundBrush; }
            protected set
            {
                m_foregroundBrush = value;
                this.NotifyPropertyChanged("ForegroundBrush");
            }
        }

        private string m_xvalueDisplayStr = string.Empty;

        public string XValueDisplayStr
        {
            get { return m_xvalueDisplayStr; }
            set
            {
                m_xvalueDisplayStr = value;
                this.NotifyPropertyChanged("XValueDisplayStr");
            }
        }

        private string m_yvalueDisplayStr = string.Empty;

        public string YValueDisplayStr
        {
            get { return m_yvalueDisplayStr; }
            set
            {
                m_yvalueDisplayStr = value;
                this.NotifyPropertyChanged("YValueDisplayStr");
            }
        }

        private void NotifyPropertyChanged(string property)
        {
            if (property == null)
                property = string.Empty;
            this.OnPropertyChanged(property);
        }

        internal void SetCurrentDisplayPoint(int second, double value)
        {
            this.XValueDisplayStr = string.Format("X: {0}    ", second);
            this.YValueDisplayStr = string.Format("{0}", Math.Round(value, 2));
            //string.Format("Y: {0}    ", Math.Round(value, 2));
        }

        private bool m_isSerieVisible = true;
        private FlightAnalysisChartGroupViewModel groupViewModel;

        public bool IsSerieVisible
        {
            get
            {
                return this.m_isSerieVisible;
            }
            set
            {
                this.SetProperty<bool>(ref m_isSerieVisible, value);

                if (this.groupViewModel != null)
                {
                    this.groupViewModel.RequestGroupVisibleChanged();
                }
            }
        }
    }

    public class FlightAnalysisChartSerieViewModel
        : ObservableCollection<AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint>
    {
        public FlightAnalysisChartSerieViewModel()
            : base()
        {
        }

        public FlightAnalysisChartSerieViewModel(IEnumerable<MyControl.SimpleDataPoint> sorted) :
            base(sorted)
        {
        }
    }
}
