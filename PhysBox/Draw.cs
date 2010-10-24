using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using System.Collections;

using PhysLib;

namespace PhysBox
{

    public class GraphicObject : Geometry
    {
        public Pen Style { get; set; }

        public GraphicObject(Vector vPosition,Point[] Geometry,Color Line)
        {
            Style = new Pen(Line);


            ObjectGeometry = new PointF[Geometry.Length];
            for (int i = 0; i < Geometry.Length; i++)
               ObjectGeometry[i] = new PointF((float)Geometry[i].X,(float)Geometry[i].Y);
            
            Position = vPosition;
        }

        public GraphicsPath MakePath()
        {
            GraphicsPath Ret = new GraphicsPath();
            Ret.AddClosedCurve(ObjectGeometry);
            
            System.Drawing.Drawing2D.Matrix Trans = new System.Drawing.Drawing2D.Matrix();
            Trans.RotateAt((float)Orientation,new PointF(0,0));
            Trans.Translate((float)Position[0],(float)Position[1],MatrixOrder.Append);

            Ret.Transform(Trans);
            
            return Ret;
        }
    }

    public partial class MainForm : Form
    {

        private ArrayList Actors;

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach (GraphicObject o in Actors.ToArray(typeof(GraphicObject)))
            {
                g.DrawPath(o.Style, o.MakePath());
            }

            base.OnPaint(e);
        }

    }
}