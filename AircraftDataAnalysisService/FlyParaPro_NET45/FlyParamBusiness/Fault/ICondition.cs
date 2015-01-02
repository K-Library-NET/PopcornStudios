using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyParamBusiness.Fault
{
    public interface ICondition
    {
        bool Check(float value);
    }
}
