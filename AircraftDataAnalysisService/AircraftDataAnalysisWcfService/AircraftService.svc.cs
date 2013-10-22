using FlightDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System.Configuration;
using System.Xml.Linq;
using System.IO;
using AircraftDataAnalysisWcfService.DALs;

namespace AircraftDataAnalysisWcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“AircraftService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 AircraftService.svc 或 AircraftService.svc.cs，然后开始调试。
    public class AircraftService : IAircraftService
    {
        public AircraftModel GetCurrentAircraftModel()
        {
            if (m_currentAircraftModel == null)
                this.InitCurrentAircraftModel();

            return m_currentAircraftModel;
        }

        private static AircraftModel m_currentAircraftModel = null;

        private void InitCurrentAircraftModel()
        {
            string modelName = ConfigurationManager.AppSettings["AircraftModelName"];
            string modelCaption = ConfigurationManager.AppSettings["AircraftModelCaption"];

            AircraftModel model = new AircraftModel() { ModelName = modelName, Caption = modelCaption, LastUsed = DateTime.Now };

            m_currentAircraftModel = model;
        }

        public FlightParameters GetAllFlightParameters(AircraftModel model)
        {
            AircraftServiceBll bll = new AircraftServiceBll();
            return bll.GetAllFlightParameters(model);
        }

        public FlightDataEntities.Charts.ChartPanel[] GetAllChartPanels(AircraftModel aircraftModel)
        {
            AircraftServiceBll bll = new AircraftServiceBll();
            return bll.GetAllChartPanels(aircraftModel);
        }

        public KeyValuePair<string, FlightDataEntities.FlightRawData[]>[] GetFlightData(Flight flight,
            string[] parameterIds, int startSecond, int endSecond)
        {
            AircraftServiceBll bll = new AircraftServiceBll();
            return bll.GetFlightData(flight, parameterIds, startSecond, endSecond);
        }

        public FlightDataEntities.LevelTopFlightRecord[] GetLevelTopFlightRecords(
            Flight flight, string[] parameterIds, bool withLevel1Data)
        {
            AircraftServiceBll bll = new AircraftServiceBll();
            return bll.GetLevelTopFlightRecords(flight, parameterIds, withLevel1Data);
        }

        public FlightDataEntities.Decisions.Decision[] GetAllDecisions(AircraftModel aircraftModel)
        {
            AircraftServiceBll bll = new AircraftServiceBll();
            return bll.GetAllDecisions(aircraftModel);
        }


        public Flight[] GetAllFlights(AircraftModel model)
        {
            AircraftServiceBll bll = new AircraftServiceBll();
            return bll.GetAllFlights(model);
        }

        public ExtremumPointInfo[] GetExtremumPointInfos(Flight flight)
        {
            AircraftServiceBll bll = new AircraftServiceBll();
            return bll.GetExtremumPointInfos(flight);
        }

        public FlightDataEntities.Decisions.DecisionRecord[] GetDecisionRecords(Flight flight)
        {
            AircraftServiceBll bll = new AircraftServiceBll();
            return bll.GetDecisionRecords(flight);
        }
    }
}
