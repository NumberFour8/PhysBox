using System;
using System.Collections;
using System.Drawing;

namespace PhysLib
{
    public struct CollisionReport
    {
        public SimObject A,B;

    }

    public sealed class CollisionSolver
    {
        private World w;
        private ArrayList Solved;
        
        public CollisionSolver(World Reference)
        {
            w = Reference;
            Solved = new ArrayList();
        }

        /// <summary>
        /// Indikuje, zda je detekce kolizí zapnuta
        /// </summary>
        public bool Enabled
        {
            get; set;
        }

        /// <summary>
        /// Sestaví report o kolizích v momentálním stavu systému pro daný objekt
        /// </summary>
        /// <returns>Výčet reportů o kolizích</returns>
        public IEnumerable DetectCollisionsFor(int ObjectIndex)
        {
            for (int i = 0; i < w.CountObjects; i++)
            {
                if (i == ObjectIndex) continue;
                if (w[ObjectIndex].Model.CollidesWith(w[i].Model))
                    throw new ExecutionEngineException("Hop, kolize!");
            }
            yield break;
        }

        /// <summary>
        /// Vyřeší konkrétní kolizi
        /// </summary>
        /// <param name="Report">Report o kolizi</param>
        public void SolveCollision(CollisionReport Report)
        {
            
            throw new NotImplementedException();
        }


    }
}
