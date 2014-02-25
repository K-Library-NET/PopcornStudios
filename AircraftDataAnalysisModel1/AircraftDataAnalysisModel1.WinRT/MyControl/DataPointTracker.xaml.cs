using Infragistics.Controls.Charts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace AircraftDataAnalysisModel1.WinRT.MyControl
{
    public sealed partial class DataPointTracker : UserControl
    {
        public DataPointTracker()
        {

            this.Loaded += DataPointTracker_Loaded;
        }

        void DataPointTracker_Loaded(object sender, RoutedEventArgs e)
        {
            //this.DataChart.Series["line1"].DataContext = new SimpleDataCollection3(); 
            //this.DataChart.Series["line2"].DataContext = new SimpleDataCollection4();
        }

        private //static 
            IEnumerable<DependencyObject> VisualDescendants(DependencyObject obj)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);
                yield return child;
                foreach (var subChild in VisualDescendants(child))
                {
                    yield return subChild;
                }
            }
        }

        public //static 
            void SelectClosest(Series series, Point point)
        {
            double minDist = double.PositiveInfinity;
            TrackingGrid closest = null;
            FrameworkElement closestContent = null;
            FrameworkElement beforeVisible = null;

            foreach (var grid in TrackingGrid.Items()
                .Where((i) => i.Series == series))
            {
                double left = GetLeft(series, grid.Item as FrameworkElement);
                double dist = System.Math.Abs(point.X - left);
                var content = grid.VisibilityItem;

                if (content != null &&
                    content.Visibility == Visibility.Visible)
                {
                    beforeVisible = content;
                    content.Visibility
                        = Visibility.Collapsed;
                }
                if (dist < minDist)
                {
                    minDist = dist;
                    closest = grid.Item;
                    closestContent = content;
                }
            }

            if (closest != null)
            {
                if (closestContent != null)
                {
                    closestContent.Visibility
                        = Visibility.Visible;
                    System.Diagnostics.Debug.WriteLine("closestContent: " + closestContent.GetHashCode());
                }
            }
            else
            {
                if (beforeVisible != null)
                {
                    beforeVisible.Visibility
                        = Visibility.Visible;
                }
            }
        }

        private //static 
            Marker FindMarker(FrameworkElement item)
        {
            while (item != null)
            {
                if (item is Marker)
                {
                    return item as Marker;
                }
                item = VisualTreeHelper.GetParent(item)
                    as FrameworkElement;
            }
            return null;
        }
        private //static 
            double GetLeft(Series series, FrameworkElement item)
        {
            Marker m = FindMarker(item);

            if (m == null)
            {
                return double.NaN;
            }
            if (m.Visibility == Visibility.Collapsed)
            {
                return double.NaN;
            }
            TranslateTransform t = m.RenderTransform as TranslateTransform;
            if (t == null)
            {
                return double.NaN;
            }
            return t.X + m.ActualWidth / 2.0;
        }

        void xamDataChart1_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            XamDataChart chart = sender as XamDataChart;
            if (chart == null)
            {
                return;
            }
            System.Diagnostics.Debug.WriteLine(this.DataChart.GetHashCode().ToString() + " PointerMoved: " + e.GetCurrentPoint(null).Position.ToString());

            chart = this.DataChart;
            foreach (var series in chart.Series)
            {
                var seriesPos = e.GetCurrentPoint(series).Position;
                System.Diagnostics.Debug.WriteLine("TrySelectClosest: " + seriesPos.ToString());
                if (seriesPos.X >= 0 &&
                    seriesPos.X < series.ActualWidth &&
                    (sender != this.DataChart || (seriesPos.Y >= 0 && seriesPos.Y < series.ActualHeight)))
                {
                    SelectClosest(
                    series, seriesPos);
                }
            }

            if (sender == this.DataChart)
                CategoryChart_PointerMoved_1(sender, e);

            if (this.TrackerParent != null && sender == this.DataChart)
            {
                this.TrackerParent.NotifyOtherTracker(sender, e);
            }
        }

        public void OnOtherTrackerNotify(object sender, PointerRoutedEventArgs e)
        {
            if (sender == this.DataChart)
                return;

            this.xamDataChart1_PointerMoved(sender, e);
        }

        private void CategoryChart_PointerMoved_1(object sender, PointerRoutedEventArgs e)
        {
            var series = this.DataChart.Series.FirstOrDefault();
            if (series == null) return;

            var position = e.GetCurrentPoint(series).Position;

            // calculate crosshair coordinates on CategoryDateTimeXAxis 
            if (((XamDataChart)series.SeriesViewer).Axes.OfType<CategoryXAxis>().Any())
            {
                var xAxis = ((XamDataChart)series.SeriesViewer).Axes.OfType<CategoryXAxis>().First();
                var yAxis = ((XamDataChart)series.SeriesViewer).Axes.OfType<NumericYAxis>().First();

                var viewport = new Rect(0, 0, xAxis.ActualWidth, yAxis.ActualHeight);
                var window = series.SeriesViewer.WindowRect;

                bool isInverted = xAxis.IsInverted;
                ScalerParams param = new ScalerParams(window, viewport, isInverted);
                var unscaledX = xAxis.GetUnscaledValue(position.X, param);

                isInverted = yAxis.IsInverted;
                param = new ScalerParams(window, viewport, isInverted);
                var unscaledY = yAxis.GetUnscaledValue(position.Y, param);

                DateTime xDate = new DateTime((long)unscaledX);

                //var x = unscaledX.ToString();//String.Format("{0:T}", xDate);
                //var y = unscaledY.ToString();// String.Format("{0:0.00}", unscaledY);
                this.TrackerParent.SetCoordinate(unscaledX, unscaledY, this.DataChart.DataContext as IEnumerable<SimpleDataPoint>);
            }
        }


        public ITrackerParent TrackerParent { get; set; }

        public Brush LineBrush
        {
            get
            {

                var serie1 = this.DataChart.Series[0];
                if (serie1 != null)
                    return serie1.Brush;

                return null;
            }
            set
            {
                var serie1 = this.DataChart.Series[0];
                if (serie1 != null)
                    serie1.Brush = value;
            }
        }
    }

    public class TrackingGrid : Windows.UI.Xaml.Controls.Grid
    {
        public static readonly DependencyProperty SeriesProperty =
           DependencyProperty.Register(
           "Series",
           typeof(Series),
           typeof(TrackingGrid),
           new PropertyMetadata(null,
               (o, e) =>
               {
                   (o as TrackingGrid)
                       .OnSeriesChanged(e);
               }));

        private void OnSeriesChanged(
            DependencyPropertyChangedEventArgs e)
        {
            Refresh();
        }

        public Series Series
        {
            get { return (Series)GetValue(SeriesProperty); }
            set { SetValue(SeriesProperty, value); }
        }

        public static readonly DependencyProperty VisibilityItemProperty =
           DependencyProperty.Register(
           "VisibilityItem",
           typeof(VisibilityItem),
           typeof(TrackingGrid),
           new PropertyMetadata(null,
               (o, e) =>
               {
                   (o as TrackingGrid)
                       .OnVisibilityItemChanged(e);
               }
               ));

        private void OnVisibilityItemChanged(
            DependencyPropertyChangedEventArgs e)
        {
            Refresh();
        }

        public VisibilityItem VisibilityItem
        {
            get { return (VisibilityItem)GetValue(VisibilityItemProperty); }
            set { SetValue(VisibilityItemProperty, value); }
        }

        public TrackingGrid()
        {
            this.Loaded += TrackingGrid_Loaded;
            this.Unloaded += TrackingGrid_Unloaded;
        }

        private void Refresh()
        {
            if (_items.ContainsKey(this))
            {
                _items.Remove(this);
            }
            _items.Add(this, new ItemInfo
            {
                Series = Series,
                Item = this,
                VisibilityItem = VisibilityItem
            });
        }

        void TrackingGrid_Unloaded(object sender, RoutedEventArgs e)
        {
            _items.Remove(this);
        }

        void TrackingGrid_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        public class ItemInfo
        {
            public Series Series { get; set; }
            public TrackingGrid Item { get; set; }
            public VisibilityItem VisibilityItem { get; set; }
        }

        private static Dictionary<TrackingGrid, ItemInfo> _items
            = new Dictionary<TrackingGrid, ItemInfo>();

        public static IEnumerable<ItemInfo> Items()
        {
            return _items.Values;
        }
    }

    public class VisibilityItem : ContentControl
    {
    }


    /// <summary>
    /// Simple storage class for pair of string and double value
    /// </summary>
    public class SimpleDataPoint
    {
        public double Value { get; set; }
        public int Label { get; set; }
    }

    public interface ITrackerParent
    {
        void NotifyOtherTracker(object sender, PointerRoutedEventArgs e);

        void SetCoordinate(double unscaledX, double unscaledY, IEnumerable<SimpleDataPoint> source);
    }
}
