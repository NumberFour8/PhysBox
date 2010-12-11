using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using PhysLib;

namespace PhysBox
{

    // Odvozená třída, reprezentující grafický podklad pro těleso
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
            Name = "obj_" + (DateTime.Now.Minute + DateTime.Now.Second).ToString(); // "Náhodný" název
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
                        if (menu_showVertices.Checked) // Je-li třeba vykresli vertexy objektu
                        {
                            foreach (PointF pt in pth.PathPoints)
                                Destination.FillRectangle(Brushes.Navy, new RectangleF(pt.X - 2, pt.Y - 2, 4, 4));
                        }
                    }

                    // Nakresli obrys objektu, jeho těžiště a osu rotace
                    Destination.DrawPath(p, pth);
                    Destination.FillEllipse(Brushes.Black, new RectangleF((float)(obj.Position[0] - 2), (float)(obj.Position[1] - 2), 4, 4)); // Těžiště
                    Destination.FillEllipse(Brushes.Blue, new RectangleF((float)(o.RotationPoint[0] - 2),(float)(o.RotationPoint[1] - 2), 4, 4)); // Osa rotace

                    // Je-li třeba vykresli i ohraničující box tělesa
                    if (menu_showBounds.Checked)
                      Destination.DrawClosedCurve(Pens.Gray, obj.BoundingBox, 0, FillMode.Alternate);
                }
                else Destination.FillPath(obj.Fill, obj.MakePath()); // Jinak nakresli objekt vyplněný texturou/barvou
            }
        }

        private int UpdateCounter = 0;
        void MyWorld_OnTick(object sender, EventArgs e)
        {
            Rectangle WorldRect = new Rectangle(0, 0, Width, Height);
            using (BufferedGraphics buf = Ctx.Allocate(CreateGraphics(), WorldRect))
            {
                buf.Graphics.FillRectangle(Brushes.White, WorldRect);

                if (AddForce && Selected != null && afOrigin != null)
                {
                    buf.Graphics.FillEllipse(Brushes.Red, new RectangleF(afOrigin.Value.X - 4, afOrigin.Value.Y - 4, 8, 8));

                    Point pt = Cursor.Position;
                    pt.Y += CursorCorrection;

                    // Nakresli čáru symbolizující aplikovanou sílu
                    double size = multiplier*Selected.Mass*Math.Round(MyWorld.Convert(Geometry.PointDistance(new PointF(Cursor.Position.X,Cursor.Position.Y),afOrigin.Value),ConversionType.PixelsToMeters), 2);
                    buf.Graphics.DrawString(String.Format("F = {0} N", size), SmallFont, Brushes.Red, new PointF(afOrigin.Value.X - 50, afOrigin.Value.Y + 10));
                    
                    // Nakresli rameno síly
                    Vector p = Selected.GetTorqueIntersection((Vector)afOrigin - (new Vector(pt.X,pt.Y)),(Vector)afOrigin);
                    buf.Graphics.DrawLine(Pens.Gray,(PointF)p,(PointF)Selected.RotationPoint);
                    buf.Graphics.FillRectangle(Brushes.Gray,(float)p[0]-2,(float)p[1]-2,2,2);
                    buf.Graphics.DrawLine(Pens.Red, pt, (PointF)p);
                }

                if (SetLevel)
                   buf.Graphics.DrawLine(Pens.DarkViolet, new PointF(0, Cursor.Position.Y + CursorCorrection), new PointF(Size.Width, Cursor.Position.Y + CursorCorrection)); 

                if (menu_showVersion.Checked) // Vykresli verzi
                    buf.Graphics.DrawString("PhysBox, v.0.2", new Font(FontFamily.GenericSansSerif, 12), Brushes.Black, new PointF(10, mainMenu.Height + 5));

                if (menu_drawInfo.Checked) // Vykresli informace o modelu
                {
                    string Info = String.Format("g: {0} m.s^-2\nRozlišení: {1}", MyWorld.Convert(MyWorld.Gravity, ConversionType.PixelsToMeters).Magnitude, MyWorld.Resolution);
                    buf.Graphics.DrawString(Info, new Font(FontFamily.GenericSansSerif, 11), Brushes.Black, Size.Width - 150, Size.Height - 120);

                    // Vykresli měřítko rozlišení
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

                    // Vykresli nulovou hladinu
                    buf.Graphics.DrawLine(Pens.Silver, new PointF(0, (float)MyWorld.Level[1]), new PointF(Size.Width, (float)MyWorld.Level[1]));

                }

                if (Tools != null && !Tools.IsDisposed && UpdateCounter >= 10)
                {
                    if (Selected != null)
                    {
                        ObjectEnergy eng = MyWorld.GetObjectEnergy(Selected,ConversionType.PixelsToMeters);
                        Tools.prop_Velocity.Text = String.Format("{0} m/s", Math.Round(MyWorld.Convert(Selected.LinearVelocity,ConversionType.PixelsToMeters).Magnitude, 2));
                        Tools.prop_angularVelocity.Text = String.Format("{0} rad/s", Math.Round(Selected.AngularVelocity.Magnitude,2));
                        Tools.prop_kineticEnergy.Text = String.Format("{0} J", Math.Round(eng.Kinetic,2));
                        Tools.prop_potentialEnergy.Text = String.Format("{0} J", Math.Round(eng.Potential,2));
                        Tools.prop_rotationalEnergy.Text = String.Format("{0} J", Math.Round(eng.Rotational, 2));
                    }
                    UpdateCounter = 0;
                }
                
                RenderAllObjects(buf.Graphics);
                buf.Render();
                ++UpdateCounter;
            }
        }
    }
}