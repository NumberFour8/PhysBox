using System;

namespace PhysLib
{
    /// <summary>
    /// Třída reprezentující vektor
    /// </summary>
    [Serializable]
    public sealed class Vector
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
        /// Zjistí jestli vektor obsahuje nekonečno nebo nedefinovaný výraz
        /// </summary>
        public bool IsNaN
        {
            get
            {
                for (int i = 0; i < Count; i++)
                    if (Double.IsNaN(t[i]) || Double.IsInfinity(t[i])) return true;
                return false;
            }
        }

        /// <summary>
        /// Nulový vektor o třech složkách
        /// </summary>
        public static Vector Zero { get { return new Vector(3); } }

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
            if (v.IsNull) return new Vector(v.Count);

            Vector Ret = new Vector(v.Count);
            for (int i = 0; i < v.Count;i++)
               Ret[i] = v[i]/v.Magnitude;

            return Ret;
        }

        /// <summary>
        /// Provede skalární součin dvou vektorů
        /// </summary>
        /// <param name="v">První vektor</param>
        /// <param name="u">Druhé vektor</param>
        /// <returns>Skalár</returns>
        public static double Dot(Vector v, Vector u)
        {
            if (v.Count != u.Count) throw new ArgumentException();
            double Ret = 0;
            for (int i = 0; i < v.Count; i++)
                Ret += v[i] * u[i];
            return Ret;
        }

        /// <summary>
        /// Vrátí 2D kolmý vektor
        /// </summary>
        /// <returns>Kolmý vektor</returns>
        public Vector Perp()
        {
            return new Vector(-t[1],t[0],0);
        }

        /// <summary>
        /// Spočítá "skalární mocninu" vektoru
        /// </summary>
        /// <param name="v">Vektor</param>
        /// <param name="n">Mocnitel</param>
        /// <returns>Velikost vektoru na ntou</returns>
        public static double Pow(Vector v, uint n)
        {
            return Math.Pow(v.Magnitude, n);
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
            return new Vector(v[2] * u[1] - v[1] * u[2], v[0] * u[2] - v[2] * u[0], v[1] * u[0] - v[0] * u[1]);
        }

        /// <summary>
        /// Zjistí úhel, který svírají dva vektory
        /// </summary>
        /// <param name="v">První vektor</param>
        /// <param name="u">Druhé vektor</param>
        /// <returns>Úhel mezi vektory</returns>
        public static double Angle(Vector v, Vector u)
        {
            if (v.IsNull || u.IsNull) throw new ArgumentException();
            return Math.Acos(Vector.Dot(v,u) / (v.Magnitude * u.Magnitude));
        }

        /// <summary>
        /// Převede daný vektor do báze zadané bázovými vektory jakožto sloupce dané diagonální matice
        /// </summary>
        /// <param name="B">Matice báze</param>
        /// <param name="A">Vektor, který se má převést</param>
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

        /// <summary>
        /// Usekne desetinnou část všech prvků ve vektoru
        /// </summary>
        /// <param name="v">Vektor</param>
        /// <returns>Vektor se složkami bez desetinných částí</returns>
        public static Vector Truncate(Vector v)
        {
            Vector Ret = new Vector(v.Count);
            if (v.IsNull) return Ret;
            for (int i = 0; i < v.Count; i++)            
                Ret[i] = Math.Truncate(v[i]);
            
            return Ret;
        }


        /// <summary>
        /// Převede všechny prvky ve vektoru na jejich horní celé části
        /// </summary>
        /// <param name="v">Vektor</param>
        /// <returns>Horní celá část vektoru</returns>
        public static Vector Ceiling(Vector v)
        {
            Vector Ret = new Vector(v.Count);
            if (v.IsNull) return Ret;
            for (int i = 0; i < v.Count; i++)
                Ret[i] = Math.Ceiling(v[i]);

            return Ret;
        }
        
        /// <summary>
        /// Převede všechny prvky ve vektoru na jejich dolní celé části
        /// </summary>
        /// <param name="v">Vektor</param>
        /// <returns>Dolní celá část vektoru</returns>
        public static Vector Floor(Vector v)
        {
            Vector Ret = new Vector(v.Count);
            if (v.IsNull) return Ret;
            for (int i = 0; i < v.Count; i++)
                Ret[i] = Math.Floor(v[i]);

            return Ret;
        }


        /// <summary>
        /// Zaokrouhlí všechny prvky vektoru s danou přesností
        /// </summary>
        /// <param name="v">Vektor k zaokrouhlení</param>
        /// <param name="decimals">Přesnost zaokrouhlování</param>
        /// <returns>Zaokrouhlený vektor</returns>
        public static Vector Round(Vector v,int decimals)
        {
            Vector Ret = new Vector(v.Count);
            if (v.IsNull) return Ret;
            for (int i = 0; i < v.Count; i++)
                Ret[i] = Math.Round(v[i],decimals);

            return Ret;
        }

        /// <summary>
        /// Spočítá vzdálenost mezi dvěma body
        /// </summary>
        /// <param name="a">Bod A</param>
        /// <param name="b">Bod B</param>
        /// <returns>Vzdálenost |AB|</returns>
        public static double PointDistance(Vector a, Vector b)
        {
            if (a.Count != b.Count) throw new ArgumentException();
            double ret = 0;
            for (int i = 0; i < a.Count; i++)
                ret += Math.Pow(a[i] - b[i], 2);
            
            return Math.Sqrt(ret);
        }

        public static Vector operator+ (Vector v, Vector u)
        {
            Vector Ret = new Vector(Math.Max(v.Count,u.Count));
            for (int i = 0; i < Ret.Count; i++)
               Ret[i] = v[i]+u[i];
            
            return Ret;
        }

        public static Vector operator- (Vector v)
        {
            Vector Ret = new Vector(v.Count);
            for (int i = 0; i < v.Count; i++)
                Ret[i] = -v[i];
            
            return Ret;
        }

        public static Vector operator- (Vector v, Vector u)
        {
            Vector Ret = new Vector(Math.Max(v.Count, u.Count));
            for (int i = 0; i < Ret.Count; i++)
                Ret[i] = v[i] - u[i];

            return Ret;
        }     

        public static Vector operator* (Vector v, Vector u)
        {
            return Cross(v, u);
        }

        public static Vector operator* (Vector v,Matrix M)
        {            
            return (M * v.ToMatrix(MatrixInitType.VectorsAreColumns)).ToVector();
        }

        public static Vector operator* (Vector v, double k)
        {
            Vector Ret = new Vector(v.Count);
            for (int i = 0; i < v.Count; i++)
                Ret[i] = v[i]*k;
            
            return Ret;
        }

        public static Vector operator* (double k, Vector v)
        {
            Vector Ret = new Vector(v.Count);
            for (int i = 0; i < v.Count; i++)
                Ret[i] = v[i] * k;

            return Ret;
        }

        public static Vector operator/ (Vector v, double k)
        {
            if (k == 0) throw new DivideByZeroException();

            return v*(1/k);
        }

        public static Vector operator/ (double k,Vector v)
        {
            throw new InvalidOperationException();
        }

        public static bool operator== (Vector v, Vector u)
        {
            if (v as object == null && u as object == null) return true;
            else if (v as object == null) return false;
            else if (u as object == null) return false;
            else
            {
                for (int i = 0; i < Math.Max(v.Count, u.Count); i++)
                    if (v[i] != u[i]) return false;

                return true;
            }
        }

        public static bool operator!= (Vector v, Vector u)
        {
            return !(v == u);
        }

        public static bool operator< (Vector v, Vector u)
        {
            return v.Magnitude < u.Magnitude;
        }

        public static bool operator> (Vector v, Vector u)
        {
            return v.Magnitude > u.Magnitude;
        }

        public static bool operator<= (Vector v, Vector u)
        {
            return v.Magnitude < u.Magnitude || v == u;
        }

        public static bool operator>= (Vector v, Vector u)
        {
            return v.Magnitude > u.Magnitude || v == u;
        }

        public override bool Equals(object obj)
        {
            return obj as Vector == this;
        }

        public override int GetHashCode()
        {
            return Magnitude.ToString().GetHashCode();
        }

        public static explicit operator Vector(System.Drawing.PointF p)
        {
            return new Vector(p.X,p.Y,0); // !
        }

        public static explicit operator System.Drawing.PointF(Vector v)
        {
            return new System.Drawing.PointF((float)v[0],(float)v[1]);
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

        /// <summary>
        /// Převede vektor na text
        /// </summary>
        /// <returns>Zápis vektoru v řádku</returns>
        public override string ToString()
        {
            base.ToString();
            string Ret = String.Empty;
            if (Count == 0) return "(0)";
            for (int i = 0;i < Count;i++)
                Ret += String.Format("{0:F2};", this[i]);
            return "(" + Ret.Remove(Ret.Length-1) + ")";
        }
    }
}
