using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Labyrinth
{
    public struct Cell
    {
        public bool wallT, wallD, wallL, wallR, wasChecked, way;
    }
    public partial class FormMain : Form
    {
        int labWidht, labHigh, labEntryW, labEntryH, labExitW, labExitH;

        private void textBoxLabHigh_TextChanged(object sender, EventArgs e)
        {
            textBoxEntryH.Text = textBoxLabHigh.Text;
        }

        private void textBoxLabWidth_TextChanged(object sender, EventArgs e)
        {
            textBoxExitW.Text = textBoxLabWidth.Text;
        }

        Cell[,] lab;
        private void buttonResolve_Click(object sender, EventArgs e)
        {
            buttonGenAndDraw.Enabled = false;
            buttonResolve.Enabled = false;

            // Получаем значения координат точек входа и выхода
            labEntryW = (int)Convert.ToDouble(textBoxEntryW.Text) - 1;
            labEntryH = (int)Convert.ToDouble(textBoxEntryH.Text) - 1;

            labExitW = (int)Convert.ToDouble(textBoxExitW.Text) - 1;
            labExitH = (int)Convert.ToDouble(textBoxExitH.Text) - 1;

            // Решаем лабиринт
            Functions.SolveLabyrinth(ref lab, labEntryW, labEntryH, labExitW, labExitH);
            var fs = new FileStream("Labyrinth.bmp", FileMode.Create, FileAccess.Write);
            var bmwr = new BinaryWriter(fs);
            Functions.WriteHeader(bmwr, labWidht, labHigh);
            Functions.DrawLabyrinth(bmwr, lab, labEntryW, labEntryH, labExitW, labExitH);
            bmwr.Flush();
            bmwr.Close();

            buttonGenAndDraw.Enabled = true;
            buttonResolve.Enabled = true;
        }

        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonGenAndDraw_Click(object sender, EventArgs e)
        {
            buttonGenAndDraw.Enabled = false;
            // Получаем значения параметров лабиринта, координаты точек входа и выхода
            labWidht = (int)Convert.ToDouble(textBoxLabWidth.Text);
            labHigh  = (int)Convert.ToDouble(textBoxLabHigh.Text);

            labEntryW = (int)Convert.ToDouble(textBoxEntryW.Text) - 1;
            labEntryH = (int)Convert.ToDouble(textBoxEntryH.Text) - 1;

            labExitW = (int)Convert.ToDouble(textBoxExitW.Text) - 1;
            labExitH = (int)Convert.ToDouble(textBoxExitH.Text) - 1;

            // Заполнение полей лабиринта, создание изначальной решетки.          
            lab = new Cell[labWidht, labHigh];

            for (int i = 0; i < lab.GetLength(0); i++)
            {
                for (int j = 0; j < lab.GetLength(1); j++)
                {
                    lab[i, j].wallT = true;
                    lab[i, j].wallD = true;
                    lab[i, j].wallL = true;
                    lab[i, j].wallR = true;
                    lab[i, j].wasChecked = false;
                    lab[i, j].way = false;
                }
            }

            Functions.GenerateLabyrinth(ref lab);

            var fs = new FileStream("Labyrinth.bmp", FileMode.Create, FileAccess.Write);
            var bmwr = new BinaryWriter(fs);
            Functions.WriteHeader(bmwr, labWidht, labHigh);
            Functions.DrawLabyrinth(bmwr, lab, labEntryW, labEntryH, labExitW, labExitH);
            bmwr.Flush();
            bmwr.Close();

            buttonGenAndDraw.Enabled = true;
            buttonResolve.Enabled = true;
        }
    }
}
