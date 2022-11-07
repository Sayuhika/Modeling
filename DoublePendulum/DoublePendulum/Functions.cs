using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoublePendulum
{
    public class Functions
    {
        public double accuracy, m1, m2, l1, l2, g, ang1, ang2, vel1, vel2;
        public Functions(double accuracy, double m1, double m2, double l1, double l2, double g, double ang1, double ang2, double vel1, double vel2)
        {
            this.accuracy = accuracy;
            this.m1 = m1;
            this.m2 = m2;
            this.l1 = l1;
            this.l2 = l2;
            this.g = g;
            this.ang1 = ang1;
            this.ang2 = ang2;
            this.vel1 = vel1;
            this.vel2 = vel2;
        }
        double AccFunc1(double ang1, double ang2, double vel1, double vel2)
        {
            double angDif = ang1 - ang2;

            return (m2 * l1 * vel1 * vel1 * Math.Sin(angDif) * Math.Cos(angDif)
                + m2 * l2 * vel2 * vel2 * Math.Sin(angDif)
                + g * (m1 + m2) * Math.Sin(ang1)
                - g * m2 * Math.Sin(ang2) * Math.Cos(angDif)) /
                (m2 * l1 * Math.Cos(angDif) * Math.Cos(angDif) - (m1 + m2) * l1);
        }

        double AccFunc2(double ang1, double ang2, double vel1, double vel2)
        {
            double angDif = ang1 - ang2;

            return (vel2 * vel2 * Math.Sin(angDif) * Math.Cos(angDif) * m2 / (m1 + m2)
                + g * (Math.Sin(ang1) * Math.Cos(angDif) - Math.Sin(ang2)) / l2
                + vel1 * vel1 * Math.Sin(angDif) * l1 / l2)
                * (m1 + m2) / (m1 + m2 * (1 - Math.Cos(angDif) * Math.Cos(angDif)));
        }
        double[] RK4(double a1, double a2, double v1, double v2, double h)
        {
            double[] k1 = new double[4];
            double[] k2 = new double[4];
            double[] k3 = new double[4];
            double[] k4 = new double[4];

            k1[0] = v1 * h;
            k1[1] = h * AccFunc1(a1, a2, v1, v2);
            k1[2] = v2 * h;
            k1[3] = h * AccFunc2(a1, a2, v1, v2);

            k2[0] = (v1 + k1[1] * 0.5) * h;
            k2[1] = h * AccFunc1(a1 + 0.5 * k1[0], a2 + 0.5 * k1[2], v1 + 0.5 * k1[1], v2 + 0.5 * k1[3]);
            k2[2] = (v2 + k1[3] * 0.5) * h;
            k2[3] = h * AccFunc2(a1 + 0.5 * k1[0], a2 + 0.5 * k1[2], v1 + 0.5 * k1[1], v2 + 0.5 * k1[3]);

            k3[0] = (v1 + h * k2[1] * 0.5) * h;
            k3[1] = h * AccFunc1(a1 + 0.5 * k2[0], a2 + 0.5 * k2[2], v1 + 0.5 * k2[1], v2 + 0.5 * k2[3]);
            k3[2] = (v2 + h * k2[3] * 0.5) * h;
            k3[3] = h * AccFunc2(a1 + 0.5 * k2[0], a2 + 0.5 * k2[2], v1 + 0.5 * k2[1], v2 + 0.5 * k2[3]);

            k4[0] = (v1 + k3[1]) * h;
            k4[1] = h * AccFunc1(a1 + k3[0], a2 + k3[2], v1 + k3[1], v2 + k3[3]);
            k4[2] = (v2 + k3[3]) * h;
            k4[3] = h * AccFunc2(a1 + k3[2], a2 + k3[2], v1 + k3[1], v2 + k3[3]);

            return new double[] { (k1[1] + 2 * k2[1] + 2 * k3[1] + k4[1]) / 6, (k1[0] + 2 * k2[0] + 2 * k3[0] + k4[0]) / 6,
                                  (k1[3] + 2 * k2[3] + 2 * k3[3] + k4[3]) / 6, (k1[2] + 2 * k2[2] + 2 * k3[2] + k4[2]) / 6 };
        }
        public void Step ()
        {
            double ang1h = accuracy * vel1;
            double ang2h = accuracy * vel2;

            // Считаем угловую скорость и угол для первого груза на следующем шаге
            double[] buffer = RK4(ang1, ang2, vel1, vel2, accuracy);
            vel1 = vel1 + buffer[0];
            ang1 = ang1 + buffer[1];
        
            // Считаем угловую скорость и угол для второго груза на следующем шаге
            vel2 = vel2 + buffer[2];
            ang2 = ang2 + buffer[3];
        }
        public double[] GetCoords()
        {
            double x1 = l1 * Math.Sin(ang1);
            double y1 = l1 * Math.Cos(ang1);
            return new double[] {x1, y1, x1 + l2 * Math.Sin(ang2), y1 + l2 * Math.Cos(ang2)};
        }
    }
}
