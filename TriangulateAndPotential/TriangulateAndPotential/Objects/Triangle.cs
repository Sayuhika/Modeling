using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TriangulateAndPotential
{
    public class Triangle
    {
        // Triangle coords data

        /// <summary>
        /// Is first of triangle coords.
        /// </summary>
        public PointDP p1 { get; protected set; }
        /// <summary>
        /// Is second of triangle coords.
        /// </summary>
        public PointDP p2 { get; protected set; }
        /// <summary>
        /// Is third of triangle coords.
        /// </summary>
        public PointDP p3 { get; protected set; }

        // Сircumscribed circle data

        /// <summary>
        /// Is radius circumscribed circle of this triangle.
        /// </summary>
        public double r { get; protected set; }
        /// <summary>
        /// Is circumscribed circle centre coords of this triangle.
        /// </summary>
        public PointDP cc { get; protected set; }

        private void CreateCCData()
        {
            double z1, z2, z3, zx, zy, z;

            z1 = p1.X * p1.X + p1.Y * p1.Y;
            z2 = p2.X * p2.X + p2.Y * p2.Y;
            z3 = p3.X * p3.X + p3.Y * p3.Y;
            zx = (p1.Y - p2.Y) * z3 + (p2.Y - p3.Y) * z1 + (p3.Y - p1.Y) * z2;
            zy = (p1.X - p2.X) * z3 + (p2.X - p3.X) * z1 + (p3.X - p1.X) * z2;
            z = 2 * ((p1.X - p2.X) * (p3.Y - p1.Y) - (p1.Y - p2.Y) * (p3.X - p1.X));

            // Circumscribed circle centre coords

            cc = new PointDP(-zx / z, zy / z);

            // Circumscribed circle radius
            r = Distance(cc, p1);
        }

        ///<summary>
        ///Set new triangle coords with using PointF[3].
        ///</summary>
        ///<param name="coords">First three elements of PointF[] varible will be using to set new coords of triangle. It's recommended to use Point[3] varible.</param>
        ///<param name="newr">Circumscribed circle radius. Skip this varible to create newcc and newr automaticly.</param>
        ///<param name="newcc">Circumscribed circle coords of this triangle.</param>
        public void SetData(PointDP[] coords) 
        {
            p1 = coords[1];
            p2 = coords[2];
            p3 = coords[3];

            CreateCCData();
        }

        ///<summary>
        ///Set new triangle coords with using 3 PointF variables
        ///</summary>
        ///<param name="newp1">First of triangle coords.</param>
        ///<param name="newp2">Second of triangle coords.</param>
        ///<param name="newp3">Third of triangle coords.</param>
        ///<param name="newr">Circumscribed circle radius. Skip this varible to create newcc and newr automaticly.</param>
        ///<param name="newcc">Circumscribed circle coords of this triangle.</param>
        public void SetData(PointDP newp1, PointDP newp2, PointDP newp3) 
        {
            p1 = newp1;
            p2 = newp2;
            p3 = newp3;

            CreateCCData();
        }

        public bool Contains(PointDP pt)
        {
            double ka = (p1.X - pt.X) * (p2.Y - p1.Y) - (p2.X - p1.X) * (p1.Y - pt.Y);
            double kb = (p2.X - pt.X) * (p3.Y - p2.Y) - (p3.X - p2.X) * (p2.Y - pt.Y);
            double kc = (p3.X - pt.X) * (p1.Y - p3.Y) - (p1.X - p3.X) * (p3.Y - pt.Y);
            return ka >= 0 && kb >= 0 && kc >= 0 || ka <= 0 && kb <= 0 && kc <= 0;
        }

        public bool IsDotInCC(PointDP point)
        {
            return Distance(cc, point) < r; 
        }
        public static double Distance(PointDP p1, PointDP p2)
        {
            return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }
        public static float Distance(PointF p1, PointF p2)
        {
            return (float)Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }
        public double GetArea()
        {
            return Distance(p1, p2) * Distance(p2, p3) * Distance(p3, p1) * 0.25f / r;
        }
    }
}