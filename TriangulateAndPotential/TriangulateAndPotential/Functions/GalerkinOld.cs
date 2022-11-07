using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Drawing;
using System.Windows.Media.Media3D;

namespace TriangulateAndPotential
{
    class GalerkinOld
    {
        public static void ConstructModel(List<PointDP> points, out double[,] A, out double[] R)
        {
            // Считаем число некраевых (внутренних) точек.
            int count = points.Count - points.Count((node) => node.border);

            // Инициализация матрицы и вектора
            A = new double[count, count];
            R = new double[count];

            int i = 0;

            // Проходимся по всем узлам системы.
            foreach (PointDP NI in points)
            {
                // Если узел является граничной точкой, то пропускаем и идем к следующей.
                if (NI.border) continue;

                int j = 0;
                double b = double.Epsilon;

                // Проходимся по всем узлам системы в новом цикле.
                foreach (PointDP NJ in points)
                {
                    double a = double.Epsilon;

                    // Если точка граничная.
                    if (NJ.border)
                    {
                        b += NJ.potential * K_ij(NI, NJ);
                    }
                    else
                    {
                        if (NJ == NI)
                        {
                            // Проходимся по всем треугольникам прилегающим к точке.
                            foreach (Triangle tri in NI.nTriangles)
                            {
                                var lower = new List<double[]>();

                                // Находим основание грани пирамиды (две точки не являющиеся вершной пирамиды).
                                if (tri.p1 != NI) { lower.Add(new double[] { tri.p1.X, tri.p1.Y, 0 }); }
                                if (tri.p2 != NI) { lower.Add(new double[] { tri.p2.X, tri.p2.Y, 0 }); }
                                if (tri.p3 != NI) { lower.Add(new double[] { tri.p3.X, tri.p3.Y, 0 }); }
                                if (lower.Count != 2) throw new InvalidOperationException();

                                // Создаем вершину.
                                var m3 = new double[] { NI.X, NI.Y, 1 };

                                // Находим коэффициенты наклона и площадь грани.
                                var t3D = ABS(lower[0], lower[1], m3);

                                a += (t3D[0] * t3D[0] + t3D[1] * t3D[1]) * t3D[2];
                            }
                        }
                        else if (NI.nPoints.Contains(NJ)) { a += K_ij(NI, NJ); }

                        A[i, j] = a;
                        j++;
                    }
                }

                R[i] = b;
                i++;
            }
        }
        static double[] ABS(double[] m1, double[] m2, double[] m3)
        {
            double GetDetRank2(double a1, double b1, double a2, double b2) { return a1 * b2 - b1 * a2; }

            // Коэффициенты наклона.
            double A = GetDetRank2(m2[1] - m1[1], m2[2] - m1[2], m3[1] - m1[1], m3[2] - m1[2]);
            double B = -GetDetRank2(m2[0] - m1[0], m2[2] - m1[2], m3[0] - m1[0], m3[2] - m1[2]);

            // Площадь.
            double side1 = Math.Sqrt((m1[0] - m2[0]) * (m1[0] - m2[0]) + (m1[1] - m2[1]) * (m1[1] - m2[1]));
            double side2 = Math.Sqrt((m2[0] - m3[0]) * (m2[0] - m3[0]) + (m2[1] - m3[1]) * (m2[1] - m3[1]));
            double side3 = Math.Sqrt((m3[0] - m1[0]) * (m3[0] - m1[0]) + (m3[1] - m1[1]) * (m3[1] - m1[1]));
            double p = (side1 + side2 + side3) * 0.5;
            double S = Math.Sqrt(p * (p - side1) * (p - side2) * (p - side3));

            return new double[] { A, B, S };
        }
        static double[] ABS2(double[] m1, double[] m2, double[] m3)
        {
            Vector3D v = Vector3D.CrossProduct(new Vector3D(m3[0] - m1[0], m3[1] - m1[1], m3[2] - m1[2]), new Vector3D(m3[0] - m2[0], m3[1] - m2[1], m3[2] - m2[2]));

            return new double[] { v.X, v.Y, v.Length * 0.5 };
        }
        static double K_ij(PointDP NI, PointDP NJ)
        {
            var Shared = new List<PointDP>();

            // Ищем смежные точки для двух соседних вершин.
            foreach (PointDP NK in NI.nPoints)
                if (NJ.nPoints.Contains(NK))
                    Shared.Add(NK);

            if (Shared.Count == 2)
            {
                // Задаем общее основание.
                var lowV1 = new double[] { Shared[0].X, Shared[0].Y, 0 };
                var lowV2 = new double[] { Shared[1].X, Shared[1].Y, 0 };
                var in0 = new double[] { NJ.X, NJ.Y, 0 };
                var in1 = new double[] { NJ.X, NJ.Y, 1 };
                var bor0 = new double[] { NI.X, NI.Y, 0 };
                var bor1 = new double[] { NI.X, NI.Y, 1 };

                // Считаем коэффициенты наклона и площади.
                var t11 = ABS(bor1, lowV1, in0);
                var t12 = ABS(bor0, lowV1, in1);
                var t21 = ABS(bor1, lowV2, in0);
                var t22 = ABS(bor0, lowV2, in1);

                // Интеграл
                return (t11[0] * t12[0] + t11[1] * t12[1]) * t11[2] + (t21[0] * t22[0] + t21[1] * t22[1]) * t21[2];
            }
            else return 0;
        }
        public static void Kaczmarz(double[,] A, double[] R, ref List<PointDP> points, double eps = 1.0e-6)
        {
            double s, norma, t;
            double[] x1 = new double[R.Length];
            double[] x = new double[R.Length];

            int i = 0;
            foreach (var pnt in points)
            {
                if (pnt.border) { continue; }
                x[i] = pnt.potential;
                i++;
            }

            s = norma = 1;
            while (s > eps * norma)
            {
                for (i = 0; i < R.Length; i++)
                    x1[i] = x[i];

                for (i = 0; i < R.Length; i++)
                {
                    s = 0;
                    norma = 0;

                    // Вычисление произведения матрицы A на вектор X.
                    for (int j = 0; j < R.Length; j++)
                    {
                        s += A[i, j] * x[j];
                        norma += A[i, j] * A[i, j];
                    }

                    // Невязка.
                    t = (R[i] - s) / norma;
                    if (double.IsInfinity(t))
                        t = double.MaxValue;

                    for (int k = 0; k < R.Length; k++)
                        x[k] += A[i, k] * t;
                }

                // Девиация
                s = 0;
                norma = 0;
                for (i = 0; i < R.Length; i++)
                {
                    s += (x[i] - x1[i]) * (x[i] - x1[i]);
                    norma += x[i] * x[i];
                }
                s = Math.Sqrt(s);
                norma = Math.Sqrt(norma);
            }

            i = 0;
            foreach (var pnt in points)
            {
                if (pnt.border) { continue; }
                pnt.potential = x[i];
                i++;
            }
        }
    }
}
