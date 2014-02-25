using FlightDataEntitiesRT;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AircraftService = AircraftDataAnalysisModel1.WinRT.AircraftService;

namespace AircraftDataAnalysisWinRT.Services
{
    /// <summary>
    /// 封装一些界面常用的从服务端取得数据的方法
    /// </summary>
    public class ServerHelper
    {
        /// <summary>
        /// 取得当前的机型
        /// </summary>
        /// <returns></returns>
        public static AircraftModel GetCurrentAircraftModel()
        {
            AircraftService.AircraftServiceClient client = GetClient();
            var result = client.GetCurrentAircraftModelAsync();
            result.Wait();
            client.CloseAsync();
            return RTConverter.FromDataInput(result.Result);
        }

        private static AircraftService.AircraftServiceClient GetClient()
        {
            if (!string.IsNullOrEmpty(ApplicationContext.Instance.AircraftServiceURL))
            {
                return new AircraftService.AircraftServiceClient(
                    AircraftService.AircraftServiceClient.EndpointConfiguration.BasicHttpBinding_IAircraftService,
                    ApplicationContext.Instance.AircraftServiceURL);
            }
            return new AircraftService.AircraftServiceClient();
        }

        /// <summary>
        /// 取得当前的机型（异步方法）
        /// </summary>
        /// <returns></returns>
        public static Task<AircraftModel> GetCurrentAircraftModelAsync()
        {
            AircraftService.AircraftServiceClient client = GetClient();
            Task<AircraftService.AircraftModel> task = client.GetCurrentAircraftModelAsync();
            Task<AircraftModel> task2 = task.ContinueWith<AircraftModel>(
                new Func<Task, AircraftModel>(
                    delegate(Task t)
                    {
                        t.Wait();
                        return RTConverter.FromDataInput(task.Result);
                    }));
            client.CloseAsync();
            return task2;
        }

        /// <summary>
        /// 取得该机型的所有架次
        /// </summary>
        /// <param name="aircraftModel"></param>
        /// <returns></returns>
        public static Flight[] GetAllFlights(AircraftModel aircraftModel)
        {
            AircraftService.AircraftServiceClient client = GetClient();
            var task = client.GetAllFlightsAsync(RTConverter.ToAircraftService(aircraftModel));
            task.Wait();
            var results = task.Result;
            client.CloseAsync();

            if (results == null || results.Count() <= 0)
                return new Flight[] { };

            var result2 = from one in results
                          select RTConverter.FromAircraftService(one);
            return result2.ToArray();
        }

        /// <summary>
        /// 根据架次、参数ID列表、起始和结束秒，获取数据
        /// 数据返回轻量级DataTable，行是秒值，列是参数值，可以直接绑定到RadGridView
        /// 在此不作分页，因为起始秒和结束秒就可以起到分页的作用
        /// </summary>
        /// <param name="flight"></param>
        /// <param name="parameterIds"></param>
        /// <param name="startSecond"></param>
        /// <param name="endSecond"></param>
        /// <returns></returns>
        public static FlightDataEntitiesRT.DataTable GetDataTable(Flight flight,
            string[] parameterIds, int startSecond, int endSecond)
        {
            ObservableCollection<string> collection = null;
            if (parameterIds != null && parameterIds.Length > 0)
                collection = new ObservableCollection<string>(parameterIds);
            AircraftService.AircraftServiceClient client = GetClient();

            var task = client.GetFlightDataAsync(RTConverter.ToAircraftService(flight),
                collection, startSecond, endSecond);
            task.Wait();
            client.CloseAsync();
            ObservableCollection<KeyValuePair<string, ObservableCollection<
                AircraftService.FlightRawData>>>
                result = task.Result;

            DataTable dt = BuildDataTable(result);

            return dt;
        }

        public static ObservableCollection<KeyValuePair<string,
            ObservableCollection<FlightDataEntitiesRT.ParameterRawData>>> GetData(Flight flight,
            string[] parameterIds, int startSecond, int endSecond)
        {
            ObservableCollection<string> collection = null;
            if (parameterIds != null && parameterIds.Length > 0)
                collection = new ObservableCollection<string>(parameterIds);
            AircraftService.AircraftServiceClient client = GetClient();

            var task = client.GetFlightDataAsync(RTConverter.ToAircraftService(flight),
                collection, startSecond, endSecond);
            task.Wait();
            client.CloseAsync();
            ObservableCollection<KeyValuePair<string, ObservableCollection<
                AircraftService.FlightRawData>>>
                result = task.Result;

            return RTConverter.FromAircraftService(task.Result);

            //DataTable dt = BuildDataTable(result);

            //return dt;
        }

        /// <summary>
        /// 根据记录创建DataTable方法
        /// 目前是秒值是行，列是参数名称，因为列不能太多（超过255列？）
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private static DataTable BuildDataTable(ObservableCollection<KeyValuePair<string,
            ObservableCollection<AircraftService.FlightRawData>>> result)
        {
            DataTable dt = new DataTable();
            //all column first
            dt.Columns.Add(new DataColumn() { ColumnName = "Second", DataType = DataTypeConverter.TypeInt32, Caption = "秒" });

            if (result == null || result.Count() == 0)
                return dt;

            var temp1 = from one in result
                        select one.Key;

            foreach (var t1 in temp1)
            {
                dt.Columns.Add(new DataColumn() { ColumnName = t1, Caption = t1, DataType = DataTypeConverter.TypeFloat });
            }

            Dictionary<int, DataRow> dataRowMap = new Dictionary<int, DataRow>();
            foreach (var t2 in result)
            {
                foreach (var data in t2.Value)
                {
                    if (!dataRowMap.ContainsKey(data.Second))
                    {
                        DataRow dr = dt.NewRow();
                        dr["Second"] = data.Second;
                        dataRowMap.Add(data.Second, dr);
                    }

                    dataRowMap[data.Second][data.ParameterID] = data.Values[0];
                }
            }

            var temp2 = from two in dataRowMap
                        orderby two.Key ascending
                        select two.Value;

            foreach (var t3 in temp2)
            {
                dt.Rows.Add(t3);
            }
            return dt;
        }

        public static FlightDataEntitiesRT.LevelTopFlightRecord[] GetLevelTopFlightRecords(Flight flight, string[] parameterIDs)
        {
            AircraftService.AircraftServiceClient client = GetClient();
            var taskInfos = client.GetLevelTopFlightRecordsAsync(RTConverter.ToAircraftService(flight),
                (parameterIDs == null || parameterIDs.Length == 0) ? new ObservableCollection<string>() :
                new ObservableCollection<string>(parameterIDs));
            taskInfos.Wait();
            client.CloseAsync();
            var result = from one in taskInfos.Result
                         select RTConverter.FromAircraftService(one);

            return result.ToArray();
        }

        /// <summary>
        /// 取得极值数据
        /// </summary>
        /// <param name="aircraft"></param>
        /// <returns></returns>
        public static FlightDataEntitiesRT.ExtremumPointInfo[]
            GetExtremumPointInfos(AircraftInstance aircraft)
        {
            AircraftService.AircraftServiceClient client = GetClient();
            var task = client.GetExtremumPointInfosByAircraftInstanceAsync(
                RTConverter.ToAircraftService(aircraft));

            task.Wait();
            client.CloseAsync();

            var items = task.Result;

            var result = from one in items
                         select RTConverter.FromAircraftService(one);

            return result.ToArray();
        }

        /// <summary>
        /// 取得极值数据
        /// </summary>
        /// <param name="aircraft"></param>
        /// <returns></returns>
        public static FlightDataEntitiesRT.ExtremumPointInfo[]
            GetExtremumPointInfos(Flight flight)
        {
            AircraftService.AircraftServiceClient client = GetClient();
            var task = client.GetExtremumReportDefinitionAsync(flight.Aircraft.AircraftModel.ModelName);
            var levelTopRecs = GetLevelTopFlightRecords(flight, null);

            task.Wait();
            client.CloseAsync();

            var definition = task.Result;
            var parames = from def in definition.Items
                          orderby def.Number ascending
                          select def.ParameterID;

            Dictionary<string, int> dic = new Dictionary<string, int>();
            foreach (var v in definition.Items)
            {
                dic.Add(v.ParameterID, v.Number);
            }

            List<ExtremumPointInfo> infs = new List<ExtremumPointInfo>();
            foreach (var r in levelTopRecs)
            {
                if (parames.Contains(r.ExtremumPointInfo.ParameterID))
                {
                    r.ExtremumPointInfo.Number = dic[r.ExtremumPointInfo.ParameterID];
                    infs.Add(r.ExtremumPointInfo);
                }
            }

            var result = from one in infs
                         orderby one.Number ascending
                         select one;

            return result.ToArray();
        }

        /// <summary>
        /// 根据机型，取得所有数据面板对象
        /// </summary>
        /// <param name="aircraftModel">当前机型，可以通过AircraftService取得</param>
        /// <returns></returns>
        public static FlightDataEntitiesRT.Charts.ChartPanel[] GetChartPanels(
            AircraftModel aircraftModel)
        {
            AircraftService.AircraftServiceClient client = GetClient();
            var task = client.GetAllChartPanelsAsync(RTConverter.ToAircraftService(aircraftModel));
            task.Wait();
            client.CloseAsync();
            var result = from one in task.Result
                         select RTConverter.FromAircraftService(one);
            return result.ToArray();
        }

        /// <summary>
        /// 取得当前机型的所有参数，可以用于曲线新增、曲线移除或者绑定曲线信息
        /// </summary>
        /// <param name="aircraftModel"></param>
        /// <returns></returns>
        public static FlightDataEntitiesRT.FlightParameters GetFlightParameters(AircraftModel aircraftModel)
        {
            AircraftService.AircraftServiceClient client = GetClient();
            var get = client.GetAllFlightParametersAsync(RTConverter.ToAircraftService(aircraftModel));
            get.Wait();
            client.CloseAsync();
            AircraftService.FlightParameters parameters = get.Result;
            return RTConverter.FromAircraftService(parameters);
        }

        public static Task<FlightDataEntitiesRT.FlightParameters> GetFlightParametersAsync(AircraftModel aircraftModel)
        {
            AircraftService.AircraftServiceClient client = GetClient();
            var get = client.GetAllFlightParametersAsync(RTConverter.ToAircraftService(aircraftModel));
            return get.ContinueWith<FlightDataEntitiesRT.FlightParameters>(
                 new Func<Task, FlightParameters>(delegate(Task t)
             {
                 if (t != null && t is Task<AircraftService.FlightParameters>)
                 {
                     return RTConverter.FromAircraftService(
                         (t as Task<AircraftService.FlightParameters>).Result);
                 }
                 return null;
             }));
        }

        private static FlightParameters ConvertToRTEntity(FlightParameters parameters)
        {

            var result2 = from re in parameters.Parameters
                          where !string.IsNullOrEmpty(re.ParameterID)//不能去掉（NULL）空值，因为可能还没数据解析
                          orderby re.Index ascending, re.SubIndex ascending
                          select new FlightDataEntitiesRT.FlightParameter()
                          {
                              Caption = re.Caption,
                              Index = re.Index,
                              ParameterDataType = re.ParameterDataType,
                              ParameterID = re.ParameterID,
                              SubIndex = re.SubIndex,
                              ByteIndexes =
                               (from one1 in re.ByteIndexes
                                select new ByteIndex()
                                {
                                    Index = one1.Index,
                                    SubIndexes = one1.SubIndexes == null ? new FlightDataEntitiesRT.BitIndex[] { } : (from one2 in one1.SubIndexes
                                                                                                                      select new BitIndex() { SubIndex = one2.SubIndex }
                                                   ).ToArray()
                                }).ToArray()
                          };

            var paras = new FlightParameters() { BytesCount = parameters.BytesCount, Parameters = result2.ToArray() };

            return paras;
        }

        /// <summary>
        /// 取得当前机型的所有判据对象，可以用于“快速判读”里面数据定位
        /// </summary>
        /// <param name="aircraftModel"></param>
        /// <returns></returns>
        public static FlightDataEntitiesRT.Decisions.Decision[] GetDecisions(AircraftModel aircraftModel)
        {
            AircraftService.AircraftServiceClient client = GetClient();
            var task = client.GetAllDecisionsAsync(RTConverter.ToAircraftService(aircraftModel));
            task.Wait();
            client.CloseAsync();
            var decisions = from one in task.Result
                            select RTConverter.FromAircraftService(one);

            List<FlightDataEntitiesRT.Decisions.Decision> tDecs = new List<FlightDataEntitiesRT.Decisions.Decision>();

            foreach (var decision in decisions)
            {
                Queue<FlightDataEntitiesRT.Decisions.SubCondition> conds
                    = new Queue<FlightDataEntitiesRT.Decisions.SubCondition>();
                foreach (var sub in decision.Conditions)
                {
                    conds.Enqueue(sub);
                }
                while (conds.Count > 0)
                {
                    var s = conds.Dequeue();
                    s.RootDecision = decision;

                    if (s.SubConditions != null)
                    {
                        foreach (var sub in s.SubConditions)
                        {
                            conds.Enqueue(sub);
                        }
                    }
                }

                tDecs.Add(decision);
            }

            return tDecs.ToArray();
            //return result2.ToArray();
        }

        /// <summary>
        /// 取得一个架次的所有判定成功记录
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        public static FlightDataEntitiesRT.Decisions.DecisionRecord[] GetDecisionRecords(Flight flight)
        {
            AircraftService.AircraftServiceClient client = GetClient();
            var task = client.GetDecisionRecordsAsync(RTConverter.ToAircraftService(flight));
            task.Wait();

            client.CloseAsync();
            var decisionRecords = from one in task.Result
                                  select RTConverter.FromAircraftService(one);

            return decisionRecords.ToArray();
        }

        public static FlightDataEntitiesRT.Decisions.FlightConditionDecision[]
            GetAllFlightConditionDecisions(FlightDataEntitiesRT.AircraftModel aircraft)
        {
            AircraftService.AircraftServiceClient client = GetClient();
            var task = client.GetAllFlightConditionDecisionsAsync(RTConverter.ToAircraftService(aircraft));
            task.Wait();
            client.CloseAsync();

            var decisionRecords = from one in task.Result
                                  select RTConverter.FromAircraftService(one);

            return decisionRecords.ToArray();
        }

        public static IEnumerable<FlightDataEntitiesRT.AircraftInstance> GetAllAircrafts(AircraftModel aircraftModel)
        {
            //AircraftService.AircraftServiceClient client = GetClient();
            //client.GetAllAircraftsAsync();

            return new FlightDataEntitiesRT.AircraftInstance[] 
            { new FlightDataEntitiesRT.AircraftInstance(){
                AircraftModel = ApplicationContext.Instance.CurrentAircraftModel ,
             AircraftNumber = "0004",
             LastUsed = DateTime.Now}
            }
             ;
        }

        public static Flight[] GetAllFlights(AircraftModel aircraftModel, AircraftInstance aircraftInstance)
        {
            AircraftService.AircraftServiceClient client = GetClient();
            var task = client.GetAllFlightsByInstanceAsync(RTConverter.ToAircraftService(aircraftModel),
                RTConverter.ToAircraftService(aircraftInstance));
            task.Wait();
            var results = task.Result;
            client.CloseAsync();

            if (results == null || results.Count() <= 0)
                return new Flight[] { };

            var result2 = from one in results
                          select RTConverter.FromAircraftService(one);
            return result2.ToArray();
        }

        public static Task<FlightRawDataRelationPoint[]> GetFlightRawDataRelationPointsAsync(
            AircraftModel aircraftModel, string FlightID, string XAxisParameterID, string YAxisParameterID)
        {
            AircraftService.AircraftServiceClient client = GetClient();
            var get = client.GetFlightRawDataRelationPointsAsync(
                RTConverter.ToAircraftService(aircraftModel), FlightID, XAxisParameterID, YAxisParameterID);
            return get.ContinueWith<FlightDataEntitiesRT.FlightRawDataRelationPoint[]>(
                 new Func<Task, FlightDataEntitiesRT.FlightRawDataRelationPoint[]>(delegate(Task t)
                 {
                     if (t != null && t is Task<AircraftService.FlightRawDataRelationPoint[]>)
                     {
                         var t2 = (t as Task<ObservableCollection<AircraftService.FlightRawDataRelationPoint>>).Result;
                         var result = from one in t2
                                      select RTConverter.FromAircraftService(one);
                         return result.ToArray();
                     }
                     return new FlightRawDataRelationPoint[] { };
                 }));

            //AircraftService.AircraftServiceClient client = GetClient();
            //var task = client.GetFlightRawDataRelationPointsAsync(RTConverter.ToAircraftService(aircraftModel), FlightID);
            //task.Wait();
            //var results = task.Result;
            //client.CloseAsync();

            //if (results == null || results.Count() <= 0)
            //    return new FlightRawDataRelationPoint[] { };

            //var result2 = from one in results
            //              select RTConverter.FromAircraftService(one);

            //return result2.ToArray();
        }

        public static FlightRawDataRelationPoint[] GetFlightRawDataRelationPoints(AircraftModel aircraftModel,
            string FlightID, string XAxisParameterID, string YAxisParameterID)
        {
            AircraftService.AircraftServiceClient client = GetClient();
            var task = client.GetFlightRawDataRelationPointsAsync(
                RTConverter.ToAircraftService(aircraftModel), FlightID, XAxisParameterID, YAxisParameterID);
            task.Wait();
            var results = task.Result;
            client.CloseAsync();

            if (results == null || results.Count() <= 0)
                return new FlightRawDataRelationPoint[] { };

            var result2 = from one in results
                          select RTConverter.FromAircraftService(one);

            return result2.ToArray();
        }
    }
}
