﻿using FlightDataEntities;
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
            LogHelper.Info("AircraftService.GetCurrentAircraftModel Requested.", null);

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
            try
            {
                LogHelper.Info("AircraftService.GetAllFlightParameters Requested.", null);
                AircraftServiceBll bll = new AircraftServiceBll();
                return bll.GetAllFlightParameters(model);
            }
            catch (Exception e)
            {
                LogHelper.Error("AircraftService.GetAllFlightParameters", e);
                return null;
            }
        }

        public FlightDataEntities.Charts.ChartPanel[] GetAllChartPanels(AircraftModel aircraftModel)
        {
            try
            {
                LogHelper.Info("AircraftService.GetAllChartPanels Requested.", null);
                AircraftServiceBll bll = new AircraftServiceBll();
                return bll.GetAllChartPanels(aircraftModel);
            }
            catch (Exception e)
            {
                LogHelper.Error("AircraftService.GetAllChartPanels", e);
                return null;
            }
        }

        public KeyValuePair<string, FlightDataEntities.FlightRawData[]>[] GetFlightData(Flight flight,
            string[] parameterIds, int startSecond, int endSecond)
        {
            try
            {
                LogHelper.Info("AircraftService.GetFlightData Requested.", null);
                AircraftServiceBll bll = new AircraftServiceBll();
                return bll.GetFlightData(flight, parameterIds, startSecond, endSecond);
            }
            catch (Exception e)
            {
                LogHelper.Error("AircraftService.GetFlightData", e);
                return null;
            }
        }

        public FlightDataEntities.LevelTopFlightRecord[] GetLevelTopFlightRecords(
            Flight flight, string[] parameterIds)
        {
            try
            {
                LogHelper.Info("AircraftService.GetLevelTopFlightRecords Requested.", null);
                AircraftServiceBll bll = new AircraftServiceBll();
                return bll.GetLevelTopFlightRecords(flight, parameterIds);
            }
            catch (Exception e)
            {
                LogHelper.Error("AircraftService.GetLevelTopFlightRecords", e);
                return null;
            }
        }

        public FlightDataEntities.Decisions.Decision[] GetAllDecisions(AircraftModel aircraftModel)
        {
            try
            {
                LogHelper.Info("AircraftService.GetAllDecisions Requested.", null);
                AircraftServiceBll bll = new AircraftServiceBll();
                return bll.GetAllDecisions(aircraftModel);
            }
            catch (Exception e)
            {
                LogHelper.Error("AircraftService.GetAllDecisions", e);
                return null;
            }
        }


        public Flight[] GetAllFlights(AircraftModel model)
        {
            try
            {
                LogHelper.Info("AircraftService.GetAllFlights Requested.", null);
                AircraftServiceBll bll = new AircraftServiceBll();
                return bll.GetAllFlights(model);
            }
            catch (Exception e)
            {
                LogHelper.Error("AircraftService.GetAllFlights", e);
                return null;
            }
        }

        public FlightDataEntities.Decisions.DecisionRecord[] GetDecisionRecords(Flight flight)
        {
            try
            {
                LogHelper.Info("AircraftService.GetDecisionRecords Requested.", null);
                AircraftServiceBll bll = new AircraftServiceBll();
                return bll.GetDecisionRecords(flight);
            }
            catch (Exception e)
            {
                LogHelper.Error("AircraftService.GetDecisionRecords", e);
                return null;
            }
        }

        public ExtremumReportDefinition GetExtremumReportDefinition(string aircraftModelName)
        {
            try
            {
                LogHelper.Info("AircraftService.GetExtremumReportDefinition Requested.", null);
                AircraftServiceBll bll = new AircraftServiceBll();
                return bll.GetExtremumReportDefinition(aircraftModelName);
            }
            catch (Exception e)
            {
                LogHelper.Error("AircraftService.GetExtremumReportDefinition", e);
                return null;
            }
        }
    }
}
