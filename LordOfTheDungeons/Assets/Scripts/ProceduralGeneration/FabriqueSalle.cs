using Assets.Scripts.ProceduralGeneration.Salles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ProceduralGeneration
{
    public class FabriqueSalle
    {
        /// <summary>
        /// Créé une nouvelle salle en fonction de son type
        /// </summary>
        /// <param name="type">Type de la salle désirée</param>
        /// <param name="ligne">Ligne où se trouve la salle désirée</param>
        /// <param name="colonne">Colonne où se trouve la salle désirée</param>
        /// <returns>Nouvelle salle</returns>
        public static Salle Creer(TypeSalle type, int ligne, int colonne)
        {
            Salle salle = null;
            switch (type)
            {
                case TypeSalle.NORMALE: salle = new SalleNormale(ligne, colonne); break;
                case TypeSalle.BOSS: salle = new SalleBoss(ligne, colonne); break;
                case TypeSalle.START: salle = new SalleStart(ligne, colonne); break;
                case TypeSalle.VIDE: salle = new SalleVide(ligne, colonne); break;
            }
            return salle;
        }
    }
}
