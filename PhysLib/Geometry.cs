using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhysLib
{

    public abstract class Geometry
    {
        public event EventHandler OnStateChange;

        public event EventHandler OnCollision;
    
        public double Volume
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public double Surface
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Vector Position
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Matrix Orientation
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public void CentralDistance(Geometry obj)
        {
            throw new System.NotImplementedException();
        }

        public void Distance(Geometry obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
