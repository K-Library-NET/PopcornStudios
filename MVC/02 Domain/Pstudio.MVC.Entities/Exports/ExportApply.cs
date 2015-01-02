using Pstudio.MVC.Entities.Infos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pstudio.MVC.Entities.Exports
{
    /// <summary>
    /// 出口记录申报
    /// </summary>
    public class ExportApply
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int ExportApplyId
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
        /// 申报主体：0,“边民个人”、1,“边民合作社”、
        /// 2,“边民家庭式拼车”、3,“游客”
        /// </summary>
        public int CivilType
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
        /// 记录创建时间（时间戳）
        /// </summary>
        public long CTIME
        {
            get;
            set;
        }

        /// <summary>
        /// 申报状态：0已申报、1已出口（完成）、
        /// 2待布控查验、3布控查验通过、-1已撤销、-2不通过
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



        public virtual ICollection<ExportApplyProduct> ExportApplyProducts
        {
            get;
            set;
        }

        public virtual ICollection<ExportApplyChangeRecord> ExportApplyChangeRecords
        {
            get;
            set;
        }

        public virtual CitizenInfo CitizenInfo { get; set; }

        [ForeignKey("VehicleInfo")]
        public int VehicleInfoId { get; set; }

        public virtual VehicleInfo VehicleInfo { get; set; }
    }
}
