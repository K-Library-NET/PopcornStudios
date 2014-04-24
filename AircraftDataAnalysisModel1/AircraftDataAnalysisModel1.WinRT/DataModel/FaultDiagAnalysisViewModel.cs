using AircraftDataAnalysisWinRT;
using FlightDataEntitiesRT;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftDataAnalysisModel1.WinRT.DataModel
{
    public class FaultDiagAnalysisViewModel : AircraftDataAnalysisWinRT.Common.BindableBase
        , AircraftDataAnalysisModel1.WinRT.DataModel.IGroupVisibleObserver
    {
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

        private double? m_unscaledX = null;

        public string CurrentSecondDisplayStr
        {
            get
            {
                if (m_unscaledX == null)
                    return string.Empty;

                return string.Format("{0}", Math.Round(m_unscaledX.Value, 2));
            }
        }

        /// <summary>
        /// 1. RelatedParameterInit
        /// 2. RelatedParameterGroupInit
        /// </summary>
        private void RelatedParameterAndGroupInit()
        {
            //先Clear
            this.Group1 = null;
            this.Group2 = null;
            this.Group3 = null;
            this.Group4 = null;
            this.Group5 = null;
            this.Group6 = null;
            this.Group7 = null;

            //TODO: 
            if (this.m_currentParameter != null
                && !string.IsNullOrEmpty(this.m_currentParameter.HostParameterID))
            {
                IEnumerable<string> parameterids = m_currentParameter.GetParameterIDs();

                AircraftDataAnalysisModel1.WinRT.Domain.AircraftAnalysisDataLoader dataLoader =
                    this.GetDataLoader();
                dataLoader.LoadRawDataAsync(parameterids);

                var groupedIDs = FlightAnalysisChartGroupFactory.CalculateBindingGroups(parameterids);
                int i = 0;
                foreach (var groupItem in groupedIDs)
                {
                    var groupViewModel = SwitchGroup(i, groupItem, dataLoader);
                    i++;
                }
            }
        }

        private Domain.AircraftAnalysisDataLoader GetDataLoader()
        {
            if (this.DataLoader == null)
                this.DataLoader = new Domain.AircraftAnalysisDataLoader();
            return this.DataLoader;
        }

        public Domain.AircraftAnalysisDataLoader DataLoader
        {
            get;
            set;
        }

        private FlightAnalysisChartGroupViewModel SwitchGroup(int i, KeyValuePair<string, IEnumerable<string>> groupItem,
            AircraftDataAnalysisModel1.WinRT.Domain.AircraftAnalysisDataLoader dataLoader)
        {
            FlightAnalysisChartGroupViewModel groupViewModel = null;
            switch (i)
            {
                case 0:
                    {
                        this.Group1 = new FlightAnalysisChartGroupViewModel(this);
                        this.LoadAndAssignValue(this.Group1, groupItem, dataLoader);
                        groupViewModel = this.Group1;
                        break;
                    }
                case 1:
                    {
                        this.Group2 = new FlightAnalysisChartGroupViewModel(this);
                        this.LoadAndAssignValue(this.Group2, groupItem, dataLoader);
                        groupViewModel = this.Group2;
                        break;
                    }
                case 2:
                    {
                        this.Group3 = new FlightAnalysisChartGroupViewModel(this);
                        this.LoadAndAssignValue(this.Group3, groupItem, dataLoader);
                        groupViewModel = this.Group3;
                        break;
                    }
                case 3:
                    {
                        this.Group4 = new FlightAnalysisChartGroupViewModel(this);
                        this.LoadAndAssignValue(this.Group4, groupItem, dataLoader);
                        groupViewModel = this.Group4;
                        break;
                    }
                case 4:
                    {
                        this.Group5 = new FlightAnalysisChartGroupViewModel(this);
                        this.LoadAndAssignValue(this.Group5, groupItem, dataLoader);
                        groupViewModel = this.Group5;
                        break;
                    }
                case 5:
                    {
                        this.Group6 = new FlightAnalysisChartGroupViewModel(this);
                        this.LoadAndAssignValue(this.Group6, groupItem, dataLoader);
                        groupViewModel = this.Group6;
                        break;
                    }
                case 6:
                    {
                        this.Group7 = new FlightAnalysisChartGroupViewModel(this);
                        this.LoadAndAssignValue(this.Group7, groupItem, dataLoader);
                        groupViewModel = this.Group7;
                        break;
                    }
                default: break;
            }

            return groupViewModel;
        }

        private void LoadAndAssignValue(FlightAnalysisChartGroupViewModel groupViewModel,
            KeyValuePair<string, IEnumerable<string>> groupItem,
            AircraftDataAnalysisModel1.WinRT.Domain.AircraftAnalysisDataLoader dataLoader)
        {
            var vm = new FlightAnalysisChartSerieViewModel();
            int j = 0;

            foreach (var parameterID in groupItem.Value)
            {
                this.LoadSimpleDataPoints(vm, parameterID, dataLoader, j);
                if (j == 0)
                {
                    groupViewModel.Serie1Definition = new SerieDefinitionViewModel(groupViewModel)
                    {
                        ParameterID = parameterID
                    };
                }
                else if (j == 1)
                {
                    groupViewModel.Serie2Definition = new SerieDefinitionViewModel(groupViewModel)
                    {
                        ParameterID = parameterID
                    };
                }
                else if (j == 2)
                {
                    groupViewModel.Serie3Definition = new SerieDefinitionViewModel(groupViewModel)
                    {
                        ParameterID = parameterID
                    };
                }
                j++;
            }

            groupViewModel.DataSerie = vm;
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

        #region repeat

        private FlightAnalysisChartGroupViewModel m_group1 = null;

        public FlightAnalysisChartGroupViewModel Group1
        {
            get
            {
                return m_group1;
            }
            set
            {
                this.SetProperty<FlightAnalysisChartGroupViewModel>(ref m_group1, value);
            }
        }

        private FlightAnalysisChartGroupViewModel m_group2 = null;

        public FlightAnalysisChartGroupViewModel Group2
        {
            get
            {
                return m_group2;
            }
            set
            {
                this.SetProperty<FlightAnalysisChartGroupViewModel>(ref m_group2, value);
            }
        }

        private FlightAnalysisChartGroupViewModel m_group3 = null;

        public FlightAnalysisChartGroupViewModel Group3
        {
            get
            {
                return m_group3;
            }
            set
            {
                this.SetProperty<FlightAnalysisChartGroupViewModel>(ref m_group3, value);
            }
        }

        private FlightAnalysisChartGroupViewModel m_group4 = null;

        public FlightAnalysisChartGroupViewModel Group4
        {
            get
            {
                return m_group4;
            }
            set
            {
                this.SetProperty<FlightAnalysisChartGroupViewModel>(ref m_group4, value);
            }
        }

        private FlightAnalysisChartGroupViewModel m_group5 = null;

        public FlightAnalysisChartGroupViewModel Group5
        {
            get
            {
                return m_group5;
            }
            set
            {
                this.SetProperty<FlightAnalysisChartGroupViewModel>(ref m_group5, value);
            }
        }

        private FlightAnalysisChartGroupViewModel m_group6 = null;

        public FlightAnalysisChartGroupViewModel Group6
        {
            get
            {
                return m_group6;
            }
            set
            {
                this.SetProperty<FlightAnalysisChartGroupViewModel>(ref m_group6, value);
            }
        }

        private FlightAnalysisChartGroupViewModel m_group7 = null;

        public FlightAnalysisChartGroupViewModel Group7
        {
            get
            {
                return m_group7;
            }
            set
            {
                this.SetProperty<FlightAnalysisChartGroupViewModel>(ref m_group7, value);
            }
        }

        #endregion repeat

        public Task<string> InitializeAsync()
        {
            if (m_task == null)
            {
                Task<string> task = Task.Run<string>(
                    new Func<string>(() =>
                    {
                        try
                        {
                            this.InitializeCore();
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

        private Task<string> m_task = null;
        private DecisionNavigationParameterHelper m_currentParameter;

        private async void InitializeCore()
        {
            ////GetChartPanels
            //await InitPanelViewModel();

            await this.UserThreadInvoker.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                new Windows.UI.Core.DispatchedHandler(
                    () =>
                    {
                        RelatedParameterAndGroupInit();
                    }));

            //3. serie definitions
            await FinishBySetSerieDefs();
            System.Diagnostics.Debug.WriteLine(string.Format("End analysis ViewModel :{0} ", DateTime.Now));
        }

        private async Task FinishBySetSerieDefs()
        {
            List<SerieDefinitionViewModel> seriedefList = new List<SerieDefinitionViewModel>();
            AddNotNullGroupDefsToList(seriedefList);
            await this.UserThreadInvoker.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                new Windows.UI.Core.DispatchedHandler(
                    () =>
                    {
                        this.SerieDefinitions = new ObservableCollection<SerieDefinitionViewModel>(seriedefList);
                        this.SetCurrentSecondDisplay(0);
                        this.RequestGroupVisibleChanged();
                        //4. Stop Intermediate
                        this.IsLoading = false;
                    }));
        }

        private void AddNotNullGroupDefsToList(List<SerieDefinitionViewModel> seriedefList)
        {
            if (this.Group1 != null)
            {
                var group = this.Group1;
                AddOneGroupNotNullDefsToList(seriedefList, group);
            }
            if (this.Group2 != null)
            {
                var group = this.Group2;
                AddOneGroupNotNullDefsToList(seriedefList, group);
            }
            if (this.Group3 != null)
            {
                var group = this.Group3;
                AddOneGroupNotNullDefsToList(seriedefList, group);
            }
            if (this.Group4 != null)
            {
                var group = this.Group4;
                AddOneGroupNotNullDefsToList(seriedefList, group);
            }
            if (this.Group5 != null)
            {
                var group = this.Group5;
                AddOneGroupNotNullDefsToList(seriedefList, group);
            }
            if (this.Group6 != null)
            {
                var group = this.Group6;
                AddOneGroupNotNullDefsToList(seriedefList, group);
            }
            if (this.Group7 != null)
            {
                var group = this.Group7;
                AddOneGroupNotNullDefsToList(seriedefList, group);
            }
        }

        private void AddOneGroupNotNullDefsToList(List<SerieDefinitionViewModel> seriedefList, FlightAnalysisChartGroupViewModel group)
        {
            if (group.Serie1Definition != null)
                seriedefList.Add(group.Serie1Definition);
            if (group.Serie2Definition != null)
                seriedefList.Add(group.Serie2Definition);
            if (group.Serie3Definition != null)
                seriedefList.Add(group.Serie3Definition);
        }

        private async void AwaitUserInvokeRun(Action func)
        {
            await this.UserThreadInvoker.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                new Windows.UI.Core.DispatchedHandler(func));
        }

        public void Wait()
        {
            if (m_task != null)
                m_task.Wait();
        }

        public Windows.UI.Core.CoreDispatcher UserThreadInvoker { get; set; }

        public void SetCurrentSecondDisplay(double unscaledX)
        {
            m_unscaledX = unscaledX;
            this.OnPropertyChanged("CurrentSecondDisplayStr");
        }

        public void RequestGroupVisibleChanged()
        {
            this.OnPropertyChanged("Group1");
            this.OnPropertyChanged("Group2");
            this.OnPropertyChanged("Group3");
            this.OnPropertyChanged("Group4");
            this.OnPropertyChanged("Group5");
            this.OnPropertyChanged("Group6");
            this.OnPropertyChanged("Group7");
        }

        internal void SetCurrentParameters(string hostParameterID, string[] relatedParameters,
            int startSecond, int endSecond, int DecisionStartSecond, int DecisionEndSecond, int DecisionHappenSecond)
        {
            DecisionNavigationParameterHelper helper = new DecisionNavigationParameterHelper()
            {
                HostParameterID = hostParameterID,
                RelatedParameters = relatedParameters,
                FlightStartSecond = startSecond,
                FlightEndSecond = endSecond,
                DecisionStartSecond = DecisionStartSecond,
                DecisionEndSecond = DecisionEndSecond,
                DecisionHappenSecond = DecisionHappenSecond
            };

            m_currentParameter = helper;
        }
    }

    internal class DecisionNavigationParameterHelper
    {
        public string HostParameterID { get; set; }

        public string[] RelatedParameters { get; set; }

        public int FlightStartSecond { get; set; }

        public int FlightEndSecond { get; set; }

        public int DecisionStartSecond { get; set; }

        public int DecisionEndSecond { get; set; }

        public int DecisionHappenSecond { get; set; }

        internal IEnumerable<string> GetParameterIDs()
        {
            List<string> list = new List<string>();

            list.Add(this.HostParameterID);

            if (RelatedParameters != null && RelatedParameters.Length > 0)
            {
                var notNull = from one in this.RelatedParameters
                              where !string.IsNullOrEmpty(one)
                              select one;
                var notNull2 = notNull.Distinct();

                list.AddRange(notNull2);
            }

            return list;
        }
    }
}
