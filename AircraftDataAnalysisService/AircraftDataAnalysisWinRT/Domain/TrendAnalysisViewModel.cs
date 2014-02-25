using AircraftDataAnalysisWinRT.DataModel;
using FlightDataEntitiesRT;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftDataAnalysisWinRT.DataModel
{
    public class TrendAnalysisViewModel : AircraftDataAnalysisWinRT.Common.BindableBase
    {
        public TrendAnalysisViewModel()
        {
            /*
            //debug 
            this.Add(new TrendAnalysisItem()
            {
                FlightDateTime = 20131020,
                Hp = 256,
                Vi = 256,
                Tt = 256,
            });
            this.Add(new TrendAnalysisItem()
            {
                FlightDateTime = 20131122,
                Hp = 128,
                Vi = 128,
                Tt = 128,
            });
            this.Add(new TrendAnalysisItem()
            {
                FlightDateTime = 20131203,
                Hp = 192,
                Vi = 192,
                Tt = 192,
            });
            this.Add(new TrendAnalysisItem()
            {
                FlightDateTime = 20131224,
                Hp = 256,
                Vi = 256,
                Tt = 256,
            });
            this.Add(new TrendAnalysisItem()
            {
                FlightDateTime = 20131225,
                Hp = 96,
                Vi = 96,
                Tt = 96,
            });
            this.Add(new TrendAnalysisItem()
            {
                FlightDateTime = 20140106,
                Hp = 277,
                Vi = 277,
                Tt = 277,
            }); */
        }

        public Dictionary<string, List<ExtremumPointInfo>> ItemsMap = new Dictionary<string, List<ExtremumPointInfo>>();


        private TrendAnalysisSubViewModel m_ttCollection = null;

        public TrendAnalysisSubViewModel TtCollection
        {
            get { return m_ttCollection; }
            set
            {
                this.SetProperty<TrendAnalysisSubViewModel>(ref m_ttCollection, value);
            }
        }



        private TrendAnalysisSubViewModel m_hpCollection = null;

        public TrendAnalysisSubViewModel HpCollection
        {
            get { return m_hpCollection; }
            set
            {
                this.SetProperty<TrendAnalysisSubViewModel>(ref m_hpCollection, value);
            }
        }



        private TrendAnalysisSubViewModel m_viCollection = null;

        public TrendAnalysisSubViewModel ViCollection
        {
            get { return m_viCollection; }
            set
            {
                this.SetProperty<TrendAnalysisSubViewModel>(ref m_viCollection, value);
            }
        }



        private TrendAnalysisSubViewModel m_nhCollection = null;

        public TrendAnalysisSubViewModel NHCollection
        {
            get { return m_nhCollection; }
            set
            {
                this.SetProperty<TrendAnalysisSubViewModel>(ref m_nhCollection, value);
            }
        }



        private TrendAnalysisSubViewModel m_t6Collection = null;

        public TrendAnalysisSubViewModel T6Collection
        {
            get { return m_t6Collection; }
            set
            {
                this.SetProperty<TrendAnalysisSubViewModel>(ref m_t6Collection, value);
            }
        }



        private TrendAnalysisSubViewModel m_kbCollection = null;

        public TrendAnalysisSubViewModel KBCollection
        {
            get { return m_kbCollection; }
            set
            {
                this.SetProperty<TrendAnalysisSubViewModel>(ref m_kbCollection, value);
            }
        }



        private TrendAnalysisSubViewModel m_nCollection = null;

        public TrendAnalysisSubViewModel NCollection
        {
            get { return m_nCollection; }
            set
            {
                this.SetProperty<TrendAnalysisSubViewModel>(ref m_nCollection, value);
            }
        }



        //private TrendAnalysisSubViewModel m_ttCollection = null;

        //public TrendAnalysisSubViewModel TtCollection
        //{
        //    get { return m_ttCollection; }
        //    set
        //    {
        //        this.SetProperty<TrendAnalysisSubViewModel>(ref m_ttCollection, value);
        //    }
        //}

        public ObservableCollection<FlightDataEntitiesRT.AircraftInstance> AircraftInstances { get; set; }
    }

    internal class AircraftInstanceViewModel
    {
        public AircraftInstanceViewModel(AircraftInstance instance)
        {
            this.m_instance = instance;
        }

        public AircraftInstance m_instance { get; set; }

        public string FlightName
        {
            get
            {
                return m_instance.AircraftNumber;
            }
        }

        private bool m_isSelected = false;

        public bool IsSelected
        {
            get
            {
                return m_isSelected;
            }
        }
    }
}
