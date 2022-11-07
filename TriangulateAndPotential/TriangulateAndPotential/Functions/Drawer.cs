using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TriangulateAndPotential
{
    class Drawer
    {

        static Pen PenRoki = new Pen(Color.FromArgb(48,48,48), 1);
        static Pen PenMegumin = new Pen(Color.Red, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };

        public static void DrawTriangles(Graphics Gr, List<Triangle> triangles, double k = 1000, double kx = 0.5)
        {
            // Рисуем описанные радиусы
            //for (int i = 0; i < triangles.Count; i++)
            //{
            //    Gr.DrawEllipse(PenMegumin, triangles[i].cc.X * k - triangles[i].r * k, triangles[i].cc.Y * k - triangles[i].r * k,
            //          triangles[i].r * k + triangles[i].r * k, triangles[i].r * k + triangles[i].r * k);
            //}
            // Рисуем треугольники
            for (int i = 0; i < triangles.Count; i++)
            {
                Gr.DrawLine(PenRoki, (float)((triangles[i].p1.X + kx) * k), (float)((triangles[i].p1.Y + 0.5f) * k), (float)((triangles[i].p2.X + kx) * k), (float)((triangles[i].p2.Y + 0.5f) * k));
                Gr.DrawLine(PenRoki, (float)((triangles[i].p2.X + kx) * k), (float)((triangles[i].p2.Y + 0.5f) * k), (float)((triangles[i].p3.X + kx) * k), (float)((triangles[i].p3.Y + 0.5f) * k));
                Gr.DrawLine(PenRoki, (float)((triangles[i].p3.X + kx) * k), (float)((triangles[i].p3.Y + 0.5f) * k), (float)((triangles[i].p1.X + kx) * k), (float)((triangles[i].p1.Y + 0.5f) * k));
            }

            Gr.Flush();
        }

        public static void DrawPhysic(Graphics Gr, List<PointDP> pointPotencial, double k = 1000, double kx = 0.5f)
        {
            SolidBrush Darkness = new SolidBrush(Color.Yellow);
            float kpxl = 2;
            double maxP = pointPotencial.Max(pt => Math.Abs(pt.potential));

            for (int i = 0; i < pointPotencial.Count; i++)
            {
                if (double.IsNaN(pointPotencial[i].potential))
                {
                    Darkness.Color = Color.Black;
                }
                else
                if (pointPotencial[i].potential >= 0)
                {
                    int kc = (int)(255 * pointPotencial[i].potential / maxP);
                    Darkness.Color = Color.FromArgb(kc, 0, 0);
                }
                else
                {
                    int kc = (int)(-255 * pointPotencial[i].potential / maxP);
                    Darkness.Color = Color.FromArgb(0, 0, kc);
                }
               
                Gr.FillEllipse(Darkness, (float)((pointPotencial[i].X + kx) * k - kpxl), (float)((pointPotencial[i].Y + 0.5f) * k - kpxl), 2 * kpxl, 2 * kpxl);
            }

            Gr.Flush();
        }

        public static void DrawIsolines(Graphics Gr, Dictionary<double, List<PointDP>> isolines, double k = 1000, double kx = 0.5f)
        {
            Pen Darkness = new Pen(Color.Yellow);

            double maxP = isolines.Max(pt => Math.Abs(pt.Key));

            foreach (var line in isolines)
            {
                if (double.IsNaN(line.Key))
                {
                    Darkness.Color = Color.White;
                }
                else
                if (line.Key >= 0)
                {
                    int kc = (int)(255 * line.Key / maxP);
                    Darkness.Color = Color.FromArgb(kc, 0, 0);
                }
                else
                {
                    int kc = (int)(-255 * line.Key / maxP);
                    Darkness.Color = Color.FromArgb(0, 0, kc);
                }
                for (int i = 0; i < line.Value.Count-1; i+=2)
                {
                    Gr.DrawLine(Darkness, (float)((line.Value[i].X + kx) * k), (float)((line.Value[i].Y + 0.5f) * k) , (float)((line.Value[i+1].X + kx) * k), (float)((line.Value[i+1].Y + 0.5f) * k));
                }
            }

            Gr.Flush();
        }

        public static void DrawFieldLines(Graphics Gr, List<List<PointF>> fieldlines, double k = 1000, double kx = 0.5f)
        {
            Pen Darkness = new Pen(Color.Green);
            Random rand = new Random();
            foreach (var line in fieldlines)
            {
                for (int i = 0; i < line.Count - 1; i ++)
                {
                    //int color = rand.Next(255);
                    //Darkness.Color = Color.FromArgb( color, color, color);
                    Gr.DrawLine(Darkness, (float)((line[i].X + kx) * k), (float)((line[i].Y + 0.5f) * k), (float)((line[i + 1].X + kx) * k), (float)((line[i + 1].Y + 0.5f) * k));
                }
            }
            Gr.Flush();
        }
    }
}
