using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntities
{
    /// <summary>
    /// 最底层的数据，直接到数据点
    /// </summary>
    [DataContract]
    public class Level1FlightRecord
    {
        public ObjectId Id
        {
            get;
            set;
        }

        /// <summary>
        /// 飞行参数ID
        /// </summary>
        [DataMember]
        public string ParameterID
        {
            get;
            set;
        }

        /// <summary>
        /// 飞行秒数（当前是第几秒）
        /// </summary>
        [DataMember]
        public int FlightSecond
        {
            get;
            set;
        }

        /// <summary>
        /// 当前秒内的采集参数
        /// </summary>
        [DataMember]
        public float[] Values
        {
            get;
            set;
        }

        [DataMember]
        public decimal Sum
        {
            get;
            set;
        }

        [DataMember]
        public float MinValue
        {
            get;
            set;
        }

        [DataMember]
        public float MaxValue
        {
            get;
            set;
        }

        [DataMember]
        public float AvgValue
        {
            get;
            set;
        }

        [DataMember]
        public int ValueCount
        {
            get;
            set;
        }

        //public FlightRawData ToFlightRecordEntity()
        //{
        //    return FlightDataEntityTransform.FromLevel1FlightRecordToFlightRawData(this);
        //}
    }
}
