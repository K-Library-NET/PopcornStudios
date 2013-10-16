using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntities
{
    [Obsolete("test")]
    /// <summary>
    /// 一个架次某一个参数的单点记录
    /// </summary>
    public class FlightRecordPoint
    {
        /// <summary>
        /// 记录ID，用作关联、定位
        /// </summary>
        public ObjectId ID
        {
            get;
            set;
        }

        /// <summary>
        /// 开始飞行到目前的秒数
        /// </summary>
        public int Second
        {
            get;
            set;
        }

        /// <summary>
        /// 当前秒钟的毫秒值
        /// </summary>
        public int MillSec
        {
            get;
            set;
        }

        /// <summary>
        /// 加个总秒数出来，用于图表很有用
        /// </summary>
        public float SecondWithMillSec
        {
            get { return (float)this.Second + ((float)this.MillSec / 1000.0F); }
        }

        /// <summary>
        /// 飞参的数值
        /// </summary>
        public float Value
        {
            get;
            set;
        }
    }
}
