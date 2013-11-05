using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT.Decisions
{
    public class DecisionRecord
    {
        public string FlightID { get; set; }

        /// <summary>
        /// 事件等级，不包含事件颜色，颜色是前端根据事件等级设定
        /// </summary>
        public int EventLevel { get; set; }

        public int StartSecond { get; set; }

        public int HappenSecond { get; set; }

        public int EndSecond { get; set; }

        public string DecisionID { get; set; }

        public string DecisionName { get; set; }

        public string DecisionDescription { get; set; }
    }
}
