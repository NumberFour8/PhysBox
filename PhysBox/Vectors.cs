using System;

namespace PhysBox
{
    public class Vector
    {
        private double x, y, z;

        public Vector(double vX, double vY, double vZ = 0)
        {
            x = vX; y = vY; z = vZ;
        }

        public Vector(double Size,double Angle)
        {
            x = Size * Math.Cos(Angle);
            y = Size * Math.Sin(Angle);
            z = 0;
        }

        public double Magnitude
        {
            get {   return Math.Sqrt(x * x + y * y + z * z);  }
        }

        public bool IsNull
        {
            get { return x == 0 && y == 0 && z == 0; }
        }

        public double X
        {
            get {   return x;  }
        }

        public double Y
        {
            get {   return y;  }
        }

        public double Z
        {
            get {   return z;  }
        }

        public Vector Perpendicular
        {
            get
            {
                if (Z == 0)
                 return new Vector(-this.Y, this.X);
                else return Cross(this, new Vector(this.X, this.Y, -this.Z));
            }
        }

        public static Vector Unit(Vector v)
        {
            return new Vector(Math.Sign(v.X),Math.Sign(v.Y),Math.Sign(v.Z));
        }

        public static double Dot(Vector v1,Vector v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public static Vector Cross(Vector v1,Vector v2)
        {
            return new Vector(v1.Z * v2.Y - v1.Y * v2.Z, v1.X * v2.Z - v1.Z * v2.X, v1.Y * v2.X - v1.X * v2.Y);
        }

        public static double Angle(Vector v1, Vector v2)
        {            
            return Math.Acos(Vector.Dot(v1,v2) / (v1.Magnitude * v2.Magnitude));
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y,v1.Z+v2.Z);
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static Vector operator *(Vector v1, Vector v2)
        {
            return Cross(v1, v2);
        }

        public static Vector operator *(Vector v1, double v)
        {
            return new Vector(v1.X * v, v1.Y * v, v1.Z * v);
        }

        public static bool operator ==(Vector v1, Vector v2)
        {
            Vector a = Unit(v1),b = Unit(v2);
            return v1.Magnitude == v2.Magnitude && (a.X == b.X && a.Y == b.Y && a.Z == b.Z);
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector)
                return (Vector)obj == this;
            else return false;
        }

        public override int GetHashCode()
        {
            return (x * y * z).ToString().GetHashCode();
        }

        public static bool operator !=(Vector v1, Vector v2)
        {
            return !(v1 == v2);
        }

        public static implicit operator Vector(double d)
        {
            return new Vector(d, d, d);
        }

        public static implicit operator double(Vector v)
        {
            return v.Magnitude;
        }

        public override string ToString()
        {
            return String.Format("({0:F3},{1:F3},{2:F3})",x,y,z);
        }

        public static double PerpDot(Vector v1, Vector v2)
        {
            return Dot(v1.Perpendicular, v2);
        }

    }
}
