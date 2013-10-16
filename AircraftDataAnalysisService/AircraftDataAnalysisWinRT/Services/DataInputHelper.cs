using FlightDataEntitiesRT.Decisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftDataAnalysisWinRT.Services
{
    /// <summary>
    /// 封装一些常用的数据导入到服务端的方法
    /// </summary>
    public class DataInputHelper
    {
        /// <summary>
        /// 添加一个架次对象到数据库，必须先添加成功，否则后面数据没有架次作为参数是不行的
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        public static AircraftService.Flight AddFlight(AircraftService.Flight flight)
        {
            throw new NotImplementedException();
        }

        public static string AddOrReplaceDecisionRecords(AircraftService.Flight flight,
            DecisionRecord[] records)
        {
            throw new NotImplementedException();
        }

        public static void AddOneParameterValue(AircraftService.Flight flight, string parameterID,
            FlightDataEntitiesRT.Level1FlightRecord[] reducedRecords, FlightDataEntitiesRT.Level2FlightRecord level2Record)
        {
            throw new NotImplementedException();
        }

        public static void AddDecisionRecordsBatch(AircraftService.Flight flight, List<DecisionRecord> decisionRecords)
        {
            throw new NotImplementedException();
        }

        public static void DeleteExistsData(AircraftService.Flight flight)
        {
            throw new NotImplementedException();
        }
    }
}
