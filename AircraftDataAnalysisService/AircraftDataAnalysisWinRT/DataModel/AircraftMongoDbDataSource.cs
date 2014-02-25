using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftDataAnalysisWinRT.DataModel
{
    public sealed class AircraftMongoDbDataSource
    {
        private static AircraftMongoDbDataSource m_rootSource = new AircraftMongoDbDataSource();

        public AircraftMongoDbDataSource()
        {
            AircraftService.AircraftServiceClient client = new AircraftService.AircraftServiceClient();
            //Task<ObservableCollection<AircraftService.AircraftModel>> modelsTask = client.GetAllAircraftModelsAsync();

            //modelsTask.Wait();

            //this.BindGroups(modelsTask.Result);
                //new Action<Task<ObservableCollection<AircraftService.AircraftModel>>>(
                //    delegate(Task<ObservableCollection<AircraftService.AircraftModel>> task)
                //    {
                //        if (task.IsCompleted)
                //        {
                //            var result = task.Result;
                //            this.BindGroups(result);
                //        }
                //    }));
        }

        private void BindGroups(ObservableCollection<AircraftService.AircraftModel> result)
        {
            this._allGroups.Clear();
            foreach (AircraftService.AircraftModel model in result)
            {
                var group = this.GetGroupFromModel(model);
                if (group == null)
                    continue;
            }
        }

        private AircraftModelDataGroup GetGroupFromModel(AircraftService.AircraftModel model)
        {
            AircraftModelDataGroup group = new AircraftModelDataGroup(
                model.ModelName, model.Caption, model.LastUsed.ToString("yyyy-MM-dd HH:mm:ss"), string.Empty,
                string.Empty);

            return group;
        }

        private ObservableCollection<AircraftModelDataGroup> _allGroups = new ObservableCollection<AircraftModelDataGroup>();
        public ObservableCollection<AircraftModelDataGroup> AllGroups
        {
            get { return this._allGroups; }
        }

        public static IEnumerable<AircraftModelDataGroup> GetGroups(string uniqueId)
        {
            //if (!uniqueId.Equals("AllGroups")) throw new ArgumentException("Only 'AllGroups' is supported as a collection of groups");

            return m_rootSource.AllGroups;
        }

        public static AircraftModelDataGroup GetGroup(string uniqueId)
        {
            // 对于小型数据集可接受简单线性搜索
            var matches = m_rootSource.AllGroups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        //public static FaultDiagnosisViewModel GetItem(string uniqueId)
        //{
        //    // 对于小型数据集可接受简单线性搜索
        //    var matches = m_rootSource.AllGroups.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
        //    if (matches.Count() == 1) return matches.First();
        //    return null;
        //}
    }
}
