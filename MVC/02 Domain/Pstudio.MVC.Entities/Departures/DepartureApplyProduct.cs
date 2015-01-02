using Pstudio.MVC.Entities.Infos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pstudio.MVC.Entities.Departures
{
    /// <summary>
    /// 出境申报的商品明细
    /// </summary>
    public class DepartureApplyProduct
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int DepartureApplyProductId
        {
            get;
            set;
        }

        /// <summary>
        /// 出境申报记录
        /// </summary>
        public int DepartureApplyId
        {
            get;
            set;
        }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductInfoId
        {
            get;
            set;
        }

        /// <summary>
        /// 商品编号
        /// </summary>
        public string ProductSerialNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 商品名称
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
        /// 数量，根据系统预设的商品对应的单位确定
        /// </summary>
        public float Quantity
        {
            get;
            set;
        }

        /// <summary>
        /// 重量，根据系统预设的商品重量单位确定
        /// </summary>
        public float Weight
        {
            get;
            set;
        }

        /// <summary>
        /// 金额（人民币），根据系统预设的商品金额单位乘以数量得出
        /// </summary>
        public float CostRMB
        {
            get;
            set;
        }

        /// <summary>
        /// 货主名或经营企业名（进出境）
        /// </summary>
        public string OwnerName
        {
            get;
            set;
        }

        /// <summary>
        /// 货主或经营企业联系电话（进出境）
        /// </summary>
        public string ContactPhone
        {
            get;
            set;
        }

        /// <summary>
        /// 货主身份证号或经营企业执照编号（进出境）
        /// </summary>
        public string OwnerSerialNumber
        {
            get;
            set;
        }


        public virtual DepartureApply DepartureApply
        {
            get;
            set;
        }

        public virtual ProductInfo ProductInfo { get; set; }
    }
}
