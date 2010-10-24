using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PhysLib
{
   
    public class Matrix
    {

        private int rows,cols;
        private double[] t;

        public Matrix(int NumRows, int NumColumns)
        {
            t = new double[NumRows * NumColumns];
            rows = NumRows;
            cols = NumColumns;
        }

        public Matrix(int NumRows, int NumColumns, double[] Values)
        {
            rows = NumRows;
            cols = NumColumns;
            t = Values;
        }

        public double this[int i,int j]
        {
            get { return t[cols * i + j]; }
            set { t[cols * i + j] = value; }
        }

        public double this[int index]
        {
            get { return t[index]; }
            set { t[index] = value; }
        }

        public int Rows
        {
            get { return rows; }
        }

        public int Columns
        {
            get { return cols; }
        }
        
        public int Dimension
        {
            get { return cols * rows; }
        }

        public static Matrix operator +(Matrix A, Matrix B)
        {
            if (A.Dimension != B.Dimension) throw new MatrixException();

            for (int i = 0; i < A.Dimension; i++)
                A[i] += B[i];
            
            return A;
        }

        public static Matrix operator -(Matrix A, Matrix B)
        {
            if (A.Dimension != B.Dimension) throw new MatrixException();

            for (int i = 0; i < A.Dimension; i++)
                A[i] -= B[i];

            return A;
        }

        public static Matrix operator *(Matrix A, Matrix B)
        {
            if (A.Rows != B.Columns) throw new MatrixException();

            Matrix C = new Matrix(A.Rows, B.Columns);

            for (int i = 0; i < A.Rows; i++)
            {
                for (int j = 0; j < B.Columns; j++)
                {
                    for (int k = 0; k < A.Columns; k++)
                       C[i, j] += A[i, k] * B[k, j];
                }
            }

            return C;
        }

        public double Determinant
        {
            get 
            { 
                double[,] d = new double[rows,cols];
                for (int i = 0;i < rows;i++)
                {
                    for (int j = 0;j < cols;j++)
                       d[i,j] = this[i,j];
                }

                return alglib.rmatrixdet(d);  
            }
        }

        public Vector ToVector()
        {
            if (rows > 1) throw new MatrixException();
            return new Vector(t);
        }

        public static int Epsilon(int i, int j, int k)
        {
            if (i + j + k > 9 || (j < 1 || k < 1 || i < 1)) throw new MatrixException();
            return (i - j) * (j - k) * (k - i) / 2;
        }

        public static int Delta(int i, int j)
        {
            return i == j ? 1 : 0;
        }
    }

    [Serializable]
    public class MatrixException : Exception
    {
        public MatrixException() { }
        public MatrixException(string message) : base(message) { }
        public MatrixException(string message, Exception inner) : base(message, inner) { }
        protected MatrixException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
