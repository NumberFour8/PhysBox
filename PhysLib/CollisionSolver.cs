using System;
using System.Collections;
using System.Drawing;

namespace PhysLib
{
    public struct CollisionReport
    {
        public const double Epsilon = 1;

        public SimObject A,B;
        public RectangleF Intersection;
        public CollisionReport(SimObject a, SimObject b, RectangleF i)
        {
            A = a;
            B = b;
            Intersection = i;
        }

        public bool Penetrating
        {
            get
            {
                return Intersection.Width > Epsilon;
            }
        }
    }

    public sealed class CollisionSolver
    {
        private World w;
        private ArrayList Solved;
        
        public CollisionSolver(World Reference)
        {
            w = Reference;
            Solved = new ArrayList();
        }

        /// <summary>
        /// Indikuje, zda je detekce kolizí zapnuta
        /// </summary>
        public bool Enabled
        {
            get; set;
        }

        /// <summary>
        /// Zresetuje stav vyřešených kolizí
        /// </summary>
        public void Reset()
        {
            Solved.Clear();
        }

        /// <summary>
        /// Sestaví report o kolizích v momentálním stavu systému pro daný objekt
        /// </summary>
        /// <returns>Výčet reportů o kolizích</returns>
        public IEnumerable DetectCollisionsFor(int ObjectIndex)
        {
            /*  if (!Enabled) yield break;
            if (ObjectIndex >= w.CountFields || ObjectIndex < 0) throw new IndexOutOfRangeException();

            SimObject Obj = w[ObjectIndex];
            int l = w.Objects.Length;
            
            for (int i = 0; i < l; i++)
            {
                if (i == ObjectIndex) continue;
                if (Solved.Contains(i)) continue;

                RectangleF c = RectangleF.Intersect(w[i].Model.BoundingBox,Obj.Model.BoundingBox);
                if (c.IsEmpty) continue;

                Solved.Add(i);

                yield return new CollisionReport(Obj,w[i],c);
            }*/
            yield break;
        }

        /// <summary>
        /// Vyřeší konkrétní kolizi
        /// </summary>
        /// <param name="Report">Report o kolizi</param>
        public void SolveCollision(CollisionReport Report)
        {
            Report.A.Model.RaiseOnCollision(Report.B);
            Report.B.Model.RaiseOnCollision(Report.A);
            
            throw new NotImplementedException();
        }


    }
}
