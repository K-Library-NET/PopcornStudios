using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT
{
    /// <summary>
    /// 生成极值和统一的第二层值
    /// </summary>
    public class DataPointReducer
    {
        public Level1FlightRecord[] ReduceFlightRawDataPoints(string parameterID,
            List<ParameterRawData> points, int secondGap)
        {
            //1. 每秒钟取一个点
            var wrapped = from one in points
                          select new ParameterRawDataWrap(one);

            //2. 每secondGap取一个值
            List<Level1FlightRecord> records = new List<Level1FlightRecord>();
            List<ParameterRawDataWrap> tempList = new List<ParameterRawDataWrap>();

            int startSec = 0;
            int endSec = Math.Min(startSec + secondGap, points[points.Count - 1].Second);
            foreach (var one in wrapped)
            {
                if (one.m_RawData.Second >= startSec
                    && one.m_RawData.Second < endSec)
                {
                    tempList.Add(one);
                }
                else
                {
                    Level1FlightRecord rec = new Level1FlightRecord()
                    {
                        ParameterID = parameterID,
                        StartSecond = startSec,
                        EndSecond = endSec,
                        Values = (from o in tempList
                                  select o.SummaryValue).ToArray()
                    };
                    records.Add(rec);
                    tempList.Clear();

                    startSec = endSec;
                    endSec = Math.Min(endSec + secondGap, points[points.Count - 1].Second);
                }
            }

            Level1FlightRecord rec2 = new Level1FlightRecord()
            {
                ParameterID = parameterID,
                StartSecond = startSec,
                EndSecond = endSec,
                Values = (from o in tempList
                          select o.SummaryValue).ToArray()
            };
            records.Add(rec2);
            tempList.Clear();

            return records.ToArray();
        }

        class ParameterRawDataWrap
        {
            public ParameterRawData m_RawData = null;

            public ParameterRawDataWrap(ParameterRawData rawData)
            {
                if (rawData == null)
                    throw new NullReferenceException();
                m_RawData = rawData;
            }

            ///<summary>
            /// 写死只要第一个值
            ///</summary>
            public float SummaryValue
            {
                get
                {
                    if (m_RawData.Values != null && m_RawData.Values.Length > 0)
                        return m_RawData.Values[0];
                    return 0;
                }
            }
        }

        public Level2FlightRecord GenerateLevel2FlightRecord(string parameterID,
            Level1FlightRecord[] level1Points)
        {
            Level2FlightRecord level2Records = new Level2FlightRecord()
            {
                StartSecond = 0,
                EndSecond = level1Points[level1Points.Length - 1].EndSecond,
                ParameterID = parameterID,
                Values = level1Points,
                ExtremumPointInfo = new ExtremumPointInfo()
                {
                    MaxValue =
                        (from one in level1Points
                         select one.Values.Max()).Max(),
                    MinValue = (from one in level1Points
                                select one.Values.Min()).Min()
                }
            };

            float maxValue = float.MinValue;
            float minValue = float.MaxValue;

            Level1FlightRecord minRec = null;
            Level1FlightRecord maxRec = null;

            foreach (Level1FlightRecord rec in level1Points)
            {
                if (rec.Values.Max() > maxValue)
                {
                    maxValue = rec.Values.Max();
                    maxRec = rec;
                }
                if (rec.Values.Min() < minValue)
                {
                    minValue = rec.Values.Min();
                    minRec = rec;
                }
            }

            if (maxRec != null)
            {
                for (int i = 0; i < maxRec.Values.Length; i++)
                {
                    if (maxRec.Values[i] == maxValue)
                    {
                        level2Records.ExtremumPointInfo.MaxValueSecond
                            = maxRec.StartSecond + (i / Convert.ToSingle(maxRec.EndSecond - maxRec.StartSecond));
                        break;
                    }
                }

                level2Records.ExtremumPointInfo.MaxValue = maxRec.Values.Max();
            }
            if (minRec != null)
            {
                for (int i = 0; i < minRec.Values.Length; i++)
                {
                    if (minRec.Values[i] == minValue)
                    {
                        level2Records.ExtremumPointInfo.MinValueSecond
                            = minRec.StartSecond + (i / Convert.ToSingle(minRec.EndSecond - minRec.StartSecond));
                        break;
                    }
                }

                level2Records.ExtremumPointInfo.MinValue = minRec.Values.Min();
            }

            return level2Records;
        }
    }
}
