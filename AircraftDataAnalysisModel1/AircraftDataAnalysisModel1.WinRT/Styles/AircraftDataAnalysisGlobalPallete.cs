using Infragistics.Controls.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace AircraftDataAnalysisWinRT.Styles
{
    public class AircraftDataAnalysisGlobalPallete
    {
        private static Windows.UI.Xaml.Media.Brush[] m_Brushes;

        public static Windows.UI.Xaml.Media.Brush[] Brushes
        {
            get
            {
                if (m_Brushes != null)
                    return m_Brushes;

                Init();

                return m_Brushes;
            }
        }

        private static void Init()
        {
            m_Brushes = new Brush[]{
                new SolidColorBrush(Colors.Aquamarine),
                new SolidColorBrush(Colors.Green),
                new SolidColorBrush(Colors.Aqua),
                new SolidColorBrush(Colors.Tan),
                new SolidColorBrush(Colors.Yellow),
                new SolidColorBrush(Colors.Teal),
                new SolidColorBrush(Colors.Tomato),
                new SolidColorBrush(Colors.SpringGreen),
                new SolidColorBrush(Colors.RosyBrown),
                new SolidColorBrush(Colors.DarkOrchid),
            };
        }
    }

    public class PalleteMarkerTypes
    {
        private static Infragistics.Controls.Charts.MarkerType[] m_MarkerTypes;

        public static Infragistics.Controls.Charts.MarkerType[] MarkerTypes
        {
            get
            {
                if (m_MarkerTypes != null)
                    return m_MarkerTypes;

                Init();

                return m_MarkerTypes;
            }
        }

        private static void Init()
        {
            m_MarkerTypes = new Infragistics.Controls.Charts.MarkerType[]{
                MarkerType.Circle,
                MarkerType.Diamond ,
                MarkerType.Hexagon,
                MarkerType.Hexagram ,
                MarkerType.Pentagon ,
                MarkerType.Pentagram ,
                MarkerType.Pyramid ,
                MarkerType.Square ,
                MarkerType.Tetragram,
                MarkerType.Triangle 
            };
        }
    }
}
