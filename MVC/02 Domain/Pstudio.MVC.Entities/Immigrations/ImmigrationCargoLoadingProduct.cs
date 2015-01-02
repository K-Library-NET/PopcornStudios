using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pstudio.MVC.Entities.Immigrations
{
    public class ImmigrationCargoLoadingProduct
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int ImmigrationCargoLoadingProductId
        {
            get;
            set;
        }

        /// <summary>
        /// 入境准装准卸单ID
        /// </summary>
        public int ImmigrationCargoLoadingId
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


        public virtual ImmigrationCargoLoading ImmigrationCargoLoading { get; set; }
    }
}
