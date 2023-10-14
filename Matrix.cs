using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GameConway
{
    internal class Matrix<T>
    {
        public int Lines { get; private set; }
        public int Columns { get; private set; }

        private T[] data;

        public Matrix(int x, int y, T value)
        {
            data = new T[x * y];
            Lines = x;
            Columns = y;

            for (int i = 0; i < Lines; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    SetValue(i, j, value);
                }
            }
        }

        public bool IsValid(int x, int y)
        {
            return (x >= 0 && x < Lines) && (y >= 0 && y < Columns);
        }

        public T GetValue(int x, int y)
        {
            return data[x * Columns + y];
        }

        public void SetValue(int x, int y, T value)
        {
            data[x * Columns + y] = value;
        }

        public override string ToString()
        {
            string text = string.Empty;
            
            for (int i = 0; i < Lines; i++)
            {
                string line = string.Empty;

                for (int j = 0; j < Columns; j++)
                {
                    line += GetValue(i, j);

                    if (j < Columns - 1)
                        line += ", ";
                }

                text += line;

                if (i < Lines - 1)
                    text += "\n";
            }

            return text;
        }
    }
}
