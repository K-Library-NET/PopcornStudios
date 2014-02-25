using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftDataAnalysisWinRT.MyControl
{
    public class FAChartModel
    {
        public FAChartModel()
        {

        }

        private Dictionary<string, FAChartSubModel> m_subModels = new Dictionary<string, FAChartSubModel>();

        public Dictionary<string, FAChartSubModel> SubModels
        {
            get
            {
                return m_subModels;
            }
        }
    }

    public class FAChartSubModel : ObservableCollection<FAChartItem>
    {
        private IEnumerable<FAChartItem> enumerable;

        public FAChartSubModel()
        {

        }

        public FAChartSubModel(IEnumerable<FAChartItem> enumerable)
            : base(enumerable)
        {
        }

        public string ParameterID { get; set; }
    }

    public class NHFAChartSubModel : FAChartSubModel
    {
        public NHFAChartSubModel()
        {

        }

        public NHFAChartSubModel(IEnumerable<NHFAChartItem> sorted)
            : base(NHFAChartSubModel.CastToBase(sorted))
        {
            // TODO: Complete member initialization
            //this.sorted = sorted;
        }

        public static IEnumerable<FAChartItem> CastToBase(IEnumerable<NHFAChartItem> items)
        {
            var converted = from one in items
                            select one as NHFAChartItem;

            return converted;
        }
    }

    public class T6FAChartSubModel : FAChartSubModel
    {
        public T6FAChartSubModel()
        {
        }

        public T6FAChartSubModel(IEnumerable<T6FAChartItem> sorted)
            : base(T6FAChartSubModel.CastToBase(sorted))
        {
            // TODO: Complete member initialization
            //this.sorted = sorted;
        }

        public static IEnumerable<FAChartItem> CastToBase(IEnumerable<T6FAChartItem> items)
        {
            var converted = from one in items
                            select one as T6FAChartItem;

            return converted;
        }
    }

    public class KGFAChartSubModel : FAChartSubModel
    {
        public KGFAChartSubModel()
        {

        }

        public KGFAChartSubModel(IEnumerable<KGFAChartItem> sorted)
            : base(KGFAChartSubModel.CastToBase(sorted))
        {
            // TODO: Complete member initialization
            //this.sorted = sorted;
        }

        public static IEnumerable<FAChartItem> CastToBase(IEnumerable<KGFAChartItem> items)
        {
            var converted = from one in items
                            select one as FAChartItem;

            return converted;
        }
    }

    public class FAChartItem
    {

        public double XValue { get; set; }

        public double YValue { get; set; }
    }

    public class NHFAChartItem : FAChartItem
    {
        public double NHL { get; set; }

        public double NHR { get; set; }
    }

    public class T6FAChartItem : FAChartItem
    {
        public double T6L { get; set; }

        public double T6R { get; set; }
    }

    public class KGFAChartItem : FAChartItem
    {
        public double KG1 { get; set; }
        public double KG2 { get; set; }
        public double KG3 { get; set; }
        public double KG4 { get; set; }
        public double KG5 { get; set; }
        public double KG6 { get; set; }
        public double KG7 { get; set; }
        public double KG8 { get; set; }
        public double KG9 { get; set; }
        public double KG10 { get; set; }
        public double KG11 { get; set; }
        public double KG12 { get; set; }
        public double KG13 { get; set; }
        public double KG14 { get; set; }
        public double KG15 { get; set; }
    }
}
