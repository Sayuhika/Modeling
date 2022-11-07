using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Numerics;


namespace EvolutionOfProbabilityDensity
{
    public partial class FormMain : Form
    {
        FunctionsMkII F;
        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonStartStop_Click(object sender, EventArgs e)
        {
            if (backgroundWorker.IsBusy)
            {
                backgroundWorker.CancelAsync();
                return;
            }
            checkBox.Checked = false;
            buttonStartStop.Text = "Остановить моделирование";
            trackBarEvolTime.Value = 0;
            double d = Convert.ToDouble(textBoxd.Text);
            double U = Convert.ToDouble(textBoxU.Text);

            F = new FunctionsMkII
                (
                    Convert.ToDouble(textBoxdx.Text),
                    Convert.ToDouble(textBoxd.Text),
                    Convert.ToDouble(textBoxU.Text),
                    Convert.ToDouble(textBoxdMult.Text),
                    createGaussCupola()
                );
            //F.Init(createGaussCupola());
            chartEoPD.ChartAreas[0].AxisX.Minimum = 0;
            chartEoPD.ChartAreas[0].AxisX.Maximum = F.E.Length - 1;
            chartEoPD.ChartAreas[0].AxisY.Minimum = 0;
            chartEoPD.ChartAreas[0].AxisY.Maximum = checkBox.Checked ? (Math.Ceiling(F.FFunctions.Max(arr => arr.Max(v => v.Magnitude)))) : (5);

            backgroundWorker.RunWorkerAsync(F);
        }

        private Complex[] createGaussCupola()
        {
            int n = (int)(2 * Convert.ToDouble(textBoxdMult.Text) / Convert.ToDouble(textBoxdx.Text));

            var psiTemp = new Complex[n];
            double dev_value = Convert.ToDouble(numericUpDownDev.Value);
            double exp_value = Convert.ToDouble(numericUpDownExp.Value);

            for (int i = 0; i < n; i++)
            {
                double x = (double)i / n - exp_value;
                psiTemp[i] = Math.Exp(-(x * x) / (2 * dev_value * dev_value))/(dev_value * Math.Sqrt(2 * Math.PI));

            }

            return psiTemp;
        }

        private void draw(Chart cht, Complex[] data)
        {
            var graph = new Series();
            graph.ChartArea = "ChartArea1";
            graph.ChartType = SeriesChartType.Line;
            for (int i = 0; i < data.Length; i++)
            {
                graph.Points.AddXY(i, data[i].Magnitude);
            }
            cht.Series.Clear();
            cht.Series.Add(graph);
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var F = e.Argument as FunctionsMkII;

            var Worker = sender as BackgroundWorker;

            while (!Worker.CancellationPending)
            {
                Worker.ReportProgress(0, F.EvolutionStep(0, 0, 0, 0));
                System.Threading.Thread.Sleep(10);
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            trackBarEvolTime.Maximum = F.EoPD.Count - 1;
            trackBarEvolTime.Value = F.EoPD.Count - 1;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            buttonStartStop.Text = "Начать моделирование";
            F.toFourier();

            chartF.ChartAreas[0].AxisX.Minimum = 0;
            chartF.ChartAreas[0].AxisX.Maximum = F.FFunctions[0].Length - 1;
            chartF.ChartAreas[0].AxisY.Minimum = 0;
            chartF.ChartAreas[0].AxisY.Maximum = Math.Ceiling(F.FFunctions.Max(arr=>arr.Max(v=>v.Magnitude)));

            trackBarFourier.Value = 0;
            trackBarFourier.Maximum = F.FFunctions.Length - 1;
            trackBarFourier_ValueChanged(null, null);
        }

        private void trackBarEvolTime_ValueChanged(object sender, EventArgs e)
        {
            var PD = checkBox.Checked ? F.GetEigenFunction(trackBarEvolTime.Value) : F.EoPD[trackBarEvolTime.Value];
            draw(chartEoPD, PD);
            textBoxCurrentTime.Text = trackBarEvolTime.Value.ToString();
        }

        private void trackBarFourier_ValueChanged(object sender, EventArgs e)
        {
            var PD = F.FFunctions[trackBarFourier.Value];
            draw(chartF, PD);
            textBoxCurrentF.Text = trackBarFourier.Value.ToString();
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            trackBarEvolTime.Maximum = checkBox.Checked ? (F.FFunctions[0].Length - 1) : (F.EoPD.Count - 1);
            chartEoPD.ChartAreas[0].AxisY.Maximum = checkBox.Checked ? (Math.Ceiling(F.FFunctions.Max(arr => arr.Max(v => v.Magnitude)))) : (5);
        }

        private void numericUpDownExp_ValueChanged(object sender, EventArgs e)
        {
            var temp = createGaussCupola();
            draw(chartEoPD, temp);
        }

        private void numericUpDownDev_ValueChanged(object sender, EventArgs e)
        {
            var temp = createGaussCupola();
            draw(chartEoPD, temp);
        }

    }
}
