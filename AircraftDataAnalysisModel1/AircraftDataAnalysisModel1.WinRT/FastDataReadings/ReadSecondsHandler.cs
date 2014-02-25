using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftDataAnalysisWinRT.FastDataReadings
{
    class ReadSecondsHandler : FastDataReadingStepHandler
    {
        public ReadSecondsHandler(int startSecond, int endSecond, bool putIntoServer,
            AircraftDataAnalysisWinRT.DataModel.RawDataPointViewModel previewModel,
            AircraftDataAnalysisWinRT.Services.IDataReading dataReading) :
            base(startSecond, endSecond, putIntoServer, previewModel, dataReading)
        {
        }

        private FlightDataEntitiesRT.IFlightRawDataExtractor m_rawDataExtractor = null;

        public FlightDataEntitiesRT.IFlightRawDataExtractor RawDataExtractor
        {
            get { return m_rawDataExtractor; }
            set { m_rawDataExtractor = value; }
        }

        protected override string DoWorkCore()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = this.m_startSecond; i < this.m_endSecond; i++)
            {
                FlightDataEntitiesRT.ParameterRawData[] datas = m_rawDataExtractor.GetDataBySecond(i);
                Parallel.ForEach(this.StepHandlers,
                    new Action<StepSecondsFastDataReadingStepHandler>(
                        delegate(StepSecondsFastDataReadingStepHandler stepHandler)
                        {
                            try
                            {
                                string result = stepHandler.OnStepData(i, datas);
                                if (!string.IsNullOrEmpty(result))
                                    builder.AppendLine(result);
                            }
                            catch (Exception ex)
                            {
                                builder.AppendLine(ex.Message + "\t\t" + ex.StackTrace);
                            }
                        }
                        ));
            }

            return builder.ToString();
        }

        public List<StepSecondsFastDataReadingStepHandler> StepHandlers { get; set; }
    }
}
