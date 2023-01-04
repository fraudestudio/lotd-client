using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace Assets.Scripts.ProceduralGeneration.Salles
{
    public class AlgorithmeEliminationSimple : IAlgorithmeGeneration
    {
        public Carte Generer(string seed)
        {
            Graphe g = new Graphe();
            GenerateurAleatoire.SetSeedGlobal(seed);

            List<Coordonnees> coordonnees = new List<Coordonnees>();


            while (coordonnees.Count < 3)
            {
                bool add = true;
                Coordonnees c = GenerateurAleatoire.NextCoordonnees();

                if (coordonnees.Count >= 1)
                {
                    for (int i = 0; i < coordonnees.Count; i++)
                    {
                        if (c.Ligne == coordonnees[i].Ligne && c.Colonne == coordonnees[i].Colonne)
                        {
                            add = false;
                        }

                        if (c.Distance(coordonnees[i]) < 6)
                        {
                            add = false;
                        }
                    }

                    if (add)
                    {
                        coordonnees.Add(c);
                    }
                }
                else
                {
                    coordonnees.Add(c);
                }

            }

            g.GetSommet(coordonnees[0].Ligne, coordonnees[0].Colonne).TypeSalle = TypeSalle.BOSS;
            g.GetSommet(coordonnees[1].Ligne, coordonnees[1].Colonne).TypeSalle = TypeSalle.START;


            for (int i = 0; i < 2; i++)
            {
                int startint = GenerateurAleatoire.Next(g.GetSommet(coordonnees[i].Ligne, coordonnees[i].Colonne).Voisins.Count);
                for (int j = 0; j < g.GetSommet(coordonnees[i].Ligne, coordonnees[i].Colonne).Voisins.Count; j++)
                {
                    if (j != startint)
                    {
                        bool canDel = true;

                        foreach (Sommet s in g.GetSommet(coordonnees[i].Ligne, coordonnees[i].Colonne).Voisins[j].Voisins)
                        {
                            if (s.TypeSalle == TypeSalle.VIDE)
                            {
                                canDel = false;
                            }
                        }

                        if (canDel)
                        {
                            g.GetSommet(coordonnees[i].Ligne, coordonnees[i].Colonne).Voisins[j].TypeSalle = TypeSalle.VIDE;
                        }
                    }
                }
            }


            List<Sommet> listeDesSommets = g.Parcours(g.GetSommet(coordonnees[2].Ligne, coordonnees[2].Colonne), null);
            for (int i = 0; i < 40; i++)
            {
                listeDesSommets = this.SupprimerSommet(g, listeDesSommets, g.GetSommet(coordonnees[2].Ligne, coordonnees[2].Colonne), g.GetSommet(coordonnees[0].Ligne, coordonnees[0].Colonne), g.GetSommet(coordonnees[1].Ligne, coordonnees[1].Colonne));
            }
            return g.ToCarte();
        }
        private List<Sommet> SupprimerSommet(Graphe graphe, List<Sommet> listeDesSommets, Sommet SalleDepart, Sommet SalleBoss, Sommet SalleItem)
        {
            Sommet sommetAEnlever = listeDesSommets[GenerateurAleatoire.Next(listeDesSommets.Count)];
            List<Sommet> resultat = listeDesSommets;
            if (sommetAEnlever != SalleDepart)
            {
                List<Sommet> newListeDesSommets = graphe.Parcours(SalleDepart, sommetAEnlever);

                if (newListeDesSommets.Contains(SalleBoss) && newListeDesSommets.Contains(SalleItem) && newListeDesSommets.Count > 15)
                {
                    resultat = newListeDesSommets;
                    for (int i = 0; i < Carte.Taille; i++)
                    {
                        for (int j = 0; j < Carte.Taille; j++)
                        {
                            if (!resultat.Contains(graphe.GetSommet(i, j)))
                            {
                                graphe.GetSommet(i, j).TypeSalle = TypeSalle.VIDE;
                            }
                        }
                    }
                }
            }
            return resultat;
        }
    }
}
