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
            PointF[] geomA = null, geomB = null;
            
            if (!ObjectA.Model.Convex)
                geomA = ObjectA.Model.BoundingBox;
            else geomA = ObjectA.Model.ObjectGeometry;
            
            if (!ObjectB.Model.Convex)
                geomB = ObjectB.Model.BoundingBox;
            else geomB = ObjectB.Model.ObjectGeometry;

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
            if (!Enabled || w.CountObjects < 2) yield break;
            for (int i = 0; i < w.CountObjects; i++)
            {
                if (i == ObjectIndex) continue;
                CollisionReport rpt = ObjectsCollide(w[ObjectIndex],w[i]);
                if (rpt != null) yield return rpt;
            }
        }

        public void SolveCollision_Experimental(CollisionReport Report) // Experimentální!
        {
            double cof = 0.3, cor = E;
            double C = 1 / (1/Report.A.Mass + 1/Report.B.Mass);
            double AinvM = 1 / Report.A.Mass, BinvM = 1 / Report.B.Mass,AinvI = 1/Report.A.MomentOfInertia,BinvI = 1/Report.B.MomentOfInertia;

            Report.A.Model.Position += Report.MTD * (1 / Report.A.Mass) * C;
            Report.B.Model.Position -= Report.MTD * (1 / Report.B.Mass) * C;           

            // apply friction impulses at contacts
            Vector pa = (Vector)Report.Pairs[0].a;
            Vector pb = (Vector)Report.Pairs[0].b;
            Vector ncol = Vector.Unit(Report.MTD);
            Vector ra = pa - Report.A.Model.Position;
            Vector rb = pb - Report.A.Model.Position;
            Vector va = Report.A.LinearVelocity + (ra.Perp()*Report.A.AngularVelocity[2]);
            Vector vb = Report.B.LinearVelocity + (rb.Perp()*Report.B.AngularVelocity[2]);
            Vector v = (va - vb);
            Vector vt = v - ncol*Vector.Dot(v,ncol);
            Vector nf = Vector.Unit(-vt); // friction normal

            // contact points separating, no impulses.
            if (Vector.Dot(v,ncol) > 0.0f) return;

            // collision impulse
            double jc = Vector.Dot(v,ncol) / ((AinvM + BinvM) +
                                        Vector.Dot(Vector.Cross(ra,ncol) , Vector.Cross(ra,ncol)) * AinvI +
                                        Vector.Dot(Vector.Cross(rb,ncol) , Vector.Cross(rb,ncol)) * BinvI);

            // friction impulse
            double jf = Vector.Dot(v,nf) / ((AinvM + BinvM) +
                                        Vector.Dot(Vector.Cross(ra,nf),Vector.Cross(ra,nf)) * AinvI +
                                        Vector.Dot(Vector.Cross(rb,nf),Vector.Cross(rb,nf)) * BinvI);

            // clamp friction. 
            if (Math.Abs(jf) > Math.Abs(jc * cof))
                jf = Math.Abs(cof) * Math.Sign(jc);

            // total impulse restituted
            Vector impulse = ncol * (jc * -(1 + cor)) + nf * (-jf);

            Report.A.LinearVelocity += impulse * AinvM;
            Report.B.LinearVelocity -= impulse * BinvM;

            Report.A.AngularVelocity += Vector.Cross(ra,impulse) * AinvI;
            Report.B.AngularVelocity -= Vector.Cross(rb,impulse) * BinvI;
        }

        /// <summary>
        /// Vyřeší konkrétní kolizi
        /// </summary>
        /// <param name="Report">Hlášení o kolizi</param>
        public void SolveCollision(CollisionReport Report)
        {
            Vector AP = ((Vector)Report.Pairs[0].b - Report.A.COG).Perp(), BP = ((Vector)Report.Pairs[0].a - Report.B.COG).Perp();

            Vector RelativeVelo = (Report.A.LinearVelocity + AP*Report.A.AngularVelocity[2]) - (Report.B.LinearVelocity+BP*Report.B.AngularVelocity[2]);
            Vector N = Vector.Unit(Report.MTD);

            double C = 1 / (1 / Report.A.Mass + 1 / Report.B.Mass);
            double I = (Math.Pow(Vector.Dot(AP, N), 2) / Report.A.MomentOfInertia) + (Math.Pow(Vector.Dot(BP, N), 2) / Report.B.MomentOfInertia);

            double Num = (-1 - E) * Vector.Dot(RelativeVelo, N);
            double Denom = Vector.Dot(N, N) * ((1 / Report.A.Mass) + (1 / Report.B.Mass)) + I;
            double Impulse_F = Num / Denom;

            Report.A.Model.Position += Report.MTD * (1 / Report.A.Mass) * C;

            Report.A.LinearVelocity += N * (Impulse_F / Report.A.Mass);
            Report.A.AngularVelocity[2] += Vector.Dot(N, AP) * (Impulse_F / Report.A.MomentOfInertia);

            Report.B.Model.Position += -Report.MTD * (1 / Report.B.Mass) * C;

            Report.B.LinearVelocity -= N * (Impulse_F / Report.B.Mass);
            Report.B.AngularVelocity[2] -= Vector.Dot(N, BP) * (Impulse_F / Report.B.MomentOfInertia);

            Report.A.Model.RaiseOnCollision(Report);
            Report.B.Model.RaiseOnCollision(Report);
        }

    }
}
