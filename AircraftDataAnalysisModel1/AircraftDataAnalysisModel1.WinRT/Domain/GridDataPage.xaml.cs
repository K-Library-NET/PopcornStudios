using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace AircraftDataAnalysisWinRT.Domain
{
    /// <summary>
    /// 基本页，提供大多数应用程序通用的特性。
    /// </summary>
    public sealed partial class GridDataPage : AircraftDataAnalysisWinRT.Common.LayoutAwarePage
    {
        public GridDataPage()
        {
            this.InitializeComponent();

            this.Loaded += GridDataPage_Loaded;
        }

        private bool m_loaded = false;
        private object m_syncRoot = new object();

        void GridDataPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (m_loaded)
                return;

            this.LoadParameterAndCreateColumnsAsync();
        }

        private void LoadParameterAndCreateColumnsAsync()
        {
            Task.Run(() =>
            {
                lock (m_syncRoot)
                {
                    var flightParameters = ApplicationContext.Instance.GetFlightParameters(
                        ApplicationContext.Instance.CurrentAircraftModel);

                    this.FlightParameters = flightParameters;

                    this.ColumnWrappers = new ObservableCollection<ColumnWrapper>();

                    if (this.FlightParameters == null || this.FlightParameters.Parameters == null
                        || this.FlightParameters.Parameters.Count() == 0)
                    {
                        this.grdData.Columns.Clear();
                        return;
                    }

                    foreach (var pm in this.FlightParameters.Parameters)
                    {
                        ColumnWrapper wrap = new ColumnWrapper(pm);
                        this.grdData.Columns.Add(wrap.GridColumn);
                        this.ColumnWrappers.Add(wrap);
                    }
                }
            }
            );
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

        public FlightDataEntitiesRT.FlightParameters FlightParameters { get; set; }

        internal ObservableCollection<ColumnWrapper> ColumnWrappers
        {
            get;
            set;
        }
    }

    class ColumnWrapper : AircraftDataAnalysisWinRT.Common.BindableBase
    {
        private FlightDataEntitiesRT.FlightParameter m_parameter;
        private bool m_IsParameterHidden;

        public ColumnWrapper(FlightDataEntitiesRT.FlightParameter pm)
        {
            // TODO: Complete member initialization
            this.m_parameter = pm;

            this.GridColumn = new Syncfusion.UI.Xaml.Grid.GridTextColumn()
            {
                MappingName = pm.ParameterID,
                ColumnSizer = Syncfusion.UI.Xaml.Grid.GridLengthUnitType.Auto,
                HeaderText = ApplicationContext.Instance.GetFlightParameterCaption(pm.ParameterID),
                TextAlignment = TextAlignment.Center,
                HorizontalHeaderContentAlignment = HorizontalAlignment.Center,
                IsHidden = this.IsParameterHidden,
            };
        }

        public Syncfusion.UI.Xaml.Grid.GridColumn GridColumn { get; set; }

        public bool IsParameterHidden
        {
            get
            {
                return this.m_IsParameterHidden;
            }
            set
            {
                this.SetProperty<bool>(ref this.m_IsParameterHidden, value);
                if (this.GridColumn != null)
                    this.GridColumn.IsHidden = this.IsParameterHidden;
            }
        }
    }
}
