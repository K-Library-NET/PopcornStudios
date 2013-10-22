using FlightDataEntities;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataReading.DataPointTransforms
{
    /// <summary>
    /// 默认策略，使用8秒钟一个点，读取配置
    /// </summary>
    public class DefaultLevel1ToLevelTopRecordStrategy
    {
        public int SecondGap = 8;

        public DefaultLevel1ToLevelTopRecordStrategy()
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Level1ToLevelTop_SecondGap"]))
            {
                int value = SecondGap;
                if (Int32.TryParse(ConfigurationManager.AppSettings["Level1ToLevelTop_SecondGap"], out value))
                {
                    SecondGap = value;
                }
            }
        }

        /// <summary>
        /// LevelTop只能有一个
        /// </summary>
        /// <param name="flight"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public LevelTopFlightRecord FromLevel1RecordCollectionToLevelTopRecords(
            Flight flight, MongoCollection<Level1FlightRecord> collection)
        {
            FlightParameter[] parameters = this.GetParameters();
            Dictionary<string, LevelTopFlightRecord> topRecordMaps = new Dictionary<string, LevelTopFlightRecord>();
            Dictionary<LevelTopFlightRecord, List<Level2FlightRecord>> level2RecordMap
                = new Dictionary<LevelTopFlightRecord, List<Level2FlightRecord>>();
            List<LevelTopFlightRecord> topRecords = new List<LevelTopFlightRecord>();

            foreach (FlightParameter para in parameters)
            {
                LevelTopFlightRecord topRecord = new LevelTopFlightRecord()
                {
                    StartSecond = flight.StartSecond,
                    EndSecond = flight.EndSecond,
                    ParameterID = para.ParameterID,
                };

                topRecordMaps.Add(para.ParameterID, topRecord);
                level2RecordMap.Add(topRecord, new List<Level2FlightRecord>());

                for (int currentSecond = flight.StartSecond; currentSecond > flight.EndSecond; currentSecond += SecondGap)
                {
                    int step = SecondGap;
                    Level2FlightRecord level2 = this.HandleOneStep(currentSecond, step, para,
                         topRecord, flight, collection);

                    level2RecordMap[topRecord].Add(level2);
                }
            }

            return null;//DEBUG
            //return topRecords.ToArray();
        }

        private Level2FlightRecord HandleOneStep(int currentSecond, int step, FlightParameter para,
            LevelTopFlightRecord topRecord, Flight flight, MongoCollection<Level1FlightRecord> collection)
        {
            IMongoQuery query = Query.And(Query.EQ("ParameterID", new BsonString(para.ParameterID)),
                Query.GTE("FlightSecond", new BsonInt32(currentSecond)),
                Query.LT("FlightSecond", new BsonInt32(currentSecond + step)));

            MongoCursor<Level1FlightRecord> flightRecord = collection.Find(query);

            Level2FlightRecord level2 = new Level2FlightRecord()
            {
                StartSecond = currentSecond,
                EndSecond = Math.Min(flight.EndSecond, currentSecond + step),
                // Level1FlightRecords = flightRecord.ToArray(),
                ParameterID = para.ParameterID,
            };

            //var sum = from one in level2.Level1FlightRecords
            //          select one.ValueCount;
            //level2.Count = sum.Sum();

            //var avg = from one in level2.Level1FlightRecords
            //          select one.AvgValue;
            //level2.AvgValue = avg.Sum() * level2.Count;

            //var min = from one in level2.Level1FlightRecords
            //          select one.MinValue;
            //level2.MinValue = min.Min();

            //var max = from one in level2.Level1FlightRecords
            //          select one.MaxValue;
            //level2.MaxValue = max.Max();

            return level2;
        }

        private FlightParameter[] GetParameters()
        {
            throw new NotImplementedException();
        }
    }
}
