using AircraftDataAnalysisWinRT;
using AircraftDataAnalysisWinRT.DataModel;
using AircraftDataAnalysisWinRT.Domain;
using Syncfusion.UI.Xaml.Controls.Navigation;
using System;
using System.Collections.Generic;
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

// “基本页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234237 上有介绍

namespace PStudio.WinApp.Aircraft.FDAPlatform.Domain
{
    /// <summary>
    /// 基本页，提供大多数应用程序通用的特性。
    /// </summary>
    public sealed partial class FlightAnalysis : AircraftDataAnalysisWinRT.Common.LayoutAwarePage
    {
        public FlightAnalysis()
        {
            this.InitializeComponent();
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
            PanelChangedWrap wrapPanel = this.GetCurrentPanel(e.Parameter);

            await NavigationToPanel(task, wrapPanel);
        }

        private PanelChangedWrap GetCurrentPanel(object p)
        {


            Task<IEnumerable<FlightDataEntitiesRT.Charts.ChartPanel>> task = Task.Run<
                IEnumerable<FlightDataEntitiesRT.Charts.ChartPanel>>(
                new Func<IEnumerable<FlightDataEntitiesRT.Charts.ChartPanel>>(delegate()
            {
                var panels = ApplicationContext.Instance.GetChartPanels(ApplicationContext.Instance.CurrentAircraftModel);
                return panels;
            }));

            //要处理别的地方导航过来的
            if (e.Parameter != null && e.Parameter is AircraftDataAnalysisWinRT.Common.DecisionWrap)
            {
                DecisionRecordFlightAnalysisViewModel viewModel1 = new DecisionRecordFlightAnalysisViewModel(
                    e.Parameter as AircraftDataAnalysisWinRT.Common.DecisionWrap);
                this.DataContext = viewModel1;
                m_viewModel = viewModel1;
                // this.grdCtrl.ViewModel = viewModel1;
                BindDataCore();
                return;
            }
            else if (e.Parameter != null && e.Parameter is ExtremumReportItemWrap)
            {
                ExtremumInfoFlightAnalysisViewModel viewModel2 = new ExtremumInfoFlightAnalysisViewModel(
                    e.Parameter as ExtremumReportItemWrap);
                this.DataContext = viewModel2;
                m_viewModel = viewModel2;
                // this.grdCtrl.ViewModel = viewModel2;
                BindDataCore();
                return;
            }
            //处理别的地方导航过来的

            if (e.Parameter != null && e.Parameter is PanelChangedWrap
               && (e.Parameter as PanelChangedWrap).SelectedPanel != null)
            {
                wrapPanel = e.Parameter as PanelChangedWrap;
            }


            throw new NotImplementedException();
        }

        private async Task NavigationToPanel(Task<IEnumerable<FlightDataEntitiesRT.Charts.ChartPanel>> task,
            PanelChangedWrap wrapPanel)
        {
            string panelID = string.Empty;
            FlightAnalysisViewModel viewModel = new FlightAnalysisViewModel()
            {
                CurrentStartSecond = ApplicationContext.Instance.CurrentFlight.StartSecond,
                CurrentEndSecond = ApplicationContext.Instance.CurrentFlight.EndSecond
            };

            if (wrapPanel != null)
            {
                panelID = wrapPanel.SelectedPanel.PanelID;
            }
            else
            {
                viewModel.CurrentStartSecond = 0;
                viewModel.CurrentEndSecond = ApplicationContext.Instance.CurrentFlight.EndSecond;
            }
            //要么是没有选择ID的，那就是第一个Panel
            var panels2 = await task;
            System.Diagnostics.Debug.WriteLine(string.Format("Flight Analysis Model Created:{0}", DateTime.Now));
            this.tabHost.Items.Clear();
            //create tabs
            if (panels2 != null && panels2.Count() > 0)
            {
                foreach (var pan in panels2)
                {
                    var content = new AircraftDataAnalysisWinRT.MyControl.FAChart()
                               {
                                   DataContext = viewModel,
                                   ViewModel = viewModel,
                               };
                    content.FlightAnalysisSubNavigationRequested += content_FlightAnalysisSubNavigationRequested;
                    SfTabItem item = new SfTabItem()
                    {
                        Header = pan.PanelName,
                        Content = content,
                        //new ScrollViewer()
                        //{
                        //    HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
                        //    HorizontalScrollMode = ScrollMode.Disabled,
                        //    VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                        //    VerticalScrollMode = ScrollMode.Auto,
                        //    Content =

                        //}
                    };
                    item.DoubleTapped += item_DoubleTapped;
                    item.Name = pan.PanelID;
                    this.tabHost.Items.Add(item);
                }
            }


            //开始注意消息循环了，最后才绑定viewModel;
            //m_viewModel = viewModel;
            if (string.IsNullOrEmpty(panelID))
            {
                System.Diagnostics.Debug.Assert(panels2 != null && panels2.Count() > 0
                    && panels2.First() != null);
                panelID = panels2.First().PanelID;
            }
            //如果还是空，则直接Return
            if (string.IsNullOrEmpty(panelID))
                return;
            m_viewModel = viewModel;

            SetSelectedPanel(panelID);
            //m_viewModel.RefreshAndRetriveData();

            //this.chartTest.SubViewModel = new FlightAnalysisSubViewModel() = m_viewModel;
            return;

            //debug

            //TODO: rebuild bindingPanel
            /*
            this.cbPanelSelect.ItemsSource = panels2;
            int index = this.GetPanelSelectedIndex(panels2, panelID);
            m_switcher = true;
            this.cbPanelSelect.SelectedIndex = index;
            m_switcher = false;
            viewModel.CurrentPanel = panels2.ElementAt(index);
            m_viewModel = viewModel;*/
            //this.grdCtrl.ViewModel = m_viewModel;
            //this.grdCtrl.ReBindColumns();
            BindDataCore();
        }

        void content_FlightAnalysisSubNavigationRequested(object sender, EventArgs e)
        {
            if (e != null && e is AircraftDataAnalysisWinRT.MyControl.FAChart.FlightAnalysisSubNavigateEventArgs)
            {
                AircraftDataAnalysisWinRT.MyControl.FAChart.FlightAnalysisSubNavigateEventArgs args = e as AircraftDataAnalysisWinRT.MyControl.FAChart.FlightAnalysisSubNavigateEventArgs;
                this.Frame.Navigate(typeof(FlightAnalysisSub), args.Parameter);
            }
        }

        private void SetSelectedPanel(string panelID)
        {
            var parameters = this.GetFlightParameters();

            foreach (var tb in this.tabHost.Items)
            {
                if (tb != null && tb is SfTabItem)
                {
                    SfTabItem tabItem = tb as SfTabItem;
                    if (tabItem.Name == panelID)
                    {
                        m_viewModel.RelatedParameterCollection.Clear();
                        this.tabHost.SelectedItem = tabItem;

                        var pars = ApplicationContext.Instance.GetChartPanels(
                            ApplicationContext.Instance.CurrentAircraftModel);
                        foreach (var p in pars)
                        {
                            if (panelID == p.PanelID)
                            {
                                var prResult = from pr in parameters
                                               where p.ParameterIDs.Contains(pr.ParameterID)
                                               select pr;

                                foreach (var pid in prResult)
                                {
                                    m_viewModel.RelatedParameterCollection.Add(new RelatedParameterViewModel(m_viewModel, true, pid));
                                }
                                break;
                            }
                        }
                        break;
                    }
                }
            }

            m_viewModel.RefreshAndRetriveData();
            ApplicationContext.Instance.SetCurrentViewModel(ApplicationContext.Instance.CurrentFlight, m_viewModel);
        }

        private void BindDataCore()
        {//debug
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

        private void RebindColumns()
        {
            var result2 = GetFlightParameters();
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

        private FlightDataEntitiesRT.FlightParameter[] GetFlightParameters()
        {
            var flightParameters = ApplicationContext.Instance.GetFlightParameters(
                ApplicationContext.Instance.CurrentAircraftModel);

            var result = from one in flightParameters.Parameters
                         where one.ParameterID != "(NULL)" && this.ExistsParameter(one.ParameterID)
                         && m_viewModel.RelatedParameterSelected(one.ParameterID)
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


        private string[] SelectedIds(FlightAnalysisViewModel viewModel)
        {
            var result = from one in viewModel.RelatedParameterCollection
                         where one.IsChecked
                         select one.Parameter.ParameterID;

            return result.ToArray();
        }

        private void tabHost_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {

        }
    }

    class PanelChangedWrap
    {
        public FlightDataEntitiesRT.Charts.ChartPanel SelectedPanel
        {
            get;
            set;
        }
    }
}
