using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TriangulateAndPotential
{
    public class PointDP
    {
        public double X;
        public double Y;
        public double potential;
        public List<Triangle> nTriangles;
        public List<PointDP> nPoints;
        public bool border = false;

        public PointDP(double X = 0, double Y = 0, double potential = 0, bool border = false)
        {
            this.X = X;
            this.Y = Y;
            this.potential = potential;
            this.border = border;
            nTriangles = new List<Triangle>();
            nPoints = new List<PointDP>();
        }
    }
}
