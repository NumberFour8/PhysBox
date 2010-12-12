using System;
using System.Collections;
using System.Drawing;

namespace PhysLib
{
    public struct CollisionReport
    {
        public SimObject A,B;
        public Vector MTD;

    }

    public sealed class CollisionSolver
    {
        private World w;
        
        public CollisionSolver(World Reference)
        {
            w = Reference;            
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
        public bool SeparatedByAxis(Vector Axis, Geometry ObjectA, Geometry ObjectB)
        {

            double minA = 0, maxA = 0, minB = 0, maxB = 0;
            ObjectA.ProjectToAxis(Axis, ref minA, ref maxA);
            ObjectB.ProjectToAxis(Axis, ref minB, ref maxB);

            return (minA > maxB) || (minB > maxA);
        }

        /// <summary>
        /// Zjistí, zda objekt koliduje s jiným - daným objektem
        /// </summary>
        /// <param name="Object"></param>
        /// <returns></returns>
        public bool ObjectsCollide(Geometry ObjectA,Geometry ObjectB)
        {
            for (int i = 0, j = ObjectA.ObjectGeometry.Length - 1; i < ObjectA.ObjectGeometry.Length; j = i, i++)
            {
                Vector v1 = (Vector)ObjectA.ObjectGeometry[i];
                Vector v2 = (Vector)ObjectA.ObjectGeometry[j];
                Vector Axis = (v1 - v2).Perp();

                if (SeparatedByAxis(Axis, ObjectA,ObjectB))
                    return false;
            }
            for (int i = 0, j = ObjectB.ObjectGeometry.Length - 1; i < ObjectB.ObjectGeometry.Length; j = i, i++)
            {
                Vector v1 = (Vector)ObjectB.ObjectGeometry[i];
                Vector v2 = (Vector)ObjectB.ObjectGeometry[j];
                Vector Axis = (v1 - v2).Perp();
                if (SeparatedByAxis(Axis, ObjectA,ObjectB))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Sestaví report o kolizích v momentálním stavu systému pro daný objekt
        /// </summary>
        /// <returns>Výčet reportů o kolizích</returns>
        public IEnumerable DetectCollisionsFor(int ObjectIndex)
        {
            for (int i = 0; i < w.CountObjects; i++)
            {
                if (i == ObjectIndex) continue;
                if (ObjectsCollide(w[ObjectIndex].Model,w[i].Model))
                    throw new ExecutionEngineException("Hop, kolize!");
            }
            yield break;
        }

        /// <summary>
        /// Vyřeší konkrétní kolizi
        /// </summary>
        /// <param name="Report">Report o kolizi</param>
        public void SolveCollision(CollisionReport Report)
        {
            
            throw new NotImplementedException();
        }


    }
}
