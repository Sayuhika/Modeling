using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Windows.Threading;

// https://github.com/petrolingus/two-source-interference/tree/master/src/main/java/me/petrolingus/modsys/twosourceinterference/core

using DiffractionV2.Drawing;

namespace DiffractionV2.Physics
{
    public class Algorithm:DispatcherTimer
    {
        Parametres Prs;

        private Cell[,] cells;
        double[,] ihx;
        double[,] ihy;
        double[] gi2;
        double[] gi3;
        double[] fi1;
        double[] fi2;
        double[] fi3;

        private double time;
        double pulse;

        public void Restart(Parametres Prs)
        {
            this.Prs = Prs;

            cells = new Cell[Prs.WIDTH, Prs.HEIGHT];

            ihx = new double[Prs.WIDTH, Prs.HEIGHT];
            ihy = new double[Prs.WIDTH, Prs.HEIGHT];

            gi2 = new double[Prs.WIDTH];
            gi3 = new double[Prs.WIDTH];

            fi1 = new double[Prs.WIDTH];
            fi2 = new double[Prs.WIDTH];
            fi3 = new double[Prs.HEIGHT];

            for (int j = 0; j < Prs.HEIGHT; j++)
            {
                for (int i = 0; i < Prs.WIDTH; i++)
                {
                    cells[i, j] = new Cell(1.0);
                }
            }

            //Calculate the PML parameters
            Parallel.For(0, Prs.WIDTH, i => 
            {
                gi2[i] = 1;
                gi3[i] = 1;
                fi2[i] = 1;
            });
            Parallel.For(0, Prs.HEIGHT, i =>
            {
                fi3[i] = 1;
            });

            for (int i = 0; i < Prs.PML_Layers; i++)
            {
                double xnum = Prs.PML_Layers - i;
                double xxn = xnum / Prs.PML_Layers;
                double xn = Prs.ALPHA * Math.Pow(xxn, Prs.N);

                gi2[i] = 1.0 / (1.0 + xn);
                gi2[Prs.WIDTH - 1 - i] = 1.0 / (1.0 + xn);

                gi3[i] = (1.0 - xn) / (1.0 + xn);
                gi3[Prs.WIDTH - 1 - i] = (1.0 - xn) / (1.0 + xn);

                xxn = (xnum - 0.5) / Prs.PML_Layers;
                xn = Prs.ALPHA * Math.Pow(xxn, Prs.N);

                fi1[i] = xn;
                fi1[Prs.WIDTH - 2 - i] = xn;

                fi2[i] = 1.0 / (1.0 + xn);
                fi2[Prs.WIDTH - 2 - i] = 1.0 / (1.0 + xn);

                fi3[i] = (1.0 - xn) / (1.0 + xn);
                fi3[Prs.WIDTH - 2 - i] = (1.0 - xn) / (1.0 + xn);
            }
        }

        public void next()
        {
            double ddt = Prs.TAU;

            // Dz
            for (int j = 1; j < Prs.HEIGHT; j++)
            {
                for (int i = 1; i < Prs.WIDTH; i++)
                {
                    double a = gi3[i] * gi3[j] * cells[i,j].dz;
                    cells[i,j].dz = a + gi2[i] * gi2[j] * ddt * (cells[i,j].hy - cells[i - 1,j].hy - cells[i,j].hx + cells[i,j - 1].hx);
                }
            }

            time = time + ddt;
            pulse = (1.0 / (1.0 + Math.Exp(-0.1 * (time - Prs.TIME_START)))) * Math.Sin(2 * Math.PI * Prs.OMEGA * time) * Prs.AMPLITUDE;


            // Coords
            cells[(int)(Prs.SourceY * Prs.WIDTH * 0.01),(int)(Prs.SourceX * Prs.HEIGHT * 0.5 * 0.01)].dz = pulse;

            for (int i = 0; i < Prs.WIDTH; i++)
            {
                cells[i, (int)(Prs.HEIGHT * 0.5)].isPlate = true;
            }
            for (int i = 0; i < Prs.ApertureCount; i++)
            {
                double y = ((Prs.ApertureW + Prs.ApertureS) * (Prs.ApertureCount - 1) + Prs.ApertureW + 100) * 0.5;

                for (int j = (int)((y- Prs.ApertureW - (Prs.ApertureW + Prs.ApertureS) * i) * 0.01 * Prs.WIDTH); j <= (int)((y - (Prs.ApertureW + Prs.ApertureS) * i) * 0.01 * Prs.WIDTH); j++)
                {
                    if (j > Prs.WIDTH) break;

                    cells[j, (int)(Prs.HEIGHT * 0.5)].isPlate = false;
                }
            }    

            // Ez
            for (int j = 0; j < Prs.HEIGHT; j++)
            {
                for (int i = 0; i < Prs.WIDTH; i++)
                {
                    cells[i,j].ez = cells[i,j].ga * (cells[i,j].isPlate?0:cells[i,j].dz);
                }
            }

            // Hx
            for (int j = 0; j < Prs.HEIGHT - 1; j++)
            {
                for (int i = 0; i < Prs.WIDTH; i++)
                {
                    double curl_e = cells[i,j].ez - cells[i,j + 1].ez;
                    ihx[i,j] = ihx[i,j] + fi1[i] * curl_e;
                    cells[i,j].hx = fi3[j] * cells[i,j].hx + fi2[j] * ddt * (curl_e + ihx[i,j]);
                }
            }

            // Hy
            for (int j = 0; j < Prs.HEIGHT; j++)
            {
                for (int i = 0; i < Prs.WIDTH - 1; i++)
                {
                    double curl_e = cells[i + 1,j].ez - cells[i,j].ez;
                    ihy[i,j] = ihy[i,j] + fi1[j] * curl_e;
                    cells[i,j].hy = fi3[i] * cells[i,j].hy + fi2[i] * ddt * (curl_e + ihy[i,j]);
                }
            }
        }

        public double [] GetScreenStatus() 
        {
            double[] result = new double[Prs.WIDTH];
            for (int i = 0; i < Prs.WIDTH; i++)
            {
                result[i] = cells[i, (int)(Prs.HEIGHT * (0.5 + Prs.DTS * 0.005))].getValue();
            }
            return result;
        }
        public ComplexImage getValues()
        {
            ComplexImage data = new ComplexImage() { Data = new Complex[Prs.WIDTH, Prs.HEIGHT], MaxV = 1};
            for (int i = 1; i < Prs.WIDTH - 1; i++)
            {
                for (int j = 1; j < Prs.HEIGHT - 1; j++)
                {
                    if (cells[i,j].isPlate)
                    {
                        data.Data[i,j] = -1;
                    }
                    else
                    {
                        data.Data[i,j] = cells[i,j].getValue();
                    }
                }
            }
            for (int i = 0; i < Prs.WIDTH; i++)
            {
               data.Data[i, (int)(Prs.HEIGHT * (0.5 + Prs.DTS * 0.005))] = -1;
            }
            return data;
        }
    }
}
