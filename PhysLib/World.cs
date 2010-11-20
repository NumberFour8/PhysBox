using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PhysLib
{

    public enum Axes
    {
        X = 1,Y = 2,Z = 3
    }

    public class World
    {   
        public static const Vector X = new Vector(1, 0, 0);
        public static const Vector Y = new Vector(0, 1, 0);
        public static const Vector Z = new Vector(0, 0, 1);
        public static const Matrix B = new Matrix(MatrixInitType.VectorsAreRows,X, Y, Z);

        private ArrayList Objs;
        private Vector cons;
        private Matrix o;
        private uint simulationTime;

        private object SimLock = null;

        public World(Vector vConstraints,Matrix mOrientation,Vector G)
        {
            cons = vConstraints;
            o = mOrientation;
            Gravity = G;
            simulationTime = 0;
        }

        public Vector Gravity
        {
            get;
            set;
        }

        public uint CurrentTimeFrame
        {
            get { return simulationTime; }
        }

        public Vector Constraints
        {
            get { return cons; }
        }

        public Matrix Orientation
        {
            get { return o;    }
        }

        public double OrientationOf(Axes Axis)
        {
            return Vector.Angle(B.GetRow((int)Axis - 1), Orientation.GetRow((int)Axis - 1));
        }

        public double Aether
        {
            get;
            set;
        }

        public void Tick()
        {
            lock (SimLock)
            {
               SimObject[] PhysObjs = (SimObject[])Objs.ToArray(typeof(SimObject));
               for (int i = 0; i < Objs.Count; i++)
               {
                   PhysObjs[i].ApplyForce(PhysObjs[i].Mass * Gravity,PhysObjs[i].Model.COG);
                   PhysObjs[i].Reset();
               }
            }
        }
    }
}
