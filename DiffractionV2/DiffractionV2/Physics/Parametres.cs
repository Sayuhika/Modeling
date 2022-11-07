using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiffractionV2.Physics
{
    public class Parametres
    {
        public Parametres() 
        {
            ALPHA = 0.1;
            PML_Layers = 16;
            N = 2;
            TIME_START = 50.0;
            AMPLITUDE = 1;
            OMEGA = 0.05;
            TAU = 0.5;
        }
        public int WIDTH;
        public int HEIGHT;
        public double SourceX;
        public double SourceY;

        // Distance To Screen
        public double DTS;

        public int ApertureCount;
        public double ApertureW;
        public double ApertureS;
        // Reflection coefficient
        public double ALPHA;
        public int PML_Layers;
        // Degree of absorbent layer
        public int N;

        public double TIME_START;
        public double AMPLITUDE;
        public double OMEGA;
        public double TAU;
    }
}
