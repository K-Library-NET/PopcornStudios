using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT
{
    /// <summary>
    /// 极值信息
    /// </summary>
    public class ExtremumPointInfo
    {
        /// <summary>
        /// 参数ID
        /// </summary>
        public string ParameterID
        {
            get;
            set;
        }

        /// <summary>
        /// 最大值
        /// </summary>
        public float MaxValue
        {
            get;
            set;
        }

        /// <summary>
        /// 最小值
        /// </summary>
        public float MinValue
        {
            get;
            set;
        }

        /// <summary>
        /// 最大值出现的秒值
        /// </summary>
        public float MaxValueSecond
        {
            get;
            set;
        }

        /// <summary>
        /// 最小值出现的秒值
        /// </summary>
        public float MinValueSecond
        {
            get;
            set;
        }
    }
}
