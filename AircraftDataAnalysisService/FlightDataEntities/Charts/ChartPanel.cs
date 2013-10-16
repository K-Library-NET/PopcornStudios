using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntities.Charts
{
    /// <summary>
    /// 数据面板
    /// </summary>
    [DataContract]
    public class ChartPanel
    {
        public ObjectId Id
        {
            get;
            set;
        }

        /// <summary>
        /// 面板ID
        /// </summary>
        [DataMember]
        public string PanelID
        {
            get;
            set;
        }

        /// <summary>
        /// 面板名称
        /// </summary>
        [DataMember]
        public string PanelName
        {
            get;
            set;
        }

        /// <summary>
        /// 面板自有的参数ID
        /// </summary>
        [DataMember]
        public string[] ParameterIDs
        {
            get;
            set;
        }
    }
}
