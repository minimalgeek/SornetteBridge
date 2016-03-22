using System;
using AmiBroker;
using AmiBroker.PlugIn;
using AmiBroker.Utils;
using LibSornette;
using MathWorks.MATLAB.NET.Arrays;

namespace Sornette
{
    public class SornetteBridge : IndicatorBase
    {
        [ABMethod]
        public ATArray SorNow(ATArray array, float period)
        {
            ATArray result = AFAvg.Ma(array, period);
            return result;
        }

        [ABMethod]
        public ATArray SorRoll(ATArray array, float period)
        {
            ATArray result = AFAvg.Ma(array, period);
            return result;
        }

        [ABMethod]
        public ATArray SorFix(ATArray array, float period)
        {
            SorFixToMatrix();
            ATArray result = new ATArray();
            return result;
        }

        public double[,] SorFixToMatrix()
        {
            try
            {
                Sornette sornette = new Sornette();
                MWArray calculation = sornette.ami_sor_fix();
                double[,] arr = (double[,])((MWNumericArray)calculation).ToArray(MWArrayComponent.Real);
                return arr;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
