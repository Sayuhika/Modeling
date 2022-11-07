using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace EvolutionOfProbabilityDensity
{
    class FunctionsMkII
    {
        public double dx, dt;
        public List<Complex[]> EoPD;
        public Complex[][] FFunctions;
        public double[] E;
        protected const double C = 5;
        private const double kzat = 4;
        // Множитель расширения границ рабочей области 
        protected double d_exMult;
        public FunctionsMkII(Double dx_, Double d, Double U, double d_exMult, Complex[]initPsi)
        {
            dx = dx_;
            dt = C * dx;

            this.d_exMult = d_exMult;

            int n = (int)(2 *  d_exMult / dx);
            E = new double[n];
            EoPD = new List<Complex[]>();
            FFunctions = new Complex[n][];

            // Создание ямы
            for (int i = 0; i < n; i++)
            {
                E[i] = 0;

                if (Math.Abs((double)i / n - 0.5) < (d / (200 * d_exMult)))
                {
                    E[i] = -U;
                }
            }

            EoPD.Add(initPsi);
        }
        Complex Sigm(double x)
        {
            if (x >= 1) {return (x - 1) * (x - 1) + Complex.Zero; }   //abs(x-rz)
            if (x <= -1){return (x + 1) * (x + 1) + Complex.Zero; }   //abs(x+rz)
            return Complex.Zero;
        }
        Complex fz(int k)
        {
            return 1 / (1 + Complex.ImaginaryOne * kzat * Sigm(k * dx - d_exMult));
        }
        Complex psi_fz_ss(int i)
        { 
            if (i == 0 || i == E.Length){return Complex.Zero;}

            return (fz(i + 1) * EoPD.Last()[i+1] - (fz(i + 1) + fz(i - 1)) * EoPD.Last()[i] + fz(i - 1) * EoPD.Last()[i - 1]) / dx / dx;
        }
        // Рассчет коэффициентов A, B, C, D, alpha, beta
        protected virtual Complex kA(int k)
        {
            return - dt * 0.5 * Complex.ImaginaryOne * fz(k) * fz(k - 1) / dx / dx;
        }
        protected virtual Complex kB(int k)
        {
            return - dt * 0.5 * Complex.ImaginaryOne * fz(k) * fz(k + 1) / dx / dx;
        }
        protected virtual Complex kC(int k)
        {
            return 1 + Complex.ImaginaryOne * dt * 0.5 * E[k]  - kA(k) - kB(k);
        }
        protected virtual Complex kD(int k)
        {
            return (1 - Complex.ImaginaryOne * dt * 0.5 * E[k]) * EoPD.Last()[k] + Complex.ImaginaryOne * dt * 0.5 * fz(k) * psi_fz_ss(k);
        }
        protected Complex kAlpha(int k, Complex AlphaPrev)
        {
            return -1.0 * kB(k) / (kC(k) + kA(k) * AlphaPrev);
        }
        protected Complex kBeta(int k, Complex AlphaPrev, Complex BetaPrev)
        {
            return (kD(k) - kA(k) * BetaPrev) / (kC(k) + kA(k) * AlphaPrev);
        }
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

            for (int i = 1; i < FFunctions.Length - 1; i++)
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
