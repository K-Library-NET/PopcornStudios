using AircraftDataAnalysisWinRT.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftDataAnalysisWinRT
{
    public class ApplicationContext
    {
        private ApplicationContext()
        {
            //使用云端IIS服务，去掉下面两行注释
            //this.DataInputServiceURL = "http://42.96.198.241/AircraftDataAnalysisWcfService/DataInputService.svc";
            //this.AircraftServiceURL = "http://42.96.198.241/AircraftDataAnalysisWcfService/AircraftService.svc";
            //throw new Exception("test");
            //使用本机IIS服务（不是VS调试器），去掉下面两行注释
            this.DataInputServiceURL = "http://127.0.0.1/AircraftDataAnalysisWcfService/DataInputService.svc";
            this.AircraftServiceURL = "http://127.0.0.1/AircraftDataAnalysisWcfService/AircraftService.svc";

            //上面四行全部注释，那就是使用VS调试器
        }

        public string AircraftServiceURL { get; set; }

        public string DataInputServiceURL { get; set; }

        private static ApplicationContext m_instance;
        public static ApplicationContext Instance
        {
            get
            {
                if (m_instance == null)
                    m_instance = new ApplicationContext();
                return m_instance;
            }
        }

        public FlightDataEntitiesRT.Flight CurrentFlight { get; set; }

        public FlightDataEntitiesRT.AircraftModel CurrentAircraftModel { get; set; }

        private Dictionary<string, object> m_objectMap = new Dictionary<string, object>();

        internal IEnumerable<FlightDataEntitiesRT.Charts.ChartPanel> GetChartPanels(FlightDataEntitiesRT.AircraftModel aircraftModel)
        {
            if (this.m_objectMap.ContainsKey("AircraftModel_ChartPanel:" + aircraftModel.ModelName)
                && this.m_objectMap["AircraftModel_ChartPanel:" + aircraftModel.ModelName] != null)
            {
                return m_objectMap["AircraftModel_ChartPanel:" + aircraftModel.ModelName] as IEnumerable<FlightDataEntitiesRT.Charts.ChartPanel>;
            }
            else
            {
                var panels = ServerHelper.GetChartPanels(aircraftModel);
                m_objectMap.Add("AircraftModel_ChartPanel:" + aircraftModel.ModelName, panels);
                return panels;
            }
        }

        internal FlightDataEntitiesRT.FlightParameters GetFlightParameters(
            FlightDataEntitiesRT.AircraftModel aircraftModel)
        {
            if (this.m_objectMap.ContainsKey("AircraftModel_FlightParameters:" + aircraftModel.ModelName)
                && this.m_objectMap["AircraftModel_FlightParameters:" + aircraftModel.ModelName] != null)
            {
                return m_objectMap["AircraftModel_FlightParameters:" + aircraftModel.ModelName] as FlightDataEntitiesRT.FlightParameters;
            }
            else
            {
                var parameters = ServerHelper.GetFlightParameters(aircraftModel);
                m_objectMap.Add("AircraftModel_FlightParameters:" + aircraftModel.ModelName, parameters);
                return parameters;
            }
        }

        internal IEnumerable<FlightDataEntitiesRT.Decisions.Decision> GetDecisions(FlightDataEntitiesRT.AircraftModel aircraftModel)
        {
            if (this.m_objectMap.ContainsKey("AircraftModel_Decisions:" + aircraftModel.ModelName)
                && this.m_objectMap["AircraftModel_Decisions:" + aircraftModel.ModelName] != null)
            {
                return m_objectMap["AircraftModel_Decisions:" + aircraftModel.ModelName] as IEnumerable<FlightDataEntitiesRT.Decisions.Decision>;
            }
            else
            {
                var decisions = ServerHelper.GetDecisions(aircraftModel);
                m_objectMap.Add("AircraftModel_Decisions:" + aircraftModel.ModelName, decisions);
                return decisions;
            }

        }


        internal string GetFlightParameterCaption(string p)
        {
            var parames = this.GetFlightParameters(CurrentAircraftModel);
            var result = from one in parames.Parameters
                         where one.ParameterID == p
                         select one.Caption;

            if (result != null && result.Count() > 0)
                return result.First();

            return p;
        }
    }
}
