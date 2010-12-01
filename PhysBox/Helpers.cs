using System;
using System.Windows.Forms;
using System.Drawing;

using PhysLib;

namespace PhysBox
{
    public partial class MainForm : Form
    {
        private static int GCD(int a, int b)
        {
            int Remainder;
            while (b != 0)
            {
                Remainder = a % b;
                a = b;
                b = Remainder;
            }

            return a;
        }

        private static Point GetPositionOnObject(Point Rel,PointF[] geom, Point In)
        {
            int a = 0, b = 1;
            Point ret = new Point();
            double ad = Double.PositiveInfinity, bd = Double.PositiveInfinity, cd = Double.PositiveInfinity, dist = 0;
            for (int i = 0; i < geom.Length; i++)
            {
                dist = Math.Sqrt(Math.Pow(In.X - (geom[i].X + Rel.X), 2) + Math.Pow(In.Y - (geom[i].Y + Rel.Y), 2));
                if (dist < ad)
                {
                    ad = dist;
                    a = i;
                }
            }

            for (int i = 0; i < geom.Length; i++)
            {
                dist = Math.Sqrt(Math.Pow(In.X - (geom[i].X + Rel.X), 2) + Math.Pow(In.Y - (geom[i].Y + Rel.Y), 2));
                if (dist < bd && i != a)
                {
                    bd = dist;
                    b = i;
                }
            }

            int ax = (int)(geom[b].X - geom[a].X), ay = (int)(geom[b].Y - geom[a].Y), x = (int)geom[a].X, y = (int)geom[a].Y;

            if (ax == 0 && ay == 0) throw new ArgumentException();

            int gcd = GCD(ax, ay);
            ax = ax / Math.Abs(gcd);
            ay = ay / Math.Abs(gcd);

            while (!(x == geom[b].X && y == geom[b].Y))
            {
                Point pt = new Point(x+Rel.X, y+Rel.Y);
                dist = Geometry.PointDistance(pt, In);
                if (dist < cd)
                {
                    cd = dist;
                    ret = pt;
                }
                x += ax;
                y += ay;
            }

            return ret;
        }
    }
    
}