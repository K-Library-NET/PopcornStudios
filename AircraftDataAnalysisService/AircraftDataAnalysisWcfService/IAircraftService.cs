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
        [OperationContract]
        void DoWork();

        //[OperationContract]
        //FlightDataEntities.AircraftModel[] GetAllAircraftModels();

        //[OperationContract]
        //string AddOrUpdateAircraftModel(FlightDataEntities.AircraftModel aircraftModel);

        //[OperationContract]
        //string DeleteAircraft(string aircraftModel);

        [OperationContract]
        FlightParameters GetAllFlightParameters();

        //[OperationContract]
        //FlightParameter[] GetAllFlightParameters(string modelName);

        [OperationContract]
        string InsertRawDataBatch(RawDataBatch batchData);

        [OperationContract]
        AircraftModel GetCurrentAircraftModel();
    }
}
