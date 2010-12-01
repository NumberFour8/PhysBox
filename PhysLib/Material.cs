using System;

namespace PhysLib
{
    /// <summary>
    /// Reprezentuje materiál
    /// </summary>
    [Serializable]
    public struct Material
    {
        private double density;
        private double frictioncoeff;
        private double susceptibility;
        private string name;

        /// <summary>
        /// Výchozí konstruktor materiálu
        /// </summary>
        /// <param name="dDensity">Hustota</param>
        /// <param name="dFriction">Součinitel smykového tření</param>
        /// <param name="dSusceptibility">Magnetická susceptibilita materiálu</param>
        /// <param name="sName">Název materiálu</param>
        public Material(double dDensity, double dFriction, double dSusceptibility, string sName)
        {
            density = dDensity;
            frictioncoeff = dFriction;
            susceptibility = dSusceptibility;
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
    }
}
