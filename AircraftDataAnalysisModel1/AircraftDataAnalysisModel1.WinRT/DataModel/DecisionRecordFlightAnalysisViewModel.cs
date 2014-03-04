using AircraftDataAnalysisWinRT.Common;
using AircraftDataAnalysisWinRT.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftDataAnalysisWinRT.DataModel
{
    public class DecisionRecordFlightAnalysisViewModel : FlightAnalysisViewModelOld
    {
        public DecisionRecordFlightAnalysisViewModel(DecisionWrap decisionWrap)
            : base()
        {
            this.DecisionWrap = decisionWrap;

            this.InitData();
        }

        private void InitData()
        {
            if (this.DecisionWrap == null)
            {
                //this.GridData = null;
                return;
            }

            this.RelatedParameterCollection = this.GetSelfRelatedParameterFromDecision();

            //拟定区间段
            ////根据Decision，取得一段数据
            //this.CurrentStartSecond = 0;
            //this.CurrentEndSecond = ApplicationContext.Instance.CurrentFlight.EndSecond;
            //if (this.DecisionWrap.Record.StartSecond > this.CurrentStartSecond)
            //{//大概预留八分之一长度：
            //    int length = (this.DecisionWrap.Record.EndSecond - this.DecisionWrap.Record.StartSecond) / 8;
            //    this.CurrentStartSecond = Math.Max(this.DecisionWrap.Record.StartSecond - length, this.CurrentStartSecond);
            //}
            //if (this.DecisionWrap.Record.EndSecond < this.CurrentEndSecond)
            //{//大概预留八分之一长度：
            //    int length = (this.DecisionWrap.Record.EndSecond - this.DecisionWrap.Record.StartSecond) / 8;
            //    this.CurrentEndSecond = Math.Min(this.DecisionWrap.Record.EndSecond + length, this.CurrentEndSecond);
            //}

            this.CurrentStartSecond = this.DecisionWrap.Record.StartSecond;
            this.CurrentEndSecond = this.DecisionWrap.Record.EndSecond;

            this.RefreshAndRetriveData();
        }

        private System.Collections.ObjectModel.ObservableCollection<RelatedParameterViewModel> GetSelfRelatedParameterFromDecision()
        {
            var parameters = ApplicationContext.Instance.GetFlightParameters(
                         ApplicationContext.Instance.CurrentAircraftModel);

            var result = from one in this.DecisionWrap.Decision.RelatedParameters
                         where this.FindParameter(one, parameters) != null
                         select new RelatedParameterViewModel(this, this.FindParameter(one, parameters));

            return new System.Collections.ObjectModel.ObservableCollection<RelatedParameterViewModel>(result);
        }

        private FlightDataEntitiesRT.FlightParameter FindParameter(string parameterId,
            FlightDataEntitiesRT.FlightParameters parameters)
        {
            var obj = parameters.Parameters.FirstOrDefault(new Func<FlightDataEntitiesRT.FlightParameter, bool>(
                delegate(FlightDataEntitiesRT.FlightParameter para)
                {
                    if (para.ParameterID == parameterId)
                        return true;
                    return false;
                }));

            return obj;
        }

        public DecisionWrap DecisionWrap { get; set; }
    }
}
