using System;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace PhysBox
{

    public partial class CreateObject : Form
    {
        private ArrayList pts;
        private PointF? COG;
        private PointF cCOG;
        double Height, Width;
        private Color col;

        public CreateObject()
        {
            InitializeComponent();
            pts = new ArrayList();
            COG = null;
            col = Color.Brown;
            Height = Width = 0;
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (COG != null) return;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                pts.Add(new PointF(e.Location.X,e.Location.Y));
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                COG = e.Location;
                PhysLib.Geometry.AnalyzeVertexGroup((PointF[])pts.ToArray(typeof(PointF)), out Height, out Width, out cCOG);

                label_cCOG.Text = String.Format("Spočítané těžiště: [{0:f1};{1:f1}]", cCOG.X, cCOG.Y);
                label_COG.Text =  String.Format("Určené těžiště: [{0};{1}]", COG.Value.X, COG.Value.Y);

            }
            
            text_objName.Enabled = COG != null;
            button_selColor.Enabled = COG != null;

            drawPanel.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (COG == null)
            {
                foreach (PointF pt in pts.ToArray(typeof(PointF)))
                {
                    g.FillRectangle(Brushes.Black, new RectangleF(pt.X - 2, pt.Y - 2, 4, 4));
                }
            }
            else
            {                                
                System.Drawing.Drawing2D.GraphicsPath Path = new System.Drawing.Drawing2D.GraphicsPath();
                Path.AddPolygon((PointF[])pts.ToArray(typeof(PointF)));
                g.DrawPath(Pens.Black, Path);

                g.FillEllipse(Brushes.Red, new RectangleF(COG.Value.X - 2, COG.Value.Y - 2, 4, 4));
                g.FillEllipse(Brushes.Blue, new RectangleF(cCOG.X - 2, cCOG.Y - 2, 4, 4));
            }
        }

        private void text_mass_TextChanged(object sender, EventArgs e)
        {
            button_OK.Enabled = text_objName.Text.Length > 0;
        }

        private void button_OK_Click(object sender, EventArgs e)
        {            

            if (Math.Sqrt(Math.Pow(cCOG.X - COG.Value.X, 2) + Math.Pow(cCOG.Y - COG.Value.Y, 2)) > 10 &&
                MessageBox.Show("Vzdálenost vypočítaného těžiště a ručně určeného težiště útvaru je větší než 10 pixelů, přesto použít ručně určené těžiště?", "Různá těžiště", MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2)
                == System.Windows.Forms.DialogResult.No) COG = cCOG;         
            
            using (XmlTextWriter Object = new XmlTextWriter("objects\\" + text_objName.Text + ".xml", Encoding.UTF8))
            {
                Object.WriteStartDocument();
                Object.WriteStartElement("Geometry");
                Object.WriteAttributeString("W", Width.ToString());
                Object.WriteAttributeString("H", Height.ToString());
                Object.WriteAttributeString("D", text_objDepth.Text);
                Object.WriteAttributeString("C", text_objDrag.Text);
                Object.WriteAttributeString("Color", col.ToArgb().ToString());
                
                for (int i = 0; i < pts.Count; i++)
                {
                    Object.WriteStartElement("Vertex");
                    Object.WriteAttributeString("X", (((PointF)pts[i]).X - COG.Value.X).ToString());
                    Object.WriteAttributeString("Y", (((PointF)pts[i]).Y - COG.Value.Y).ToString());
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
            col = colorDialog.Color;
        }
    }
}
