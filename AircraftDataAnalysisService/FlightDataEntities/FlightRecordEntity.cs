using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntities
{
    [Obsolete("test")]
    /// <summary>
    /// 某一个飞行参数的单条记录，粒度为秒
    /// </summary>
    public class FlightRecordEntity
    {
        /// <summary>
        /// 飞行参数ID
        /// </summary>
        public string ParameterID
        {
            get;
            set;
        }

        /// <summary>
        /// 飞行秒数（当前是第几秒）
        /// </summary>
        public int FlightSecond
        {
            get;
            set;
        }

        /// <summary>
        /// 当前秒内的采集参数
        /// </summary>
        public float[] Values
        {
            get;
            set;
        }

        public float MinValue
        {
            get
            {
                if (this.Values != null && this.Values.Length > 0)
                    return this.Values.Min();
                return 0;
            }
        }

        public float MaxValue
        {
            get
            {
                if (this.Values != null && this.Values.Length > 0)
                    return this.Values.Max();
                return 0;
            }
        }

        public float AvgValue
        {
            get
            {
                if (this.Values != null && this.Values.Length > 0)
                    return this.Values.Average();
                return 0;
            }
        }

        public int ValueCount
        {
            get
            {
                if (Values != null)
                    return Values.Length;

                return 0;
            }
        }

        public Level1FlightRecord ToLevel1FlightRecord()
        {
            return new Level1FlightRecord();
        }
    }
}
