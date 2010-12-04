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
        /// <summary>
        /// Událost nastávající při kolizi tělesa
        /// </summary>
        public event EventHandler OnCollision;
        
        private PointF center;
        private PointF[] geom;
        private double height, width, depth,surf,vol,farea;

        /// <summary>
        /// Vytvoří fyzický model tělesa jako objekt z daných vertexů
        /// </summary>       
        /// <param name="Vertices">Vertexy tělesa</param>
        /// <param name="InitPosition">Počáteční poloha tělesa</param>
        /// <param name="Height">Výška tělesa (volitelné)</param>
        /// <param name="Width">Šířka tělesa (volitelné)</param>
        /// <param name="Centroid">Geometrický střed tělesa - centroid (volitelné)</param>
        public Geometry(PointF[] Vertices,PointF InitPosition, float Height = 0,float Width = 0,PointF? Centroid = null)
        {

            if (Vertices == null || Vertices.Length < 3) throw new ArgumentException();
            surf = depth = vol = 0;

            if (Height != 0 && Width != 0 && Centroid.HasValue)
            {
                height = Height;
                width = Width;
                center = Centroid.Value;
            }
            else AnalyzeVertexGroup(Vertices, out height, out width, out center, out farea);

            Orientation = new Vector(3);
            Position = InitPosition;

            geom = Vertices;
            Nail = center;
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
            get { return depth; }
            set
            {
                depth = Math.Abs(value);
                if (depth != 0)
                {
                    for (int i = 0; i < geom.Length; i++)
                        surf += depth * Geometry.PointDistance(geom[i], geom[i < geom.Length - 1 ? i + 1 : 0]);                    
                    surf += farea*2;
                    vol = farea * depth;
                }
            }
        }

        /// <summary>
        /// Pozice těžiště objektu vzhledem k počátku světa
        /// </summary>
        public virtual Vector Position
        {
            get; set;
        }

        /// <summary>
        /// Orientace objektu
        /// </summary>
        public virtual Vector Orientation
        {
            get;
            set;
        }

        /// <summary>
        /// Vertexy tvořící fyzický model tělesa
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
        /// <param name="ObjHeight">Výška útvaru</param>
        /// <param name="ObjWidth">Šířka útvaru</param>
        /// <param name="Centroid">Geometrický střed útvaru</param>
        /// <param name="Area">Obsah obrazce</param>
        public static void AnalyzeVertexGroup(PointF[] Vertices, out double ObjHeight, out double ObjWidth, out PointF Centroid, out double Area)
        {
            Centroid = new PointF(0,0);
            PointF top = Vertices[0], bottom = Vertices[0], left = Vertices[0], right = Vertices[0];
            Area = PolygonArea(Vertices);

            for (int i = 0,j = 0; i < Vertices.Length; i++)
            {
                j = (i + 1) % Vertices.Length;
                Centroid.X += (Vertices[i].X + Vertices[j].X) * (Vertices[i].X * Vertices[j].Y - Vertices[i].Y * Vertices[j].X);
                Centroid.Y += (Vertices[i].Y + Vertices[j].Y) * (Vertices[i].X * Vertices[j].Y - Vertices[i].Y * Vertices[j].X);

                if (left.X > Vertices[j].X) left = Vertices[j];
                if (right.X < Vertices[j].X) right = Vertices[j];

                if (bottom.X > Vertices[j].Y) bottom = Vertices[j];
                if (top.X < Vertices[j].Y) top = Vertices[j];
            }

            Centroid.X /= (float)(Area * 6);
            Centroid.Y /= (float)(Area * 6);

            ObjHeight = top.Y - bottom.Y;
            ObjWidth = right.X - left.X;
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
        /// Geometrický střed objektu (centroid)
        /// </summary>
        public PointF Centroid
        {
            get { return center; }    
        }


        /// <summary>
        /// Poloha bodu, kterým prochází osa otáčení objektu vzhledem k centroidu
        /// </summary>
        public PointF Nail
        {
            get; set;
        }

        /// <summary>
        /// Výška objektu
        /// </summary>
        public double Height 
        {
            get { return height; }
        }

        /// <summary>
        /// Šířka objektu
        /// </summary>
        public double Width
        {
            get { return width; }
        }

        /// <summary>
        /// Objem objektu v krychlových pixelech
        /// </summary>
        public double Volume
        {
            get; set;
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
            get { return farea; }
        }
    }
}
