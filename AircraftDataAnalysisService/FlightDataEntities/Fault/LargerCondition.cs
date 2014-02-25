using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightDataEntities.Fault
{
    /// <summary>
    /// 大于或大于等于条件类
    /// </summary>
    public class LargeCondition : ICondition
    {
        public float BottomBoundary { get; set; }
        public bool IncludeBottom { get; set; }

        public bool Check(float value)
        {
            if (IncludeBottom)
            {
                if (value >= BottomBoundary)
                    return true;
            }
            else
            {
                if (value > BottomBoundary)
                    return true;
            }
            return false;
        }
    }
}
