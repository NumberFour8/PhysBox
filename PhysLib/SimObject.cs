using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhysLib
{
    public class SimObject
    {
        private double m;
        private double q;
    
        public double Mass
        {
            get
            {
                return m;
            }
            set
            {
                if (m > 0)
                  m = value;
                else throw new ArgumentException();
            }
        }

        public Vector LinearVelocity
        {
            get;
            set;
        }

        public bool Enabled
        {
            get; set;
        }

        public Field ForceField
        {
            get;
            set;
        }

        public Geometry Model
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Vector AngularVelocity
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
}
