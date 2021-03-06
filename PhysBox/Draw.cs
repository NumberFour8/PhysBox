﻿using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using PhysLib;

namespace PhysBox
{

    // Vykreslovací kód
    public partial class MainForm : Form
    {

        private BufferedGraphicsContext Ctx;
        public const int CursorCorrection = -23;

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
                    if (menu_showBounds.Checked && !obj.Convex)
                      Destination.DrawClosedCurve(Pens.Gray, obj.BoundingBox, 0, FillMode.Alternate);

                    if (menu_showVectors.Checked && !o.Static){
                        Vector v = o.LinearVelocity/10;
                        Destination.DrawLine(Pens.DarkOliveGreen, (PointF)o.COG, (PointF)(o.COG+v));
                        
                        Vector textPos = new Vector(o.COG); 
                        textPos[1] += 5*ScaleRatio;

                        Destination.DrawString(String.Format("{0:f2} m/s", MyWorld.Convert(o.LinearVelocity.Magnitude, ConversionType.PixelsToMeters)), new Font(FontFamily.GenericSansSerif, 6 * ScaleRatio), Brushes.DarkOliveGreen, (PointF)textPos);
                        textPos[1] += 7*ScaleRatio;
                        Destination.DrawString(String.Format("{0:f2} rad/s", o.AngularVelocity.Magnitude), new Font(FontFamily.GenericSansSerif, 6 * ScaleRatio), Brushes.DarkOrange, (PointF)textPos);
                    }
                }
                else Destination.FillPath(obj.Fill, obj.MakePath()); // Jinak nakresli objekt vyplněný texturou/barvou
            }
        }

        private int UpdateCounter = 0;
        void MyWorld_OnTick(object sender, EventArgs e)
        {
            
            Buffer.Graphics.Clear(Color.White);

            if (AddForce && Selected != null && afOrigin != null)
            {
                Buffer.Graphics.FillEllipse(Brushes.Red, new RectangleF(afOrigin.Value.X - 4, afOrigin.Value.Y - 4, 8, 8));

                PointF pt = Cursor.Position;
                pt.Y += CursorCorrection;
                pt.X *= ScaleRatio; pt.Y *= ScaleRatio;

                // Nakresli čáru symbolizující aplikovanou sílu
                double size = multiplier*Selected.Mass*Math.Round(MyWorld.Convert(Geometry.PointDistance(pt,afOrigin.Value),ConversionType.PixelsToMeters), 2);
                Buffer.Graphics.DrawString(String.Format("F = {0} N", size), new Font(FontFamily.GenericSansSerif, 7*ScaleRatio), Brushes.Red, new PointF(afOrigin.Value.X - 50, afOrigin.Value.Y + 10));
                    
                // Nakresli rameno síly
                Vector p = Selected.GetTorqueIntersection((Vector)afOrigin - (new Vector(pt.X,pt.Y)),(Vector)afOrigin);
                Buffer.Graphics.DrawLine(Pens.Gray,(PointF)p,(PointF)Selected.RotationPoint);
                Buffer.Graphics.FillRectangle(Brushes.Gray,(float)p[0]-2,(float)p[1]-2,2,2);
                Buffer.Graphics.DrawLine(Pens.Red, pt, (PointF)p);
            }

            if (SetLevel)
                Buffer.Graphics.DrawLine(Pens.DarkViolet, new PointF(0, (Cursor.Position.Y + CursorCorrection)*ScaleRatio), new PointF(Size.Width*ScaleRatio, (Cursor.Position.Y + CursorCorrection)*ScaleRatio)); 

            if (menu_showVersion.Checked) // Vykresli verzi
                Buffer.Graphics.DrawString("PhysBox, v.0.2", new Font(FontFamily.GenericSansSerif, 12*ScaleRatio), Brushes.Black, new PointF(10, (mainMenu.Height + 5)*ScaleRatio));

            if (menu_drawInfo.Checked) // Vykresli informace o modelu
            {
                string Info = String.Format("g: {0} m.s^-2\nRozlišení: {1}\nZvětšení: {2}x", MyWorld.Convert(MyWorld.Gravity, ConversionType.PixelsToMeters).Magnitude, MyWorld.Resolution,1/ScaleRatio);
                if (MyWorld.Paused)
                    Info += "\nPOZASTAVENO";

                Buffer.Graphics.DrawString(Info, new Font(FontFamily.GenericSansSerif, 11*ScaleRatio), Brushes.Black, (Size.Width - 150)*ScaleRatio, (Size.Height - 120)*ScaleRatio);

                // Vykresli měřítko rozlišení
                if (MyWorld.Resolution > 500)
                {
                    Buffer.Graphics.DrawString(String.Format("0,1 m = {0} px", MyWorld.Resolution / 10), new Font(FontFamily.GenericSansSerif, 7*ScaleRatio), Brushes.IndianRed, new PointF(30, Size.Height - 90));
                    Buffer.Graphics.DrawLine(Pens.IndianRed, new PointF(30 * ScaleRatio, (Size.Height - 70) * ScaleRatio), new PointF(30 * ScaleRatio + (float)MyWorld.Resolution / 10, (Size.Height - 70) * ScaleRatio));
                }
                else
                {
                    Buffer.Graphics.DrawString(String.Format("1 m = {0} px", MyWorld.Resolution), new Font(FontFamily.GenericSansSerif, 7*ScaleRatio), Brushes.IndianRed, new PointF(30 * ScaleRatio, (Size.Height - 90) * ScaleRatio));
                    Buffer.Graphics.DrawLine(Pens.IndianRed, new PointF(30*ScaleRatio, (Size.Height - 70)*ScaleRatio), new PointF(30*ScaleRatio + (float)MyWorld.Resolution, (Size.Height - 70)*ScaleRatio));
                }

                // Vykresli nulovou hladinu
                Buffer.Graphics.DrawLine(Pens.Silver, new PointF(0, (float)MyWorld.Level[1]*ScaleRatio), new PointF(Size.Width*ScaleRatio, (float)MyWorld.Level[1]*ScaleRatio));
            }

            // Je-li třeba vykresli siluetu
            if (Placing != null && menu_showPhantom.Checked)
            {
                GraphicObject obj = Placing.Model as GraphicObject;
                
                PointF pt = Cursor.Position;
                pt.Y += CursorCorrection;
                pt.X *= ScaleRatio; pt.Y *= ScaleRatio;

                Pen pn = new Pen(Color.ForestGreen);
                pn.DashStyle = DashStyle.Dash;
                obj.Position = (Vector)pt;
                Buffer.Graphics.DrawPath(pn, obj.MakePath());
            }

            if (Tools != null && !Tools.IsDisposed && UpdateCounter >= 10)
            {
                if (Selected != null)
                {
                    if (!Selected.Static)
                    {
                        ObjectEnergy eng = MyWorld.GetObjectEnergy(Selected, ConversionType.PixelsToMeters);
                        Tools.prop_Velocity.Text = String.Format("{0} m/s", Math.Round(MyWorld.Convert(Selected.LinearVelocity, ConversionType.PixelsToMeters).Magnitude, 2));
                        Tools.prop_angularVelocity.Text = String.Format("{0} rad/s", Math.Round(Selected.AngularVelocity.Magnitude, 2));
                        Tools.prop_kineticEnergy.Text = String.Format("{0} J", Math.Round(eng.Kinetic, 2));
                        Tools.prop_potentialEnergy.Text = String.Format("{0} J", Math.Round(eng.Potential, 2));
                        Tools.prop_rotationalEnergy.Text = String.Format("{0} J", Math.Round(eng.Rotational, 2));
                    }
                    else
                    {
                        Tools.prop_Velocity.Text = "0 m/s";
                        Tools.prop_angularVelocity.Text = "0 rad/s";
                        Tools.prop_kineticEnergy.Text = "-- J";
                        Tools.prop_potentialEnergy.Text = "-- J";
                        Tools.prop_rotationalEnergy.Text = "-- J";
                    }
                }
                UpdateCounter = 0;
            }
                
            RenderAllObjects(Buffer.Graphics);
            Buffer.Render();
            ++UpdateCounter;
        }
    }
}