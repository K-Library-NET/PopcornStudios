using AircraftDataAnalysisWinRT.Services;
using FlightDataEntitiesRT;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftDataAnalysisModel1.WinRT.Domain
{
    /// <summary>
    /// 把数据读取的职责从FlightAnalysisViewModel里面分离出来
    /// FlightAnalysisViewModel重新只做FlightAnalysis Page所需的功能
    /// 实现功能：
    /// 1. 保持RawData，VM根据自己需要再去Filter、转换等
    /// 2. 带有一定的缓存功能。VM不考虑缓存数据，每一次都从DataLoader里面取得数据
    /// 3. 用于数据在Page间传递，特别适合FlightAnalysis之间、FlightAnalysis和Sub的传递
    /// </summary>
    public class AircraftAnalysisDataLoader
    {
        public FlightDataEntitiesRT.Flight CurrentFlight
        {
            get;
            set;
        }

        public FlightDataEntitiesRT.AircraftModel CurrentAircraftModel
        {
            get;
            set;
        }

        private Task<string> m_task;

        public Task<string> LoadRawDataAsync(IEnumerable<string> parameterids)
        {
            this.Wait();

            Task<string> task = Task.Run<string>(
                new Func<string>(delegate()
            {
                this.LoadRawDataCore(parameterids);

                return string.Empty;
            }));

            this.m_task = task;

            return task;
        }

        private System.Collections.Concurrent.ConcurrentDictionary<string, ObservableCollection<ParameterRawData>>
            m_existsParameterDataConcurrent = new System.Collections.Concurrent.ConcurrentDictionary<string, ObservableCollection<ParameterRawData>>();

        private void LoadRawDataCore(IEnumerable<string> parameterids)
        {
            var nonexistsParameterIds = from one in parameterids
                                        where !this.m_existsParameterDataConcurrent.ContainsKey(one)
                                        select one;

            if (nonexistsParameterIds == null || nonexistsParameterIds.Count() <= 0)
                return;

            var result = ServerHelper.GetData(this.CurrentFlight, nonexistsParameterIds.ToArray(),
                0, this.CurrentFlight.EndSecond);

            foreach (var resultItem in result)
            {
                m_existsParameterDataConcurrent.TryAdd(resultItem.Key, resultItem.Value);
            }
        }

        public void Wait()
        {
            if (m_task != null)
                m_task.Wait();
        }

        public IEnumerable<ParameterRawData> GetRawData(string parameterID)
        {
            if (string.IsNullOrEmpty(parameterID))
                return null;

            if (m_existsParameterDataConcurrent.ContainsKey(parameterID))
            {
                return m_existsParameterDataConcurrent[parameterID];
            }
            else
            {//如果没有Key，可能正在加载，先Wait一下，如果还是没有，那就真的没有了
                this.Wait();
                if (m_existsParameterDataConcurrent.ContainsKey(parameterID))
                {
                    return m_existsParameterDataConcurrent[parameterID];
                }
            }

            return null;
        }
    }
}
