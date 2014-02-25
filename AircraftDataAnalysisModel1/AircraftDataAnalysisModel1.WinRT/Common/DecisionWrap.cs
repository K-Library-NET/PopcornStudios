using FlightDataEntitiesRT.Decisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftDataAnalysisWinRT.Common
{
    /// <summary>
    /// 用于封装判据展示
    /// </summary>
    public class DecisionWrap
    {
        private DecisionRecord one;

        public DecisionRecord Record
        {
            get { return one; }
            set { one = value; }
        }

        private Decision decision;

        public Decision Decision
        {
            get { return decision; }
            set { decision = value; }
        }

        public int EventLevel
        {
            get
            {
                if (this.Record != null)
                    return Record.EventLevel;
                return -1;
            }
        }

        /// <summary>
        /// 判据封装类
        /// </summary>
        /// <param name="one">如果这个是空则说明是空判据</param>
        /// <param name="decision"></param>
        public DecisionWrap(DecisionRecord one, Decision decision)
        {
            // TODO: Complete member initialization
            this.one = one;
            this.decision = decision;
        }

        public string DecisionID
        {
            get
            {
                if (this.decision != null)
                    return this.decision.DecisionID;
                return string.Empty;
            }
        }

        public string StartTime
        {
            get
            {
                if (this.Record != null)
                {
                    TimeSpan span = new TimeSpan(0, 0, this.Record.StartSecond);
                    return span.ToString();
                }
                return "00:00:00";
            }
        }

        public string EndTime
        {
            get
            {
                if (this.Record != null)
                {
                    TimeSpan span = new TimeSpan(0, 0, this.Record.EndSecond);
                    return span.ToString();
                }
                return "00:00:00";
            }
        }

        public TimeSpan StartTimeSpan
        {
            get
            {
                if (this.Record != null)
                {
                    TimeSpan span = new TimeSpan(0, 0, this.Record.StartSecond);
                    return span;
                }
                return new TimeSpan();
            }
        }

        public TimeSpan EndTimeSpan
        {
            get
            {
                if (this.Record != null)
                {
                    TimeSpan span = new TimeSpan(0, 0, this.Record.EndSecond);
                    return span;
                }
                return new TimeSpan();
            }
        }

        public string Description
        {
            get
            {
                string template = "{0}：{1}";

                if (this.Record != null)
                    return string.Format(template, this.Decision.DecisionName, this.Record.DecisionDescription);

                return string.Format(template, this.Decision.DecisionName, "(此故障未发生)");
            }
        }
    }
}
