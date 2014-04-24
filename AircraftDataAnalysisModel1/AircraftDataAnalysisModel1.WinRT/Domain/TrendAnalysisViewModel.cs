using AircraftDataAnalysisWinRT.DataModel;
using FlightDataEntitiesRT;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

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

            this.m_hpCollection = new TrendAnalysisSubViewModel();
            this.m_kbCollection = new TrendAnalysisSubViewModel();
            this.m_nCollection = new TrendAnalysisSubViewModel();
            this.m_nhCollection = new TrendAnalysisSubViewModel();
            this.m_t6Collection = new TrendAnalysisSubViewModel();
            this.m_ttCollection = new TrendAnalysisSubViewModel();
            this.m_viCollection = new TrendAnalysisSubViewModel();
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

        public Brush T6LBrush
        {
            get
            {
                return Styles.AircraftDataAnalysisGlobalPallete.Brushes[0];
            }
        }

        public Brush T6RBrush
        {
            get
            {
                return Styles.AircraftDataAnalysisGlobalPallete.Brushes[1];
            }
        }

        public Brush NHLBrush
        {
            get
            {
                return Styles.AircraftDataAnalysisGlobalPallete.Brushes[2];
            }
        }

        public Brush NHRBrush
        {
            get
            {
                return Styles.AircraftDataAnalysisGlobalPallete.Brushes[3];
            }
        }

        public Brush ViBrush
        {
            get
            {
                return Styles.AircraftDataAnalysisGlobalPallete.Brushes[4];
            }
        }

        public Brush HpBrush
        {
            get
            {
                return Styles.AircraftDataAnalysisGlobalPallete.Brushes[5];
            }
        }

        public Brush KZBBrush
        {
            get
            {
                return Styles.AircraftDataAnalysisGlobalPallete.Brushes[6];
            }
        }

        public Brush KCBBrush
        {
            get
            {
                return Styles.AircraftDataAnalysisGlobalPallete.Brushes[7];
            }
        }

        public Brush NyBrush
        {
            get
            {
                return Styles.AircraftDataAnalysisGlobalPallete.Brushes[8];
            }
        }

        public Brush NxBrush
        {
            get
            {
                return Styles.AircraftDataAnalysisGlobalPallete.Brushes[9];
            }
        }

        public Brush NzBrush
        {
            get
            {
                return Styles.AircraftDataAnalysisGlobalPallete.Brushes[0];
            }
        }

        public Brush TtBrush
        {
            get
            {
                return Styles.AircraftDataAnalysisGlobalPallete.Brushes[1];
            }
        }

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
