using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PopcornStudio.MetroTrainInterop;

namespace UnitTestMetroTrainInterop
{
    /// <summary>
    /// 单元测试，以广州的数据进行测试
    /// </summary>
    [TestClass]
    public class UnitTestGuangzhou
    {
        public UnitTestGuangzhou()
        {
        }

        /// <summary>
        /// 单元测试
        /// </summary>
        [TestMethod]
        public void TestGuangzhouMethod1()
        {
            MetroPath path = new MetroPath()
            {
                CityName = "广州",
                StartStation = new MetroStation() { StationName = "番禺广场" },
                EndStation = new MetroStation() { StationName = "科韵路" }
            };

            Assert.AreEqual(path.SelectedPrice, 6);
        }
    }
}
