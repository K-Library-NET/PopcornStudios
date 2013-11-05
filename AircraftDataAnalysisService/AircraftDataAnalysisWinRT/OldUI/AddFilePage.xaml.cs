using AircraftDataAnalysisWinRT.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “基本页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234237 上有介绍

namespace AircraftDataAnalysisWinRT
{
    /// <summary>
    /// 基本页，提供大多数应用程序通用的特性。
    /// </summary>
    public sealed partial class AddFilePage : AircraftDataAnalysisWinRT.Common.LayoutAwarePage
    {
        public AddFilePage()
        {
            this.InitializeComponent();
        }

        public AddFileViewModel ViewModel
        {
            get;
            set;
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            //base.OnNavigatedTo(e);

            // Create the picker object and set options
            var openPicker = new Windows.Storage.Pickers.FileOpenPicker();
            openPicker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            openPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.ComputerFolder;
            // Users expect to have a filtered view of their folders depending on the scenario.
            // For example, when choosing a documents folder, restrict the filetypes to documents for your application.
            openPicker.FileTypeFilter.Add(".phy");// ([".png", ".jpg", ".jpeg"]);
            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                AddFileViewModel model = new AddFileViewModel(null, file, null, null, null);
                this.ViewModel = model;
                this.DataContext = model;
            }
            else
            {
                this.ViewModel = null;
                this.DataContext = null;
            }
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

        private void ImportModelData_Click(object sender, RoutedEventArgs e)
        {
            //if (this.ViewModel != null)
            //{
            //    this.ViewModel.ImportData();

            //    string uniqueID = this.ViewModel.UniqueId;
            //    this.Frame.Navigate(typeof(ItemDetailPage), uniqueID);
            //}
        }
    }
}
