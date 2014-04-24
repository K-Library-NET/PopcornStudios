using System;
namespace AircraftDataAnalysisModel1.WinRT.Domain
{
    public interface IDataLoaderNavigator
    {
        AircraftAnalysisDataLoader DataLoader { get; set; }
    }
}
