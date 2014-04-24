using AircraftDataAnalysisWinRT.Common;
using AircraftDataAnalysisWinRT.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftDataAnalysisWinRT.DataModel
{
    public class StatReportViewModel : BindableBase
    {
        public StatReportViewModel()
        {
            this.SelectModel = new StatReportSelectViewModel(this);
            this.DataModel = new StatReportDataViewModel(this);
        }

        public StatReportSelectViewModel SelectModel
        {
            get;
            set;
        }

        public StatReportDataViewModel DataModel
        {
            get;
            set;
        }

        public void RefreshData()
        {
            DateTime startMonth = this.GetStartMonth();
            DateTime endMonth = this.GetEndMonth();
            string[] aircraftNumbers = this.AircraftNumbers();

            var decisionRecords = ServerHelper.GetFlightConditionDecisionRecords(
                ApplicationContext.Instance.CurrentAircraftModel,
                startMonth, endMonth, aircraftNumbers);

            DataModel.ReloadDecisionRecords(decisionRecords);
        }

        private string[] AircraftNumbers()
        {
            if (this.SelectModel.Aircrafts != null && this.SelectModel.Aircrafts.Count > 0)
            {
                if (this.SelectModel.Aircrafts[0] is AllFlightSelectViewModelItem)
                {
                    AllFlightSelectViewModelItem all = this.SelectModel.Aircrafts[0] as AllFlightSelectViewModelItem;
                    if (all.IsSelected)
                    {
                        var result = from two in this.SelectModel.Aircrafts
                                     where !string.IsNullOrEmpty(two.AircraftNumber) && !(two is AllFlightSelectViewModelItem)
                                     select two.AircraftNumber;

                        return result.ToArray();
                    }
                    return new string[] { };
                }

                if (this.SelectModel.Aircrafts.Count > 1)
                {
                    var result = from one in this.SelectModel.Aircrafts.Skip(1).Take(this.SelectModel.Aircrafts.Count - 1)
                                 where one.IsSelected
                                 select one.AircraftNumber;

                    return result.Distinct().ToArray();
                }
            }

            return new string[] { };
        }

        private DateTime GetEndMonth()
        {
            if (this.SelectModel.SelectedMonth != null && this.SelectModel.SelectedMonth is AllMonthSelectViewModelItem)
            {
                if (this.SelectModel.SelectedYear != null && this.SelectModel.SelectedYear is AllYearSelectViewModelItem)
                {
                    int year = DateTime.Now.Year;
                    //ServerHelper.GetEarliestYear(ApplicationContext.Instance.CurrentAircraftModel);
                    int month = 12;
                    return new DateTime(year, month, 1);
                }
                else if (this.SelectModel.SelectedYear != null)
                {
                    int year = this.SelectModel.SelectedYear.Year;
                    int month = 12;
                    return new DateTime(year, month, 1);
                }
            }
            else
            {
                if (this.SelectModel.SelectedYear != null && this.SelectModel.SelectedYear is AllYearSelectViewModelItem)
                {
                    int year = DateTime.Now.Year;
                    //ServerHelper.GetEarliestYear(ApplicationContext.Instance.CurrentAircraftModel);
                    int month = this.SelectModel.SelectedMonth.Month;
                    return new DateTime(year, month, 1);
                }
                else if (this.SelectModel.SelectedYear != null)
                {
                    int year = this.SelectModel.SelectedYear.Year;
                    int month = this.SelectModel.SelectedMonth.Month;
                    return new DateTime(year, month, 1);
                }
            }

            return DateTime.Now;
        }

        private DateTime GetStartMonth()
        {
            if (this.SelectModel.SelectedMonth != null && this.SelectModel.SelectedMonth is AllMonthSelectViewModelItem)
            {
                if (this.SelectModel.SelectedYear != null && this.SelectModel.SelectedYear is AllYearSelectViewModelItem)
                {
                    int year = ServerHelper.GetEarliestYear(ApplicationContext.Instance.CurrentAircraftModel);
                    int month = 1;
                    return new DateTime(year, month, 1);
                }
                else if (this.SelectModel.SelectedYear != null)
                {
                    int year = this.SelectModel.SelectedYear.Year;
                    int month = 1;
                    return new DateTime(year, month, 1);
                }
            }
            else
            {
                if (this.SelectModel.SelectedYear != null && this.SelectModel.SelectedYear is AllYearSelectViewModelItem)
                {
                    int year = ServerHelper.GetEarliestYear(ApplicationContext.Instance.CurrentAircraftModel);
                    int month = this.SelectModel.SelectedMonth.Month;
                    return new DateTime(year, month, 1);
                }
                else if (this.SelectModel.SelectedYear != null)
                {
                    int year = this.SelectModel.SelectedYear.Year;
                    int month = this.SelectModel.SelectedMonth.Month;
                    return new DateTime(year, month, 1);
                }
            }

            return DateTime.Now;
        }
    }
}
