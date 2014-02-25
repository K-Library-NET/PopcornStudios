using PopcornStudio.MetroTrainInterop.MapTableCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopcornStudio.MetroTrainInterop
{
    /// <summary>
    /// 默认查找路线的策略，通过对应表直接查找
    /// </summary>
    public class ThroughPathCalcStrategy : PopcornStudio.MetroTrainInterop.IThroughPathCalcStrategy
    {
        private MetroPath m_metroPath;

        public ThroughPathCalcStrategy(MetroPath metroPath)
        {
            // TODO: Complete member initialization
            this.m_metroPath = metroPath;
        }

        public List<ThroughPath> CalcThroughPaths()
        {
            string cityName = m_metroPath.CityName;

            var table = MetroStationMapTable.GetTable(cityName);
            if (table != null)
            {
                var result = table.CalcPath(m_metroPath.StartStationName, m_metroPath.EndStationName);
                if (result != null && result.Length > 0)
                    return new List<ThroughPath>(result);
            }

            return null;
        }

    }
}
