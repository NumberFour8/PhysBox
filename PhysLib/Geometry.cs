using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
namespace PhysLib
{

    public abstract class Geometry
    {
        public event EventHandler OnChange;
    
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

        public double Orientation
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public PointF[] ObjectGeometry
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
