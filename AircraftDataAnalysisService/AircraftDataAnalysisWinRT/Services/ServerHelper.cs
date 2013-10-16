using FlightDataEntitiesRT;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static AircraftService.AircraftModel GetCurrentAircraftModel()
        {
            AircraftService.AircraftServiceClient client = new AircraftService.AircraftServiceClient();
            var result = client.GetCurrentAircraftModelAsync();
            result.Wait();
            return result.Result;
        }

        /// <summary>
        /// 取得当前的机型（异步方法）
        /// </summary>
        /// <returns></returns>
        public static Task<AircraftService.AircraftModel> GetCurrentAircraftModelAsync()
        {
            AircraftService.AircraftServiceClient client = new AircraftService.AircraftServiceClient();
            return client.GetCurrentAircraftModelAsync();
            //result.Wait();
            //return result.Result;
        }

        /// <summary>
        /// 取得该机型的所有架次
        /// </summary>
        /// <param name="aircraftModel"></param>
        /// <returns></returns>
        public static AircraftService.Flight[] GetAllFlights(AircraftService.AircraftModel aircraftModel)
        {
            throw new NotImplementedException();
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
        public static FlightDataEntitiesRT.DataTable GetData(AircraftService.Flight flight,
            string[] parameterIds, int startSecond, int endSecond)
        {
            ObservableCollection<string> collection = null;
            if (parameterIds != null && parameterIds.Length > 0)
                collection = new ObservableCollection<string>(parameterIds);
            AircraftService.AircraftServiceClient client = new AircraftService.AircraftServiceClient();

            var task = client.GetFlightDataAsync(flight, collection, startSecond, endSecond);
            task.Wait();

            ObservableCollection<KeyValuePair<string, ObservableCollection<AircraftService.FlightRawData>>>
                result = task.Result;

            DataTable dt = BuildDataTable(result);

            return dt;
        }

        /// <summary>
        /// 根据记录创建DataTable方法
        /// 目前是秒值是行，列是参数名称，因为列不能太多（超过255列？）
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private static DataTable BuildDataTable(
            ObservableCollection<KeyValuePair<string, ObservableCollection<AircraftService.FlightRawData>>> result)
        {
            DataTable dt = new DataTable();
            //all column first
            dt.Columns.Add(new DataColumn() { ColumnName = "Second", DataType = DataTypeConverter.TypeInt32, Caption = "秒" });

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
                        dataRowMap.Add(data.Second, dt.NewRow());
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

        /// <summary>
        /// 取得极值数据
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        public static FlightDataEntitiesRT.ExtremumPointInfo[] GetExtremumPointInfos(AircraftService.Flight flight)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 根据机型，取得所有数据面板对象
        /// </summary>
        /// <param name="aircraftModel">当前机型，可以通过AircraftService取得</param>
        /// <returns></returns>
        public static FlightDataEntitiesRT.Charts.ChartPanel[] GetChartPanels(
            AircraftService.AircraftModel aircraftModel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 取得当前机型的所有参数，可以用于曲线新增、曲线移除或者绑定曲线信息
        /// </summary>
        /// <param name="aircraftModel"></param>
        /// <returns></returns>
        public static FlightDataEntitiesRT.FlightParameters GetFlightParameters(AircraftService.AircraftModel aircraftModel)
        {
            AircraftService.AircraftServiceClient client = new AircraftService.AircraftServiceClient();
            var get = client.GetAllFlightParametersAsync(aircraftModel);
            get.Wait();
            AircraftService.FlightParameters parameters = get.Result;

            return ConvertToRTEntity(parameters);
        }

        private static FlightParameters ConvertToRTEntity(AircraftService.FlightParameters parameters)
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
        public static FlightDataEntitiesRT.Decisions.Decision[] GetDecisions(AircraftService.AircraftModel aircraftModel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 取得一个架次的所有判定成功记录
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        public static FlightDataEntitiesRT.Decisions.DecisionRecord[] GetDecisionRecords(AircraftService.Flight flight)
        {
            throw new NotImplementedException();
        }
    }
}
