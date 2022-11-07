using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Argon_IdealGasModel.Physics;

namespace Argon_IdealGasModel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Modeling CalcModel = new (); 

        // params
        public static int N;
        public static double max_v;
        public double E_k, E_p, E_f;

        public bool doModeling = false;

        private PlotViewModelPoints pvm_atoms = new();
        private PlotViewModel pvm_energy = new();
        private PlotViewModel pvm_PT = new();

        public MainWindow()
        {
            InitializeComponent();

            CalcModel.Interval = TimeSpan.FromMilliseconds(10);
            CalcModel.Tick += Calculate;

            atomsPlot.DataContext = pvm_atoms;
            energyPlot.DataContext = pvm_energy;
            PTPlot.DataContext = pvm_PT;

			pvm_energy.Points = new[] { new List<OxyPlot.DataPoint>(), new List<OxyPlot.DataPoint>(), new List<OxyPlot.DataPoint>() };
            pvm_PT.Points = new[] { new List<OxyPlot.DataPoint>(), new List<OxyPlot.DataPoint>() };
            //pvm_PT.Min = 0;
        }

        private void Button_StartModeling(object sender, RoutedEventArgs e)
        {
            doModeling = !doModeling;

            if (doModeling)
            {
                N = int.Parse(this.textBox_N.Text);
                double a = double.Parse(this.textBox_a.Text);
                max_v = double.Parse(this.textBox_vmax.Text);
				Atom2d.delta_t = double.Parse(this.textBox_dt.Text);
                Atom2d.delta_t2 = Atom2d.delta_t * Atom2d.delta_t;
                CalcModel.L = pvm_atoms.Max = a * LennardJonesPotential.r0;
                CalcModel.T = double.Parse(this.textBox_T.Text);

                bool[,] map = new bool[(int) a / 2, (int) a / 2];

                Random rand = new();
                CalcModel.atoms = new Atom2d[N].ToList();
                double vx_total = 0;
                double vy_total = 0;
                double atomsVsq10Steps = 0;

                // Создаем атомы со случайными параметрами
                for (int i = 0; i < N; i++) {
                    double x_map = rand.NextDouble() * a / 2;
                    double y_map = rand.NextDouble() * a / 2;

                    while (map[(int)x_map, (int)y_map]) 
                    {
                        x_map = rand.NextDouble() * a / 2;
                        y_map = rand.NextDouble() * a / 2;
                    }
                    map[(int)x_map, (int)y_map] = true;
                    double phi = 2 * Math.PI * rand.NextDouble();
                    CalcModel.atoms[i] = new Atom2d {
						x = (int)x_map * LennardJonesPotential.r0 * 2,
						y = (int)y_map * LennardJonesPotential.r0 * 2,
						//vx = max_v * Math.Cos(phi),
						//vy = max_v * Math.Sin(phi)
                        vx = (2 * rand.NextDouble() - 1) * max_v,
                        vy = (2 * rand.NextDouble() - 1) * max_v
                    };
                    atomsVsq10Steps += CalcModel.atoms[i].getSpeedsq();
                }

                // Усреднение скорости по температуре
                double b = Math.Sqrt((2 * CalcModel.T * N * 1.380649e-23) / (atomsVsq10Steps * Atom2d.m));
                for (int i = 0; i < N; i++)
                {
                    CalcModel.atoms[i].vx = CalcModel.atoms[i].vx * b;
                    CalcModel.atoms[i].vy = CalcModel.atoms[i].vy * b;

                    vx_total += CalcModel.atoms[i].vx;
                    vy_total += CalcModel.atoms[i].vy;
                }

                // Сохранение импульса
                vx_total /= N;
                vy_total /= N;

                Parallel.For(0, N, i => 
                {
                    CalcModel.atoms[i].vx -= vx_total;
                    CalcModel.atoms[i].vy -= vy_total;
                });

				CalcModel.Ek.Clear();
				CalcModel.Ep.Clear();
				CalcModel.E.Clear();
                CalcModel.pFromT_verial.Clear();
                CalcModel.pFromT_impact.Clear();
                CalcModel.count = 0;

                CalcModel.Start();

                Rect bounds = VisualTreeHelper.GetDescendantBounds(this.atomsPlot);
                pvm_atoms.PointSize = (int) (bounds.Width * 0.5 / a);

                if (sender is Button button)
					button.Content = "Остановить моделирование";
            }
            else
            {
                CalcModel.Stop();

				// Блокировать изменения текста

				if (sender is Button button)
					button.Content = "Началь моделирование";
			}
        }

        private void Calculate(object? sender, EventArgs e) 
        {
            //var stopWatch = new System.Diagnostics.Stopwatch();
            //stopWatch.Start();  
            for (int i = 0; i < 10; i++)
            {
                CalcModel.Step();
            }
            //stopWatch.Stop();

            pvm_atoms.Points[0] = CalcModel.atoms.Select(atom => new OxyPlot.DataPoint(atom.x, atom.y)).ToList();
            pvm_energy.Points[0] = CalcModel.Ek.Select((atom, i) => new OxyPlot.DataPoint(i, atom)).ToList();
            pvm_energy.Points[1] = CalcModel.Ep.Select((atom, i) => new OxyPlot.DataPoint(i, atom)).ToList();
            pvm_energy.Points[2] = CalcModel.E.Select((atom, i) => new OxyPlot.DataPoint(i, atom)).ToList();

            pvm_PT.Points[0] = CalcModel.pFromT_verial.Select((atom, i) => new OxyPlot.DataPoint(i, atom)).ToList();
            pvm_PT.Points[1] = CalcModel.pFromT_impact.Select((atom, i) => new OxyPlot.DataPoint(i, atom)).ToList();

            textBlock_current_T.Text = $"{CalcModel.T}";
        }
    }
}
