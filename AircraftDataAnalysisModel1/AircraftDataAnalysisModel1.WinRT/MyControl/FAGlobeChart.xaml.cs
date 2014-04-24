using FlightDataEntitiesRT;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

// “用户控件”项模板在 http://go.microsoft.com/fwlink/?LinkId=234236 上提供

namespace AircraftDataAnalysisWinRT.MyControl
{
    public sealed partial class FAGlobeChart : UserControl
    {
        public FAGlobeChart()
        {
            this.InitializeComponent();

            this.SizeChanged += FAGlobeChart_SizeChanged;
        }

        void FAGlobeChart_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            //this.m_renderSize = this.RenderSize;
            this.Render(this.RenderSize);
            //throw new NotImplementedException();
        }

        private IEnumerable<GlobeData> m_itemsSource = null;

        public IEnumerable<GlobeData> ItemsSource
        {
            get
            {
                return m_itemsSource;
            }
            set
            {
                m_itemsSource = value;

                if (m_itemsSource == null)
                    return;

                BubbleDataScatterSample dataSet = this.GetDataSet();
                if (dataSet == null)
                    return;
                dataSet.Clear();
                //int val =  (int)dataSet.Settings.YMin;
                //int i = 0;
                float maxLongitude = float.MinValue;
                float maxLatitude = float.MinValue;
                float minLongitude = float.MaxValue;
                float minLatitude = float.MaxValue;

                foreach (var one in m_itemsSource)
                {
                    maxLongitude = Math.Max(one.Longitude, maxLongitude);
                    maxLatitude = Math.Max(one.Latitude, maxLatitude);
                    minLongitude = Math.Min(one.Longitude, minLongitude);
                    minLatitude = Math.Min(one.Latitude, minLatitude);

                    dataSet.Add(new GlobeData2()
                    //{ Latitude = one.Latitude, Longitude = one.Longitude });
                    {
                        Longitude = one.Longitude,// BubbleDataGenerator.Random.Next(i, i + 5),
                        Latitude = one.Latitude// BubbleDataGenerator.Random.Next(val - 50, val + 50)
                    });
                    //i++; 
                }
                var xaxis = this.ScatterSplineChart.Axes["ScatterSplineXAxis"] as Infragistics.Controls.Charts.NumericXAxis;
                var yaxis = this.ScatterSplineChart.Axes["ScatterSplineYAxis"] as Infragistics.Controls.Charts.NumericYAxis;

                var xmin = minLongitude - Math.Abs(maxLongitude - minLongitude) * 0.05;
                var xmax = maxLongitude + Math.Abs(maxLongitude - minLongitude) * 0.05;
                var ymin = minLatitude - Math.Abs(maxLatitude - minLatitude) * 0.05;
                var ymax = maxLatitude + Math.Abs(maxLatitude - minLatitude) * 0.05;

                xmin = Math.Round(xmin, 2);
                xmax = Math.Round(xmax, 2);
                ymin = Math.Round(ymin, 2);
                ymax = Math.Round(ymax, 2);
                //xmin = Math.Floor(xmin);
                //xmax = Math.Ceiling(xmax);
                //ymin = Math.Floor(ymin);
                //ymax = Math.Ceiling(ymax);

                xaxis.MinimumValue = xmin;// minLongitude - Math.Abs(maxLongitude - minLongitude) * 0.05;
                xaxis.MaximumValue = xmax;// maxLongitude + Math.Abs(maxLongitude - minLongitude) * 0.05;
                yaxis.MinimumValue = ymin;// minLatitude - Math.Abs(maxLatitude - minLatitude) * 0.05;
                yaxis.MaximumValue = ymax;// maxLatitude + Math.Abs(maxLatitude - minLatitude) * 0.05;

                //this.ScatterSplineChart.Series[0].ItemsSource = dataSet;
                //this.RefreshInternalItemSource();
            }
        }

        private BubbleDataScatterSample GetDataSet()
        {
            object value = null;
            this.Resources.TryGetValue("ScatterData1", out value);
            if (value != null && value is BubbleDataScatterSample)
            {
                return value as BubbleDataScatterSample;
            }
            return null;
        }

        private Size m_renderSize = new Size();

        public void Render(Size size)
        {
            this.m_renderSize = size;

            this.RefreshInternalItemSource();
        }


        private void RefreshInternalItemSource()
        {
            if (this.m_itemsSource == null)
                return;

            IEnumerable<float> primeLatitudes = from one in this.m_itemsSource
                                                select one.Latitude;
            IEnumerable<float> primeLongitudes = from one in this.m_itemsSource
                                                 select one.Longitude;

            float[] longitudeArray = (primeLongitudes == null || primeLongitudes.Count() < 1) ? new float[] { } : primeLongitudes.ToArray();
            float[] latitudeArray = (primeLatitudes == null || primeLatitudes.Count() < 1) ? new float[] { } : primeLatitudes.ToArray();
            Size size = this.m_renderSize; //this.grdHostGrid.RenderSize;

            this.ReAdjustSizes(size, ref longitudeArray, ref latitudeArray);

            this.m_latitudes = latitudeArray;
            this.m_longitudes = longitudeArray;

            this.DrawEWNS_map();
        }


        private FlightDataEntitiesRT.Flight m_currentFlight = null;

        public FlightDataEntitiesRT.Flight CurrentFlight
        {
            get { return m_currentFlight; }
            set
            {
                m_currentFlight = value;

                if (this.m_currentFlight != null && this.m_currentFlight.GlobeDatas != null
                    && this.m_currentFlight.GlobeDatas.Length > 0)
                {
                    this.ItemsSource = m_currentFlight.GlobeDatas;
                }
                else
                    this.ItemsSource = null;
            }
        }

        private float[] m_longitudes;

        public IEnumerable<float> Longitudes
        {
            get
            {
                return m_longitudes;
            }
        }

        private float[] m_latitudes;

        public IEnumerable<float> Latitudes
        {
            get
            {
                return m_latitudes;
            }
        }

        private void DrawEWNS_map()
        {
            if (this.Longitudes.Count() < 4 || this.Latitudes.Count() < 4)
            {
                return;
            }

            Size size = this.m_renderSize;// this.grdHostGrid.RenderSize;
            float[] ews = this.m_longitudes;
            float[] nss = this.m_latitudes;

            GeometryGroup rootGroup = new GeometryGroup();
            //创建起点和终点
            float startX = ews.First();
            float startY = nss.First();
            rootGroup.Children.Add(new EllipseGeometry() { Center = new Point(startX, startY), RadiusX = 10, RadiusY = 10 });
            rootGroup.Children.Add(new EllipseGeometry() { Center = new Point(startX, startY), RadiusX = 8, RadiusY = 8 });

            PathGeometry pg = new PathGeometry() { Figures = new PathFigureCollection() };
            PathFigure figure = new PathFigure() { IsClosed = false, StartPoint = new Point(startX, startY) };
            figure.IsFilled = false;


            PolyBezierSegment segments = this.CreateSegments(ews, nss);

            figure.Segments.Add(segments);
            pg.Figures.Add(figure);
            rootGroup.Children.Add(pg);
            //this.grdHostGrid.Data = rootGroup;
        }

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

        private PolyBezierSegment CreateSegments(float[] ews, float[] nss)
        {
            PolyBezierSegment segments = new PolyBezierSegment();
            if (ews != null && nss != null && ews.Length > 4 && nss.Length > 4)
            {
                int length = Math.Min(ews.Length, nss.Length);

                for (int i = 1; i < length; i++)//跳过第一个点，第一个点是起始点
                {
                    segments.Points.Add(new Point()
                        {
                            X = ews[i],
                            Y = nss[i]
                        });
                }

                int reminder = segments.Points.Count % 3;
                if (reminder != 0)
                {//贝塞尔曲线必须被3整除（否则爆Win32致命异常），如果不够，根据业务需要，添加起始点来完善
                    //必然是1或者2，起码要添加一个：
                    segments.Points.Add(new Point() { X = ews[0], Y = nss[0] });
                    if (reminder == 1)
                    {//差两个，已经添加了一个，再添加一个
                        segments.Points.Add(new Point() { X = ews[0], Y = nss[0] });
                    }
                }
            }

            return segments;
        }
    }

    internal class BubbleDataScatterSample1 : ObservableCollection<GlobeData2>
    {
        public BubbleDataScatterSample1()
        {
            //double startY = 23;
            //double endY = 36;

            //double startX = 115;
            //double endX = 137;

            //Random rand = new Random();

            //for (int i = 0; i < 10000; i++)
            //{
            //    var rate = rand.NextDouble();
            //    var x = startX + (rate * (endX - startX));
            //    var y = startY + (rate * (endY - startY));

            //    GlobeData2 dt = new GlobeData2() { Longitude = (double)x, Latitude = (double)y };
            //    this.Add(dt);
            //}
            this.Generate();
        }
        protected void Generate()
        {
            this.Clear();
            int value = 10;
            for (int i = 0; i < 1000; i++)
            {
                double change = BubbleDataGenerator.Random.NextDouble();
                if (change > .5)
                {
                    value += (int)(change * 20);
                }
                else
                {
                    value -= (int)(change * 20);
                }

                this.Add(new GlobeData2()
                {
                    Longitude = BubbleDataGenerator.Random.Next(i, i + 5),
                    Latitude = BubbleDataGenerator.Random.Next(value - 50, value + 50),
                    //  Radius = BubbleDataGenerator.Random.Next((int)Settings.RadiusMin, (int)Settings.RadiusMax)
                });
                //this.Add(new BubbleDataPoint
                //{
                //    X = BubbleDataGenerator.Random.Next(i, i + 5),
                //    Y = BubbleDataGenerator.Random.Next(value - 50, value + 50),
                //    Radius = BubbleDataGenerator.Random.Next((int)Settings.RadiusMin, (int)Settings.RadiusMax)
                //});
            }
        }
    }

    public class GlobeData2
    {

        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude
        {
            get;
            set;
        }

        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude
        {
            get;
            set;
        }

    }


    public class ScatterDataSample : ScatterDataCollection
    {
        public ScatterDataSample()
        {
            int value = 50;
            for (int i = 0; i < 100; i++)
            {
                double change = ScatterDataGenerator.Random.NextDouble();
                if (change > .5)
                {
                    value += (int)(change * 20);
                }
                else
                {
                    value -= (int)(change * 20);
                }
                value %= 100;
                this.Add(new ScatterDataPoint
                {
                    X = ScatterDataGenerator.Random.Next(i, i + 5),
                    Y = ScatterDataGenerator.Random.Next(value - 50, value + 50)
                });
            }
        }
    }

    public class ScatterDataCollection : ObservableCollection<ScatterDataPoint> { }

    public class ScatterDataPoint : INotifyPropertyChanged
    {
        private double _y;
        public double Y
        {
            get { return _y; }
            set { if (_y == value) return; _y = value; OnPropertyChanged("Y"); }
        }

        private double _x;
        public double X
        {
            get { return _x; }
            set { if (_x == value) return; _x = value; OnPropertyChanged("X"); }
        }
        public new string ToString()
        {
            return String.Format("X {0}, Y {1}", X, Y);
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
                handler(sender, e);
        }
        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    public class ScatterDataRandomSample : ScatterDataCollection
    {
        public ScatterDataRandomSample()
        {
            _settings = new ScatterDataSettings
            {
                DataPoints = 50,
                XStart = 0,
                XChange = 5,
                YStart = 0,
                YChange = 50
            };
            _settings.PropertyChanged += OnSettingsPropertyChanged;
            this.Generate();
        }
        protected void Generate()
        {
            this.Clear();
            int x = (int)this.Settings.XStart;
            int y = (int)this.Settings.YStart;
            for (int i = 0; i < this.Settings.DataPoints; i++)
            {
                double change = ScatterDataGenerator.Random.NextDouble();
                if (change > .5)
                {
                    y += (int)(change * 20);
                }
                else
                {
                    y -= (int)(change * 20);
                }

                this.Add(new ScatterDataPoint
                {
                    X = ScatterDataGenerator.Random.Next(x, x + (int)Settings.XChange),
                    Y = ScatterDataGenerator.Random.Next(y - (int)Settings.YChange, y + (int)Settings.YChange)
                });
                x++;
            }
        }
        private void OnSettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.Generate();
        }
        private ScatterDataSettings _settings;
        public ScatterDataSettings Settings
        {
            get { return _settings; }
            set
            {
                if (_settings == value) return;
                _settings = value;
                this.Generate();
            }
        }
    }

    public class ScatterDataRangeSample : ScatterDataCollection
    {
        public ScatterDataRangeSample()
        {
            _settings = new ScatterDataSettings
            {
                DataPoints = 20,
                XMin = 10,
                XMax = 180,
                YMin = 10,
                YMax = 180
            };
            _settings.PropertyChanged += OnSettingsPropertyChanged;
            this.Generate();
        }
        protected void Generate()
        {
            this.Clear();
            for (int i = 0; i < this.Settings.DataPoints; i++)
            {
                double x = ScatterDataGenerator.Random.Next((int)Settings.XMin, (int)Settings.XMax);
                double y = ScatterDataGenerator.Random.Next((int)Settings.YMin, (int)Settings.YMax);
                this.Add(new ScatterDataPoint { X = x, Y = y });
            }
        }

        private void OnSettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.Generate();
        }
        private ScatterDataSettings _settings;
        public ScatterDataSettings Settings
        {
            get { return _settings; }
            set { if (_settings == value) return; _settings = value; this.Generate(); }
        }
    }

    public class ScatterDataSettings : INotifyPropertyChanged
    {
        public ScatterDataSettings()
        {
            DataPoints = 50;

            XStart = 10;
            XChange = 5;
            //XMax = 90;

            YStart = 10;
            YChange = 5;
            //YMax = 90;
        }
        public double XChange { get; set; }
        public double YChange { get; set; }
        public double XMin { get; set; }
        public double XMax { get; set; }

        public double YMin { get; set; }
        public double YMax { get; set; }

        private int _dataPoints;
        public int DataPoints
        {
            get { return _dataPoints; }
            set { _dataPoints = value; OnPropertyChanged("DataPoints"); }
        }

        private double _xMin;
        public double XStart
        {
            get { return _xMin; }
            set { if (_xMin == value) return; _xMin = value; OnPropertyChanged("XMin"); }
        }

        private double _yMin;
        public double YStart
        {
            get { return _yMin; }
            set { if (_yMin == value) return; _yMin = value; OnPropertyChanged("YMin"); }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
                handler(sender, e);
        }
        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    public static class ScatterDataGenerator
    {
        public static Random Random = new Random();
    }

    public class BubbleDataSample : BubbleDataCollection
    {
        public BubbleDataSample()
        {
            List<BubbleDataPoint> items = new List<BubbleDataPoint>
            {
                new BubbleDataPoint {X = 100, Y = 100, Radius = 50},
                new BubbleDataPoint {X = 80, Y = 80, Radius = 35},
                new BubbleDataPoint {X = 80, Y = 160, Radius = 35},
                new BubbleDataPoint {X = 160, Y = 80, Radius = 35},
                new BubbleDataPoint {X = 160, Y = 160, Radius = 35},
                new BubbleDataPoint {X = 60, Y = 60, Radius = 25},
                new BubbleDataPoint {X = 60, Y = 140, Radius = 25},
                new BubbleDataPoint {X = 140, Y = 60, Radius = 25},
                new BubbleDataPoint {X = 140, Y = 140, Radius = 25},
                new BubbleDataPoint {X = 40, Y = 40, Radius = 15},
                new BubbleDataPoint {X = 40, Y = 120, Radius = 15},
                new BubbleDataPoint {X = 120, Y = 40, Radius = 15},
                new BubbleDataPoint {X = 120, Y = 120, Radius = 15},
                new BubbleDataPoint {X = 20, Y = 20, Radius = 10},
                new BubbleDataPoint {X = 20, Y = 180, Radius = 10},
                new BubbleDataPoint {X = 180, Y = 20, Radius = 10},
                new BubbleDataPoint {X = 180, Y = 180, Radius = 10}
            };


            List<GlobeData2> items2 = new List<GlobeData2>
            {
                new GlobeData2 {Longitude = 100, Latitude = 100},
                new GlobeData2 {Longitude = 80, Latitude = 80},
                new GlobeData2 {Longitude = 80, Latitude = 160},
                new GlobeData2 {Longitude = 160, Latitude = 80},
                new GlobeData2 {Longitude = 160, Latitude = 160},
                new GlobeData2 {Longitude = 60, Latitude = 60},
                new GlobeData2 {Longitude = 60, Latitude = 140},
                new GlobeData2 {Longitude = 140, Latitude = 60},
                new GlobeData2 {Longitude = 140, Latitude = 140},
                new GlobeData2 {Longitude = 40, Latitude = 40},
                new GlobeData2 {Longitude = 40, Latitude = 120},
                new GlobeData2 {Longitude = 120, Latitude = 40},
                new GlobeData2 {Longitude = 120, Latitude = 120},
                new GlobeData2 {Longitude = 20, Latitude = 20},
                new GlobeData2 {Longitude = 20, Latitude = 180},
                new GlobeData2 {Longitude = 180, Latitude = 20},
                new GlobeData2 {Longitude = 180, Latitude = 180}
            };

            var sortedItems = items2.ToArray();//.OrderByDescending(item => item.Radius).ToArray();
            for (int i = 0; i < sortedItems.Count(); i++)
            {
                //sortedItems[i].Label = "Item " + (i + 1);
                this.Add(sortedItems[i]);
            }
        }
    }

    public class BubbleDataRandomSample : BubbleDataCollection
    {
        public BubbleDataRandomSample()
        {
            _settings = new BubbleDataSettings
            {
                DataPoints = 15,
                RadiusMin = 5,
                RadiusMax = 50,
                XMin = 10,
                XMax = 200,
                YMin = 10,
                YMax = 200
            };
            _settings.PropertyChanged += OnSettingsPropertyChanged;
            this.Generate();
        }
        protected void Generate()
        {
            this.Clear();
            List<BubbleDataPoint> randomItems = new List<BubbleDataPoint>();
            for (int i = 0; i < Settings.DataPoints; i++)
            {
                double r = BubbleDataGenerator.Random.Next((int)Settings.RadiusMin, (int)Settings.RadiusMax);
                double x = BubbleDataGenerator.Random.Next((int)Settings.XMin, (int)Settings.XMax);
                double y = BubbleDataGenerator.Random.Next((int)Settings.YMin, (int)Settings.YMax);
                randomItems.Add(new BubbleDataPoint { X = x, Y = y, Radius = r });
            }
            var sortedItems = randomItems.OrderByDescending(item => item.Radius).ToArray();
            for (int i = 0; i < sortedItems.Count(); i++)
            {
                sortedItems[i].Label = "Item " + (i + 1);
                //this.Add(sortedItems[i]);
            }
        }
        private void OnSettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.Generate();
        }
        private BubbleDataSettings _settings;
        public BubbleDataSettings Settings
        {
            get { return _settings; }
            set
            {
                if (_settings == value) return;
                _settings = value;
                this.Generate();
            }
        }
    }

    public class BubbleDataScatterSample : BubbleDataCollection
    {
        public BubbleDataScatterSample()
        {
            //_settings = new BubbleDataSettings
            //{
            //    DataPoints = 50,
            //    RadiusMin = 5,
            //    RadiusMax = 40,
            //    XMin = 0,
            //    YMin = 0,
            //};
            //_settings.PropertyChanged += OnSettingsPropertyChanged;
            //this.Generate();
        }

        protected void Generate()
        {
            this.Clear();
            int value = (int)this.Settings.YMin;
            for (int i = 0; i < this.Settings.DataPoints; i++)
            {
                double change = BubbleDataGenerator.Random.NextDouble();
                if (change > .5)
                {
                    value += (int)(change * 20);
                }
                else
                {
                    value -= (int)(change * 20);
                }

                this.Add(new GlobeData2()
                {
                    Longitude = BubbleDataGenerator.Random.Next(i, i + 5),
                    Latitude = BubbleDataGenerator.Random.Next(value - 50, value + 50),
                    //  Radius = BubbleDataGenerator.Random.Next((int)Settings.RadiusMin, (int)Settings.RadiusMax)
                });
                //this.Add(new BubbleDataPoint
                //{
                //    X = BubbleDataGenerator.Random.Next(i, i + 5),
                //    Y = BubbleDataGenerator.Random.Next(value - 50, value + 50),
                //    Radius = BubbleDataGenerator.Random.Next((int)Settings.RadiusMin, (int)Settings.RadiusMax)
                //});
            }
        }
        private void OnSettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.Generate();
        }
        private BubbleDataSettings _settings;
        public BubbleDataSettings Settings
        {
            get { return _settings; }
            set
            {
                if (_settings == value) return;
                _settings = value;
                this.Generate();
            }
        }
    }

    public static class BubbleDataGenerator
    {
        public static Random Random = new Random();
    }

    public class BubbleDataSettings : INotifyPropertyChanged
    {
        public BubbleDataSettings()
        {
            DataPoints = 100;

            RadiusMin = 10;
            RadiusMax = 50;

            XMin = 10;
            XInterval = 5;
            XMax = 90;

            YMin = 10;
            YInterval = 5;
            YMax = 90;
        }
        public double XInterval { get; set; }
        public double YInterval { get; set; }

        private int _dataPoints;
        public int DataPoints
        {
            get { return _dataPoints; }
            set { _dataPoints = value; OnPropertyChanged("DataPoints"); }
        }

        private double _radiusMin;
        public double RadiusMin
        {
            get { return _radiusMin; }
            set { if (_radiusMin == value) return; _radiusMin = value; OnPropertyChanged("RadiusMin"); }
        }
        private double _radiusMax;
        public double RadiusMax
        {
            get { return _radiusMax; }
            set { if (_radiusMax == value) return; _radiusMax = value; OnPropertyChanged("RadiusMax"); }
        }

        private double _xMin;
        public double XMin
        {
            get { return _xMin; }
            set { if (_xMin == value) return; _xMin = value; OnPropertyChanged("XMin"); }
        }

        private double _xMax;
        public double XMax
        {
            get { return _xMax; }
            set { if (_xMax == value) return; _xMax = value; OnPropertyChanged("XMax"); }
        }

        private double _yMin;
        public double YMin
        {
            get { return _yMin; }
            set { if (_yMin == value) return; _yMin = value; OnPropertyChanged("YMin"); }
        }

        private double _yMax;
        public double YMax
        {
            get { return _yMax; }
            set { if (_yMax == value) return; _yMax = value; OnPropertyChanged("YMax"); }
        }
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
                handler(sender, e);
        }
        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    public class BubbleDataCollection : ObservableCollection<GlobeData2> //BubbleDataPoint>
    { }

    public class BubbleDataPoint : INotifyPropertyChanged
    {
        #region Properties

        private double _y;
        public double Y
        {
            get { return this._y; }
            set { if (_y == value) return; _y = value; OnPropertyChanged("Y"); }
        }

        private double _x;
        public double X
        {
            get { return _x; }
            set { if (_x == value) return; _x = value; OnPropertyChanged("X"); }
        }

        private double _radius;
        public double Radius
        {
            get { return _radius; }
            set { if (_radius == value) return; _radius = value; OnPropertyChanged("Radius"); }
        }

        private string _label;
        public string Label
        {
            get { return _label; }
            set { if (_label == value) return; _label = value; OnPropertyChanged("Label"); }
        }

        #endregion

        public new string ToString()
        {
            return String.Format("X {0}, Y {1}, Radius {2}", X, Y, Radius);
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
                handler(sender, e);
        }
        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
