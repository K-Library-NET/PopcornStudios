using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pstudio.MVC.Entities.Infos
{
    /// <summary>
    /// 边民互市进出境船舶
    /// </summary>
    public class ShipInterInfo
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int ShipInterInfoId
        {
            get;
            set;
        }

        /// <summary>
        /// 船号
        /// </summary>
        public string ShipNumber
        {
            get;
            set;
        }

        /// <summary>
        /// IMO号
        /// </summary>
        public string IMO
        {
            get;
            set;
        }

        /// <summary>
        /// 备案编号（注册登记编号）
        /// </summary>
        public string SerialNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 船名（中文）
        /// </summary>
        public string ShipName
        {
            get;
            set;
        }

        /// <summary>
        /// 船名（越文）
        /// </summary>
        public string ShipNameENG
        {
            get;
            set;
        }

        /// <summary>
        /// 证书编号
        /// </summary>
        public string CertSerial
        {
            get;
            set;
        }

        /// <summary>
        /// 发证机关
        /// </summary>
        public string CertOrg
        {
            get;
            set;
        }

        /// <summary>
        /// 国籍
        /// </summary>
        public string Nation
        {
            get;
            set;
        }

        /// <summary>
        /// 船舶所属企业
        /// </summary>
        public string ShipOwner
        {
            get;
            set;
        }

        /// <summary>
        /// 船长姓名
        /// </summary>
        public string CaptainName
        {
            get;
            set;
        }

        /// <summary>
        /// 船长国籍
        /// </summary>
        public string CaptainNation
        {
            get;
            set;
        }

        /// <summary>
        /// 船宽
        /// </summary>
        public string ShipWidth
        {
            get;
            set;
        }

        /// <summary>
        /// 船高
        /// </summary>
        public string ShipHeight
        {
            get;
            set;
        }

        /// <summary>
        /// 净吨位
        /// </summary>
        public string NetWeight
        {
            get;
            set;
        }

        /// <summary>
        /// 总吨位
        /// </summary>
        public string MaxWeight
        {
            get;
            set;
        }

        /// <summary>
        /// 载重吨位
        /// </summary>
        public string CarryWeight
        {
            get;
            set;
        }

        /// <summary>
        /// 互市地点编号（用于数据权限过滤）
        /// </summary>
        public int LocationCode
        {
            get;
            set;
        }

        /// <summary>
        /// 单证图片上传地址1
        /// </summary>
        public string CertURL1
        {
            get;
            set;
        }

        /// <summary>
        /// 单证图片上传地址2
        /// </summary>
        public string CertURL2
        {
            get;
            set;
        }

        /// <summary>
        /// 单证图片上传地址3
        /// </summary>
        public string CertURL3
        {
            get;
            set;
        }

        /// <summary>
        /// 单证图片上传地址4
        /// </summary>
        public string CertURL4
        {
            get;
            set;
        }

        /// <summary>
        /// 单证图片上传地址5
        /// </summary>
        public string CertURL5
        {
            get;
            set;
        }

        /// <summary>
        /// 有效期至
        /// </summary>
        public long ValidUntil
        {
            get;
            set;
        }

        /// <summary>
        /// 记录创建时间
        /// </summary>
        public long CTIME
        {
            get;
            set;
        }

        /// <summary>
        /// 申报状态：0已申报、1申报已批准（完成）、
        /// 4一级审核通过、5二级审核通过、-1已撤销、-2不通过
        /// </summary>
        public int Status
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            get;
            set;
        }
    }
}
