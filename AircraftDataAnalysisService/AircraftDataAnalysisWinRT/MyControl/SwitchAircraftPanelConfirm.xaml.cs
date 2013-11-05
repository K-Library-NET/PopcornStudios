using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AircraftDataAnalysisWinRT.AircraftService;
using AircraftDataAnalysisWinRT.DataModel;
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
    public sealed partial class SwitchAircraftPanelConfirm : Page
    {
        public SwitchAircraftPanelConfirm()
        {
            this.InitializeComponent();
        }

        private Grid m_parentGrid;
        private ChartPanelViewModel m_model;

        public void Show(Grid parent, ChartPanelViewModel model)
        {
            m_parentGrid = parent;
            m_model = model;

            this.DataContext = m_model;
            parent.Children.Insert(0, this);
        }

        public void Close()
        {
            this.Visibility = Visibility.Collapsed;
            m_parentGrid.Children.Remove(this);
        }

        private void btImport_Click(object sender, RoutedEventArgs e)
        {//设定面板
            this.m_model.CurrentPanel = this.m_model.ChartPanelCollections[this.m_model.CurrentIndex].Panel;

            this.Close();
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
