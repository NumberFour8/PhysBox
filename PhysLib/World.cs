using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhysLib
{
    public class World
    {
        private uint lastTick;

        public PhysBox.Field[] Fields
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Fluid[] Fluids
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Geometry Constraints
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

        public void Tick()
        {
            throw new System.NotImplementedException();
        }
    }
}
