using Assets.Scripts.ProceduralGeneration;
using Assets.Scripts.ProceduralGeneration.Objets;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour, IGeneratorAlgo
{
    [SerializeField]
    private GameObject salleBoss;
    [SerializeField]
    private GameObject salleNormale;
    [SerializeField]
    private GameObject salleVide;
    [SerializeField]
    private GameObject salleStart;
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private GameObject arrow;

    private Carte carte;

    private GameObject firstRoom;

    public GameObject FirstRoom { get => firstRoom; }


    private GameObject[,] salles;

    public void GenerateMap(Carte carte)
    {
        Carte.Taille = 6;
        salles = new GameObject[Carte.Taille, Carte.Taille];
        this.carte = carte;

        Bounds mapBounds = new Bounds();

        for (int ligne = 0; ligne < Carte.Taille; ligne++)
        {
            for (int colonne = 0; colonne < Carte.Taille; colonne++)
            {
                if (carte.Salles[ligne, colonne].Ligne == ligne && carte.Salles[ligne, colonne].Colonne == colonne)
                {
                    GameObject salle = GetObjectSalle(carte.Salles[ligne, colonne].Type);

                    salle.name = "Salle_" + ligne + "_" + colonne + "_" + carte.Salles[ligne, colonne].Type.ToString();

                    salle.transform.position = new Vector2(colonne, ligne);
                    
                    salle.GetComponent<SalleObjectScript>().Salle = carte.Salles[ligne, colonne];
                    salle.GetComponent<SalleObjectScript>().Seed = int.Parse(ligne.ToString() + colonne.ToString());

                    salles[ligne,colonne] = salle;

                    if (carte.Salles[ligne, colonne].Type == TypeSalle.START)
                    {
                        firstRoom = salle;
                        arrow.transform.position = new Vector3(colonne, ligne, -8);
                    }

                    if (carte.Salles[ligne, colonne].Type != TypeSalle.VIDE)
                    {
                        mapBounds.Encapsulate(salle.transform.position);
                    }

                }
            }
        }

        SetVoisinsInObjects();

        arrow.transform.SetAsLastSibling();
        camera.transform.position = new Vector3(mapBounds.center.x, mapBounds.center.y,-10);
        camera.orthographicSize = mapBounds.size.x / 1.2f;
    }


    private void SetVoisinsInObjects()
    {
        for (int ligne = 0; ligne < Carte.Taille; ligne++)
        {
            for (int colonne = 0; colonne < Carte.Taille; colonne++)
            {
                GameObject salle = salles[ligne,colonne];


                if (ligne + 1 < Carte.Taille)
                {
                    if (carte.Salles[ligne + 1, colonne].Type != TypeSalle.VIDE)
                    {
                        salle.GetComponent<SalleObjectScript>().Top = salles[ligne + 1, colonne];
                    }
                }

                if (ligne - 1 >= 0)
                {
                    if (carte.Salles[ligne - 1, colonne].Type != TypeSalle.VIDE)
                    {
                        salle.GetComponent<SalleObjectScript>().Bottom = salles[ligne - 1, colonne];
                    }
                }

                if (colonne + 1 < Carte.Taille)
                {
                    if (carte.Salles[ligne, colonne + 1].Type != TypeSalle.VIDE)
                    {
                        salle.GetComponent<SalleObjectScript>().Right = salles[ligne, colonne + 1];
                    }
                }

                if (colonne - 1 >= 0)
                {
                    if (carte.Salles[ligne, colonne - 1].Type != TypeSalle.VIDE)
                    {
                        salle.GetComponent<SalleObjectScript>().Left = salles[ligne, colonne - 1];
                    }
                }

            }
        }
    }


    public GameObject GetObjectSalle(TypeSalle salle)
    {
        GameObject result;

        switch (salle)
        {
            case TypeSalle.NORMALE: result = Instantiate(salleNormale); break;
            case TypeSalle.START: result = Instantiate(salleStart); break;
            case TypeSalle.BOSS: result = Instantiate(salleBoss); break;
            default: result = Instantiate(salleVide); break;
        }

        return result;
    }

    public void SetArrowPosition(int ligne, int colonne)
    {
        arrow.transform.position = new Vector3(colonne, ligne, -8);
    }
}
