using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Pstudio.MVC.NTSvr.ApplyWcfCore.Imports
{
    /// <summary>
    /// 用户提交进口要求的结果对象
    /// </summary>
    [DataContract]
    public class UserImportApplyResult : IApplyServiceResult
    {
        /// <summary>
        /// 结果标识码，返回0则是成功
        /// </summary>
        [DataMember]
        public int HResult
        {
            get;
            set;
        }

        /// <summary>
        /// 错误信息。如果结果标识码不为0，则此字段应该有值
        /// </summary>
        [DataMember]
        public string ErrorMsg
        {
            get;
            set;
        }
    }
}
