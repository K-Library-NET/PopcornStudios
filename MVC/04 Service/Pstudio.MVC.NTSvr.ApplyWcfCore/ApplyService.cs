using Pstudio.MVC.Common.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Pstudio.MVC.NTSvr.ApplyWcfCore
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“ApplyService”。
    public class ApplyService : IApplyService
    {
        public UserLoginResult UserLoginRequest(string machineName)
        {
            UserInfo userinfo = null;
            VehicleInfo vehicleInfo = null;
            string userId = this.GetUserIdFromHardware(out userinfo, out vehicleInfo);

            long currentTimeStamp = TimeStampUtility.ToTimeStamp(DateTime.Now);
            string token = SecurityTokenGenerator.GenerateSecurityToken(machineName,
                 currentTimeStamp, userId);

            SecurityTokenManager.Instance.AddOrUpdateToken(token, currentTimeStamp);

            return new UserLoginResult() { SecurityToken = token };
        }

        /// <summary>
        /// 阻塞并监听硬件信息
        /// </summary>
        /// <param name="userinfo"></param>
        /// <param name="vehicleInfo"></param>
        /// <returns></returns>
        private string GetUserIdFromHardware(
            out UserInfo userinfo, out VehicleInfo vehicleInfo)
        {
            throw new NotImplementedException();
        }

        public Imports.UserImportApplyResult UserImportApplyResult(
            string machineName, string securityToken, Imports.UserImportApply importApply,
            VehicleInfo vehicleInfo)
        {
            Imports.UserImportApplyResult result = null;

            string messageResult = this.GetInvalidLoginTokenResult(machineName, securityToken);
            if (!string.IsNullOrEmpty(messageResult))
                return new Imports.UserImportApplyResult() { HResult = -10, ErrorMsg = messageResult };

            throw new NotImplementedException();

            if (result != null && result.HResult == 0)
            {
                //如果成功申报，则需要清理时间戳，必须重新刷卡登录
                SecurityTokenManager.Instance.RemoveToken(securityToken);
            }
        }

        public Exports.UserExportApplyResult UserExportApplyResult(
            string machineName, string securityToken, Exports.UserExportApply exportApply,
            VehicleInfo vehicleInfo)
        {
            Exports.UserExportApplyResult result = null;

            string messageResult = this.GetInvalidLoginTokenResult(machineName, securityToken);
            if (!string.IsNullOrEmpty(messageResult))
                return new Exports.UserExportApplyResult() { HResult = -10, ErrorMsg = messageResult };

            throw new NotImplementedException();

            if (result != null && result.HResult == 0)
            {
                //如果成功申报，则需要清理时间戳，必须重新刷卡登录
                SecurityTokenManager.Instance.RemoveToken(securityToken);
            }
        }

        public AuthorizedProductListResult GetAuthorizedProductList(string machineName)
        {
            throw new NotImplementedException();
        }

        private string GetInvalidLoginTokenResult(string machineName, string securityToken)
        {
            if (!SecurityTokenManager.Instance.ExistsToken(securityToken))
            {
                return "安全令牌过期或非法请求。";
            }

            long timeStamp = SecurityTokenManager.Instance.GetTimeStamp(securityToken);

            long currentTimeStamp = TimeStampUtility.ToTimeStamp(DateTime.Now);

            if (currentTimeStamp - timeStamp > SecurityTokenGenerator.MAX_TIMESTAMP_INTERVAL)
            {
                return "安全令牌过期或非法请求。";
            }

            return string.Empty;
        }
    }
}
