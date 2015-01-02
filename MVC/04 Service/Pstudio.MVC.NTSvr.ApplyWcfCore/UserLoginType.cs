using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pstudio.MVC.NTSvr.ApplyWcfCore
{
    /// <summary>
    /// 用户登录方式
    /// </summary>
    public enum UserLoginType
    {
        IcCardTypeI = 0, //使用Type 1 IC卡登录
        FingerPrint = 1, //使用指纹登录
    }
}
