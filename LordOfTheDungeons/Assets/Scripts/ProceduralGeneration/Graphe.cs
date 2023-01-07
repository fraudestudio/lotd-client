using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ProceduralGeneration.Salles
{
    public class Graphe
    {
        private Dictionary<Coordonnees, Sommet> sommets = new Dictionary<Coordonnees, Sommet>();
        
        /// <summary>
        /// Generate all the vertex in function of size
        /// </summary>
        public Graphe()
        {
            for (int x = 0; x < Carte.Taille; x++)
            {
                for (int y = 0; y < Carte.Taille; y++)
                {
                    Sommet s = new Sommet();
                    sommets.Add(new Coordonnees(x, y), s);
                }
            }


            for (int x = 0; x < Carte.Taille - 1; x++)
            {
                for (int y = 0; y < Carte.Taille; y++)
                {
                    sommets[new Coordonnees(x, y)].AjouterVoisin(sommets[new Coordonnees(x + 1, y)]);
                    sommets[new Coordonnees(x + 1, y)].AjouterVoisin(sommets[new Coordonnees(x, y)]);
                }
            }

            for (int x = 0; x < Carte.Taille; x++)
            {
                for (int y = 0; y < Carte.Taille - 1; y++)
                {
                    sommets[new Coordonnees(x, y)].AjouterVoisin(sommets[new Coordonnees(x, y + 1)]);
                    sommets[new Coordonnees(x, y + 1)].AjouterVoisin(sommets[new Coordonnees(x, y)]);
                }
            }
        }


        /// <summary>
        /// Send back the sommet that match the line and column
        /// </summary>
        /// <param name="ligne"></param>
        /// <param name="colonne"></param>
        /// <returns>Send back the sommet</returns>
        public Sommet GetSommet(int ligne, int colonne)
        {
            Sommet sommet = null;

            if (sommets.ContainsKey(new Coordonnees(ligne, colonne)))
            {
                sommet = sommets[new Coordonnees(ligne, colonne)];
            }

            return sommet;
        }

        /// <summary>
        /// Convert the graph in map
        /// </summary>
        /// <returns>The generated map</returns>
        public Carte ToCarte()
        {
            Carte carte = new Carte();

            foreach (Coordonnees c in sommets.Keys)
            {
                carte.AjouterSalle(c.Ligne, c.Colonne, GetSommet(c.Ligne, c.Colonne).TypeSalle);
            }
            return carte;
        }

        /// <summary>
        /// Pathwidth of the startpoint to the arrival
        /// </summary>
        /// <param name="depart"></param>
        /// <param name="aEnlever"></param>
        /// <returns>Renvoie le parcours</returns>
        public List<Sommet> Parcours(Sommet depart, Sommet aEnlever)
        {
            List<Sommet> resultat = new List<Sommet>();
            List<Sommet> aTraiter = new List<Sommet>();
            Dictionary<Sommet, int> distances = new Dictionary<Sommet, int>();

            foreach (Sommet s in sommets.Values)
            {
                distances.Add(s, -1);
            }

            distances[depart] = 0;
            aTraiter.Add(depart);

            while (aTraiter.Count > 0)
            {
                Sommet sommetEnCours = aTraiter[0];
                aTraiter.RemoveAt(0);

                foreach (Sommet v in sommetEnCours.Voisins)
                {
                    if (distances[v] == -1 && v != aEnlever && v.TypeSalle != TypeSalle.VIDE)
                    {
                        distances[v] = distances[sommetEnCours] + 1;
                        aTraiter.Add(v);
                    }
                }
            }

            foreach (Sommet s in distances.Keys)
            {
                if (distances[s] != -1)
                {
                    resultat.Add(s);
                }
            }

            return resultat;
        }
    }
}
