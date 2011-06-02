using System;
using System.Drawing;
using System.Collections.Generic;

namespace PhysLib
{
    
    /// <summary>
    /// Abstraktní třída reprezentující 2D fyzický model tělesa
    /// </summary>
    [Serializable]
    public abstract class Geometry
    {
        /// <summary>
        /// Delegát pro událost kolize
        /// </summary>
        /// <param name="sender">Objekt, který hlásil kolizi</param>
        /// <param name="e">Hlášení o kolizi</param>
        public delegate void CollisionHandler(object sender,CollisionEventArgs e);
        
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
            geom = new PointF[Vertices.Length];
            Vertices.CopyTo(geom, 0);

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
        /// Pozice těžiště objektu vzhledem k počátku světa
        /// </summary>
        public Vector Position
        {
            get { return (Vector)center; }
            set
            {
                if (value.IsNaN) throw new ArgumentOutOfRangeException();
                if ((value - center).Magnitude < 0.1) return;

                geom = Transform2D.TransformPoints(Transform2D.Translate(value - center), geom);
                if (Nail != center) Nail += value - center;
                else Nail = value;

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
                if (Double.IsNaN(value) || Double.IsInfinity(value)) throw new ArgumentOutOfRangeException();
                if (Math.Abs(value - angle) < 0.1) return;

                Matrix Rotation = Transform2D.Rotate((value - angle)*Math.PI/180, Nail);
                geom = Transform2D.TransformPoints(Rotation,geom);
                center = Vector.Round(Transform2D.TransformVectors(Rotation,center)[0],2);

                angle = value;
            }
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
                if (Double.IsNaN(value) || Double.IsInfinity(value)) throw new ArgumentOutOfRangeException();

                geom = Transform2D.TransformPoints(Transform2D.Scale(new Vector(Scale, Scale), Position),geom);
                scale = value;
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
            if (Vertices.Length <= 1)
                throw new ArgumentException();

            GeometryDescriptor Description = new GeometryDescriptor(Vertices);
            Description.FrontalArea = PolygonArea(Vertices);

            for (int i = 0,j = 0; i < Vertices.Length; i++)
            {
                j = (i + 1) % Vertices.Length;
                Description.Centroid.X += (Vertices[i].X + Vertices[j].X) * (Vertices[i].X * Vertices[j].Y - Vertices[i].Y * Vertices[j].X);
                Description.Centroid.Y += (Vertices[i].Y + Vertices[j].Y) * (Vertices[i].X * Vertices[j].Y - Vertices[i].Y * Vertices[j].X);
            }

            Description.ConvexHull = ConvexHull.GetConvexHull(Vertices);
            Description.Centroid.X /= (float)(Description.FrontalArea * 6);
            Description.Centroid.Y /= (float)(Description.FrontalArea * 6);
            
            using (System.Drawing.Drawing2D.GraphicsPath p = new System.Drawing.Drawing2D.GraphicsPath())
            {
                p.AddPolygon(Vertices);
                RectangleF r = p.GetBounds();

                Description.Width = r.Width;
                Description.Height = r.Height;
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
        /// Absolutní poloha konvexního útvaru ohraničující těleso
        /// </summary>
        public PointF[] BoundingBox
        {
            get
            {
                if (Convex) return geom;
                PointF[] transformedHull = new PointF[desc.ConvexHull.Length];
                desc.ConvexHull.CopyTo(transformedHull, 0);

                //// DOČASNÉ!
                using (System.Drawing.Drawing2D.Matrix rot = new System.Drawing.Drawing2D.Matrix())
                {
                    rot.Translate((float)center[0], (float)center[1]);
                    rot.RotateAt((float)angle, (PointF)Nail, System.Drawing.Drawing2D.MatrixOrder.Append);
                    rot.TransformPoints(transformedHull);
                }

                /*
                //Matrix rot = Transform2D.Translate(center);
                Matrix rot = Transform2D.Rotate(angle * Math.PI / 180, Nail);
                transformedHull = Transform2D.TransformPoints(rot, transformedHull);*/

                return transformedHull;
            }
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
                    surf += desc.FrontalArea * 2;
                    vol = desc.FrontalArea * desc.Depth;
                }
            }
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
        public virtual double Volume
        {
            get { return vol; }
        }

        /// <summary>
        /// Indikuje, zda je útvar konvexní
        /// </summary>
        public bool Convex
        {
            get { return desc.Convex; }
        }

        /// <summary>
        /// Povrch objektu v pixelech čtverečních
        /// </summary>
        public virtual double Surface
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
            PointF[] axProj = BoundingBox;

            min = max = Vector.Dot(Axis, (Vector)axProj[0]);
            for (int i = 1; i < axProj.Length; i++)
            {
                double d = Vector.Dot(Axis, (Vector)axProj[i]);
                if (d < min)
                    min = d;
                else if (d > max)
                    max = d;
            }
        }

        internal PointF[] SupportPoints(Vector Axis)
        {   
            double min = -1.0f;
            const double threshold = 1.0E-1;            
            PointF[] supGeom = BoundingBox;

            List<PointF> sp = new List<PointF>();

            for (int i = 0; i < supGeom.Length; i++)
            {
                double t = Vector.Dot(Axis,(Vector)supGeom[i]);
                if (t < min || i == 0)
                    min = t;
            }

            for (int i = 0; i < supGeom.Length; i++)
            {
                double t = Vector.Dot(Axis,(Vector)supGeom[i]);

                if (t < min + threshold)
                {
                    sp.Add(supGeom[i]);
                    if (sp.Count == 2) break;
                }
            }

            return sp.ToArray();
        }

        internal void RaiseOnCollision(CollisionReport Report)
        {
            if (OnCollision != null)
              OnCollision(this,new CollisionEventArgs(Report));
        }
    }

    /// <summary>
    /// Popisovač rovinného geometrického útvaru
    /// </summary>
    [Serializable]
    public sealed class GeometryDescriptor
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
        /// Indikuje, zda je polygon konvexní
        /// </summary>
        public bool Convex
        {
            get { return ConvexHull.Length == DefaultVertices.Length; }
        }

        /// <summary>
        /// Ohraničující konvexní polygon
        /// </summary>
        public PointF[] ConvexHull;

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
            Height = Width = Depth = FrontalArea = 0;
            
            DefaultVertices = new PointF[Default.Length];
            Default.CopyTo(DefaultVertices, 0);
        }
    }

    /// <summary>
    /// Třída pro argumenty události kolize těles
    /// </summary>
    public sealed class CollisionEventArgs : EventArgs
    {
        /// <summary>
        /// Hlášení o kolizi
        /// </summary>
        public CollisionReport Report { get; private set; }

        /// <summary>
        /// Výchozí konstruktor
        /// </summary>
        /// <param name="Rep">Hlášení o kolizi</param>
        public CollisionEventArgs(CollisionReport Rep)
        {
            Report = Rep;
        }
    }

    /// <summary>
    /// Třída generující konvexní obal z vertexů
    /// </summary>
    public class ConvexHull
    {
        private ConvexHull() { }

        private static int LexicalPointComparison(PointF a, PointF b)
        {
            if (a.X != b.X)
                return a.X > b.X ? 1 : -1;
            else if (a.Y != b.Y)
                return a.Y > b.Y ? 1 : -1;
            else return 0;
        }

        private static float zc(PointF O,PointF A,PointF B)
        {
	        return (A.X - O.X) * (B.Y - O.Y) - (A.Y - O.Y) * (B.X - O.X);
        }

        /// <summary>
        /// Vytvoří konvexní obal z daných vertexů
        /// </summary>
        /// <param name="Vertices">Vertexy</param>
        /// <returns>Pole vertexů tvořící konvexní obal</returns>
        public static PointF[] GetConvexHull(PointF[] Vertices)
        {
            int n = Vertices.Length, k = 0;
            PointF[] Hull = new PointF[2 * n],Sorted = new PointF[Vertices.Length];
            Vertices.CopyTo(Sorted, 0);

            Array.Sort<PointF>(Sorted, new Comparison<PointF>(LexicalPointComparison));

            for (int i = 0; i < n; i++)
            {
                while (k >= 2 && zc((PointF)Hull[k - 2],(PointF)Hull[k - 1], Sorted[i]) <= 0) k--;
                Hull[k++] = Sorted[i];
            }

            for (int i = n - 2, t = k + 1; i >= 0; i--)
            {
                while (k >= t && zc((PointF)Hull[k - 2], (PointF)Hull[k - 1], Sorted[i]) <= 0) k--;
                Hull[k++] = Sorted[i];
            }

            PointF[] Ret = new PointF[k-1];
            for (int i = 0; i < k - 1; i++)
            {
                Ret[i].X = Hull[i].X;
                Ret[i].Y = Hull[i].Y;
            }
            return Ret;
        }
    }
}
