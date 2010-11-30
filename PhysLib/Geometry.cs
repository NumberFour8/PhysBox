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
        private double height,width;

        /// <summary>
        /// Vytvoří fyzický model tělesa jako objekt z daných vertexů
        /// </summary>       
        /// <param name="Vertices">Vertexy tělesa</param>
        /// <param name="InitPosition">Počáteční poloha tělesa</param>
        /// <param name="AngleX">Počáteční orientace tělesa vzhledem k ose X</param>
        /// <param name="Height">Výška tělesa (volitelné)</param>
        /// <param name="Width">Šířka tělesa (volitelné)</param>
        /// <param name="Centroid">Geometrický střed tělesa - centroid (volitelné)</param>
        public Geometry(PointF[] Vertices,PointF InitPosition,float AngleX = 0,float Height = 0,float Width = 0,PointF? Centroid = null)
        {

            if (Height != 0 && Width != 0 && Centroid.HasValue)
            {
                height = Height;
                width = Width;
                center = Centroid.Value;
            }
            else AnalyzeVertexGroup(Vertices, out height, out width, out center);

            Orientation = new Vector(AngleX,0,0);
            Position = InitPosition;

            geom = Vertices;
            Nail = center;
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
            get; set;
        }

        /// <summary>
        /// Vertexy tvořící fyzický model tělesa
        /// </summary>
        public PointF[] ObjectGeometry
        {
            get { return geom; }           
        }

        
        /// <summary>
        /// Analyzuje pole vertexů jako polygon a zjistí jeho geometrický střed (centroid), šířku a výšku
        /// </summary>
        /// <param name="Vertices">Pole vertexů</param>
        /// <param name="ObjHeight">Výška tělesa</param>
        /// <param name="ObjWidth">Šířka tělesa</param>
        /// <param name="Centroid">Geometrický střed tělesa</param>
        public static void AnalyzeVertexGroup(PointF[] Vertices, out double ObjHeight, out double ObjWidth, out PointF Centroid)
        {
            Centroid = new PointF(0,0);
            PointF top = Vertices[0], bottom = Vertices[0], left = Vertices[0], right = Vertices[0];
            float s = 0;

            for (int i = 0; i < Vertices.Length - 1; i++)
                s += Vertices[i].X * Vertices[i + 1].Y - Vertices[i].Y * Vertices[i + 1].X;

            for (int i = 0; i < Vertices.Length - 1; i++)
            {
                Centroid.X += (Vertices[i].X + Vertices[i + 1].X) * (Vertices[i].X * Vertices[i + 1].Y - Vertices[i].Y * Vertices[i + 1].X);
                Centroid.Y += (Vertices[i].Y + Vertices[i + 1].Y) * (Vertices[i].X * Vertices[i + 1].Y - Vertices[i].Y * Vertices[i + 1].X);

                if (left.X > Vertices[i + 1].X) left = Vertices[i + 1];
                if (right.X < Vertices[i + 1].X) right = Vertices[i + 1];

                if (bottom.X > Vertices[i + 1].Y) bottom = Vertices[i + 1];
                if (top.X < Vertices[i + 1].Y) top = Vertices[i + 1];
            }

            Centroid.X /= 3 * s;
            Centroid.Y /= 3 * s;

            ObjHeight = top.Y - bottom.Y;
            ObjWidth = right.X - left.X;
        }

        /// <summary>
        /// Geometrický střed objektu (centroid)
        /// </summary>
        public PointF Centroid
        {
            get { return center; }    
        }

        /// <summary>
        /// Bod, kterým prochází osa otáčení objektu
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
        /// Objem objektu
        /// </summary>
        public double Volume
        {
            get; set;
        }

        /// <summary>
        /// Povrch objektu
        /// </summary>
        public double Surface
        {
            get; set;
        }
    }
}
