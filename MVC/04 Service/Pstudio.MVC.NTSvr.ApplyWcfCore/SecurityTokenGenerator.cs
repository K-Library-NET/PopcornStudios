using Pstudio.MVC.Common.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Pstudio.MVC.NTSvr.ApplyWcfCore
{
    /// <summary>
    /// 专门生成安全令牌的类
    /// </summary>
    public class SecurityTokenGenerator
    {
        /// <summary>
        /// 时间戳最大间隔，暂定10分组
        /// </summary>
        public static readonly long MAX_TIMESTAMP_INTERVAL = 10 * 60 * 100000;

        public static string GenerateSecurityToken(string machineName,
            long timeStamp, string userId)
        {
            string combine = string.Format("{0}@#{1}@#{2}",
                machineName, timeStamp.ToString(), userId);

            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(combine));
                return Convert.ToBase64String(hash);
            }
        }
    }
}
