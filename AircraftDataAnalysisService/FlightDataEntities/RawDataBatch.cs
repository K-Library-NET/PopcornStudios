using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntities
{
    [Obsolete("test")]
    public class RawDataBatch
    {
        public int Second
        {
            get;
            set;
        }

        public Flight Flight
        {
            get;
            set;
        }

        public RawDataParamBatch[] Datas
        {
            get;
            set;
        }
    }

    [Obsolete("test")]
    public class RawDataParamBatch
    {
        public string ParameterID
        {
            get;
            set;
        }

        public float[] Values
        {
            get;
            set;
        }
    }
}
