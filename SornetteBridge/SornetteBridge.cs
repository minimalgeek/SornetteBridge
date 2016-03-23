using System;
using AmiBroker;
using AmiBroker.PlugIn;
using AmiBroker.Utils;
using MathWorks.MATLAB.NET.Arrays;
using LibSornette;
using System.Collections.Generic;

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

        public SornetteBridgeParameterObject() {}

        public SornetteBridgeParameterObject(
            ATArray atArray1, 
            ATArray atArray2, 
            float num1,
            float num2,
            float num3,
            float num4,
            float num5,
            float num6)
        {
            array1 = ATArrayToMWArray(atArray1);
            array2 = ATArrayToMWArray(atArray2);
            array3 = new MWObjectArray((double)num1);
            array4 = new MWObjectArray((double)num2);
            array5 = new MWObjectArray((double)num3);
            array6 = new MWObjectArray((double)num4);
            array7 = new MWObjectArray((double)num5);
            array8 = new MWObjectArray((double)num6);
        }

        public SornetteBridgeParameterObject(
            ATArray atArray1,
            ATArray atArray2,
            ATArray atArray3,
            float num1,
            float num2,
            float num3,
            float num4,
            float num5,
            float num6)
        {
            array1 = ATArrayToMWArray(atArray1);
            array2 = ATArrayToMWArray(atArray2);
            array3 = ATArrayToMWArray(atArray3);
            array4 = new MWObjectArray((double)num1);
            array5 = new MWObjectArray((double)num2);
            array6 = new MWObjectArray((double)num3);
            array7 = new MWObjectArray((double)num4);
            array8 = new MWObjectArray((double)num5);
            array9 = new MWObjectArray((double)num6);
        }

        private MWArray ATArrayToMWArray(ATArray atArray)
        {
            List<double> container = new List<double>();
            for (int i = 0; i < atArray.Length; i++)
            {
                YTrace.Trace("ATArrayToMWArray: " + atArray[i], YTrace.TraceLevel.Information);
                container.Add((double)atArray[i]);
            }

            return new MWNumericArray(1, atArray.Length, container.ToArray());
        }
    }

    public class SornetteBridge : IndicatorBase
    {
        [ABMethod]
        public ATArray SorNow(
            ATArray array1,
            ATArray array2,
            float num1,
            float num2,
            float num3,
            float num4,
            float num5,
            float num6)
        {
            double[,] matrix = SorNowToMatrix(
                new SornetteBridgeParameterObject(array1, array2, num1, num2, num3, num4, num5, num6));
            return DoubleMatrixToATArray(matrix);
        }

        [ABMethod]
        public ATArray SorRoll(
            ATArray array1,
            ATArray array2,
            ATArray array3,
            float num1,
            float num2,
            float num3,
            float num4,
            float num5,
            float num6)
        {
            double[,] matrix = SorRollToMatrix(
                new SornetteBridgeParameterObject(array1, array2, array3, num1, num2, num3, num4, num5, num6));
            return DoubleMatrixToATArray(matrix);
        }

        [ABMethod]
        public ATArray SorFix(
            ATArray array1, 
            ATArray array2, 
            ATArray array3, 
            float num1,
            float num2,
            float num3,
            float num4,
            float num5,
            float num6)
        {
            double[,] matrix = SorFixToMatrix(
                new SornetteBridgeParameterObject(array1, array2, array3, num1, num2, num3, num4, num5, num6));
            return DoubleMatrixToATArray(matrix);
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
                YTrace.Trace("SorFixToMatrix: " + e.Message, YTrace.TraceLevel.Information);
                return null;
            }
        }

        public double[,] SorRollToMatrix(SornetteBridgeParameterObject parameter)
        {
            try
            {
                Sornette sornette = new Sornette();
                MWArray calculation = sornette.ami_sor_roll(
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
                YTrace.Trace("SorRollToMatrix: " + e.Message, YTrace.TraceLevel.Information);
                return null;
            }
        }

        public double[,] SorNowToMatrix(SornetteBridgeParameterObject parameter)
        {
            try
            {
                Sornette sornette = new Sornette();
                MWArray calculation = sornette.ami_sor_now(
                    parameter.array1,
                    parameter.array2,
                    parameter.array3,
                    parameter.array4,
                    parameter.array5,
                    parameter.array6,
                    parameter.array7,
                    parameter.array8);

                double[,] ret = CalculationToDoubleMatrix(calculation);
                return ret;
            }
            catch (Exception e)
            {
                YTrace.Trace("SorNowToMatrix: " + e.Message, YTrace.TraceLevel.Information);
                return null;
            }
        }

        private double[,] CalculationToDoubleMatrix(MWArray calculation)
        {
            double[,] ret = new double[calculation.Dimensions[0], calculation.Dimensions[1]];
            for (int i = 0; i < calculation.Dimensions[0]; i++)
            {
                for (int j = 0; j < calculation.Dimensions[1]; j++)
                {
                    ret[i, j] = (double)calculation.ToArray().GetValue(i, j);
                    YTrace.Trace("CalculationToDoubleMatrix: " + ret[i, j], YTrace.TraceLevel.Information);
                }
                i++;
            }

            return ret;
        }

        private ATArray DoubleMatrixToATArray(double[,] matrix)
        {
            ATArray ret = new ATArray();

            if (matrix != null)
            {
                for (int i = 0; i < matrix.GetLength(1); i++)
                {
                    ret[i] = (float)matrix[0, i];
                    YTrace.Trace("DoubleMatrixToATArray: " + ret[i], YTrace.TraceLevel.Information);
                }
            }

            return ret;
        }
    }
}
