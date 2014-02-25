using AircraftDataAnalysisWinRT.Common;
using AircraftDataAnalysisWinRT.Services;
using FlightDataEntitiesRT.Decisions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace AircraftDataAnalysisWinRT.DataModel
{
    public class FaultDiagnosisViewModel : AircraftDataAnalysisWinRT.Common.BindableBase
    {
        public FaultDiagnosisViewModel()
        {
            //InitEventLevelBinder();

            //取得当前架次的判据
            var decisionRecords = ServerHelper.GetDecisionRecords(ApplicationContext.Instance.CurrentFlight);
            var decisions = ApplicationContext.Instance.GetDecisions(ApplicationContext.Instance.CurrentAircraftModel);

            this.BuildAndBindRecord(decisionRecords, decisions);
        }

        private bool m_isValueSelected = false;

        public bool IsValueSelected
        {
            get { return m_isValueSelected; }
            set
            {
                this.SetProperty<bool>(ref m_isValueSelected, value);
            }
        }

        private bool m_isShowAllDecisions = true;

        public bool IsShowAllDecisions
        {
            get
            {
                return m_isShowAllDecisions;
            }
            set
            {
                this.SetProperty<bool>(ref m_isShowAllDecisions, value);
                this.Filter();
            }
        }

        /// <summary>
        /// 直接用一个ToggleSwitch就够了
        /// </summary>
        [Obsolete]
        private void InitEventLevelBinder()
        {
            EventLevelBinder binder = new EventLevelBinder(this);
            EventLevelBinder binder1 = new EventLevelBinder(this) { EventLevelName = "级别1", EventLevel = 1 };
            EventLevelBinder binder2 = new EventLevelBinder(this) { EventLevelName = "级别2", EventLevel = 2 };

            this.EventBinderCollection.Add(binder);
            this.EventBinderCollection.Add(binder1);
            this.EventBinderCollection.Add(binder2);
        }

        private void BuildAndBindRecord(FlightDataEntitiesRT.Decisions.DecisionRecord[] decisionRecords,
            IEnumerable<FlightDataEntitiesRT.Decisions.Decision> decisions)
        {
            var decisionRecordWrap = from one in decisionRecords
                                     where FindDecisionRecord(one, decisions) != null
                                     select new DecisionWrap(one,
                                         FindDecisionRecord(one, decisions));

            var decisionNullWrap = from one in decisions
                                   where FindDecisionRecord(one, decisionRecords) == null
                                   select new DecisionWrap(null, one);

            List<DecisionWrap> wrapList = new List<DecisionWrap>();
            if (decisionRecordWrap != null && decisionRecordWrap.Count() > 0)
            {
                wrapList.AddRange(decisionRecordWrap);
            }
            if (decisionNullWrap != null && decisionNullWrap.Count() > 0)
            {
                wrapList.AddRange(decisionNullWrap);
            }

            this.values = new List<DecisionWrap>(wrapList);

            this.DecisionRecordCollection = new ObservableCollection<DecisionWrap>(values);
        }

        private IEnumerable<DecisionWrap> values = new List<DecisionWrap>();

        /// <summary>
        /// 通过判据找到记录
        /// </summary>
        /// <param name="one"></param>
        /// <param name="decisionRecords"></param>
        /// <returns></returns>
        private DecisionRecord FindDecisionRecord(Decision one, DecisionRecord[] decisionRecords)
        {
            var finded = decisionRecords.FirstOrDefault(new Func<DecisionRecord, bool>(
                delegate(DecisionRecord rec)
                {
                    if (rec.DecisionID == one.DecisionID)
                        return true;
                    return false;
                }));

            return finded;
        }

        /// <summary>
        /// 通过记录找到判据
        /// </summary>
        /// <param name="one"></param>
        /// <param name="decisions"></param>
        /// <returns></returns>
        private Decision FindDecisionRecord(DecisionRecord one, IEnumerable<Decision> decisions)
        {
            var finded = decisions.FirstOrDefault(new Func<Decision, bool>(delegate(Decision dec)
            {
                if (dec.DecisionID == one.DecisionID)
                    return true;
                return false;
            }));

            return finded;
        }

        private ObservableCollection<EventLevelBinder> m_eventBinderCollection = new ObservableCollection<EventLevelBinder>();

        public ObservableCollection<EventLevelBinder> EventBinderCollection
        {
            get { return m_eventBinderCollection; }
            set
            {
                this.SetProperty<ObservableCollection<EventLevelBinder>>(ref m_eventBinderCollection, value);
            }
        }

        private ObservableCollection<AircraftDataAnalysisWinRT.Common.DecisionWrap> m_decisionWraps
            = new ObservableCollection<Common.DecisionWrap>();

        public ObservableCollection<AircraftDataAnalysisWinRT.Common.DecisionWrap> DecisionRecordCollection
        {
            get { return m_decisionWraps; }
            set
            {
                this.SetProperty<ObservableCollection<AircraftDataAnalysisWinRT.Common.DecisionWrap>>(
                    ref m_decisionWraps, value);
            }
        }

        internal void Filter(EventLevelBinder eventLevelBinder)
        {
            var selectedLevels = from o in this.EventBinderCollection
                                 where o.IsChecked
                                 select o.EventLevel;

            var filtered = from one in this.values
                           where selectedLevels.Contains(one.EventLevel)
                           select one;

            this.DecisionRecordCollection = new ObservableCollection<DecisionWrap>(filtered);
        }

        private void Filter()
        {
            var filtered = from one in this.values
                           where this.IsShowAllDecisions || one.Record != null
                          // where selectedLevels.Contains(one.EventLevel)
                           select one;

            this.DecisionRecordCollection = new ObservableCollection<DecisionWrap>(filtered);
        }
    }

    public class EventLevelBinder : AircraftDataAnalysisWinRT.Common.BindableBase
    {
        public EventLevelBinder(FaultDiagnosisViewModel viewModel)
        {
            m_viewModel = viewModel;
        }

        private FaultDiagnosisViewModel m_viewModel;

        private int m_eventLevel = -1;

        public int EventLevel
        {
            get { return m_eventLevel; }
            set
            {
                this.SetProperty<int>(ref m_eventLevel, value);
            }
        }

        private string m_eventLevelName = "未发生事件";

        public string EventLevelName
        {
            get { return m_eventLevelName; }
            set { this.SetProperty<string>(ref m_eventLevelName, value); }
        }

        private bool m_isChecked = true;

        public bool IsChecked
        {
            get
            {
                return m_isChecked;
            }
            set
            {
                this.SetProperty<bool>(ref m_isChecked, value);
                this.m_viewModel.Filter(this);
            }
        }
    }
}
