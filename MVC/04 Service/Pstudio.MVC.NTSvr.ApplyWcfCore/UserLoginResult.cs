using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Pstudio.MVC.NTSvr.ApplyWcfCore
{
    /// <summary>
    /// 用户通过IC卡刷卡或者指纹登录的结果
    /// </summary>
    [DataContract]
    public class UserLoginResult
    {
        public UserLoginResult()
        { 
        }

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

        /// <summary>
        /// 以哪种方式登录
        /// </summary>
        [DataMember]
        public UserLoginType LoginType
        {
            get;
            set;
        }

        /// <summary>
        /// 登录的人员信息
        /// </summary>
        [DataMember]
        public UserInfo UserInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 登录的车辆信息
        /// </summary>
        [DataMember]
        public VehicleInfo VehicleInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 因为用户提交申报请求到服务端需要用户名密码，但是不能通过HTTP返回系统内的密码到客户端，
        /// 容易中途被人截获，造成信息的泄露。所以提供使用随时间变化的Token的信息，如果在时限内用户通过Token
        /// 发起请求，则认为是合法请求，否则视为非法请求，服务不响应。
        /// </summary>
        [DataMember]
        public string SecurityToken
        {
            get;
            set;
        }
    }
}
