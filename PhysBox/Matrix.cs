using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PhysBox
{
    public class Matrix<T>
    {

        private int rows,cols;
        private T[] inner;

        public Matrix(int Rows, int Columns)
        {
            inner = new T[Rows * Columns];
            rows = Rows;
            cols = Columns;
        }

        public T this[int i,int j]
        {
            get { return inner[cols * j + i]; }
            set { inner[cols * j + i] = value; }
        }
    }
}
