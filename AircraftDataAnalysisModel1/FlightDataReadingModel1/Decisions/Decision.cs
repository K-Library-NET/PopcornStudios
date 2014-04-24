using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT.Decisions
{
    public class Decision : IDecision
    {
        private bool m_isActive = false;
        public bool IsActive
        {
            get { return m_isActive; }
            internal set
            {
                m_isActive = value;
            }
        }

        private int m_activeStartSecond = 0;

        public int ActiveStartSecond
        {
            get { return m_activeStartSecond; }
            internal set
            {
                m_activeStartSecond = value;
            }
        }

        private int m_activeEndSecond = 0;

        public int ActiveEndSecond
        {
            get { return m_activeEndSecond; }
            internal set
            {
                m_activeEndSecond = value;
            }
        }

        private bool m_hasHappend = false;

        public bool HasHappened
        {
            get { return m_hasHappend; }
            internal set
            {
                m_hasHappend = value;
            }
        }

        private int m_happenedSecond = 0;

        public int HappenedSecond
        {
            get { return m_happenedSecond; }
            internal set { m_happenedSecond = value; }
        }

        public void AddOneSecondDatas(int second, ParameterRawData[] rawDatas)
        {
            Dictionary<string, List<float>> rawDataDics = new Dictionary<string, List<float>>();
            foreach (var raw in rawDatas)
            {
                if (!rawDataDics.ContainsKey(raw.ParameterID))
                    rawDataDics.Add(raw.ParameterID, new List<float>());

                rawDataDics[raw.ParameterID].AddRange(raw.Values);
            }

            foreach (var con in this.Conditions)
                con.AddOneSecondDatas(second, rawDataDics);

            if (AllConditionTrue())
            {//先设置为Active，不管持续时间够不够长
                if (this.IsActive == false)
                {
                    this.ActiveStartSecond = second;
                    this.IsActive = true;
                }
            }
            else
            {//没有Active了
                if (this.IsActive)
                {
                    this.IsActive = false;
                    this.ActiveEndSecond = second;
                }
            }

            if (AllConditionTrue() && this.LastTime > 0
                && (second - this.ActiveStartSecond >= this.LastTime))
            {//所有条件都发生，并且大于等于持续时间，则认为真正发生了
                if (HasHappened)
                {
                }
                else
                {
                    this.HappenedSecond = second;
                    HasHappened = true;
                }
            }
            else
            {
                HasHappened = false;
            }

            //如果有子条件则自身不算
            return;
        }

        private bool AllConditionTrue()
        {
            if (this.Conditions != null && this.Conditions.Length > 0)
            {
                foreach (var con in this.Conditions)
                {
                    if (con.ConditionTrue == false)
                        return false;
                }

                return true;
            }
            return false;
        }

        public SubCondition[] Conditions
        {
            get;
            set;
        }

        public int LastTime
        {
            get;
            set;
        }

        public string DecisionID { get; set; }

        public string DecisionName { get; set; }

        /// <summary>
        /// 事件等级，不包含事件颜色，颜色是前端根据事件等级设定
        /// </summary>
        public int EventLevel { get; set; }

        public string[] RelatedParameters { get; set; }

        public string ToDecisionDescriptionString(DecisionRecord record)
        {
            //   "左发排气温度=000℃>630℃，dT=00s≥1s"
            string template = this.DecisionDescriptionStringTemplate;// "@@T6L#=##T6L@℃>630℃，dT=##dT@s≥1s";
            if (string.IsNullOrEmpty(template))
                return record.ToString();
            //暂时只做单层算了
            foreach (var sub in this.Conditions)
            {
                string paramChar = string.Format("@@{0}#", sub.ParameterID);
                string paramValueChar = string.Format("##{0}@", sub.ParameterID);
                if (template.Contains(paramChar) && template.Contains(paramValueChar))
                {
                    template = template.Replace(paramChar, this.GetParameterCaption(sub.ParameterID));
                    template = template.Replace(paramValueChar, this.GetParameterValue(sub.ParameterValue));
                }
            }
            if (template.Contains("##dT@"))
                template = template.Replace("##dT@", Convert.ToString(record.EndSecond - record.StartSecond));

            return template;
        }

        public IEnumerable<FlightParameter> ParameterObjects
        {
            get;
            set;
        }

        private string GetParameterCaption(string parameterID)
        {
            if (ParameterObjects != null || ParameterObjects.Count() > 0)
            {
                var par = ParameterObjects.Single(
                     new Func<FlightParameter, bool>(delegate(FlightParameter fp)
                 {
                     if (fp != null && fp.ParameterID == parameterID)
                         return true;
                     return false;
                 }));

                if (par != null)
                    return par.Caption;
            }
            return parameterID;
        }

        /// <summary>
        /// 通常保留两位小数
        /// </summary>
        /// <param name="pValue"></param>
        /// <returns></returns>
        private string GetParameterValue(float pValue)
        {
            var values = Math.Round(pValue);
            return values.ToString();
        }

        public string DecisionDescriptionStringTemplate
        {
            get;
            set;
        }

        public string SolutionInstruction
        {
            get;
            set;
        }
    }
}
