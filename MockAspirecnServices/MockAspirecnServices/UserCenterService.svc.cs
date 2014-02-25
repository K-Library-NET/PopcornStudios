using MockAspirecnServices.UserCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MockAspirecnServices
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“UserCenterService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 UserCenterService.svc 或 UserCenterService.svc.cs，然后开始调试。
    public class UserCenterService : IUserCenterService
    {
        public string doget(string world)
        {
            Console.WriteLine(string.Format("Hello {0}, UserCenter GET.", world));
            return string.Format("Hello {0}, UserCenter.", world);
        }

        public string DoWork(string world)
        {
            Console.WriteLine(string.Format("Hello {0}, UserCenter POST.", world));
            return string.Format("Hello {0}, UserCenter.", world);
        }

        public Aspirecn.Entities.UserCenter.UserLogonResp RequestLogon(Aspirecn.Entities.UserCenter.UserLogonReq request)
        {
            UserCenterBll bll = new UserCenterBll();
            return bll.RequestLogon(request);
        }
    }
}
