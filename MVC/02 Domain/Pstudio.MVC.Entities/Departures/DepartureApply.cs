using Pstudio.MVC.Entities.Infos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pstudio.MVC.Entities.Departures
{
    /// <summary>
    /// 车辆船舶出境申报信息
    /// </summary>
    public class DepartureApply
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int DepartureApplyId
        {
            get;
            set;
        }

        /// <summary>
        /// 申报单编号
        /// </summary>
        public string SerialNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 申报人ID
        /// </summary>
        public int CitizenInfoId
        {
            get;
            set;
        }

        /// <summary>
        /// 申报人姓名
        /// </summary>
        public string CitizenName
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
        /// 申报人证件类型：0身份证；1军官证；2护照……
        /// </summary>
        public int IDCardType
        {
            get;
            set;
        }

        /// <summary>
        /// 申报人证件号码
        /// </summary>
        public string IDCardNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 申请出入境的交通工具类型：0汽车、1船舶
        /// </summary>
        public int TransportType
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
        /// 车辆车牌号
        /// </summary>
        public string VehicleNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 车辆备案编号
        /// </summary>
        public string VehicleSerialNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 驾驶员姓名
        /// </summary>
        public string DriverName
        {
            get;
            set;
        }

        /// <summary>
        /// 车辆国籍
        /// </summary>
        public string VehicleNation
        {
            get;
            set;
        }

        /// <summary>
        /// 船名
        /// </summary>
        public string ShipName
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
        /// 船舶备案编号（注册登记编号）
        /// </summary>
        public string ShipSerialNumber
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
        /// 船主姓名
        /// </summary>
        public string ShipOwnerName
        {
            get;
            set;
        }

        /// <summary>
        /// 船主联系电话
        /// </summary>
        public string ShipOwnerPhone
        {
            get;
            set;
        }

        /// <summary>
        /// 入境时间
        /// </summary>
        public long ImmigrationTime
        {
            get;
            set;
        }

        /// <summary>
        /// 出境时间
        /// </summary>
        public long DepartureTime
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
        /// 申报状态：0已申报、1已入境（完成）、2待布控查验、3布控查验通过、
        /// 4一级审核通过、5二级审核通过、6准装准卸、7申报货物已入境
        /// -1已撤销、-2不通过 
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


        public virtual ICollection<DepartureApplyProduct> DepartureApplyProducts
        {
            get;
            set;
        }

        public virtual ICollection<DepartureApplyChangeRecord> DepartureApplyChangeRecords
        {
            get;
            set;
        }

        public virtual CitizenInfo CitizenInfo { get; set; }

        [ForeignKey("VehicleInterInfo")]
        public int VehicleInterInfoId { get; set; }

        public virtual VehicleInterInfo VehicleInterInfo { get; set; }

        [ForeignKey("ShipInterInfo")]
        public int ShipInterInfoId { get; set; }

        public virtual ShipInterInfo ShipInterInfo { get; set; }
    }
}
