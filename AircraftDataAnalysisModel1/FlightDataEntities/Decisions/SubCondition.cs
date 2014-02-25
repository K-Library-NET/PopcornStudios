using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntities.Decisions
{
    [DataContract]
    public class SubCondition
    {
        [DataMember]
        public Decision RootDecision
        {
            get;
            set;
        }

        [DataMember]
        public float ParameterValue
        {
            get;
            set;
        }

        [DataMember]
        public string ParameterID
        {
            get;
            set;
        }

        [DataMember]
        public ConditionRelation Relation
        {
            get;
            set;
        }

        [DataMember]
        public SubCondition[] SubConditions
        {
            get;
            set;
        }

        [DataMember]
        public SubConditionType ConditionType
        {
            get;
            set;
        }

        [DataMember]
        public CompareOperator Operator
        {
            get;
            set;
        }
    }
}
