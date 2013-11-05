using AircraftDataAnalysisWinRT.Common;
using AircraftDataAnalysisWinRT.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftDataAnalysisWinRT.DataModel
{
    public class ExtremumReportViewModel : BindableBase
    {
        public ExtremumReportViewModel()
        {
            var infos = ServerHelper.GetExtremumPointInfos(ApplicationContext.Instance.CurrentFlight);

            this.FlightParameters = ApplicationContext.Instance.GetFlightParameters(
                ApplicationContext.Instance.CurrentAircraftModel);

            this.InitInfoWrappers(infos);
        }

        private void InitInfoWrappers(FlightDataEntitiesRT.ExtremumPointInfo[] infos)
        {
            var vinfos = from inf in infos
                         select new ExtremumReportItemWrap(this, inf);

            this.Collection = new ObservableCollection<ExtremumReportItemWrap>(vinfos);
        }

        private ObservableCollection<ExtremumReportItemWrap> m_collection
            = new ObservableCollection<ExtremumReportItemWrap>();

        private FlightDataEntitiesRT.FlightParameters m_flightParameters;

        public FlightDataEntitiesRT.FlightParameters FlightParameters
        {
            get { return m_flightParameters; }
            set { this.SetProperty<FlightDataEntitiesRT.FlightParameters>(ref m_flightParameters, value); }
        }

        public ObservableCollection<ExtremumReportItemWrap> Collection
        {
            get { return m_collection; }
            set
            {
                this.SetProperty<ObservableCollection<ExtremumReportItemWrap>>(ref m_collection,
                value);
            }
        }
    }

    public class ExtremumReportItemWrap : BindableBase
    {
        private FlightDataEntitiesRT.ExtremumPointInfo extremumInfo;
        private ExtremumReportViewModel viewModel;

        public ExtremumReportItemWrap(ExtremumReportViewModel viewModel,
            FlightDataEntitiesRT.ExtremumPointInfo info)
        {
            this.viewModel = viewModel;
            this.extremumInfo = info;

            var para = this.viewModel.FlightParameters.Parameters.FirstOrDefault(
                new Func<FlightDataEntitiesRT.FlightParameter, bool>(delegate(FlightDataEntitiesRT.FlightParameter p)
                    {
                        if (p.ParameterID == info.ParameterID)
                            return true;
                        return false;
                    }));

            if (para != null)
                this.ParameterCaption = para.Caption;
        }

        public int Number
        {
            get
            {
                return this.extremumInfo.Number;
            }
        }

        public string ParameterID
        {
            get
            {
                if (this.extremumInfo != null)
                    return this.extremumInfo.ParameterID;
                return string.Empty;
            }
        }

        private string m_parameterCaption = string.Empty;

        public string ParameterCaption
        {
            get
            {
                return m_parameterCaption;
            }
            set
            {
                this.SetProperty(ref m_parameterCaption, value);
            }
        }

        public float MaxValue
        {
            get
            {
                return this.extremumInfo.MaxValue;
            }
        }

        public float MinValue
        {
            get
            {
                return this.extremumInfo.MinValue;
            }
        }

        public int MaxValueSecond
        {
            get
            {
                return Convert.ToInt32(this.extremumInfo.MaxValueSecond);
            }
        }

        public int MinValueSecond
        {
            get
            {
                return Convert.ToInt32(this.extremumInfo.MinValueSecond);
            }
        }

        public TimeSpan MaxValueTimeSpan
        {
            get
            {
                return new TimeSpan(0, 0, Convert.ToInt32(this.extremumInfo.MaxValueSecond));
            }
        }

        public TimeSpan MinValueTimeSpan
        {
            get
            {
                return new TimeSpan(0, 0, Convert.ToInt32(this.extremumInfo.MinValueSecond));
            }
        }
    }
}
