using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NPlot
{
    public interface IParameter
    {
        string ID { get; }
        int Index { get; set; }
        int SubIndex { get; set; }
        string Caption { get; set; }
        string Unit { get; set; }
        int Frequence { get; set; }
    }
}
