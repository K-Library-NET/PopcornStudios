using FlightDataEntitiesRT;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftDataAnalysisWinRT.Domain
{
    public class FlightAnalysisViewModel : AircraftDataAnalysisWinRT.Common.BindableBase
    {
        public FlightAnalysisViewModel()
        {
        }

        private FlightDataEntitiesRT.Charts.ChartPanel m_currentPanel = null;

        public FlightDataEntitiesRT.Charts.ChartPanel CurrentPanel
        {
            get
            {
                return m_currentPanel;
            }
            set
            {
                this.SetProperty<FlightDataEntitiesRT.Charts.ChartPanel>(ref m_currentPanel, value);
                this.RelatedParameterCollection.Clear();
                //RelatedParametery要改变
                if (this.m_currentPanel == null)
                {
                    return;
                }
                else
                {
                    var paramIDs = m_currentPanel.ParameterIDs;

                    FlightDataEntitiesRT.FlightParameters parameters =
                        ApplicationContext.Instance.GetFlightParameters(ApplicationContext.Instance.CurrentAircraftModel);
                    var pms = from one in parameters.Parameters
                              where paramIDs.Contains(one.ParameterID)
                              select new RelatedParameterViewModel(this) { IsChecked = true, Parameter = one };
                    this.RelatedParameterCollection = new ObservableCollection<RelatedParameterViewModel>(pms);
                }
            }
        }

        /// <summary>
        /// 根据起始、结束时间，相关的参数刷新数据
        /// </summary>
        public void RefreshAndRetriveData()
        {
            DataTable dt = AircraftDataAnalysisWinRT.Services.ServerHelper.GetDataTable(
                ApplicationContext.Instance.CurrentFlight, this.FromRelatedParametersToStrs(),
                this.CurrentStartSecond, this.CurrentEndSecond);

            var datas = AircraftDataAnalysisWinRT.Services.ServerHelper.GetData(
                ApplicationContext.Instance.CurrentFlight, this.FromRelatedParametersToStrs(),
                this.CurrentStartSecond, this.CurrentEndSecond);

            this.GridData = dt;
            this.EntityData = datas;
        }

        private string[] FromRelatedParametersToStrs()
        {
            var result = from one in this.m_relatedParameterCollection
                         select one.Parameter.ParameterID;
            return result.ToArray();
        }

        public bool RelatedParameterSelected(string parameterId)
        {
            if (this.RelatedParameterCollection == null
               || this.RelatedParameterCollection.Count == 0)
                return true;

            var vm1 = this.RelatedParameterCollection.FirstOrDefault(
                new Func<RelatedParameterViewModel, bool>(delegate(RelatedParameterViewModel vm)
                {
                    if (vm.Parameter.ParameterID == parameterId)
                        return true;
                    return false;
                }));

            if (vm1 != null && vm1.IsChecked == false)
                return false;

            return true;
        }

        private DataTable m_gridData = null;

        public DataTable GridData
        {
            get
            {
                return m_gridData;
            }
            internal set
            {
                this.SetProperty<DataTable>(ref m_gridData, value);
            }
        }

        private ObservableCollection<KeyValuePair<string, ObservableCollection<ParameterRawData>>> m_entityData = null;

        public ObservableCollection<KeyValuePair<string, ObservableCollection<ParameterRawData>>> EntityData
        {
            get
            {
                return m_entityData;
            }
            set
            {
                this.SetProperty<ObservableCollection<KeyValuePair<string, ObservableCollection<ParameterRawData>>>>(
                    ref m_entityData, value);
            }
        }

        private ObservableCollection<RelatedParameterViewModel> m_relatedParameterCollection
            = new ObservableCollection<RelatedParameterViewModel>();

        public ObservableCollection<RelatedParameterViewModel> RelatedParameterCollection
        {
            get
            {
                return m_relatedParameterCollection;
            }
            internal set
            {
                this.SetProperty<ObservableCollection<RelatedParameterViewModel>>(ref m_relatedParameterCollection, value);
            }
        }

        private int m_currentStartSecond = 0;

        public int CurrentStartSecond
        {
            get { return m_currentStartSecond; }
            set
            {
                this.SetProperty<int>(ref m_currentStartSecond, value);
            }
        }

        private int m_currentEndSecond = 0;

        public int CurrentEndSecond
        {
            get { return m_currentEndSecond; }
            set
            {
                this.SetProperty<int>(ref m_currentEndSecond, value);
            }
        }

        internal void FilterData(RelatedParameterViewModel relatedParameterViewModel)
        {
            if (FilterDataChanged != null)
                this.FilterDataChanged(this, EventArgs.Empty);
        }

        public event EventHandler FilterDataChanged;
    }

    public class RelatedParameterViewModel : AircraftDataAnalysisWinRT.Common.BindableBase
    {
        public RelatedParameterViewModel(FlightAnalysisViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        private bool m_isChecked = true;

        public bool IsChecked
        {
            get { return m_isChecked; }
            set
            {
                this.SetProperty<bool>(ref m_isChecked, value);
                this.ViewModel.FilterData(this);
                System.Diagnostics.Debug.WriteLine("IsCheckedChanged");
            }
        }

        private FlightAnalysisViewModel viewModel;

        public FlightAnalysisViewModel ViewModel
        {
            get { return this.viewModel; }
            set { this.SetProperty<FlightAnalysisViewModel>(ref this.viewModel, value); }
        }

        private FlightParameter m_parameter;

        public FlightParameter Parameter
        {
            get { return m_parameter; }
            set { this.SetProperty<FlightParameter>(ref m_parameter, value); }
        }

    }
}
