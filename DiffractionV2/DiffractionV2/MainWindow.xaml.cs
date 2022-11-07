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
using DiffractionV2.Physics;
using DiffractionV2.Drawing;

namespace DiffractionV2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ComplexImage CI;
        Parametres Prs;
        Algorithm Am;
        bool running = false;
        public MainWindow()
        {
            InitializeComponent();
            ChartBox.ChartAreas.Clear();
            ChartBox.ChartAreas.Add("default");
            ChartBox.ChartAreas["default"].AxisX.Minimum = 0;
            Am = new Algorithm();
            Prs = new Parametres();
            Am.Interval = TimeSpan.FromMilliseconds(20);
            Am.Tick += animation;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                TextBoxAC.Text = $"{(int)SliderAC.Value}";
                TextBoxAS.Text = $"{SliderAS.Value}";
                TextBoxAW.Text = $"{SliderAW.Value}";
                Prs.WIDTH = Convert.ToInt32(TextBoxSS.Text);
                Prs.HEIGHT = Convert.ToInt32(TextBoxSS.Text);
                Prs.ApertureCount = Convert.ToInt32(TextBoxAC.Text);
                Prs.ApertureW = Convert.ToDouble(TextBoxAW.Text);
                Prs.ApertureS = Convert.ToDouble(TextBoxAS.Text);
                Prs.DTS = Convert.ToDouble (SliderDtS.Value);
                Prs.SourceX = Convert.ToDouble(SliderLHP.Value);
                Prs.SourceY = Convert.ToDouble(SliderLVP.Value);
                Prs.OMEGA = Convert.ToDouble(SliderLF.Value * 1e-4);
            }
            catch (NullReferenceException) { }
            catch (ArgumentException) { }

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
            catch (Exception) { }

            Slider_ValueChanged(this, null);
        }

        void animation(object sender, EventArgs e) 
        {
            Am.next();
            DrawChart(Am.GetScreenStatus());
            CI = Am.getValues();
            ImageBox.Source = ImageProcessing.ConvertToSource(CI.ToBitmap(true));
        }

        private void ButtonStartStop_Click(object sender, RoutedEventArgs e)
        {
            running = !running;
            if (running) 
            {
                ButtonStartStop.Content = "Stop";
                Slider_ValueChanged(this, null);
                Am.Restart(Prs);
                Am.Start();
            }
            else 
            {
                ButtonStartStop.Content = "Start";
                Am.Stop();               
            }
        }
    }
}
