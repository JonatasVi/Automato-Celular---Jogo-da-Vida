using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace GameConway
{
    internal class Game
    {
        private int lines, columns;
        private Matrix<byte> current;
        private Matrix<byte> next;

        public Game(int x, int y)
        {
            lines = x;
            columns = y;
            current = new Matrix<byte>(x, y, 0);
            next = new Matrix<byte>(x, y, 0);
        }

        private int SumNeighborhood(int x, int y)
        {
            int sum = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if ((i != 0 || j != 0) && current.IsValid(x + i, y + j))
                    {
                        sum += current.GetValue(x + i, y + j);
                    }
                }
            }

            return sum;
        }

        private void ComputeNextGeneration()
        {
            int sum;

            for (int i = 0; i < current.Lines; i++)
            {
                for (int j = 0; j < current.Columns; j++)
                {
                    sum = SumNeighborhood(i, j);

                    if (current.GetValue(i, j) == 1)
                    {
                        if (sum < 2 || sum > 3)
                            next.SetValue(i, j, 0);
                        else
                            next.SetValue(i, j, 1);
                    }

                    if (current.GetValue(i, j) == 0) 
                    {
                        if (sum == 3)
                            next.SetValue(i, j, 1);
                    }
                }
            }
        }

        public void Update()
        {
            ComputeNextGeneration();
            current = next;
            next = new Matrix<byte>(lines, columns, 0);
        }

        public void SetCellState(int x, int y, bool alive)
        {
            if (alive)
                current.SetValue(x, y, 1);
            else
                current.SetValue(x, y, 0);
        }

        public bool GetCellState(int x, int y)
        {
            return current.GetValue(x, y) == 1;
        }

        public Bitmap DrawBitmap(int width, int height, int cellSize)
        {
            Bitmap bitmap = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(bitmap);

            for (int i = 0; i < lines; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    int posX = cellSize * i;
                    int posY = cellSize * j;
                    bool alive = GetCellState(i, j);

                    if (alive)
                    {
                        graphics.FillRectangle(Brushes.Black, posX, posY, cellSize, cellSize);
                        graphics.DrawRectangle(Pens.Black, posX, posY, cellSize, cellSize);
                    }
                    else
                    {
                        graphics.FillRectangle(Brushes.White, posX, posY, cellSize, cellSize);
                        graphics.DrawRectangle(Pens.Black, posX, posY, cellSize, cellSize);
                    }
                }
            }

            return bitmap;
        }

        public bool IsValid(int x, int y)
        {
            return current.IsValid(x, y);
        }

        public override string ToString()
        {
            return current.ToString();
        }
    }
}
