using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT.Charts
{
    /// <summary>
    /// 数据面板
    /// </summary>
    public class ChartPanel
    {
        /// <summary>
        /// 面板ID
        /// </summary>
        public string PanelID
        {
            get;
            set;
        }

        /// <summary>
        /// 面板名称
        /// </summary>
        public string PanelName
        {
            get;
            set;
        }

        /// <summary>
        /// 面板自有的参数ID，带有顺序
        /// </summary>
        public string[] ParameterIDs
        {
            get;
            set;
        }
    }
}
