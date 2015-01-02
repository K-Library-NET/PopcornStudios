using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pstudio.MVC.NTSvr.ApplyWcfCore
{
    /// <summary>
    /// 用户申请服务的通用结果集合
    /// </summary>
    public interface IApplyServiceResult
    {
        /// <summary>
        /// 结果标识码，返回0则是成功
        /// </summary>
        int HResult
        {
            get;
        }

        /// <summary>
        /// 错误信息。如果结果标识码不为0，则此字段应该有值
        /// </summary>
        string ErrorMsg
        {
            get;
        }
    }
}
