using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AircraftDataAnalysisWcfService.DALs
{
    public class AircraftMongoDb
    {
        public const string DATABASE_COMMON = "AircraftDataAnalysisCommon";

        public const string COLLECTION_AIRCRAFT_MODEL = "AircraftModel";
        public const string COLLECTION_AIRCRAFT_PARAMETER = "AircraftParameter";
        public const string COLLECTION_AIRCRAFT_INSTANCE = "AircraftInstance";

        public const string COLLECTION_FLIGHT = "Flight";
        //COLLECTION_FLIGHT开头的记录，都是需要根据Flight分块的，一般的原则是COLLECTION_FLIGHT_XXXX + "_"+flightID
        public const string COLLECTION_FLIGHT_DECISIONS = "FlightDecisions";
        public const string COLLECTION_FLIGHT_RECORD_LEVEL1 = "FlightRecordLevel1";
        public const string COLLECTION_FLIGHT_RECORD_LEVELTOP = "FlightRecordLevelTop";
        public const string COLLECTION_FLIGHT_RAWDATA_RELATION = "FlightRawDataRelation";
        public const string COLLECTION_FLIGHT_EXTREME = "FlightExtreme";
        public const string COLLECTION_FLIGHT_GLOBE_DATA = "FlightGlobeDatas";

        public const string COLLECTION_FLIGHT_CONDITION_DECISION = "FlightConditionDecisions";

        private static string m_mongoConnectionString = "mongodb://localhost/?w=1";

        public static string ConnectionString
        {
            get
            {
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["MongoDBConnectionString"]))
                    return ConfigurationManager.AppSettings["MongoDBConnectionString"];
                return m_mongoConnectionString;
            }
        }
    }
}