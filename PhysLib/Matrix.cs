using System;
using AlgLib;

namespace PhysLib
{
    /// <summary>
    /// Typ inicializace matice pomocí vektorů
    /// </summary>
    public enum MatrixInitType  { VectorsAreRows = 0, VectorsAreColumns}

    /// <summary>
    /// Reprezentuje matici
    /// </summary>
    public class Matrix
    {

        private int rows,cols;
        private double[] t;

        /// <summary>
        /// Vytvoří matici MxN a vyplní ji nulami
        /// </summary>
        /// <param name="NumRows">Počet M řádku</param>
        /// <param name="NumColumns">Počet N sloupců</param>
        public Matrix(int NumRows, int NumColumns)
        {
            if (NumRows * NumColumns > Vector.MaximumVectorElements * Vector.MaximumVectorElements)
                throw new ArgumentException();
            
            t = new double[NumRows * NumColumns];
            for (int i = 0; i < NumRows * NumColumns; i++)
                t[i] = 0;

            rows = NumRows;
            cols = NumColumns;
        }

        /// <summary>
        /// Vytvoří matici MxN a vyplní ji danými hodnotami
        /// </summary>
        /// <param name="NumRows">Počet M řádků</param>
        /// <param name="NumColumns">Počet N sloupců</param>
        /// <param name="Values">Hodnoty</param>
        public Matrix(int NumRows, int NumColumns,params double[] Values)
        {
            if (NumRows * NumColumns > Vector.MaximumVectorElements * Vector.MaximumVectorElements)
                throw new ArgumentException();

            rows = NumRows;
            cols = NumColumns;
            
            t = new double[NumRows * NumColumns];
            for (int i = 0; i < NumRows * NumColumns; i++)
            {
                if (i >= Values.Length)
                    t[i] = 0;
                else t[i] = Values[i];
            }
        }

        /// <summary>
        /// Vytvoří matici zkopírováním jiné
        /// </summary>
        /// <param name="Clone">Vzorová matice</param>
        public Matrix(Matrix Clone)
        {
            CopyFrom(Clone);
        }

        /// <summary>
        /// Vytvoří matici pomocí vektorů
        /// </summary>
        /// <param name="Type">Typ inicializace matice vektory</param>
        /// <param name="Vectors">Vektory</param>
        public Matrix(MatrixInitType Type,params Vector[] Vectors)
        {
            t = new double[Vectors[0].Count * Vectors.Length];
            int ctr = 0;

            if (Type == MatrixInitType.VectorsAreRows)
            {
                cols = Vectors[0].Count;
                rows = Vectors.Length;

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++,ctr++)
                        t[ctr] = Vectors[i][j];
                }
            }
            else
            {
                rows = Vectors[0].Count;
                cols = Vectors.Length;

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++,ctr++)
                        t[ctr] = Vectors[j][i];
                }
            }
        }

        /// <summary>
        /// Získá/změní prvek matice na daných souřadnicích
        /// </summary>
        /// <param name="i">Souřadnice řádku</param>
        /// <param name="j">Souřadnice sloupce</param>
        /// <returns>Prvek matice</returns>
        public double this[int i, int j]
        {
            get { return t[cols * i + j]; }
            set { t[cols * i + j] = value; }
        }

        /// <summary>
        /// Získá/změní i-tý prvek matice
        /// </summary>
        /// <param name="i">Lineární souřadnice</param>
        /// <returns>Prvek matice</returns>
        public double this[int i]
        {
            get { return t[i]; }
            set { t[i] = value; }
        }

        /// <summary>
        /// Počet řádků matice
        /// </summary>
        public int Rows
        {
            get { return rows; }
        }

        /// <summary>
        /// Počet sloupců matice
        /// </summary>
        public int Columns
        {
            get { return cols; }
        }
        
        /// <summary>
        /// Dimenze matice
        /// </summary>
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

        /// <summary>
        /// Zkopíruje prvky matice z jiné
        /// </summary>
        /// <param name="M">Vzorová matice</param>
        public void CopyFrom(Matrix M)
        {
            cols = M.Columns;
            rows = M.Rows;
            
            t = new double[cols * rows];
            for (int i = 0; i < rows*cols; i++)            
                t[i] = M[i];            
        }

        /// <summary>
        /// Spočítá determinant matice
        /// </summary>
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


        /// <summary>
        /// Transponuje matici danou matici
        /// </summary>
        /// <param name="M">Matice, která se má transponovat</param>
        public static Matrix Transpose(Matrix M)
        {
            Matrix Ret = new Matrix(M.Columns, M.Rows);
            for (int i = 0; i < M.Rows; i++)
            {
                for (int j = 0; j < M.Columns; j++)
                    Ret[j,i] = M[i, j];                
            }
            
            return Ret;
        }

        /// <summary>
        /// Převede matici o jednom řádku na vektor
        /// </summary>
        /// <returns>Vektor</returns>
        public Vector ToVector()
        {
            if (rows > 1) throw new MatrixException();
            return new Vector(t);
        }

        /// <summary>
        /// Získá řádek matice jako vektor
        /// </summary>
        /// <param name="Row">Souřadnice řádku</param>
        /// <returns>Vektor</returns>
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

        /// <summary>
        /// Získá sloupec matice jako vektor
        /// </summary>
        /// <param name="Column">Souřadnice sloupce</param>
        /// <returns>Vektor</returns>
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

        /// <summary>
        /// Získá prvek totálně antisymetrického tenzoru (Levi-Civitův symbol)
        /// </summary>
        /// <param name="i">i-tá součadnice</param>
        /// <param name="j">j-tá souřadnice</param>
        /// <param name="k">k-tá souřadnice</param>
        /// <returns>Číslo -1,0 nebo 1</returns>
        public static int Epsilon(int i, int j, int k)
        {
            if (i + j + k > 9 || (j < 1 || k < 1 || i < 1)) throw new MatrixException();
            return (i - j) * (j - k) * (k - i) / 2;
        }

        /// <summary>
        /// Kronekerovo delta
        /// </summary>
        /// <param name="i">i-tá souřadnice</param>
        /// <param name="j">j-tá souřadnice</param>
        /// <returns>1 nebo 0</returns>
        public static int Delta(int i, int j)
        {
            return i == j ? 1 : 0;
        }

        /// <summary>
        /// Vytvoří matici 3D rotace
        /// </summary>
        /// <param name="Phi">Úhel při ose Z</param>
        /// <param name="Theta">Úhel při ose X</param>
        /// <param name="Psi">Úhel při ose Y</param>
        /// <returns>Matice rotace</returns>
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

    /// <summary>
    /// Vyjímka nastávající při operaci s maticí
    /// </summary>
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
