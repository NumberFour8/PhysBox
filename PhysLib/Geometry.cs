﻿using System;
using System.Drawing;

namespace PhysLib
{
   
    /// <summary>
    /// Abstraktní třída reprezentující fyzický model tělesa
    /// </summary>
    [Serializable]
    public abstract class Geometry
    {
        /// <summary>
        /// Událost nastávající při kolizi tělesa
        /// </summary>
        public event EventHandler OnCollision;
        
        private Vector center;
        private PointF[] geom;
        private GeometryDescriptor desc;
        private double surf, vol, angle;

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
                System.Drawing.Drawing2D.Matrix Mat = new System.Drawing.Drawing2D.Matrix();
                Mat.Translate((float)(value[0]-center[0]), (float)(value[1]-center[1]));
                Mat.TransformPoints(geom);
                if (Nail != center) Nail += value - center;
                else Nail = value;

                center = value;
            }
        }

        /// <summary>
        /// Orientace objektu
        /// </summary>
        public double Orientation
        {
            get { return angle; }
            set {
                System.Drawing.Drawing2D.Matrix Mat = new System.Drawing.Drawing2D.Matrix();
                Mat.RotateAt((float)(value-angle), (PointF)(Nail));
                Mat.TransformPoints(geom);

                if (Nail != center)
                {
                    PointF[] t = new PointF[] { (PointF)center };
                    Mat.TransformPoints(t);
                    center = (Vector)t[0];
                }

                angle = value;
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
            GeometryDescriptor Description = new GeometryDescriptor();

            PointF top = Vertices[0], bottom = Vertices[0], left = Vertices[0], right = Vertices[0];
            Description.FrontalArea = PolygonArea(Vertices);

            for (int i = 0,j = 0; i < Vertices.Length; i++)
            {
                j = (i + 1) % Vertices.Length;
                Description.Centroid.X += (Vertices[i].X + Vertices[j].X) * (Vertices[i].X * Vertices[j].Y - Vertices[i].Y * Vertices[j].X);
                Description.Centroid.Y += (Vertices[i].Y + Vertices[j].Y) * (Vertices[i].X * Vertices[j].Y - Vertices[i].Y * Vertices[j].X);

                if (left.X > Vertices[j].X) left = Vertices[j];
                if (right.X < Vertices[j].X) right = Vertices[j];

                if (bottom.X > Vertices[j].Y) bottom = Vertices[j];
                if (top.X < Vertices[j].Y) top = Vertices[j];
            }

            Description.Centroid.X /= (float)(Description.FrontalArea * 6);
            Description.Centroid.Y /= (float)(Description.FrontalArea * 6);

            Description.Height = top.Y - bottom.Y;
            Description.Width = right.X - left.X;

            return Description;
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
    }

    /// <summary>
    /// Popisovač rovinného geometrického útvaru
    /// </summary>
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
        /// Výchozí konstruktor
        /// </summary>
        public GeometryDescriptor()
        {
            Centroid = new PointF(0, 0);
            Height = Width = Depth = FrontalArea = 0;
        }
    }
}
