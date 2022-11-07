using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Drawing;

namespace TriangulateAndPotential
{
    public class Mesh
    {
		private BackgroundWorker MeshProc;
		private BackgroundWorker PowerLinesProc;

		public List<List<PointF>> PowerLines { get; private set; }
		public Dictionary<float, List<PointF>> Isolines { get; private set; }
        public List<DwLaP> points; 

		public Mesh(
			RunWorkerCompletedEventHandler Completed_Mesh, 
			RunWorkerCompletedEventHandler Completed_PowerLines, 
			ProgressChangedEventHandler Progress_PowerLines)
		{
			MeshProc = new BackgroundWorker() { WorkerSupportsCancellation = true };
			MeshProc.DoWork += Calculate;
			MeshProc.RunWorkerCompleted += Completed_Mesh;

			PowerLinesProc = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
			PowerLinesProc.DoWork += GetPowerLines;
			PowerLinesProc.RunWorkerCompleted += Completed_PowerLines;
			PowerLinesProc.ProgressChanged += Progress_PowerLines;
		}

		public void CalculateAsync() {
			MeshProc.RunWorkerAsync();
		}

		public void GetPowerLinesAsync(float df) {
			PowerLines = new List<List<PointF>>();
			PowerLinesProc.RunWorkerAsync(df);
		}

		public void Cancel() {
			if (MeshProc.IsBusy)
				MeshProc.CancelAsync();
			if (PowerLinesProc.IsBusy)
				PowerLinesProc.CancelAsync();
		}

		private Triangle GetTriCage(PointF pt, DwLaP center)
		{
			foreach (Triangle tri in center.nTriangles)
				if (tri.Contains(pt)) return tri;

			return null;
		}

		private static float IntersectLine(PointF C, float Fx, float Fy, PointF A, PointF B)
		{
            float bak = (B.Y - A.Y) / (B.X - A.X);
            float line = bak * (C.X - A.X) - C.Y;
            float ray = Fy - bak * Fx;
			return line / ray;
		}

		private static PointF IntersectTri(PointF O, float Fx, float Fy, Triangle tri, float offset)
		{
			var ks = new List<float>();

			ks.Add(IntersectLine(O, Fx, Fy, tri.p1, tri.p2));
			ks.Add(IntersectLine(O, Fx, Fy, tri.p2, tri.p3));
			ks.Add(IntersectLine(O, Fx, Fy, tri.p3, tri.p1));

            float k = ks.OrderBy((v) => v).First((v) => v > 0.0);

            return new PointF(O.X - Fx * k * (1.0f + offset), O.Y - Fy * k * (1.0f + offset));
		}

		private void GetPowerLines(object sender, DoWorkEventArgs e)
        {
			var rand = new Random();
			for (int i = 0; i < points.Count; i++)
			{
				if (!points[i].Links.Any((n2) => n2.Bored)) continue;

				var pline = new List<PointF>();
				// Начало силовой линии:
				pline.Add(
					new PointF(
                        points[i].Pt.X + (rand.NextDouble() - 0.5) / Math.Sqrt(points.Count),
                        points[i].Pt.Y + (rand.NextDouble() - 0.5) / Math.Sqrt(points.Count)
						)
					);

				// Определение первого рабочего треугольника:
				Trimap triCage = GetTriCage(pline.Last(), points[i]);
				if (triCage == null) continue;

				do {
					if (PowerLinesProc.CancellationPending) { e.Cancel = true; return; }
					var lastp = pline.Last();
					if (!triCage.tri.Contains(lastp))
					{   // Переход к новому рабочему треугольнику:
						float ar = Tri.PDist2(lastp, triCage.A.Pt);
						float br = Tri.PDist2(lastp, triCage.B.Pt);
						float cr = Tri.PDist2(lastp, triCage.C.Pt);
						DwLaP center = triCage.A;
						if (br <= ar && br <= cr)
							center = triCage.B;
						else if (cr <= ar && cr <= br)
							center = triCage.C;
						triCage = GetTriCage(lastp, center);
					}
					if (triCage == null) break;

					// Смещение частицы на градиент потенциала:
					float fa, fb;
					Trimap.AB(triCage.A, triCage.B, triCage.C, out fa, out fb);
					if (fa * fa + fb * fb < 1e-16) break;

					if (PowerLinesProc.CancellationPending) { e.Cancel = true; return; }
					pline.Add(IntersectTri(lastp, fa, fb, triCage.tri, 0.05));
				}	// Остановиться, когда точка выйдет за границы рабочей области:
				while (BorderShape.Compliance(pline.Last(), Constraints));

				pline.RemoveAt(pline.Count - 1);
				PowerLines.Add(pline);
				PowerLinesProc.ReportProgress((int)(100.0 * (i + 1) / points.Count));
			}
            e.Result = PowerLines;
        }
    }
}
