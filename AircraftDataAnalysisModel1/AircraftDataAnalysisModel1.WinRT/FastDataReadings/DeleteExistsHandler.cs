using AircraftDataAnalysisWinRT.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftDataAnalysisWinRT.FastDataReadings
{
    class DeleteExistsHandler : FastDataReadingStepHandler
    {
        public DeleteExistsHandler(int startSecond, int endSecond, bool putIntoServer,
            AircraftDataAnalysisWinRT.DataModel.RawDataPointViewModel previewModel,
            AircraftDataAnalysisWinRT.Services.IDataReading dataReading) :
            base(startSecond, endSecond, putIntoServer, previewModel, dataReading)
        {
        }

        protected override string DoWorkCore()
        {
            if (!this.m_putIntoServer)
                return string.Empty;

            try
            {
                DataInputHelper.DeleteExistsData(this.CurrentFlight);
                DataInputHelper.AddOrReplaceFlightAsync(this.CurrentFlight);//需要先新增或者更新Flight
            }
            catch (Exception e)
            {
                return e.Message + "\r\n" + e.StackTrace;
            }

            return string.Empty;
        }
    }
}
