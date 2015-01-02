using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pstudio.MVC.Entities.Departures
{
    /// <summary>
    /// 出境准装准卸单
    /// </summary>
    public class DepartureCargoLoading
    {
        public int DepartureCargoLoadingId
        {
            get;
            set;
        }

        /// <summary>
        /// 出境申请ID
        /// </summary>
        public int DepartureApplyId
        {
            get;
            set;
        }

        /// <summary>
        /// 船舶备案编号
        /// </summary>
        public string ShipSerialNumber
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
        /// 停靠位置/装卸货地点
        /// </summary>
        public string Position
        {
            get;
            set;
        }

        /// <summary>
        /// 监装监卸开始时间
        /// </summary>
        public long StartTime
        {
            get;
            set;
        }

        /// <summary>
        /// 监装监卸结束时间
        /// </summary>
        public long EndTime
        {
            get;
            set;
        }

        /// <summary>
        /// 监装监卸情况：0已申报、1准装准卸通过（完成）、
        /// 4一级审核通过、5二级审核通过、-1已撤销、-2不通过
        /// </summary>
        public int Status
        {
            get;
            set;
        }

        /// <summary>
        /// 操作备注：例如审批不通过写原因
        /// </summary>
        public string Memo
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


        public virtual DepartureApply DepartureApply
        {
            get;
            set;
        }

        public virtual ICollection<DepartureCargoLoadingProduct>
            DepartureCargoLoadingProducts { get; set; }
    }
}
