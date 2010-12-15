﻿using System;

namespace PhysLib
{
    /// <summary>
    /// Reprezentuje těleso ve fyzikálním světě
    /// </summary>
    public class SimObject
    {
        private double m,J;
        private Vector totalForce,totalTorque;
        private Geometry model;

        /// <summary>
        /// Vytvoří těleso s danou geometrií a hmotností
        /// </summary>
        /// <param name="gModel">Model tělesa</param>
        /// <param name="dMass">Hmotnost tělesa</param>
        public SimObject(Geometry ObjectModel, double ObjectMass)
        {
            model = ObjectModel;
            Mass = ObjectMass;
            
            totalForce = new Vector(3);
            totalTorque = new Vector(3);
            LinearVelocity = new Vector(3);
            AngularVelocity = new Vector(3);

            Enabled = true;
            NoTranslations = false;
            IsStatic = false;

            // Předpočítej část momentu setrvačnosti
            double denom = 0,nom = 0,factor = 0;
            for (int i = 0, j = 0; i < Model.ObjectGeometry.Length; i++)
            {
                j = (i + 1) % Model.ObjectGeometry.Length;
                factor = Vector.Pow((Vector)Model.ObjectGeometry[j], 2) + Vector.Dot((Vector)Model.ObjectGeometry[j], (Vector)Model.ObjectGeometry[i]) + Vector.Pow((Vector)Model.ObjectGeometry[i], 2);
                nom += Vector.Cross(((Vector)Model.ObjectGeometry[j]), ((Vector)Model.ObjectGeometry[i])).Magnitude * factor;
                denom += Vector.Cross(((Vector)Model.ObjectGeometry[j]), ((Vector)Model.ObjectGeometry[i])).Magnitude;

            }
            J = nom / denom;
        }

        /// <summary>
        /// Hmotnost tělesa
        /// </summary>
        public double Mass
        {
            get { return m; }
            set
            {
                if (value > 0)
                  m = value;
                else throw new ArgumentException();
            }
        }

        /// <summary>
        /// Spočítá moment setrvačnosti tělesa v závislosti na rozlišení
        /// </summary>
        /// <param name="Resolution">Rozlišení</param>
        /// <returns>Moment setrvačnosti</returns>
        public double MomentOfInertia
        {
            get
            {
                double d = Math.Round(Vector.PointDistance(RotationPoint, COG),1);
                return (J * m / 6) + m * Math.Pow(d,2);
            }
        }

        /// <summary>
        /// Absolutní souřadnice těžiště tělesa (poloha tělesa)
        /// </summary>
        public Vector COG
        {
            get { return model.Position; }
        }

        /// <summary>
        /// Výslednice všech sil působící na těleso
        /// </summary>
        public Vector TotalForce
        {
            get { return totalForce; }
            set { totalForce = value; }
        }

        /// <summary>
        /// Výslednice všech točivých momentů působící na těleso
        /// </summary>
        public Vector TotalTorque
        {
            get { return totalTorque; }
            set { totalTorque = value; }
        }

        /// <summary>
        /// Indikuje, zda je těleso zapnuté
        /// </summary>
        public bool Enabled
        {
            get; set;
        }

        /// <summary>
        /// Indikuje, zda zakázány lineární translační pohyby tělesa
        /// </summary>
        public bool NoTranslations
        {
            get; set;
        }

        /// <summary>
        /// Fyzický model tělesa
        /// </summary>
        public Geometry Model
        {
            get { return model; }
        }

        /// <summary>
        /// Absolutní poloha bodu, jímž prochází osa otáčení
        /// </summary>
        public Vector RotationPoint
        {
            get { return model.Nail;  }
            set { model.Nail = value; }
        }

        /// <summary>
        /// Rychlost tělesa
        /// </summary>
        public Vector LinearVelocity
        {
            get; set;
        }

        /// <summary>
        /// Úhlová rychlost tělesa
        /// </summary>
        public Vector AngularVelocity
        {
            get; set;
        }

        /// <summary>
        /// Libovolné parametry
        /// </summary>
        public object[] Parameters
        {
            get; set;
        }

        /// <summary>
        /// Materiálové konstanty tělesa
        /// </summary>
        public Material ObjectMaterial
        {
            get;
            set;
        }

        /// <summary>
        /// Aplikuje sílu na těleso
        /// </summary>
        /// <param name="Force">Vektor síly</param>
        /// <param name="Origin">Působiště síly</param>
        public void ApplyForce(Vector Force,Vector Origin)
        {
            if (Force.IsNull) return;
            totalForce += Force;

            totalTorque += Vector.Cross(GetTorqueIntersection(Force, Model.ProjectToObject(Origin)) - RotationPoint, Force);
        }

        /// <summary>
        /// Aplikuje na těleso odporovou sílu pohybuje-li se v prostředí o dané hustotě
        /// </summary>
        /// <param name="EnvDensity">Hustota prostředí, ve kterém se těleso pohybuje</param>
        public void ApplyDrag(double EnvDensity)
        {
            if (LinearVelocity.IsNull) return;
            double DragSize = 0.5 * Model.DragCoefficient * EnvDensity * (Model.Surface - 2 * Model.FrontalArea);
            ApplyForce(Vector.Unit(-LinearVelocity) * Math.Round(DragSize * Vector.Pow(LinearVelocity, 2),3), COG);
        }

        /// <summary>
        /// Získá bod ve kterém se protíná prodloužený vektor síly a vektor ramene síly
        /// </summary>
        /// <param name="Force">Síla působící na těleso</param>
        /// <param name="Origin">Působiště síly</param>
        /// <returns>Průsečík ramene a síly</returns>
        public Vector GetTorqueIntersection(Vector Force,Vector Origin)
        {
            Vector P = new Vector(3);
            double c = (Force[0] * (RotationPoint[0] - Origin[0]) + Force[1] * (RotationPoint[1] - Origin[1])) / Math.Pow(Force.Magnitude, 2);

            P[0] = Origin[0] + Force[0] * c;
            P[1] = Origin[1] + Force[1] * c;
            P[2] = 0;

            if (Vector.PointDistance(P, RotationPoint) < 3) return RotationPoint;

            return P;
        }

        /// <summary>
        /// Zruší všechny síly působící na těleso
        /// </summary>
        public void Reset()
        {
            totalForce.Null();
            totalTorque.Null();
        }

        /// <summary>
        /// Zruší všechny síly a zastaví těleso
        /// </summary>
        public void ResetAll()
        {
            totalForce.Null();
            totalTorque.Null();
            LinearVelocity.Null();
            AngularVelocity.Null();
        }

        /// <summary>
        /// Indikuje, zda je těleso nehybné (např. zeď)
        /// </summary>
        public bool IsStatic
        {
            get; set;
        }

        /// <summary>
        /// Potenciální energie tělesa v místě vložení
        /// </summary>
        public double InitialEnergy
        {
            get; set;
        }
    }
}
