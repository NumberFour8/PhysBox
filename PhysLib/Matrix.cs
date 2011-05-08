using System;

namespace PhysLib
{
    /// <summary>
    /// Typ inicializace matice pomocí vektorů
    /// </summary>
    public enum MatrixInitType  { VectorsAreRows = 0, VectorsAreColumns}

    /// <summary>
    /// Reprezentuje matici
    /// </summary>
    public sealed class Matrix
    {

        private int rows,cols;
        private double[] t;

        /// <summary>
        /// Vytvoří nulovou matici MxN
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
        /// Vytvoří jednotkovou čtvercovou matici daného řádu.
        /// </summary>
        /// <param name="MatOrder">Řád matice</param>
        public Matrix(int MatOrder)
        {
            if (MatOrder < 1) throw new ArgumentException();
            t = new double[MatOrder * MatOrder];
            rows = cols = MatOrder;
            
            Reset();
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
        /// Zjistí, zda je matice čtvercová
        /// </summary>
        public bool Square
        {
            get { return rows == cols; }
        }

        /// <summary>
        /// Zjistí řád čtvercové matice
        /// </summary>
        public int Order
        {
            get
            {
                if (!Square) return 0;
                return rows;
            }
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
            if (!A.Square || !B.Square) throw new MatrixException();

            for (int i = 0; i < A.Dimension; i++)
                A[i] += B[i];
            
            return A;
        }

        public static Matrix operator -(Matrix A, Matrix B)
        {
            if (!A.Square || !B.Square) throw new MatrixException();

            for (int i = 0; i < A.Dimension; i++)
                A[i] -= B[i];

            return A;
        }

        public static Matrix operator *(Matrix A, Matrix B)
        {
            if (A.Columns != B.Rows) throw new MatrixException();

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
            
            t = new double[Dimension];
            for (int i = 0; i < Dimension; i++)            
                t[i] = M[i];            
        }

        /// <summary>
        /// Indikuje zda je daná čtvercová matice diagonální
        /// </summary>
        public bool Diagonal
        {
            get
            {
                if (!Square) return false;
                for (int i = 0; i < Dimension; i++)
                {
                    if (t[i] != 0 && i % (Order + 1) != 0) return false;
                }
                return true;
            }
        }

        /// <summary>
        /// Vynuluje matici
        /// </summary>
        public void Null()
        {
            for (int i = 0; i < Dimension; i++)
                t[i] = 0;
        }

        /// <summary>
        /// Vytvoří diagonální čtvercovou jednotkovou matici
        /// </summary>
        public void Reset()
        {
            if (!Square) throw new MatrixException();
            for (int i = 0; i < Dimension; i++)
            {
                if (i % (Order + 1) == 0) t[i] = 1;
                else t[i] = 0;
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
        /// Převede matici o jednom řádku nebo sloupci na vektor
        /// </summary>
        /// <returns>Vektor</returns>
        public Vector ToVector()
        {
            if (rows > 1 && cols > 1) throw new MatrixException();
            return new Vector(t);
        }

        /// <summary>
        /// Získá řádek matice jako vektor
        /// </summary>
        /// <param name="Row">Souřadnice řádku</param>
        /// <returns>Vektor</returns>
        public Vector GetRowAsVector(int Row)
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
        public Vector GetColumnAsVector(int Column)
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
        /// Získá prvek totálně antisymetrického tenzoru (Levi-Civitův)
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


    /// <summary>
    /// Statická třída pro běžné 2D transformace
    /// </summary>
    public class Transform2D
    {
        private Transform2D() { }

        /// <summary>
        /// Vytvoří 2D transformační matici rotace podle počátku o daný úhel (pro homogenní souřadnice)
        /// </summary>
        /// <param name="Angle">Úhel otočení v radiánech</param>
        /// <returns>Transformační matice rotace</returns>
        public static Matrix Rotate(double Angle)
        {
            Matrix Rot = new Matrix(3);
            Rot[0, 0] = Math.Cos(Angle); Rot[0, 1] = Math.Sin(Angle); Rot[0, 2] = 1;
            Rot[1, 0] = -Math.Sin(Angle); Rot[1, 1] = Math.Cos(Angle); Rot[1, 2] = 0;
            Rot[2, 0] = 0; Rot[2, 1] = 0; Rot[2, 2] = 0;

            return Rot;
        }
        
        /// <summary>
        /// Vytvoří 2D transformační matici rotace podle dané osy o daný úhel (pro homogenní souřadnice)
        /// </summary>
        /// <param name="Angle">Úhel otočení v radiánech</param>
        /// <param name="Axis">Osa otočení</param>
        /// <returns>Transformační matice rotace</returns>
        public static Matrix Rotate(double Angle, Vector Axis)
        {
            Matrix Rot = new Matrix(3);
            Rot[0, 0] = Math.Cos(Angle); Rot[0, 1] = -Math.Sin(Angle); Rot[0, 2] = -Axis[0] * Math.Cos(Angle) + Axis[1] * Math.Sin(Angle) + Axis[0];
            Rot[1, 0] = Math.Sin(Angle); Rot[1, 1] = Math.Cos(Angle);  Rot[1, 2] =  -Axis[0] * Math.Sin(Angle) - Axis[1] * Math.Cos(Angle) + Axis[1];
            Rot[2, 0] = 0; Rot[2, 1] = 0; Rot[2, 2] = 1;

            return Rot ;
        }

        /// <summary>
        /// Vytvoří 2D transformační matici posunutí o daný vektor (pro homogenní souřadnice)
        /// </summary>
        /// <param name="To">Vektor posunutí</param>
        /// <returns>Transformační matice translace</returns>
        public static Matrix Translate(Vector To)
        {
            Matrix Trans = new Matrix(3);
            Trans[0, 0] = 1; Trans[0, 1] = 0; Trans[0, 2] = To[0];
            Trans[1, 0] = 0; Trans[1, 1] = 1; Trans[1, 2] = To[1];
            Trans[2, 0] = 0; Trans[2, 1] = 0; Trans[2, 2] = 1;

            return Trans;
        }

        /// <summary>
        /// Vytvoří 2D transformační matici škálování s daným škálovacím faktorem (pro homogenní souřadnice)
        /// </summary>
        /// <param name="Factor">Škálovací faktor</param>
        /// <returns>Transformační matice škálování</returns>
        public static Matrix Scale(Vector Factor)
        {
            Matrix Scale = new Matrix(3);

            Scale[0, 0] = Factor[0]; Scale[0, 1] = 0;         Scale[0, 2] = 0;
            Scale[1, 0] = 0;         Scale[1, 1] = Factor[1]; Scale[1, 2] = 0;
            Scale[2, 0] = 0;         Scale[2, 1] = 0;         Scale[2, 2] = 1;

            return Scale;
        }

        /// <summary>
        /// Vytvoří 2D transformační matici škálování a translace s daným škálovacím faktorem a posunutím
        /// </summary>
        /// <param name="Factor">Škálovací faktor</param>
        /// <param name="At">Vektor posunutí</param>
        /// <returns>Transformační matice škálování a translace</returns>
        public static Matrix Scale(Vector Factor, Vector At)
        {
            Matrix Scale = new Matrix(3);

            Scale[0, 0] = Factor[0]; Scale[0, 1] = 0;         Scale[0, 2] = -At[0] * Factor[0] + At[0] ;
            Scale[1, 0] = 0;         Scale[1, 1] = Factor[1]; Scale[1, 2] = -At[1] * Factor[1] + At[1];
            Scale[2, 0] = 0;         Scale[2, 1] = 0;         Scale[2, 2] = 1;

            return Scale;
        }

        /// <summary>
        /// Aplikuje 2D transformaci danou maticí na vektory v homogenní soustavě souřadné
        /// </summary>
        /// <param name="Vectors">Vektory, na které má být aplikována transformace</param>
        public static Vector[] TransformVectors(Matrix Transform,params Vector[] Vectors)
        {
            if (Transform.Order != 3) throw new MatrixException();

            for (int i = 0,c = 0; i < Vectors.Length; i++)
            {
                Vector T = Vectors[i];
                c = T.Count;
                
                T.Count = 3; T[2] = 1;
                Vectors[i] = (Transform * T.ToMatrix(MatrixInitType.VectorsAreColumns)).ToVector();
                
                Vectors[i].Count = c;
            }
            return Vectors;
        }

        /// <summary>
        /// Aplikuje 2D transformaci danou maticí na body v homogenní soustavě souřadné
        /// </summary>
        /// <param name="Points">Body, na které má být aplikována transformace</param>
        public static System.Drawing.PointF[] TransformPoints(Matrix Transform, params System.Drawing.PointF[] Points)
        {
            if (Transform.Order != 3) throw new MatrixException();

            for (int i = 0; i < Points.Length; i++)
            {
                Vector T = new Vector(Points[i].X, Points[i].Y, 1);
                T = (Transform * T.ToMatrix(MatrixInitType.VectorsAreColumns)).ToVector();

                Points[i].X = (float)T[0]; Points[i].Y = (float)T[1];
            }
            return Points;
        }


    }
}
