using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT.Decisions
{
    public enum CompareOperator
    {
        /// <summary>
        /// 等于
        /// </summary>
        Equal = 0,
        /// <summary>
        /// 不等于
        /// </summary>
        NotEqual,
        /// <summary>
        /// 大于
        /// </summary>
        GreaterThan,
        /// <summary>
        /// 大于等于
        /// </summary>
        GreaterOrEqual,
        /// <summary>
        /// 小于
        /// </summary>
        SmallerThan,
        /// <summary>
        /// 小于等于
        /// </summary>
        SmallerOrEqual,
        ///// <summary>
        ///// 持续时间（重要）
        ///// </summary>
        //TimeRange,

    }
}
