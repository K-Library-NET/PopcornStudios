using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntities.Decisions
{
    /// <summary>
    /// 判据/判定成立的记录
    /// </summary>
    [DataContract]
    public class DecisionRecord
    {
        public ObjectId Id
        {
            get;
            set;
        }

        /// <summary>
        /// 架次ID，必须，用于批量删除
        /// </summary>
        [DataMember]
        public string FlightID { get; set; }

        /// <summary>
        /// 事件等级，不包含事件颜色，颜色是前端根据事件等级设定
        /// </summary>
        [DataMember]
        public int EventLevel { get; set; }

        /// <summary>
        /// 起始秒
        /// </summary>
        [DataMember]
        public int StartSecond { get; set; }

        /// <summary>
        /// 事件发生秒
        /// </summary>
        [DataMember]
        public int HappenSecond { get; set; }

        /// <summary>
        /// 结束秒
        /// </summary>
        [DataMember]
        public int EndSecond { get; set; }

        /// <summary>
        /// 判据ID
        /// </summary>
        [DataMember]
        public string DecisionID { get; set; }

        /// <summary>
        /// 判据名称
        /// </summary>
        [DataMember]
        public string DecisionName { get; set; }

        /// <summary>
        /// 判据发生的内容描述
        /// </summary>
        [DataMember]
        public string DecisionDescription { get; set; }
    }
}
