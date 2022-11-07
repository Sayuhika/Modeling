using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TriangulateAndPotential
{
    public partial class FormMain : Form
    {
        Bitmap img;
        Graphics gr;
        Model model;
        List<Triangle> triangles;
        Physic Phys;
        double k, kx;
        double Q;
        public FormMain()
        {
            InitializeComponent();

            img = new Bitmap(pictureBox.Size.Width, pictureBox.Size.Height);
            gr = Graphics.FromImage(img);
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            gr.Clear(Color.Black);
            gr.ResetTransform();
        }

        private void buttonGenGrid_Click(object sender, EventArgs e)
        {
            double R, D, L;
            int gridX, gridY;
            Phys = null;

            R = (double)numericUpDownR.Value;
            D = (double)numericUpDownD.Value;
            L = (double)numericUpDownL.Value;
            gridX = (int)numericUpDownMaxX.Value;
            gridY = (int)numericUpDownMaxY.Value;

            kx = (gridX+1) / (gridY+1) * 0.5;
            k = pictureBox.Height;

            model = new Model(gridX, gridY, R, D, L);
            List<PointDP> grid = model.allDots;
            List<Triangle> tris = Triangulate.BowyerWatsonTriangulate(grid, kx * 2);
            triangles = Triangulate.CutFigure(tris, model.fDots);

            Draw();
        }

        private void buttonGenPhysic_Click(object sender, EventArgs e)
        {
            Q = (float)numericUpDownQ.Value;

            Random rand = new Random();

            Phys = new Physic(physicCulculateComplite, null, null);
            Phys.points = model.allDots;
            Triangulate.CreateNeighbours(ref Phys.points, ref triangles);
            Phys.triangles = triangles;

            // Инициализация граничных значений и непосредственно самих границ. 
            for (int i = 0; i < Phys.points.Count; i++)
            {
                for (int j = 0; j < model.fDots.Count; j++)
                {
                    if (Phys.points[i] == model.fDots[j])
                    {
                        Phys.points[i].border = true;

                        if (Phys.points[i].X < 0)
                        { Phys.points[i].potential = Q * (1 + (rand.NextDouble() - 0.5f) * 0.00001); }
                        else
                        { Phys.points[i].potential = - Q * (1 + (rand.NextDouble() - 0.5f) * 0.00001); }

                        break;
                    }
                    // Установка малых случайных начальных значений.
                    if (!Phys.points[i].border) Phys.points[i].potential = (rand.NextDouble() - 0.5f) * 0.01f;
                }          
            }

            // Начало расчета физики задачи.
            Phys.CalculateAsync();
        }

        private void checkBoxGrid_CheckedChanged(object sender, EventArgs e)
        {
            Draw();
        }

        private void checkBoxIsolines_CheckedChanged(object sender, EventArgs e)
        {
            Draw();
        }

        private void physicCulculateComplite(object sender, System.ComponentModel.RunWorkerCompletedEventArgs rcea)
        {
            Draw();
        }

        private void checkBoxPL_CheckedChanged(object sender, EventArgs e)
        {
            Draw();
        }

        public void Draw()
        {
            gr.Clear(Color.Black);
            if (checkBoxGrid.Checked){Drawer.DrawTriangles(gr, triangles, k, kx);}
            if (Phys != null)
            {
                if (checkBoxIsolines.Checked) { Drawer.DrawIsolines(gr, Phys.Isolines, k, kx); };
                if (checkBoxFieldLines.Checked) { Drawer.DrawFieldLines(gr, Phys.FieldLines, k, kx); };
                Drawer.DrawPhysic(gr, Phys.points, k, kx);
            }
            pictureBox.Image = img;
        }
    }
}
