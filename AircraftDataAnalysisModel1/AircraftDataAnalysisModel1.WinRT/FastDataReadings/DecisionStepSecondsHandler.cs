using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftDataAnalysisWinRT.FastDataReadings
{
    class DecisionStepSecondsHandler : StepSecondsFastDataReadingStepHandler
    {
        public DecisionStepSecondsHandler(int startSecond, int endSecond, bool putIntoServer,
             AircraftDataAnalysisWinRT.DataModel.RawDataPointViewModel previewModel,
             AircraftDataAnalysisWinRT.Services.IDataReading dataReading)
            : base(startSecond, endSecond,
                putIntoServer, previewModel, dataReading)
        {
        }

        protected override string HandleOneStepData(int second, FlightDataEntitiesRT.ParameterRawData[] datas)
        {
            throw new NotImplementedException();
        }
    }
}
