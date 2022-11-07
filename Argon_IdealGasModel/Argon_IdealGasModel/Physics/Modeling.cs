using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;

namespace Argon_IdealGasModel.Physics
{
    internal class Modeling : DispatcherTimer
    {
        public double L;
        public double T; // Установленная температура системы
        public double pressurePlate;
        public ulong count = 0;
        public uint period_PFT = 2000;
        public List<double> Virial = new();
        public List<Atom2d> atoms;
        public List<double> momentums_of_atoms = new();
        public List<double> Ep = new();
        public List<double> Ek = new();
        public List<double> E = new();
        public List<double> pFromT_verial = new();
        public List<double> pFromT_impact = new();
        const double k = 1.380649e-23;
        public void Step()
        {
            pressurePlate = 0.5 * L;
            count += 1;

            VerletStep();

            CulculateEnergy();

            if (count % 10 == 0 && count < period_PFT) 
                ToAverageSpeed();

            if (count % period_PFT == 0) {
                PressureFromTemperature_Verial(period_PFT);
                PressureFromTemperature_Impact(period_PFT);
            }
                
        }

        /// <summary>
        /// Шаг по алгоритму Верле
        /// </summary>
        void VerletStep() 
        {
            double momentum_x = 0;
            double virial = 0;
            List<double> x_last = new List<double>();
            
            foreach (var atom in atoms)
            {
                x_last.Add(atom.x);

                atom.x += atom.vx * Atom2d.delta_t + 0.5 * atom.ax * Atom2d.delta_t2 / Atom2d.m;
                atom.y += atom.vy * Atom2d.delta_t + 0.5 * atom.ay * Atom2d.delta_t2 / Atom2d.m;

                atom.periodic(L, L);
            }

            foreach (var atom in atoms)
            {
                atom.vx += 0.5 * atom.ax * Atom2d.delta_t / Atom2d.m;
                atom.vy += 0.5 * atom.ay * Atom2d.delta_t / Atom2d.m;
            }

            foreach (var atom in atoms)
            {
                atom.ax = 0;
                atom.ay = 0;
            }

            for (int i = 0; i < atoms.Count; i++)
            {
                Atom2d a = atoms[i];

                for (int j = i + 1; j < atoms.Count; j++)
                {
                    Atom2d b = atoms[j];
                    double dx = a.x - b.x;
                    double dy = a.y - b.y;
                    dx = (Math.Abs(dx) > 0.5 * L) ? dx - L * Math.Sign(dx) : dx;
                    dy = (Math.Abs(dy) > 0.5 * L) ? dy - L * Math.Sign(dy) : dy;
                    double r2 = dx * dx + dy * dy;
                    double force = LennardJonesPotential.GetForce(r2);

                    double fx = force * dx;
                    double fy = force * dy;

                    a.ax += fx;
                    a.ay += fy;
                    b.ax -= fx;
                    b.ay -= fy;
                }

                virial += a.x * a.ax + a.y * a.ay;
            }

            Virial.Add(virial);

            for (int i = 0; i < atoms.Count; i++)
            {
                atoms[i].vx += 0.5 * atoms[i].ax * Atom2d.delta_t / Atom2d.m;
                atoms[i].vy += 0.5 * atoms[i].ay * Atom2d.delta_t / Atom2d.m;

                if (x_last[i] < pressurePlate && atoms[i].x > pressurePlate) 
                    momentum_x += Math.Abs(atoms[i].getMomentumX());
                if (x_last[i] > pressurePlate && atoms[i].x < pressurePlate) 
                    momentum_x += Math.Abs(atoms[i].getMomentumX());
            }

            momentums_of_atoms.Add(momentum_x);
        }

        /// <summary>
        /// Усреднение скорости по заданной температуре за 10 шагов
        /// </summary>
        void ToAverageSpeed(int steps = 10) 
        {
            double atomsVsq10Steps = 0;

            for (int i = 0; i < steps; i++)
            {
                atomsVsq10Steps += Ek[Ek.Count - 1 - i];
            }
            atomsVsq10Steps /= steps;

            double b = Math.Sqrt((T * atoms.Count * k) / (atomsVsq10Steps));
            Parallel.For(0, atoms.Count, j =>
            {
                atoms[j].vx = atoms[j].vx * b;
                atoms[j].vy = atoms[j].vy * b;
            });

            atomsVsq10Steps = 0;
        }

        /// <summary>
        /// Успокоение бешАных атомов
        /// </summary>
        void SystemStabilityControl() 
        {
            Parallel.For(0, atoms.Count, i =>
            {
                if (atoms[i].x > 2 * L)
                {
                    atoms[i].x = 0;
                    atoms[i].vx = Math.Sqrt(2 * T * k / Atom2d.m);
                }
                if (atoms[i].x < -L)
                {
                    atoms[i].x = L;
                    atoms[i].vx = -Math.Sqrt(2 * T * k / Atom2d.m);
                }
                if (atoms[i].y > 2 * L)
                {
                    atoms[i].y = 0;
                    atoms[i].vy = Math.Sqrt(2 * T * k / Atom2d.m);
                }
                if (atoms[i].y < -L)
                {
                    atoms[i].y = L;
                    atoms[i].vy =  -Math.Sqrt(2 * T * k / Atom2d.m);
                }
            });
        }

        /// <summary>
        /// Расчет энергии
        /// </summary>
        void CulculateEnergy() 
        {
            double Ek_t = 0, Ep_t = 0;

            for (int i = 0; i < atoms.Count; i++)
            {
                Ek_t += (atoms[i].vx * atoms[i].vx + atoms[i].vy * atoms[i].vy) * Atom2d.m * 0.5;

                for (int j = i + 1; j < atoms.Count; j++)
                {   
                    Vector dF = new Vector(atoms[i].x - atoms[j].x, atoms[i].y - atoms[j].y);
                    Ep_t += LennardJonesPotential.GetValue(dF.Length);
                }
            }

            Ep.Add(Ep_t);
            Ek.Add(Ek_t);
            E.Add(Ek_t + Ep_t);
        }

        /// <summary>
        /// Текущая усредненная за несколько шагов температура системы
        /// </summary>
        /// <param name="steps">количество шагов за которое происходит усреднение</param>
        /// <returns></returns>
        double GetTemperature(uint steps = 500) 
        {
            double atomsVsq10Steps = 0;

            for (int i = 0; i < steps; i++)
            {
                atomsVsq10Steps += Ek[Ek.Count - 1 - i];
            }

            atomsVsq10Steps /= steps;

            return atomsVsq10Steps / (atoms.Count * k);
        }

        /// <summary>
        /// Расчет давления от температуры по теореме вериала
        /// </summary>
        void PressureFromTemperature_Verial(uint steps = 500) 
        {
            T = GetTemperature(steps);
            double a = 0;

            for (int t = 0; t < steps; t++)
            {
                a += Virial[Virial.Count - 1 - t];
            }

            pFromT_verial.Add((T * k * atoms.Count + 0.5 * a / steps) / (L * L));
        }

        void PressureFromTemperature_Impact(uint steps = 500) 
        {
            double dF = 0;

            for (int i = 0; i < steps; i++)
            {
                dF += momentums_of_atoms[momentums_of_atoms.Count - 1 - i];
            }

            pFromT_impact.Add(dF / (2 * L * steps * Atom2d.delta_t));
        }
    }
}
