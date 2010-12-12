using System;
using System.Drawing;

namespace PhysLib
{
    
    /// <summary>
    /// Abstraktní třída reprezentující fyzický model tělesa
    /// </summary>
    [Serializable]
    public abstract class Geometry
    {
        public delegate void CollisionHandler(SimObject Sender);
        
        /// <summary>
        /// Událost nastávající při kolizi tělesa
        /// </summary>
        public event CollisionHandler OnCollision;
        
        private Vector center;
        private PointF[] geom;
        private GeometryDescriptor desc;
        private double surf, vol, angle;
        private float scale;

        /// <summary>
        /// Vytvoří fyzický model tělesa jako objekt z daných vertexů
        /// </summary>       
        /// <param name="Vertices">Vertexy tělesa</param>
        /// <param name="InitPosition">Počáteční poloha tělesa</param>
        /// <param name="COG">Těžiště tělesa</param>
        public Geometry(PointF[] Vertices,PointF InitPosition, PointF? COG)
        {

            if (Vertices == null || Vertices.Length < 3) throw new ArgumentException();
            surf = vol = angle = 0;
            scale = 1.0f;

            desc = AnalyzeVertexGroup(Vertices);
            geom = Vertices;

            if (COG.HasValue)
                center = (Vector)COG;
            else center = (Vector)desc.Centroid;

            Nail = (Vector)InitPosition;
            Position = (Vector)InitPosition;
        }

        /// <summary>
        /// Koeficient odporu
        /// </summary>
        public double DragCoefficient
        {
            get; set;
        }

        /// <summary>
        /// Hloubka tělesa
        /// </summary>
        public double Depth
        {
            get { return desc.Depth; }
            set
            {
                desc.Depth = Math.Abs(value);
                if (desc.Depth != 0)
                {
                    for (int i = 0; i < geom.Length; i++)
                        surf += desc.Depth * Geometry.PointDistance(geom[i], geom[i < geom.Length - 1 ? i + 1 : 0]);                    
                    surf += desc.FrontalArea*2;
                    vol = desc.FrontalArea * desc.Depth;
                }
            }
        }

        /// <summary>
        /// Pozice těžiště objektu vzhledem k počátku světa
        /// </summary>
        public Vector Position
        {
            get { return (Vector)center; }
            set
            {
                using (System.Drawing.Drawing2D.Matrix Mat = new System.Drawing.Drawing2D.Matrix())
                {
                    Mat.Translate((float)(value[0] - center[0]), (float)(value[1] - center[1]));
                    Mat.TransformPoints(geom);
                    if (Nail != center) Nail += value - center;
                    else Nail = value;
                }
                center = value; 
            }
        }

        /// <summary>
        /// Orientace objektu v úhlových stupních
        /// </summary>
        public double Orientation
        {
            get { return angle; }
            set {
                if (Math.Abs(value - angle) > 0.05)
                {
                    using (System.Drawing.Drawing2D.Matrix Mat = new System.Drawing.Drawing2D.Matrix())
                    {
                        Mat.RotateAt((float)(value - angle), (PointF)(Nail));
                        Mat.TransformPoints(geom);

                        if (Nail != center)
                        {
                            PointF[] t = new PointF[] { (PointF)center };
                            Mat.TransformPoints(t);
                            center = new Vector(Math.Round(t[0].X, 2), Math.Round(t[0].Y, 2), 0);
                        }
                    }
                    angle = value;
                }
            }
        }

        /// <summary>
        /// Absolutní polohy vertexů tvořící fyzický model tělesa
        /// </summary>
        public PointF[] ObjectGeometry
        {
            get { return geom; }           
        }

        /// <summary>
        /// Relativní polohy vertexů tvořící fyzický model tělesa vzhledem k centroidu
        /// </summary>
        public PointF[] RelativeGeometry
        {
            get { return desc.DefaultVertices; }
        }

        /// <summary>
        /// Škáluje těleso daným faktorem
        /// </summary>
        /// <param name="Factor">Faktor</param>
        public float Scale
        {
            get { return scale; }
            set
            {
                using (System.Drawing.Drawing2D.Matrix mat = new System.Drawing.Drawing2D.Matrix())
                {
                    mat.Translate((float)-Position[0], (float)-Position[1]);
                    mat.Scale(Math.Abs(value - scale), Math.Abs(value - scale), System.Drawing.Drawing2D.MatrixOrder.Append);
                    mat.Translate((float)Position[0], (float)Position[1], System.Drawing.Drawing2D.MatrixOrder.Append);
                    mat.TransformPoints(geom);
                }
                scale = value;
            }
        }

        /// <summary>
        /// Spočte obsah polygonu daný určenými vertexy
        /// </summary>
        /// <param name="Vertices">Vertexy tvořící polygon</param>
        /// <returns>Rovinný obsah polygonu</returns>
        public static double PolygonArea(PointF[] Vertices)
        {
            double area = 0;
            for (int i = 0,j = 0; i < Vertices.Length; i++)
            {
                j = (i + 1) % Vertices.Length;
                area += Vertices[i].X * Vertices[j].Y;
                area -= Vertices[i].Y * Vertices[j].X;
            }
            return Math.Abs(area)/2;
        }


        /// <summary>
        /// Analyzuje pole vertexů jako polygon a zjistí jeho geometrický střed (centroid), šířku a výšku
        /// </summary>
        /// <param name="Vertices">Pole vertexů</param>
        /// <returns>Výsledek analýzy polygonu</returns>
        public static GeometryDescriptor AnalyzeVertexGroup(PointF[] Vertices)
        {
            GeometryDescriptor Description = new GeometryDescriptor(Vertices);

            PointF top = Vertices[0], bottom = Vertices[0], left = Vertices[0], right = Vertices[0];
            Description.FrontalArea = PolygonArea(Vertices);

            for (int i = 0,j = 0; i < Vertices.Length; i++)
            {
                j = (i + 1) % Vertices.Length;
                Description.Centroid.X += (Vertices[i].X + Vertices[j].X) * (Vertices[i].X * Vertices[j].Y - Vertices[i].Y * Vertices[j].X);
                Description.Centroid.Y += (Vertices[i].Y + Vertices[j].Y) * (Vertices[i].X * Vertices[j].Y - Vertices[i].Y * Vertices[j].X);
            }

            Description.Centroid.X /= (float)(Description.FrontalArea * 6);
            Description.Centroid.Y /= (float)(Description.FrontalArea * 6);

            
            using (System.Drawing.Drawing2D.GraphicsPath p = new System.Drawing.Drawing2D.GraphicsPath())
            {
                p.AddPolygon(Vertices);
                RectangleF r = p.GetBounds();

                Description.BoundingBox[0] = r.Location;
                Description.BoundingBox[1] = new PointF(r.X + r.Width, r.Y);
                Description.BoundingBox[2] = new PointF(r.X + r.Width, r.Y+r.Height);
                Description.BoundingBox[3] = new PointF(r.X, r.Y+r.Height);
                Description.Width = r.Width;
                Description.Height = r.Height;
            }
            
            double dist = Double.PositiveInfinity;            
            for (int i = 0; i < Vertices.Length; i++)
            {
                double d = Geometry.PointDistance(Vertices[i], Description.Centroid);
                if (d < dist)
                {
                    dist = d;
                    Description.Farthest = i;
                }
            }

            return Description;
        }

        
        /// <summary>
        /// Provede projekci libovolného bodu na povrch tělesa
        /// </summary>
        /// <param name="ExternalPoint">Bod ležící mimo těleso</param>
        /// <returns>Bod na tělese</returns>
        public Vector ProjectToObject(Vector ExternalPoint)
        {
            if (ExternalPoint == center) return center;

            int a = 0, b = 1;
            Vector Ret = null;      
            double ad = double.PositiveInfinity, bd = 0, cd = double.PositiveInfinity, dist = 0;
            for (int i = 0; i < ObjectGeometry.Length; i++)
            {
                dist = Math.Sqrt(Math.Pow(ExternalPoint[0] - ObjectGeometry[i].X, 2) + Math.Pow(ExternalPoint[1] - ObjectGeometry[i].Y, 2));
                if (dist < ad)
                {
                    bd = ad;
                    ad = dist;
                    b = a;
                    a = i;
                }
                else if (dist < bd)
                {
                    bd = dist;
                    b = i;
                }
            }

            float ax = ObjectGeometry[b].X - ObjectGeometry[a].X, ay = ObjectGeometry[b].Y - ObjectGeometry[a].Y, 
                  x = ObjectGeometry[a].X, y = ObjectGeometry[a].Y,max = (float)Math.Sqrt(ax * ax + ay * ay);

            while (x != ObjectGeometry[b].X || y != ObjectGeometry[b].Y)
            {
                dist = Geometry.PointDistance(new PointF(x,y), (PointF)ExternalPoint);
                if (dist < cd)
                {
                    cd = dist;
                    Ret = new Vector(x,y,0);
                }
                if (dist > cd) break;
               
                x += ax / max;
                y += ay / max;
            }

            return Ret;
        }

        /// <summary>
        /// Absolutní poloha obdélníku ohraničující těleso
        /// </summary>
        public PointF[] BoundingBox
        {
            get
            {       
                PointF[] transformedBox = new PointF[4];
                desc.BoundingBox.CopyTo(transformedBox, 0);    

                System.Drawing.Drawing2D.Matrix rot = new System.Drawing.Drawing2D.Matrix();
                rot.Translate((float)center[0], (float)center[1]);
                rot.RotateAt((float)angle, (PointF)Nail,System.Drawing.Drawing2D.MatrixOrder.Append);
                rot.TransformPoints(transformedBox);

                return transformedBox;
            }
        }

        /// <summary>
        /// Nejvzdálenější bod od středu tělesa
        /// </summary>
        public PointF FarthestPoint
        {
            get { return geom[desc.Farthest]; }
        }

        /// <summary>
        /// Spočte vzdálenost dvou bodů
        /// </summary>
        /// <param name="A">Bod A</param>
        /// <param name="B">Bod B</param>
        /// <returns>Vzdálenost |AB|</returns>
        public static double PointDistance(PointF A, PointF B)
        {
            return Math.Sqrt(Math.Pow(A.X - B.X, 2) + Math.Pow(A.Y - B.Y,2));
        }

        /// <summary>
        /// Absolutní poloha bodu osy otáčení v rovině těžiště
        /// </summary>
        public Vector Nail
        {
            get; set;
        }

        /// <summary>
        /// Výška objektu
        /// </summary>
        public double Height 
        {
            get { return desc.Height; }
        }

        /// <summary>
        /// Šířka objektu
        /// </summary>
        public double Width
        {
            get { return desc.Width; }
        }

        /// <summary>
        /// Objem objektu v krychlových pixelech
        /// </summary>
        public double Volume
        {
            get { return vol; }
        }

        /// <summary>
        /// Povrch objektu v pixelech čtverečních
        /// </summary>
        public double Surface
        {
            get { return surf; }
        }

        /// <summary>
        /// Obsah plochy polygonu
        /// </summary>
        public double FrontalArea
        {
            get { return desc.FrontalArea; }
        }

        /// <summary>
        /// Projektuje těleso na danou osu
        /// </summary>
        /// <param name="Axis">Osa projekce</param>
        /// <param name="min">Dolní mez intervalu</param>
        /// <param name="max">Horní mez intervalu</param>
        public void ProjectToAxis(Vector Axis, ref double min, ref double max)
        {
            min = max = Vector.Dot(Axis, (Vector)ObjectGeometry[0]);
            for (int i = 1; i < ObjectGeometry.Length; i++)
            {
                double d = Vector.Dot(Axis, (Vector)ObjectGeometry[i]);
                if (d < min)
                    min = d;
                else if (d > max)
                    max = d;
            }
        }

        internal void RaiseOnCollision(SimObject Param)
        {
            if (OnCollision != null)
              OnCollision(Param);
        }
    }

    /// <summary>
    /// Popisovač rovinného geometrického útvaru
    /// </summary>
    [Serializable]
    public class GeometryDescriptor
    {
        /// <summary>
        /// Výška útvaru
        /// </summary>
        public double Height;

        /// <summary>
        /// Šířka útvaru
        /// </summary>
        public double Width;

        /// <summary>
        /// Hloubka útvaru
        /// </summary>
        public double Depth;

        /// <summary>
        /// Obsah útvaru
        /// </summary>
        public double FrontalArea;

        /// <summary>
        /// Centroid útvaru
        /// </summary>
        public PointF Centroid;

        /// <summary>
        /// Index bodu nejdále od centroidu
        /// </summary>
        public int Farthest;

        /// <summary>
        /// Box ohraničující polygon
        /// </summary>
        public PointF[] BoundingBox;

        /// <summary>
        /// Původní netransformované vertexy
        /// </summary>
        public PointF[] DefaultVertices;

        /// <summary>
        /// Výchozí konstruktor
        /// </summary>
        public GeometryDescriptor(PointF[] Default)
        {
            Centroid = new PointF(0, 0);
            Height = Width = Depth = FrontalArea = Farthest = 0;
            
            BoundingBox = new PointF[4];
            DefaultVertices = new PointF[Default.Length];
            Default.CopyTo(DefaultVertices, 0);
        }
    }
}
