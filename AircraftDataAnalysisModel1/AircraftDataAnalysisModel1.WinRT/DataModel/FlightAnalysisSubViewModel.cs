using AircraftDataAnalysisWinRT.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftDataAnalysisWinRT.DataModel
{
    /// <summary>
    /// 图表联动的DataContext，
    /// 应该直接包含FlightAnalysisViewModel的内容，
    /// 避免对象耦合过多
    /// </summary>
    public class FlightAnalysisSubViewModel : BindableBase
    {
        /// <summary>
        /// 必须的元素：start/end，HostParameterID，RelatedParameterIDs
        /// CurrentSecond（当前秒，可以为空）、CurrentSecond Row Object
        /// 数据集合（相关参数的全集，增加参数可以取）
        /// </summary>
        public FlightAnalysisSubViewModel()
        {
            this.DataCollection = new ObservableCollection<FlightDataReading.AircraftModel1.AircraftModel1RawData>();
            this.m_relatedParameterIDs.CollectionChanged += m_relatedParameterIDs_CollectionChanged;
        }

        void m_relatedParameterIDs_CollectionChanged(object sender,
            System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {//debug
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

        public FlightDataReading.AircraftModel1.AircraftModel1RawData CurrentSecondRowObject
        {
            get
            {
                this.DataCollection.Single(
                    new Func<FlightDataReading.AircraftModel1.AircraftModel1RawData, bool>(
                        delegate(FlightDataReading.AircraftModel1.AircraftModel1RawData dt)
                        {
                            if (dt != null && this.CurrentSecond != null
                                && dt.Second == this.CurrentSecond.Value)
                                //暂时不用四舍五入
                                //&& dt.Second == Math.Round(this.CurrentSecond.Value))
                                return true;
                            return false;
                        }));
                return null;
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

        private string m_hostParameterID = string.Empty;

        public string HostParameterID
        {
            get { return m_hostParameterID; }
            set
            {
                this.SetProperty<string>(ref m_hostParameterID, value);
            }
        }

        private double? m_currentSecond = null;

        public double? CurrentSecond
        {
            get { return m_currentSecond; }
            set
            {
                this.SetProperty<double?>(ref m_currentSecond, value);
                base.OnPropertyChanged("CurrentSecondRowObject");
            }
        }

        private ObservableCollection<string> m_relatedParameterIDs = new ObservableCollection<string>();

        public ObservableCollection<string> RelatedParameterIDs
        {
            get
            {
                return m_relatedParameterIDs;
            }
        }

        public ObservableCollection<FlightDataReading.AircraftModel1.AircraftModel1RawData> DataCollection
        {
            get;
            set;
        }

        public IEnumerable<FlightDataReading.AircraftModel1.AircraftModel1RawData> GetFilteredDataCollection()
        {

            var filteredCollection = from one in this.DataCollection
                                     where one.Second >= this.CurrentStartSecond
                                     && one.Second < this.CurrentEndSecond
                                     select one;

            if (filteredCollection == null
                || filteredCollection.Count() <= 0)
            {
                return new List<FlightDataReading.AircraftModel1.AircraftModel1RawData>();
            }

            return filteredCollection;
        }

        public virtual MyControl.FAChartModel GetFAChartModel()
        {
            if (this.ViewModel == null)
                return null;

            return this.ViewModel.GetFAChartModel();
            //if (m_faChartModel == null)
            //    m_faChartModel = new MyControl.FAChartModel();

            //return m_faChartModel;
        }

        public AircraftDataAnalysisWinRT.Domain.FlightAnalysisViewModel ViewModel
        {
            get;
            set;
        }
    }
}
