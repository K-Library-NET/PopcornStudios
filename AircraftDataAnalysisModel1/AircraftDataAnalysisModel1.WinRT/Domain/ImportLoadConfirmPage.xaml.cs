using AircraftDataAnalysisWinRT.DataModel;
using AircraftDataAnalysisWinRT.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace AircraftDataAnalysisWinRT.Domain
{
    /// <summary>
    /// 基本页，提供大多数应用程序通用的特性。
    /// </summary>
    public sealed partial class ImportLoadConfirmPage : AircraftDataAnalysisWinRT.Common.LayoutAwarePage
    {
        public ImportLoadConfirmPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e == null || e.Parameter == null
                || !(e.Parameter is AddFileViewModel))
            {
                this.DataContext = null;
                return;
            }

            this.AddFileViewModel = e.Parameter as AddFileViewModel;

            this.DataContext = this.AddFileViewModel;

            this.SetLoading(true);

            this.DoDisplayHeaderDataAsync();
        }

        public AddFileViewModel AddFileViewModel
        {
            get;
            set;
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

        private void SetLoading(bool loading)
        {
            if (loading)
            {
                //this.grdPanel1.IsTapEnabled = false;
                //this.grdPanel2.IsTapEnabled = false;
                this.ProgressBar1.Visibility = Windows.UI.Xaml.Visibility.Visible;
                this.ProgressBar1.IsIndeterminate = true;
            }
            else
            {
                //this.grdPanel1.IsTapEnabled = true;
                //this.grdPanel2.IsTapEnabled = true;
                this.ProgressBar1.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                this.ProgressBar1.IsIndeterminate = false;
            }
        }

        /// <summary>
        /// 临时的方法，多个机型后要考虑使用别的方式创建，例如反射
        /// </summary>
        /// <param name="file"></param>
        /// <param name="aircraftModel"></param>
        /// <param name="flightParameter"></param>
        /// <param name="extractor"></param>
        /// <param name="currentFlight"></param>
        private void CreateTempCurrentFlight(StorageFile file, FlightDataEntitiesRT.AircraftModel aircraftModel,
            FlightDataEntitiesRT.FlightParameters flightParameter,
            ref FlightDataEntitiesRT.IFlightRawDataExtractor extractor,
            ref FlightDataEntitiesRT.Flight currentFlight)
        {
            if (aircraftModel != null && !string.IsNullOrEmpty(aircraftModel.ModelName))
            {
                if (aircraftModel.ModelName == "F4D")
                {
                    var result = FlightDataReading.AircraftModel1.FlightRawDataExtractorFactory
                        .CreateFlightRawDataExtractor(file, flightParameter);
                    extractor = result;
                }
            }
            currentFlight = new FlightDataEntitiesRT.Flight()
            {
                Aircraft = new FlightDataEntitiesRT.AircraftInstance()
                {
                    AircraftModel = aircraftModel,
                    AircraftNumber = "1234", //debug
                    LastUsed = DateTime.Now
                },
                StartSecond = 0,
                FlightName = file.Name,
                FlightID = this.RemoveIllegalChars(file.DisplayName)
            };
        }

        private string RemoveIllegalChars(string p)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char c in p)
            {
                if (char.IsNumber(c))
                    builder.Append(c);
            }
            return builder.ToString();
        }

        private Task m_runningTask = null;

        private void btImport_Click(object sender, RoutedEventArgs e)
        {
            if (this.AddFileViewModel == null)
                return;

            this.AddFileViewModel.Progress += new AsyncActionProgressHandler<int>(this.OnProgressChanged);
            this.AddFileViewModel.Completed += new AsyncActionWithProgressCompletedHandler<int>(this.OnCompleted);

            this.ProgressBar1.IsIndeterminate = false;
            this.ProgressBar1.Minimum = 0;
            this.ProgressBar1.Maximum = 100;
            this.ProgressBar1.Value = 0;
            this.ProgressBar1.Visibility = Windows.UI.Xaml.Visibility.Visible;

            this.m_runningTask = System.Threading.Tasks.Task.Run(new Action(delegate()
             {
                 DoImportCore();
             }));
        }

        private void DoImportCore()
        {
            try
            {
                var parameters = ApplicationContext.Instance.GetFlightParameters(
                    ApplicationContext.Instance.CurrentAircraftModel);

                var extractor = FlightDataReading.AircraftModel1.FlightRawDataExtractorFactory
                    .CreateFlightRawDataExtractor(this.AddFileViewModel.File, parameters);

                IDataReading reading = new DataReading(extractor, this.AddFileViewModel.Flight,
                    parameters);

                reading.Progress = new AsyncActionProgressHandler<int>(
                    async delegate(IAsyncActionWithProgress<int> prog, int progressInt)
                    {
                        await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High,
                            new Windows.UI.Core.DispatchedHandler(delegate()
                            {
                                if (this.AddFileViewModel.Progress != null)
                                    this.AddFileViewModel.Progress(prog, progressInt);
                            }));
                    });

                reading.Completed = new AsyncActionWithProgressCompletedHandler<int>(
                    async delegate(IAsyncActionWithProgress<int> prog, AsyncStatus status)
                    {
                        await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High,
                            new Windows.UI.Core.DispatchedHandler(delegate()
                            {
                                if (this.AddFileViewModel.Completed != null)
                                    this.AddFileViewModel.Completed(prog, status);
                            }));
                    });

                reading.Header = this.AddFileViewModel.Header;
                reading.ReadData();

                this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                    new Windows.UI.Core.DispatchedHandler(delegate()
                {
                    this.m_runningTask = null;

                    //completed!
                    ApplicationContext.Instance.CurrentFlight = reading.Flight;
                    this.Frame.GoBack();
                    this.Frame.Navigate(typeof(PStudio.WinApp.Aircraft.FDAPlatform.Domain.FlightAnalysis));//不带参数，当前架次已经改变
                }));
            }
            catch (Exception ex)
            {
                //SERVER log?
                System.Diagnostics.Debug.WriteLine(ex.Message);
                this.AddFileViewModel.Completed(this.AddFileViewModel, AsyncStatus.Error);
            }
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.m_runningTask != null && this.m_runningTask.IsCompleted == false)
                {
                    var t = this.m_runningTask.AsAsyncAction();
                    t.Cancel();
                    this.m_runningTask = null;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            this.Frame.GoBack();
        }

        private void OnProgressChanged(IAsyncActionWithProgress<int> asyncInfo, int progress)
        {
            this.ProgressBar1.Value = progress;

            //if (this.AddFileViewModel.Status == AsyncStatus.Started)
            //{
            //    this.ProgressBar1.Value = progress;
            //}
            //else
            //{
            //    this.ProgressBar1.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            //    this.ProgressBar1.IsIndeterminate = false;
            //}
        }

        private async void DoDisplayHeaderDataAsync()
        {
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal
                , new Windows.UI.Core.DispatchedHandler(
                    delegate()
                    {
                        this.AddFileViewModel.InitLoadHeader();
                        //经纬度已经读出来了，但是开始总有一段是接近0的，很难画出
                        DrawEWNS_map();

                        //预览数据
                        int endPreviewSecond = 50;
                        endPreviewSecond = Math.Min(endPreviewSecond, this.AddFileViewModel.EndSecond / 20);
                        var previewModel = this.AddFileViewModel.GetPreviewRawDataModel(0, endPreviewSecond);
                        foreach (var col in previewModel.ColumnCollection)
                        {
                            this.grdPreviewData.Columns.Add(col);
                        }
                        this.grdPreviewData.ItemsSource = previewModel.RawDataRowViewModel;
                    }));

            this.SetLoading(false);
        }

        private void DrawEWNS_map()
        {
            Size size = this.canvasEWNS.RenderSize;
            float[] ews = (float[])this.AddFileViewModel.Header.Longitudes.Clone();
            float[] nss = (float[])this.AddFileViewModel.Header.Latitudes.Clone();

            this.canvasEWNS.CurrentFlight = this.AddFileViewModel.Flight;
            this.canvasEWNS.Render(this.canvasEWNSback.RenderSize);

            /* region old 
             * 
            this.ReAdjustSizes(size, ref ews, ref nss);
            //ews = this.ReAdjustEWs(size);
            //nss = this.ReAdjustNSs(size);
            LineSegment[] segments = this.CreateSegments(ews, nss);

            PathSegmentCollection collection = new PathSegmentCollection();
            foreach (var seg in segments)
            {
                collection.Add(seg);
            }
            var pathGeometry = new PathGeometry();
            pathGeometry.Figures.Add(new PathFigure() { Segments = collection });

            this.canvasEWNS.Children.Clear();
            Windows.UI.Xaml.Shapes.Path path = new Windows.UI.Xaml.Shapes.Path()
            {
                Stroke = new SolidColorBrush(Windows.UI.Colors.Azure),
            };

            path.Data = pathGeometry;
            this.canvasEWNS.Children.Add(path);*/
        }

        /// <summary>
        /// 修改成能够绘制的数字
        /// </summary>
        /// <param name="size"></param>
        /// <param name="ews"></param>
        /// <param name="nss"></param>
        private void ReAdjustSizes(Size size, ref float[] ews, ref float[] nss)
        {
            int startLoc = int.MaxValue;
            int endLoc = int.MinValue;

            for (int i = 0; i < ews.Length; i++)
            {
                if (ews[i] < 50 || ews[i] > 150)
                    continue;//写死，认定经度在50-150之内合理

                if (nss[i] < 0 || nss[i] > 60)
                    continue;

                startLoc = Math.Min(i, startLoc);
                endLoc = Math.Max(endLoc, i);
            }

            if (endLoc > startLoc)
            {
                ews = ews.Skip(startLoc).Take(endLoc - startLoc).ToArray();
                nss = nss.Skip(startLoc).Take(endLoc - startLoc).ToArray();
            }

            float minEws = ews.Min();
            float maxEws = ews.Max();
            float absRangeEws = maxEws - minEws;
            float gapEws = absRangeEws * 0.2F;

            minEws = minEws - gapEws;
            maxEws = maxEws + gapEws;

            absRangeEws = maxEws - minEws;

            var newEws = from one in ews
                         select Convert.ToSingle(size.Width * ((one - minEws) / absRangeEws));

            float minNss = nss.Min();
            float maxNss = nss.Max();
            float absRangeNss = maxNss - minNss;
            float gapNss = absRangeNss * 0.2F;

            minNss = minNss - gapNss;
            maxNss = maxNss + gapNss;

            absRangeNss = maxNss - minNss;

            var newNss = from one in nss
                         select Convert.ToSingle(size.Width * ((one - minNss) / absRangeNss));

            ews = newEws.ToArray();
            nss = newNss.ToArray();
        }

        private LineSegment[] CreateSegments(float[] ews, float[] nss)
        {
            List<LineSegment> segments = new List<LineSegment>();
            for (int i = 0; i < ews.Length; i++)
            {
                LineSegment seg = new LineSegment()
                {
                    Point = new Point()
                    {
                        X = ews[i],
                        Y = nss[i]
                    }
                };
                segments.Add(seg);
            }
            return segments.ToArray();
        }

        private async void OnCompleted(IAsyncActionWithProgress<int> progress, AsyncStatus status)
        {
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                new Windows.UI.Core.DispatchedHandler(() =>
                {
                    this.ProgressBar1.Value = 100;
                    this.ProgressBar1.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    this.ProgressBar1.IsIndeterminate = false;

                    this.AddFileViewModel.Progress -= new AsyncActionProgressHandler<int>(this.OnProgressChanged);
                    this.AddFileViewModel.Completed -= new AsyncActionWithProgressCompletedHandler<int>(this.OnCompleted);
                }));
        }
    }
}
