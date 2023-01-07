using Assets.Scripts.ProceduralGeneration.Salles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Unity.Burst;
using UnityEditor;
using UnityEngine.UIElements.Experimental;

namespace Assets.Scripts.ProceduralGeneration.Algorithme
{

    public class AlgorithmeRoomGeneration : IAlgorithmeGeneration
    {
        // Size of the room 
        private static int roomSize = 18;
        public static int RoomSize { get => roomSize; set => roomSize = value; }


        // List of the validate figure present on the graphe 
        private List<Coordonnees> valideFigure = new List<Coordonnees>();


        public Carte Generer(int seed)
        {
            // we modify the map length for the room
            Carte.Taille = roomSize;
            Graphe g = new Graphe();
            // we insert the local seed
            GenerateurAleatoire.Instance.SetSeedLocale(seed);


            // We set all the tiles to normal
            for (int ligne = 0; ligne < Carte.Taille; ligne++)
            {
                for (int colonne = 0; colonne < Carte.Taille; colonne++)
                {
                    g.GetSommet(ligne, colonne).TypeSalle = TypeSalle.TILENORMALE;
                }
            }

            // We create some squares
            for (int i = 0; i < GenerateurAleatoire.Instance.Next(6) + 6; i++)
            {

                // We take a random coordinates and length
                Coordonnees c = GenerateurAleatoire.Instance.NextCoordonnees();
                int taille = GenerateurAleatoire.Instance.Next(2) + 2;

                // We make sure that we can create the square
                // If we can't, we redo it until we can
                while(!CanCreateSquare(g,c.Colonne, c.Ligne, taille))
                {
                    c = GenerateurAleatoire.Instance.NextCoordonnees();
                    taille = GenerateurAleatoire.Instance.Next(2) + 2;
                }
                g = CreateSquare(g, c.Colonne, c.Ligne, taille);
            }

            // we return the map
            return g.ToCarte();
        }


        /// <summary>
        /// Verify if we can create the square with parameters that we want on the graph
        /// </summary>
        /// <param name="g">graphe that we want</param>
        /// <param name="colonne">column that is set</param>
        /// <param name="ligne">line that is set</param>
        /// <param name="taille">length of the square</param>
        /// <returns>Boolean of if the square can be created</returns>
        private bool CanCreateSquare(Graphe g, int colonne, int ligne, int taille)
        {

            bool result = true;


            // Line and column with the length need to be lower than the maximum map range
            if (ligne + taille > Carte.Taille)
            {
                result = false;
            }

            if (colonne + taille > Carte.Taille)
            {
                result = false;
            }

            // Line and column has to be superior or equal to 0
            if (ligne < 0)
            {
                result = false;
            }
            if (colonne < 0)
            {
                result = false;
            }

            // If it pass the first tested
            if (result)
            {

                // Foreach tiles of the square, we make sure that it is at least 2 blocks away of already 
                // created square
                for(int i = 0; i < taille; i++)
                {
                    for(int j = 0; j < taille; j++)
                    {
                        Coordonnees coordinate = new Coordonnees(ligne + i, colonne + j);
                        foreach(Coordonnees valideCoordinate in valideFigure)
                        {
                            if (coordinate.Distance(valideCoordinate) < 2)
                            {
                                result = false;
                            }
                        }
                    }
                }
            }
            // We send back the result
            return result;
        }

        /// <summary>
        /// Create a square with parameters that we want on the graph
        /// </summary>
        /// <param name="g"></param>
        /// <param name="colonne"></param>
        /// <param name="ligne"></param>
        /// <param name="taille"></param>
        /// <returns></returns>
        private Graphe CreateSquare(Graphe g, int colonne, int ligne, int taille)
        {
            Graphe graphe = g;

            // We create the square
            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    graphe.GetSommet(ligne + i, colonne + j).TypeSalle = TypeSalle.TILEFULL;
                    // We add it to the valide figure of the graph
                    valideFigure.Add(new Coordonnees(ligne + i, colonne + j));
                }
            }

            // we return the new graph
            return graphe;
        }
    }
}
