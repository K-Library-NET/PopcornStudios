using FlightDataEntitiesRT;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AircraftDataAnalysisWinRT.DataModel
{
    /// <summary>
    /// 按照日期构造树形
    /// </summary>
    public class FlightDateTreeViewModel : AircraftDataAnalysisWinRT.Common.BindableBase
    {
        private IEnumerable<Flight> m_flights;

        public FlightDateTreeViewModel(IEnumerable<Flight> flights)
            : base()
        {
            m_flights = flights;
            this.InitFlights();
        }

        private void InitFlights()
        {
            if (m_flights != null && m_flights.Count() > 0)
            {
                var dates = from one in m_flights
                            group one by one.FlightDate.ToString("yyyy-MM") into gp
                            orderby gp.Key descending
                            select gp;
                foreach (var gpdt in dates)
                {
                    IEnumerable<IFlightTreeNode> flightNodes = from one in gpdt
                                                               select new FlightViewNode(one) as IFlightTreeNode;

                    FlightGroupNode dateNode = new FlightGroupNode()
                    {
                        Caption = gpdt.Key,
                        Children = new ObservableCollection<IFlightTreeNode>(flightNodes)
                    };
                    this.m_nodes.Add(dateNode);
                }
            }
        }

        private ObservableCollection<IFlightTreeNode> m_nodes = new ObservableCollection<IFlightTreeNode>();

        public ObservableCollection<IFlightTreeNode> Nodes
        {
            get
            {
                return m_nodes;
            }
        }
    }

    public class FlightAircraftInstanceTreeViewModel
    {
        private IEnumerable<Flight> m_flights;

        public FlightAircraftInstanceTreeViewModel(IEnumerable<Flight> flights)
            : base()
        {
            m_flights = flights;
            this.InitFlights();
        }

        private void InitFlights()
        {
            if (m_flights != null && m_flights.Count() > 0)
            {
                var dates = from one in m_flights
                            group one by one.Aircraft.AircraftNumber into gp
                            //orderby gp.Key descending
                            select gp;
                foreach (var gpdt in dates)
                {
                    IEnumerable<IFlightTreeNode> flightNodes = from one in gpdt
                                                               select new FlightViewNode(one) as IFlightTreeNode;

                    FlightGroupNode dateNode = new FlightGroupNode()
                    {
                        Caption = gpdt.Key,
                        Children = new ObservableCollection<IFlightTreeNode>(flightNodes)
                    };
                    this.m_nodes.Add(dateNode);
                }
            }
        }

        private ObservableCollection<IFlightTreeNode> m_nodes = new ObservableCollection<IFlightTreeNode>();

        public ObservableCollection<IFlightTreeNode> Nodes
        {
            get
            {
                return m_nodes;
            }
        }
    }

    public class FlightGroupNode : AircraftDataAnalysisWinRT.Common.BindableBase, IFlightTreeNode
    {
        private ObservableCollection<IFlightTreeNode> m_children = null;
        private string m_caption = string.Empty;

        public string Caption
        {
            get { return m_caption; }
            internal set
            {
                this.SetProperty<string>(ref m_caption, value);
            }
        }

        public IFlightTreeNode Parent
        {
            get { return null; }
        }

        public ObservableCollection<IFlightTreeNode> Children
        {
            get { return m_children; }
            internal set
            {
                this.SetProperty<ObservableCollection<IFlightTreeNode>>(ref m_children, value);
            }
        }
    }

    public class FlightViewNode : AircraftDataAnalysisWinRT.Common.BindableBase, IFlightTreeNode
    {
        private IFlightTreeNode m_parent;
        public FlightViewNode(Flight flight)
        {
            m_flight = flight;
        }

        private Flight m_flight = null;

        public string Caption
        {
            get { return m_flight.FlightName; }
        }

        public IFlightTreeNode Parent
        {
            get { throw new NotImplementedException(); }
            internal set
            {
                this.SetProperty<IFlightTreeNode>(ref m_parent, value);
            }
        }

        public ObservableCollection<IFlightTreeNode> Children
        {
            get { return null; }
            internal set
            {
                //
            }
        }
    }

    public interface IFlightTreeNode
    {
        string Caption
        {
            get;
        }

        IFlightTreeNode Parent
        {
            get;
        }

        ObservableCollection<IFlightTreeNode> Children
        {
            get;
        }
    }
}
