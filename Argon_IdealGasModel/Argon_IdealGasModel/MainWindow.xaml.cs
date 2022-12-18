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
            pvm_PT.Min = 0;

            pvm_energy.Points = new[] { new List<OxyPlot.DataPoint>(), new List<OxyPlot.DataPoint>(), new List<OxyPlot.DataPoint>() };
            pvm_PT.Points = new[] { new List<OxyPlot.DataPoint>(), new List<OxyPlot.DataPoint>() };
        }

        private void InitializeCalcModel() 
        {
            N = int.Parse(this.textBox_N.Text);
            int a = int.Parse(this.textBox_a.Text);
            max_v = double.Parse(this.textBox_vmax.Text);
            Atom2d.delta_t = double.Parse(this.textBox_dt.Text);
            Atom2d.delta_t2 = Atom2d.delta_t * Atom2d.delta_t;
            CalcModel.T = double.Parse(this.textBox_T.Text);
            CalcModel.L = pvm_atoms.Max = a * LennardJonesPotential.r0;
            CalcModel.period_PFT = uint.Parse(this.textBox_ptSteps.Text);
            CalcModel.period_NV = uint.Parse(this.textBox_nvSteps.Text);

            CalcModel.InitializeModel(a, N, max_v);
        }
        private void Button_StartModeling(object sender, RoutedEventArgs e)
        {
            doModeling = !doModeling;

            if (doModeling)
            {
                InitializeCalcModel();

                CalcModel.Start();

                Rect bounds = VisualTreeHelper.GetDescendantBounds(this.atomsPlot);
                pvm_atoms.PointSize = (int) (bounds.Width * 0.5 / int.Parse(this.textBox_a.Text));

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
            for (int i = 0; i < 100000; i++)
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

        private void Button_StartResearch(object sender, RoutedEventArgs e)
        {
            using (OutputDataCollector ODC = new()) 
            {
                ODC.WriteHeader();
                int sample_count = 25;
                uint period_PFT = 20000;
                uint period_NV = 20000;
                Modeling[] models = new Modeling[sample_count];

                // Проводим исследования с разной плотностью
                for (int i = 0; i < 3; i++)
                {
                    int N = (50 + i * 350);
                    int a = int.Parse(this.textBox_a.Text);
                    max_v = double.Parse(this.textBox_vmax.Text);
                    Atom2d.delta_t = double.Parse(this.textBox_dt.Text);
                    Atom2d.delta_t2 = Atom2d.delta_t * Atom2d.delta_t;
                    double L = a * LennardJonesPotential.r0;

                    // Меняем температуру и проводим новое исследование с ней
                    Parallel.For(0, sample_count, j =>
                    {
                        models[j] = new Modeling() 
                        { 
                            T = 2 + j * 2, 
                            period_PFT = period_PFT,
                            period_NV = period_NV,
                            L = L,

                        };

                        models[j].InitializeModel(a, N, max_v);

                        while (models[j].count <= 40000)
                        {
                            models[j].Step();
                        }
                    });

                    // Запись данных исследования в фаил
                    for (int j = 0; j < sample_count; j++)
                    {
                        ODC.WriteStats(models[j].atoms.Count, 50 + j * 5, models[j].T, models[j].pFromT_verial.Last(), models[j].pFromT_impact.Last());
                    }

                    ODC.WriteSeparator();
                }
            }
        }
    }
}
