using AircraftDataAnalysisWinRT.Common;
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
            
        }
    }
}
