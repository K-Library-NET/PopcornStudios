using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntities
{
    /// <summary>
    /// 判据/判定成立的记录
    /// </summary>
    [DataContract]
    public class DecisionRecord
    {
        /// <summary>
        /// 起始秒
        /// </summary>
        [DataMember]
        public int StartSecond { get; set; }

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
