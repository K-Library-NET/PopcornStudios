using FlightDataEntitiesRT;
using System;
using System.Collections.Generic;
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

                //this.RefreshInternalItemSource();
            }
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
            this.grdHostGrid.Data = rootGroup;
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

}
