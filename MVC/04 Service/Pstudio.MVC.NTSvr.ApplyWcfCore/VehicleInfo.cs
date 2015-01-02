using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Pstudio.MVC.NTSvr.ApplyWcfCore
{
    /// <summary>
    /// 车辆工具信息
    /// </summary>
    [DataContract]
    public class VehicleInfo
    {
        /// <summary>
        /// 车牌号
        /// </summary>
        [DataMember]
        public string LicenseNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 车型
        /// </summary>
        [DataMember]
        public string VehicleTypeName
        {
            get;
            set;
        }
    }
}
