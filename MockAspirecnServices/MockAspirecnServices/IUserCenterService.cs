using Aspirecn.Entities.UserCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MockAspirecnServices
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IUserCenterService”。
    [ServiceContract]
    public interface IUserCenterService
    {
        [WebGet]
        [OperationContract]
        string doget(string world);

        [WebInvoke]
        [OperationContract]
        string DoWork(string world);

        [WebInvoke(UriTemplate = "requestlogon", Method = "POST", BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        UserLogonResp RequestLogon(UserLogonReq request);
    }
}
