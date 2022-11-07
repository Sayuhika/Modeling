using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DoublePendulum
{
    class Drawer
    {
        public static float w = 0.001f;
        public static Pen RokiPen = new Pen(Color.Black, w);
        public static Pen Pendalf = new Pen(Color.Gray, w);

        public static void DrawDoublePendulum(Graphics DPgr, double[] coords)
        {
            float radius = 25 * w;

            // Рисуем
            DPgr.Clear(Color.White);
            DPgr.DrawLine(Pendalf, 0, 0, (float)coords[0], (float)coords[1]);
            DPgr.DrawLine(Pendalf, (float)coords[0], (float)coords[1], (float)coords[2], (float)coords[3]);
            DPgr.FillEllipse(Brushes.Aqua, (float)coords[0] - radius, (float)coords[1] - radius, 2 * radius, 2 * radius);
            DPgr.DrawEllipse(RokiPen, (float)coords[0] - radius, (float)coords[1] - radius, 2 * radius, 2 * radius);
            DPgr.FillEllipse(Brushes.Aqua, (float)coords[2] - radius, (float)coords[3] - radius, 2 * radius, 2 * radius);
            DPgr.DrawEllipse(RokiPen, (float)coords[2] - radius, (float)coords[3] - radius, 2 * radius, 2 * radius);

            // Не рисуем
            DPgr.Flush();
        }
        public static void DrawPhasePath(Graphics PPHgr, List<PointF> coords)
        {
            PPHgr.Clear(Color.White);
            PPHgr.DrawCurve(RokiPen, coords.ToArray());
            PPHgr.Flush();
        }
        public static void DrawPhasePortrait(Graphics PPTgr, List<List<PointF>> coords)
        {
            PPTgr.Clear(Color.White);
            foreach (var path in coords)
            {
                PPTgr.DrawCurve(RokiPen, path.ToArray());
            }
            PPTgr.Flush();
        }
    }
}
