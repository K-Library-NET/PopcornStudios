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
            var flightParameters = ApplicationContext.Instance.GetFlightParameters(
                ApplicationContext.Instance.CurrentAircraftModel);

            var rsult = from fp in flightParameters.Parameters
                        select fp.ParameterID;
            this.AllParameterIDs = rsult.ToArray();
        }

        public string[] AllParameterIDs
        {
            get;
            set;
        }

        private ObservableCollection<FlightDataReading.AircraftModel1.AircraftModel1RawData> m_rawDatas = null;

        /// <summary>
        /// 局部缓存的全局数据
        /// </summary>
        public ObservableCollection<FlightDataReading.AircraftModel1.AircraftModel1RawData> RawDatas
        {
            get { return m_rawDatas; }
            set { m_rawDatas = value; }
        }

        private FlightDataEntitiesRT.Charts.ChartPanel m_currentPanel = null;

        /// <summary>
        /// 当前面板，
        /// TODO TODO: 需要修改适应SELECTED TAB
        /// </summary>
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
                              select new RelatedParameterViewModel(this, true, one);
                    this.RelatedParameterCollection = new ObservableCollection<RelatedParameterViewModel>(pms);
                }
            }
        }

        /// <summary>
        /// 根据起始、结束时间，相关的参数刷新数据
        /// </summary>
        public virtual void RefreshAndRetriveData()
        {
            //DataTable dt = AircraftDataAnalysisWinRT.Services.ServerHelper.GetDataTable(
            //    ApplicationContext.Instance.CurrentFlight, this.FromRelatedParametersToStrs(),
            //    this.CurrentStartSecond, this.CurrentEndSecond); 
            string[] parameterKeys = this.FromRelatedParametersToStrs();

            if (this.EntityData == null)
                this.EntityData = new ObservableCollection<KeyValuePair<string, ObservableCollection<ParameterRawData>>>();

            var par1 = from keyset in this.EntityData
                       select keyset.Key;

            var par2 = from one in parameterKeys
                       // where !this.EntityBindingCollection.ValueAddedKey.Contains(one)
                       where !par1.Contains(one)
                       select one;


            if (par2 != null && par2.Count() > 0)
            {
                var datas = AircraftDataAnalysisWinRT.Services.ServerHelper.GetData(
                    ApplicationContext.Instance.CurrentFlight, par2.ToArray(),
                    this.CurrentStartSecond, this.CurrentEndSecond);
                this.MergeEntityData(datas);
                //this.GridData = dt;
                //没必要替换了吧？
                //this.EntityData = datas;

                this.AddToRawDataCollection(this.EntityData,
                    new FlightDataReading.AircraftModel1.AircraftModel1RawDataBuilder());
            }
        }

        private void AddToRawDataCollection(ObservableCollection<KeyValuePair<string,
            ObservableCollection<ParameterRawData>>> collection,
            FlightDataReading.AircraftModel1.AircraftModel1RawDataBuilder builder)
        {//clear
            Dictionary<int, FlightDataReading.AircraftModel1.AircraftModel1RawData> dicSecond
                = new Dictionary<int, FlightDataReading.AircraftModel1.AircraftModel1RawData>();

            foreach (var one in collection)
            {
                foreach (var two in one.Value)
                {
                    if (!dicSecond.ContainsKey(two.Second))
                    {
                        FlightDataReading.AircraftModel1.AircraftModel1RawData dt =
                            new FlightDataReading.AircraftModel1.AircraftModel1RawData();
                        dt.Second = two.Second;
                        dicSecond.Add(two.Second, dt);
                    }

                    FlightDataReading.AircraftModel1.AircraftModel1RawData data = dicSecond[two.Second];
                    builder.AssignValueExt(data, two);
                }
            }

            var result = from i in dicSecond
                         orderby i.Key ascending
                         select i.Value;

            this.RawDatas = new ObservableCollection<FlightDataReading.AircraftModel1.AircraftModel1RawData>(result);
        }

        private void MergeEntityData(ObservableCollection<KeyValuePair<string, ObservableCollection<ParameterRawData>>> datas)
        {
            if (this.EntityData == null || this.EntityData.Count == 0)
            {
                this.EntityData = datas;
                return;
            }

            var result = from one in this.EntityData
                         select one.Key;

            foreach (var i in datas)
            {
                if (!result.Contains(i.Key))
                {
                    this.EntityData.Add(i);
                }
            }
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

        public virtual MyControl.FAChartModel GetFAChartModel()
        {
            if (m_faChartModel == null)
                m_faChartModel = new MyControl.FAChartModel();

            return m_faChartModel;
        }

        private AircraftDataAnalysisWinRT.MyControl.FAChartModel m_faChartModel = null;

        //internal void FilterData(RelatedParameterViewModel relatedParameterViewModel)
        //{
        //    if (FilterDataChanged != null)
        //        this.FilterDataChanged(this, EventArgs.Empty);
        //}
        //FilterData不在这里使用，转移到SubModel里面
        //public event EventHandler FilterDataChanged;

    }

    public class RelatedParameterViewModel : AircraftDataAnalysisWinRT.Common.BindableBase
    {
        public RelatedParameterViewModel(FlightAnalysisViewModel viewModel, bool isChecked, FlightParameter para)
        {
            this.viewModel = viewModel;
            this.m_isChecked = isChecked;
            this.m_parameter = para;
        }

        private bool m_isChecked = true;

        public bool IsChecked
        {
            get { return m_isChecked; }
            set
            {
                this.SetProperty<bool>(ref m_isChecked, value);
                //this.ViewModel.FilterData(this);
                //System.Diagnostics.Debug.WriteLine("IsCheckedChanged");
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
