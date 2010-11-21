using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using AlgLib;

namespace PhysLib
{

    public enum MatrixInitType  { VectorsAreRows = 0, VectorsAreColumns}

    public class Matrix
    {

        private int rows,cols;
        private double[] t;

        public Matrix(int NumRows, int NumColumns)
        {
            if (NumRows * NumColumns > Vector.MaximumVectorElements * Vector.MaximumVectorElements)
                throw new ArgumentException();
            
            t = new double[NumRows * NumColumns];
            rows = NumRows;
            cols = NumColumns;
        }

        public Matrix(int NumRows, int NumColumns,params double[] Values)
        {
            if (NumRows * NumColumns > Vector.MaximumVectorElements * Vector.MaximumVectorElements)
                throw new ArgumentException();

            rows = NumRows;
            cols = NumColumns;
            t = Values;
        }

        public Matrix(Matrix Clone)
        {
            CopyFrom(Clone);
        }

        public Matrix(MatrixInitType Type,params Vector[] Vectors)
        {
            t = new double[Vectors[0].Size * Vectors.Length];
            int ctr = 0;

            if (Type == MatrixInitType.VectorsAreRows)
            {
                cols = Vectors[0].Size;
                rows = Vectors.Length;

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++,ctr++)
                        t[ctr] = Vectors[i][j];
                }
            }
            else
            {
                rows = Vectors[0].Size;
                cols = Vectors.Length;

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++,ctr++)
                        t[ctr] = Vectors[j][i];
                }
            }
        }

        public double this[int i, int j]
        {
            get { return t[cols * i + j]; }
            set { t[cols * i + j] = value; }
        }

        public double this[int i]
        {
            get { return t[i]; }
            set { t[i] = value; }
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

        public void CopyFrom(Matrix M)
        {
            cols = M.Columns;
            rows = M.Rows;
            
            t = new double[cols * rows];
            for (int i = 0; i < rows*cols; i++)            
                t[i] = M[i];            
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

        public Vector GetRow(int Row)
        {
            if (Row >= rows) throw new MatrixException();

            Vector Ret = new Vector(cols);
            for (int i = 0; i < cols; i++)
            {
                Ret[i] = this[Row, i];
            }
            return Ret;
        }

        public Vector GetColumn(int Column)
        {
            if (Column >= cols) throw new MatrixException();

            Vector Ret = new Vector(rows);
            for (int i = 0; i < rows; i++)
            {
                Ret[i] = this[i, Column];
            }
            return Ret;
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

        public static Matrix Make3DRotation(double Phi,double Theta,double Psi)
        {
            Matrix RotMatrix = new Matrix(3, 3);
                   
            RotMatrix[0, 0] = Math.Cos(Theta) * Math.Cos(Psi);
            RotMatrix[0, 1] = -Math.Cos(Phi) * Math.Sin(Psi) + Math.Sin(Phi) * Math.Sin(Theta) * Math.Cos(Psi);
            RotMatrix[0, 2] = Math.Sin(Phi) * Math.Sin(Psi) + Math.Cos(Phi) * Math.Sin(Theta) * Math.Cos(Psi);

            RotMatrix[1, 0] = Math.Cos(Theta) * Math.Sin(Psi);
            RotMatrix[1, 1] = Math.Cos(Phi) * Math.Cos(Psi) + Math.Sin(Phi) * Math.Sin(Theta) * Math.Sin(Psi);
            RotMatrix[1, 2] = -Math.Sin(Phi) * Math.Cos(Psi) + Math.Cos(Phi) * Math.Sin(Theta) * Math.Sin(Psi);

            RotMatrix[2, 0] = -Math.Sin(Theta);
            RotMatrix[2, 1] = Math.Sin(Phi) * Math.Cos(Theta);
            RotMatrix[2, 2] = Math.Cos(Phi) * Math.Cos(Theta);

            return RotMatrix;
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
