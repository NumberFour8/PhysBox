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
        public float Tension { get; set; }

        public GraphicObject(Brush Texture,PointF vPosition, PointF[] Geometry,float Angle,float Height,float Width,PointF Centroid)
            : base(Geometry, vPosition, Angle, Height, Width, Centroid)
        {
            Fill = Texture;
            ShowVectors = false;
            Name = "obj_" + (DateTime.Now.Minute + DateTime.Now.Second).ToString();
            Tension = 0;
        }

        public GraphicsPath MakePath()
        {
            GraphicsPath Ret = new GraphicsPath();
            Ret.AddClosedCurve(ObjectGeometry,Tension);
            
            System.Drawing.Drawing2D.Matrix Trans = new System.Drawing.Drawing2D.Matrix();
            Trans.RotateAt((float)Orientation[0], Nail); 
            Trans.Translate((float)Position[0],(float)Position[1],MatrixOrder.Append);

            Ret.Transform(Trans);            

            return Ret;
        }
    }

    public partial class MainForm : Form
    {

        private BufferedGraphicsContext Ctx;
        public const int CursorCorrection = -23;

        private void RenderAllObjects(Graphics Destination,bool WireFrame,bool ShowVectors)
        {
            foreach (SimObject o in MyWorld.Objects)
            {
                GraphicObject obj = o.Model as GraphicObject;
                if (WireFrame)
                {
                    Pen p = Pens.Black;
                    if (Selected != null && obj.Name == (Selected.Model as GraphicObject).Name)
                    {
                        p = Pens.Violet;
                        if (afOrigin != null && AddForce)
                          Destination.DrawLine(Pens.Gray, new PointF(afOrigin.Value.X,afOrigin.Value.Y), new PointF(obj.Nail.X + (float)obj.Position[0], obj.Nail.Y + (float)obj.Position[1]));
                    }

                    Destination.DrawPath(p, obj.MakePath());
                    Destination.FillEllipse(Brushes.Black, new RectangleF((float)obj.Position[0] - 2.0f, (float)obj.Position[1] - 2, 4, 4));
                    Destination.FillEllipse(Brushes.Blue, new RectangleF(obj.Nail.X + (float)obj.Position[0] - 2.0f, obj.Nail.Y + (float)obj.Position[1] - 2.0f, 4, 4));
                }
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

                if (AddForce && Selected != null && afOrigin != null)
                {
                    buf.Graphics.FillEllipse(Brushes.Red, new Rectangle(afOrigin.Value.X - 4, afOrigin.Value.Y - 4, 8, 8));

                    Point pt = Cursor.Position;
                    pt.Y += CursorCorrection;

                    buf.Graphics.DrawLine(Pens.Red, pt, afOrigin.Value);
                    double size = Math.Round(Math.Sqrt(Math.Pow(Cursor.Position.X - afOrigin.Value.X, 2) + Math.Pow(Cursor.Position.Y - afOrigin.Value.Y, 2)) * MyWorld.Resolution, 2);
                    buf.Graphics.DrawString(String.Format("{0} N", size), new Font(FontFamily.GenericSansSerif, 7), Brushes.Red, new PointF(afOrigin.Value.X - 50, afOrigin.Value.Y + 10));
                }

                RenderAllObjects(buf.Graphics, drátovýModelToolStripMenuItem.Checked, kreslitVektoryToolStripMenuItem.Checked);
                buf.Render();
            }
        }
    }
}