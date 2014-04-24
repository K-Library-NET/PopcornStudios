using AircraftDataAnalysisModel1.WinRT.DataModel;
using AircraftDataAnalysisWinRT.DataModel;
using AircraftDataAnalysisWinRT.Domain;
using FlightDataEntitiesRT;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “用户控件”项模板在 http://go.microsoft.com/fwlink/?LinkId=234236 上提供

namespace AircraftDataAnalysisWinRT.MyControl
{
    public sealed partial class FAGridModel1 : UserControl
    {
        public FAGridModel1()
        {
            this.InitializeComponent();

            this.Loaded += FAGridModel1_Loaded;
            this.Unloaded += FAGridModel1_Unloaded;
        }

        private void FAGridModel1_Unloaded(object sender, RoutedEventArgs e)
        {
            //debug
            System.Diagnostics.Debug.WriteLine("FAGridModel1_Unloaded unloaded...");
        }

        private bool m_loaded = false;

        void FAGridModel1_Loaded(object sender, RoutedEventArgs e)
        {
            //debug
            System.Diagnostics.Debug.WriteLine("FAGridModel1_Loaded loaded...");
            if (m_loaded == false)
            {
                this.m_loaded = true;
                if (m_subViewModel != null)
                {//value assigned
                    this.ResetGrid();
                }
            }
        }

        private void ResetGrid()
        {
            this.ReBindColumns();
        }

        private FlightAnalysisSubViewModel m_subViewModel = null;

        public FlightAnalysisSubViewModel SubViewModel
        {
            get { return m_subViewModel; }
            set
            {
                //if (m_subViewModel != null)
                //{
                //    m_subViewModel.RelatedParameterIDs.CollectionChanged -= RelatedParameterIDs_CollectionChanged;
                //    m_subViewModel = null;
                //}

                //m_subViewModel = value;
                //if (m_subViewModel != null)
                //{
                //    m_subViewModel.RelatedParameterIDs.CollectionChanged += RelatedParameterIDs_CollectionChanged;
                //}

                if (this.m_loaded)
                {
                    this.ResetGrid();
                }
            }
        }
        void RelatedParameterIDs_CollectionChanged(object sender,
            System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                //add one chart to tail
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                //remove selected chart
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Move)
            {
                //move chartPositions
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Replace)
            {
                //replace chart
            }
            //else
            //{
            //Reset
            this.ResetGrid();
            //}
        }

        public void ReBindColumns()
        {
            if (m_subViewModel == null)
                return;

            List<string> colList = new List<string>();
            colList.Add("Second");//must have seconds column
            //colList.Add(this.m_subViewModel.HostParameterID);
            //if (this.m_subViewModel != null &&
            //    this.m_subViewModel.RelatedParameterIDs != null
            //    && this.m_subViewModel.RelatedParameterIDs.Count > 0)
            //{
            //    colList.AddRange(m_subViewModel.RelatedParameterIDs);
            //}

            var cols = colList;
            //var cols = from one in this.m_viewModel.RelatedParameterCollection
            //           //where one.IsChecked        //只要是相关都行，不管是否Checked，Checked只在曲线上用
            //           select one.Parameter.ParameterID;

            foreach (var col in this.grdData.Columns)
            {
                if (cols.Contains(col.MappingName) || col.MappingName == "Second")
                {
                    col.IsHidden = false;
                }
                else col.IsHidden = true;
            }

            //this.m_dataCollection = new ObservableCollection<FlightDataReading.AircraftModel1.AircraftModel1RawData>(
            //    this.m_subViewModel.GetFilteredDataCollection());
            this.grdData.ItemsSource = m_dataCollection;
        }

        private ObservableCollection<FlightDataReading.AircraftModel1.AircraftModel1RawData> m_dataCollection
            = new ObservableCollection<FlightDataReading.AircraftModel1.AircraftModel1RawData>();


        [Obsolete("Old")]
        private FlightAnalysisViewModelOld m_viewModel = null;

        [Obsolete("Old")]
        public FlightAnalysisViewModelOld ViewModel
        {
            get { return m_viewModel; }
            set
            {
                m_viewModel = value;

                //this.OnViewModelChanged();
            }
        }

        [Obsolete("Old")]
        private void OnViewModelChanged()
        {
            //this.ReBindColumns();
            //this.BindGridData();
        }

        [Obsolete("Old")]
        public async void BindGridData()
        {
            return;
            var res = this.Dispatcher.RunAsync(//new Windows.UI.Core.IdleDispatchedHandler(
                Windows.UI.Core.CoreDispatcherPriority.Normal, new Windows.UI.Core.DispatchedHandler(
                    delegate()
                    {
                        //ReBindColumns();
                        //不能运行时加列，需要列全部绑定好，通过Visibility控制？
                        //this.grdData.ItemsSource = this.ViewModel.EntityBindingCollection.RawDataItems;
                        //m_dataCollection = new ObservableCollection<ModelRawData>(
                        //    this.ViewModel.EntityBindingCollection.RawDataItems.Take(100));
                        this.grdData.ItemsSource = m_dataCollection;
                    }));

            res.AsTask().ContinueWith(new Action<Task>(delegate(Task t)
            {
                this.Dispatcher.RunAsync(
                   Windows.UI.Core.CoreDispatcherPriority.Normal, new Windows.UI.Core.DispatchedHandler(
                       delegate()
                       {
                           int cnt = this.ViewModel.RawDatas.Count();
                           if (cnt > 100)
                           {
                               for (int i = 100; i < cnt; i++)
                               {
                                   m_dataCollection.Add(this.ViewModel.RawDatas.ElementAt(i));
                               }
                           }
                       }));
            }));
        }

        [Obsolete("Old")]
        private FlightDataEntitiesRT.FlightParameter[] GetFlightParameters()
        {
            var flightParameters = ApplicationContext.Instance.GetFlightParameters(
                ApplicationContext.Instance.CurrentAircraftModel);

            var result = from one in flightParameters.Parameters
                         where one.ParameterID != "(NULL)" && this.ExistsParameter(one.ParameterID)
                         && ViewModel.RelatedParameterSelected(one.ParameterID)
                         select one;

            return result.ToArray();
        }

        [Obsolete("Old")]
        private bool ExistsParameter(string p)
        {
            if (this.ViewModel.AllParameterIDs.Contains(p))
                return true;
            return false;
            //foreach (var dc in this.ViewModel.AllParameterIDs)
            //{
            //    if (dc == p)
            //        return true;
            //}
            //return false;
        }
    }
}
