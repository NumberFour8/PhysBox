﻿using System;

namespace PhysLib
{

    /// <summary>
    /// Delegát zastupující silovou funkci pole
    /// </summary>
    /// <param name="F">Pole spojené s funkcí</param>
    /// <param name="Object">Těleso na které pole působí</param>
    /// <returns>Síla působící na těleso v poli</returns>
    public delegate Vector ForceFunction(Field F,SimObject Object);
    
    /// <summary>
    /// Reprezentuje fyzikální silové pole
    /// </summary>
    public struct Field
    {
        private ForceFunction F;
        private Vector center;
        private object[] iparams;
        private bool enabled;

        /// <summary>
        /// Vytvoří nové fyzikální pole sil
        /// </summary>
        /// <param name="Center">Souřadnice centra pole</param>
        /// <param name="FieldFunction">Funkce vracející sílu pole na dané těleso</param>
        /// <param name="FieldParams">Volitelné parametry pole</param>
        public Field(Vector Center, ForceFunction FieldFunction, params object[] FieldParams)
        {
            if (FieldFunction == null) throw new ArgumentNullException();

            F = FieldFunction;
            center = Center;
            iparams = FieldParams;

            enabled = true;
        }
        
        /// <summary>
        /// Centrum pole
        /// </summary>
        public Vector FieldCenter { get { return center; } set { center = value; } }
        
        /// <summary>
        /// Síla působící na těleso v poli
        /// </summary>
        public ForceFunction GetForce { get { return F; } }
        
        /// <summary>
        /// Indikuje, zda je pole zapnuté
        /// </summary>
        public bool Enabled { get { return enabled; } set { enabled = value; } }

        /// <summary>
        /// Libovolné parametry pole
        /// </summary>
        public object[] Parameters { get { return iparams; } set { iparams = value; } }
    }
}
