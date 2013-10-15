using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT.Decisions
{
    /// <summary>
    /// 变化率条件处理
    /// </summary>
    public class DeltaRateSubCondition : CompareSubCondition
    {
        protected override bool IsSelfConditionMatch(int second, Dictionary<string, List<float>> rawDatas)
        {
            if (this.RootDecision == null)
                return false;

            if (this.RootDecision.IsActive)//m_conditionStartValue肯定是上一秒或更早之前
            {
                //被除数是ABS:| Delta = current-m_conditionStartValue |
                //除数是second - StartSecond，所以必须两次Active才算数
                float delta = Math.Abs(rawDatas[ParameterID][0] - m_conditionStartValue);
                float rate = delta / (second - RootDecision.ActiveStartSecond);

                switch (Operator)
                {
                    case CompareOperator.GreaterOrEqual: return rate >= ParameterValue;
                    case CompareOperator.GreaterThan: return rate > ParameterValue;
                    case CompareOperator.NotEqual: return rate != ParameterValue;
                    case CompareOperator.SmallerOrEqual: return rate <= ParameterValue;
                    case CompareOperator.SmallerThan: return rate < ParameterValue;
                    case CompareOperator.Equal: return rate == ParameterValue;
                    default: return false;
                }
            }

            return true;
        }

        private float m_conditionStartValue = 0;

        public override void AddOneSecondDatas(int second, Dictionary<string, List<float>> rawDatas)
        {
            if (this.RootDecision == null)
                return;

            if (this.RootDecision.IsActive && rawDatas.ContainsKey(ParameterID)
                && rawDatas[ParameterID].Count > 0)
            {//只要当前还不是Active，那么这个值就有可能是变化率的起始值
                //因为是先判断AllConditionTrue再设为Active的
                if (this.RootDecision.IsActive == false)
                    this.m_conditionStartValue = rawDatas[ParameterID][0];
                else this.m_conditionStartValue = 0;//置空
            }

            base.AddOneSecondDatas(second, rawDatas);
        }
    }
}
