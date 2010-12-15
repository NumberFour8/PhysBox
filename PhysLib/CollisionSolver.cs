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
        internal CollisionSolver(World Reference)
        {
            w = Reference;
            Enabled = true;
            E = 0.5;
        }

        /// <summary>
        /// Indikuje, zda je detekce kolizí zapnuta
        /// </summary>
        public bool Enabled
        {
            get; set;
        }

        /// <summary>
        /// Koeficient restituce
        /// </summary>
        public double E
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
        /// <param name="ObjectA">Účastník kolize</param>
        /// <param name="ObjectB">Účastník kolize</param>
        /// <returns>Hlášení o kolizi nebo null</returns>
        public CollisionReport ObjectsCollide(SimObject ObjectA,SimObject ObjectB)
        {
            CollisionReport Ret = new CollisionReport(ObjectA,ObjectB);
            PointF[] geomA = ObjectA.Model.ObjectGeometry;
            PointF[] geomB = ObjectB.Model.ObjectGeometry;

            int Count = geomA.Length + geomB.Length;
            for (int i = 0, j; i < Count; i++)
            {
                Vector Axis = null;
                if (i < geomA.Length)
                {
                    j = (i == geomA.Length - 1) ? 0 : i + 1;
                    Vector v1 = (Vector)geomA[i];
                    Vector v2 = (Vector)geomA[j];
                    Axis = (v1 - v2).Perp();
                }
                else
                {
                    j = (i == Count - 1) ? geomA.Length : i + 1;
                    Vector v1 = (Vector)geomB[i - geomA.Length];
                    Vector v2 = (Vector)geomB[j - geomA.Length];
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
        /// <param name="Report">Hlášení o kolizi</param>
        public void SolveCollision(CollisionReport Report)
        {
            Vector RelativeVelo = Report.A.LinearVelocity - Report.B.LinearVelocity;
            Vector N = Vector.Unit(Report.MTD);

            double Num = (-1 - E) * Vector.Dot(RelativeVelo, N);
            double C = 1 / (Report.A.Mass + Report.B.Mass);

            Vector AP = ((Vector)Report.Pairs[0].a - Report.A.COG).Perp(), BP = ((Vector)Report.Pairs[0].b - Report.B.COG).Perp();
            double I = (Math.Pow(Vector.Dot(AP, N), 2) / Report.A.MomentOfInertia) + (Math.Pow(Vector.Dot(BP, N), 2) / Report.B.MomentOfInertia);
            double Denom = Vector.Dot(N, N) * ((1 / Report.A.Mass) + (1 / Report.B.Mass)) + I;

            double Impulse = Num / Denom;

            Report.A.Model.Position += Report.MTD * Report.A.Mass * C;
            Report.A.LinearVelocity += N * (Impulse / Report.A.Mass);
            Report.A.AngularVelocity[2] += Vector.Dot(N, AP) * (Impulse / Report.A.MomentOfInertia);

            Report.B.Model.Position += -Report.MTD * Report.B.Mass * C;
            Report.B.LinearVelocity += -N * (Impulse / Report.B.Mass);
            Report.B.AngularVelocity[2] += Vector.Dot(N, BP) * (Impulse / Report.B.MomentOfInertia);

            Report.A.Model.RaiseOnCollision(Report);
            Report.B.Model.RaiseOnCollision(Report);
        }

    }
}
