using FlightDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataReading.DataPointTransforms
{
    /// <summary>
    /// 默认的数据精简与插值回复数据方法：
    /// 如果多个值一致，则只保留第一个值；
    /// 回复数据也是一样，如果数值不等，则补充到个数正确为止
    /// </summary>
    //public class DefaultFlightDataEntityTransformStrategy :
    //    FlightDataReading.DataPointTransforms.IFlightDataEntityTransformStrategy
    //{
    //    public FlightRawData FromLevel1FlightRecordToFlightRawData(Level1FlightRecord record)
    //    {
    //        FlightRawData entity = new FlightRawData()
    //        {
    //            ParameterID = record.ParameterID,
    //            Second = record.FlightSecond,
    //        };

    //        if (record.ValueCount == record.Values.Length)
    //        {
    //            entity.Values = record.Values;
    //        }
    //        else
    //        {//如果不等，说明已经经过精简，要补充值
    //            float prevValue = 0;
    //            List<float> values = new List<float>();

    //            for (int i = 0; i < record.ValueCount; i++)
    //            {
    //                if (i < record.Values.Length)
    //                {
    //                    values.Add(record.Values[i]);
    //                    prevValue = record.Values[i];
    //                }
    //                else
    //                {
    //                    values.Add(prevValue);
    //                }
    //            }

    //            entity.Values = values.ToArray();
    //        }

    //        return entity;
    //    }

    //    public Level1FlightRecord FromFlightRecordEntityToLevel1FlightRecord(FlightRawData data)
    //    {
    //        Level1FlightRecord record = new Level1FlightRecord()
    //        {
    //            ParameterID = data.ParameterID,
    //            FlightSecond = data.Second,
    //            AvgValue = data.Values.Average(),
    //            MaxValue = data.Values.Max(),
    //            MinValue = data.Values.Min(),
    //            Sum = Convert.ToDecimal(data.Values.Sum()),
    //            ValueCount = data.Values.Length
    //        };

    //        if (data.Values.Distinct().Count() == 1 //只有一个值，多个是重复
    //            && record.AvgValue == record.MaxValue
    //            && record.MaxValue == record.MinValue
    //            && record.AvgValue == record.MinValue) //三个汇总值全等
    //        {//可以视为能够精简，只保留第一个值
    //            record.Values = new float[] { data.Values[0] };
    //        }
    //        else
    //        {//保留全部值
    //            record.Values = data.Values;
    //        }

    //        return record;
    //    }
    //}
}
