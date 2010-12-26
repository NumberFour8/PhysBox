using System;
using System.Drawing;
using System.Collections;

namespace PhysLib
{
    /// <summary>
    /// Třída hlášení o kolizi
    /// </summary>
    public sealed class CollisionReport
    {
        /// <summary>
        /// Výchozí konstruktor 
        /// </summary>
        /// <param name="ObjA">První účastník kolize</param>
        /// <param name="ObjB">Druhý účastník kolize</param>
        /// <param name="MinimumTranslation">Vektor minimálního posunutí</param>
        public CollisionReport(SimObject ObjA, SimObject ObjB,Vector MinimumTranslation)
        {
            A = ObjA;
            B = ObjB;
            MTD = MinimumTranslation;
            N = Vector.Unit(MTD);

            Prepare();

            NAP = ((Vector)Pairs[0].A - A.COG).Perp();
            NBP = ((Vector)Pairs[0].B - B.COG).Perp();
            RelativeVelocity = (A.LinearVelocity + Vector.Cross(NAP.Perp(), A.AngularVelocity)) - (B.LinearVelocity + Vector.Cross(NBP.Perp(), B.AngularVelocity));
        }

        /// <summary>
        /// První účastník kolize
        /// </summary>
        public SimObject A
        {
            get;
            private set;
        }

        /// <summary>
        /// Druhý účastník kolize
        /// </summary>
        public SimObject B
        {
            get;
            private set;
        }

        /// <summary>
        /// Páry bodů dotyku
        /// </summary>
        public ContactPair[] Pairs
        {
            get;
            private set;
        }

        /// <summary>
        /// Typ dotyku
        /// </summary>
        public ContactType TouchType
        {
            get;
            private set;
        }

        /// <summary>
        /// Minimální vektor oddělení
        /// </summary>
        public Vector MTD
        {
            get;
            private set;
        }

        /// <summary>
        /// Normálový vektor ke spojnici těžiště tělesa A s bodem dotyku
        /// </summary>
        public Vector NAP
        {
            get;
            private set;
        }

        /// <summary>
        /// Normálový vektor ke spojnici těžiště tělesa B s bodem dotyku
        /// </summary>
        public Vector NBP
        {
            get;
            private set;
        }

        /// <summary>
        /// Vzájemná relativní rychlost v době kontaktu
        /// </summary>
        public Vector RelativeVelocity
        {
            get;
            private set;
        }

        /// <summary>
        /// Normálový vektor ke kolizi
        /// </summary>
        public Vector N
        {
            get;
            private set;
        }

        /// <summary>
        /// Zredukuje kontaktní páry na jeden jediný
        /// </summary>
        private void Reduce()
        {
            ContactPair[] Single = new ContactPair[1];
            Single[0] = new ContactPair(Pairs[0].A, Pairs[0].B);

            int len = Pairs.Length;
            for (int i = 1; i < len; i++)
            {
                Single[0].A.X += Pairs[i].A.X;
                Single[0].A.Y += Pairs[i].A.Y;

                Single[0].B.X += Pairs[i].B.X;
                Single[0].B.Y += Pairs[i].B.Y;
            }

            Single[0].A.X /= len; Single[0].B.X /= len;
            Single[0].A.Y /= len; Single[0].B.Y /= len;
            Pairs = Single;
        }

        /// <summary>
        /// Připraví hlášení o kolizi
        /// </summary>
        private void Prepare()
        {
            Vector umtd = Vector.Unit(MTD);
            object c1 = null, c2 = null;

            PointF[] supA = A.Model.SupportPoints(umtd), supB = B.Model.SupportPoints(-umtd);
            if (supA.Length == 1)
            {
                if (supB.Length == 1)
                {
                    TouchType = ContactType.VertexVertex;
                    c1 = supA[0]; c2 = supB[0];
                }
                else if (supB.Length == 2)
                {
                    TouchType = ContactType.VertexEdge;
                    c1 = supA[0]; c2 = supB;
                }
                else throw new ArgumentException();
            }
            else if (supA.Length == 2)
            {
                if (supB.Length == 1)
                {
                    TouchType = ContactType.VertexEdge;
                    c1 = supA; c2 = supB[0];
                }
                else if (supB.Length == 2)
                {
                    TouchType = ContactType.EdgeEdge;
                    c1 = supA; c2 = supB;
                }
                else throw new ArgumentException();
            }
            else return;
            GetContactPairs(c1, c2);
            Reduce();
        }

        /// <summary>
        /// Najde nejbližší bod na dané hraně
        /// </summary>
        /// <param name="Edge">Hrana</param>
        /// <param name="V">Vnější bod</param>
        /// <returns>Bod na hraně</returns>
        private static PointF ClosestPointOnEdge(PointF[] Edge, PointF V)
        {
            Vector e = (Vector)Edge[1] - (Vector)Edge[0];
            Vector d = (Vector)V - (Vector)Edge[0];

            double t = Vector.Dot(e, d) / Vector.Pow(e, 2);

            t = (t < 0) ? 0 : (t > 1) ? 1 : t;
            return (PointF)((Vector)Edge[0] + (e * t));
        }

        /// <summary>
        /// Porovnávač páru dotyku
        /// </summary>
        /// <param name="p1">První pár</param>
        /// <param name="p2">Druhý pár</param>
        /// <returns>Porovnání</returns>
        private static int CompareCP(ContactPair p1, ContactPair p2)
        {
            if (p1.DistanceSquared == p2.DistanceSquared) return 0;
            return (p1.DistanceSquared > p2.DistanceSquared) ? 1 : -1;
        }

        /// <summary>
        /// Spočítá kontaktní páry
        /// </summary>
        /// <param name="e1">Bod/hrana</param>
        /// <param name="e2">Bod/hrana</param>
        private void GetContactPairs(object e1, object e2)
        {
            switch (TouchType)
            {
                case ContactType.VertexVertex:
                    Pairs = new ContactPair[1];
                    Pairs[0] = new ContactPair((PointF)e1, (PointF)e2);
                    break;
                case ContactType.VertexEdge:
                    if (e1.GetType() == typeof(PointF[]))
                    {
                        Pairs = new ContactPair[1];
                        Pairs[0] = new ContactPair(ClosestPointOnEdge((PointF[])e1, (PointF)e2), (PointF)e2);
                    }
                    else if (e2.GetType() == typeof(PointF[]))
                    {
                        Pairs = new ContactPair[1];
                        Pairs[0] = new ContactPair((PointF)e1, ClosestPointOnEdge((PointF[])e2, (PointF)e1));
                    }
                    else throw new ArgumentException();
                    break;
                case ContactType.EdgeEdge:
                    Pairs = new ContactPair[4];
                    PointF[] Edge1 = (PointF[])e1, Edge2 = (PointF[])e2;

                    Pairs[0] = new ContactPair(Edge1[0], ClosestPointOnEdge(Edge2, Edge1[0]));
                    Pairs[1] = new ContactPair(Edge1[1], ClosestPointOnEdge(Edge2, Edge1[1]));
                    Pairs[2] = new ContactPair(ClosestPointOnEdge(Edge1, Edge2[0]), Edge2[0]);
                    Pairs[3] = new ContactPair(ClosestPointOnEdge(Edge1, Edge2[1]), Edge2[1]);

                    Array.Sort<ContactPair>(Pairs, new Comparison<ContactPair>(CompareCP));
                    break;
            }
        }

    }

    /// <summary>
    /// Struktura pro pár kontaktních bodů
    /// </summary>
    public struct ContactPair
    {
        /// <summary>
        /// První bod dotyku
        /// </summary>
        public PointF A;

        /// <summary>
        /// Druhý bod dotyku
        /// </summary>
        public PointF B;

        /// <summary>
        /// Výchozí konstruktor
        /// </summary>
        /// <param name="first">První bod v páru</param>
        /// <param name="second">Druhý bod v páru</param>
        public ContactPair(PointF first, PointF second)
        {
            A = new PointF(first.X, first.Y);
            B = new PointF(second.X, second.Y);
        }

        /// <summary>
        /// Druhá mocnina vzdálenosti bodů
        /// </summary>
        public double DistanceSquared
        {
            get { return Math.Pow(Geometry.PointDistance(A, B), 2); }
        }
    }

    /// <summary>
    /// Možné typy dotyku
    /// </summary>
    public enum ContactType
    {
        VertexVertex = 1, VertexEdge = 2, EdgeEdge = 3
    }
}