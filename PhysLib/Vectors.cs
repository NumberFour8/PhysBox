using System;

namespace PhysLib
{
    public class Vector
    {
        private double[] t;
        public const uint MaximumVectorElements = 20;

        public Vector(params double[] Values)
        {
            if (Values.Length > MaximumVectorElements)
                throw new ArgumentException();

            t = new double[Values.Length];
            t = Values;
        }

        public Vector(int Count)
        {
            if (Count > MaximumVectorElements)
                throw new ArgumentException();

            t = new double[Count];
        }

        public int Size
        {
            get { return t.Length; }
        }

        public void Null()
        {
            for (int i = 0; i < t.Length; i++)
                t[i] = 0;
        }

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

        public bool IsNull
        {
            get { return Magnitude == 0; }
        }

        public double this[int index]
        {
            get
            {
                if (index >= Size)
                    return 0;
                else return t[index]; 
            }
            set { t[index] = value; }
        }

        public static Vector Unit(Vector v)
        {
            for (int i = 0; i < v.Size;i++)
               v[i] = (double)Math.Sign(v[i]);

            return v;
        }

        public static double Dot(Vector v, Vector u)
        {
            double Ret = 0;
            for (int i = 0; i < v.Size; i++)
                Ret += v[i] * u[i];
            return Ret;
        }

        public static Vector Cross(Vector u, Vector v)
        {
            if (u.Size > 3 || v.Size > 3) throw new NotImplementedException();
            return new Vector(v[3] * u[2] - v[2] * u[3], v[1] * u[3] - v[3] * u[1], v[2] * u[1] - v[1] * u[2]);
        }

        public static double Angle(Vector v, Vector u)
        {            
            return Math.Acos(Vector.Dot(v,u) / (v.Magnitude * u.Magnitude));
        }

        public static Vector operator +(Vector v, Vector u)
        {
            for (int i = 0; i < Math.Max(v.Size, u.Size); i++)
               v[i] += u[i];
            
            return v;
        }

        public static Vector operator -(Vector v, Vector u)
        {
            for (int i = 0; i < Math.Max(v.Size, u.Size); i++)
                v[i] -= u[i];

            return v;
        }

        public static Vector operator *(Vector v, Vector u)
        {
            return Cross(v, u);
        }

        public static Vector operator *(Matrix M,Vector v)
        {
            return (M * v.ToMatrix()).ToVector();
        }

        public static Vector operator *(Vector v, double k)
        {
            for (int i = 0; i < v.Size; i++)
                v[i] *= k;
            
            return v;
        }

        public static bool operator ==(Vector v, Vector u)
        {
            for (int i = 0; i < Math.Max(v.Size, u.Size); i++)
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

        public static implicit operator Vector(double d)
        {
            return new Vector(d, d, d);
        }

        public static implicit operator Vector(System.Drawing.PointF p)
        {
            return new Vector(p.X, p.Y);
        }

        public static implicit operator double(Vector v)
        {
            return v.Magnitude;
        }

        public static implicit operator float(Vector v)
        {
            return (float)v.Magnitude;
        }

        public Matrix ToMatrix()
        {
            return new Matrix(1, Size, t);
        }

        public override string ToString()
        {
            string Ret = String.Empty;
            for (int i = 0;i < Size;i++)
            {
                Ret += String.Format("{0:F0}", this[i]);
            }
            return "(" + Ret + ")";
        }
    }
}
