using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopcornStudio.MetroTrainInterop.MapTableCalc
{
    public class MetroStationMapTable
    {
        public ThroughLine[] ThroughLines
        {
            get;
            set;
        }

        /// <summary>
        /// 根据城市名返回一个对应表
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns></returns>
        public static MetroStationMapTable GetTable(string cityName)
        {
            if (m_weakReferenceTables.ContainsKey(cityName))
                return m_weakReferenceTables[cityName];

            MetroStationMapTable table = TryConstructTableByCityName(cityName);

            if (table != null)
            {
                m_weakReferenceTables.Add(cityName, table);
            }

            return table;
        }

        /// <summary>
        /// 读取对应表
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns></returns>
        private static MetroStationMapTable TryConstructTableByCityName(string cityName)
        {//TODO: 读取静态资源构造表
            MetroStationMapTable table = new MetroStationMapTable(cityName);

            //debug:加测试数据，之后要转换成真实数据
            ThroughPath path = new ThroughPath();
            //path.ThroughNodes.Add(new ThroughPathNode() { StationName = "番禺广场" });
            //path.ThroughNodes.Add(new ThroughPathNode() { StationName = "珠江新城" });
            //path.ThroughNodes.Add(new ThroughPathNode() { StationName = "科韵路" });
            path.Price = 6;

            table.m_pathMap.Add(BuildKey("番禺广场", "科韵路"), new ThroughPath[] { path });

            return table;
        }

        private static Dictionary<string, MetroStationMapTable> m_weakReferenceTables
            = new Dictionary<string, MetroStationMapTable>();

        public static string BuildKey(string startStationName, string endStationName)
        {
            return string.Format("{0} -> {1}", startStationName, endStationName);
        }

        private Dictionary<string, ThroughPath[]> m_pathMap = new Dictionary<string, ThroughPath[]>();
        private string m_cityName = string.Empty;

        public string CityName
        {
            get { return m_cityName; }
            //set { m_cityName = value; }
        }

        public MetroStationMapTable(string cityName)
        {
            // TODO: Complete member initialization
            this.m_cityName = cityName;
        }

        public ThroughPath[] CalcPath(string startStation, string endStation)
        {
            string key = BuildKey(startStation, endStation);
            if (m_pathMap.ContainsKey(key))
                return m_pathMap[key];

            return new ThroughPath[] { };
        }
    }
}
