using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyParamBusiness.Fault
{
    public class SmallCondition : ICondition
    {
        public float TopBoundary { get; set; }
        public bool IncludeTop { get; set; }

        public bool Check(float value)
        {
            if (IncludeTop)
            {
                if (value <= TopBoundary)
                    return true;
            }
            else
            {
                if (value < TopBoundary)
                    return true;
            }
            return false;
        }
    }
}
