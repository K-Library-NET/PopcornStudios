using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyParamBusiness.Fault
{
    public class BooleanCondition : ICondition
    {
        public bool Check(float value)
        {
            return Convert.ToBoolean(value);
        }
    }
}
