using System;

namespace PhysLib
{
    /// <summary>
    /// Reprezentuje těleso ve fyzikálním světě
    /// </summary>
    public class SimObject
    {
        private double m;
        private Vector totalForce,totalTorque;
        private Geometry model;

        /// <summary>
        /// Vytvoří těleso s danou geometrií a hmotností
        /// </summary>
        /// <param name="gModel">Model tělesa</param>
        /// <param name="dMass">Hmotnost tělesa</param>
        public SimObject(Geometry gModel, double dMass)
        {
            model = gModel;
            Mass = dMass;
         
            Enabled = true;
        }

        /// <summary>
        /// Hmotnost tělesa
        /// </summary>
        public double Mass
        {
            get { return m; }
            set
            {
                if (m > 0)
                  m = value;
                else throw new ArgumentException();
            }
        }

        /// <summary>
        /// Moment setrvačnosti tělesa vzhledem k ose otáčení
        /// </summary>
        public double MomentOfInertia
        {
            get { return ((Mass * Math.Pow(Model.Height, 2) + Math.Pow(Model.Width, 2)) / 12)+((COG-RotationPoint).Magnitude*Mass); }
        }

        /// <summary>
        /// Souřadnice těžiště tělesa
        /// </summary>
        public Vector COG
        {
            get { return model.Centroid; }
        }

        /// <summary>
        /// Výslednice všech sil působící na těleso
        /// </summary>
        public Vector TotalForce
        {
            get { return totalForce; }
        }

        /// <summary>
        /// Výslednice všech točivých momentů působící na těleso
        /// </summary>
        public Vector TotalTorque
        {
            get { return totalTorque; }
        }

        /// <summary>
        /// Indikuje, zda je těleso zapnuté
        /// </summary>
        public bool Enabled
        {
            get; set;
        }

        /// <summary>
        /// Fyzický model tělesa
        /// </summary>
        public Geometry Model
        {
            get
            {
                return model;
            }
        }

        /// <summary>
        /// Bod, jímž prochází osa otáčení
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
            totalForce  += Force;
            totalTorque += Vector.Cross(Origin - RotationPoint, Force);
        }

        /// <summary>
        /// Zruší všechny síly působící na těleso
        /// </summary>
        public void Reset()
        {
            totalForce.Null();
            totalTorque.Null();
        }
    }
}
