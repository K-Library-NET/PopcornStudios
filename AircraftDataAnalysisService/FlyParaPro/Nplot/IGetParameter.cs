using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NPlot
{
    public interface IGetParameter
    {
        List<float> GetParameterData(IParameter param);
        List<IParameter> GetParameterDef();
    }
}
