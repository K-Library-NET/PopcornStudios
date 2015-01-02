using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pstudio.MVC.Entities.Infos
{
    /// <summary>
    /// 边民互市贸易车辆
    /// </summary>
    public class VehicleInfo
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int VehicleInfoId
        {
            get;
            set;
        }

        /// <summary>
        /// 车辆车牌号
        /// </summary>
        public string VehicleNumber
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
        /// 发动机号
        /// </summary>
        public string EngineNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 车架号
        /// </summary>
        public string CarriageNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 外籍车牌号
        /// </summary>
        public string VehicleNumberENG
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
        /// 车辆类型
        /// </summary>
        public string VehicleType
        {
            get;
            set;
        }

        /// <summary>
        /// 厂牌型号
        /// </summary>
        public string FactoryTypeNum
        {
            get;
            set;
        }

        /// <summary>
        /// 车身颜色
        /// </summary>
        public string Color
        {
            get;
            set;
        }

        /// <summary>
        /// 车主证件号
        /// </summary>
        public string IDCardNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 车主联系电话
        /// </summary>
        public string Phone
        {
            get;
            set;
        }

        /// <summary>
        /// 车辆自重（KG）
        /// 转换为吨需要除以1000
        /// </summary>
        public int NetWeight
        {
            get;
            set;
        }

        /// <summary>
        /// 核定载重（KG）
        /// 转换为吨需要除以1000
        /// </summary>
        public int MaxWeight
        {
            get;
            set;
        }

        /// <summary>
        /// IC卡（用于过关口开闸）号码
        /// </summary>
        public string ICCardNumber
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
