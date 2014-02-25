using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT.Decisions
{
    public abstract class SubCondition
    {
        public Decision RootDecision
        {
            get;
            set;
        }

        public float ParameterValue
        {
            get;
            set;
        }

        public string ParameterID
        {
            get;
            set;
        }

        public bool ConditionTrue
        {
            get
            {
                if (SubConditions != null && SubConditions.Length > 0)
                {
                    if (Relation == ConditionRelation.OR)
                    {
                        foreach (var con in SubConditions)
                        {//OR关系一个成功则全部成功
                            if (con.ConditionTrue)
                                return true;
                        }
                    }
                    else
                    {
                        foreach (var con in SubConditions)
                        {//AND关系一个失败则全部失败
                            if (con.ConditionTrue == false)
                                return false;
                        }
                    }
                }

                return m_selfCondition;
            }
        }

        private bool m_selfCondition = false;

        public bool SelfCondition
        {
            get { return m_selfCondition; }
            internal set { m_selfCondition = value; }
        }

        public virtual void AddOneSecondDatas(int second, Dictionary<string, List<float>> rawDatas)
        {
            if (this.SubConditions != null && this.SubConditions.Length > 0)
            {
                foreach (var con in this.SubConditions)
                    con.AddOneSecondDatas(second, rawDatas);

                //如果有子条件则自身不算
                return;
            }
            else
            {
                if (this.IsSelfConditionMatch(second, rawDatas))
                {
                    this.SelfCondition = true;
                }
                else
                {
                    this.SelfCondition = false;
                }
            }
        }

        /// <summary>
        /// 只判断自身，不要判断下级别
        /// </summary>
        /// <param name="second"></param>
        /// <param name="rawDatas"></param>
        /// <returns></returns>
        protected abstract bool IsSelfConditionMatch(int second, Dictionary<string, List<float>> rawDatas);

        public ConditionRelation Relation
        {
            get;
            set;
        }

        public SubCondition[] SubConditions
        {
            get;
            set;
        }
    }
}
