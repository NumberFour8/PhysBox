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
        private Vector mtd;

        /// <summary>
        /// Struktura pro pár kontaktních bodů
        /// </summary>
        public struct ContactPair
        {
            public PointF a, b;
            public ContactPair(PointF first, PointF second)
            {
                a = first; b = second;
            }

            /// <summary>
            /// Druhá mocnina vzdálenosti bodů
            /// </summary>
            public double DistanceSquared
            {
                get { return Vector.Pow((Vector)b - (Vector)a, 2);  }
            }
        }

        /// <summary>
        /// Výchozí konstruktor 
        /// </summary>
        /// <param name="ObjA">První účastník kolize</param>
        /// <param name="ObjB">Druhý účastník kolize</param>
        public CollisionReport(SimObject ObjA, SimObject ObjB)
        {
            mtd = new Vector(3);

            A = ObjA;
            B = ObjB;
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
        /// Možné typy dotyku
        /// </summary>
        public enum ContactType
        {
            VertexVertex = 1, VertexEdge = 2, EdgeEdge = 3
        }

        /// <summary>
        /// Typ dotyku
        /// </summary>
        public ContactType Type
        {
            get;
            private set;
        }

        /// <summary>
        /// Minimální vektor oddělení
        /// </summary>
        public Vector MTD
        {
            get { return mtd;  }
            set { mtd = value; }
        }

        /// <summary>
        /// Spočítá kontaktní páry
        /// </summary>
        internal void CalculatePairs()
        {
            Vector umtd = Vector.Unit(mtd);
            object c1 = null, c2 = null;

            PointF[] supA = A.Model.SupportPoints(umtd), supB = B.Model.SupportPoints(-umtd);
            if (supA.Length == 1)
            {
                if (supB.Length == 1)
                {
                    Type = ContactType.VertexVertex;
                    c1 = supA[0]; c2 = supB[0];
                }
                else if (supB.Length == 2)
                {
                    Type = ContactType.VertexEdge;
                    c1 = supA[0]; c2 = supB;
                }
                else throw new ArgumentException();
            }
            else if (supA.Length == 2)
            {
                if (supB.Length == 1)
                {
                    Type = ContactType.VertexEdge;
                    c1 = supA; c2 = supB[0];
                }
                else if (supB.Length == 2)
                {
                    Type = ContactType.EdgeEdge;
                    c1 = supA; c2 = supB;
                }
                else throw new ArgumentException();
            }
            else throw new ArgumentException();
            GetContactPairs(c1, c2);
        }

        /// <summary>
        /// Najde nejbližší bod na dané hraně
        /// </summary>
        /// <param name="Edge">Hrana</param>
        /// <param name="V">Vnější bod</param>
        /// <returns>Bod na hraně</returns>
        private PointF ClosestPointOnEdge(PointF[] Edge, PointF V)
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
            switch (Type)
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
                    Pairs[0] = new ContactPair(((PointF[])e1)[0], ClosestPointOnEdge((PointF[])e2, ((PointF[])e1)[0]));
                    Pairs[1] = new ContactPair(((PointF[])e1)[1], ClosestPointOnEdge((PointF[])e2, ((PointF[])e1)[1]));
                    Pairs[2] = new ContactPair(ClosestPointOnEdge((PointF[])e1, ((PointF[])e2)[0]), ((PointF[])e2)[0]);
                    Pairs[3] = new ContactPair(ClosestPointOnEdge((PointF[])e1, ((PointF[])e2)[1]), ((PointF[])e2)[1]);

                    Array.Sort<ContactPair>(Pairs, new Comparison<ContactPair>(CompareCP));
                    break;
            }
        }

    }
}