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

        public override void Generation()
        {
            //Génération fixe que l'on modifiera plus tard
            base.Generation();
            GenerateurAleatoire.SetSeedLocale(this);
            this.NbMonstre = GenerateurAleatoire.Next() % 5 + 1;
            this.NbItems = GenerateurAleatoire.Next() % 3;
        }

        public override TypeSalle Type => TypeSalle.NORMALE;
    }
}
