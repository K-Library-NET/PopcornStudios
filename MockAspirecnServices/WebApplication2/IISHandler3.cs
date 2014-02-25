using Aspirecn.Entities.Cssp;
using Aspirecn.Entities.UserCenter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace WebApplication2
{
    public class IISHandler3 : IHttpHandler
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
            if (!"POST".Equals(context.Request.RequestType, StringComparison.InvariantCultureIgnoreCase))
            {
                context.Response.Write("CSSP Request Processed: " + context.Request.RequestType);
                Console.WriteLine("CSSP Request Processed: " + context.Request.RequestType);

                return;
            }

            XElement requestElement = this.ParseRequestElement(context);
            if (requestElement == null)
            {
                context.Response.Write("CSSP non-xml Request Processed: " + context.Request.RequestType);
                Console.WriteLine("CSSP non-xml Request Processed: " + context.Request.RequestType);

                return;
            }

            if (requestElement.Name.LocalName.Equals(
                "ServiceAccesssReq", StringComparison.InvariantCulture))
            {
                string xml = requestElement.ToString();
                ServiceAccesssReq req = ServiceAccesssReq.FromXml(xml);
                Aspirecn.Entities.Cssp.ServiceAccessBll bll = new ServiceAccessBll();
                var resp = bll.GetResp(req);

                string respXml = this.ConvertToServiceAccessRespXml(resp);
                context.Response.Write(respXml);
            }
            else if (requestElement.Name.LocalName.Equals(
                "QueryUserOrderHisReq", StringComparison.InvariantCulture))
            {
            }

            context.Response.Write("Request Processed. ");
            Console.WriteLine("Request Processed. ");
        }

        private string ConvertToServiceAccessRespXml(ServiceAccesssResp resp)
        {
            string xml = resp.ToXml();
            return xml;
        }

        private XElement ParseRequestElement(HttpContext context)
        {
            Stream stream = context.Request.InputStream;
            if (stream == null)
                return null;
            StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);

            XElement element = XElement.Load(reader);
            return element;
        }

        #endregion
    }
}
