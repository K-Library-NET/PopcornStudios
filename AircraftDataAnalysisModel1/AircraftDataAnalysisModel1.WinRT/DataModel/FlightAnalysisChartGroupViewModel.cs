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
        private FlightAnalysisChartSerieViewModel m_Serie1 = null;
        private FlightAnalysisChartSerieViewModel m_Serie2 = null;
        private FlightAnalysisChartSerieViewModel m_Serie3 = null;

        public FlightAnalysisChartSerieViewModel Serie1
        {
            get { return m_Serie1; }
            set { this.SetProperty<FlightAnalysisChartSerieViewModel>(ref m_Serie1, value); }
        }

        public FlightAnalysisChartSerieViewModel Serie2
        {
            get { return m_Serie2; }
            set { this.SetProperty<FlightAnalysisChartSerieViewModel>(ref m_Serie2, value); }
        }

        public FlightAnalysisChartSerieViewModel Serie3
        {
            get { return m_Serie3; }
            set { this.SetProperty<FlightAnalysisChartSerieViewModel>(ref m_Serie3, value); }
        }
    }

    public class FlightAnalysisChartSerieViewModel
        : ObservableCollection<AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint>
    {
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
            this.OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(property));
        }
    }
}
