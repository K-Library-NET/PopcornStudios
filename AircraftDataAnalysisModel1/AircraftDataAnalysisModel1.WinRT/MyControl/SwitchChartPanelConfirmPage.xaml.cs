using AircraftDataAnalysisModel1.WinRT.Domain;
using AircraftDataAnalysisWinRT.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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

namespace AircraftDataAnalysisWinRT.MyControl
{
    /// <summary>
    /// 基本页，提供大多数应用程序通用的特性。
    /// </summary>
    public sealed partial class SwitchChartPanelConfirmPage : AircraftDataAnalysisWinRT.Common.LayoutAwarePage, ISubEditChartNavigationParameterValidator
    {
        public SwitchChartPanelConfirmPage()
        {
            this.InitializeComponent();
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
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// 保留与此页关联的状态，以防挂起应用程序或
        /// 从导航缓存中放弃此页。值必须符合
        /// <see cref="SuspensionManager.SessionState"/> 的序列化要求。
        /// </summary>
        /// <param name="pageState">要使用可序列化状态填充的空字典。</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter == null || !(e.Parameter is SubEditChartNavigationParameter))
            {
                return;//debug 暂时必须确定最起码有一个参数过来
            }

            this.InitComboBoxBindingSources();

            SubEditChartNavigationParameter parameter = e.Parameter as SubEditChartNavigationParameter;
            if (string.IsNullOrEmpty(parameter.HostParameterID) || parameter.HostParameterYAxis == FlightAnalysisSubViewYAxis.RightYAxis
                || !string.IsNullOrEmpty(this.ValidateParameter(parameter)))
                return; //不合法条件

            var rootViewModel = GetRootViewModel();
            rootViewModel.Validator = this;

            rootViewModel.Serie1ParameterID = parameter.HostParameterID;
            //rootViewModel.Serie1YAxis = parameter.HostParameterYAxis;//这个不需要设置，一般保持默认
            if (parameter.RelatedParameterIDs != null && parameter.RelatedParameterIDs.Length > 0)
            {
                if (parameter.RelatedParameterIDs.Length >= 1)
                {
                    var info = parameter.RelatedParameterIDs[0];
                    rootViewModel.Serie2ParameterID = info.RelatedParameterID;
                    rootViewModel.Serie2YAxis = info.YAxis;
                }
                if (parameter.RelatedParameterIDs.Length >= 2)
                {
                    var info = parameter.RelatedParameterIDs[1];
                    rootViewModel.Serie3ParameterID = info.RelatedParameterID;
                    rootViewModel.Serie3YAxis = info.YAxis;
                }
                if (parameter.RelatedParameterIDs.Length >= 3)
                {
                    var info = parameter.RelatedParameterIDs[2];
                    rootViewModel.Serie4ParameterID = info.RelatedParameterID;
                    rootViewModel.Serie4YAxis = info.YAxis;
                }
                if (parameter.RelatedParameterIDs.Length >= 4)
                {
                    var info = parameter.RelatedParameterIDs[3];
                    rootViewModel.Serie5ParameterID = info.RelatedParameterID;
                    rootViewModel.Serie5YAxis = info.YAxis;
                }
                if (parameter.RelatedParameterIDs.Length >= 5)
                {
                    var info = parameter.RelatedParameterIDs[4];
                    rootViewModel.Serie6ParameterID = info.RelatedParameterID;
                    rootViewModel.Serie6YAxis = info.YAxis;
                }
                if (parameter.RelatedParameterIDs.Length >= 6)
                {
                    var info = parameter.RelatedParameterIDs[5];
                    rootViewModel.Serie7ParameterID = info.RelatedParameterID;
                    rootViewModel.Serie7YAxis = info.YAxis;
                }
            }

            //if (e.Parameter != null && e.Parameter is ChartPanelViewModel)
            //{
            //    this.m_model = e.Parameter as ChartPanelViewModel;

            //    this.DataContext = m_model;
            //}
        }

        private FlightAnalysisSubSwitchChartViewModel GetRootViewModel()
        {
            object src = null;
            if (this.Resources.TryGetValue("datacontext", out src))
            {
                if (src != null && (src is FlightAnalysisSubSwitchChartViewModel))
                    return src as FlightAnalysisSubSwitchChartViewModel;
            }

            return null;
        }

        private void InitComboBoxBindingSources()
        {
            ParameterComboBoxBindingSource source1 = this.GetComboBindingSource1();
            if (source1 != null)
            {
                source1.Init();
            }
        }

        private ParameterComboBoxBindingSource GetComboBindingSource1()
        {
            object src = null;
            if (this.Resources.TryGetValue("parameterBindingSource", out src))
            {
                if (src != null && (src is ParameterComboBoxBindingSource))
                    return src as ParameterComboBoxBindingSource;
            }

            return null;
        }

        private void btImport_Click(object sender, RoutedEventArgs e)
        {
            //if (m_model != null)
            //{
            //    //this.m_model.CurrentPanel = this.m_model.ChartPanelCollections[this.m_model.CurrentIndex].Panel;
            //    //this.Frame.Navigate(typeof(PStudio.WinApp.Aircraft.FDAPlatform.Domain.FlightAnalysis), this.m_model.CurrentPanel);
            //    return;
            //}
            var rootViewModel = this.GetRootViewModel();
            if (rootViewModel != null)
            {
                bool result = this.InvokeValidation();

                //SubEditChartNavigationParameter parameter;
                //string errorMessage = InvokeValidation(rootViewModel, out parameter);

                //rootViewModel.ErrorMessage = errorMessage;

                //if (!string.IsNullOrEmpty(errorMessage))
                //{

                //    return;
                //}
                if (result)
                {
                    var parameter = this.GenerateParameter(rootViewModel);
                    this.Frame.Navigate(typeof(AircraftDataAnalysisWinRT.Domain.FlightAnalysisSub), parameter);
                    return;
                }
            }

            this.Frame.GoBack();
        }

        public bool InvokeValidation()
        {
            var rootViewModel = this.GetRootViewModel();
            SubEditChartNavigationParameter parameter;
            string errorMessage = InvokeValidation(rootViewModel, out parameter);

            rootViewModel.ErrorMessage = errorMessage;

            if (!string.IsNullOrEmpty(errorMessage))
            {
                return false;
            }
            return true;
        }

        private string InvokeValidation(FlightAnalysisSubSwitchChartViewModel rootViewModel, out SubEditChartNavigationParameter parameter)
        {
            parameter = this.GenerateParameter(rootViewModel);

            string errorMessage = this.ValidateParameter(parameter);
            return errorMessage;
        }

        public string ValidateParameter(SubEditChartNavigationParameter parameter)
        {
            if (parameter == null)
            {
                return "导航参数不合法。";
            }

            var source1 = this.GetComboBindingSource1();
            if (string.IsNullOrEmpty(parameter.HostParameterID)
                || !source1.Any(
                new Func<ParameterComboBoxBindingClass, bool>(delegate(ParameterComboBoxBindingClass c)
                    {
                        if (c != null && c.ParameterID == parameter.HostParameterID)
                            return true;
                        return false;
                    })))
            {
                return "导航参数的参数ID不合法。";
            }

            Dictionary<string, string> tempval = new Dictionary<string, string>();

            foreach (var info in parameter.RelatedParameterIDs)
            {
                if (info != null && !string.IsNullOrEmpty(info.RelatedParameterID)
                    && source1.Any(
                        new Func<ParameterComboBoxBindingClass, bool>(delegate(ParameterComboBoxBindingClass c)
                        {
                            if (c != null && c.ParameterID == info.RelatedParameterID)
                                return true;
                            return false;
                        })))
                {
                    return "导航参数中的相关参数不合法：" + info.RelatedParameterID;
                }

                if (!tempval.ContainsKey(info.RelatedParameterID))
                {
                    tempval.Add(info.RelatedParameterID, info.RelatedParameterID);
                    continue;
                }
                else
                {
                    return "导航参数中的相关参数重复：" + info.RelatedParameterID;
                }
            }

            return string.Empty;
        }

        private SubEditChartNavigationParameter GenerateParameter(FlightAnalysisSubSwitchChartViewModel rootViewModel)
        {
            SubEditChartNavigationParameter parameter = new SubEditChartNavigationParameter();
            if (rootViewModel == null || rootViewModel.IsSerie1Editable
                || rootViewModel.Serie1YAxis == FlightAnalysisSubViewYAxis.RightYAxis) //serie1 的条件一般是写死的
                return null;

            parameter.HostParameterID = rootViewModel.Serie1ParameterID;
            parameter.HostParameterYAxis = rootViewModel.Serie1YAxis;

            List<SubEditChartNavigationParameter.RelatedParameterInfo> relatedInfo = new List<SubEditChartNavigationParameter.RelatedParameterInfo>();
            if (rootViewModel.IsSerie2Editable == false)//已经增加Serie2
            {
                relatedInfo.Add(new SubEditChartNavigationParameter.RelatedParameterInfo()
                {
                    RelatedParameterID = rootViewModel.Serie2ParameterID,
                    YAxis = rootViewModel.Serie2YAxis
                });
            }
            if (rootViewModel.IsSerie3Editable == false)//已经增加Serie3
            {
                relatedInfo.Add(new SubEditChartNavigationParameter.RelatedParameterInfo()
                {
                    RelatedParameterID = rootViewModel.Serie3ParameterID,
                    YAxis = rootViewModel.Serie3YAxis
                });
            }
            if (rootViewModel.IsSerie4Editable == false)//已经增加Serie4
            {
                relatedInfo.Add(new SubEditChartNavigationParameter.RelatedParameterInfo()
                {
                    RelatedParameterID = rootViewModel.Serie4ParameterID,
                    YAxis = rootViewModel.Serie4YAxis
                });
            }
            if (rootViewModel.IsSerie5Editable == false)//已经增加Serie5
            {
                relatedInfo.Add(new SubEditChartNavigationParameter.RelatedParameterInfo()
                {
                    RelatedParameterID = rootViewModel.Serie5ParameterID,
                    YAxis = rootViewModel.Serie5YAxis
                });
            }
            if (rootViewModel.IsSerie6Editable == false)//已经增加Serie6
            {
                relatedInfo.Add(new SubEditChartNavigationParameter.RelatedParameterInfo()
                {
                    RelatedParameterID = rootViewModel.Serie6ParameterID,
                    YAxis = rootViewModel.Serie6YAxis
                });
            }
            if (rootViewModel.IsSerie7Editable == false)//已经增加Serie7
            {
                relatedInfo.Add(new SubEditChartNavigationParameter.RelatedParameterInfo()
                {
                    RelatedParameterID = rootViewModel.Serie7ParameterID,
                    YAxis = rootViewModel.Serie7YAxis
                });
            }

            parameter.RelatedParameterIDs = relatedInfo.ToArray();

            return parameter;
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private void btSerie2Editable_Click(object sender, RoutedEventArgs e)
        {
            FlightAnalysisSubSwitchChartViewModel rootViewModel = this.GetRootViewModel();
            rootViewModel.IsSerie2Editable = true;
        }

        private void btSerie3Editable_Click(object sender, RoutedEventArgs e)
        {
            FlightAnalysisSubSwitchChartViewModel rootViewModel = this.GetRootViewModel();
            rootViewModel.IsSerie3Editable = true;
        }

        private void btSerie4Editable_Click(object sender, RoutedEventArgs e)
        {
            FlightAnalysisSubSwitchChartViewModel rootViewModel = this.GetRootViewModel();
            rootViewModel.IsSerie4Editable = true;
        }

        private void btSerie5Editable_Click(object sender, RoutedEventArgs e)
        {
            FlightAnalysisSubSwitchChartViewModel rootViewModel = this.GetRootViewModel();
            rootViewModel.IsSerie5Editable = true;
        }

        private void btSerie6Editable_Click(object sender, RoutedEventArgs e)
        {
            FlightAnalysisSubSwitchChartViewModel rootViewModel = this.GetRootViewModel();
            rootViewModel.IsSerie6Editable = true;
        }

        private void btSerie7Editable_Click(object sender, RoutedEventArgs e)
        {
            FlightAnalysisSubSwitchChartViewModel rootViewModel = this.GetRootViewModel();
            rootViewModel.IsSerie7Editable = true;
        }

        private void btSerie2UnEditable_Click(object sender, RoutedEventArgs e)
        {
            FlightAnalysisSubSwitchChartViewModel rootViewModel = this.GetRootViewModel();
            rootViewModel.IsSerie2Editable = false;
        }

        private void btSerie3UnEditable_Click(object sender, RoutedEventArgs e)
        {
            FlightAnalysisSubSwitchChartViewModel rootViewModel = this.GetRootViewModel();
            rootViewModel.IsSerie3Editable = false;
        }

        private void btSerie4UnEditable_Click(object sender, RoutedEventArgs e)
        {
            FlightAnalysisSubSwitchChartViewModel rootViewModel = this.GetRootViewModel();
            rootViewModel.IsSerie4Editable = false;
        }

        private void btSerie5UnEditable_Click(object sender, RoutedEventArgs e)
        {
            FlightAnalysisSubSwitchChartViewModel rootViewModel = this.GetRootViewModel();
            rootViewModel.IsSerie5Editable = false;
        }

        private void btSerie6UnEditable_Click(object sender, RoutedEventArgs e)
        {
            FlightAnalysisSubSwitchChartViewModel rootViewModel = this.GetRootViewModel();
            rootViewModel.IsSerie6Editable = false;
        }

        private void btSerie7UnEditable_Click(object sender, RoutedEventArgs e)
        {
            FlightAnalysisSubSwitchChartViewModel rootViewModel = this.GetRootViewModel();
            rootViewModel.IsSerie7Editable = false;
        }


        //private ChartPanelViewModel m_model;

        //public ChartPanelViewModel Model
        //{
        //    get { return m_model; }
        //    set { m_model = value; }
        //}
    }

    internal class ParameterComboBoxBindingClass : AircraftDataAnalysisWinRT.Common.BindableBase
    {
        private FlightDataEntitiesRT.FlightParameter m_parameter;

        public ParameterComboBoxBindingClass(FlightDataEntitiesRT.FlightParameter par)
        {
            // TODO: Complete member initialization
            this.m_parameter = par;
        }

        public string ParameterDisplay
        {
            get
            {
                return string.Format("{0} ({1}) ", m_parameter.Caption, m_parameter.ParameterID);
            }
        }

        public string ParameterID
        {
            get
            {
                return m_parameter.ParameterID;
            }
        }
    }

    internal class ParameterComboBoxBindingSource : ObservableCollection<ParameterComboBoxBindingClass>
    {
        public ParameterComboBoxBindingSource()
            : base()
        {
        }

        public void Init()
        {
            var flightParameters = ApplicationContext.Instance.GetFlightParameters(ApplicationContext.Instance.CurrentAircraftModel);

            if (flightParameters != null && flightParameters.Parameters != null
                && flightParameters.Parameters.Length > 0)
            {
                foreach (var par in flightParameters.Parameters)
                {
                    ParameterComboBoxBindingClass binding = new ParameterComboBoxBindingClass(par);

                    this.Add(binding);
                }
            }
        }
    }

    internal class YAxisComboBoxBindingClass : AircraftDataAnalysisWinRT.Common.BindableBase
    {
        public YAxisComboBoxBindingClass()
        {
        }

        private FlightAnalysisSubViewYAxis m_yaxis = FlightAnalysisSubViewYAxis.LeftYAxis;

        public FlightAnalysisSubViewYAxis YAxis
        {
            get
            {
                return m_yaxis;
            }
            set
            {
                this.SetProperty<FlightAnalysisSubViewYAxis>(ref m_yaxis, value);
                this.OnPropertyChanged("BindingYAxisDisplay");
            }
        }

        public string BindingYAxisDisplay
        {
            get
            {
                if (m_yaxis == FlightAnalysisSubViewYAxis.RightYAxis)
                    return "右边Y轴";

                return "左边Y轴";
            }
        }
    }

    internal class YAxisComboBoxBindingSource : ObservableCollection<YAxisComboBoxBindingClass>
    {
        public YAxisComboBoxBindingSource()
        {
            this.Init();
        }

        private void Init()
        {
            this.Add(new YAxisComboBoxBindingClass());
            this.Add(new YAxisComboBoxBindingClass() { YAxis = FlightAnalysisSubViewYAxis.RightYAxis });
        }
    }

    public class SubEditChartNavigationParameter : IDataLoaderNavigator
    {
        public string HostParameterID
        {
            get;
            set;
        }

        public FlightAnalysisSubViewYAxis HostParameterYAxis
        {
            get;
            set;
        }

        public class RelatedParameterInfo
        {
            public string RelatedParameterID
            {
                get;
                set;
            }

            public FlightAnalysisSubViewYAxis YAxis
            {
                get;
                set;
            }
        }

        public RelatedParameterInfo[] RelatedParameterIDs
        {
            get;
            set;
        }

        public AircraftDataAnalysisModel1.WinRT.Domain.AircraftAnalysisDataLoader DataLoader { get; set; }
    }

    public class ExtremumReportSubEditChartNavigationParameter : SubEditChartNavigationParameter
    {
        public int MinValueSecond { get; set; }

        public int MaxValueSecond { get; set; }
    }
}
