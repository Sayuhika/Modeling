using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace EvolutionOfProbabilityDensity
{
    class Functions
    {
        public double dx, dt;
        public List<Complex[]> EoPD;
        public Complex[][] FFunctions;
        public double[] E;
        protected const double C = 5;

        public Functions(Double dx_, Double d, Double U)
        {
            dx = dx_;
            dt = C * dx;
            int n = (int)(1 / dx);
            E = new double[n];
            EoPD = new List<Complex[]>();
            FFunctions = new Complex[n][];
            
            // Создание ямы
            for (int i = 0; i < n; i++)
            {
                E[i] = 0;

                if (Math.Abs((double)i / n - 0.5) < (d / 200))
                {
                    E[i] = -U;
                }
            }
        }
        public virtual void Init(Complex[] initPsi)
        {
            EoPD.Add(initPsi);
        }
        // Рассчет коэффициентов A, B, C, D, alpha, beta
        protected virtual Complex kA(int k)
        {
            Double A = -dt / (2.0 * dx * dx);

            return Complex.Multiply(Complex.ImaginaryOne, A);
        }
        protected virtual Complex kB(int k)
        {
            Double B = -dt / (2.0 * dx * dx);

            return Complex.Multiply(Complex.ImaginaryOne, B);
        }
        protected virtual Complex kC(int k)
        {
            Double C = 0.5 * dt * E[k];

            return Complex.Add(1.0, Complex.Multiply(Complex.ImaginaryOne, C) - kA(k) - kB(k));
        }
        protected virtual Complex kD(int k)
        {
            Complex ddPsi = Complex.Divide(EoPD.Last()[k + 1] - EoPD.Last()[k], dx) - Complex.Divide(EoPD.Last()[k] - EoPD.Last()[k - 1], dx);
            ddPsi = Complex.Multiply(2.0 / (2.0 * dx), ddPsi);

            return EoPD.Last()[k] + Complex.Multiply(Complex.ImaginaryOne, 0.5 * dt) * (ddPsi - Complex.Multiply(E[k], EoPD.Last()[k]));
        }

        protected Complex kAlpha(int k, Complex AlphaPrev)
        {
            return -1.0 * kB(k) / (kC(k) + kA(k) * AlphaPrev);
        }
        protected Complex kBeta(int k, Complex AlphaPrev, Complex BetaPrev)
        {
            return (kD(k) - kA(k) * BetaPrev) / (kC(k) + kA(k) * AlphaPrev);
        }

        // Шаг
        public virtual Complex[] EvolutionStep(Complex Mu0, Complex Nu0, Complex MuN, Complex NuN)
        {
            var Alpha = new Complex[EoPD.Last().Length];
            var Beta = new Complex[EoPD.Last().Length];
            Alpha[1] = Mu0; Beta[1] = Nu0;

            for (int k = 2; k < EoPD.Last().Length; k++)
            {
                Alpha[k] = kAlpha(k - 1, Alpha[k - 1]);
                Beta[k] = kBeta(k - 1, Alpha[k - 1], Beta[k - 1]);
            }

            var NewPsi = new Complex[EoPD.Last().Length];
            NewPsi[NewPsi.Length - 1] = (MuN * Beta.Last() + NuN) / (1.0 - MuN * Alpha.Last());

            for (int k = NewPsi.Length - 1; k > 0; k--)
            {
                NewPsi[k - 1] = Alpha[k] * NewPsi[k] + Beta[k];
            }
        
            EoPD.Add(NewPsi);

            return EoPD.Last();
        }

        public Complex[] GetEigenFunction(int freq)
        {
            var result = new Complex[FFunctions.Length];

            for (int i = 1; i < FFunctions.Length-1; i++)
            {
                result[i] = FFunctions[i][freq];
            }
            // Костыльная палка для рисовалки
            if (FFunctions.Length >= 2)
            {
                result[0] = result[1];
                result[FFunctions.Length - 1] = result[FFunctions.Length - 2];
            }

            return result;
        }
        public void toFourier()
        {
            int n = 1;
            while (n < EoPD.Count)
            {
                n *= 2;
            }
            n /= 2;
            for (int i = 0; i < FFunctions.Length; i++)
            {
                var temp = new Complex[n];

                for (int j = 0; j < n; j++)
                {
                    temp[j] = EoPD[j][i];
                }
                
                FFunctions[i] = Fourier(temp, false);

                // Масштабирование к 1
                //double max = FFunctions[i].Max(v => v.Magnitude);
                //for (int j = 0; j < FFunctions[i].Length; j++)
                //{
                //    FFunctions[i][j] /= max;
                //}
            }
        }

        // Метод Фурье
        public static Complex[] Fourier(Complex[] Data, bool Reverse)
        {
            int n = Data.Length;
            int i, j, istep;
            int m, mmax;
            Complex temp;
            double r = Math.PI;
            if (!Reverse) r = -r;
            Complex[] work = new Complex[n];
            Data.CopyTo(work, 0);

            j = 0;
            for (i = 0; i < n; i++)
            {
                if (i < j)
                {
                    temp = work[j];
                    work[j] = work[i];
                    work[i] = temp;
                }
                m = n >> 1;
                while (j >= m)
                {
                    j -= m;
                    m = (m + 1) / 2;
                }
                j += m;
            }
            mmax = 1;
            while (mmax < n)
            {
                istep = mmax << 1;
                double r1 = r / (float)mmax;
                for (m = 0; m < mmax; m++)
                {
                    Complex w = new Complex(Math.Cos(r1 * m), Math.Sin(r1 * m));
                    for (i = m; i < n; i += istep)
                    {
                        j = i + mmax;
                        temp = w * work[j];
                        work[j] = work[i] - temp;
                        work[i] = work[i] + temp;
                    }
                }
                mmax = istep;
            }
            return work;
        }
    }
}
