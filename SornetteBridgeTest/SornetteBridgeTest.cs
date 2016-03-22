using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bridge;

namespace BridgeTest
{
    [TestClass]
    public class SornetteBridgeTest
    {
        [TestMethod]
        public void TestSorFixToMatrix()
        {
            var bridge = new SornetteBridge();
            var ret = bridge.SorFixToMatrix();

            Console.WriteLine("Unit ready");
        }
    }
}
