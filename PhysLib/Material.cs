using System;

namespace PhysLib
{
    /// <summary>
    /// Reprezentuje materiál
    /// </summary>
    [Serializable]
    public struct Material
    {
        public static Material Wood { get { return new Material(700, 0.4, 0, 0.15, "Dřevo"); } }
        public static Material Concrete { get { return new Material(2000, 0.65, 0, 0.1, "Beton"); } }
        public static Material Steel { get { return new Material(7300, 0.43, 0.00720, 0.3, "Ocel"); } }
        public static Material Rubber { get { return new Material(1000, 0.7, 0, 0.8, "Guma"); } } 
        
        private double density;
        private double frictioncoeff;
        private double susceptibility;
        private double restitution;
        private string name;

        /// <summary>
        /// Výchozí konstruktor materiálu
        /// </summary>
        /// <param name="dDensity">Hustota</param>
        /// <param name="dFriction">Průměrný součinitel smykového tření</param>
        /// <param name="dSusceptibility">Magnetická susceptibilita materiálu</param>
        /// <param name="dRestitution">Průměrný koeficient restituce materiálu</param>
        /// <param name="sName">Název materiálu</param>
        public Material(double dDensity, double dFriction, double dSusceptibility,double dRestitution, string sName)
        {
            density = dDensity;
            frictioncoeff = dFriction;
            susceptibility = dSusceptibility;
            restitution = dRestitution;
            name = sName;
        }

        /// <summary>
        /// Hustota matriálu
        /// </summary>
        public double Density
        {
            get
            {
                return density;
            }
        }

        /// <summary>
        /// Součinitel smykového tření
        /// </summary>
        public double FrictionCoefficient
        {
            get
            {
                return frictioncoeff;
            }
        }

        /// <summary>
        /// Průměrný koeficient restituce
        /// </summary>
        public double RestitutionCoefficient
        {
            get
            {
                return restitution;
            }
        }

        /// <summary>
        /// Magnetická susceptibilita
        /// </summary>
        public double MagneticSusceptibility
        {
            get
            {
                return susceptibility;
            }
        }

        /// <summary>
        /// Název materiálu
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}
