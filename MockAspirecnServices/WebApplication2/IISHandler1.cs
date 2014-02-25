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
    public class IISHandler1 : IHttpHandler
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
                context.Response.Write("User Center Request Processed: " + context.Request.RequestType);
                Console.WriteLine("User Center Request Processed: " + context.Request.RequestType);

                return;
            }

            XElement requestElement = this.ParseRequestElement(context);
            if (requestElement == null)
            {
                context.Response.Write("User Center non-xml Request Processed: " + context.Request.RequestType);
                Console.WriteLine("User Center non-xml Request Processed: " + context.Request.RequestType);

                return;
            }

            IEnumerable<XElement> msgTypes = requestElement.Descendants("msgType");
            if (msgTypes == null)
            {
                context.Response.Write("User Center non-msgType Request Processed: " + context.Request.RequestType);
                Console.WriteLine("User Center non-msgType Request Processed: " + context.Request.RequestType);

                return;
            }

            foreach (XElement e in msgTypes)
            {
                if (!string.IsNullOrEmpty(e.Value.Trim())
                    && "UserLogon".Equals(e.Value.Trim(), StringComparison.InvariantCultureIgnoreCase))
                {
                    this.DoWithUserLogon(context, requestElement);
                    break;
                }
            }

            context.Response.Write("Request Processed. ");
            Console.WriteLine("Request Processed. ");
        }

        private void DoWithUserLogon(HttpContext context, XElement requestElement)
        {
            UserLogonReq request = this.ParseRequestElement(requestElement);
            UserLogonBll bll = new UserLogonBll();
            UserLogonResp response = bll.DoWithUserLogon(request);

            if (response == null)
            {//response no
                return;
            }

            this.ResponseUserLogonResp(context, response);
        }

        private void ResponseUserLogonResp(HttpContext context, UserLogonResp response)
        {
            string userInfoXml = response.ToXml();
            XElement source = XElement.Parse(userInfoXml);
            source.Name = "body";
            
            TextReader reader = this.GetUserLogonRespDocTemplate(context);
            XDocument docElement = XDocument.Load(reader, LoadOptions.PreserveWhitespace);
            IEnumerable<XElement> userInfos = docElement.Descendants("body");

            foreach (XElement e in userInfos)
            {
                e.ReplaceNodes(source.Nodes());
                break;
            }

            StringBuilder builder = new StringBuilder();
            builder.AppendLine(docElement.Declaration.ToString());
            builder.AppendLine(docElement.ToString());
            context.Response.Write(builder.ToString());
        }

        private TextReader GetUserLogonRespDocTemplate(HttpContext context)
        {
            string xml = Resource1.UserLogonResp;

            //string path = context.Server.MapPath("bin");

            //string xmlPath = Path.Combine(path, "UserLogonResp.xml");

            StringReader reader = new StringReader(xml);
            return reader;
        }

        private UserLogonReq ParseRequestElement(XElement requestElement)
        {
            UserLogonReq request = UserLogonReq.ParseToEntity(requestElement);

            return request;
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
