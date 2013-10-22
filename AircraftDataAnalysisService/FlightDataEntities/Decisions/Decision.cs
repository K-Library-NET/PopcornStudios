using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntities.Decisions
{
    [DataContract]
    public class Decision
    {
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
        /// 事件等级，不包含事件颜色，颜色是前端根据事件等级设定
        /// </summary>
        [DataMember]
        public int EventLevel { get; set; }

        /// <summary>
        /// 相关的参数ID，用于双击记录的时候跳转到自定义面板
        /// </summary>
        [DataMember]
        public string[] RelatedParameters { get; set; }

        /// <summary>
        /// 条件
        /// </summary>
        [DataMember]
        public SubCondition[] Conditions
        {
            get;
            set;
        }

        /// <summary>
        /// 持续时长
        /// </summary>
        [DataMember]
        public int LastTime
        {
            get;
            set;
        }
    }
}
