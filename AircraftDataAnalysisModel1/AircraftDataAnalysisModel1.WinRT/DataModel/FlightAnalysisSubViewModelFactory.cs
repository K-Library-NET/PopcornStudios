using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftDataAnalysisWinRT.DataModel
{
    class FlightAnalysisSubViewModelFactory
    {
        internal static FlightAnalysisSubViewModel Create(
            AircraftDataAnalysisWinRT.Domain.FlightAnalysisViewModelOld HostViewModel,
            FlightDataEntitiesRT.Flight flight, string hostParameterID)
        {
            FlightAnalysisSubViewModel viewModel = new FlightAnalysisSubViewModel() { ViewModel = HostViewModel };
            viewModel.HostParameterID = hostParameterID;
            viewModel.CurrentStartSecond = flight.StartSecond;
            viewModel.CurrentEndSecond = flight.EndSecond;

            var dataEntities = AircraftDataAnalysisWinRT.Services.ServerHelper.GetData(flight, new string[] { hostParameterID },
                flight.StartSecond, flight.EndSecond);

            return viewModel;
        }
    }
}
