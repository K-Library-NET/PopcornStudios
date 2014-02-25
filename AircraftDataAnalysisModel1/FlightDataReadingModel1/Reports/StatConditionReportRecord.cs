using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT.Reports
{
    public class StatConditionReportRecord
    {
        public string FlightID
        {
            get;
            set;
        }

        public string AircraftNumber
        {
            get;
            set;
        }

        public string ModelName
        {
            get;
            set;
        }

        public DateTime FlightDate
        {
            get;
            set;
        }

        public int Year
        {
            get
            {
                return this.FlightDate.Year;
            }
        }

        public int Month
        {
            get
            {
                return this.FlightDate.Month;
            }
        }

        public int Quarter
        {
            get
            {
                switch (this.FlightDate.Month)
                {
                    case 1:
                    case 2:
                    case 3: return 1;
                    case 4:
                    case 5:
                    case 6: return 2;
                    case 7:
                    case 8:
                    case 9: return 3;
                    default: return 4;
                }
            }
        }

        public double Condition1Value
        {
            get;
            set;
        }

        public double Condition2Value
        {
            get;
            set;
        }

        public double Condition3Value
        {
            get;
            set;
        }

        public double Condition4Value
        {
            get;
            set;
        }
    }
}
