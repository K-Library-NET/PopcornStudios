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

namespace AircraftDataAnalysisWinRT.Test
{
    /// <summary>
    /// 基本页，提供大多数应用程序通用的特性。
    /// </summary>
    public sealed partial class TestReadFile : AircraftDataAnalysisWinRT.Common.LayoutAwarePage
    {
        public TestReadFile()
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var openPicker = new Windows.Storage.Pickers.FileOpenPicker();
            openPicker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            openPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.ComputerFolder;
            // Users expect to have a filtered view of their folders depending on the scenario.
            // For example, when choosing a documents folder, restrict the filetypes to documents for your application.
            openPicker.FileTypeFilter.Add(".phy");// ([".png", ".jpg", ".jpeg"]);
            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                AddFileViewModel model = new AddFileViewModel(file);
                this.ViewModel = model;
                this.DataContext = model;
                this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                    new Windows.UI.Core.DispatchedHandler(
                    delegate()
                    {
                        model.InitLoadHeader();
                    }));
            }
            else
            {
                this.ViewModel = null;
                this.DataContext = null;
            }
        }

        public AddFileViewModel ViewModel { get; set; }

        private void readToDb_Click(object sender, RoutedEventArgs e)
        {
            if (this.ViewModel == null)
                return;

            this.ViewModel.Progress += new AsyncActionProgressHandler<int>(this.OnProgressChanged);
            this.ViewModel.Completed = new AsyncActionWithProgressCompletedHandler<int>(this.OnCompleted);

            this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                new Windows.UI.Core.DispatchedHandler(
                    delegate()
                    {
                        this.gridData.AutoGenerateColumns = false;
                        this.gridData.Columns.Clear();
                        var rawDataModel = this.ViewModel.GetRawDataModel();
                        foreach (var col in rawDataModel.ColumnCollection)
                        {
                            this.gridData.Columns.Add(col);
                        }
                        this.gridData.ItemsSource = rawDataModel.RawDataRowViewModel;
                    }));
        }

        private void OnProgressChanged(IAsyncActionWithProgress<int> asyncInfo, int progress)
        {
            if (this.ViewModel.Status == AsyncStatus.Started)
                this.progressBar1.Visibility = Windows.UI.Xaml.Visibility.Visible;
            else
                this.progressBar1.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            if (progress > 30)
            {//可以读取首页
                this.DoDisplayDataAsync();
            }
        }

        private void DoDisplayDataAsync()
        {
            //this.ViewModel.ReadRawData();
        }

        private void OnCompleted(IAsyncActionWithProgress<int> progress, AsyncStatus status)
        {
            this.progressBar1.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            this.ViewModel.Progress -= new AsyncActionProgressHandler<int>(this.OnProgressChanged);
            this.ViewModel.Completed -= new AsyncActionWithProgressCompletedHandler<int>(this.OnCompleted);
        }
    }
}
