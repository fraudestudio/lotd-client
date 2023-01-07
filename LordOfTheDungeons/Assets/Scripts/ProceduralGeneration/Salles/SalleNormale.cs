using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ProceduralGeneration.Salles
{
    /// <summary>
    /// Salle normale
    /// </summary>
    public class SalleNormale : Salle
    {
        public SalleNormale(int ligne, int colonne) : base(ligne, colonne)
        {
        }

        public override TypeSalle Type => TypeSalle.NORMALE;
    }
}
