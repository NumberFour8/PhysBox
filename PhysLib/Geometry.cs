using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
namespace PhysLib
{

    public abstract class Geometry
    {
        public event EventHandler OnChange;
        public event EventHandler OnCollision;
        
        private PointF cog;
        private PointF[] geom;

        public Vector Position
        {
            get;
            set;
        }

        public Matrix Orientation
        {
            get;
            set;
        }

        public double OrientationOf(Vector RefAxis,Axes Axis)
        {
            return Vector.Angle(RefAxis, Orientation.GetRow((int)Axis - 1));
        }

        public PointF[] ObjectGeometry
        {
            get { return geom; }
            set
            {
                float s = 0;
                for (int i = 0; i < value.Length - 1; i++)
                    s += value[i].X * value[i + 1].Y - value[i].Y * value[i + 1].X;

                for (int i = 0; i < value.Length - 1; i++)
                {
                    cog.X += (value[i].X + value[i + 1].X) * (value[i].X * value[i + 1].Y - value[i].Y * value[i + 1].X);
                    cog.Y += (value[i].Y + value[i + 1].Y) * (value[i].X * value[i + 1].Y - value[i].Y * value[i + 1].X);
                }
                cog.X /= 3*s;
                cog.Y /= 3*s;
                geom   = value;
            }
        }

        public PointF COG
        {
            get { return cog; }    
        }

        public double Volume
        {
            get;
            set;
        }

        public double Surface
        {
            get;
            set;
        }
    }
}
