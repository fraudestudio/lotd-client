using Assets.Scripts.ProceduralGeneration.Salles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Unity.Burst;
using UnityEngine.UIElements.Experimental;

namespace Assets.Scripts.ProceduralGeneration.Algorithme
{
    public class AlgorithmeRoomGeneration : IAlgorithmeGeneration
    {
        public Carte Generer(int seed)
        {
            Carte.Taille = 15;
            Graphe g = new Graphe();
            GenerateurAleatoire.Instance.SetSeedLocale(seed);

            // We set all the tiles to normal
            for (int ligne = 0; ligne < Carte.Taille; ligne++)
            {
                for (int colonne = 0; colonne < Carte.Taille; colonne++)
                {
                    g.GetSommet(ligne, colonne).TypeSalle = TypeSalle.TILENORMALE;
                }
            }

            for (int i = 0; i < GenerateurAleatoire.Instance.Next(6) + 2; i++)
            {

                Coordonnees c = GenerateurAleatoire.Instance.NextCoordonnees();
                int taille = GenerateurAleatoire.Instance.Next(2) + 2;

                while(!CanCreateSquare(g,c.Colonne, c.Ligne, taille))
                {
                    c = GenerateurAleatoire.Instance.NextCoordonnees();
                    taille = GenerateurAleatoire.Instance.Next(2) + 2;
                }
                g = CreateSquare(g, c.Colonne, c.Ligne, taille);
            }

            return g.ToCarte();
        }



        private bool CanCreateSquare(Graphe g, int colonne, int ligne, int taille)
        {

            bool result = true;


            if (ligne + taille >= Carte.Taille - taille)
            {
                result = false;
            }

            if (colonne + taille >= Carte.Taille - taille)
            {
                result = false;
            }

            if (ligne < 0)
            {
                result = false;
            }
            if (colonne < 0)
            {
                result = false;
            }


            if (result)
            {
                if (ligne > 0 && colonne > 0)
                {
                    for (int i = -1; i < taille + 1; i++)
                    {
                        for (int j = -1; j < taille + 1; j++)
                        {
                            if (g.GetSommet(ligne + i, colonne + j).TypeSalle == TypeSalle.TILEFULL)
                            {
                                result = false;
                            }
                        }
                    }
                }

                if (ligne != 0)
                {
                    if (g.GetSommet(ligne -1, colonne).TypeSalle == TypeSalle.TILEFULL)
                    {
                        result = false;
                    }
                }
                if (colonne != 0)
                {
                    if (g.GetSommet(ligne, colonne - 1).TypeSalle == TypeSalle.TILEFULL)
                    {
                        result = false;
                    }
                }
            }

            return result;
        }

        private Graphe CreateSquare(Graphe g, int colonne, int ligne, int taille)
        {
            Graphe graphe = g;


            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    graphe.GetSommet(ligne + i, colonne + j).TypeSalle = TypeSalle.TILEFULL;
                }
            }

            
            return graphe;
        }
    }
}
