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
    public sealed class World
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

        private ArrayList Objs, Fields;

        private Matrix b;
        private double simulationTime, maxRad, r,density;
        private Vector lv;

        private object SimLock = null;
        private bool paused;

        private CollisionSolver csolve;

        /// <summary>
        /// Vytvoří fyzikální svět
        /// </summary>
        /// <param name="WorldOrientation">Orientace os zobrazovacího zařízení</param>
        /// <param name="GravityAccel">Gravitační zrychlení (v jednotkách SI)</param>
        /// <param name="WorldConstraints">Poloměr světa v pixelech (maximální vzdálnost tělesa od počátku souřadnic)</param>
        /// <param name="WorldResolution">Počet pixelů který představuje jeden fyzický metr</param>
        public World(Matrix WorldOrientation, Vector GravityAccel, double WorldDiameter, double WorldResolution = 30)
        {
            Fields = new ArrayList();
            Objs = new ArrayList();
            lv = new Vector(0, WorldDiameter, 0);
            SimLock = new object();
            csolve = new CollisionSolver(this);

            b = WorldOrientation;
            maxRad = WorldDiameter;

            Gravity = Vector.ToBasis(b, GravityAccel) * WorldResolution;
            simulationTime = 0;
            r = WorldResolution;
            Delta = 0.01;
            paused = false;
            DeleteOutOfBounds = false;
        }

        /// <summary>
        /// Provede převod z metrů na pixely nebo z pixelů na metry podle daného rozlišení světa
        /// </summary>
        /// <param name="Input">Vstupní číslo</param>
        /// <param name="Type">Typ převodu</param>
        /// <returns>Převedené číslo</returns>
        public double Convert(double Input, ConversionType Type)
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
        public double Resolution { get { return r; } set { r = value; } }

        /// <summary>
        /// Třída pro vyhodnocení kolizí
        /// </summary>
        public CollisionSolver Solver
        {
            get { return csolve; }
        }

        /// <summary>
        /// Vektor gravitace
        /// </summary>
        public Vector Gravity
        {
            get;
            set;
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
        /// Hustota prostředí (v jednotkách SI)
        /// </summary>
        public double Aether
        {
            get { return density; }
            set { density = value/Math.Pow(Resolution, 3); }
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
        /// Smaže všechna tělesa
        /// </summary>
        public void ClearObjects()
        {
            lock (SimLock)
            {
                Objs.Clear();
            }
        }

        /// <summary>
        /// Smaže těleso s daným indexem
        /// </summary>
        /// <param name="Index">Index tělesa</param>
        public void DeleteObject(int Index)
        {
            if (Index < 0 || Index >= Objs.Count) throw new IndexOutOfRangeException();
            lock (SimLock)
            {
                Objs.RemoveAt(Index);
            }
        }

        /// <summary>
        /// Smaže silové pole s daným indexem
        /// </summary>
        /// <param name="Index">Index pole</param>
        public void DeleteField(int Index)
        {
            if (Index < 0 || Index >= Fields.Count) throw new IndexOutOfRangeException();
            lock (SimLock)
            {
                Fields.RemoveAt(Index);
            }
        }

        /// <summary>
        /// Smaže všechna silová pole
        /// </summary>
        public void ClearFields()
        {
            lock (SimLock)
            {
                Fields.Clear();
            }
        }

        /// <summary>
        /// Indikuje, zda je simulace pozastavena
        /// </summary>
        public bool Paused
        {
            get { return paused; }
            set { paused = value; }
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
                O.InitialEnergy = O.Mass * Gravity.Magnitude * Vector.PointDistance(new Vector(O.COG[0], Level[1], 0), O.COG);
                Objs.Add(O);
            }
            return Objs.Count - 1;
        }

        /// <summary>
        /// Najde první nejbližší těžiště objektu ve světě k dané pozici
        /// </summary>
        /// <param name="Position">Pozice</param>
        /// <param name="SkipStatic">Indikuje, zda přeskočit statické objekty</param>
        /// <returns>Nejbližší objekt</returns>
        public SimObject NearestObject(Vector Position,bool SkipStatic = true)
        {
            double distance = Double.PositiveInfinity;
            int index = -1;
            if (Objs.Count == 0)
                throw new InvalidOperationException();

            for (int i = 0; i < Objs.Count; i++)
            {
                if (((SimObject)Objs[i]).Static && SkipStatic) continue;
                double dist = (((SimObject)Objs[i]).Model.Position - Position).Magnitude;
                if (dist == 0) return Objs[i] as SimObject;

                if (dist < distance)
                {
                    distance = dist;
                    index = i;
                }
            }
            if (index < 0) return null;
            return Objs[index] as SimObject;
        }

        /// <summary>
        /// Velikost kroku simulace (v sekundách)
        /// </summary>
        public double Delta
        {
            get;
            set;
        }

        /// <summary>
        /// Indikuje, zda objekty mimo simulační okno budou smazány místo vypnutí
        /// </summary>
        public bool DeleteOutOfBounds
        {
            get;
            set;
        }

        /// <summary>
        /// Provede simulační krok
        /// </summary>
        public void Tick()
        {
            if (!paused)
            {
                lock (SimLock)
                {
                    SimObject[] PhysObjs = (SimObject[])Objs.ToArray(typeof(SimObject));
                    for (int i = 0; i < Objs.Count; i++)
                    {
                        if (PhysObjs[i].Model.Position.Magnitude > maxRad && PhysObjs[i].Enabled)
                        {
                            PhysObjs[i].LinearVelocity = PhysObjs[i].AngularVelocity = Vector.Zero;
                            PhysObjs[i].Enabled = false;
                            if (DeleteOutOfBounds) Objs.RemoveAt(i);
                        }
                        if (!PhysObjs[i].Enabled) continue;

                        foreach (Field f in ForceFields)
                        {
                            if (f.Enabled)
                                PhysObjs[i].ApplyForce(f.GetForce(f, PhysObjs[i]), PhysObjs[i].COG);
                        }

                        PhysObjs[i].ApplyForce(PhysObjs[i].Mass * Gravity, PhysObjs[i].COG);
                        PhysObjs[i].ApplyDrag(Aether);
                        
                        if (Aether != 0)
                          PhysObjs[i].TotalTorque -= Vector.Round(20 * PhysObjs[i].AngularVelocity,2);
                        
                        if (!PhysObjs[i].NoTranslations)
                        {
                            PhysObjs[i].Model.Position += PhysObjs[i].LinearVelocity * Delta;
                            PhysObjs[i].LinearVelocity += PhysObjs[i].TotalForce * (Delta / PhysObjs[i].Mass);
                        }

                        if (PhysObjs[i].LinearVelocity.IsNaN)
                            throw new DataMisalignedException("Tuhle vtipnou vyjímku musím chytit, abych zjistil kdy tenhle divnej stav nastává");

                        PhysObjs[i].Model.Orientation += Math.Round((PhysObjs[i].AngularVelocity[2] * 180 / Math.PI) * Delta, 3);
                        PhysObjs[i].AngularVelocity   += PhysObjs[i].TotalTorque * (Delta / PhysObjs[i].MomentOfInertia);

                        PhysObjs[i].Reset();

                        foreach (CollisionReport rep in csolve.DetectCollisionsFor(i))
                            csolve.SolveCollision(rep);

                        if (PhysObjs[i].Static) PhysObjs[i].ResetAll();
                    }
                }
            }

            OnTick(this, null);
        }

        /// <summary>
        /// Poloha bodu, podle kterého se určuje hladina potenciální energie
        /// </summary>
        public Vector Level
        {
            get { return lv; }
            set
            {
                for (int i = 0; i < Objs.Count; i++)
                    (Objs[i] as SimObject).InitialEnergy = value[1] - lv[1];
                lv[0] = 0; lv[1] = value[1]; lv[2] = 0;
            }
        }

        /// <summary>
        /// Spočítá všechny typy energií pro dané těleso
        /// </summary>
        /// <param name="Object">Těleso</param>
        /// <param name="Units">Jednotky</param>
        /// <returns>Energie</returns>
        public ObjectEnergy GetObjectEnergy(SimObject Object,ConversionType Units = ConversionType.MetersToPixels)
        {
            if (Object == null) throw new ArgumentNullException();

            ObjectEnergy Ret = new ObjectEnergy();

            if (Units == ConversionType.MetersToPixels)
            {
                Ret.Kinetic = 0.5 * Object.Mass * Vector.Pow(Object.LinearVelocity, 2);
                Ret.Potential = Object.Mass * Vector.PointDistance(new Vector(Object.COG[0], Level[1], 0), Object.COG) * Gravity.Magnitude;
            }
            else
            {
                Ret.Kinetic = 0.5 * Object.Mass * Vector.Pow(Convert(Object.LinearVelocity,ConversionType.PixelsToMeters), 2);
                Ret.Potential = Object.Mass * Convert(Vector.PointDistance(new Vector(Object.COG[0], Level[1], 0), Object.COG),ConversionType.PixelsToMeters) * Convert(Gravity,ConversionType.PixelsToMeters).Magnitude;
            }
            
            Ret.Rotational = 0.5 * Object.Mass * Object.MomentOfInertia * Vector.Pow(Object.AngularVelocity, 2);
            return Ret;
        }

    }

    /// <summary>
    /// Struktura obsahující informace o energiích tělesa
    /// </summary>
    public struct ObjectEnergy
    {
        public double Kinetic, Potential, Rotational;
        public double Total
        {
            get { return Kinetic + Potential + Rotational; }
        }
    }
}
