using FlightDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataReading.AircraftFault
{
    public class ParamCondition
    {
        /// <summary>
        /// 飞行参数（如果有下级节点，则不适用）
        /// </summary>
        public FlightParameter Parameter
        {
            get;
            set;
        }

        /// <summary>
        /// 子条件的关系运算符
        /// </summary>
        public ConditionRelation Relation
        {
            get;
            set;
        }

        /// <summary>
        /// 一个或多个判定条件，同样可以有下级判定条件
        /// </summary>
        public ParamCondition[] Conditions
        {
            get;
            set;
        }

        /// <summary>
        /// 判定运算符（如果有下级节点，则不适用）
        /// </summary>
        public Operator Operator
        {
            get;
            set;
        }

        /// <summary>
        /// 判定参数值，不同运算符有不同解析方法
        /// </summary>
        public string Value
        {
            get;
            set;
        }
    }
}
