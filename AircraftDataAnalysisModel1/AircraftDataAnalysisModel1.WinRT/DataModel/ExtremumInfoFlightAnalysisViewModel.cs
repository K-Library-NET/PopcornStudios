using AircraftDataAnalysisWinRT.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftDataAnalysisWinRT.DataModel
{
    public class ExtremumInfoFlightAnalysisViewModel : FlightAnalysisViewModel
    {
        private ExtremumReportItemWrap extremumReportItemWrap;

        public ExtremumInfoFlightAnalysisViewModel(ExtremumReportItemWrap extremumReportItemWrap)
            : base()
        {
            // TODO: Complete member initialization
            this.extremumReportItemWrap = extremumReportItemWrap;

            this.InitData();
        }

        private void InitData()
        {
            if (this.extremumReportItemWrap == null)
            {
                //this.GridData = null;
                return;
            }

            this.RelatedParameterCollection = this.GetSelfRelatedParameterFromDecision();

            //拟定区间段
            ////根据ExtremumInfo，取得一段数据
            this.CurrentStartSecond = 0;
            this.CurrentEndSecond = ApplicationContext.Instance.CurrentFlight.EndSecond;

            int minSec = Math.Min(this.extremumReportItemWrap.MinValueSecond, this.extremumReportItemWrap.MaxValueSecond);
            int maxSec = Math.Max(this.extremumReportItemWrap.MinValueSecond, this.extremumReportItemWrap.MaxValueSecond);

            if (minSec > this.CurrentStartSecond)
            {//大概预留八分之一长度：
                int length = (maxSec - minSec) / 8;
                this.CurrentStartSecond = Math.Max(minSec - length, this.CurrentStartSecond);
            }
            if (maxSec < this.CurrentEndSecond)
            {//大概预留八分之一长度：
                int length = (maxSec - minSec) / 8;
                this.CurrentEndSecond = Math.Min(maxSec + length, this.CurrentEndSecond);
            }

            this.RefreshAndRetriveData();
        }

        private System.Collections.ObjectModel.ObservableCollection<RelatedParameterViewModel> GetSelfRelatedParameterFromDecision()
        {
            var parameters = ApplicationContext.Instance.GetFlightParameters(
                         ApplicationContext.Instance.CurrentAircraftModel);

            RelatedParameterViewModel model = new RelatedParameterViewModel(this, true,
                this.FindParameter(this.extremumReportItemWrap.ParameterID, parameters));

            var collection = new System.Collections.ObjectModel.ObservableCollection<RelatedParameterViewModel>();
            collection.Add(model);
            return collection;
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
    }
}
