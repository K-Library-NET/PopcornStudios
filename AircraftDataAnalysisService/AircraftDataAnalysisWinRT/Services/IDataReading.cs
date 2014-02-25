using System;
namespace AircraftDataAnalysisWinRT.Services
{
    /// <summary>
    /// 数据读取处理接口
    /// </summary>
    public interface IDataReading: Windows.Foundation.IAsyncActionWithProgress<int>
    {
        void Cancel();
        void Close();
        global::Windows.Foundation.AsyncActionWithProgressCompletedHandler<int> Completed { get; set; }
        int DataReductionSecondGap { get; set; }
        Exception ErrorCode { get; }
        FlightDataEntitiesRT.Flight Flight { get; set; }
        void GetResults();
        FlightDataEntitiesRT.FlightDataHeader Header { get; set; }
        FlightDataEntitiesRT.FlightParameters Parameters { get; set; }
        double PercentCurrent { get; set; }
        global::Windows.Foundation.AsyncActionProgressHandler<int> Progress { get; set; }
        FlightDataEntitiesRT.IFlightRawDataExtractor RawDataExtractor { get; set; }
        void ReadData();
        void ReadData(int startSecond, int endSecond, bool putIntoServer, AircraftDataAnalysisWinRT.DataModel.RawDataPointViewModel previewModel);
        System.Threading.Tasks.Task ReadDataAsync();
        void ReadHeader();
        System.Threading.Tasks.Task ReadHeaderAsync();
        global::Windows.Foundation.AsyncStatus Status { get; }
    }
}
