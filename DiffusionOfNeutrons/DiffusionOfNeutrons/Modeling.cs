using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiffusionOfNeutrons
{
    internal class Modeling
    {
        public double Pa = 0.15;
        public double lambda = 10;
        public double dDl = 5;
        public double d => dDl * lambda;
        Random random = new Random();
        public enum reasons
        {
            absorbed, reflected, passed
        }
        public (List<Point>, reasons) modeling()
        {
            List<Point> trajectory = new List<Point>();
            reasons reason = reasons.absorbed;

            for (Point cPos = new Point(0, 0); random.NextDouble() > Pa; cPos = move(cPos))
            {
                trajectory.Add(cPos);
                if (cPos.X < 0) 
                {
                    reason = reasons.reflected;
                    break;
                };
                if (cPos.X > d)
                {
                    reason = reasons.passed;
                    break;
                };
            }

            return (trajectory, reason);
        }
        Point move(Point cPos)
        {
            double theta = Math.Tau * random.NextDouble();
            double L = -lambda * Math.Log(random.NextDouble());
            return new Point(cPos.X + L * Math.Cos(theta), cPos.Y + L * Math.Sin(theta));
        }
    }
}
