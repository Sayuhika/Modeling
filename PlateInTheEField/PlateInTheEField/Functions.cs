using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateInTheEField
{
    class Functions
    {
        public double L, V, N;
        public int n;
        public double[] Potential;
        public Functions() { }

        public void Resolve()
        {
            Potential = new double[n];

            for (uint i = 0; i < Potential.Length; i++)
                Potential[i] = - V * (1 - i / (Potential.Length - 1));

            var FA = new double[Potential.Length];
            var FB = new double[Potential.Length];
            var FQ = new double[Potential.Length];
            var FR = new double[Potential.Length];
            double Diff;
            do
            {
                for (uint i = 0; i < Potential.Length; i++)
                {
                    FQ[i] = pFuncDer(Potential[i]);
                    FR[i] = pFunc(Potential[i]) + FQ[i] * Potential[i];
                }
                PropFwd(FQ, FR, V, out FA, out FB, 1.0 / Potential.Length);
                var NewPot = PropRev(Potential, FQ, FR, FA, FB, 1.0 / Potential.Length);
                Diff = CurveDistance(Potential, NewPot);

                if (double.IsNaN(Diff))
                {
                    throw new Exception("Произошел NaN");
                }
                else
                {
                    Potential = NewPot;
                }
            } while (Diff > 1e-10);

            if (double.IsNaN(Diff))
            {
                throw new Exception("Произошел NaN");
            }
        }

        public double pFunc(double psi)
        {
            return N * (1 - Math.Exp(-psi));
        }

        public double pFuncDer(double psi)
        {
            return N * Math.Exp(-psi);
        }
        public static void PropFwd(double[] FQ, double[] FR, double LeftVal, out double[] FA, out double[] FB, double h = 1.0)
        {
            FA = new double[FQ.Length];
            FB = new double[FQ.Length];
            FA[0] = 0.0; FB[0] = LeftVal;

            for (int t = 1; t < FQ.Length; t++)
            {
                FA[t] = FA[t - 1] + h * (FQ[t - 1] * FA[t - 1] * FA[t - 1] - 1.0);                      // A' = Q * A^2 - 1 // q - a^2
                FB[t] = FB[t - 1] + h * (FQ[t - 1] * FA[t - 1] * FB[t - 1] - FR[t - 1] * FA[t - 1]);    // B' = Q * A * B - R * A // r - ab
            }
        }

        public static double[] PropRev(double[] Data, double[] FQ, double[] FR, double[] FA, double[] FB, double h = 1.0)
        {
            var NewData = new double[Data.Length];
            NewData[Data.Length - 1] = (Data.Last() - FB.Last()) / FA.Last();
            for (int t = Data.Length - 1; t > 0; t--)
                NewData[t - 1] = NewData[t] - h * (FR[t] - FQ[t] * (FA[t] * Data[t] + FB[t]));  // Y' = R-Q*(AY+B)
            for (int i = 0; i < Data.Length; i++)
                NewData[i] = FA[i] * NewData[i] + FB[i];    // P = AY+B
            return NewData;
        }

        public static double CurveDistance(double[] FA, double[] FB)
        {
            double Result = 0.0;
            for (uint i = 0; i < FA.Length; i++)
                Result += (FA[i] - FB[i]) * (FA[i] - FB[i]);
            return (Result / FA.Length);
        }
    }
}
