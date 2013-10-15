using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT.Decisions
{
    public class DecisionRecord
    {
        public int StartSecond { get; set; }

        public int EndSecond { get; set; }

        public string DecisionID { get; set; }

        public string DecisionName { get; set; }

        public string DecisionDescription { get; set; }
    }
}
