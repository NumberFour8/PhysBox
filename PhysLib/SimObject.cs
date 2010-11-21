using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhysLib
{
    public class SimObject
    {
        private double m;
        private Vector totalForce,totalTorque,RotPoint;
        private Geometry model;

        public SimObject(Geometry gModel, double dMass)
        {
            model = gModel;
            Mass = dMass;
         
            Enabled = true;
            RotPoint = COG;
        }

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

        public double MomentOfInertia
        {
            get { return (Mass * Math.Pow(Model.Height, 2) + Math.Pow(Model.Width, 2)) / 12; }
        }

        public Vector COG
        {
            get { return model.Centroid; }
        }

        public Vector TotalForce
        {
            get { return totalForce; }
        }

        public Vector TotalTorque
        {
            get { return totalTorque; }
        }

        public bool Enabled
        {
            get; set;
        }

        public Geometry Model
        {
            get
            {
                return model;
            }
        }

        public Vector RotationPoint
        {
            get { return RotPoint;  }
            set { RotPoint = value; }
        }

        public Vector LinearVelocity
        {
            get;
            set;
        }

        public Vector AngularVelocity
        {
            get;
            set;
        }

        public void ApplyForce(Vector Force,Vector Origin)
        {
            totalForce  += Force;
            totalTorque += Vector.Cross(Origin - RotPoint, Force);
        }

        public void Reset()
        {
            totalForce.Null();
            totalTorque.Null();
        }
    }
}
