using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FlightDataEntities.Fault
{
    public class AircraftFault
    {
        public AircraftFault()
        {
            this.ParamConditions = new List<ParameterCondition>();
        }

        public List<ParameterCondition> ParamConditions { get; private set; }
        public float Duration { get; set; }
        public int EventLevel { get; set; }
        public string Title { get; set; }
        public string EventHandling { get; set; }
        public string Remark { get; set; }

        public static List<AircraftFault> GetFaults()
        {
            //从XML获取故障信息
            return GetFaultFromXML();

            //在代码里获取故障信息
            //return GetFaultList();
        }

        private static List<AircraftFault> GetFaultFromXML()
        {
            List<AircraftFault> list = new List<AircraftFault>();

            XmlDocument doc = new XmlDocument();
            doc.Load("AircraftFault.xml");
                //Application.StartupPath+ "\\AircraftFault.xml");
            XmlNodeList nodes = doc.DocumentElement.SelectNodes("Fault");
            foreach (XmlNode n in nodes)
            {
                AircraftFault fault = new AircraftFault();
                fault.Title = n.Attributes["title"].Value;
                fault.Duration = Convert.ToInt32(n.Attributes["duratioin"].Value);
                fault.EventLevel = Convert.ToInt32(n.Attributes["EventLevel"].Value);
                fault.EventHandling = n.Attributes["EventHandling"].Value;
                fault.Remark = n.Attributes["Remark"].Value;

                XmlNodeList conditionNodes = n.SelectNodes("ParameterCondition");
                foreach (XmlNode cnode in conditionNodes)
                {
                    ParameterCondition pc = new ParameterCondition();
                    pc.Parameter = FlyParameter.Parameters[cnode.Attributes["ParameterKey"].Value];
                    float value = Convert.ToSingle(cnode.Attributes["value"].Value);
                    switch (cnode.Attributes["condition"].Value.ToLower())
                    {
                        case "大于":
                            pc.Condition = new LargeCondition { BottomBoundary = value, IncludeBottom = false };
                            break;
                        case "大于等于":
                            pc.Condition = new LargeCondition { BottomBoundary = value, IncludeBottom = true };
                            break;
                        case "小于":
                            pc.Condition = new SmallCondition { TopBoundary = value, IncludeTop = false };
                            break;
                        case "小于等于":
                            pc.Condition = new SmallCondition { TopBoundary = value, IncludeTop = true };
                            break;
                        case "等于":
                            pc.Condition = new EqualCondition { EqualValue = value };
                            break;
                        case "is":
                            pc.Condition = new BooleanCondition();
                            break;
                        case "范围":
                            float value2 = Convert.ToSingle(cnode.Attributes["value2"].Value);
                            bool top = Convert.ToBoolean(cnode.Attributes["top"].Value);
                            bool bottom = Convert.ToBoolean(cnode.Attributes["bottom"].Value);
                            bool nop = Convert.ToBoolean(cnode.Attributes["nop"].Value);
                            pc.Condition = new BoundCondition { TopBoundary = value2, BottomBoundary = value, IncludeTop = top, IncludeBottom = bottom, Nop = nop };
                            break;
                    }
                    fault.ParamConditions.Add(pc);
                }
                list.Add(fault);
            }
            return list;
        }

        private static List<AircraftFault> GetFaultList()
        {
            List<AircraftFault> list = new List<AircraftFault>();
            AircraftFault fault = new AircraftFault();

            ParameterCondition pc = new ParameterCondition();
            pc.Parameter = new FlyParameter { Index = 77, Caption = "俯仰角", Frequence = 4 };
            pc.Condition = new LargeCondition { BottomBoundary = 11, IncludeBottom = false };
            fault.ParamConditions.Add(pc);

            pc = new ParameterCondition();
            pc.Parameter = new FlyParameter { Index = 223, Caption = "左发高压转子转速", Frequence = 4 };
            pc.Condition = new LargeCondition { BottomBoundary = 0.95F, IncludeBottom = false };
            fault.ParamConditions.Add(pc);

            pc = new ParameterCondition();
            pc.Parameter = new FlyParameter { Index = 225, Caption = "右发高压转子转速", Frequence = 4 };
            pc.Condition = new LargeCondition { BottomBoundary = 0.95F, IncludeBottom = false };
            fault.ParamConditions.Add(pc);

            fault.Title = "起飞时仰角大";
            fault.Duration = 1;
            fault.EventLevel = 2;
            fault.EventHandling = "（1）通知训练部门（2）查看飞机腹鳍是否擦伤";

            list.Add(fault);

            return list;
        }
    }
}
