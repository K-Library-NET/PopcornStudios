using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pstudio.MVC.Entities.Departures
{
    /// <summary>
    /// 出境申请状态变更记录
    /// </summary>
    public class DepartureApplyChangeRecord
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int DepartureApplyChangeRecordId
        {
            get;
            set;
        }

        /// <summary>
        /// 出境记录ID
        /// </summary>
        public int DepartureApplyId
        {
            get;
            set;
        }

        /// <summary>
        /// 上一个状态
        /// </summary>
        public int StatusFrom
        {
            get;
            set;
        }

        /// <summary>
        /// 下一个状态（当前状态）
        /// </summary>
        public int StatusTo
        {
            get;
            set;
        }

        /// <summary>
        /// 操作用户名或ID
        /// </summary>
        public string ActionUserNameOrUserId
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


        public virtual DepartureApply DepartureApply { get; set; }
    }
}
