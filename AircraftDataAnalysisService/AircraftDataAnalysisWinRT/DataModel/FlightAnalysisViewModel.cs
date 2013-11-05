using FlightDataEntitiesRT;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//[obsoleted(转移到domain）
namespace AircraftDataAnalysisWinRT.DataModel
{
    //public class FlightAnalysisViewModel : AircraftDataAnalysisWinRT.Common.BindableBase
    //{
    //    public FlightAnalysisViewModel()
    //    {
    //    }

    //    /// <summary>
    //    /// 根据起始、结束时间，相关的参数刷新数据
    //    /// </summary>
    //    public void RefreshAndRetriveData()
    //    {
    //        DataTable dt = AircraftDataAnalysisWinRT.Services.ServerHelper.GetData(
    //            ApplicationContext.Instance.CurrentFlight, this.FromRelatedParametersToStrs(),
    //            this.CurrentStartSecond, this.CurrentEndSecond);

    //        this.GridData = dt;
    //    }

    //    private string[] FromRelatedParametersToStrs()
    //    {
    //        var result = from one in this.m_relatedParameterCollection
    //                     select one.Parameter.ParameterID;
    //        return result.ToArray();
    //    }

    //    private DataTable m_gridData = null;

    //    public DataTable GridData
    //    {
    //        get
    //        {
    //            return m_gridData;
    //        }
    //        internal set
    //        {
    //            this.SetProperty<DataTable>(ref m_gridData, value);
    //        }
    //    }

    //    private ObservableCollection<RelatedParameterViewModel> m_relatedParameterCollection
    //        = new ObservableCollection<RelatedParameterViewModel>();

    //    public ObservableCollection<RelatedParameterViewModel> RelatedParameterCollection
    //    {
    //        get
    //        {
    //            return m_relatedParameterCollection;
    //        }
    //        internal set
    //        {
    //            this.SetProperty<ObservableCollection<RelatedParameterViewModel>>(ref m_relatedParameterCollection, value);
    //        }
    //    }

    //    private int m_currentStartSecond = 0;

    //    public int CurrentStartSecond
    //    {
    //        get { return m_currentStartSecond; }
    //        set
    //        {
    //            this.SetProperty<int>(ref m_currentStartSecond, value);
    //        }
    //    }

    //    private int m_currentEndSecond = 0;

    //    public int CurrentEndSecond
    //    {
    //        get { return m_currentEndSecond; }
    //        set
    //        {
    //            this.SetProperty<int>(ref m_currentEndSecond, value);
    //        }
    //    }

    //    internal void FilterData(RelatedParameterViewModel relatedParameterViewModel)
    //    {
    //        if (FilterDataChanged != null)
    //            this.FilterDataChanged(this, EventArgs.Empty);
    //    }

    //    public event EventHandler FilterDataChanged;
    //}

    //public class RelatedParameterViewModel : AircraftDataAnalysisWinRT.Common.BindableBase
    //{
    //    public RelatedParameterViewModel(FlightAnalysisViewModel viewModel)
    //    {
    //        this.viewModel = viewModel;
    //    }

    //    private bool m_isChecked = true;

    //    public bool IsChecked
    //    {
    //        get { return m_isChecked; }
    //        set
    //        {
    //            this.SetProperty<bool>(ref m_isChecked, value);
    //            this.ViewModel.FilterData(this);
    //            System.Diagnostics.Debug.WriteLine("IsCheckedChanged");
    //        }
    //    }

    //    private FlightAnalysisViewModel viewModel;

    //    public FlightAnalysisViewModel ViewModel
    //    {
    //        get { return this.viewModel; }
    //        set { this.SetProperty<FlightAnalysisViewModel>(ref this.viewModel, value); }
    //    }

    //    private FlightParameter m_parameter;

    //    public FlightParameter Parameter
    //    {
    //        get { return m_parameter; }
    //        set { this.SetProperty<FlightParameter>(ref m_parameter, value); }
    //    }

    //}
}
