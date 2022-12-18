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
using OxyPlot;
using OxyPlot.Series;

namespace DiffusionOfNeutrons
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PlotViewModel pvm_m = new();
        private PlotViewModel pvm_ud1 = new();
        private PlotViewModel pvm_ud2 = new();
        private PlotViewModel pvm_ud_l1 = new();
        private PlotViewModel pvm_ud_l2 = new();
        private PlotViewModel pvm_uPa1 = new();
        private PlotViewModel pvm_uPa2 = new();
        public MainWindow()
        {
            InitializeComponent();

            TBox_labda.Text = "10";
            TBox_lDd.Text = "5";
            TBox_Pa.Text = "0,15";
            TBox_N.Text = "15000";
            TBox_steps_count.Text = "100";
            TBox_lDd_lb.Text = "0,1";
            TBox_lDd_rb.Text = "10";
            TBox_Pa_lb.Text = "0,01";
            TBox_Pa_rb.Text = "0,3";
            double d = 50;

            pvm_m.Min_X = -d;
            pvm_m.Max_X = 2 * d;
            pvm_m.Min_Y = -d;
            pvm_m.Max_Y = d;

            // График моделирования
            pvm_m.Points = new[] 
            { 
                new List<OxyPlot.DataPoint>() { new DataPoint(0, - 10 * d), new DataPoint(0, 10 * d) }, 
                new List<OxyPlot.DataPoint>() { new DataPoint(d, - 10 * d), new DataPoint(d, 10 * d) }, 
                new List<OxyPlot.DataPoint>() 
            };
            (pvm_m.Model.Series[0] as LineSeries).Color = OxyColors.Black;
            (pvm_m.Model.Series[1] as LineSeries).Color = OxyColors.Black;
            (pvm_m.Model.Series[2] as LineSeries).Color = OxyColors.Cyan;

            ImageBox.DataContext = pvm_m;

            // Графики u(d)
            pvm_ud1.Points = new[]
            {
                new List<OxyPlot.DataPoint>(), new List<OxyPlot.DataPoint>(), new List<OxyPlot.DataPoint>(), new List<OxyPlot.DataPoint>()
            };

            PC_ud1.DataContext = pvm_ud1;

            pvm_ud2.Points = new[]
            {
                new List<OxyPlot.DataPoint>(), new List<OxyPlot.DataPoint>(), new List<OxyPlot.DataPoint>(), new List<OxyPlot.DataPoint>()
            };

            PC_ud2.DataContext = pvm_ud2;

            // Графики u(d) линейные
            pvm_ud_l1.Points = new[]
            {
                new List<OxyPlot.DataPoint>(), new List<OxyPlot.DataPoint>(), new List<OxyPlot.DataPoint>(), new List<OxyPlot.DataPoint>()
            };

            PC_ud_l1.DataContext = pvm_ud_l1;

            pvm_ud_l2.Points = new[]
            {
                new List<OxyPlot.DataPoint>(), new List<OxyPlot.DataPoint>(), new List<OxyPlot.DataPoint>(), new List<OxyPlot.DataPoint>()
            };

            PC_ud_l2.DataContext = pvm_ud_l2;

            // Графики u(Pa)
            pvm_uPa1.Points = new[]
            {
                new List<OxyPlot.DataPoint>(), new List<OxyPlot.DataPoint>(), new List<OxyPlot.DataPoint>()
            };

            PC_uPa1.DataContext = pvm_uPa1;

            pvm_uPa2.Points = new[]
            {
                new List<OxyPlot.DataPoint>(), new List<OxyPlot.DataPoint>(), new List<OxyPlot.DataPoint>()
            };

            PC_uPa2.DataContext = pvm_uPa2;
        }

        private void ButtonDo_Click(object sender, RoutedEventArgs e)
        {
            Modeling M = new Modeling() 
            { 
                dDl = double.Parse(TBox_lDd.Text), 
                lambda = double.Parse(TBox_labda.Text),
                Pa = double.Parse(TBox_Pa.Text)
            };
            var (projectory, reason) = M.modeling();

            pvm_m.Points[2] = projectory.Select((p) => new OxyPlot.DataPoint(p.X, p.Y)).ToList();
        }

        private void ButtonResearch_Click(object sender, RoutedEventArgs e)
        {
            // Получаем начальные данные
            double dDl = double.Parse(TBox_lDd.Text);
            double lambda = double.Parse(TBox_labda.Text);
            double Pa = double.Parse(TBox_Pa.Text);
            int N = int.Parse(TBox_N.Text);
            int maxSteps = int.Parse(TBox_steps_count.Text);
            double lDd_lb = double.Parse(TBox_lDd_lb.Text);
            double lDd_rb = double.Parse(TBox_lDd_rb.Text);
            double Pa_lb = double.Parse(TBox_Pa_lb.Text);
            double Pa_rb = double.Parse(TBox_Pa_rb.Text);

            // Инициализируем массивы для хранения данных
            var stats_ud1 = new List<DataPoint>[] {new(), new(), new(), new() };
            var stats_ud2 = new List<DataPoint>[] { new(), new(), new(), new() };
            var stats_ud_l1 = new List<DataPoint>[] { new(), new() };
            var stats_ud_l2 = new List<DataPoint>[] { new(), new() };
            var stats_uPa1 = new List<DataPoint>[] { new(), new(), new() };
            var stats_uPa2 = new List<DataPoint>[] { new(), new(), new() };

            // Расчитываем шаги для проведения исследования
            double step_dDl = (lDd_rb - lDd_lb) / (maxSteps - 1);
            double step_Pa  = (Pa_rb - Pa_lb)   / (maxSteps - 1);

            double count_absorbed, count_reflected, count_passed;

            // Расчитываем графики 
            for (int i = 0; i < maxSteps; i++)
            {
                double k1 = 0.1 + step_dDl * i;

                (count_absorbed, count_reflected, count_passed) = 
                    GetStats(k1, lambda, Pa, N);

                stats_ud1[0].Add(new (k1, count_absorbed / N));
                stats_ud1[1].Add(new(k1, count_reflected / N));
                stats_ud1[2].Add(new(k1, count_passed / N));

                (count_absorbed, count_reflected, count_passed) =
                    GetStats(k1, lambda, Pa * 2, N);

                stats_ud2[0].Add(new(k1, count_absorbed / N));
                stats_ud2[1].Add(new(k1, count_reflected / N));
                stats_ud2[2].Add(new(k1, count_passed / N));

                double k2 = 0.01 + step_Pa * i;

                (count_absorbed, count_reflected, count_passed) =
                    GetStats(dDl, lambda, k2, N);

                stats_uPa1[0].Add(new(k2, count_absorbed / N));
                stats_uPa1[1].Add(new(k2, count_reflected / N));
                stats_uPa1[2].Add(new(k2, count_passed / N));

                (count_absorbed, count_reflected, count_passed) =
                    GetStats(dDl * 2, lambda, k2, N);

                stats_uPa2[0].Add(new(k2, count_absorbed / N));
                stats_uPa2[1].Add(new(k2, count_reflected / N));
                stats_uPa2[2].Add(new(k2, count_passed / N));
            }

            // Расчет коэффициента поглощения
            int size = stats_ud1[2].Count;
            double[] x = new double[size];
            double[] y = new double[size];

            for (int i = 0; i < size; i++)
            {
                x[i] = stats_ud1[2][i].X;
                y[i] = Math.Log(stats_ud1[2][i].Y);
            }
            var reg1 = Accord.Statistics.Models.Regression.Linear.SimpleLinearRegression.FromData(x, y);
            double m1 = -1 / reg1.Slope;
            double a1 = reg1.Intercept;

            for (int i = 0; i < size; i++)
            {
                x[i] = stats_ud2[2][i].X;
                y[i] = Math.Log(stats_ud2[2][i].Y);
            }

            var reg2 = Accord.Statistics.Models.Regression.Linear.SimpleLinearRegression.FromData(x, y);
            double m2 = -1 / reg2.Slope;
            double a2 = reg2.Intercept;

            TBox_m_ul1.Text = m1.ToString();
            TBox_m_ul2.Text = m2.ToString();
            TBox_a_ul1.Text = a1.ToString();
            TBox_a_ul2.Text = a2.ToString();

            // Формирование аппроксимаций и линеризованных графиков
            for (int i = 0; i < size; i++)
            {
                stats_ud1[3].Add(new(stats_ud1[2][i].X, Math.Exp(a1 - stats_ud1[2][i].X / m1)));
                stats_ud2[3].Add(new(stats_ud2[2][i].X, Math.Exp(a2 - stats_ud2[2][i].X / m2)));

                stats_ud_l1[0].Add(new(stats_ud1[2][i].X, Math.Log(stats_ud1[2][i].Y)));
                stats_ud_l1[1].Add(new(stats_ud1[2][i].X, Math.Log(stats_ud1[3].Last().Y)));

                stats_ud_l2[0].Add(new(stats_ud1[2][i].X, Math.Log(stats_ud2[2][i].Y)));
                stats_ud_l2[1].Add(new(stats_ud1[2][i].X, Math.Log(stats_ud2[3].Last().Y)));
            }

            // Отрисовка графиков
            // u(d)
            pvm_ud1.Points[0] = stats_ud1[0];
            pvm_ud1.Points[1] = stats_ud1[1];
            pvm_ud1.Points[2] = stats_ud1[2];
            pvm_ud1.Points[3] = stats_ud1[3];

            pvm_ud2.Points[0] = stats_ud2[0];
            pvm_ud2.Points[1] = stats_ud2[1];
            pvm_ud2.Points[2] = stats_ud2[2];
            pvm_ud2.Points[3] = stats_ud2[3];

            // u(d) linear
            pvm_ud_l1.Points[2] = stats_ud_l1[0];
            pvm_ud_l1.Points[1] = stats_ud_l1[1];

            pvm_ud_l2.Points[2] = stats_ud_l2[0];
            pvm_ud_l2.Points[1] = stats_ud_l2[1];

            // u(Pa)
            pvm_uPa1.Points[0] = stats_uPa1[0];
            pvm_uPa1.Points[1] = stats_uPa1[1];
            pvm_uPa1.Points[2] = stats_uPa1[2];

            pvm_uPa2.Points[0] = stats_uPa2[0];
            pvm_uPa2.Points[1] = stats_uPa2[1];
            pvm_uPa2.Points[2] = stats_uPa2[2];
        }

        (int,int,int) GetStats(double dDl, double lambda, double Pa, int N) 
        {
            Modeling M = new Modeling()
            {
                dDl = dDl,
                lambda = lambda,
                Pa = Pa
            };

            var (count_absorbed, count_reflected, count_passed) = (0, 0, 0);

            for (int i = 0; i < N; i++)
            {
                var (projectory, reason) = M.modeling();
                switch (reason)
                {
                    case Modeling.reasons.absorbed:
                        count_absorbed++;
                        break;
                    case Modeling.reasons.reflected:
                        count_reflected++;
                        break;
                    case Modeling.reasons.passed:
                        count_passed++;
                        break;
                    default:
                        break;
                }
            }

            return (count_absorbed, count_reflected, count_passed);
        }
    }
}
