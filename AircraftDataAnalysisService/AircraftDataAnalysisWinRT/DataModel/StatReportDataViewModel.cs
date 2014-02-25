using AircraftDataAnalysisWinRT.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace AircraftDataAnalysisWinRT.DataModel
{
    public class StatReportDataViewModel : BindableBase
    {
        private StatReportViewModel statReportViewModel;

        public StatReportDataViewModel(StatReportViewModel statReportViewModel)
        {
            // TODO: Complete member initialization
            this.statReportViewModel = statReportViewModel;

            this.PieChartViewModel = new ObservableCollection<ISummaryItem>();
        }

        public ObservableCollection<ISummaryItem> PieChartViewModel
        {
            get;
            set;
        }

        public ObservableCollection<StackColumnChartData> StackColumnChartViewModel
        {
            get;
            set;
        }

        public ObservableCollection<PieChartDetailData> PieChartDetails
        {
            get;
            set;
        }

        public ObservableCollection<ColumnChartDetailData> ColumnChartDetails
        {
            get;
            set;
        }
    }


    public interface ISummaryItem
    {
        string CategoryID { get; set; }
        string CategoryName { get; set; }
        double SummaryValue { get; set; }

        int Order { get; set; }
    }

    class SummaryItem : ISummaryItem
    {
        public string CategoryID
        {
            get;
            set;
        }

        public string CategoryName
        {
            get;
            set;
        }

        public virtual double SummaryValue
        {
            get;
            set;
        }

        public int Order
        {
            get;
            set;
        }
    }

    class SummaryGroup : ObservableCollection<SummaryItem>, Windows.UI.Xaml.Data.ISupportIncrementalLoading
    {
        public SummaryGroup()
        {
            this.CollectionChanged += SummaryGroup_CollectionChanged;
        }

        public SummaryGroup(System.Collections.Generic.IEnumerable<SummaryItem> items)
            : base(items)
        {
            this.CollectionChanged += SummaryGroup_CollectionChanged;

            this.GenerateGroupItems();
        }

        private ObservableCollection<SummaryGroupItem> m_groupItems = null;

        void SummaryGroup_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (m_groupItems == null)
                return;

            this.GenerateGroupItems();
            //throw new NotImplementedException();
        }

        private void GenerateGroupItems()
        {
            var maplist = from one in this.Items
                          group one by one.CategoryID into gp
                          select gp;

            List<SummaryGroupItem> gis = new List<SummaryGroupItem>();
            foreach (var item in maplist)
            {
                SummaryGroupItem group = new SummaryGroupItem() { CategoryID = item.Key, CategoryName = item.First().CategoryName, Order = item.First().Order };
                group.AddItems(item.ToArray());
            }

            this.m_groupItems = new ObservableCollection<SummaryGroupItem>(gis);
        }

        public ObservableCollection<SummaryGroupItem> GroupItems
        {
            get
            {
                return m_groupItems;
            }
        }

        public bool HasMoreItems
        {
            get { throw new NotImplementedException(); }
        }

        public Windows.Foundation.IAsyncOperation<Windows.UI.Xaml.Data.LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            IAsyncOperation<LoadMoreItemsResult> loader = this.GetResultAsyncLoader(count);
            return loader;
        }

        private IAsyncOperation<LoadMoreItemsResult> GetResultAsyncLoader(uint count)
        {
            return new ResultAsyncLoader(count);
        }
    }

    class ResultAsyncLoader : IAsyncOperation<LoadMoreItemsResult>
    {
        public ResultAsyncLoader(uint count)
        {
            Task task = Task.Run(new Action(delegate()
            {

            }));

            this.m_task = task;

            this._asyncStatus = AsyncStatus.Completed;
            if (this.Completed != null)
                this.Completed(this, this._asyncStatus);
        }

        private AsyncStatus _asyncStatus = AsyncStatus.Started;
        private Task m_task;
        private LoadMoreItemsResult m_result;

        public AsyncOperationCompletedHandler<LoadMoreItemsResult> Completed
        {
            get;
            set;
        }

        public LoadMoreItemsResult GetResults()
        {
            if (m_task != null)
                m_task.Wait();
            return m_result;
        }

        public void Cancel()
        {
            this._asyncStatus = AsyncStatus.Canceled;
        }

        public void Close()
        {
            try
            {
                if (this.m_task != null)
                {
                    this.m_task.AsAsyncAction().Close();
                }
            }
            catch (Exception e)
            {
                this.m_exception = e;
            }
        }

        public Exception ErrorCode
        {
            get { return this.m_exception; }
        }

        public uint Id
        {
            get { throw new NotImplementedException(); }
        }

        public AsyncStatus Status
        {
            get { return this._asyncStatus; }
        }

        public Exception m_exception { get; set; }
    }

    class SummaryGroupItem : SummaryItem, ISummaryItem
    {
        List<ISummaryItem> m_subItems = new List<ISummaryItem>();

        internal void AddItems(SummaryItem[] summaryItem)
        {
            foreach (var item in summaryItem)
            {
                if (item != null && !string.IsNullOrEmpty(item.CategoryID) &&
                    string.Equals(item.CategoryID, this.CategoryID))
                {
                    this.m_subItems.Add(item);
                }
            }
        }

        public override double SummaryValue
        {
            get
            {
                var summary = this.m_subItems.Sum<ISummaryItem>(new Func<ISummaryItem, decimal?>(
                    delegate(ISummaryItem item)
                    {
                        if (item != null)
                            return Convert.ToDecimal(item.SummaryValue);
                        return 0;
                    }));

                return Convert.ToDouble(summary);
            }
            set
            {
                // base.SummaryValue = value;
            }
        }
    }



    public class StackColumnChartData
    {
        /// <summary>
        /// 横轴X轴标记
        /// </summary>
        public string Category
        {
            get;
            set;
        }

        /// <summary>
        /// 停车通电状态
        /// </summary>
        public double Condition1
        {
            get;
            set;
        }

        /// <summary>
        /// 发动机地面开车状态
        /// </summary>
        public double Condition2
        {
            get;
            set;
        }

        /// <summary>
        /// 正常飞行状态
        /// </summary>
        public double Condition3
        {
            get;
            set;
        }

        /// <summary>
        /// 最大军用转速状态
        /// </summary>
        public double Condition4
        {
            get;
            set;
        }

        public double Condition5
        {
            get;
            set;
        }
    }

    public class PieChartDetailData
    {

        public string CategoryName
        {
            get;
            set;
        }

        public double SummaryValue
        {
            get;
            set;
        }

        public double Partition
        {
            get;
            set;
        }
    }

    public class ColumnChartDetailData
    {
        public string CategoryName
        {
            get;
            set;
        }

        public double Janurary
        {
            get;
            set;
        }

        public double February
        {
            get;
            set;
        }

        public double March
        {
            get;
            set;
        }

        public double April
        {
            get;
            set;
        }

        public double May
        {
            get;
            set;
        }

        public double June
        {
            get;
            set;
        }

        public double July
        {
            get;
            set;
        }

        public double August
        {
            get;
            set;
        }

        public double September
        {
            get;
            set;
        }

        public double October
        {
            get;
            set;
        }

        public double November
        {
            get;
            set;
        }

        public double December
        {
            get;
            set;
        }
    }
}
