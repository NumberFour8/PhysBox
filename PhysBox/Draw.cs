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

        public GraphicObject(Brush Texture, PointF[] Geometry,PointF COG)
            : base(Geometry, new PointF(0, 0), COG)
        {
            Fill = Texture;
            ShowVectors = false;
            Name = "obj_" + (DateTime.Now.Minute + DateTime.Now.Second).ToString();
            Tension = 0;
        }

        public GraphicsPath MakePath()
        {
            GraphicsPath Ret = new GraphicsPath();
            Ret.AddClosedCurve(ObjectGeometry, Tension);            
            return Ret;
        }
    }

    public partial class MainForm : Form
    {

        private BufferedGraphicsContext Ctx;
        public const int CursorCorrection = -23;
        private readonly Font SmallFont = new Font(FontFamily.GenericSansSerif, 7);

        private void RenderAllObjects(Graphics Destination)
        {
            foreach (SimObject o in MyWorld.Objects)
            {
                GraphicObject obj = o.Model as GraphicObject;
                GraphicsPath pth = obj.MakePath();
                if (menu_enableWireframe.Checked)
                {
                    Pen p = Pens.Black;
                    if (Selected != null && obj.Name == (Selected.Model as GraphicObject).Name)
                    {
                        p = Pens.Violet;
                        if (menu_showVertices.Checked)
                        {
                            foreach (PointF pt in pth.PathPoints)
                                Destination.FillRectangle(Brushes.Navy, new RectangleF(pt.X - 2, pt.Y - 2, 4, 4));
                        }
                    }

                    Destination.DrawPath(p, pth);
                    Destination.FillEllipse(Brushes.Black, new RectangleF((float)(obj.Position[0] - 2), (float)(obj.Position[1] - 2), 4, 4));
                    Destination.FillEllipse(Brushes.Blue, new RectangleF((float)(o.RotationPoint[0] - 2),(float)(o.RotationPoint[1] - 2), 4, 4));
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

                if (menu_showVersion.Checked)
                    buf.Graphics.DrawString("PhysBox, v.0.1", new Font(FontFamily.GenericSansSerif, 12), Brushes.Black, new PointF(10, mainMenu.Height + 5));

                if (AddForce && Selected != null && afOrigin != null)
                {
                    buf.Graphics.FillEllipse(Brushes.Red, new RectangleF(afOrigin.Value.X - 4, afOrigin.Value.Y - 4, 8, 8));

                    Point pt = Cursor.Position;
                    pt.Y += CursorCorrection;

                    double size = Math.Round(Geometry.PointDistance(new PointF(Cursor.Position.X,Cursor.Position.Y),afOrigin.Value) * MyWorld.Resolution, 2);
                    buf.Graphics.DrawString(String.Format("F = {0} N", size), SmallFont, Brushes.Red, new PointF(afOrigin.Value.X - 50, afOrigin.Value.Y + 10));
                    
                    Vector p = Selected.GetP((Vector)afOrigin - (new Vector(pt.X,pt.Y)),(Vector)afOrigin);
                    buf.Graphics.DrawLine(Pens.Gray,(PointF)p,(PointF)Selected.RotationPoint);
                    buf.Graphics.FillRectangle(Brushes.Gray,(float)p[0]-2,(float)p[1]-2,2,2);
                    buf.Graphics.DrawLine(Pens.Red, pt, (PointF)p);

                }

                if (menu_showResolution.Checked)
                {
                    if (MyWorld.Resolution > 500)
                    {
                        buf.Graphics.DrawString(String.Format("0,1 m = {0} px", MyWorld.Resolution / 10), SmallFont, Brushes.IndianRed, new PointF(30, Size.Height - 90));
                        buf.Graphics.DrawLine(Pens.IndianRed, new PointF(30, Size.Height - 70), new PointF(30 + (float)MyWorld.Resolution / 10, Size.Height - 70));
                    }
                    else
                    {
                        buf.Graphics.DrawString(String.Format("1 m = {0} px", MyWorld.Resolution), SmallFont, Brushes.IndianRed, new PointF(30, Size.Height - 90));
                        buf.Graphics.DrawLine(Pens.IndianRed, new PointF(30, Size.Height - 70), new PointF(30 + (float)MyWorld.Resolution, Size.Height - 70));
                    }
                }

                RenderAllObjects(buf.Graphics);
                buf.Render();
            }
        }
    }
}