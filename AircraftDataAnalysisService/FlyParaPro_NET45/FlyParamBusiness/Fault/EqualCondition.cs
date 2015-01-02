using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyParamBusiness.Fault
{
    public class EqualCondition : ICondition
    {
        public float EqualValue { get; set; }

        public bool Check(float value)
        {
            if (value == EqualValue)
                return true;
            return false;
        }
    }
}
