using System;
using AmiBroker;
using AmiBroker.PlugIn;
using AmiBroker.Utils;
using MathWorks.MATLAB.NET.Arrays;
using LibSornette;

namespace Bridge
{
    public class SornetteBridgeParameterObject
    {
        public MWArray array1;
        public MWArray array2;
        public MWArray array3;
        public MWArray array4;
        public MWArray array5;
        public MWArray array6;
        public MWArray array7;
        public MWArray array8;
        public MWArray array9;
    }

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
            //SorFixToMatrix();
            ATArray result = new ATArray();
            return result;
        }

        public double[,] SorFixToMatrix(SornetteBridgeParameterObject parameter)
        {
            try
            {
                Sornette sornette = new Sornette();
                MWArray calculation = sornette.ami_sor_fix(
                    parameter.array1,
                    parameter.array2,
                    parameter.array3,
                    parameter.array4,
                    parameter.array5,
                    parameter.array6,
                    parameter.array7,
                    parameter.array8,
                    parameter.array9);

                double[,] ret = CalculationToDoubleMatrix(calculation);
                return ret;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        private static double[,] CalculationToDoubleMatrix(MWArray calculation)
        {
            double[,] ret = new double[calculation.Dimensions[0], calculation.Dimensions[1]];
            for (int i = 0; i < calculation.Dimensions[0]; i++)
            {
                for (int j = 0; j < calculation.Dimensions[1]; j++)
                {
                    ret[i, j] = (double)calculation.ToArray().GetValue(i, j);
                }
                i++;
            }

            return ret;
        }
    }
}
