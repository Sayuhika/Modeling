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

namespace PlateInTheEField
{
    public partial class FormMain : System.Windows.Forms.Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            var F = new Functions
            {
                V = Convert.ToDouble(textBoxV.Text), L = Convert.ToDouble(textBoxL.Text),
                N = Convert.ToDouble(textBoxN.Text), n = Convert.ToInt32(textBoxCount.Text)
            };

            F.Resolve();

            var graph = new Series();
            for (int i = 0; i < F.n; i++)
            {
                graph.Points.AddXY(F.L * i / (F.n - 1), F.Potential[i]);
            }
            graph.ChartType = SeriesChartType.Line;
            chart.Series.Add(graph);
            chart.ChartAreas[0].AxisX.Minimum = 0;
            chart.ChartAreas[0].AxisX.Maximum = F.L;
        }
    }
}
