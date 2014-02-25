using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspirecn.Entities.DownloadCenter
{
    public class DownloadParameter
    {
        /// <summary>
        /// 必须 contentid
        /// </summary>
        public string ContentID { get; set; }

        /// <summary>
        /// 必须。表示为订单付费的手机号码。
        /// </summary>
        public string Msisdn { get; set; }

        /// <summary>
        /// TID，即是订单中的PUSHID，必须
        /// </summary>
        public string PushID { get; set; }

        /// <summary>
        /// destmsisdn，可选参数,适用于赠送索要需求。
        /// 表示下载应用的手机号码，当此号码不传递时表示下载应用的是付费手机号码。
        /// </summary>
        public string DestMsisdn { get; set; }

        /// <summary>
        /// 订购方式（不填则默认为移动用户订购），可选参数,
        /// 前4种指经过用户中心的订购，后3种是不经用户中心的订购；
        /// 1：移动用户订购
        /// 2：联通电信用户订购；
        /// 3：邮箱用户订购；
        /// 4：设备用户订购；
        /// 5：IMSI订购；
        /// 6：32位IMSI订购
        /// 7：MAC地址订购
        /// 8：免登陆订购
        /// 该字段为可选字段，免登陆用户下载必须带该参数，正常移动用户登录可以不带该参数
        /// </summary>
        public string OnDemandType { get; set; }
        
        /// <summary>
        /// 全校验码SecurityCode：SCode，安全校验码SecurityCode：SCode，算法为：
        /// byte转换成16进制函数（MD5算法函数（device +msisdn + TID +密钥字符串））
        /// MD5(privateKey +msisdn+id+TID+OndemandType)
        /// 其中，密钥字符串在WWW门户系统和下载平台系统内部统一维护，目前不使用，预留
        /// </summary>
        public string SCode { get; set; }

        /// <summary>
        /// 取值为(self,auto)self由下载方自己发起通知， 
        /// auto 由下载平台代通知，本参数为可选参数，
        /// 不传值时默认为auto。参数名称和值都为小写。
        /// </summary>
        public string Notify { get; set; }

        /// <summary>
        /// 必须，约定传递deviceId
        /// </summary>
        public string DeviceId { get; set; }

        public void DoDownloadRecord()
        {
            using (var context = new Aspirecn.Entities.DownloadCenter.ModelDownloadCenterContainer())
            {
                var result = from one in context.DownloadCenterRequestEntities
                             select one;

                Aspirecn.Entities.DownloadCenter.DownloadCenterRequest req
                    = new DownloadCenterRequest()
                    {
                        ContentID = this.ContentID,
                        DestMsisdn = this.DestMsisdn,
                        DeviceId = this.DeviceId,
                        Msisdn = this.Msisdn,
                        Notify = this.Notify,
                        OnDemandType = this.OnDemandType,
                        PushID = this.PushID,
                        SCode = this.SCode
                    };

                context.DownloadCenterRequestEntities.Add(req);
                context.SaveChanges();
            }
        }
    }
}
