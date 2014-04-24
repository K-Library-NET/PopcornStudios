using AircraftDataAnalysisWinRT.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftDataAnalysisWinRT.DataModel
{
    public class FlightAnalysisSubSwitchChartViewModel : BindableBase
    {
        public FlightAnalysisSubSwitchChartViewModel()
        {
        }

        public ISubEditChartNavigationParameterValidator Validator
        {
            get;
            set;
        }

        private void InvokeValidate()
        {
            if (Validator != null)
                this.Validator.InvokeValidation();
        }

        private string m_errorMessage = string.Empty;

        public string ErrorMessage
        {
            get
            {
                return m_errorMessage;
            }
            set
            {
                this.SetProperty<string>(ref m_errorMessage, value);
            }
        }

        #region repeat

        private string m_Serie1ParameterID = string.Empty;

        public string Serie1ParameterID
        {
            get
            {
                return m_Serie1ParameterID;
            }
            set
            {
                this.SetProperty<string>(ref m_Serie1ParameterID, value);
                this.InvokeValidate();
            }
        }

        private string m_Serie2ParameterID = string.Empty;

        public string Serie2ParameterID
        {
            get
            {
                return m_Serie2ParameterID;
            }
            set
            {
                this.SetProperty<string>(ref m_Serie2ParameterID, value);
                this.InvokeValidate();
            }
        }

        private string m_Serie3ParameterID = string.Empty;

        public string Serie3ParameterID
        {
            get
            {
                return m_Serie3ParameterID;
            }
            set
            {
                this.SetProperty<string>(ref m_Serie3ParameterID, value);
                this.InvokeValidate();
            }
        }

        private string m_Serie4ParameterID = string.Empty;

        public string Serie4ParameterID
        {
            get
            {
                return m_Serie4ParameterID;
            }
            set
            {
                this.SetProperty<string>(ref m_Serie4ParameterID, value);
                this.InvokeValidate();
            }
        }

        private string m_Serie5ParameterID = string.Empty;

        public string Serie5ParameterID
        {
            get
            {
                return m_Serie5ParameterID;
            }
            set
            {
                this.SetProperty<string>(ref m_Serie5ParameterID, value);
                this.InvokeValidate();
            }
        }

        private string m_Serie6ParameterID = string.Empty;

        public string Serie6ParameterID
        {
            get
            {
                return m_Serie6ParameterID;
            }
            set
            {
                this.SetProperty<string>(ref m_Serie6ParameterID, value);
                this.InvokeValidate();
            }
        }

        private string m_Serie7ParameterID = string.Empty;

        public string Serie7ParameterID
        {
            get
            {
                return m_Serie7ParameterID;
            }
            set
            {
                this.SetProperty<string>(ref m_Serie7ParameterID, value);
                this.InvokeValidate();
            }
        }

        private bool m_isSerie1Editable = false;

        public bool IsSerie1Editable
        {
            get
            {
                return m_isSerie1Editable;
            }
            set
            {
                this.SetProperty<bool>(ref m_isSerie1Editable, value);
                this.InvokeValidate();
            }
        }

        private bool m_isSerie2Editable = false;

        public bool IsSerie2Editable
        {
            get
            {
                return m_isSerie2Editable;
            }
            set
            {
                this.SetProperty<bool>(ref m_isSerie2Editable, value);
                this.InvokeValidate();
            }
        }

        private bool m_isSerie3Editable = false;

        public bool IsSerie3Editable
        {
            get
            {
                return m_isSerie3Editable;
            }
            set
            {
                this.SetProperty<bool>(ref m_isSerie3Editable, value);
                this.InvokeValidate();
            }
        }

        private bool m_isSerie4Editable = false;

        public bool IsSerie4Editable
        {
            get
            {
                return m_isSerie4Editable;
            }
            set
            {
                this.SetProperty<bool>(ref m_isSerie4Editable, value);
                this.InvokeValidate();
            }
        }

        private bool m_isSerie5Editable = false;

        public bool IsSerie5Editable
        {
            get
            {
                return m_isSerie5Editable;
            }
            set
            {
                this.SetProperty<bool>(ref m_isSerie5Editable, value);
                this.InvokeValidate();
            }
        }

        private bool m_isSerie6Editable = false;

        public bool IsSerie6Editable
        {
            get
            {
                return m_isSerie6Editable;
            }
            set
            {
                this.SetProperty<bool>(ref m_isSerie6Editable, value);
                this.InvokeValidate();
            }
        }

        private bool m_isSerie7Editable = false;

        public bool IsSerie7Editable
        {
            get
            {
                return m_isSerie7Editable;
            }
            set
            {
                this.SetProperty<bool>(ref m_isSerie7Editable, value);
                this.InvokeValidate();
            }
        }

        private FlightAnalysisSubViewYAxis m_serie1YAxis = FlightAnalysisSubViewYAxis.LeftYAxis;

        public FlightAnalysisSubViewYAxis Serie1YAxis
        {
            get
            {
                return m_serie1YAxis;
            }
            set
            {
                this.SetProperty<FlightAnalysisSubViewYAxis>(ref m_serie1YAxis, value);
                this.InvokeValidate();
            }
        }

        private FlightAnalysisSubViewYAxis m_serie2YAxis = FlightAnalysisSubViewYAxis.LeftYAxis;

        public FlightAnalysisSubViewYAxis Serie2YAxis
        {
            get
            {
                return m_serie2YAxis;
            }
            set
            {
                this.SetProperty<FlightAnalysisSubViewYAxis>(ref m_serie2YAxis, value);
                this.InvokeValidate();
            }
        }

        private FlightAnalysisSubViewYAxis m_serie3YAxis = FlightAnalysisSubViewYAxis.LeftYAxis;

        public FlightAnalysisSubViewYAxis Serie3YAxis
        {
            get
            {
                return m_serie3YAxis;
            }
            set
            {
                this.SetProperty<FlightAnalysisSubViewYAxis>(ref m_serie3YAxis, value);
                this.InvokeValidate();
            }
        }

        private FlightAnalysisSubViewYAxis m_serie4YAxis = FlightAnalysisSubViewYAxis.LeftYAxis;

        public FlightAnalysisSubViewYAxis Serie4YAxis
        {
            get
            {
                return m_serie4YAxis;
            }
            set
            {
                this.SetProperty<FlightAnalysisSubViewYAxis>(ref m_serie4YAxis, value);
                this.InvokeValidate();
            }
        }

        private FlightAnalysisSubViewYAxis m_serie5YAxis = FlightAnalysisSubViewYAxis.LeftYAxis;

        public FlightAnalysisSubViewYAxis Serie5YAxis
        {
            get
            {
                return m_serie5YAxis;
            }
            set
            {
                this.SetProperty<FlightAnalysisSubViewYAxis>(ref m_serie5YAxis, value);
                this.InvokeValidate();
            }
        }

        private FlightAnalysisSubViewYAxis m_serie6YAxis = FlightAnalysisSubViewYAxis.LeftYAxis;

        public FlightAnalysisSubViewYAxis Serie6YAxis
        {
            get
            {
                return m_serie6YAxis;
            }
            set
            {
                this.SetProperty<FlightAnalysisSubViewYAxis>(ref m_serie6YAxis, value);
                this.InvokeValidate();
            }
        }

        private FlightAnalysisSubViewYAxis m_serie7YAxis = FlightAnalysisSubViewYAxis.LeftYAxis;

        public FlightAnalysisSubViewYAxis Serie7YAxis
        {
            get
            {
                return m_serie7YAxis;
            }
            set
            {
                this.SetProperty<FlightAnalysisSubViewYAxis>(ref m_serie7YAxis, value);
                this.InvokeValidate();
            }
        }

        #endregion repeat

        #region old
        //public FlightAnalysisSubSwitchChartViewModel()
        //{
        //    var panels = AircraftDataAnalysisWinRT.Services.ServerHelper.GetChartPanels(
        //        ApplicationContext.Instance.CurrentAircraftModel);
        //    if (panels != null && panels.Count() > 0)
        //    {
        //        var tmp = from one in panels
        //                  select new ChartPanelWrap(one, this);

        //        this.m_chartPanelCollections = new ObservableCollection<ChartPanelWrap>(tmp);
        //    }
        //}

        //private int m_currentIndex = -1;

        //public int CurrentIndex
        //{
        //    get { return m_currentIndex; }
        //    set
        //    {
        //        this.SetProperty<int>(ref m_currentIndex, value);
        //        base.OnPropertyChanged("CurrentPanel");
        //    }
        //}

        //private FlightDataEntitiesRT.Charts.ChartPanel m_currentChartPanel = null;

        //public FlightDataEntitiesRT.Charts.ChartPanel CurrentPanel
        //{
        //    get
        //    {
        //        return m_currentChartPanel;
        //    }
        //    set
        //    {
        //        this.SetProperty<FlightDataEntitiesRT.Charts.ChartPanel>(
        //            ref m_currentChartPanel, value);
        //        if (m_currentChartPanel != null)
        //        {
        //            var tmp = from one in m_chartPanelCollections
        //                      where one.Panel.PanelID == m_currentChartPanel.PanelID
        //                      select one;
        //            if (tmp != null && tmp.Count() > 0)
        //            {
        //                int index = m_chartPanelCollections.IndexOf(tmp.First());
        //                this.CurrentIndex = index;
        //            }
        //        }
        //        else
        //        {
        //        }
        //    }
        //}

        //private ObservableCollection<ChartPanelWrap> m_chartPanelCollections = new ObservableCollection<ChartPanelWrap>();

        //public ObservableCollection<ChartPanelWrap> ChartPanelCollections
        //{
        //    get
        //    {
        //        return m_chartPanelCollections;
        //    }
        //}
        #endregion
    }

    public interface ISubEditChartNavigationParameterValidator
    {
        bool InvokeValidation();
    }

    //[Obsolete]
    //public class ChartPanelWrap : BindableBase
    //{
    //    private FlightDataEntitiesRT.Charts.ChartPanel m_panel;
    //    private ChartPanelViewModel viewModel;

    //    public FlightDataEntitiesRT.Charts.ChartPanel Panel
    //    {
    //        get { return m_panel; }
    //        set { this.SetProperty<FlightDataEntitiesRT.Charts.ChartPanel>(ref m_panel, value); }
    //    }

    //    public ChartPanelWrap(FlightDataEntitiesRT.Charts.ChartPanel panel, ChartPanelViewModel viewModel)
    //    {
    //        this.m_panel = panel;
    //        this.viewModel = viewModel;
    //    }

    //    public bool IsCurrent
    //    {
    //        get
    //        {
    //            if (viewModel.CurrentIndex >= 0 &&
    //                this.viewModel.ChartPanelCollections.Count > viewModel.CurrentIndex)
    //            {
    //                if (viewModel.ChartPanelCollections.IndexOf(this) == viewModel.CurrentIndex)
    //                    return true;
    //            }

    //            return false;
    //        }
    //        set
    //        {
    //            if (viewModel != null)
    //            {
    //                this.viewModel.CurrentIndex = this.viewModel.ChartPanelCollections.IndexOf(this);
    //                foreach (var one in this.viewModel.ChartPanelCollections)
    //                {
    //                    one.OnPropertyChanged("IsCurrent");
    //                }
    //            }
    //        }
    //    }
    //}
}
