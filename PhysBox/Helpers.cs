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

        private static PointF GetPositionOnObject(PointF[] TransformedVertices, Point In)
        {
            int a = 0, b = 1;
            PointF Ret = new PointF();            
            double ad = double.PositiveInfinity, bd = 0, cd = double.PositiveInfinity, dist = 0;
            for (int i = 0; i < TransformedVertices.Length; i++)
            {
                dist = Math.Sqrt(Math.Pow(In.X - TransformedVertices[i].X, 2) + Math.Pow(In.Y - TransformedVertices[i].Y, 2));
                if (dist < ad)
                {
                    bd = ad;
                    ad = dist;
                    b = a;
                    a = i;
                }
                else if (dist < bd)
                {
                    bd = dist;
                    b = i;
                }
            }

            float ax = TransformedVertices[b].X - TransformedVertices[a].X, ay = TransformedVertices[b].Y - TransformedVertices[a].Y, x = TransformedVertices[a].X, y = TransformedVertices[a].Y,max;

            if (ax == ay)
            {
                ax = ay = Math.Sign(ax);
                max = 1;
            }
            else max = Math.Max(Math.Abs(ax), Math.Abs(ay));

            while (x != TransformedVertices[b].X || y != TransformedVertices[b].Y)
            {
                PointF pt = new PointF(x, y);
                dist = Geometry.PointDistance(pt, In);
                if (dist < cd)
                {
                    cd = dist;
                    Ret = pt;
                }
                if (dist > cd) break;
               
                x += ax / max;
                y += ay / max;
            }

            return Ret;
        }
    }
    
}