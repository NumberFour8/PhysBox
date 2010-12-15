using System;
using System.Collections;
using System.Drawing;

namespace PhysLib
{
    /// <summary>
    /// Třída hlášení o kolizi
    /// </summary>
    public sealed class CollisionReport
    {
        private Vector mtd;
        
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
            get; private set;
        }
        
        /// <summary>
        /// Druhý účastník kolize
        /// </summary>
        public SimObject B
        {
            get; private set;
        }

        /// <summary>
        /// Párové body dotyku
        /// </summary>
        public PointF[,] ContactPairs
        {
            get; private set;
        }

        /// <summary>
        /// Možné typy dotyku
        /// </summary>
        public enum ContactType
        {
            VertexVertex = 1,VertexEdge = 2,EdgeEdge = 3
        }

        /// <summary>
        /// Typ dotyku
        /// </summary>
        public ContactType Type
        {
            get; private set;
        }

        /// <summary>
        /// Minimální vektor oddělení
        /// </summary>
        public Vector MTD
        {
            get { return mtd; }
            set
            {
                Vector umtd = Vector.Unit(value);
                object c1 = null,c2 = null;

                PointF[] supA = A.Model.SupportPoints(umtd), supB = B.Model.SupportPoints(-umtd);
                mtd = value;
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
                GetContactPoints(c1, c2);
            }
        }

        private PointF ClosestPointOnEdge(PointF[] Edge,PointF v)
        {
	        Vector e = (Vector)Edge[1] - (Vector)Edge[0];
	        Vector d = (Vector)v - (Vector)Edge[0];
	        
            double t = Vector.Dot(e,d) / Vector.Pow(e,2);
	       
            t = (t < 0) ? 0 : (t > 1) ? 1 : t;
	        return (PointF)((Vector)Edge[0] + (e * t));
        }

        private void GetContactPoints(object e1, object e2)
        {
            switch (Type)
            {
                case ContactType.VertexVertex:
                    ContactPairs = new PointF[1,2];
                    ContactPairs[0,0] = (PointF)e1;
                    ContactPairs[0,1] = (PointF)e2;
                    break;
                case ContactType.VertexEdge:
                    if (e1.GetType() == typeof(PointF[]))
                    {
                        ContactPairs = new PointF[1, 2];
                    }
                    else if (e2.GetType() == typeof(PointF[]))
                    {
                        ContactPairs = new PointF[1, 2];
                    }
                    else throw new ArgumentException();
                    break;
                case ContactType.EdgeEdge:
                    ContactPairs = new PointF[4,2];
                    // setřídit
                    break;
            }
        }

    }

    /// <summary>
    /// Třída pro řešení kolizí
    /// </summary>
    public sealed class CollisionSolver
    {
        private World w;
        
        /// <summary>
        /// Výchozí konstruktor
        /// </summary>
        /// <param name="Reference">Svět, ve kterém počítat kolize</param>
        public CollisionSolver(World Reference)
        {
            w = Reference;
            Enabled = true;
        }

        /// <summary>
        /// Indikuje, zda je detekce kolizí zapnuta
        /// </summary>
        public bool Enabled
        {
            get; set;
        }

        /// <summary>
        /// Zjistí, zda je jedno těleso od druhého odděleno danou osou
        /// </summary>
        /// <param name="Axis">Osa oddělení</param>
        /// <param name="Object">Objekt</param>
        /// <returns>True pokud je odděleno, False pokud nikoliv</returns>
        public bool SeparatedByAxis(Vector Axis, Geometry ObjectA, Geometry ObjectB,ref CollisionReport Rep)
        {
            double minA = 0, maxA = 0, minB = 0, maxB = 0;
            ObjectA.ProjectToAxis(Axis, ref minA, ref maxA);
            ObjectB.ProjectToAxis(Axis, ref minB, ref maxB);

	        double d0 = (maxB - minA);
	        double d1 = (minB - maxA);

	        if(d0 < 0.0f || d1 > 0.0f) return true;
            
            double overlap = (d0 < -d1) ? d0 : d1;

            Vector Sep = Axis * (overlap / Vector.Pow(Axis, 2));

            if (Rep.MTD.IsNull || Vector.Pow(Sep, 2) < Vector.Pow(Rep.MTD, 2))
                Rep.MTD = Sep;

            return false;

        }

        /// <summary>
        /// Zjistí, zda objekt koliduje s jiným - daným objektem
        /// </summary>
        /// <param name="Object"></param>
        /// <returns></returns>
        public CollisionReport ObjectsCollide(SimObject ObjectA,SimObject ObjectB)
        {
            CollisionReport Ret = new CollisionReport(ObjectA,ObjectB);
            int Count = ObjectA.Model.ObjectGeometry.Length + ObjectB.Model.ObjectGeometry.Length;
            for (int i = 0, j; i < Count; i++)
            {
                Vector Axis = null;
                if (i < ObjectA.Model.ObjectGeometry.Length)
                {
                    j = (i == ObjectA.Model.ObjectGeometry.Length - 1) ? 0 : i + 1;
                    Vector v1 = (Vector)ObjectA.Model.ObjectGeometry[i];
                    Vector v2 = (Vector)ObjectA.Model.ObjectGeometry[j];
                    Axis = (v1 - v2).Perp();
                }
                else
                {
                    j = (i == Count - 1) ? ObjectA.Model.ObjectGeometry.Length : i + 1;
                    Vector v1 = (Vector)ObjectB.Model.ObjectGeometry[i - ObjectA.Model.ObjectGeometry.Length];
                    Vector v2 = (Vector)ObjectB.Model.ObjectGeometry[j - ObjectA.Model.ObjectGeometry.Length];
                    Axis = (v1 - v2).Perp();
                }

                if (SeparatedByAxis(Axis,ObjectA.Model, ObjectB.Model, ref Ret))
                    return null;
            }
            
            return Ret;
        }

        /// <summary>
        /// Sestaví report o kolizích v momentálním stavu systému pro daný objekt
        /// </summary>
        /// <returns>Výčet reportů o kolizích</returns>
        public IEnumerable DetectCollisionsFor(int ObjectIndex)
        {
            if (!Enabled) yield break;
            for (int i = 0; i < w.CountObjects; i++)
            {
                if (i == ObjectIndex) continue;
                CollisionReport rpt = ObjectsCollide(w[ObjectIndex],w[i]);
                if (rpt != null) yield return rpt;
            }
        }

        /// <summary>
        /// Vyřeší konkrétní kolizi
        /// </summary>
        /// <param name="Report">Report o kolizi</param>
        public void SolveCollision(CollisionReport Report)
        {
            return;
        }


    }
}
