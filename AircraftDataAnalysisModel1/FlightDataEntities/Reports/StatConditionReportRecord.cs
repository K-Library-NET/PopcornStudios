using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntities.Reports
{
    public class StatConditionReportRecord
    {
        public ObjectId Id
        {
            get;
            set;
        }

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
