using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftDataAnalysisWinRT.DataModel
{
    public sealed class TestModelDataSource : ObservableCollection<FlightDataReading.AircraftModel1.AircraftModel1RawData> //, IEnumerable<TestModelItem>
    {
        public TestModelDataSource()
        {
            Random ran = new Random();

            for (int i = 1; i <= 120; i++)
            {
                FlightDataReading.AircraftModel1.AircraftModel1RawData item = new FlightDataReading.AircraftModel1.AircraftModel1RawData()
                {
                    Second = i,
                    //SecondStr = i.ToString(),
                    Hp = Convert.ToDouble(100 * ran.NextDouble())
                };

                this.Add(item);
            }
        }

        private static TestModelDataSource m_instance = new TestModelDataSource();

        public static ObservableCollection<FlightDataReading.AircraftModel1.AircraftModel1RawData> Instance
        {
            get
            {
                return m_instance;
            }
        }
    }

    public class TestModelItem
    {

        public double Value { get; set; }

        public string Label { get; set; }
    }
}
