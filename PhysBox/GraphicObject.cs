using System;
using System.Collections;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml;
using System.IO;

using PhysLib;

namespace PhysBox
{
    /// <summary>
    /// Konkrétní implementace třídy Geometry
    /// </summary>
    [Serializable]
    public class GraphicObject : Geometry
    {
        private string texture;
        public bool ShowVectors { get; set; }
        public string Name { get; set; }
        public float Tension { get; set; }
        public int WorldIndex { get; set; }

        /// <summary>
        /// Výchozí konstruktor objektu
        /// </summary>
        /// <param name="Texture">Barva (ARGB) nebo cesta k souboru s texturou (musí začínat znakem : )</param>
        /// <param name="Geometry">Soubor vertexů tvořící těleso</param>
        /// <param name="COG">Poloha těžiště tělesa nebo null (bude vypočítána)</param>
        public GraphicObject(string Texture, PointF[] Geometry, PointF COG)
            : base(Geometry, new PointF(0, 0), COG)
        {
            texture = Texture;

            if (texture[0] == ':' && !File.Exists(texture.Substring(1)))
                throw new FileNotFoundException(String.Format("Textura nenalezena: {0}", texture.Substring(1)));

            ShowVectors = false;
            Name = "obj_" + (DateTime.Now.Minute + DateTime.Now.Second).ToString(); // "Náhodný" název
            Tension = 0;
        }

        /// <summary>
        /// Výplň tělesa
        /// </summary>
        public Brush Fill
        {
            get
            {
                if (texture[0] == ':')
                    return new TextureBrush(Image.FromFile(texture.Substring(1)));
                else return new SolidBrush(Color.FromArgb(int.Parse(texture)));
            }
        }

        /// <summary>
        /// Vytvoří 2D cestu pro vykreslení
        /// </summary>
        /// <returns></returns>
        public GraphicsPath MakePath()
        {
            GraphicsPath Ret = new GraphicsPath();
            Ret.AddClosedCurve(ObjectGeometry, Tension);
            return Ret;
        }

        /// <summary>
        /// Načte objekt z daného XML souboru
        /// </summary>
        /// <param name="Path">Cesta k XML souboru</param>
        /// <returns>Načtený objekt</returns>
        public static GraphicObject LoadFromFile(string Path)
        {
            ArrayList points = new ArrayList();
            float drag = 0, depth = 0, tension = 0, h = 0, w = 0;
            string fill = String.Empty;
            Guid guid = Guid.Empty;
            PointF COG = new PointF();
            using (XmlTextReader rdr = new XmlTextReader(Path))
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
                    if (rdr.Name == "Geometry" && rdr.NodeType == XmlNodeType.Element)
                    {
                        drag = float.Parse(rdr.GetAttribute("C"));
                        depth = float.Parse(rdr.GetAttribute("D"));
                        tension = float.Parse(rdr.GetAttribute("Tension"));
                        h = float.Parse(rdr.GetAttribute("H"));
                        w = float.Parse(rdr.GetAttribute("W"));

                        fill = rdr.GetAttribute("Color");
                        if (fill[0] == ':' && !File.Exists(fill.Substring(1)))
                            throw new FileNotFoundException(String.Format("Textura nenalezena: {0}",fill.Substring(1)));
                        
                    }
                    if (rdr.Name == "COG" && rdr.NodeType == XmlNodeType.Element)
                    {
                        COG.X = float.Parse(rdr.GetAttribute("X"));
                        COG.Y = float.Parse(rdr.GetAttribute("Y"));
                    }
                }
            }

            GraphicObject Ret = new GraphicObject(fill, (PointF[])points.ToArray(typeof(PointF)), COG);
            Ret.Depth = depth;
            Ret.DragCoefficient = drag;
            Ret.Tension = tension;
            return Ret;
        }
    }
}
