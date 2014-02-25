using AircraftDataAnalysisWinRT.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftDataAnalysisWinRT.DataModel
{
    public class TrendAnalysisItem : BindableBase
    {
        public TrendAnalysisItem()
            : base()
        {

        }

        public string AircraftNumber
        {
            get;
            set;
        }

        private DateTime m_flightDateTime;

        /// <summary>
        /// 飞行时间
        /// </summary>
        public DateTime FlightDateTime
        {
            get { return m_flightDateTime; }
            set { this.SetProperty<DateTime>(ref m_flightDateTime, value); }
        }

        private double m_Value = 0;

        /// <summary>
        /// 最大值
        /// </summary>
        public double Value
        {
            get { return m_Value; }
            set { this.SetProperty<double>(ref m_Value, value); }
        }

        private double m_hp;

        public double Hp
        {
            get { return m_hp; }
            set
            {
                this.SetProperty<double>(ref m_hp, value);
            }
        }

        private double m_vi;

        public double Vi
        {
            get { return m_vi; }
            set
            {
                this.SetProperty<double>(ref m_vi, value);
            }
        }

        private double m_tt;

        public double Tt
        {
            get { return m_tt; }
            set
            {
                this.SetProperty<double>(ref m_tt, value);
            }
        }


        private double m_KZB;

        public double KZB
        {
            get { return m_KZB; }
            set
            {
                this.SetProperty<double>(ref m_KZB, value);
            }
        }


        private double m_KCB;

        public double KCB
        {
            get { return m_KCB; }
            set
            {
                this.SetProperty<double>(ref m_KCB, value);
            }
        }


        private double m_Ny;

        public double Ny
        {
            get { return m_Ny; }
            set
            {
                this.SetProperty<double>(ref m_Ny, value);
            }
        }

        private double m_Nx;

        public double Nx
        {
            get { return m_Nx; }
            set
            {
                this.SetProperty<double>(ref m_Nx, value);
            }
        }

        private double m_Nz;

        public double Nz
        {
            get { return m_Nz; }
            set
            {
                this.SetProperty<double>(ref m_Nz, value);
            }
        }

        private double m_T6L;

        public double T6L
        {
            get { return m_T6L; }
            set
            {
                this.SetProperty<double>(ref m_T6L, value);
            }
        }


        private double m_T6R;

        public double T6R
        {
            get { return m_T6R; }
            set
            {
                this.SetProperty<double>(ref m_T6R, value);
            }
        }

        private double m_NHL;

        public double NHL
        {
            get { return m_NHL; }
            set
            {
                this.SetProperty<double>(ref m_NHL, value);
            }
        }

        private double m_NHR;

        public double NHR
        {
            get { return m_NHR; }
            set
            {
                this.SetProperty<double>(ref m_NHR, value);
            }
        }

        public static string[] Fields
        {
            get
            {
                return new string[] { "NHL", "NHR", "T6L", "T6R", "Nz", "Nx", "Ny", "KZB", "KCB", "Tt", "Vi", "Hp" };
            }
        }
    }

    public class TrendAnalysisSubViewModel : System.Collections.ObjectModel.ObservableCollection<TrendAnalysisItem>
    {
        public TrendAnalysisSubViewModel()
        {
            //debug 
            /* this.Add(new TrendAnalysisItem()
             {
                 FlightDateTime = 20131020,
                 Value = 256
             });
             this.Add(new TrendAnalysisItem()
             {
                 FlightDateTime = 20131122,
                 Value = 128
             });
             this.Add(new TrendAnalysisItem()
             {
                 FlightDateTime = 20131203,
                 Value = 192
             });
             this.Add(new TrendAnalysisItem()
             {
                 FlightDateTime = 20131224,
                 Value = 256
             });
             this.Add(new TrendAnalysisItem()
             {
                 FlightDateTime = 20131225,
                 Value = 96
             });
             this.Add(new TrendAnalysisItem()
             {
                 FlightDateTime = 20140106,
                 Value = 277
             });

             Random ran = new Random();

             for (int i = 0; i < 1000000; i++)
             {
                 //this.TtCollection.Add(new TrendAnalysisItem()
                 //{
                 //    FlightDateTime = "20131122",
                 //    Value = 128
                 //});
                 //this.TtCollection.Add(new TrendAnalysisItem()
                 //{
                 //    FlightDateTime = "20131203",
                 //    Value = 192
                 //});
                 //this.TtCollection.Add(new TrendAnalysisItem()
                 //{
                 //    FlightDateTime = "20131224",
                 //    Value = 256
                 //});
                 //this.TtCollection.Add(new TrendAnalysisItem()
                 //{
                 //    FlightDateTime = "20131225",
                 //    Value = 96
                 //});
                 //this.TtCollection.Add(new TrendAnalysisItem()
                 //{
                 //    FlightDateTime = "20140106",
                 //    Value = 277
                 //});
             } */
        }

        public TrendAnalysisSubViewModel(IEnumerable<TrendAnalysisItem> items)
            : base(items)
        {

        }
    }
    /// <summary>
    /// Simple storage class for pair of string and double value
    /// </summary>
    public class SimpleDataPoint
    {
        public double Value { get; set; }
        public string Label { get; set; }
    }

}
