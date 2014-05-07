using AircraftDataAnalysisModel1.WinRT.DataModel;
using AircraftDataAnalysisModel1.WinRT.Domain;
using AircraftDataAnalysisWinRT;
using AircraftDataAnalysisWinRT.Common;
using AircraftDataAnalysisWinRT.Styles;
using FlightDataEntitiesRT;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftDataAnalysisModel1.WinRT.DataModel
{
    /// <summary>
    /// FlightAnalysisSub图表联动的DataContext
    /// 20140321 liangdawen refactored
    /// </summary>
    public class FlightAnalysisSubViewModel : BindableBase
    {
        /// <summary>
        /// FlightAnalysisSub图表联动的DataContext
        /// 20140321 liangdawen refactored
        /// </summary>
        public FlightAnalysisSubViewModel()
        {
            return;//non debug
            this.DebugInit();
        }

        private void DebugInit()
        {
        }

        public bool IsParameterCanAdd
        {
            get
            {
                return true;
            }
        }

        public bool IsParameterCanRemove
        {
            get
            {
                return true;
            }
        }

        public string HostParameterTitleStr
        {
            get
            {
                if (!string.IsNullOrEmpty(this.m_hostParameterID))
                {
                    return string.Format("——{0}", ApplicationContext.Instance.GetFlightParameterCaption(m_hostParameterID));
                }
                return "——";
            }
        }

        private string m_hostParameterID = string.Empty;

        public string HostParameterID
        {
            get
            {
                return m_hostParameterID;
            }
            set
            {
                this.SetProperty<string>(ref m_hostParameterID, value);
                this.OnPropertyChanged("HostParameterTitleStr");
            }
        }

        private ObservableCollection<SerieDefinitionViewModel> m_serieDefinition = null;

        public ObservableCollection<SerieDefinitionViewModel> SerieDefinitions
        {
            get
            {
                return m_serieDefinition;
            }
            internal set
            {
                this.SetProperty<ObservableCollection<SerieDefinitionViewModel>>(ref m_serieDefinition, value);
            }
        }

        private AircraftAnalysisDataLoader GetDataLoader()
        {
            if (this.DataLoader == null)
                this.DataLoader = new AircraftAnalysisDataLoader();
            return this.DataLoader;
        }

        public AircraftAnalysisDataLoader DataLoader
        {
            get;
            set;
        }

        private bool m_isLoading = true;

        public bool IsLoading
        {
            get { return m_isLoading; }
            set
            {
                this.SetProperty<bool>(ref m_isLoading, value);
                this.OnPropertyChanged("IsEnable");
            }
        }

        public bool IsEnable
        {
            get
            {
                return !m_isLoading;
            }
        }

        private FlightAnalysisChartGroupSubExtendedViewModel m_SerieGroup = null;

        public FlightAnalysisChartGroupSubExtendedViewModel SerieGroup
        {
            get
            {
                return m_SerieGroup;
            }
            set
            {
                this.SetProperty<FlightAnalysisChartGroupSubExtendedViewModel>(ref m_SerieGroup, value);
            }
        }

        public Task<string> InitializeAsync(AircraftDataAnalysisWinRT.MyControl.SubEditChartNavigationParameter parameter)
        {
            if (m_task == null)
            {
                Task<string> task = Task.Run<string>(
                    new Func<string>(() =>
                    {
                        try
                        {
                            this.InitializeCore(parameter);
                        }
                        catch (Exception e)
                        {
                            LogHelper.Error(e);
                            return e.Message;
                        }
                        return string.Empty;
                    }));
                m_task = task;
            }

            return m_task;
        }

        public void Wait()
        {
            if (m_task != null)
                m_task.Wait();
        }

        private async void AwaitUserInvokeRun(Action func)
        {
            await this.UserThreadInvoker.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                new Windows.UI.Core.DispatchedHandler(func));
        }

        public Windows.UI.Core.CoreDispatcher UserThreadInvoker { get; set; }

        private Task<string> m_task = null;

        private async void InitializeCore(
            AircraftDataAnalysisWinRT.MyControl.SubEditChartNavigationParameter parameter)
        {
            //await this.DebugSetZoomMode(parameter);

            this.LoadDatas(parameter);
            ////GetChartPanels
            //await InitPanelViewModel();
            //await this.DebugSetZoomMode(parameter);
            ////2. currentPanel
            //await SetCurrentPanelCore();

            ////3. serie definitions
            //await FinishBySetSerieDefs();

            //finish
            await Finish();
            System.Diagnostics.Debug.WriteLine(string.Format("End analysis sub ViewModel :{0} ", DateTime.Now));
        }

        private async Task DebugSetZoomMode(AircraftDataAnalysisWinRT.MyControl.SubEditChartNavigationParameter parameter)
        {
            await this.UserThreadInvoker.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                new Windows.UI.Core.DispatchedHandler(
                    () =>
                    {
                        this.SerieGroup.IsZoomMode = true;
                    }));
        }

        private async void LoadDatas(AircraftDataAnalysisWinRT.MyControl.SubEditChartNavigationParameter parameter)
        {
            List<string> pids = new List<string>();

            if (this.SerieGroup == null)
                this.SerieGroup = new FlightAnalysisChartGroupSubExtendedViewModel();

            await this.UserThreadInvoker.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                new Windows.UI.Core.DispatchedHandler(() =>
                {
                    this.SerieGroup.Serie1Definition = new SerieDefinitionViewModel()
                    {
                        ParameterID = parameter.HostParameterID,
                        //ParameterCaption = ApplicationContext.Instance.GetFlightParameterCaption(parameter.HostParameterID),
                        //ForegroundBrush = AircraftDataAnalysisGlobalPallete.Brushes[0]
                    };
                }));
            pids.Add(parameter.HostParameterID);

            if (parameter.RelatedParameterIDs != null && parameter.RelatedParameterIDs.Length > 0)
            {
                if (parameter.RelatedParameterIDs.Length > 0)
                {
                    await this.UserThreadInvoker.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                        new Windows.UI.Core.DispatchedHandler(() =>
                        {
                            this.SerieGroup.Serie2Definition = new SerieDefinitionViewModel()
                            {
                                ParameterID = parameter.RelatedParameterIDs[0].RelatedParameterID
                            };
                        }));
                    pids.Add(parameter.RelatedParameterIDs[0].RelatedParameterID);
                }
                if (parameter.RelatedParameterIDs.Length > 1)
                {
                    await this.UserThreadInvoker.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                        new Windows.UI.Core.DispatchedHandler(() =>
                        {
                            this.SerieGroup.Serie3Definition = new SerieDefinitionViewModel()
                            {
                                ParameterID = parameter.RelatedParameterIDs[1].RelatedParameterID
                            };
                        }));
                    pids.Add(parameter.RelatedParameterIDs[1].RelatedParameterID);
                }
            }

            Task<string> resultTask = this.DataLoader.LoadRawDataAsync(pids);
            await this.UserThreadInvoker.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                new Windows.UI.Core.DispatchedHandler(() =>
                {
                    this.SerieGroup.DataSerie = new FlightAnalysisChartSerieViewModel();

                    this.LoadSimpleDataPoints(this.SerieGroup.DataSerie, pids[0],
                        this.DataLoader, 0);
                    if (pids.Count > 1)
                    {
                        this.LoadSimpleDataPoints(this.SerieGroup.DataSerie, pids[1],
                            this.DataLoader, 1);
                    }
                    if (pids.Count > 2)
                    {
                        this.LoadSimpleDataPoints(this.SerieGroup.DataSerie, pids[2],
                            this.DataLoader, 2);
                    }
                    //var raws1 = this.DataLoader.GetRawData(pids[0]);
                }));

        }

        private void LoadSimpleDataPoints(FlightAnalysisChartSerieViewModel vm, string parameterID,
           Domain.AircraftAnalysisDataLoader dataLoader, int serieNum)
        {
            IEnumerable<ParameterRawData> rawdatas = dataLoader.GetRawData(parameterID);
            if (rawdatas == null)
                return;

            Dictionary<int, MyControl.SimpleDataPoint> pointMap = new Dictionary<int, MyControl.SimpleDataPoint>();

            foreach (var v in vm)
            {
                if (pointMap.ContainsKey(v.Second))
                    continue;

                pointMap.Add(v.Second, v);
            }

            #region debug test
            //var serie1 = vm;
            //serie1.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            //{
            //    Label = 1,
            //    Value = 3
            //});
            //serie1.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            //{
            //    Label = 2,
            //    Value = 4
            //});
            //serie1.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            //{
            //    Label = 3,
            //    Value = 2
            //});
            //serie1.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            //{
            //    Label = 4,
            //    Value = 1
            //});
            //serie1.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            //{
            //    Label = 5,
            //    Value = 5
            //});
            //serie1.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            //{
            //    Label = 6,
            //    Value = 3
            //});
            //return;//debug
            #endregion

            foreach (var rd in rawdatas)
            {//暂时先写死第一秒钟的值
                if (pointMap.ContainsKey(rd.Second))
                {
                    if (serieNum == 0)
                        pointMap[rd.Second].Value1 = rd.Values[0];
                    else if (serieNum == 1)
                        pointMap[rd.Second].Value2 = rd.Values[0];
                    else if (serieNum == 2)
                        pointMap[rd.Second].Value3 = rd.Values[0];
                }
                else
                {
                    var point = new MyControl.SimpleDataPoint()
                    {
                        Second = rd.Second
                    };
                    if (serieNum == 0)
                        point.Value1 = rd.Values[0];
                    else if (serieNum == 1)
                        point.Value2 = rd.Values[0];
                    else if (serieNum == 2)
                        point.Value3 = rd.Values[0];
                    pointMap.Add(rd.Second, point);
                }
            }

            var result = from pt in pointMap
                         orderby pt.Key ascending
                         select pt.Value;
            foreach (var re in result)
            {
                if (!vm.Contains(re))
                    vm.Add(re);
            }
        }


        private async Task Finish()
        {
            await this.UserThreadInvoker.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                new Windows.UI.Core.DispatchedHandler(
                    () =>
                    {
                        // Stop Intermediate
                        this.IsLoading = false;
                    }));
        }

        #region old
        //void m_relatedParameterIDs_CollectionChanged(object sender,
        //    System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        //{//debug
        //}

        //private int m_currentStartSecond = 0;

        //public int CurrentStartSecond
        //{
        //    get { return m_currentStartSecond; }
        //    set
        //    {
        //        this.SetProperty<int>(ref m_currentStartSecond, value);
        //    }
        //}

        //public FlightDataReading.AircraftModel1.AircraftModel1RawData CurrentSecondRowObject
        //{
        //    get
        //    {
        //        this.DataCollection.Single(
        //            new Func<FlightDataReading.AircraftModel1.AircraftModel1RawData, bool>(
        //                delegate(FlightDataReading.AircraftModel1.AircraftModel1RawData dt)
        //                {
        //                    if (dt != null && this.CurrentSecond != null
        //                        && dt.Second == this.CurrentSecond.Value)
        //                        //暂时不用四舍五入
        //                        //&& dt.Second == Math.Round(this.CurrentSecond.Value))
        //                        return true;
        //                    return false;
        //                }));
        //        return null;
        //    }
        //}

        //private int m_currentEndSecond = 0;

        //public int CurrentEndSecond
        //{
        //    get { return m_currentEndSecond; }
        //    set
        //    {
        //        this.SetProperty<int>(ref m_currentEndSecond, value);
        //    }
        //}

        //private string m_hostParameterID = string.Empty;

        //public string HostParameterID
        //{
        //    get { return m_hostParameterID; }
        //    set
        //    {
        //        this.SetProperty<string>(ref m_hostParameterID, value);
        //    }
        //}

        //private double? m_currentSecond = null;

        //public double? CurrentSecond
        //{
        //    get { return m_currentSecond; }
        //    set
        //    {
        //        this.SetProperty<double?>(ref m_currentSecond, value);
        //        base.OnPropertyChanged("CurrentSecondRowObject");
        //    }
        //}

        //private ObservableCollection<string> m_relatedParameterIDs = new ObservableCollection<string>();

        //public ObservableCollection<string> RelatedParameterIDs
        //{
        //    get
        //    {
        //        return m_relatedParameterIDs;
        //    }
        //}

        //public ObservableCollection<FlightDataReading.AircraftModel1.AircraftModel1RawData> DataCollection
        //{
        //    get;
        //    set;
        //}

        //public IEnumerable<FlightDataReading.AircraftModel1.AircraftModel1RawData> GetFilteredDataCollection()
        //{

        //    var filteredCollection = from one in this.DataCollection
        //                             where one.Second >= this.CurrentStartSecond
        //                             && one.Second < this.CurrentEndSecond
        //                             select one;

        //    if (filteredCollection == null
        //        || filteredCollection.Count() <= 0)
        //    {
        //        return new List<FlightDataReading.AircraftModel1.AircraftModel1RawData>();
        //    }

        //    return filteredCollection;
        //}

        //public virtual AircraftDataAnalysisWinRT.MyControl.FAChartModel GetFAChartModel()
        //{
        //    if (this.ViewModel == null)
        //        return null;

        //    return this.ViewModel.GetFAChartModel();
        //    //if (m_faChartModel == null)
        //    //    m_faChartModel = new MyControl.FAChartModel();

        //    //return m_faChartModel;
        //}

        //public AircraftDataAnalysisWinRT.Domain.FlightAnalysisViewModelOld ViewModel
        //{
        //    get;
        //    set;
        //}
        #endregion

    }
}
