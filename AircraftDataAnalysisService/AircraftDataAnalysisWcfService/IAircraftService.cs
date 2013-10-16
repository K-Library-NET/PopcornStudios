using FlightDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AircraftDataAnalysisWcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IAircraftService”。
    [ServiceContract]
    public interface IAircraftService
    {
        /// <summary>
        /// 通过机型，取得所有曲线面板
        /// </summary>
        /// <param name="aircraftModel">机型</param>
        /// <returns></returns>
        [OperationContract]
        FlightDataEntities.Charts.ChartPanel[] GetAllChartPanels(AircraftModel aircraftModel);

        /// <summary>
        /// 取得最顶层数据
        /// </summary>
        /// <param name="flight">架次</param>
        /// <param name="parameterIds">参数ID列表，如果传入空或者空数组，则返回该架次的全部</param>
        /// <param name="withLevel1Data">是否包含最底层数据，如果包含，数据量可能很大</param>
        /// <returns></returns>
        [OperationContract]
        FlightDataEntities.LevelTopFlightRecord[] GetLevelTopFlightRecords(Flight flight,
            string[] parameterIds, bool withLevel1Data);

        /// <summary>
        /// 保存判据记录
        /// </summary>
        /// <param name="flight">架次</param>
        /// <param name="records">如果原有架次记录存在，则删除再增加</param>
        /// <returns></returns>
        [OperationContract]
        string AddOrUpdateDecisionRecords(Flight flight, FlightDataEntities.DecisionRecord[] records);

        //[OperationContract]
        //string DeleteAircraft(string aircraftModel);

        /// <summary>
        /// 取得架次（通过GetCurrentAircraftModel可以获得）对应的参数列表
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        FlightParameters GetAllFlightParameters(AircraftModel aircraftModel);

        /// <summary>
        /// 取得明细数据
        /// </summary>
        /// <param name="flight">架次</param>
        /// <param name="parameterIds">参数ID</param>
        /// <param name="startSecond">起始秒（0秒钟开始）</param>
        /// <param name="endSecond">结束秒</param>
        /// <returns></returns>
        [OperationContract]
        KeyValuePair<string, FlightDataEntities.FlightRawData[]>[] GetFlightData(Flight flight,
            string[] parameterIds, int startSecond, int endSecond);

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="flight">架次</param>
        /// <param name="batchData">批量数据，已经在客户端整理好</param>
        /// <returns></returns>
        [OperationContract]
        string InsertRawDataBatch(Flight flight, Level1FlightRecord[] batchData);

        /// <summary>
        /// 取得当前服务端配置的机型信息（对客户端来说基本算写死的常量）
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        AircraftModel GetCurrentAircraftModel();
    }
}
