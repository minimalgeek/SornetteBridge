using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bridge;
using MathWorks.MATLAB.NET.Arrays;

namespace BridgeTest
{
    [TestClass]
    public class SornetteBridgeTest
    {
        private SornetteBridge bridge = new SornetteBridge();

        [TestMethod]
        public void TestSorFixToMatrix()
        {
            var param = new SornetteBridgeParameterObject();

            param.array1 = new MWNumericArray(new int[] { 1, 10 });
            param.array2 = new MWNumericArray(new int[] { 1, 20 });
            param.array3 = new MWNumericArray(new int[] { 1, 12 });
            param.array4 = new MWObjectArray(1);
            param.array5 = new MWObjectArray(2);
            param.array6 = new MWObjectArray(3);
            param.array7 = new MWObjectArray(4);
            param.array8 = new MWObjectArray(5);
            param.array9 = new MWObjectArray(6);

            var ret = bridge.SorFixToMatrix(param);
            Assert.IsNotNull(ret);
        }
    }
}
