using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TriangulateAndPotential
{
    class Model
    {
        public List<PointDP> grid { get; protected set; }
        public List<PointDP> fDots { get; protected set; }
        public List<PointDP> allDots { get; protected set; }
        public Model(int Xmax, int Ymax, double R, double D, double fL)
        {
            grid = new List<PointDP>();
            fDots = new List<PointDP>();
            Random rand = new Random();
            double step = 1.0f / (Ymax);
            double L = fL * 0.5f;

            // Создаем основную сетку
            for (int i = -Xmax / 2; i <= Xmax / 2; i++)
            {
                for (int j = -Ymax / 2; j <= Ymax / 2; j++)
                {
                    double x = step * i;
                    double y = step * j;

                    double modkoff = 0.05f;
                    double Rkoff1 = 1 - modkoff;
                    double Rkoff2 = 1 + modkoff;

                    if (x <- L + 0.0011f)
                    {
                        x -=- L;
                        if ((x * x + y * y > R * R * Rkoff1) && (x * x + y * y < (R + D) * (R + D) * Rkoff2)) { continue; };
                    }

                    x = step * i;
                    if (x > L - 0.0011f)
                    {
                        x -= L;
                        if ((x * x + y * y > R * R * Rkoff1) && (x * x + y * y < (R + D) * (R + D) * Rkoff2)) { continue; };
                    }

                    var point = new PointDP(step * i + rand.NextDouble() * 0.0001f, step * j + rand.NextDouble() * 0.0001f);
                    point.border = (i == Xmax / 2) || (i == -Xmax / 2) || (j == Ymax / 2) || (j == -Ymax / 2);
                    grid.Add(point);
                }
            }

            // Создаем точки трубы по X
            for (int i = -Xmax / 2; i < Xmax / 2; i++)
            {
                double x = step * i;
                double tempX;

                if (x < - L)
                {
                    tempX = x - (- L);

                    if (R * R - tempX * tempX >= 0)
                    {
                        fDots.Add(new PointDP(x,Math.Sqrt(R * R - tempX * tempX)));
                        fDots.Add(new PointDP(x, -Math.Sqrt(R * R - tempX * tempX)));
                    }
                    if ((R + D) * (R + D) - tempX * tempX >= 0)
                    {
                        fDots.Add(new PointDP(x, Math.Sqrt((R + D) * (R + D) - tempX * tempX)));
                        fDots.Add(new PointDP(x, -Math.Sqrt((R + D) * (R + D) - tempX * tempX)));
                    }
                }

                if (x > L)
                {
                    tempX = x - (L);

                    if (R * R - tempX * tempX >= 0)
                    {
                        fDots.Add(new PointDP(x, Math.Sqrt(R * R - tempX * tempX)));
                        fDots.Add(new PointDP(x, -Math.Sqrt(R * R - tempX * tempX)));
                    }
                    if ((R + D) * (R + D) - tempX * tempX >= 0)
                    {
                        fDots.Add(new PointDP(x, Math.Sqrt((R + D) * (R + D) - tempX * tempX)));
                        fDots.Add(new PointDP(x, - Math.Sqrt((R + D) * (R + D) - tempX * tempX)));
                    }
                }
            }

            // Создаем точки трубы по Y
            for (int i = -Ymax / 2; i < Ymax / 2; i++)
            {
                double y = step * i;
                double tempY;

                tempY = y;

                if (R * R - tempY * tempY >= 0)
                {
                    fDots.Add(new PointDP((- L) - Math.Sqrt(R * R - tempY * tempY), y));
                }
                if ((R + D) * (R + D) - tempY * tempY >= 0)
                {
                    fDots.Add(new PointDP((- L) - Math.Sqrt((R + D) * (R + D) - tempY * tempY), y));
                }

                if (R * R - tempY * tempY >= 0)
                {
                    fDots.Add(new PointDP((L) + Math.Sqrt(R * R - tempY * tempY), y));
                }
                if ((R + D) * (R + D) - tempY * tempY >= 0)
                {
                    fDots.Add(new PointDP((L) + Math.Sqrt((R + D) * (R + D) - tempY * tempY), y));
                }
                if ((y < (R + D)) && (y > (R)))
                {
                    fDots.Add(new PointDP(- L, y));
                    fDots.Add(new PointDP(L, y));
                }
                if ((y > (- R - D)) && (y < (- R)))
                {
                    fDots.Add(new PointDP(- L, y));
                    fDots.Add(new PointDP(L, y));
                }
            }

            // Наводим красоту (удаляем близкие точки на трубе)
            for (int i = 0; i < fDots.Count; i++)
            {
                for (int j = i + 1; j < fDots.Count; j++)
                {
                    if (((fDots[j].X - fDots[i].X) * (fDots[j].X - fDots[i].X) + (fDots[j].Y - fDots[i].Y) * (fDots[j].Y - fDots[i].Y)) < 0.25 * step * step)
                    {
                        fDots.RemoveAt(j);
                        j--;
                    }
                }
            }

            allDots = grid.Concat(fDots).ToList();
        }
    }
}
