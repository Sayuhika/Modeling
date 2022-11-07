using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Media.Media3D;

namespace TriangulateAndPotential
{
    class Physic
    {
        private BackgroundWorker PhysicProc;

        public List<PointDP> points;
        public List<Triangle> triangles;
        public Dictionary<double, List<PointDP>>  Isolines;
        public List<List<PointF>> FieldLines;
        public Physic(RunWorkerCompletedEventHandler Completed_Mesh, RunWorkerCompletedEventHandler Completed_PowerLines, ProgressChangedEventHandler Progress_PowerLines)
        {
            PhysicProc = new BackgroundWorker() { WorkerSupportsCancellation = true };
            PhysicProc.DoWork += Calculate;
            PhysicProc.RunWorkerCompleted += Completed_Mesh;
        }
        public void CalculateAsync()
        {
            PhysicProc.RunWorkerAsync();
        }
        public void Cancel()
        {
            if (PhysicProc.IsBusy)
                PhysicProc.CancelAsync();
        }

        public static Dictionary<double, List<PointDP>> CalculateIsolines(double[] Z, List<PointDP> points, List<Triangle> triangles)
        {
            Dictionary<double, List<PointDP>>  Isolines = new Dictionary<double, List<PointDP>>();

            for (int z = 0; z < Z.Length; z++)
            {
                var isol = new List<PointDP>();

                foreach (var tri in triangles)
                {
                    var Order = new List<PointDP> { tri.p1, tri.p2, tri.p3 };

                    Order.Sort((n1, n2) => n1.potential.CompareTo(n2.potential));

                    if ((Z[z] >= Order[0].potential) && (Z[z] <= Order[2].potential))
                    {
                        if (Z[z] >= Order[1].potential)
                            isol.Add(GetIsoPoint(Z[z], Order[1], Order[2]));
                        else isol.Add(GetIsoPoint(Z[z], Order[0], Order[1]));

                        isol.Add(GetIsoPoint(Z[z], Order[0], Order[2]));
                    }
                }
                Isolines.Add(Z[z], isol);
            }

            return Isolines;
        }

        private static Func<double, PointDP, PointDP, PointDP> GetIsoPoint = (T, K1, K2) => {
            if (double.IsPositiveInfinity(K1.potential)) return new PointDP(K1.X, K1.Y);
            if (double.IsPositiveInfinity(K2.potential)) return new PointDP(K2.X, K2.Y);
            double Ratio = (K2.potential - T) / (K2.potential - K1.potential);
            if (double.IsNaN(Ratio)) { Ratio = 0; };
            double x = K2.X + (K1.X - K2.X) * Ratio;
            double y = K2.Y + (K1.Y - K2.Y) * Ratio;
            return new PointDP(x, y);
        };

        public static List<List<PointF>> CalculateFieldLines(List<PointDP> points, List<Triangle> triangles, float k = 0.01f)
        {
            var FieldLines = new System.Collections.Concurrent.ConcurrentBag<List<PointF>>();
            List<PointDP> startPoints = points.Where(pnt => pnt.border && pnt.potential != 0).ToList();

            Parallel.ForEach(startPoints, sPnt =>
            {
                List<PointF> FieldLine = new List<PointF>();
                FieldLine.Add(new PointF((float)sPnt.X, (float)sPnt.Y));

                float gX = 0;
                float gY = 0;
                float gI = 0;
                foreach (var tri in sPnt.nTriangles)
                {
                    var g = Grad(tri);
                    gX += g.X;
                    gY += g.Y;
                    gI++;
                }
                gX = gX / gI;
                gY = gY / gI;
                gI = k / (float)Math.Sqrt(gX * gX + gY * gY);
                FieldLine.Add(new PointF(FieldLine.Last().X + gX * gI, FieldLine.Last().Y + gY * gI));

                Triangle triangle = sPnt.nTriangles.Find(tri => tri.Contains(new PointDP(FieldLine.Last().X, FieldLine.Last().Y)));

                while (triangle != null)
                {
                    PointF grad = Grad(triangle);
                    gI = k / (float)Math.Sqrt(grad.X * grad.X + grad.Y * grad.Y);
                    FieldLine.Add(new PointF(FieldLine.Last().X + grad.X * gI, FieldLine.Last().Y + grad.Y * gI));
                    if (grad.X * grad.X + grad.Y * grad.Y < float.Epsilon) { break; }
                    float distance1 = Triangle.Distance(new PointF((float)triangle.p1.X, (float)triangle.p1.Y), FieldLine.Last());
                    float distance2 = Triangle.Distance(new PointF((float)triangle.p2.X, (float)triangle.p2.Y), FieldLine.Last());
                    float distance3 = Triangle.Distance(new PointF((float)triangle.p3.X, (float)triangle.p3.Y), FieldLine.Last());
                    PointDP searchPnt = triangle.p1;
                    if (distance2 < distance1 && distance2 < distance3) { searchPnt = triangle.p2; };
                    if (distance3 < distance1 && distance3 < distance2) { searchPnt = triangle.p3; };
                    try
                    {
                        triangle = searchPnt.nTriangles.First(tri => tri.Contains(new PointDP(FieldLine.Last().X, FieldLine.Last().Y)));
                    }
                    catch (Exception) { triangle = null; }
                }
                FieldLine.RemoveAt(FieldLine.Count-1);

                FieldLines.Add(FieldLine);
            });

            Parallel.ForEach(startPoints, sPnt =>
            {
                List<PointF> FieldLine = new List<PointF>();
                FieldLine.Add(new PointF((float)sPnt.X, (float)sPnt.Y));

                float gX = 0;
                float gY = 0;
                float gI = 0;
                foreach (var tri in sPnt.nTriangles)
                {
                    var g = Grad(tri);
                    gX += g.X;
                    gY += g.Y;
                    gI++;
                }
                gX = gX / gI;
                gY = gY / gI;
                gI = k / (float)Math.Sqrt(gX * gX + gY * gY);
                FieldLine.Add(new PointF(FieldLine.Last().X - gX * gI, FieldLine.Last().Y - gY * gI));

                Triangle triangle = sPnt.nTriangles.Find(tri => tri.Contains(new PointDP(FieldLine.Last().X, FieldLine.Last().Y)));

                while (triangle != null)
                {
                    PointF grad = Grad(triangle);
                    gI = k / (float)Math.Sqrt(grad.X * grad.X + grad.Y * grad.Y);
                    FieldLine.Add(new PointF(FieldLine.Last().X - grad.X * gI, FieldLine.Last().Y - grad.Y * gI));
                    if (grad.X * grad.X + grad.Y * grad.Y < float.Epsilon) { break; }
                    float distance1 = Triangle.Distance(new PointF((float)triangle.p1.X, (float)triangle.p1.Y), FieldLine.Last());
                    float distance2 = Triangle.Distance(new PointF((float)triangle.p2.X, (float)triangle.p2.Y), FieldLine.Last());
                    float distance3 = Triangle.Distance(new PointF((float)triangle.p3.X, (float)triangle.p3.Y), FieldLine.Last());
                    PointDP searchPnt = triangle.p1;
                    if (distance2 < distance1 && distance2 < distance3) { searchPnt = triangle.p2; };
                    if (distance3 < distance1 && distance3 < distance2) { searchPnt = triangle.p3; };
                    try
                    {
                        triangle = searchPnt.nTriangles.First(tri => tri.Contains(new PointDP(FieldLine.Last().X, FieldLine.Last().Y)));
                    }
                    catch (Exception) { triangle = null; }
                }
                FieldLine.RemoveAt(FieldLine.Count - 1);

                FieldLines.Add(FieldLine);
            });

            return FieldLines.ToList();
        }

        public static PointF Grad(Triangle triangle)
        {
            Vector3D norm = Vector3D.CrossProduct(
                new Vector3D(
                    triangle.p1.X - triangle.p2.X, 
                    triangle.p1.Y - triangle.p2.Y, 
                    triangle.p1.potential - triangle.p2.potential), 
                new Vector3D(
                    triangle.p1.X - triangle.p3.X, 
                    triangle.p1.Y - triangle.p3.Y, 
                    triangle.p1.potential - triangle.p3.potential));
            if (norm.Z < 0) { norm *= -1; } // Разворачиваем вектор
            Vector3D tang = Vector3D.CrossProduct(new Vector3D(0, 0, 1), norm);
            Vector3D grad = Vector3D.CrossProduct(tang, norm);

            return new PointF((float)grad.X, (float)grad.Y);
        } 
        private void Calculate(object sender, DoWorkEventArgs e)
        {
            double[,] A;
            double[] R;

            // Формирование системы уравнений.
            //GalerkinOld.ConstructModel(points, out A, out R);
            // Решение системы уравнений.
            // GalerkinOld.Kaczmarz(A, R, ref points, 1e-9);

            Galerkin.GetAR(points, triangles, out A, out R);

            var result = Accord.Math.Matrix.Decompose(A).Solve(R);
            int i = 0;
            foreach (var pnt in points)
            {
                if (pnt.border) { continue; }
                pnt.potential = result[i];
                i++;
            }
            double[] Z = new double[201];
            double max = points.Max(p=>Math.Abs(p.potential));
            int kZ = Z.Length / 2;
            for (i = -kZ; i <= kZ; i++)
            {
                Z[i + kZ] = max * i/ kZ;
            }
            Isolines = CalculateIsolines(Z, points, triangles);
            FieldLines = CalculateFieldLines(points, triangles, (float) (0.5 / Math.Sqrt(points.Count)));

            e.Result = 1;
        }
    }
}
