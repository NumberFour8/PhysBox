using System;
using System.Collections;

namespace PhysLib
{
    /// <summary>
    /// Výčet všech prostorových os
    /// </summary>
    public enum Axes
    {
        X = 1, Y = 2, Z = 3
    }

    /// <summary>
    /// Reprezentuje fyzikální svět
    /// </summary>
    public class World
    {   
        /// <summary>
        /// Směr osy X
        /// </summary>
        public static const Vector X = new Vector(1, 0, 0);
        
        /// <summary>
        /// Směr osy Y
        /// </summary>
        public static const Vector Y = new Vector(0, 1, 0);
        
        /// <summary>
        /// Směr osy Z
        /// </summary>
        public static const Vector Z = new Vector(0, 0, 1);
        
        /// <summary>
        /// Matice souřadných os
        /// </summary>
        public static const Matrix B = new Matrix(MatrixInitType.VectorsAreRows, X, Y, Z);

        /// <summary>
        /// Průměrné zemské gravitační zrychlení
        /// </summary>
        public static const Vector Ag = new Vector(0, -9.89, 0);

        private ArrayList Objs,Fields;
        private Vector cons;
        private Matrix o;
        private uint simulationTime;

        private object SimLock = null;

        /// <summary>
        /// Vytvoří fyzikální svět
        /// </summary>
        /// <param name="vConstraints">Rozměry světa</param>
        /// <param name="mOrientation">Orientace os</param>
        /// <param name="g">Gravitační zrychlení</param>
        public World(Vector vConstraints,Matrix mOrientation,Vector g)
        {
            cons = vConstraints;
            o = mOrientation;
            Gravity = g;
            simulationTime = 0;
        }

        /// <summary>
        /// Vektor gravitace
        /// </summary>
        public Vector Gravity
        {
            get; set;
        }

        /// <summary>
        /// Aktuální krok simulace
        /// </summary>
        public uint CurrentTimeFrame
        {
            get { return simulationTime; }
        }

        /// <summary>
        /// Rozměry světa
        /// </summary>
        public Vector Constraints
        {
            get { return cons; }
        }

        /// <summary>
        /// Orientace světa
        /// </summary>
        public Matrix Orientation
        {
            get { return o; }
        }

        /// <summary>
        /// Získá úhel orientace dané osy vzhledem k bázovému směru osy
        /// </summary>
        /// <param name="Axis">Osa</param>
        /// <returns>Úhel</returns>
        public double OrientationOf(Axes Axis)
        {
            return Vector.Angle(B.GetRow((int)Axis - 1), Orientation.GetRow((int)Axis - 1));
        }

        /// <summary>
        /// Vše obklopující fluidum způsobující disipaci sil
        /// </summary>
        public double Aether
        {
            get;
            set;
        }

        /// <summary>
        /// Silová pole
        /// </summary>
        public Field[] ForceFields { get { return (Field[])Fields.ToArray(typeof(Field)); } }


        /// <summary>
        /// Získá/změní těleso ve světě
        /// </summary>
        /// <param name="index">Index tělesa</param>
        /// <returns>Těleso</returns>
        public SimObject this[int index]
        {
            get { return (SimObject)Objs[index]; }
            set { Objs[index] = value; }
        }

        /// <summary>
        /// Přidá fyzikální pole
        /// </summary>
        /// <param name="F">Pole sil</param>
        /// <returns>Index pole</returns>
        public int AddField(Field F)
        {
            lock (SimLock)
            {
                Fields.Add(F);
            }
            return Fields.Count - 1;
        }

        /// <summary>
        /// Přidá těleso do světa
        /// </summary>
        /// <param name="O">Těleso</param>
        /// <returns>Index tělesa</returns>
        public int AddObject(SimObject O)
        {
            lock (SimLock)
            {
                Objs.Add(O);
            }
            return Objs.Count - 1;
        }

        /// <summary>
        /// Provede simulační krok
        /// </summary>
        public void Tick()
        {
            double Delta = 10;
            lock (SimLock)
            {
               SimObject[] PhysObjs = (SimObject[])Objs.ToArray(typeof(SimObject));
               for (int i = 0; i < Objs.Count; i++)
               {
                   if (!PhysObjs[i].Enabled) continue;

                   foreach (Field f in ForceFields)
                   {
                       if (f.Enabled)
                         PhysObjs[i].ApplyForce(f.GetForce(f, PhysObjs[i]), PhysObjs[i].COG);
                   }
                   
                   PhysObjs[i].ApplyForce(PhysObjs[i].Mass * Gravity,PhysObjs[i].COG);

                   PhysObjs[i].Model.Position += PhysObjs[i].LinearVelocity * Delta;
                   PhysObjs[i].Model.Orientation *= Matrix.Make3DRotation(PhysObjs[i].AngularVelocity[0] * Delta, PhysObjs[i].AngularVelocity[1] * Delta, PhysObjs[i].AngularVelocity[2] * Delta);

                   PhysObjs[i].LinearVelocity += PhysObjs[i].TotalForce * Delta / PhysObjs[i].Mass;
                   PhysObjs[i].AngularVelocity += PhysObjs[i].TotalTorque * Delta / PhysObjs[i].MomentOfInertia;

                   PhysObjs[i].Reset();                   
               }
            }
        }
    }
}
