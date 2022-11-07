using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoublePendulum
{
    public partial class FormMain : Form
    {
        public Functions Physics;
        List<PointF> phasePath1, phasePath2;
        List<List<PointF>> phasePortrait1, phasePortrait2;
        const double gEarth = 9.80665;
        bool startStopMode;
        Bitmap DPimg, PPHimg, PPTimg;
        Graphics DPgr, PPHgr, PPTgr;

        public FormMain()
        {
            InitializeComponent();

            phasePortrait1 = new List<List<PointF>>();
            phasePortrait2 = new List<List<PointF>>();
            comboBoxPlanets.SelectedIndex = 2;
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            startStopMode = false;
            DPimg = new Bitmap(pictureBoxModel.Size.Width, pictureBoxModel.Size.Height);
            PPHimg = new Bitmap(pictureBoxPhasePath.Size.Width, pictureBoxPhasePath.Size.Height);
            PPTimg = new Bitmap(pictureBoxPhasePortrait.Size.Width, pictureBoxPhasePortrait.Size.Height);
            DPgr = Graphics.FromImage(DPimg);
            PPHgr = Graphics.FromImage(PPHimg);
            PPTgr = Graphics.FromImage(PPTimg);
            DPgr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            PPHgr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            PPTgr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            DPgr.Clear(Color.White);
            PPHgr.Clear(Color.White);
            PPTgr.Clear(Color.White);
        }

        private void buttonResetSettings_Click(object sender, EventArgs e)
        {
            textBoxA1.Text = "45";
            textBoxA2.Text = "25";
            textBoxV1.Text = "0";
            textBoxV2.Text = "0";
            numericUpDownAccuracy.Value = 0.001m;
            numericUpDownL1.Value = 0.15m;
            numericUpDownL2.Value = 0.10m;
            numericUpDownM1.Value = 0.2m;
            numericUpDownM2.Value = 0.3m;
            comboBoxPlanets.SelectedIndex = 2;
        }

        public void buttonStartStop_Click(object sender, EventArgs e)
        {
            startStopMode = !startStopMode;

            if (startStopMode)
            {
                // Изменение интерфейса
                textBoxA1.Enabled = false;
                textBoxA2.Enabled = false;
                textBoxV1.Enabled = false;
                textBoxV2.Enabled = false;
                numericUpDownM1.Enabled = false;
                numericUpDownM2.Enabled = false;
                numericUpDownL1.Enabled = false;
                numericUpDownL2.Enabled = false;
                comboBoxPlanets.Enabled = false;
                numericUpDownAccuracy.Enabled = false;
                buttonResetSettings.Enabled = false;
                buttonStartStop.Text = "Остановить моделирование";
                DPgr.Clear(Color.White);
                PPHgr.Clear(Color.White);

                // Очистка массивов
                phasePath1 = new List<PointF>();
                phasePath2 = new List<PointF>();

                // Принимаем и устанавливаем начальные значения

                double g = 0;

                switch (comboBoxPlanets.SelectedIndex)
                {
                    case 0:
                        g = 0.38 * gEarth;
                        break;
                    case 1:
                        g = 0.904 * gEarth;
                        break;
                    case 2:
                        g = gEarth;
                        break;
                    case 3:
                        g = 0.376 * gEarth;
                        break;
                    case 4:
                        g = 2.53 * gEarth;
                        break;
                    case 5:
                        g = 1.07 * gEarth;
                        break;
                    case 6:
                        g = 0.89 * gEarth;
                        break;
                    case 7:
                        g = 1.14 * gEarth;
                        break;
                    case 8:
                        g = 0.067 * gEarth;
                        break;
                }

                Physics = new Functions(Convert.ToDouble(numericUpDownAccuracy.Value),
                    Convert.ToDouble(numericUpDownM1.Value), Convert.ToDouble(numericUpDownM2.Value),
                    Convert.ToDouble(numericUpDownL1.Value), Convert.ToDouble(numericUpDownL2.Value),
                    g, Convert.ToDouble(textBoxA1.Text) * Math.PI / 180, Convert.ToDouble(textBoxA2.Text) * Math.PI / 180,
                    Convert.ToDouble(textBoxV1.Text) * Math.PI / 180, Convert.ToDouble(textBoxV2.Text) * Math.PI / 180);

                phasePath1.Add(new PointF((float)Physics.ang1, (float)Physics.vel1));
                phasePath2.Add(new PointF((float)Physics.ang2, (float)Physics.vel2));

                DPgr.ResetTransform();
                PPHgr.ResetTransform();
                PPTgr.ResetTransform();

                float zoom = 0.010f;

                DPgr.TranslateTransform(pictureBoxModel.Size.Width / 2, pictureBoxModel.Size.Height / 2);
                float scale = (float)(Physics.l1 + Physics.l2)*5*pictureBoxModel.Size.Height;
                DPgr.ScaleTransform(scale, scale);
                
                PPHgr.TranslateTransform(pictureBoxPhasePath.Size.Width / 2, pictureBoxPhasePath.Size.Height / 2);
                float scalePPHTa = (float)(2 * Math.PI * zoom * pictureBoxPhasePath.Size.Width);
                float scalePPHTv = (float)(Math.Sqrt((Physics.l1 + Physics.l2)* 4 * g) * zoom * pictureBoxPhasePath.Size.Height);
                PPHgr.ScaleTransform(scalePPHTa, scalePPHTv);

                PPTgr.TranslateTransform(pictureBoxPhasePortrait.Size.Width / 2, pictureBoxPhasePortrait.Size.Height / 2);
                scalePPHTa = (float)(2 * Math.PI * zoom * pictureBoxPhasePortrait.Size.Width);
                scalePPHTv = (float)(Math.Sqrt((Physics.l1 + Physics.l2) * 4 * g) * zoom * pictureBoxPhasePortrait.Size.Height);
                PPTgr.ScaleTransform(scalePPHTa, scalePPHTv);

                backgroundWorker.RunWorkerAsync();
            }

            else
            {
                backgroundWorker.CancelAsync();

                // Изменение интерфейса
                textBoxA1.Enabled = true;
                textBoxA2.Enabled = true;
                textBoxV1.Enabled = true;
                textBoxV2.Enabled = true;
                numericUpDownM1.Enabled = true;
                numericUpDownM2.Enabled = true;
                numericUpDownL1.Enabled = true;
                numericUpDownL2.Enabled = true;
                comboBoxPlanets.Enabled = true;
                numericUpDownAccuracy.Enabled = true;
                buttonResetSettings.Enabled = true;
                buttonStartStop.Text = "Начать новое моделирование";
            }
        }
        private void numericUpDownPhasePortrait_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownPhasePortrait.Value == 1)
            { Drawer.DrawPhasePortrait(PPTgr, phasePortrait1); }
            else
            { Drawer.DrawPhasePortrait(PPTgr, phasePortrait2); };
        }

        private void numericUpDownPhasePath_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownPhasePath.Value == 1)
            { Drawer.DrawPhasePath(PPHgr, phasePath1); }
            else
            { Drawer.DrawPhasePath(PPHgr, phasePath2); };
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Drawer.DrawDoublePendulum(DPgr, Physics.GetCoords());

            if (numericUpDownPhasePath.Value == 1)
            {
                PPHgr.DrawLine(Drawer.RokiPen, phasePath1.Last(), new PointF((float)Physics.ang1, (float)Physics.vel1));
            }
            else
            {
                PPHgr.DrawLine(Drawer.RokiPen, phasePath2.Last(), new PointF((float)Physics.ang2, (float)Physics.vel2));
            }
           
            phasePath1.Add(new PointF((float)Physics.ang1, (float)Physics.vel1));
            phasePath2.Add(new PointF((float)Physics.ang2, (float)Physics.vel2));
            pictureBoxModel.Image = DPimg;
            pictureBoxPhasePath.Image = PPHimg;
            pictureBoxPhasePortrait.Image = PPTimg;
            textBoxA1.Text = ((Physics.ang1 * 180 / Math.PI) % 180).ToString();
            textBoxA2.Text = ((Physics.ang2 * 180 / Math.PI) % 180).ToString();
            textBoxV1.Text =((Physics.vel1 * 180 / Math.PI)).ToString();
            textBoxV2.Text = ((Physics.vel2 * 180 / Math.PI)).ToString();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!backgroundWorker.CancellationPending)
            {
                Physics.Step();
                backgroundWorker.ReportProgress(1);
                System.Threading.Thread.Sleep(10);
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (numericUpDownPhasePortrait.Value == 1)
            {
                PPTgr.DrawCurve(Drawer.RokiPen, phasePath1.ToArray());
            }
            else
            {
                PPTgr.DrawCurve(Drawer.RokiPen, phasePath2.ToArray());
            }

            phasePortrait1.Add(phasePath1);
            phasePortrait2.Add(phasePath2);
        }
    }
}
