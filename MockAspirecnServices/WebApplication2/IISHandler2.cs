using Aspirecn.Entities.DownloadCenter;
using System;
using System.IO;
using System.Web;

namespace WebApplication2
{
    public class IISHandler2 : IHttpHandler
    {
        /// <summary>
        /// 您将需要在网站的 Web.config 文件中配置此处理程序 
        /// 并向 IIS 注册它，然后才能使用它。有关详细信息，
        /// 请参见下面的链接: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // 如果无法为其他请求重用托管处理程序，则返回 false。
            // 如果按请求保留某些状态信息，则通常这将为 false。
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (!"GET".Equals(context.Request.RequestType, StringComparison.InvariantCultureIgnoreCase))
            {
                context.Response.Write("Download Center Request Processed: " + context.Request.RequestType);
                Console.WriteLine("Download Center Request Processed: " + context.Request.RequestType);

                return;
            }

            this.HandleParameters(context);

            context.Response.BinaryWrite(Resource1.mobilemarket);

            //context.Response.Write("Download Center Request Processed. ");
            Console.WriteLine("Download Center Request Processed. ");
        }

        private void HandleParameters(HttpContext context)
        {
            //downloadAppForWWW?id=xx&device=xx&msisdn=xx&TID=xx&destmsisdn=xx&OndemandType=xxx&SCode=xx
            //downloadAppForWeb?id=xx&device=xx&msisdn=xx&TID=xx&destmsisdn=xx&notify=xx&SCode=xx
            DownloadParameter param = new DownloadParameter();
            param.ContentID = context.Request.QueryString["id"];
            param.DeviceId = context.Request.QueryString["device"];
            param.Msisdn = context.Request.QueryString["msisdn"];
            param.PushID = context.Request.QueryString["TID"];
            param.DestMsisdn = context.Request.QueryString["destmsisdn"];
            param.OnDemandType = context.Request.QueryString["OndemandType"];
            param.SCode = context.Request.QueryString["SCode"];
            param.Notify = context.Request.QueryString["notify"];

            param.DoDownloadRecord();
        }

        #endregion
    }
}
