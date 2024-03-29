﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ProceduralGeneration
{
    public class Coordonnees
    {
        // Line of the coordinates
        private int ligne;

        public int Ligne { get => ligne; set => ligne = value; }

        /// Column of the coordinates
        private int colonne;
        public int Colonne { get => colonne; set => colonne = value; }


        public Coordonnees(int ligne, int colonne)
        {
            Ligne = ligne;
            Colonne = colonne;
        }

        public override bool Equals(object? obj)
        {
            return obj is Coordonnees coordonnees &&
                   ligne == coordonnees.ligne &&
                   colonne == coordonnees.colonne;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ligne, colonne);
        }

        /// <summary>
        /// Send back the distance with the specify coordinates
        /// </summary>
        /// <param name="cible"></param>
        /// <returns>the distance</returns>
        public int Distance(Coordonnees cible)
        {
            return Math.Abs(cible.Ligne - Ligne) + Math.Abs(cible.Colonne - Colonne);
        }

    }
}
