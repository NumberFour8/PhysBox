using System;

namespace PhysLib
{
    /// <summary>
    /// Třída reprezentující vektor
    /// </summary>
    public class Vector
    {
        private double[] t;
        
        /// <summary>
        /// Maximální počet složek vektoru
        /// </summary>
        public const uint MaximumVectorElements = 20;

        /// <summary>
        /// Konstruktor vektoru
        /// </summary>
        /// <param name="Values">Jednotlivé složky v novém vektoru</param>
        public Vector(params double[] Values)
        {
            if (Values.Length > MaximumVectorElements)
                throw new ArgumentException();

            t = new double[Values.Length];
            t = Values;
        }

        /// <summary>
        /// Konstruktor vektoru
        /// </summary>
        /// <param name="Count">Počet složek v novém vektoru inicializované na 0</param>
        public Vector(int Count)
        {
            if (Count > MaximumVectorElements)
                throw new ArgumentException();

            t = new double[Count];
            for (int i = 0; i < Count; i++)
                t[i] = 0;
        }

        /// <summary>
        /// Počet složek ve vektoru
        /// </summary>
        public int Count
        {
            get { return t.Length; }
        }

        /// <summary>
        /// Vynuluje všechny složky vektoru (vytvoří z něj nulový)
        /// </summary>
        public void Null()
        {
            for (int i = 0; i < t.Length; i++)
                t[i] = 0;
        }

        /// <summary>
        /// Nulový vektor
        /// </summary>
        public static readonly Vector Zero = new Vector(0);

        /// <summary>
        /// Velikost vektoru
        /// </summary>
        public double Magnitude
        {
            get 
            {   
                double agg = 0;
                for (int i = 0; i < t.Length; i++)
                    agg += Math.Pow(t[i], 2);
                return Math.Sqrt(agg);
            }
        }

        /// <summary>
        /// Indikuje, zda je vektor jednotkový
        /// </summary>
        public bool IsUnit
        {
            get { return Magnitude == 1; }
        }

        /// <summary>
        /// Indikuje, zda je vektor nulový
        /// </summary>
        public bool IsNull
        {
            get { return Magnitude == 0; }
        }

        /// <summary>
        /// Přístup k jednotlivým složkám vektoru
        /// </summary>
        /// <param name="index">Index složky vektoru</param>
        /// <returns>Složka vektoru</returns>
        public double this[int index]
        {
            get
            {
                if (index >= Count)
                    return 0;
                else return t[index]; 
            }
            set { t[index] = value; }
        }

        /// <summary>
        /// Udělá z daného vektoru jednotkový vektor ukazující pouze směr
        /// </summary>
        /// <param name="v">Vstupní vektor</param>
        /// <returns>Jednotkový vektor</returns>
        public static Vector Unit(Vector v)
        {
            for (int i = 0; i < v.Count;i++)
               v[i] = (double)Math.Sign(v[i]);

            return v;
        }

        /// <summary>
        /// Provede skalární součin dvou vektorů
        /// </summary>
        /// <param name="v">První vektor</param>
        /// <param name="u">Druhé vektor</param>
        /// <returns>Skalár</returns>
        public static double Dot(Vector v, Vector u)
        {
            double Ret = 0;
            for (int i = 0; i < v.Count; i++)
                Ret += v[i] * u[i];
            return Ret;
        }

        /// <summary>
        /// Provede vektorový součin dvou vektorů
        /// </summary>
        /// <param name="u">První vektor</param>
        /// <param name="v">Druhý vektor</param>
        /// <returns>Vektor kolmý k oběma daným vektorům</returns>
        public static Vector Cross(Vector u, Vector v)
        {
            if (u.Count > 3 || v.Count > 3) throw new NotImplementedException();
            return new Vector(v[3] * u[2] - v[2] * u[3], v[1] * u[3] - v[3] * u[1], v[2] * u[1] - v[1] * u[2]);
        }

        /// <summary>
        /// Zjistí úhel, který svírají dva vektory
        /// </summary>
        /// <param name="v">První vektor</param>
        /// <param name="u">Druhé vektor</param>
        /// <returns>Úhel mezi vektory</returns>
        public static double Angle(Vector v, Vector u)
        {            
            return Math.Acos(Vector.Dot(v,u) / (v.Magnitude * u.Magnitude));
        }

        /// <summary>
        /// Vyjádří daný vektor v bázi zadané vektory ze sloupců dané diagonální matice
        /// </summary>
        /// <param name="B">Matice báze</param>
        /// <param name="V">Vektor, který se má převést</param>
        /// <returns>Vektor A v bázi B</returns>
        public static Vector ToBasis(Matrix B, Vector A)
        {
            if (!B.Diagonal || B.Columns != A.Count)
                throw new MatrixException();

            Vector Ret = new Vector(A.Count);
            for (int i = 0, j = 0; i < B.Dimension; i += B.Columns + 1, j++)
                Ret[j] = B[i] * A[j];

            return Ret;
        }

        public static Vector operator +(Vector v, Vector u)
        {
            Vector Ret = new Vector(Math.Max(v.Count,u.Count));
            for (int i = 0; i < Ret.Count; i++)
               Ret[i] = v[i]+u[i];
            
            return Ret;
        }

        public static Vector operator -(Vector v)
        {
            Vector Ret = new Vector(v.Count);
            for (int i = 0; i < v.Count; i++)
                Ret[i] = -v[i];
            
            return Ret;
        }

        public static Vector operator -(Vector v, Vector u)
        {
            Vector Ret = new Vector(Math.Max(v.Count, u.Count));
            for (int i = 0; i < Ret.Count; i++)
                Ret[i] = v[i] - u[i];

            return Ret;
        }     

        public static Vector operator *(Vector v, Vector u)
        {
            return Cross(v, u);
        }

        public static Vector operator *(Vector v,Matrix M)
        {            
            return (M * v.ToMatrix(MatrixInitType.VectorsAreColumns)).ToVector();
        }

        public static Vector operator *(Vector v, double k)
        {
            Vector Ret = new Vector(v.Count);
            for (int i = 0; i < v.Count; i++)
                Ret[i] = v[i]*k;
            
            return Ret;
        }

        public static Vector operator *(double k, Vector v)
        {
            return v * k;
        }

        public static Vector operator /(Vector v, double k)
        {
            if (k == 0) throw new DivideByZeroException();

            return v*(1/k);
        }

        public static Vector operator /(double k,Vector v)
        {
            throw new InvalidOperationException();

        }

        public static bool operator ==(Vector v, Vector u)
        {
            for (int i = 0; i < Math.Max(v.Count, u.Count); i++)
               if (v[i] != u[i]) return false;
            
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector)
                return (Vector)obj == this;
            else return false;
        }

        public override int GetHashCode()
        {
            return Magnitude.ToString().GetHashCode();
        }

        public static bool operator !=(Vector v1, Vector v2)
        {
            return !(v1 == v2);
        }

        public static explicit operator Vector(double d)
        {
            return new Vector(d, d, d);
        }

        public static explicit operator Vector(System.Drawing.PointF p)
        {
            return new Vector(p.X,p.Y,0); // !
        }

        public static explicit operator System.Drawing.PointF(Vector v)
        {
            return new System.Drawing.PointF((float)v[0],(float)v[1]);
        }

        public static explicit operator double(Vector v)
        {
            return v.Magnitude;
        }

        /// <summary>
        /// Převede vektor na matici o jednom řádku/sloupci
        /// </summary>
        /// <returns>Matice</returns>
        public Matrix ToMatrix(MatrixInitType Type)
        {
            if (Type == MatrixInitType.VectorsAreRows)
                return new Matrix(1, Count, t);
            else return new Matrix(Count, 1, t);
        }

        public override string ToString()
        {
            string Ret = String.Empty;
            for (int i = 0;i < Count;i++)
            {
                Ret += String.Format("{0:F0}", this[i]);
            }
            return "(" + Ret + ")";
        }
    }
}
