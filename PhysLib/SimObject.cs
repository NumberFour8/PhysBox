using System;

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
        public SimObject(Geometry gModel, double dMass)
        {
            model = gModel;
            Mass = dMass;
            
            totalForce = new Vector(3);
            totalTorque = new Vector(3);
            LinearVelocity = new Vector(3);
            AngularVelocity = new Vector(3);

            Enabled = true;

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
        public double GetMomentOfInertia(double Resolution)
        {
             double d = Vector.PointDistance(RotationPoint,COG);
             return (J * Resolution * m / 6) + m * d;
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

            totalTorque += Vector.Cross(GetTorqueIntersection(Force,Origin) - RotationPoint, Force);
            return;
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
    }
}
