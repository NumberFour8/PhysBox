using System;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PhysBox
{
    public partial class MainForm : Form
    {
        private void RefreshGeometries()
        {
            foreach (string name in Directory.GetFiles("objects"))
                newobj_Geometry.Items.Add(Path.GetFileNameWithoutExtension(name));
        }

        private void vytvořitGeometriiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateObject obj = new CreateObject();
            obj.ShowDialog();

            RefreshGeometries();
        }

        private GraphicObject ReadObjectFromFile(string file)
        {
            ArrayList points = new ArrayList();
            using (XmlTextReader rdr = new XmlTextReader("objects\\"+file+".xml"))
            {
                while (rdr.Read())
                {
                    if (rdr.Name == "Vertex" && rdr.NodeType == XmlNodeType.Element)
                    {
                        PointF pt = new PointF();
                        pt.X = float.Parse(rdr.GetAttribute("X"));
                        pt.Y = float.Parse(rdr.GetAttribute("Y"));
                        points.Add(pt);
                    }
                }
            }

            return new GraphicObject(System.Drawing.Brushes.Brown, new System.Drawing.PointF(0, 0), (PointF[])points.ToArray(typeof(PointF)), 0, 0, 0, PointF.Empty);
        }
    }
}
