using FlightDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AircraftDataAnalysisWcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 Service1.svc 或 Service1.svc.cs，然后开始调试。
    public class DataInputService : IAircraftDataInput
    {
        public FlightDataEntities.Flight AddOrReplaceFlight(FlightDataEntities.Flight flight)
        {
            try
            {
                DataInputServiceBll bll = new DataInputServiceBll();
                return bll.AddOrReplaceFlight(flight);
            }
            catch (Exception ex)
            {
                LogHelper.Error("AddOrReplaceFlight", ex);
                return null;
            }
        }

        public string DeleteExistsData(FlightDataEntities.Flight flight)
        {
            try
            {
                DataInputServiceBll bll = new DataInputServiceBll();
                return bll.DeleteExistsData(flight);
            }
            catch (Exception ex)
            {
                LogHelper.Error("DeleteExistsData", ex);
                return ex.Message;
            }
        }

        public string AddDecisionRecordsBatch(FlightDataEntities.Flight flight, FlightDataEntities.Decisions.DecisionRecord[] records)
        {
            try
            {
                DataInputServiceBll bll = new DataInputServiceBll();
                return bll.AddDecisionRecordsBatch(flight, records);
            }
            catch (Exception ex)
            {
                LogHelper.Error("AddDecisionRecordsBatch", ex);
                return ex.Message;
            }
        }

        public string AddOneParameterValue(FlightDataEntities.Flight flight, string parameterID,
            FlightDataEntities.Level1FlightRecord[] reducedRecords)
        {
            try
            {
                DataInputServiceBll bll = new DataInputServiceBll();
                return bll.AddOneParameterValue(flight, parameterID, reducedRecords);
            }
            catch (Exception ex)
            {
                LogHelper.Error("AddOneParameterValue", ex);
                return ex.Message;
            }
        }

        public string AddLevelTopFlightRecords(FlightDataEntities.Flight flight,
            FlightDataEntities.LevelTopFlightRecord[] topRecords)
        {
            try
            {
                DataInputServiceBll bll = new DataInputServiceBll();
                return bll.AddLevelTopFlightRecords(flight, topRecords);
            }
            catch (Exception ex)
            {
                LogHelper.Error("AddLevelTopFlightRecords", ex);
                return ex.Message;
            }
        }
    }
}
