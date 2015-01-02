using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pstudio.MVC.Entities.Infos
{
    /// <summary>
    /// 边民信息
    /// </summary>
    public class CitizenInfo
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int CitizenInfoId
        {
            get;
            set;
        }

        /// <summary>
        /// 申报单编号/备案编号（注册登记编号）
        /// </summary>
        public string SerialNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 申报人姓名
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 申报人国籍
        /// </summary>
        public string Nation
        {
            get;
            set;
        }

        /// <summary>
        /// 申报人证件类型：0，身份证；1，军官证；
        /// 2，护照……
        /// </summary>
        public int IDCardType
        {
            get;
            set;
        }

        /// <summary>
        /// 证件号
        /// </summary>
        public string IDCardNumber
        {
            get;
            set;
        }

        public int CivilType
        {
            get;
            set;
        }

        /// <summary>
        /// IC卡号码（用于身份识别）
        /// </summary>
        public string ICCardNumber
        {
            get;
            set;
        }

        /// <summary>
        /// IC卡密码（用于身份识别）
        /// </summary>
        public string ICCardPassword
        {
            get;
            set;
        }

        /// <summary>
        /// 指纹信息
        /// </summary> 
        public string FingerPrint
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
        /// 互市地点编号（用于数据权限过滤）
        /// </summary>
        public int LocationCode
        {
            get;
            set;
        }

        /// <summary>
        /// 有效期至（时间戳）
        /// </summary>
        public long ValidUntil
        {
            get;
            set;
        }

        /// <summary>
        /// 记录创建时间（时间戳）
        /// </summary>
        public long CTIME
        {
            get;
            set;
        }

        /// <summary>
        /// 申报状态：0已申报、1申报已批准并发卡（完成）、
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
