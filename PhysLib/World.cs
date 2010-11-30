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
    /// Definuje typ převodu podle rozlišení
    /// </summary>
    public enum ConversionType
    {
        MetersToPixels = 0, PixelsToMeters = 1
    }

    /// <summary>
    /// Reprezentuje fyzikální svět
    /// </summary>
    public class World
    {   
        /// <summary>
        /// Směr osy X
        /// </summary>
        public static readonly Vector X = new Vector(1, 0, 0);
        
        /// <summary>
        /// Směr osy Y
        /// </summary>
        public static readonly Vector Y = new Vector(0, 1, 0);
        
        /// <summary>
        /// Směr osy Z
        /// </summary>
        public static readonly Vector Z = new Vector(0, 0, 1);
        
        /// <summary>
        /// Standardní matice souřadných os
        /// </summary>
        public static readonly Matrix B = new Matrix(MatrixInitType.VectorsAreRows, X, Y, Z);

        /// <summary>
        /// Výchozí zobrazovací matice souřadných os na displeji
        /// </summary>
        public static readonly Matrix DisplayDefault = new Matrix(MatrixInitType.VectorsAreRows, X, -Y, Z);

        /// <summary>
        /// Průměrné zemské gravitační zrychlení
        /// </summary>
        public static readonly Vector EarthG = new Vector(0, -9.89, 0);        

        private ArrayList Objs,Fields;        

        private Matrix b;
        private double simulationTime,maxRad, r;

        private object SimLock = null;

        /// <summary>
        /// Vytvoří fyzikální svět
        /// </summary>
        /// <param name="WorldOrientation">Orientace os zobrazovacího zařízení</param>
        /// <param name="GravityAccel">Gravitační zrychlení</param>
        /// <param name="WorldConstraints">Poloměr světa v pixelech (maximální vzdálnost tělesa od počátku souřadnic)</param>
        /// <param name="WorldResolution">Počet pixelů který představuje jeden fyzický metr</param>
        public World(Matrix WorldOrientation,Vector GravityAccel,double WorldDiameter,double WorldResolution = 30)
        {
            Fields = new ArrayList();
            Objs = new ArrayList();

            
            b = WorldOrientation;
            maxRad = WorldDiameter;
            
            Gravity = Vector.ToBasis(b,GravityAccel)*WorldResolution;
            simulationTime = 0;
            r = WorldResolution;
            SimLock = new object();
        }

        /// <summary>
        /// Provede převod z metrů na pixely nebo z pixelů na metry podle daného rozlišení světa
        /// </summary>
        /// <param name="Input">Vstupní číslo</param>
        /// <param name="Type">Typ převodu</param>
        /// <returns>Převedené číslo</returns>
        public double Convert(double Input,ConversionType Type)
        {
            if (Type == ConversionType.MetersToPixels)
                return Input * r;
            else return Input / r;
        }

        /// <summary>
        /// Provede převod vektoru z metrů na pixely nebo z pixelů na metry podle daného rozlišení světa
        /// </summary>
        /// <param name="Input">Vstupní číslo</param>
        /// <param name="Type">Typ převodu</param>
        /// <returns>Převedené číslo</returns>
        public Vector Convert(Vector Input, ConversionType Type)
        {
            if (Type == ConversionType.MetersToPixels)
                return Input * r;
            else return Input / r;
        }

        /// <summary>
        /// Rozlišení světa
        /// </summary>
        public double Resolution { get { return r; } }

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
        public double CurrentTimeFrame
        {
            get { return simulationTime; }
        }

        /// <summary>
        /// Maximální vzdálenost tělesa od počátku
        /// </summary>
        public double MaximumRadius
        {
            get { return maxRad; }
            set
            {
                if (value < r * 30)
                  maxRad = r * 30;
                else maxRad = value;
            }
        }

        /// <summary>
        /// Orientace světa
        /// </summary>
        public Matrix Orientation
        {
            get { return b; }
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
        /// Disipační konstanta
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
        /// Všechna tělesa ve světě
        /// </summary>
        public SimObject[] Objects { get { return (SimObject[])Objs.ToArray(typeof(SimObject)); } }


        /// <summary>
        /// Počet těles ve světě
        /// </summary>
        public uint CountObjects
        {
            get { return (uint)Objs.Count; }
        }

        /// <summary>
        /// Počet polí ve světě
        /// </summary>
        public uint CountFields
        {
            get { return (uint)Fields.Count; }
        }

        /// <summary>
        /// Získá/změní těleso ve světě
        /// </summary>
        /// <param name="index">Index tělesa</param>
        /// <returns>Těleso</returns>
        public SimObject this[int index]
        {
            get { return (SimObject)Objs[index]; }
            set
            {
                lock (SimLock)
                {
                    Objs[index] = value;
                }
            }
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
        /// Událost nastávající při každém simulačním kroku
        /// </summary>
        public event EventHandler OnTick;    

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
        /// Najde první nejbližší těžiště objektu ve světě k dané pozici
        /// </summary>
        /// <param name="Position">Pozice</param>
        /// <returns>Nejbližší objekt</returns>
        public SimObject NearestObject(Vector Position)
        {
            double distance = Double.PositiveInfinity; 
            int index = -1;
            if (Objs.Count == 0)
                throw new InvalidOperationException();

            for (int i = 0; i < Objs.Count; i++)
            {
                double dist = (((SimObject)Objs[i]).Model.Position-Position).Magnitude;
                if (dist == 0) return Objs[i] as SimObject;

                if (dist < distance)
                {
                    distance = dist;
                    index = i;
                }
            }
            return Objs[index] as SimObject;
        }

        /// <summary>
        /// Provede simulační krok
        /// </summary>
        public void Tick()
        {
            double ms = DateTime.Now.Ticks / 10000;
            double Delta = simulationTime == 0 ? 0 : (ms - simulationTime) / 1000;

            lock (SimLock)
            {
               SimObject[] PhysObjs = (SimObject[])Objs.ToArray(typeof(SimObject));
               for (int i = 0; i < Objs.Count; i++)
               {
                   if (PhysObjs[i].Model.Position.Magnitude > maxRad)
                   {
                       PhysObjs[i].LinearVelocity = PhysObjs[i].AngularVelocity = 0;
                       PhysObjs[i].Enabled = false;
                   }
                   if (!PhysObjs[i].Enabled) continue;

                   foreach (Field f in ForceFields)
                   {
                       if (f.Enabled)
                         PhysObjs[i].ApplyForce(f.GetForce(f, PhysObjs[i]), PhysObjs[i].COG);
                   }
                   
                   PhysObjs[i].ApplyForce(PhysObjs[i].Mass * Gravity,PhysObjs[i].COG);

                   PhysObjs[i].Model.Position += PhysObjs[i].LinearVelocity * Delta;
                   PhysObjs[i].Model.Orientation += PhysObjs[i].AngularVelocity * Delta;

                   PhysObjs[i].LinearVelocity += PhysObjs[i].TotalForce * (Delta / PhysObjs[i].Mass);
                   PhysObjs[i].AngularVelocity += PhysObjs[i].TotalTorque * (Delta / PhysObjs[i].MomentOfInertia);

                   PhysObjs[i].Reset();                   
               }
            }
            simulationTime = ms;

            OnTick.DynamicInvoke(this, null);
        }
    }
}
