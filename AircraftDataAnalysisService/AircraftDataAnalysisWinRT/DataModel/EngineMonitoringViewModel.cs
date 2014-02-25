using FlightDataEntitiesRT;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace AircraftDataAnalysisWinRT.DataModel
{
    public class EngineMonitoringViewModel : AircraftDataAnalysisWinRT.Common.BindableBase
    {
        public EngineMonitoringViewModel()
        {
            //this.T6LViewModel = new EngineMonitoringT6LViewModel();
            //this.T6RViewModel = new EngineMonitoringT6RViewModel();
            this.FlightViewModel = new FlightSelectViewModel();
        }

        private bool m_ist6lSelected = true;

        public bool IsT6LSelected
        {
            get { return m_ist6lSelected; }
            set
            {
                m_ist6lSelected = value;
                this.OnPropertyChanged("IsT6LSelected");
                this.OnPropertyChanged("IsT6RSelected");
            }
        }

        public bool IsT6RSelected
        {
            get
            {
                return !m_ist6lSelected;
            }
            set
            {
                m_ist6lSelected = !value;
                this.OnPropertyChanged("IsT6LSelected");
                this.OnPropertyChanged("IsT6RSelected");
            }
        }

        private EngineMonitoringT6LViewModel m_t6LModel = null;

        private EngineMonitoringT6RViewModel m_t6RModel = null;

        private FlightSelectViewModel m_flightModel = null;

        public EngineMonitoringT6LViewModel T6LViewModel
        {
            get
            {
                return m_t6LModel;
            }
            set
            {
                this.SetProperty<EngineMonitoringT6LViewModel>(ref m_t6LModel, value);
            }
        }

        public EngineMonitoringT6RViewModel T6RViewModel
        {
            get
            {
                return m_t6RModel;
            }
            set
            {
                this.SetProperty<EngineMonitoringT6RViewModel>(ref m_t6RModel, value);
            }
        }

        public FlightSelectViewModel FlightViewModel
        {
            get
            {
                return m_flightModel;
            }
            set
            {
                this.SetProperty<FlightSelectViewModel>(ref m_flightModel, value);
            }
        }

        public string AddItems(IEnumerable<FlightRawDataRelationPoint> flightRawDataRelationPoint,
            string flightID, bool isT6LSelected)
        {
            if (flightRawDataRelationPoint == null || flightRawDataRelationPoint.Count() == 0
                || string.IsNullOrEmpty(flightID))
                return string.Empty;

            if (m_ItemsMap.ContainsKey(flightID))
                this.m_ItemsMap[flightID] = new EngineMonitoringOneViewModel(flightID, isT6LSelected, flightRawDataRelationPoint);
            else m_ItemsMap.Add(flightID, new EngineMonitoringOneViewModel(flightID, isT6LSelected, flightRawDataRelationPoint));

            return flightID;
        }

        private Dictionary<string, EngineMonitoringOneViewModel> m_ItemsMap = new Dictionary<string, EngineMonitoringOneViewModel>();
        public Dictionary<string, EngineMonitoringOneViewModel> ItemsMap
        {
            get { return m_ItemsMap; }
            set { m_ItemsMap = value; }
        }
    }

    public class FlightSelectViewModel : ObservableCollection<FlightSelectItem>
    {
        public bool? IsAllSelected
        {
            get
            {
                bool? prev = null;
                foreach (var item in this.Items)
                {
                    if (prev == null)
                        prev = item.IsSelected;
                    else if (prev.Value == true)
                    {
                        if (item.IsSelected == false)
                            return null;
                    }
                    else if (prev.Value == false)
                    {
                        if (item.IsSelected == true)
                            return null;
                    }
                }

                return prev;
            }
            set
            {
                if (value == null)
                    return;

                foreach (var item in this.Items)
                {
                    item.IsSelected = value.Value;
                }

                this.OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("IsAllSelected"));
            }
        }
    }

    public class FlightSelectItem : AircraftDataAnalysisWinRT.Common.BindableBase
    {
        private Flight m_flight;

        public Flight Flight
        {
            get { return m_flight; }
            set { m_flight = value; }
        }

        public FlightSelectItem(Flight flight)
        {
            m_flight = flight;
        }

        public string FlightName
        {
            get
            {
                return m_flight.FlightName;
            }
        }

        private bool m_isSelected = false;

        public bool IsSelected
        {
            get
            {
                return m_isSelected;
            }
            set
            {
                this.SetProperty<bool>(ref m_isSelected, value);
            }
        }
    }

    /// <summary>
    /// 大气总温、左发排气温度
    /// </summary>
    public class EngineMonitoringOneViewModel
    {
        private string flightID;
        public string FlightID
        {
            get { return flightID; }
            set { flightID = value; }
        }

        private EngineMonitoringT6LViewModel m_t6LViewModel = new EngineMonitoringT6LViewModel();

        public EngineMonitoringT6LViewModel T6LViewModel
        {
            get { return m_t6LViewModel; }
            set { m_t6LViewModel = value; }
        }

        private EngineMonitoringT6RViewModel m_t6RViewModel = new EngineMonitoringT6RViewModel();

        public EngineMonitoringT6RViewModel T6RViewModel
        {
            get { return m_t6RViewModel; }
            set { m_t6RViewModel = value; }
        }

        public EngineMonitoringOneViewModel(string flightID, bool isT6LSelected,
            IEnumerable<FlightRawDataRelationPoint> flightRawDataRelationPoint)
        {
            // TODO: Complete member initialization
            this.flightID = flightID;

            if (flightRawDataRelationPoint != null && flightRawDataRelationPoint.Count() > 0)
            {
                foreach (var pt in flightRawDataRelationPoint)
                {
                    if (pt.FlightID == this.FlightID)
                    {
                        var xAxisParameterID = pt.XAxisParameterID;
                        var yAxisParameterID = pt.YAxisParameterID;

                        if (isT6LSelected && xAxisParameterID == "Tt" && yAxisParameterID == "T6L")
                        {
                            this.T6LViewModel.Add(new EngineMonitoringT6LItem()
                            {
                                XValue = pt.XAxisParameterValue,
                                YValue = pt.YAxisParameterValue
                            });
                        }
                        else if (xAxisParameterID == "Tt" && yAxisParameterID == "T6R")
                        {
                            this.T6RViewModel.Add(new EngineMonitoringT6RItem()
                            {
                                XValue = pt.XAxisParameterValue,
                                YValue = pt.YAxisParameterValue
                            });

                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 大气总温、左发排气温度
    /// </summary>
    public class EngineMonitoringT6LViewModel : ObservableCollection<EngineMonitoringT6LItem>
    {
        public EngineMonitoringT6LViewModel()
        {
            //Random rand = new Random();
            //int xvalue = 24;
            //int yvalue = 50;
            ////debug 
            //for (int i = 0; i < 10000; i++)
            //{
            //    double xval = xvalue + rand.NextDouble() * 20.0;
            //    double yval = yvalue + rand.NextDouble() * 100;
            //    this.Add(new EngineMonitoringT6LItem() { XValue = xval, YValue = yval });
            //}

        }
    }

    /// <summary>
    /// 大气总温、右发排气温度
    /// </summary>
    public class EngineMonitoringT6RViewModel : ObservableCollection<EngineMonitoringT6RItem>
    {
        public EngineMonitoringT6RViewModel()
        {
            //    Random rand = new Random();
            //    int xvalue = 33;
            //    int yvalue = 60;
            //    //debug 
            //    for (int i = 0; i < 10000; i++)
            //    {
            //        double xval = xvalue + rand.NextDouble() * 20.0;
            //        double yval = yvalue + rand.NextDouble() * 100;
            //        this.Add(new EngineMonitoringT6RItem() { XValue = xval, YValue = yval });
            //    }
        }
    }

    /// <summary>
    /// 大气总温、左发排气温度
    /// </summary>
    public class EngineMonitoringT6LItem : AircraftDataAnalysisWinRT.Common.BindableBase
    {
        private double m_XValue;
        private double m_YValue;

        /// <summary>
        /// 大气总温	Tt
        /// </summary>
        public double XValue
        {
            get
            {
                return m_XValue;
            }
            set
            {
                this.SetProperty<double>(ref m_XValue, value);
            }
        }

        /// <summary>
        /// 左发排气温度	T6L
        /// </summary>
        public double YValue
        {
            get
            {
                return m_YValue;
            }
            set
            {
                this.SetProperty<double>(ref m_YValue, value);
            }
        }
    }

    /// <summary>
    /// 大气总温、右发排气温度
    /// </summary>
    public class EngineMonitoringT6RItem : AircraftDataAnalysisWinRT.Common.BindableBase
    {
        private double m_XValue;
        private double m_YValue;

        /// <summary>
        /// 大气总温	Tt
        /// </summary>
        public double XValue
        {
            get
            {
                return m_XValue;
            }
            set
            {
                this.SetProperty<double>(ref m_XValue, value);
            }
        }

        /// <summary>
        /// 右发排气温度	T6R
        /// </summary>
        public double YValue
        {
            get
            {
                return m_YValue;
            }
            set
            {
                this.SetProperty<double>(ref m_YValue, value);
            }
        }
    }
}
