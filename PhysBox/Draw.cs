using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using PhysLib;

namespace PhysBox
{

    public class GraphicObject : Geometry
    {
        public Pen Style { get; set; }

        public GraphicObject(Color Line,PointF vPosition,PointF[] Geometry) 
            : base (Geometry,vPosition)
        {
            Style = new Pen(Line);            
        }

        public GraphicObject(Color Line,PointF vPosition, PointF[] Geometry,float Angle,float Height,float Width,PointF Centroid)
            : base(Geometry, vPosition, Angle, Height, Width, Centroid)
        {
            Style = new Pen(Line);
        }

        public GraphicsPath MakePath()
        {
            GraphicsPath Ret = new GraphicsPath();
            Ret.AddClosedCurve(ObjectGeometry);
            
            System.Drawing.Drawing2D.Matrix Trans = new System.Drawing.Drawing2D.Matrix();
            Trans.RotateAt(OrientationOf(World.B, Axes.X), Nail); 
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