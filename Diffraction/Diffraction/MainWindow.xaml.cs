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
using System.Numerics;
using System.Windows.Forms.DataVisualization.Charting;

namespace Diffraction
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ChartBox.ChartAreas.Clear();
            ChartBox.ChartAreas.Add("default");
            ChartBox.ChartAreas["default"].AxisX.Minimum = 0;
            Slider_ValueChanged(this, null);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                TextBoxAC.Text = $"{(int)SliderAC.Value}";
                TextBoxAS.Text = $"{SliderAS.Value}";
                TextBoxAW.Text = $"{SliderAW.Value}";
                var solution = GetPhysicsResolved();
                DrawChart(solution);
            }
            catch (NullReferenceException) { }
            catch (ArgumentException) { }
        }

        private double[] GetPhysicsResolved()
        {
            int samplerate = Convert.ToInt32(TextBoxSS.Text);
            bool showOrigin = false;
            try
            {
                showOrigin = ShowOL.IsChecked == true;
            }
            catch (NullReferenceException) { }

            ComplexImage Field = new ComplexImage()
            {
                Data = new Complex[2 * samplerate, samplerate]
            };

            LightSource LS_Origin = new LightSource()
            {
                amplitude = 1,
                frequency = SliderLF.Value,
                position = new Vector(-SliderLHP.Value * 0.01 * samplerate, SliderLVP.Value * 0.01 * samplerate)
            };

            LightSource[] LS_derivatives = new LightSource[2 * (int)SliderAC.Value];
            double apW = Math.Pow(10, SliderAW.Value - 2);
            double apS = Math.Pow(10, SliderAS.Value - 2);

            for (int i = 0; i < LS_derivatives.Length / 2; i++)
            { 
                double y = (100 - (apW + apS) * (SliderAC.Value))/2;

                LS_derivatives[2 * i] = new LightSource()
                {
                    frequency = LS_Origin.frequency,
                    position = new Vector(0, (y + (apW + apS) * i) * 0.01 * samplerate)
                };
                LS_derivatives[2 * i].amplitude = LS_Origin.WaveValue(LS_derivatives[2 * i].position);

                LS_derivatives[2 * i + 1] = new LightSource()
                {
                    frequency = LS_Origin.frequency,
                    position = new Vector(0, (y + apW + (apW + apS) * i) * 0.01 * samplerate)
                };
                LS_derivatives[2 * i + 1].amplitude = LS_Origin.WaveValue(LS_derivatives[2 * i + 1].position);
            }

            double[] solution = new double[samplerate];
            double screenX = SliderDtS.Value * 0.01;

            Parallel.For(0, solution.Length, i =>
             {
                 Parallel.For(0, 2 * solution.Length, j =>
                   {
                       if (showOrigin)
                       {
                           Field.Data[j, i] = LS_Origin.WaveValue(new Vector((double)(j) / (solution.Length - 1) - 1, (double)(i) / (solution.Length - 1)) * samplerate);
                       }
                       else { Field.Data[j, i] = Complex.Zero; }
                      
                       for (int k = 0; k < LS_derivatives.Length; k++)
                       {
                           Field.Data[j, i] += LS_derivatives[k].WaveValue(new Vector((double)(j) / (solution.Length - 1) - 1, (double)(i) / (solution.Length - 1)) * samplerate);
                       }
                   });

                 Complex value = Complex.Zero;
                 double screenY = (double)i / (solution.Length - 1);

                 for (int k = 0; k < LS_derivatives.Length; k++)
                 {
                     value += LS_derivatives[k].WaveValue(new Vector(screenX, screenY) * samplerate);
                 }

                 solution[i] = (value * Complex.Conjugate(value)).Real;
             });

            ImageBox.Source = ImageProcessing.ConvertToSource(Field.ToBitmap(true));

            return solution;
        }

        void DrawChart(double[] values)
        {
            ChartBox.Series.Clear();
            ChartBox.Series.Add("main");
            ChartBox.Series["main"].ChartArea = "default";
            ChartBox.Series["main"].ChartType = SeriesChartType.Line;

            for (int i = 0; i < values.Length; i++)
            {
                ChartBox.Series["main"].Points.AddXY(i, values[i]);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                SliderAC.Value = Convert.ToInt32(TextBoxAC.Text);
                SliderAS.Value = Convert.ToDouble(TextBoxAS.Text);
                SliderAW.Value = Convert.ToDouble(TextBoxAW.Text);
            }
            catch (Exception){ }

            Slider_ValueChanged(this, null);
        }

        private void ShowOL_Click(object sender, RoutedEventArgs e)
        {
             Slider_ValueChanged(this, null);
        }
    }
}
