using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pstudio.MVC.Entities.Records
{
    /// <summary>
    /// 商品贸易进出口货品日数量
    /// </summary>
    public class ImpExpProductsPerDay
    {
        public int ImpExpProductsPerDayId
        {
            get;
            set;
        }

        /// <summary>
        /// 商品名
        /// </summary>
        public string ProductName
        {
            get;
            set;
        }

        /// <summary>
        /// 原产地
        /// </summary>
        public string MadeIn
        {
            get;
            set;
        }

        /// <summary>
        /// 年份（数字）
        /// </summary>
        public int YearNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 月：1-12
        /// </summary>
        public int MonthNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 季度：1、2、3、4
        /// </summary>
        public int QuarterNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 日：1-31
        /// </summary>
        public int DayNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 当天总重量
        /// </summary>
        public float SubTotalWeight
        {
            get;
            set;
        }

        /// <summary>
        /// 当天总货值
        /// </summary>
        public float SubTotalValues
        {
            get;
            set;
        }

        /// <summary>
        /// 0:进口；1:出口
        /// </summary>
        public int Type
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
    }
}
