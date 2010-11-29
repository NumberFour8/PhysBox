using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using PhysLib;

namespace PhysBox
{

    public class GraphicObject : Geometry
    {
        public Brush Fill { get; set; }
        public bool ShowVectors { get; set; }
        public string Name { get; set; }

        public GraphicObject(Brush Texture,PointF vPosition, PointF[] Geometry,float Angle,float Height,float Width,PointF Centroid)
            : base(Geometry, vPosition, Angle, Height, Width, Centroid)
        {
            Fill = Texture;
            ShowVectors = false;
            Name = "obj_" + (DateTime.Now.Minute + DateTime.Now.Second).ToString();
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

        private BufferedGraphicsContext Ctx;

        private void RenderAllObjects(Graphics Destination,bool WireFrame,bool ShowVectors)
        {
            foreach (SimObject o in MyWorld.Objects)
            {
                GraphicObject obj = o.Model as GraphicObject;
                if (WireFrame)
                    Destination.DrawPath(Pens.Black, obj.MakePath());
                else Destination.FillPath(obj.Fill, obj.MakePath());
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);  
        }

        void MyWorld_OnTick(object sender, EventArgs e)
        {
            Rectangle WorldRect = new Rectangle(0, 0, Width, Height);
            using (BufferedGraphics buf = Ctx.Allocate(CreateGraphics(), WorldRect))
            {
                buf.Graphics.FillRectangle(Brushes.White, WorldRect);

                if (verzeToolStripMenuItem.Checked)
                    buf.Graphics.DrawString("PhysBox, v.0.1", new Font(FontFamily.GenericSansSerif, 12), Brushes.Black, new PointF(10, mainMenu.Height + 5));

                RenderAllObjects(buf.Graphics, drátovýModelToolStripMenuItem.Checked, kreslitVektoryToolStripMenuItem.Checked);
                buf.Render();
            }
        }
    }
}