using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pstudio.MVC.Entities.Infos
{
    /// <summary>
    /// 进出口、进出境货品信息
    /// </summary>
    public class ProductInfo
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int ProductInfoId
        {
            get;
            set;
        }

        /// <summary>
        /// 商品编号
        /// </summary>
        public string SerialNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 品名/商品名称
        /// </summary>
        public string ProductName
        {
            get;
            set;
        }

        /// <summary>
        /// 计量/重量单位
        /// </summary>
        public string Unit
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
        /// 单位金额（人民币）
        /// </summary>
        public double ProductValue
        {
            get;
            set;
        }

        /// <summary>
        /// 最后更新时间（时间戳）
        /// </summary>
        public long UTIME
        {
            get;
            set;
        }
    }
}
