using AircraftDataAnalysisWinRT;
using AircraftDataAnalysisWinRT.DataModel;
using AircraftDataAnalysisWinRT.Domain;
using Syncfusion.UI.Xaml.Controls.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “基本页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234237 上有介绍

namespace PStudio.WinApp.Aircraft.FDAPlatform.Domain
{
    /// <summary>
    /// 基本页，提供大多数应用程序通用的特性。
    /// </summary>
    public sealed partial class FlightAnalysis : AircraftDataAnalysisWinRT.Common.LayoutAwarePage,
        AircraftDataAnalysisModel1.WinRT.MyControl.ITrackerParent
    {
        public FlightAnalysis()
        {
            this.InitializeComponent();

            m_charts.Add(this.tracker1);
            m_charts.Add(this.tracker2);
            m_charts.Add(this.tracker3);
            m_charts.Add(this.tracker4);
            m_charts.Add(this.tracker5);
            m_charts.Add(this.tracker6);
            m_charts.Add(this.tracker7);

            this.tracker1.TrackerParent = this;
            this.tracker2.TrackerParent = this;
            this.tracker3.TrackerParent = this;
            this.tracker4.TrackerParent = this;
            this.tracker5.TrackerParent = this;
            this.tracker6.TrackerParent = this;
            this.tracker7.TrackerParent = this;

            this.tracker1.LineBrush = AircraftDataAnalysisWinRT.Styles.AircraftDataAnalysisGlobalPallete.Brushes[0];
            this.tracker2.LineBrush = AircraftDataAnalysisWinRT.Styles.AircraftDataAnalysisGlobalPallete.Brushes[1];
            this.tracker3.LineBrush = AircraftDataAnalysisWinRT.Styles.AircraftDataAnalysisGlobalPallete.Brushes[3];
            this.tracker4.LineBrush = AircraftDataAnalysisWinRT.Styles.AircraftDataAnalysisGlobalPallete.Brushes[3];
            this.tracker5.LineBrush = AircraftDataAnalysisWinRT.Styles.AircraftDataAnalysisGlobalPallete.Brushes[4];
            this.tracker6.LineBrush = AircraftDataAnalysisWinRT.Styles.AircraftDataAnalysisGlobalPallete.Brushes[5];
            this.tracker7.LineBrush = AircraftDataAnalysisWinRT.Styles.AircraftDataAnalysisGlobalPallete.Brushes[6];
        }

        private List<AircraftDataAnalysisModel1.WinRT.MyControl.DataPointTracker> m_charts
            = new List<AircraftDataAnalysisModel1.WinRT.MyControl.DataPointTracker>();

        public void NotifyOtherTracker(object sender, PointerRoutedEventArgs e)
        {
            foreach (var t in m_charts)
            {
                t.OnOtherTrackerNotify(sender, e);
            }
        }

        public void SetCoordinate(double unscaledX, double unscaledY,
            IEnumerable<AircraftDataAnalysisModel1.WinRT.MyControl.SimpleDataPoint> source)
        {
            throw new NotImplementedException();
        }

        private FlightAnalysisViewModel m_viewModel = null;

        private void OnNavigateToHome_Clicked(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        void item_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            System.Diagnostics.Debug.WriteLine(string.Format("Start analysis:{0}", DateTime.Now));

            var chartPanels = ApplicationContext.Instance.GetChartPanels(ApplicationContext.Instance.CurrentAircraftModel);
            IEnumerable<PanelChangedWrap> allPanels = null;

            if (chartPanels != null && chartPanels.Count() > 0)
            {
                var converted = from one in chartPanels
                                select new PanelChangedWrap() { SelectedPanel = one };
                allPanels = converted;
            }

            m_viewModel = new FlightAnalysisViewModel()
            {
                CurrentStartSecond = ApplicationContext.Instance.CurrentFlight.StartSecond,
                CurrentEndSecond = ApplicationContext.Instance.CurrentFlight.EndSecond
            };

            PanelChangedWrap wrapPanel = this.GetCurrentPanel(allPanels, e.Parameter);

            if (wrapPanel == null)
                return;

            NavigationToPanelAsync(wrapPanel, allPanels);
        }

        private PanelChangedWrap GetCurrentPanel(IEnumerable<PanelChangedWrap> allPanels, object navigateParameter)
        {
            var chartPanels = allPanels;
            //ApplicationContext.Instance.GetChartPanels(ApplicationContext.Instance.CurrentAircraftModel);

            if (chartPanels != null && chartPanels.Count() > 0)
            {
                if (navigateParameter != null && navigateParameter is FlightAnalysisNavigationParameter)
                {
                    FlightAnalysisNavigationParameter parameter = navigateParameter as FlightAnalysisNavigationParameter;

                    foreach (var c in chartPanels)
                    {
                        string selectedPanelID = parameter.SelectedPanelID;
                        if (c.SelectedPanel.PanelID == selectedPanelID)
                            return c;
                    }
                }

                var selected = chartPanels.First();

                return selected;
            }

            return null;
        }

        private async Task NavigationToPanelAsync(PanelChangedWrap wrapPanel, IEnumerable<PanelChangedWrap> allPanels)
        {
            if (wrapPanel == null || wrapPanel.SelectedPanel == null
                || string.IsNullOrEmpty(wrapPanel.SelectedPanel.PanelID))
                return;

            SetSelectedPanel(wrapPanel, allPanels);

            BindDataCore(wrapPanel, allPanels);
        }

        void content_FlightAnalysisSubNavigationRequested(object sender, EventArgs e)
        {
            if (e != null && e is AircraftDataAnalysisWinRT.MyControl.FAChart.FlightAnalysisSubNavigateEventArgs)
            {
                AircraftDataAnalysisWinRT.MyControl.FAChart.FlightAnalysisSubNavigateEventArgs args = e as AircraftDataAnalysisWinRT.MyControl.FAChart.FlightAnalysisSubNavigateEventArgs;
                this.Frame.Navigate(typeof(FlightAnalysisSub), args.Parameter);
            }
        }

        private void SetSelectedPanel(PanelChangedWrap wrapPanel, IEnumerable<PanelChangedWrap> allPanels)
        {
            var parameters = this.GetFlightParameters(wrapPanel.SelectedPanel.ParameterIDs);

            this.m_viewModel.RelatedParameterCollection.Clear();

            foreach (var par in parameters)
            {
                this.m_viewModel.RelatedParameterCollection.Add(
                    new RelatedParameterViewModel(this.m_viewModel, par)
                    );
            }

            m_viewModel.RefreshAndRetriveData();
            ApplicationContext.Instance.SetCurrentViewModel(ApplicationContext.Instance.CurrentFlight, m_viewModel);
        }

        private void BindDataCore(PanelChangedWrap wrapPanel, IEnumerable<PanelChangedWrap> allPanels)
        {
            //1. m_viewModel绑定左边列表
            this.panelParameterListCtrl.DataContext = m_viewModel;

            //2. btPanels 绑定Selected Panel
            FlightAnalysisCommandViewModel commandViewModel = new FlightAnalysisCommandViewModel(
                m_viewModel, allPanels, this.Frame) { SelectedPanel = wrapPanel };

            this.btPanel1.DataContext = commandViewModel;
            this.btPanel2.DataContext = commandViewModel;
            this.btPanel3.DataContext = commandViewModel;
            this.btPanel4.DataContext = commandViewModel;
            this.btPanel5.DataContext = commandViewModel;
            this.btPanel6.DataContext = commandViewModel;
            this.btPanel7.DataContext = commandViewModel;
            this.btPanel8.DataContext = commandViewModel;
            this.btPanel9.DataContext = commandViewModel;
            this.btPanel10.DataContext = commandViewModel;
            this.btPanel11.DataContext = commandViewModel;
            this.btPanel12.DataContext = commandViewModel;

            //3. 根据当前Selected的面板加载数据
            for (int i = 0; i < this.m_charts.Count; i++)
            {
                if (i < this.m_viewModel.RelatedParameterCollection.Count)
                {
                    var related = this.m_viewModel.RelatedParameterCollection[i];
                    System.Collections.ObjectModel.ObservableCollection<SimpleDataPoint> points
                        = this.GetRelatedData(m_viewModel, related);
                    this.m_charts[i].DataContext = points;
                }
                else
                {
                    this.m_charts[i].Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    if (this.m_charts[i].Parent != null &&
                        this.m_charts[i].Parent is UIElement)
                    {
                        (this.m_charts[i].Parent as UIElement).Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    }
                }
            }


            //debug
            return;
            //debug

            /*
            Task.Run(new Action(async delegate()
            {
                System.Diagnostics.Debug.WriteLine(string.Format("before bind analysis:{0}", DateTime.Now));
                m_viewModel.RefreshAndRetriveData();
                System.Diagnostics.Debug.WriteLine(string.Format("after data retrieved:{0}", DateTime.Now));
                var res3 = this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                        new Windows.UI.Core.DispatchedHandler(delegate()
                        {
                            this.DataContext = m_viewModel;
                            this.lvParameters.ItemsSource = m_viewModel.RelatedParameterCollection;
                        }));

                var res2 = this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                        new Windows.UI.Core.DispatchedHandler(delegate()
                        {
                            this.chartCtrl.ViewModel = m_viewModel;
                        })
                );
                //this.grdCtrl.BindGridData();// = m_viewModel;
                var res1 = this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                        new Windows.UI.Core.DispatchedHandler(delegate()
                        {
                            //this.grdData.Columns.Clear();
                            //this.grdData.ItemsSource = m_viewModel.RawDatas;
                            this.RebindColumns();
                            //this.grdData.ItemsSource = m_viewModel.EntityBindingCollection.RawDataItems;
                            //this.grdCtrl.ReBindColumns();
                        })
                );
                await res1;
                await res2;
                await res3;
                System.Diagnostics.Debug.WriteLine(string.Format("End analysis:{0}", DateTime.Now));
            }));*/
        }

        private System.Collections.ObjectModel.ObservableCollection<SimpleDataPoint>
            GetRelatedData(FlightAnalysisViewModel m_viewModel, RelatedParameterViewModel related)
        {
            throw new NotImplementedException();
        }

        private void RebindColumns()
        {
            var result2 = GetFlightParameters(null);
            var related = from o1 in this.m_viewModel.RelatedParameterCollection
                          select o1.Parameter.ParameterID;

            //foreach (var cc in this.grdData.Columns)
            //{
            //    cc.AllowSorting = false;
            //    //cc.MinimumWidth = 80;
            //    //cc.ColumnSizer = Syncfusion.UI.Xaml.Grid.GridLengthUnitType.Auto;
            //    cc.TextAlignment = Windows.UI.Xaml.TextAlignment.Center;
            //    cc.HorizontalHeaderContentAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;

            //    if (result2 != null && result2.Count() > 0)
            //    {
            //        cc.IsHidden = related.Contains(cc.MappingName) ? false : true;
            //        //int i = 0;
            //        //foreach (var one in result2)
            //        //{
            //        //    Syncfusion.UI.Xaml.Grid.GridTextColumn col
            //        //        = new Syncfusion.UI.Xaml.Grid.GridTextColumn()
            //        //        {
            //        //            MappingName = one.ParameterID,
            //        //            HeaderText = one.Caption,
            //        //            AllowSorting = false,
            //        //            MinimumWidth = 80,
            //        //            ColumnSizer = Syncfusion.UI.Xaml.Grid.GridLengthUnitType.Auto,
            //        //            TextAlignment = Windows.UI.Xaml.TextAlignment.Center,
            //        //            HorizontalHeaderContentAlignment = Windows.UI.Xaml.HorizontalAlignment.Center
            //        //        };
            //        //    col.IsHidden = related.Contains(one.ParameterID) ? false : true;
            //        //    this.grdData.Columns.Add(col);
            //        //    i++;
            //        //}
            //    }
            //}

            //this.grdData.Columns["Second"].HeaderText = "秒值";
            //this.grdData.Columns["Second"].IsHidden = false;

            //this.grdData.Columns["Second"].TextAlignment = Windows.UI.Xaml.TextAlignment.Center;
            //this.grdData.Columns["Second"].HorizontalHeaderContentAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
        }

        private FlightDataEntitiesRT.FlightParameter[] GetFlightParameters(IEnumerable<string> parameterIDs)
        {
            var flightParameters = ApplicationContext.Instance.GetFlightParameters(
                ApplicationContext.Instance.CurrentAircraftModel);

            var result = from one in flightParameters.Parameters
                         where one.ParameterID != "(NULL)" && this.ExistsParameter(one.ParameterID)
                         && (parameterIDs == null || parameterIDs.Count() <= 0 ||
                         parameterIDs.Contains(one.ParameterID))
                         //m_viewModel.RelatedParameterSelected(one.ParameterID)
                         select one;

            return result.ToArray();
        }

        private bool ExistsParameter(string p)
        {
            if (this.m_viewModel.AllParameterIDs.Contains(p))
                return true;
            return false;
        }

        private int GetPanelSelectedIndex(IEnumerable<FlightDataEntitiesRT.Charts.ChartPanel> panels2, string panelID)
        {
            int i = 0;
            foreach (var p in panels2)
            {
                if (p != null && p.PanelID == panelID)
                    return i;
                i++;
            }
            return -1;
        }

        [Obsolete("重设绑定和消息机制")]
        private void BindCurrentPanelData()
        {
            m_viewModel.CurrentStartSecond = 0;
            m_viewModel.CurrentEndSecond = ApplicationContext.Instance.CurrentFlight.EndSecond;

            System.Diagnostics.Debug.WriteLine(string.Format("before bind analysis:{0}", DateTime.Now));
            m_viewModel.RefreshAndRetriveData();
            System.Diagnostics.Debug.WriteLine(string.Format("after data retrieved:{0}", DateTime.Now));
            this.DataContext = m_viewModel;

            //this.chartUc1.ViewModel = m_viewModel;

            //this.lvParameters.ItemsSource = m_viewModel.RelatedParameterCollection;
            ////this.grdCtrl.ViewModel = m_viewModel;
            //this.chartCtrl.ViewModel = m_viewModel;
        }

        /// <summary>
        /// 使用在导航过程中传递的内容填充页。在从以前的会话
        /// 重新创建页时，也会提供任何已保存状态。
        /// </summary>
        /// <param name="navigationParameter">最初请求此页时传递给
        /// <see cref="Frame.Navigate(Type, Object)"/> 的参数值。
        /// </param>
        /// <param name="pageState">此页在以前会话期间保留的状态
        /// 字典。首次访问页面时为 null。</param>
        //protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        //{
        //}

        ///// <summary>
        ///// 保留与此页关联的状态，以防挂起应用程序或
        ///// 从导航缓存中放弃此页。值必须符合
        ///// <see cref="SuspensionManager.SessionState"/> 的序列化要求。
        ///// </summary>
        ///// <param name="pageState">要使用可序列化状态填充的空字典。</param>
        //protected override void SaveState(Dictionary<String, Object> pageState)
        //{
        //}

        private bool m_switcher = false;

        //private void cbPanelSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ////重做Navigation吧，所有加载都在Navigation中做
        //    if (m_switcher)
        //        return;

        //    m_switcher = true;
        //    try
        //    {
        //        if (this.tabHost.SelectedItem != null
        //            && this.tabHost.SelectedItem is SfTabItem)
        //        {
        //            var item = this.tabHost.SelectedItem as SfTabItem;
        //            if (item != null && item.Content != null
        //                && !string.IsNullOrEmpty(item.Name))
        //            {
        //                string panelID = item.Name;

        //                this.SetSelectedPanel(panelID);
        //            }
        //        }

        //        //    if (this.cbPanelSelect.SelectedItem != null
        //        //        && this.cbPanelSelect.SelectedItem is FlightDataEntitiesRT.Charts.ChartPanel)
        //        //    {
        //        //        var panel = this.cbPanelSelect.SelectedItem as FlightDataEntitiesRT.Charts.ChartPanel;
        //        //        PanelChangedWrap wrap = new PanelChangedWrap() { SelectedPanel = panel };

        //        //        Task<IEnumerable<FlightDataEntitiesRT.Charts.ChartPanel>> task = Task.Run<
        //        //            IEnumerable<FlightDataEntitiesRT.Charts.ChartPanel>>(
        //        //            new Func<IEnumerable<FlightDataEntitiesRT.Charts.ChartPanel>>(delegate()
        //        //            {
        //        //                var panels = ApplicationContext.Instance.GetChartPanels(
        //        //                    ApplicationContext.Instance.CurrentAircraftModel);
        //        //                return panels;
        //        //            }));

        //        //        //this.Frame.Navigate(typeof(FlightAnalysis), wrap);
        //        //        this.NavigationToPanel(task, wrap);

        //        //        //this.m_viewModel.CurrentPanel = this.cbPanelSelect.SelectedItem as FlightDataEntitiesRT.Charts.ChartPanel;
        //        //        //this.BindCurrentPanelData();
        //        //    }
        //    }
        //    catch (Exception ex) { LogHelper.Error(ex); }
        //    finally
        //    {
        //        m_switcher = false;
        //    }
        //}

        private void tabHost_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {

        }

    }

    public class PanelChangedWrap
    {
        public FlightDataEntitiesRT.Charts.ChartPanel SelectedPanel
        {
            get;
            set;
        }
    }

    public class FlightAnalysisNavigationParameter
    {
        public string SelectedPanelID { get; set; }
    }

    public class FlightAnalysisCommandViewModel : AircraftDataAnalysisWinRT.Common.BindableBase
    {
        public FlightAnalysisCommandViewModel(FlightAnalysisViewModel viewModel,
            IEnumerable<PanelChangedWrap> wrapPanels, Frame frame)
        {
            this.m_viewModel = viewModel;
            this.m_wrapPanels = wrapPanels;

            this.m_panel1SelectedCommand = new FlightAnalysisNavCommand(viewModel,
                m_wrapPanels.ElementAt(0).SelectedPanel.PanelID, false, frame);

            this.m_panel2SelectedCommand = new FlightAnalysisNavCommand(viewModel,
                m_wrapPanels.ElementAt(1).SelectedPanel.PanelID, false, frame);

            this.m_panel3SelectedCommand = new FlightAnalysisNavCommand(viewModel,
                m_wrapPanels.ElementAt(2).SelectedPanel.PanelID, false, frame);

            this.m_panel4SelectedCommand = new FlightAnalysisNavCommand(viewModel,
                m_wrapPanels.ElementAt(3).SelectedPanel.PanelID, false, frame);

            this.m_panel5SelectedCommand = new FlightAnalysisNavCommand(viewModel,
                m_wrapPanels.ElementAt(4).SelectedPanel.PanelID, false, frame);

            this.m_panel6SelectedCommand = new FlightAnalysisNavCommand(viewModel,
                m_wrapPanels.ElementAt(5).SelectedPanel.PanelID, false, frame);

            this.m_panel7SelectedCommand = new FlightAnalysisNavCommand(viewModel,
                m_wrapPanels.ElementAt(6).SelectedPanel.PanelID, false, frame);

            this.m_panel8SelectedCommand = new FlightAnalysisNavCommand(viewModel,
                m_wrapPanels.ElementAt(7).SelectedPanel.PanelID, false, frame);

            this.m_panel9SelectedCommand = new FlightAnalysisNavCommand(viewModel,
                m_wrapPanels.ElementAt(8).SelectedPanel.PanelID, false, frame);

            this.m_panel10SelectedCommand = new FlightAnalysisNavCommand(viewModel,
                m_wrapPanels.ElementAt(9).SelectedPanel.PanelID, false, frame);

            this.m_panel11SelectedCommand = new FlightAnalysisNavCommand(viewModel,
                m_wrapPanels.ElementAt(10).SelectedPanel.PanelID, false, frame);

            this.m_panel12SelectedCommand = new FlightAnalysisNavCommand(viewModel,
                m_wrapPanels.ElementAt(11).SelectedPanel.PanelID, false, frame);
        }

        private PanelChangedWrap m_selectedPanel;

        public PanelChangedWrap SelectedPanel
        {
            get { return m_selectedPanel; }
            set
            {
                this.SetProperty<PanelChangedWrap>(ref m_selectedPanel, value);

                int index = this.FindSelectedIndex();

                switch (index)
                {
                    case 0: { m_panel1SelectedCommand.IsPanelSelected = true; break; }
                    case 1: { m_panel2SelectedCommand.IsPanelSelected = true; break; }
                    case 2: { m_panel3SelectedCommand.IsPanelSelected = true; break; }
                    case 3: { m_panel4SelectedCommand.IsPanelSelected = true; break; }
                    case 4: { m_panel5SelectedCommand.IsPanelSelected = true; break; }
                    case 5: { m_panel6SelectedCommand.IsPanelSelected = true; break; }
                    case 6: { m_panel7SelectedCommand.IsPanelSelected = true; break; }
                    case 7: { m_panel8SelectedCommand.IsPanelSelected = true; break; }
                    case 8: { m_panel9SelectedCommand.IsPanelSelected = true; break; }
                    case 9: { m_panel10SelectedCommand.IsPanelSelected = true; break; }
                    case 10: { m_panel11SelectedCommand.IsPanelSelected = true; break; }
                    case 11: { m_panel12SelectedCommand.IsPanelSelected = true; break; }
                    default: break;
                }
            }
        }

        private int FindSelectedIndex()
        {
            int i = 0;
            foreach (var one in m_wrapPanels)
            {
                if (one.SelectedPanel.PanelID == m_selectedPanel.SelectedPanel.PanelID)
                    return i;
                i++;
            }

            return -1;
        }

        private FlightAnalysisNavCommand m_panel1SelectedCommand = null;

        public ICommand Panel1SelectedCommand
        {
            get
            {
                return m_panel1SelectedCommand;
            }
        }

        private FlightAnalysisNavCommand m_panel2SelectedCommand = null;

        public ICommand Panel2SelectedCommand
        {
            get
            {
                return m_panel2SelectedCommand;
            }
        }

        private FlightAnalysisNavCommand m_panel3SelectedCommand = null;

        public ICommand Panel3SelectedCommand
        {
            get
            {
                return m_panel3SelectedCommand;
            }
        }

        private FlightAnalysisNavCommand m_panel4SelectedCommand = null;

        public ICommand Panel4SelectedCommand
        {
            get
            {
                return m_panel4SelectedCommand;
            }
        }

        private FlightAnalysisNavCommand m_panel5SelectedCommand = null;

        public ICommand Panel5SelectedCommand
        {
            get
            {
                return m_panel5SelectedCommand;
            }
        }

        private FlightAnalysisNavCommand m_panel6SelectedCommand = null;

        public ICommand Panel6SelectedCommand
        {
            get
            {
                return m_panel6SelectedCommand;
            }
        }

        private FlightAnalysisNavCommand m_panel7SelectedCommand = null;

        public ICommand Panel7SelectedCommand
        {
            get
            {
                return m_panel7SelectedCommand;
            }
        }

        private FlightAnalysisNavCommand m_panel8SelectedCommand = null;

        public ICommand Panel8SelectedCommand
        {
            get
            {
                return m_panel8SelectedCommand;
            }
        }

        private FlightAnalysisNavCommand m_panel9SelectedCommand = null;

        public ICommand Panel9SelectedCommand
        {
            get
            {
                return m_panel9SelectedCommand;
            }
        }

        private FlightAnalysisNavCommand m_panel10SelectedCommand = null;

        public ICommand Panel10SelectedCommand
        {
            get
            {
                return m_panel10SelectedCommand;
            }
        }

        private FlightAnalysisNavCommand m_panel11SelectedCommand = null;

        public ICommand Panel11SelectedCommand
        {
            get
            {
                return m_panel11SelectedCommand;
            }
        }

        private FlightAnalysisNavCommand m_panel12SelectedCommand = null;

        private FlightAnalysisViewModel m_viewModel;
        private IEnumerable<PanelChangedWrap> m_wrapPanels;

        public ICommand Panel12SelectedCommand
        {
            get
            {
                return m_panel12SelectedCommand;
            }
        }
    }

    public class FlightAnalysisNavCommand : ICommand
    {
        private bool m_isPanelSelected;

        public bool IsPanelSelected
        {
            get { return m_isPanelSelected; }
            set
            {
                m_isPanelSelected = value;
                if (CanExecuteChanged != null)
                    CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        private string m_panelID;
        private FlightAnalysisViewModel m_viewModel;
        private Frame m_frame;

        public FlightAnalysisNavCommand(FlightAnalysisViewModel viewModel,
            string panelID, bool isPanelSelected, Frame frame)
        {
            this.m_viewModel = viewModel;
            this.m_panelID = panelID;
            this.m_isPanelSelected = isPanelSelected;
            this.m_frame = frame;
        }

        public bool CanExecute(object parameter)
        {
            if (m_isPanelSelected)
                return false;
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            FlightAnalysisNavigationParameter navParameter
                = new FlightAnalysisNavigationParameter() { SelectedPanelID = this.m_panelID };

            m_frame.Navigate(typeof(FlightAnalysis), navParameter);
        }
    }
}
