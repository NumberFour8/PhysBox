﻿using System;
using System.Drawing;
using System.Collections;

namespace PhysLib
{
    
    /// <summary>
    /// Abstraktní třída reprezentující fyzický model tělesa
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
        /// Absolutní poloha konvexního útvaru ohraničující těleso
        /// </summary>
        public PointF[] BoundingBox
        {
            get
            {
                if (Convex) return geom;

                PointF[] transformedHull = new PointF[desc.ConvexHull.Length];
                desc.ConvexHull.CopyTo(transformedHull, 0);

                using (System.Drawing.Drawing2D.Matrix rot = new System.Drawing.Drawing2D.Matrix())
                {
                    rot.Translate((float)center[0], (float)center[1]);
                    rot.RotateAt((float)angle, (PointF)Nail, System.Drawing.Drawing2D.MatrixOrder.Append);
                    rot.TransformPoints(transformedHull);
                }

                return transformedHull;
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
        /// Indikuje, zda je útvar konvexní
        /// </summary>
        public bool Convex
        {
            get { return desc.Convex; }
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
            PointF[] geom = null;
            if (Convex)
                geom = ObjectGeometry;
            else geom = BoundingBox;

            min = max = Vector.Dot(Axis, (Vector)geom[0]);
            for (int i = 1; i < geom.Length; i++)
            {
                double d = Vector.Dot(Axis, (Vector)geom[i]);
                if (d < min)
                    min = d;
                else if (d > max)
                    max = d;
            }
        }

        internal PointF[] SupportPoints(Vector Axis)
        {
            PointF[] geom = null;
            if (Convex)
                geom = ObjectGeometry;
            else geom = BoundingBox;            
            
            double min = -1.0f;
            const double threshold = 1.0E-1;            
            int num = geom.Length;

            ArrayList sp = new ArrayList();

            for (int i = 0; i < num; i++)
            {
                double t = Vector.Dot(Axis,(Vector)geom[i]);
                if (t < min || i == 0)
                    min = t;
            }

            for (int i = 0; i < num; i++)
            {
                double t = Vector.Dot(Axis,(Vector)geom[i]);

                if (t < min + threshold)
                {
                    sp.Add(ObjectGeometry[i]);
                    if (sp.Count == 2) break;
                }
            }

            return (PointF[])sp.ToArray(typeof(PointF));
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
            Height = Width = Depth = FrontalArea = Farthest = 0;
            
            DefaultVertices = new PointF[Default.Length];
            Default.CopyTo(DefaultVertices, 0);
        }
    }

    /// <summary>
    /// Třída pro argumenty události kolize těles
    /// </summary>
    public class CollisionEventArgs : EventArgs
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
    /// Interní třída generující konvexní obal z vertexů
    /// </summary>
    internal class ConvexHull
    {
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

        public static PointF[] GetConvexHull(PointF[] input)
        {
            int n = input.Length, k = 0;
            PointF[] Hull = new PointF[2 * n],Sorted = new PointF[input.Length];
            input.CopyTo(Sorted, 0);

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
