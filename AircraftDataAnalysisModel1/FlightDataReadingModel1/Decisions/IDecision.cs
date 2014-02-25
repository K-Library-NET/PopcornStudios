using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT.Decisions
{
    public interface IDecision
    {
        bool IsActive
        {
            get;
        }

        int ActiveStartSecond
        {
            get;
        }

        int ActiveEndSecond
        {
            get;
        }

        bool HasHappened
        {
            get;
        }

        int HappenedSecond
        {
            get;
        }

        SubCondition[] Conditions
        {
            get;
            set;
        }
        
        /// <summary>
        /// 如果是小于等于0，则不适用，否则视为持续时间秒
        /// </summary>
        int LastTime
        {
            get;
            set;
        }

        void AddOneSecondDatas(int second, ParameterRawData[] rawDatas);
    }
}
