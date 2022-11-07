using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Argon_IdealGasModel.Physics
{
    internal class Atom2d
    {
        public override string ToString()
        {
            return $"Atom( vx: {vx} vy: {vy} v: {getSpeedsq()})";
        }
        public double x, y, vx, vy, ax, ay;
        public static double delta_t;
        public static double delta_t2;
        public static double m = 66.335252e-27; // kg
        public double last_Fx = 0;
        public double last_Fy = 0;

        /// <summary>
        /// Производит перерасчет координат и скоростей основываясь на известных данных
        /// </summary>
        /// <param name="Fx">Сила действующая на атом в проекции на ось X</param>
        /// <param name="Fy">Сила действующая на атом в проекции на ось Y</param>
        public void move(double Fx = 0, double Fy = 0)
        {
            x = x + vx * delta_t + 0.5 * last_Fx / m * delta_t2;
            y = y + vy * delta_t + 0.5 * last_Fy / m * delta_t2;

            vx = vx + 0.5 * (last_Fx + Fx) / m * delta_t;
            vy = vy + 0.5 * (last_Fy + Fy) / m * delta_t;

            last_Fx = Fx;
            last_Fy = Fy;
        }

        public void periodic(double width, double height)
        {
            x = (x < 0) ? (x + width) : x;
            x = (x > width) ? (x - width) : x;
            y = (y < 0) ? (y + height) : y;
            y = (y > height) ? (y - height) : y;
        }

        public double getSpeed() 
        {
            return Math.Sqrt(getSpeedsq());
        }
        public double getSpeedsq()
        {
            return vx * vx + vy * vy;
        }

        public double getMomentumX()
        {
            return m * vx;
        }
    }
}
