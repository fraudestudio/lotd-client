using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ProceduralGeneration
{
    public abstract class Salle
    {
        //Numéro de ligne où se trouve la salle
        public int Ligne { get => ligne; set => ligne = value; }
        private int ligne;

        //Numéro de colonne où se trouve la salle
        public int Colonne { get => colonne; set => colonne = value; }
        private int colonne;

        protected Salle(int ligne, int colonne)
        {
            this.ligne = ligne;
            this.colonne = colonne;
        }

        public abstract TypeSalle Type { get; }

        public override bool Equals(object? obj)
        {
            return obj is Salle salle &&
                   ligne == salle.ligne &&
                   colonne == salle.colonne;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ligne, colonne);
        }


    }
}
