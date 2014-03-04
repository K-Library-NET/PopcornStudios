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
    public class FlightAnalysisViewModel : BindableBase
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

            this.Group1 = new FlightAnalysisChartGroupViewModel();
            this.Group1.Serie1 = new FlightAnalysisChartSerieViewModel() { ParameterID = "Hp" };
            var serie1 = this.Group1.Serie1;
            serie1.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Label = 1,
                Value = 3
            });
            serie1.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Label = 2,
                Value = 4
            });
            serie1.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Label = 3,
                Value = 2
            });
            serie1.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Label = 4,
                Value = 1
            });
            serie1.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Label = 5,
                Value = 5
            });
            serie1.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Label = 6,
                Value = 3
            });

            this.Group2 = new FlightAnalysisChartGroupViewModel();
            this.Group2.Serie2 = new FlightAnalysisChartSerieViewModel() { ParameterID = "Tt" };
            var serie2 = this.Group2.Serie2;
            serie2.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Label = 1,
                Value = 3
            });
            serie2.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Label = 2,
                Value = 4
            });
            serie2.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Label = 3,
                Value = 2
            });
            serie2.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Label = 4,
                Value = 1
            });
            serie2.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Label = 5,
                Value = 5
            });
            serie2.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Label = 6,
                Value = 3
            });

            this.Group3 = new FlightAnalysisChartGroupViewModel();
            this.Group3.Serie3 = new FlightAnalysisChartSerieViewModel() { ParameterID = "Pt" };
            var serie3 = this.Group3.Serie3;
            serie3.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Label = 1,
                Value = 3
            });
            serie3.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Label = 2,
                Value = 4
            });
            serie3.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Label = 3,
                Value = 2
            });
            serie3.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Label = 4,
                Value = 1
            });
            serie3.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Label = 5,
                Value = 5
            });
            serie3.Add(new AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint()
            {
                Label = 6,
                Value = 3
            });
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
                        this.Group1 = new FlightAnalysisChartGroupViewModel();
                        this.LoadAndAssignValue(this.Group1, groupItem, dataLoader);
                        groupViewModel = this.Group1;
                        break;
                    }
                case 1:
                    {
                        this.Group2 = new FlightAnalysisChartGroupViewModel();
                        this.LoadAndAssignValue(this.Group2, groupItem, dataLoader);
                        groupViewModel = this.Group2;
                        break;
                    }
                case 2:
                    {
                        this.Group3 = new FlightAnalysisChartGroupViewModel();
                        this.LoadAndAssignValue(this.Group3, groupItem, dataLoader);
                        groupViewModel = this.Group3;
                        break;
                    }
                case 3:
                    {
                        this.Group4 = new FlightAnalysisChartGroupViewModel();
                        this.LoadAndAssignValue(this.Group4, groupItem, dataLoader);
                        groupViewModel = this.Group4;
                        break;
                    }
                case 4:
                    {
                        this.Group5 = new FlightAnalysisChartGroupViewModel();
                        this.LoadAndAssignValue(this.Group5, groupItem, dataLoader);
                        groupViewModel = this.Group5;
                        break;
                    }
                case 5:
                    {
                        this.Group6 = new FlightAnalysisChartGroupViewModel();
                        this.LoadAndAssignValue(this.Group6, groupItem, dataLoader);
                        groupViewModel = this.Group6;
                        break;
                    }
                case 6:
                    {
                        this.Group7 = new FlightAnalysisChartGroupViewModel();
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
            int j = 0;
            foreach (var parameterID in groupItem.Value)
            {
                var vm = new FlightAnalysisChartSerieViewModel();
                vm.ParameterID = parameterID;
                this.LoadSimpleDataPoints(vm, parameterID, dataLoader);
                if (j == 0)
                {
                    groupViewModel.Serie1 = vm;
                }
                else if (j == 1)
                {
                    groupViewModel.Serie2 = vm;
                }
                else if (j == 2)
                {
                    groupViewModel.Serie3 = vm;
                }
                j++;
            }
        }

        private void LoadSimpleDataPoints(FlightAnalysisChartSerieViewModel vm, string parameterID,
            Domain.AircraftAnalysisDataLoader dataLoader)
        {
            IEnumerable<ParameterRawData> rawdatas = dataLoader.GetRawData(parameterID);
            if (rawdatas == null)
                return;
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
            foreach (var rd in rawdatas)
            {
                vm.Add(new MyControl.SimpleDataPoint()
                {
                    Label = rd.Second,
                    Value = rd.Values[0]//暂时先写死第一秒钟的值
                });
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

        private void InitializeCore()
        {
            //1. GetChartPanels
            var chartPanels = ApplicationContext.Instance.GetChartPanels(
                ApplicationContext.Instance.CurrentAircraftModel);
            this.UserThreadInvoker.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                new Windows.UI.Core.DispatchedHandler(
                    () =>
                    {
                        foreach (var panel in chartPanels)
                        {
                            this.m_panelViewItems.Add(new PanelViewModelItem(panel));
                        }

                        //2. currentPanel
                        if (!string.IsNullOrEmpty(m_preSetCurrentPanelID))
                        {
                            for (int j = 0; j < m_panelViewItems.Count; j++)
                            {
                                if (m_panelViewItems[j].PanelID == m_preSetCurrentPanelID)
                                {
                                    this.SelectedPanelIndex = j;
                                    return;
                                }
                            }
                        }

                        this.SelectedPanelIndex = 0;
                    }));
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

        private string m_preSetCurrentPanelID = string.Empty;

        public void SetCurrentPanel(string p)
        {
            m_preSetCurrentPanelID = p;
        }

        public Windows.UI.Core.CoreDispatcher UserThreadInvoker { get; set; }
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
            List<string> kgGroup = null; //new KeyValuePair<string, IEnumerable<string>>();
            List<string> t6Group = null; //new KeyValuePair<string, IEnumerable<string>>();
            List<string> nhGroup = null;//new KeyValuePair<string, IEnumerable<string>>();
            Dictionary<string, List<string>> objMap = new Dictionary<string, List<string>>();

            int item = 0;
            foreach (var res in parameterIDs)
            {
                string key = res;
                if (res.StartsWith("KG"))
                {
                    key = "KG";
                }
                else if (res.StartsWith("T6"))
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

                if (key == "KG")
                {
                    if (kgGroup != null)
                        kgGroup.Add(res);
                    else
                    {
                        kgGroup = new List<string>();
                        kgGroup.Add(res);
                        list.Add(new KeyValuePair<string, IEnumerable<string>>(key, kgGroup));
                    }
                }
                else if (key == "T6")
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
