using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Pstudio.MVC.NTSvr.ApplyWcfCore
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [DataContract]
    public class UserInfo
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [DataMember]
        public string FullName
        {
            get;
            set;
        }

        /// <summary>
        /// 身份证号
        /// </summary>
        [DataMember]
        public string IdentityCardSerialNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 性别：1，男；0，女；
        /// </summary>
        [DataMember]
        public int Sex
        {
            get;
            set;
        }
    }
}
