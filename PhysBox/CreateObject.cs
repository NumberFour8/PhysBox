using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

using PhysLib;

namespace PhysBox
{

    public partial class CreateObject : Form
    {
        private List<PointF> pts;
        private PointF? COG;
        private GeometryDescriptor Desc;        
        private string col;

        public CreateObject()
        {
            InitializeComponent();
                       
            pts = new List<PointF>();
           
            COG = null;
            col = Color.Brown.ToArgb().ToString();
            text_objTension.ValidatingType = text_objDrag.ValidatingType = typeof(float);
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (COG != null) return;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                foreach (PointF p in pts)
                {
                    if (p.X == e.X && p.Y == e.Y)
                      return;
                }
                pts.Add(new PointF(e.X, e.Y));
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                COG = e.Location;
                try
                {
                    Desc = Geometry.AnalyzeVertexGroup(pts.ToArray());
    
                    label_cCOG.Text = String.Format("Centroid: [{0:f1};{1:f1}]", Desc.Centroid.X, Desc.Centroid.Y);
                    label_COG.Text = String.Format("Určené těžiště: [{0};{1}]", COG.Value.X, COG.Value.Y);
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("Neplatný počet vertexů!");
                }
            }
            
            text_objName.Enabled = COG != null;
            button_selTexture.Enabled = button_SetCOG.Enabled = button_selColor.Enabled = COG != null;
            
            drawPanel.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (COG == null)
            {
                foreach (PointF pt in pts.ToArray(typeof(PointF)))                
                   g.FillRectangle(Brushes.Black, new RectangleF(pt.X - 2, pt.Y - 2, 4, 4));
            }
            else
            {                                
                System.Drawing.Drawing2D.GraphicsPath Path = new System.Drawing.Drawing2D.GraphicsPath();
                Path.AddClosedCurve((PointF[])pts.ToArray(typeof(PointF)),float.Parse(text_objTension.Text));                
                g.DrawPath(Pens.Black, Path);

                g.FillEllipse(Brushes.Red, new RectangleF(COG.Value.X - 2, COG.Value.Y - 2, 4, 4));
                g.FillEllipse(Brushes.Blue, new RectangleF(Desc.Centroid.X - 2, Desc.Centroid.Y - 2, 4, 4));
            }
        }

        private void text_mass_TextChanged(object sender, EventArgs e)
        {
            button_OK.Enabled = text_objName.Text.Length > 0;
        }

        private void button_OK_Click(object sender, EventArgs e)
        {            
            
            if (!Desc.Convex)
              MessageBox.Show("Útvar není konvexní a při detekci kolizí bude nahrazen jeho konvexním obalem.","Konvexnost",MessageBoxButtons.OK,MessageBoxIcon.Information);

            using (XmlTextWriter Object = new XmlTextWriter("objects\\" + text_objName.Text + ".xml", Encoding.UTF8))
            {
                Object.WriteStartDocument();
                Object.WriteStartElement("Geometry");
                Object.WriteAttributeString("W", Desc.Width.ToString());
                Object.WriteAttributeString("H", Desc.Height.ToString());
                Object.WriteAttributeString("D", text_objDepth.Text);
                Object.WriteAttributeString("C", text_objDrag.Text);
                Object.WriteAttributeString("Color", col);
                Object.WriteAttributeString("Tension", text_objTension.Text);
                Object.WriteAttributeString("ID", System.Guid.NewGuid().ToString());

                Object.WriteStartElement("COG");
                Object.WriteAttributeString("X", (COG.Value.X-Desc.Centroid.X).ToString());
                Object.WriteAttributeString("Y", (COG.Value.Y-Desc.Centroid.Y).ToString());
                Object.WriteEndElement();

                for (int i = 0; i < pts.Count; i++)
                {
                    Object.WriteStartElement("Vertex");
                    Object.WriteAttributeString("X", (((PointF)pts[i]).X - Desc.Centroid.X).ToString());
                    Object.WriteAttributeString("Y", (((PointF)pts[i]).Y - Desc.Centroid.Y).ToString());
                    Object.WriteEndElement();
                }

                Object.WriteEndElement();
                Object.WriteEndDocument();
            }
            Close();
        }

        private void button_selColor_Click(object sender, EventArgs e)
        {
            colorDialog.ShowDialog();
            col = colorDialog.Color.ToArgb().ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textureDialog.ShowDialog();
            col = ":" + textureDialog.FileName;
        }

        private void text_objTension_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            if (e.IsValidInput)
                drawPanel.Invalidate();
        }

        private void button_SetCOG_Click(object sender, EventArgs e)
        {
            COG = Desc.Centroid;
            label_cCOG.Text = String.Format("Centroid: [{0:f1};{1:f1}]", Desc.Centroid.X, Desc.Centroid.Y);
            label_COG.Text = String.Format("Určené těžiště: [{0:f1};{1:f1}]", COG.Value.X, COG.Value.Y);
        }
    }
}
