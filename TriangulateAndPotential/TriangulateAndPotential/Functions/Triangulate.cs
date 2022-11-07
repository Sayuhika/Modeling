using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TriangulateAndPotential
{
    class Triangulate
    {
        static public List<Triangle> BowyerWatsonTriangulate(List<PointDP> points, double kx = 1)
        {
            List<Triangle> triangles = Delone(CreateSuperstractCoords());
            List<PointDP> tempPoints = new List<PointDP>();
            List<Triangle> goodTriangles = new List<Triangle>();

            for (int i = 0; i < points.Count; i++)
            {
                tempPoints.Add(points[i]);

                for (int j = 0; j < triangles.Count; j++)
                {
                    if (triangles[j].IsDotInCC(points[i]))
                    {
                        tempPoints.Add(triangles[j].p1);
                        tempPoints.Add(triangles[j].p2);
                        tempPoints.Add(triangles[j].p3);
                    }
                    else
                    {
                        goodTriangles.Add(triangles[j]);
                    }
                }
                tempPoints = tempPoints.Distinct().ToList();
                triangles.Clear();
                DeloneStep(tempPoints, ref triangles, 0);
                triangles = triangles.Concat(goodTriangles).ToList();

                goodTriangles.Clear();
                tempPoints.Clear();
            }

            for (int j = 0; j < triangles.Count; j++)
            {
                if (triangles[j].p1.X < -kx || triangles[j].p1.X > kx || triangles[j].p1.Y < -1 || triangles[j].p1.Y > 1) continue;
                if (triangles[j].p2.X < -kx || triangles[j].p2.X > kx || triangles[j].p2.Y < -1 || triangles[j].p2.Y > 1) continue;
                if (triangles[j].p3.X < -kx || triangles[j].p3.X > kx || triangles[j].p3.Y < -1 || triangles[j].p3.Y > 1) continue;

                goodTriangles.Add(triangles[j]);
            }

            return goodTriangles;
        }

        static public void CreateNeighbours(ref  List<PointDP> points, ref List<Triangle> triangles)
        {
            // Устанавливаем соседей
            foreach (var tri in triangles)
            {
                if (!tri.p1.nPoints.Contains(tri.p2)) { tri.p1.nPoints.Add(tri.p2); }
                if (!tri.p1.nPoints.Contains(tri.p3)) { tri.p1.nPoints.Add(tri.p3); }
                if (!tri.p2.nPoints.Contains(tri.p1)) { tri.p2.nPoints.Add(tri.p1); }
                if (!tri.p2.nPoints.Contains(tri.p3)) { tri.p2.nPoints.Add(tri.p3); }
                if (!tri.p3.nPoints.Contains(tri.p1)) { tri.p3.nPoints.Add(tri.p1); }
                if (!tri.p3.nPoints.Contains(tri.p2)) { tri.p3.nPoints.Add(tri.p2); }

                tri.p1.nTriangles.Add(tri);
                tri.p2.nTriangles.Add(tri);
                tri.p3.nTriangles.Add(tri);
            }
            foreach (var pnt in points)
            {
                pnt.nPoints = pnt.nPoints.Distinct().ToList();
            }
        }
        static private List<PointDP> CreateSuperstractCoords()
        {
            List<PointDP> result = new List<PointDP>();

            result.Add(new PointDP(-2 , -2));
            result.Add(new PointDP(-2, 2));
            result.Add(new PointDP(2, -2));
            result.Add(new PointDP(2, 2));

            return result;
        }

        static private List<Triangle> Delone(List<PointDP> points)
        {
            // Делоне
            List<Triangle> triangles = new List<Triangle>();
            int size = points.Count;

            for (int i = 0; i < size; i++)
            {
                DeloneStep(points, ref triangles, i);
            }

            return triangles;
        }

        static private void DeloneStep(List<PointDP> points, ref List<Triangle> triangles, int i)
        {
            int size = points.Count;
            bool flag;

            for (int j = i + 1; j < size; j++)
            {
                for (int k = j + 1; k < size; k++)
                {
                    var tempTriangle = new Triangle();

                    tempTriangle.SetData(points[i], points[j], points[k]);
                    flag = true;

                    // Проверка наличия точек внутри круга
                    for (int v = 0; v < size; v++)
                    {
                        if (v == i || v == j || v == k) { continue; }

                        if (tempTriangle.IsDotInCC(points[v]))
                        {
                            flag = false;
                            break;
                        }
                    }

                    if (flag) triangles.Add(tempTriangle);
                }
            }
        }

        static public List<Triangle> CutFigure(List<Triangle> triangles, List<PointDP> figure)
        {
            List<Triangle> result = new List<Triangle>(triangles);

            result.RemoveAll(triangle =>
            {
                bool fp1 = false;
                bool fp2 = false;
                bool fp3 = false;

                foreach (var dot in figure)
                {
                    if ((!fp1) && (dot == triangle.p1))
                    {
                        fp1 = true;
                    }

                    if ((!fp2) && (dot == triangle.p2))
                    {
                        fp2 = true;
                    }
                    if ((!fp3) && (dot == triangle.p3))
                    {
                        fp3 = true;
                    }
                    if (fp1 && fp2 && fp3) { break; }
                }
                return fp1 && fp2 && fp3;
            });

            return result;
        }
    }
}
