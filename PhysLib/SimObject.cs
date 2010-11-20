using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhysLib
{
    public class SimObject
    {
        private double m;
        private Vector TotalForce,TotalTorque,RotPoint;
        private Geometry model;

        public SimObject(Geometry gModel, double dMass)
        {
            RotPoint = gModel.COG;
            model = gModel;
            Mass = dMass;
            Enabled = true;
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

        public bool Enabled
        {
            get; set;
        }

        public Field ForceField
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
            TotalForce  += Force;
            TotalTorque += Vector.Cross(Origin - RotPoint, Force);
        }

        public void Reset()
        {
            TotalForce.Null();
            TotalTorque.Null();
        }
    }
}
