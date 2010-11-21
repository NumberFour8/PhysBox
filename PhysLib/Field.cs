using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhysLib
{

    public delegate Vector ForceFunction(Field F,SimObject Object);
    
    public struct Field
    {
        private ForceFunction F;

        public Field(Vector Center, ForceFunction FieldFunction, params double[] FieldParams)
        {
            if (FieldFunction == null) throw new ArgumentNullException();

            F = FieldFunction;
            FieldCenter = Center;
            Parameters = FieldParams;

            Enabled = true;
        }
        
        public Vector FieldCenter { get; set; }
        public ForceFunction GetForce { get { return F; } }
        public bool Enabled { get; set; }
        public double[] Parameters { get; set; }
    }
}
