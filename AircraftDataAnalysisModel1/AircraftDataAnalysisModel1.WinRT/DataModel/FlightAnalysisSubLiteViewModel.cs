using AircraftDataAnalysisWinRT.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace AircraftDataAnalysisWinRT.DataModel
{
    public class FlightAnalysisSubLiteViewModel : AircraftDataAnalysisWinRT.Common.BindableBase
    {
        public FlightAnalysisSubLiteViewModel()
            : base()
        {
            this.Init();
        }

        private void Init()
        {
            var flightParameters = ApplicationContext.Instance.GetFlightParameters(
                ApplicationContext.Instance.CurrentAircraftModel);

            List<ColumnWrapper> temp = new List<ColumnWrapper>();

            if (flightParameters != null && flightParameters.Parameters != null
                && flightParameters.Parameters.Length > 0)
            {
                var ps = from par in flightParameters.Parameters
                         where par.ParameterID != "(NULL)"
                         orderby par.Index ascending, par.SubIndex descending
                         select par;

                foreach (var pm in ps)//flightParameters.Parameters)
                {
                    ColumnWrapper wrap = new ColumnWrapper(pm);
                    //this.grdData.Columns.Add(wrap.GridColumn);
                    if (wrap.ParameterID != "(NULL)")
                        temp.Add(wrap);
                    //this.ComboBoxParameterIDSource.Add(wrap);
                }
            }

            this.ComboBoxParameterIDSource = new ObservableCollection<ColumnWrapper>(temp);
        }

        private bool m_isAddCompareParameterEnable = false;

        public bool IsAddCompareParameterEnable
        {
            get
            {
                return m_isAddCompareParameterEnable;
            }
            set
            {
                this.SetProperty<bool>(ref m_isAddCompareParameterEnable, value);

                this.OnPropertyChanged("CompareParameterSerieVisibility");
            }
        }

        public Visibility CompareParameterSerieVisibility
        {
            get
            {
                if (m_isAddCompareParameterEnable && !string.IsNullOrEmpty(this.SelectedParameterID))
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }

        private string m_selectedParameterID = string.Empty;

        public string SelectedParameterID
        {
            get
            {
                return m_selectedParameterID;
            }
            set
            {
                var temp1 = m_selectedParameterID;

                this.SetProperty<string>(ref m_selectedParameterID, value);

                if (value != temp1)
                {
                    this.InvokeSelectedParameterIDDataChanged();
                }

                this.OnPropertyChanged("CompareParameterSerieVisibility");
            }
        }

        public event EventHandler SelectedParameterIDChanged;

        private void InvokeSelectedParameterIDDataChanged()
        {
            if (SelectedParameterIDChanged != null)
                SelectedParameterIDChanged(this, EventArgs.Empty);
        }

        public ObservableCollection<ColumnWrapper> ComboBoxParameterIDSource
        {
            get;
            set;
        }

        //private ExtremumReportItemWrap extremumReportItemWrap;

        //public ExtremumInfoFlightAnalysisViewModel(ExtremumReportItemWrap extremumReportItemWrap)
        //    : base()
        //{
        //    // TODO: Complete member initialization
        //    this.extremumReportItemWrap = extremumReportItemWrap;

        //    this.InitData();
        //}

        //private void InitData()
        //{
        //    if (this.extremumReportItemWrap == null)
        //    {
        //        //this.GridData = null;
        //        return;
        //    }

        //    this.RelatedParameterCollection = this.GetSelfRelatedParameterFromDecision();

        //    //拟定区间段
        //    ////根据ExtremumInfo，取得一段数据
        //    this.CurrentStartSecond = 0;
        //    this.CurrentEndSecond = ApplicationContext.Instance.CurrentFlight.EndSecond;

        //    int minSec = Math.Min(this.extremumReportItemWrap.MinValueSecond, this.extremumReportItemWrap.MaxValueSecond);
        //    int maxSec = Math.Max(this.extremumReportItemWrap.MinValueSecond, this.extremumReportItemWrap.MaxValueSecond);

        //    if (minSec > this.CurrentStartSecond)
        //    {//大概预留八分之一长度：
        //        int length = (maxSec - minSec) / 8;
        //        this.CurrentStartSecond = Math.Max(minSec - length, this.CurrentStartSecond);
        //    }
        //    if (maxSec < this.CurrentEndSecond)
        //    {//大概预留八分之一长度：
        //        int length = (maxSec - minSec) / 8;
        //        this.CurrentEndSecond = Math.Min(maxSec + length, this.CurrentEndSecond);
        //    }

        //    this.RefreshAndRetriveData();
        //}

        //private System.Collections.ObjectModel.ObservableCollection<RelatedParameterViewModel> GetSelfRelatedParameterFromDecision()
        //{
        //    var parameters = ApplicationContext.Instance.GetFlightParameters(
        //                 ApplicationContext.Instance.CurrentAircraftModel);

        //    RelatedParameterViewModel model = new RelatedParameterViewModel(this,// true,
        //        this.FindParameter(this.extremumReportItemWrap.ParameterID, parameters));

        //    var collection = new System.Collections.ObjectModel.ObservableCollection<RelatedParameterViewModel>();
        //    collection.Add(model);
        //    return collection;
        //}

        //private FlightDataEntitiesRT.FlightParameter FindParameter(string parameterId,
        //    FlightDataEntitiesRT.FlightParameters parameters)
        //{
        //    var obj = parameters.Parameters.FirstOrDefault(new Func<FlightDataEntitiesRT.FlightParameter, bool>(
        //        delegate(FlightDataEntitiesRT.FlightParameter para)
        //        {
        //            if (para.ParameterID == parameterId)
        //                return true;
        //            return false;
        //        }));

        //    return obj;
        //}

        public AircraftDataAnalysisModel1.WinRT.DataModel.FlightAnalysisChartSerieViewModel ChartViewModel { get; set; }

        public string Group1ID { get; set; }

        public string Group2ID { get; set; }
    }

    //[Obsolete]
    public class ColumnWrapper : AircraftDataAnalysisWinRT.Common.BindableBase
    {
        private FlightDataEntitiesRT.FlightParameter m_parameter;
        //private bool m_IsParameterHidden;

        public ColumnWrapper(FlightDataEntitiesRT.FlightParameter pm)
        {
            // TODO: Complete member initialization
            this.m_parameter = pm;

            //this.GridColumn = new Syncfusion.UI.Xaml.Grid.GridTextColumn()
            //{
            //    MappingName = pm.ParameterID,
            //    ColumnSizer = Syncfusion.UI.Xaml.Grid.GridLengthUnitType.Auto,
            //    HeaderText = ApplicationContext.Instance.GetFlightParameterCaption(pm.ParameterID),
            //    TextAlignment = TextAlignment.Center,
            //    HorizontalHeaderContentAlignment = HorizontalAlignment.Center,
            //    IsHidden = this.IsParameterHidden,
            //};
        }

        public string Caption
        {
            get
            {
                return this.m_parameter.Caption;
            }
        }

        public string ParameterID
        {
            get
            {
                return this.m_parameter.ParameterID;
            }
        }

        //public Syncfusion.UI.Xaml.Grid.GridColumn GridColumn { get; set; }

        //public bool IsParameterHidden
        //{
        //    get
        //    {
        //        return this.m_IsParameterHidden;
        //    }
        //    set
        //    {
        //        this.SetProperty<bool>(ref this.m_IsParameterHidden, value);
        //        if (this.GridColumn != null)
        //            this.GridColumn.IsHidden = this.IsParameterHidden;
        //    }
        //}
    }
}
