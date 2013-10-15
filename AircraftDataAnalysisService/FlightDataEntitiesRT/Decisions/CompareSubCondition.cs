using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT.Decisions
{
    /// <summary>
    /// 大于小于等于不等于……
    /// 各种对比条件
    /// </summary>
    public class CompareSubCondition : SubCondition
    {
        protected override bool IsSelfConditionMatch(int second, Dictionary<string, List<float>> rawDatas)
        {
            if (this.RootDecision == null)
                return false;

            if (string.IsNullOrEmpty(ParameterID) || !rawDatas.ContainsKey(ParameterID))
                return false;

            if (rawDatas[ParameterID].Count <= 0)
                //没数据也认为异常
                return true;

            switch (Operator)
            {
                case CompareOperator.GreaterOrEqual: return rawDatas[ParameterID][0] >= ParameterValue;
                case CompareOperator.GreaterThan: return rawDatas[ParameterID][0] > ParameterValue;
                case CompareOperator.NotEqual: return rawDatas[ParameterID][0] != ParameterValue;
                case CompareOperator.SmallerOrEqual: return rawDatas[ParameterID][0] <= ParameterValue;
                case CompareOperator.SmallerThan: return rawDatas[ParameterID][0] < ParameterValue;
                case CompareOperator.Equal: return rawDatas[ParameterID][0] == ParameterValue;
                default: return false;
            }
        }

        public CompareOperator Operator
        {
            get;
            set;
        }
    }
}
