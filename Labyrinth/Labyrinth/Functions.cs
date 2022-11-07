using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Labyrinth
{
    class Functions
    {
        const int cellSize = 4;
        static public void GenerateLabyrinth(ref Cell[,] lab)
        {
            Stack<int[]> way = new Stack<int[]>();
            int currW = 0;
            int currH = 0;
            Random rand = new Random();
            int maxCells = lab.GetLength(0) * lab.GetLength(1) - 1;

            while (maxCells > 0)
            {
                lab[currW, currH].wasChecked = true;
                way.Push(new int[2] { currW, currH });
                bool flag = false;
                int rN = rand.Next(0, 3);

                for (int i = 0; i < 4; i++)
                {
                    switch ((rN + i) % 4)
                    {
                        //go Top
                        case 0:
                            if (((currH - 1) >= 0) && (!lab[currW, currH - 1].wasChecked))
                            {
                                lab[currW, currH].wallT = false;
                                currH--;
                                lab[currW, currH].wallD = false;
                                flag = true;
                            }
                            break;
                        //go Down
                        case 1:
                            if (((currH + 1) < lab.GetLength(1) && (!lab[currW, currH + 1].wasChecked)))
                            {
                                lab[currW, currH].wallD = false;
                                currH++;
                                lab[currW, currH].wallT = false;
                                flag = true;
                            }
                            break;
                        //go Left
                        case 2:
                            if (((currW - 1) >= 0) && (!lab[currW - 1, currH].wasChecked))
                            {
                                lab[currW, currH].wallL = false; 
                                currW--;
                                lab[currW, currH].wallR = false;
                                flag = true;
                            }
                            break;
                        //go Right
                        case 3:
                            if (((currW + 1) < lab.GetLength(0)) && (!lab[currW + 1, currH].wasChecked))
                            {
                                lab[currW, currH].wallR = false;
                                currW++;
                                lab[currW, currH].wallL = false;
                                flag = true;
                            }
                            break;
                    }

                    if (flag) {
                        maxCells--;
                        break;
                    }
                }

                if (!flag)
                {
                    way.Pop();
                    var curr = way.Pop();
                    currW = curr[0];
                    currH = curr[1];
                }
            }
            lab[currW, currH].wasChecked = true;
        }
        static public void WriteHeader(BinaryWriter bmwr, int sizeW, int sizeH)
        {
            // Bitmap file header:
            bmwr.Write('B'); bmwr.Write('M');               // Header field;
            bmwr.Write(sizeW * sizeH * (cellSize*cellSize) + 54);             // File byte size;
            bmwr.Write((int)(0));                           // Reserved (app source);
            bmwr.Write((int)(54));                          // Offset;
            // Bitmap info header:
            bmwr.Write((int)(40));                          // Size of bitmap info header;
            bmwr.Write(sizeW * cellSize); bmwr.Write(sizeH * cellSize);   // Dimensions;
            bmwr.Write((short)(1));                         // Number of color planes;
            bmwr.Write((short)(8));                         // Color depth (bpp);
            bmwr.Write((int)(0));                           // Compression method;
            bmwr.Write((int)(sizeW * sizeH * (cellSize * cellSize)));            // Raw bitmap data size;
            bmwr.Write((int)(3780)); bmwr.Write((int)(3780));   // Resolution (ppm);
            bmwr.Write((int)(5));                           // Number of colors in palette;
            bmwr.Write((int)(0));                           // Number of important colors;
            // Color table (palette):
            bmwr.Write(0x00000000);     // Black (walls);
            bmwr.Write(0xFFFFFFFF);     // White (background);
            bmwr.Write(0x00FF8F8F);     // Pink (visited);
            bmwr.Write(0x00008F00);     // Green (pathway);
            bmwr.Write(0x0000008F);     // Blue (entrance);
        }
        static public void DrawLabyrinth(BinaryWriter bmwr, Cell[,]lab, int entryW, int entryH, int exitW, int exitH)
        { 
            for (int j = 0; j < lab.GetLength(1) * cellSize; j++)
            {
                for (int i = 0; i < lab.GetLength(0); i++)
                {
                    byte bg = 1;
                    if(!lab[i,j / cellSize].wasChecked)
                    {
                        bg = 2;
                    }
                    if (lab[i, j / cellSize].way)
                    {
                        bg = 3;
                    }
                    if ((i == entryW) && (j / cellSize == entryH))
                    {
                        bg = 4;
                    }
                    if ((i == exitW) && (j / cellSize == exitH))
                    {
                        bg = 4;
                    }
                    switch (j % cellSize)
                    {
                        case 0:
                            if (lab[i, (j / cellSize)].wallT)
                            {
                                for (int m = 0; m < cellSize; m++)
                                    bmwr.Write((byte)0);
                            }
                            else
                            {
                                bmwr.Write((byte)0);

                                for (int m = 1; m < cellSize - 1; m++)
                                    bmwr.Write(bg);

                                bmwr.Write((byte)0);
                            }
                            break;
                        case (cellSize - 1):
                            if (lab[i, (j / cellSize)].wallD)
                            {
                                for (int m = 0; m < cellSize; m++)
                                    bmwr.Write((byte)0);
                            }
                            else
                            {
                                bmwr.Write((byte)0);

                                for (int m = 1; m < cellSize - 1; m++)
                                    bmwr.Write(bg);

                                bmwr.Write((byte)0);
                            }
                            break;
                        default:
                            if (lab[i, (j / cellSize)].wallL)
                                bmwr.Write((byte)0);
                            else bmwr.Write(bg);

                            for (int m = 1; m < cellSize - 1; m++)
                                bmwr.Write(bg);

                            if (lab[i, (j / cellSize)].wallR)
                                bmwr.Write((byte)0);
                            else bmwr.Write(bg);

                            break;
                    }
                }
            }
            
        }
        static public void SolveLabyrinth(ref Cell[,] lab, int entryW, int entryH, int exitW, int exitH)
        {
            Stack<int[]> way = new Stack<int[]>();
            int currW = entryW;
            int currH = entryH;
            Random rand = new Random();

            while ((currW!=exitW)||(currH!=exitH))
            {
                lab[currW, currH].wasChecked = false;
                way.Push(new int[2] { currW, currH });
                bool flag = false;
                int rN = rand.Next(0, 3);

                for (int i = 0; i < 4; i++)
                {
                    switch ((rN + i) % 4)
                    {
                        //go Top
                        case 0:
                            if ((!lab[currW, currH].wallT) && (lab[currW, currH - 1].wasChecked))
                            {
                                currH--;
                                flag = true;
                            }
                            break;
                        //go Down
                        case 1:
                            if ((!lab[currW, currH].wallD) && (lab[currW, currH + 1].wasChecked))
                            {
                                currH++;
                                flag = true;
                            }
                            break;
                        //go Left
                        case 2:
                            if ((!lab[currW, currH].wallL) && (lab[currW - 1, currH].wasChecked))
                            {
                                currW--;
                                flag = true;
                            }
                            break;
                        //go Right
                        case 3:
                            if ((!lab[currW, currH].wallR) && (lab[currW + 1, currH].wasChecked))
                            {
                                currW++;
                                flag = true;
                            }
                            break;
                    }
                    if (flag)
                    {
                        break;
                    }
                }

                if (!flag)
                {
                    way.Pop();
                    var curr = way.Pop();
                    currW = curr[0];
                    currH = curr[1];
                }
            }
            while (way.Count > 0)
            {
                var curr = way.Pop();
                lab[curr[0], curr[1]].way = true;
            }
        }
    }
}

