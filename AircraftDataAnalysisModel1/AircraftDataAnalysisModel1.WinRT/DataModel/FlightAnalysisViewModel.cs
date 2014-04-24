using AircraftDataAnalysisWinRT;
using AircraftDataAnalysisWinRT.Common;
using FlightDataEntitiesRT;
using FlightDataEntitiesRT.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftDataAnalysisModel1.WinRT.DataModel
{
    public class FlightAnalysisViewModel : BindableBase, AircraftDataAnalysisModel1.WinRT.DataModel.IGroupVisibleObserver
    {
        public FlightAnalysisViewModel()
        {
            return;//non debug
            this.DebugInit();
        }

        private void DebugInit()
        {
            //    左发动机	右发动机	发动机	大气机数据	惯导飞控	位移	
            //着陆姿态A	着陆姿态B	角速度	过载	告警信号A	告警信号B
            //1. SetPanel
            ChartPanel[] chartPanels = new ChartPanel[]{
                new ChartPanel(){ PanelID = "A", PanelName="左发动机" , 
                    ParameterIDs = new string[]{
                        "Hp","Tt","Pt"
                    }
                },
                new ChartPanel(){ PanelID = "B", PanelName="右发动机" , 
                    ParameterIDs = new string[]{
                        "Hp","Tt","Pt"
                    }
                },
                new ChartPanel(){ PanelID = "C", PanelName="发动机" , 
                    ParameterIDs = new string[]{
                        "Hp","Tt","Pt"
                    }
                },
                new ChartPanel(){ PanelID = "D", PanelName="大气机数据" , 
                    ParameterIDs = new string[]{
                        "Hp","Tt","Pt"
                    }
                },
                new ChartPanel(){ PanelID = "E", PanelName="惯导飞控" , 
                    ParameterIDs = new string[]{
                        "Hp","Tt","Pt"
                    }
                },
                new ChartPanel(){ PanelID = "F", PanelName="位移" , 
                    ParameterIDs = new string[]{
                        "Hp","Tt","Pt"
                    }
                },
                new ChartPanel(){ PanelID = "G", PanelName="着陆姿态A" , 
                    ParameterIDs = new string[]{
                        "Hp","Tt","Pt"
                    }
                },
                new ChartPanel(){ PanelID = "H", PanelName="着陆姿态B" , 
                    ParameterIDs = new string[]{
                        "Hp","Tt","Pt"
                    }
                },
                new ChartPanel(){ PanelID = "I", PanelName="角速度" , 
                    ParameterIDs = new string[]{
                        "Hp","Tt","Pt"
                    }
                },
                new ChartPanel(){ PanelID = "J", PanelName="过载" , 
                    ParameterIDs = new string[]{
                        "Hp","Tt","Pt"
                    }
                },
                new ChartPanel(){ PanelID = "K", PanelName="告警信号A" , 
                    ParameterIDs = new string[]{
                        "Hp","Tt","Pt"
                    }
                },
                new ChartPanel(){ PanelID = "L", PanelName="告警信号B" , 
                    ParameterIDs = new string[]{
                        "Hp","Tt","Pt"
                    }
                },
            };

            foreach (var panel in chartPanels)
            {
                this.m_panelViewItems.Add(new PanelViewModelItem(panel));
            }

            //2. currentPanel
            this.SelectedPanelIndex = 0;

            this.Group1 = new FlightAnalysisChartGroupViewModel()
            {
                Serie1Definition = new SerieDefinitionViewModel() { ParameterID = "Hp" }
            };
            this.Group1.DataSerie = new FlightAnalysisChartSerieViewModel();
            var serie1 = this.Group1.DataSerie;
            serie1.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Second = 1,
                Value = 3
            });
            serie1.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Second = 2,
                Value = 4
            });
            serie1.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Second = 3,
                Value = 2
            });
            serie1.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Second = 4,
                Value = 1
            });
            serie1.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Second = 5,
                Value = 5
            });
            serie1.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Second = 6,
                Value = 3
            });

            this.Group2 = new FlightAnalysisChartGroupViewModel()
            {
                Serie1Definition = new SerieDefinitionViewModel() { ParameterID = "Tt" }
            };
            this.Group2.DataSerie = new FlightAnalysisChartSerieViewModel();
            var serie2 = this.Group2.DataSerie;
            serie2.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Second = 1,
                Value = 3
            });
            serie2.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Second = 2,
                Value = 4
            });
            serie2.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Second = 3,
                Value = 2
            });
            serie2.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Second = 4,
                Value = 1
            });
            serie2.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Second = 5,
                Value = 5
            });
            serie2.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Second = 6,
                Value = 3
            });

            this.Group3 = new FlightAnalysisChartGroupViewModel()
            {
                Serie1Definition = new SerieDefinitionViewModel() { ParameterID = "Pt" }
            };
            this.Group3.DataSerie = new FlightAnalysisChartSerieViewModel();
            var serie3 = this.Group3.DataSerie;
            serie3.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Second = 1,
                Value = 3
            });
            serie3.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Second = 2,
                Value = 4
            });
            serie3.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Second = 3,
                Value = 2
            });
            serie3.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Second = 4,
                Value = 1
            });
            serie3.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Second = 5,
                Value = 5
            });
            serie3.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Second = 6,
                Value = 3
            });
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

        private ObservableCollection<PanelViewModelItem> m_panelViewItems
            = new ObservableCollection<PanelViewModelItem>();

        public ObservableCollection<PanelViewModelItem> PanelViewItems
        {
            get { return m_panelViewItems; }
            set
            {
                this.SetProperty<ObservableCollection<PanelViewModelItem>>(ref m_panelViewItems, value);
                this.OnPropertyChanged(string.Empty);
            }
        }

        private int m_selectedPanelIndex = -1;

        public int SelectedPanelIndex
        {
            get { return m_selectedPanelIndex; }
            set
            {
                this.SetProperty<int>(ref m_selectedPanelIndex, value);
                if (m_selectedPanelIndex < -1)
                    return;
                if (m_panelViewItems != null &&
                    m_selectedPanelIndex >= m_panelViewItems.Count)
                    return;

                RelatedParameterAndGroupInit();

                this.OnPropertyChanged(string.Empty);
            }
        }

        public PanelViewModelItem CurrentPanel
        {
            get
            {
                if (this.m_panelViewItems != null
                    && this.m_selectedPanelIndex >= 0
                    && this.m_panelViewItems.Count > this.m_selectedPanelIndex)
                {
                    return m_panelViewItems[this.m_selectedPanelIndex];
                }
                return null;
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

        private string m_preSetCurrentPanelID = string.Empty;

        public void SetCurrentPanel(string p)
        {
            m_preSetCurrentPanelID = p;
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

            if (this.CurrentPanel != null)
            {
                IEnumerable<string> parameterids = this.CurrentPanel.GetParameterIDs();
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

        public bool IsPanel1Selected
        {
            get
            {
                if (m_selectedPanelIndex == 0)
                    return true;
                return false;
            }
        }

        public bool IsPanel2Selected
        {
            get
            {
                if (m_selectedPanelIndex == 1)
                    return true;
                return false;
            }
        }

        public bool IsPanel3Selected
        {
            get
            {
                if (m_selectedPanelIndex == 2)
                    return true;
                return false;
            }
        }

        public bool IsPanel4Selected
        {
            get
            {
                if (m_selectedPanelIndex == 3)
                    return true;
                return false;
            }
        }

        public bool IsPanel5Selected
        {
            get
            {
                if (m_selectedPanelIndex == 4)
                    return true;
                return false;
            }
        }

        public bool IsPanel6Selected
        {
            get
            {
                if (m_selectedPanelIndex == 5)
                    return true;
                return false;
            }
        }

        public bool IsPanel7Selected
        {
            get
            {
                if (m_selectedPanelIndex == 6)
                    return true;
                return false;
            }
        }

        public bool IsPanel8Selected
        {
            get
            {
                if (m_selectedPanelIndex == 7)
                    return true;
                return false;
            }
        }

        public bool IsPanel9Selected
        {
            get
            {
                if (m_selectedPanelIndex == 8)
                    return true;
                return false;
            }
        }

        public bool IsPanel10Selected
        {
            get
            {
                if (m_selectedPanelIndex == 9)
                    return true;
                return false;
            }
        }

        public bool IsPanel11Selected
        {
            get
            {
                if (m_selectedPanelIndex == 10)
                    return true;
                return false;
            }
        }

        public bool IsPanel12Selected
        {
            get
            {
                if (m_selectedPanelIndex == 11)
                    return true;
                return false;
            }
        }

        public string Panel1Caption
        {
            get
            {
                if (m_panelViewItems.Count >= 1)
                    return m_panelViewItems[0].PanelName;
                return string.Empty;
            }
        }

        public string Panel2Caption
        {
            get
            {
                if (m_panelViewItems.Count >= 2)
                    return m_panelViewItems[1].PanelName;
                return string.Empty;
            }
        }

        public string Panel3Caption
        {
            get
            {
                if (m_panelViewItems.Count >= 3)
                    return m_panelViewItems[2].PanelName;
                return string.Empty;
            }
        }

        public string Panel4Caption
        {
            get
            {
                if (m_panelViewItems.Count >= 4)
                    return m_panelViewItems[3].PanelName;
                return string.Empty;
            }
        }

        public string Panel5Caption
        {
            get
            {
                if (m_panelViewItems.Count >= 5)
                    return m_panelViewItems[4].PanelName;
                return string.Empty;
            }
        }

        public string Panel6Caption
        {
            get
            {
                if (m_panelViewItems.Count >= 6)
                    return m_panelViewItems[5].PanelName;
                return string.Empty;
            }
        }

        public string Panel7Caption
        {
            get
            {
                if (m_panelViewItems.Count >= 7)
                    return m_panelViewItems[6].PanelName;
                return string.Empty;
            }
        }

        public string Panel8Caption
        {
            get
            {
                if (m_panelViewItems.Count >= 8)
                    return m_panelViewItems[7].PanelName;
                return string.Empty;
            }
        }

        public string Panel9Caption
        {
            get
            {
                if (m_panelViewItems.Count >= 9)
                    return m_panelViewItems[8].PanelName;
                return string.Empty;
            }
        }

        public string Panel10Caption
        {
            get
            {
                if (m_panelViewItems.Count >= 10)
                    return m_panelViewItems[9].PanelName;
                return string.Empty;
            }
        }

        public string Panel11Caption
        {
            get
            {
                if (m_panelViewItems.Count >= 11)
                    return m_panelViewItems[10].PanelName;
                return string.Empty;
            }
        }

        public string Panel12Caption
        {
            get
            {
                if (m_panelViewItems.Count >= 12)
                    return m_panelViewItems[11].PanelName;
                return string.Empty;
            }
        }

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

        private async void InitializeCore()
        {
            //GetChartPanels
            await InitPanelViewModel();

            //2. currentPanel
            await SetCurrentPanelCore();

            //3. serie definitions
            await FinishBySetSerieDefs();
            System.Diagnostics.Debug.WriteLine(string.Format("End analysis ViewModel :{0} ", DateTime.Now));
        }

        private async Task InitPanelViewModel()
        {
            var chartPanels = ApplicationContext.Instance.GetChartPanels(
                ApplicationContext.Instance.CurrentAircraftModel);

            await this.UserThreadInvoker.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                new Windows.UI.Core.DispatchedHandler(
                    () =>
                    {
                        //1. GetChartPanels and assign ViewModelItem
                        foreach (var panel in chartPanels)
                        {
                            this.m_panelViewItems.Add(new PanelViewModelItem(panel));
                        }
                    }));
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

                        //4. Stop Intermediate
                        this.IsLoading = false;
                    }));
        }

        private async Task SetCurrentPanelCore()
        {
            bool selected = false;
            if (!string.IsNullOrEmpty(m_preSetCurrentPanelID))
            {
                for (int j = 0; j < m_panelViewItems.Count; j++)
                {
                    if (m_panelViewItems[j].PanelID == m_preSetCurrentPanelID)
                    {
                        await this.UserThreadInvoker.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                            new Windows.UI.Core.DispatchedHandler(
                                () =>
                                {
                                    this.SelectedPanelIndex = j;
                                }));
                        selected = true;
                        break;
                    }
                }
            }
            if (selected == false)
            {
                await this.UserThreadInvoker.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                    new Windows.UI.Core.DispatchedHandler(
                        () =>
                        {
                            this.SelectedPanelIndex = 0;
                        }));
            }
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
    }

    public class PanelViewModelItem : BindableBase
    {
        private FlightDataEntitiesRT.Charts.ChartPanel m_panel;

        public PanelViewModelItem(FlightDataEntitiesRT.Charts.ChartPanel panel)
        {
            this.m_panel = panel;
        }

        public string PanelName
        {
            get
            {
                return m_panel.PanelName;
            }
        }

        public string PanelID
        {
            get
            {
                return m_panel.PanelID;
            }
        }

        private bool m_isPanelSelected = false;

        public bool IsPanelSelected
        {
            get
            {
                return m_isPanelSelected;
            }
            set
            {
                this.SetProperty<bool>(ref m_isPanelSelected, value);
            }
        }

        internal IEnumerable<string> GetParameterIDs()
        {
            return m_panel.ParameterIDs;
        }
    }

    public class FlightAnalysisChartGroupFactory
    {
        /// <summary>
        /// 按照：
        /// KG(开关量)分一组
        /// T6L、T6R分一组
        /// NHL、NHR分一组  
        /// 的规则分组之后返回ParameterID
        /// </summary>
        /// <param name="parameterIDs"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, IEnumerable<string>>>
            CalculateBindingGroups(IEnumerable<string> parameterIDs)
        {
            if (parameterIDs == null
                || parameterIDs.Count() <= 0)
                return new KeyValuePair<string, IEnumerable<string>>[] { };


            List<KeyValuePair<string, IEnumerable<string>>> list
                = new List<KeyValuePair<string, IEnumerable<string>>>();
            //List<string> kgGroup = null; //new KeyValuePair<string, IEnumerable<string>>();
            List<string> t6Group = null; //new KeyValuePair<string, IEnumerable<string>>();
            List<string> nhGroup = null;//new KeyValuePair<string, IEnumerable<string>>();
            Dictionary<string, List<string>> objMap = new Dictionary<string, List<string>>();

            //int item = 0;
            foreach (var res in parameterIDs)
            {
                //list.Add(new KeyValuePair<string, IEnumerable<string>>(
                //    //key, 
                //    res, new string[] { res }));
                //continue;
                //debug

                string key = res;
                //if (res.StartsWith("KG"))
                //{
                //    key = "KG";
                //}
                //else
                if (res.StartsWith("T6"))
                {
                    key = "T6";
                }
                else if (res.StartsWith("NH"))
                {
                    key = "NH";
                }
                else
                {
                    list.Add(new KeyValuePair<string, IEnumerable<string>>(
                        key, new string[] { res }));
                    continue;
                }

                //if (key == "KG")
                //{
                //    if (kgGroup != null)
                //        kgGroup.Add(res);
                //    else
                //    {
                //        kgGroup = new List<string>();
                //        kgGroup.Add(res);
                //        list.Add(new KeyValuePair<string, IEnumerable<string>>(key, kgGroup));
                //    }
                //}
                //else 
                if (key == "T6")
                {
                    if (t6Group != null)
                        t6Group.Add(res);
                    else
                    {
                        t6Group = new List<string>();
                        t6Group.Add(res);
                        list.Add(new KeyValuePair<string, IEnumerable<string>>(key, t6Group));
                    }
                }
                else if (key == "NH")
                {
                    if (nhGroup != null)
                        nhGroup.Add(res);
                    else
                    {
                        nhGroup = new List<string>();
                        nhGroup.Add(res);
                        list.Add(new KeyValuePair<string, IEnumerable<string>>(key, nhGroup));
                    }
                }
            }

            return list;
        }

        public static int BrushCounter = 0;

        /// <summary>
        /// 返回轮转的颜色Brush，
        /// 并且轮转的颜色值加1
        /// </summary>
        /// <returns></returns>
        internal static Windows.UI.Xaml.Media.Brush LoadOneBrushPlus()
        {
            var brush = AircraftDataAnalysisWinRT.Styles.AircraftDataAnalysisGlobalPallete.Brushes[BrushCounter];
            BrushCounter = (BrushCounter + 1) % AircraftDataAnalysisWinRT.Styles.AircraftDataAnalysisGlobalPallete.Brushes.Length;
            return brush;
        }
    }
}
