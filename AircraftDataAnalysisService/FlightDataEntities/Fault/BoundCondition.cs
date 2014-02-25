using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightDataEntities.Fault
{
    /// <summary>
    /// 范围检查类
    /// </summary>
    public class BoundCondition : ICondition
    {
        /// <summary>
        /// 上边界值
        /// </summary>
        public float TopBoundary { get; set; }
        /// <summary>
        /// 下边界值
        /// </summary>
        public float BottomBoundary { get; set; }
        /// <summary>
        /// 是否包含上边界
        /// </summary>
        public bool IncludeTop { get; set; }
        /// <summary>
        /// 是否包含下边界
        /// </summary>
        public bool IncludeBottom { get; set; }
        /// <summary>
        /// 是否进行非操作
        /// 相当与两个条件的或，如大于多少或小于多少。
        /// </summary>
        public bool Nop { get; set; }

        public bool Check(float value)
        {
            if (IncludeTop && IncludeBottom)
            {
                if (value <= TopBoundary && value >= BottomBoundary)
                    return true && (!Nop);
            }
            else if (IncludeTop)
            {
                if (value <= TopBoundary && value > BottomBoundary)
                    return true && (!Nop);
            }
            else if (IncludeBottom)
            {
                if (value < TopBoundary && value >= BottomBoundary)
                    return true && (!Nop);
            }
            else
            {
                if (value < TopBoundary && value > BottomBoundary)
                    return true && (!Nop);
            }
            return false || (Nop);
        }
    }
}
