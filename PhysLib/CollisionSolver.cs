﻿using System;
using System.Collections.Generic;
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
        /// <param name="ObjectA">Objekt A</param>
        /// <param name="ObjectB">Objekt B</param>
        /// <param name="MTD">Minimální vektor oddělení</param>
        /// <returns>True pokud je odděleno, False pokud nikoliv</returns>
        public static bool SeparatedByAxis(Vector Axis, Geometry ObjectA, Geometry ObjectB,ref Vector MTD)
        {
            double minA = 0, maxA = 0, minB = 0, maxB = 0;
            ObjectA.ProjectToAxis(Axis, ref minA, ref maxA);
            ObjectB.ProjectToAxis(Axis, ref minB, ref maxB);

	        double d0 = (maxB - minA);
	        double d1 = (minB - maxA);

	        if(d0 < 0.0f || d1 > 0.0f) return true;
            
            double overlap = (d0 < -d1) ? d0 : d1;

            Vector Sep = Axis * (overlap / Vector.Pow(Axis, 2));

            if (MTD == null || MTD.IsNull || Sep.Magnitude < MTD.Magnitude)
                MTD = Sep;

            return false;

        }

        /// <summary>
        /// Zjistí, zda objekt koliduje s jiným - daným objektem
        /// </summary>
        /// <param name="ObjectA">Účastník kolize</param>
        /// <param name="ObjectB">Účastník kolize</param>
        /// <returns>Hlášení o kolizi nebo null</returns>
        public static CollisionReport ObjectsCollide(SimObject ObjectA,SimObject ObjectB)
        {
            PointF[] geomA = ObjectA.Model.BoundingBox,geomB = ObjectB.Model.BoundingBox;
            Vector mtd = null;
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

                if (SeparatedByAxis(Axis,ObjectA.Model, ObjectB.Model, ref mtd))
                    return null;
            }

            return new CollisionReport(ObjectA, ObjectB, mtd);
        }

        /// <summary>
        /// Sestaví report o kolizích v momentálním stavu systému pro daný objekt
        /// </summary>
        /// <returns>Výčet reportů o kolizích</returns>
        public List<CollisionReport> DetectCollisionsFor(int ObjectIndex)
        {
            List<CollisionReport> Output = new List<CollisionReport>();
            if (!Enabled || w.CountObjects < 2) return Output;
            for (int i = 0; i < w.CountObjects; i++)
            {
                if (i == ObjectIndex || !w[i].Enabled || w[i].Static) continue;
                CollisionReport rpt = ObjectsCollide(w[ObjectIndex],w[i]);
                if (rpt != null) Output.Add(rpt);
            }
            return Output;
        }

        internal void SolveCollision_Experimental(CollisionReport Report) // Experimentální!
        {
            double cof = 0.3, cor = 0.5;
            double C = 1 / (1/Report.A.Mass + 1/Report.B.Mass);
            double AinvM = 1 / Report.A.Mass, BinvM = 1 / Report.B.Mass,AinvI = 1/Report.A.MomentOfInertia,BinvI = 1/Report.B.MomentOfInertia;

            Report.A.Model.Position += Report.MTD * (1 / Report.A.Mass * C);
            Report.B.Model.Position -= Report.MTD * (1 / Report.B.Mass * C);           

            // apply friction impulses at contacts
            Vector pa = (Vector)Report.Pairs[0].A;
            Vector pb = (Vector)Report.Pairs[0].B;
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
            double k = Math.Max(Report.A.ObjMaterial.RestitutionCoefficient,Report.B.ObjMaterial.RestitutionCoefficient);
            double f = Math.Max(Report.A.ObjMaterial.FrictionCoefficient, Report.B.ObjMaterial.FrictionCoefficient);
            double M = 1 / (1 / Report.A.Mass + 1 / Report.B.Mass);
            
            double I = (Math.Pow(Vector.Dot(Report.NAP, Report.N), 2) / Report.A.MomentOfInertia) + (Math.Pow(Vector.Dot(Report.NBP, Report.N), 2) / Report.B.MomentOfInertia);
            double Num = (-1 - k) * Vector.Dot(Report.RelativeVelocity, Report.N);
            double Denom = Vector.Dot(Report.N, Report.N) * ((1 / Report.A.Mass) + (1 / Report.B.Mass)) + I;
            double Impulse_C = Math.Round(Num / Denom,2);
            
            Report.A.Model.Position += Report.MTD / Report.A.Mass * M * (Report.B.Static ? 2.1 : 1);
            Report.B.Model.Position += -Report.MTD / Report.B.Mass * M * (Report.A.Static ? 2.1 : 1);

            if (Double.IsNaN(Impulse_C)) return;

            Report.A.LinearVelocity += Vector.Floor(Report.N * (Impulse_C / Report.A.Mass));
            Report.A.AngularVelocity[2] += Vector.Dot(Report.N, Report.NAP) * (Impulse_C / Report.A.MomentOfInertia);            

            Report.B.LinearVelocity -= Vector.Floor(Report.N * (Impulse_C / Report.B.Mass));
            Report.B.AngularVelocity[2] -= Vector.Dot(Report.N, Report.NBP) * (Impulse_C / Report.B.MomentOfInertia);

            Report.A.Model.RaiseOnCollision(Report);
            Report.B.Model.RaiseOnCollision(Report);
        }

    }
}
