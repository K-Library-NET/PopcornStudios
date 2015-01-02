using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyParamBusiness.Fault
{
    public class FaultHelper
    {
        /// <summary>
        /// 从故障列表中提取出每一秒要读取的飞参数据列表
        /// </summary>
        /// <param name="faults"></param>
        /// <returns></returns>
        public static List<FlyParameter> GetFlyParamNoRepetion(List<AircraftFault> faults)
        {
            SortedList<int, FlyParameter> list = new SortedList<int, FlyParameter>();
            foreach (AircraftFault fault in faults)
            {
                foreach (ParameterCondition pc in fault.ParamConditions)
                {
                    if (list.ContainsKey(pc.Parameter.Index))
                        continue;
                    list.Add(pc.Parameter.Index, pc.Parameter);
                }
            }

            return list.Values.ToList();
        }
        /// <summary>
        /// 检查某一秒的指定的故障条件是否成立
        /// </summary>
        /// <param name="fault"></param>
        /// <param name="paramValues"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool CheckFault(AircraftFault fault, Dictionary<string, List<float>> paramValues, int second)
        {
            second = second - 1;//为了减少下面循环里的计算这里先减1
            foreach (ParameterCondition pc in fault.ParamConditions)
            {
                List<float> list = paramValues[pc.Parameter.ID];
                bool isValid = false;
                int freq = pc.Parameter.Frequence;
                for (int i = 0; i < freq; i++)
                {
                    try
                    {
                        isValid = pc.Condition.Check(list[second * freq + i]);
                    }
                    catch (Exception ex)
                    {
                    }
                    //如果持续时间为0，则其中有一个值符合条件，这个条件就成立
                    if (fault.Duration == 0 && isValid)
                        break;

                    //如果持续时间大于0，则其中有一个值不符合条件，这个条件就不成立
                    if (fault.Duration > 0 && isValid == false)
                        break;
                }

                if (isValid == false)
                    return false;
            }
            return true;
        }


    }
}
