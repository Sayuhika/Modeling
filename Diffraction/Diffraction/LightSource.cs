using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Numerics;

namespace Diffraction
{
    class LightSource
    {
        public Complex amplitude;
        public double frequency;
        public Vector position;
        public double k { get => 2 * Math.PI * frequency * 1e12 / 299792458; }
        public Complex WaveValue(Vector target)
        {

            double r = (position - target).Length;
            if (r < 1) r = 1;
            return amplitude * Complex.Exp(Complex.ImaginaryOne * (-k * r))  / r;
        }
    }
}
