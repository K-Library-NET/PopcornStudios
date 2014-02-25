using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace AircraftDataAnalysisWinRT.DataModel
{
    public class AircraftModelDataGroup : DataCommon
    {
        public AircraftModelDataGroup(String uniqueId, String title, String subtitle,
            String imagePath, String description)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
            Items.CollectionChanged += ItemsCollectionChanged;
        }

        private void ItemsCollectionChanged(object sender,
            System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // 由于两个原因提供要从 GroupedItemsPage 绑定到的完整
            // 项集合的子集: GridView 不会虚拟化大型项集合，并且它
            // 可在浏览包含大量项的组时改进用户
            // 体验。
            //
            // 最多显示 12 项，因为无论显示 1、2、3、4 还是 6 行，
            // 它都生成填充网格列

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewStartingIndex < 12)
                    {
                        TopItems.Insert(e.NewStartingIndex, Items[e.NewStartingIndex]);
                        if (TopItems.Count > 12)
                        {
                            TopItems.RemoveAt(12);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Move:
                    if (e.OldStartingIndex < 12 && e.NewStartingIndex < 12)
                    {
                        TopItems.Move(e.OldStartingIndex, e.NewStartingIndex);
                    }
                    else if (e.OldStartingIndex < 12)
                    {
                        TopItems.RemoveAt(e.OldStartingIndex);
                        TopItems.Add(Items[11]);
                    }
                    else if (e.NewStartingIndex < 12)
                    {
                        TopItems.Insert(e.NewStartingIndex, Items[e.NewStartingIndex]);
                        TopItems.RemoveAt(12);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldStartingIndex < 12)
                    {
                        TopItems.RemoveAt(e.OldStartingIndex);
                        if (Items.Count >= 12)
                        {
                            TopItems.Add(Items[11]);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    if (e.OldStartingIndex < 12)
                    {
                        TopItems[e.OldStartingIndex] = Items[e.OldStartingIndex];
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    TopItems.Clear();
                    while (TopItems.Count < Items.Count && TopItems.Count < 12)
                    {
                        TopItems.Add(Items[TopItems.Count]);
                    }
                    break;
            }
        }

        private ObservableCollection<FaultDiagnosisViewModel> _items = new ObservableCollection<FaultDiagnosisViewModel>();
        public ObservableCollection<FaultDiagnosisViewModel> Items
        {
            get { return this._items; }
        }

        private ObservableCollection<FaultDiagnosisViewModel> _topItem = new ObservableCollection<FaultDiagnosisViewModel>();
        public ObservableCollection<FaultDiagnosisViewModel> TopItems
        {
            get { return this._topItem; }
        }
    }
}
