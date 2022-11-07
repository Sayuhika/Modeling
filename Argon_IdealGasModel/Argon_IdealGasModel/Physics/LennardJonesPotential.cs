#pragma warning disable IDE1006	// Названия с маленькой буквы

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Argon_IdealGasModel.Physics
{
    internal class LennardJonesPotential
    {
        public static double r0 = 0.382;
        public static double r06 = Math.Pow(r0, 6);
		public static double D = 0.0103 * 1.60218e-19;  // джоули (м^2 / с^2)
        public static double mod_r1 = 1.15;
		public static double mod_r2 = 1.75;
		protected static double r1 => mod_r1 * r0;
		protected static double r2 => mod_r2 * r0;

        public static double GetValue(double r)
        {
            if (r <= r1)
            {
                return D * (Math.Pow(r0 / r, 12) - 2 * Math.Pow(r0 / r, 6));
            }
            else if (r <= r2)
            {
                double K = (1 - (r - r1) / (r1 - r2) * (r - r1) / (r1 - r2)) * (1 - (r - r1) / (r1 - r2) * (r - r1) / (r1 - r2));

                return D * K * (Math.Pow(r0 / r, 12) - 2 * Math.Pow(r0 / r, 6));
            }
            return 0;
        }

        // Антиградиент от потенциала
        public static double GetForce(double r_sqr)
        {
            if (Math.Sqrt(r_sqr) > r2) 
            {
                return 0;
            }
            double r4 = r_sqr * r_sqr;
            double r6 = r_sqr * r4;
            double r8 = r4 * r4;

            double a6 = r06;
            return 12.0 * D * a6 * (a6 / r6 - 1.0) / r8;
        }

    }
}
