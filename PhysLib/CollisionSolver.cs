using System;
using System.Collections;
using System.Drawing;

namespace PhysLib
{   

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
            
            Ret.CalculatePairs();
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
            Report.A.Model.RaiseOnCollision(Report);
            Report.B.Model.RaiseOnCollision(Report);
        }

    }
}
