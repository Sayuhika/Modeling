using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiffractionV2.Physics
{
    public class Cell
    {
        public double dz;
        public double ez;
        public double ga;
        public double hx;
        public double hy;
        public bool isPlate = false;

        public Cell(double ga)
        {
            this.ga = ga;
        }

        public double getValue()
        {
            return Math.Sqrt(hx * hx + hy * hy) / 2.0;
        }
    }
}
