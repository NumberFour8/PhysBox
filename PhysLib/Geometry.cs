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
        
        private PointF center;
        private PointF[] geom;
        private double height,width;

        public Geometry(PointF[] Vertices,PointF InitPosition,float AngleX = 0)
        {
            ObjectGeometry = Vertices;
            
            Position = InitPosition;
            Orientation = World.B;

            if (AngleX / Math.PI != 0)
               Orientation *= Matrix.Make3DRotation(AngleX, 0, 0);
        }

        public Vector Position
        {
            get; set;
        }

        public Matrix Orientation
        {
            get; set;
        }

        public double OrientationOf(Matrix RefFrame,Axes Axis)
        {
            return Vector.Angle(RefFrame.GetRow((int)Axis - 1), Orientation.GetRow((int)Axis - 1));
        }

        public PointF[] ObjectGeometry
        {
            get { return geom; }
            set
            {
                PointF top = value[0], bot = value[0], left = value[0], right = value[0];
                float s = 0;

                for (int i = 0; i < value.Length - 1; i++)
                    s += value[i].X * value[i + 1].Y - value[i].Y * value[i + 1].X;

                for (int i = 0; i < value.Length - 1; i++)
                {
                    center.X += (value[i].X + value[i + 1].X) * (value[i].X * value[i + 1].Y - value[i].Y * value[i + 1].X);
                    center.Y += (value[i].Y + value[i + 1].Y) * (value[i].X * value[i + 1].Y - value[i].Y * value[i + 1].X);

                    if (left.X > value[i+1].X) left = value[i+1];
                    if (right.X < value[i+1].X) right = value[i+1];

                    if (bot.X > value[i+1].Y) bot = value[i+1];
                    if (top.X < value[i+1].Y) top = value[i+1];
                }

                center.X /= 3*s;
                center.Y /= 3*s;

                height = top.Y - bot.Y;
                width = right.X - left.X;

                geom   = value;
            }
        }

        public PointF Centroid
        {
            get { return center; }    
        }

        public double Height 
        {
            get { return height; }
        }

        public double Width
        {
            get { return width; }
        }

        public double Volume
        {
            get; set;
        }

        public double Surface
        {
            get; set;
        }
    }
}
