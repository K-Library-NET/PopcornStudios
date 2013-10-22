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
    public sealed partial class ImportAircraftConfirm : UserControl, IConfirmDialog<AddFileViewModel>
    {
        public ImportAircraftConfirm()
        {
            this.InitializeComponent();
        }

        private Grid m_ParentGrid;
        private AddFileViewModel m_Model;

        public void Show(Grid parent, AddFileViewModel dataContext)
        {
            m_ParentGrid = parent;
            m_Model = dataContext;
            parent.Children.Add(this);
        }

        public void Close()
        {
            this.Visibility = Visibility.Collapsed;
            m_ParentGrid.Children.Remove(this);
        }

        private void btImport_Click(object sender, RoutedEventArgs e)
        {
            m_Model.GetRawDataModel();
            this.Close();
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
