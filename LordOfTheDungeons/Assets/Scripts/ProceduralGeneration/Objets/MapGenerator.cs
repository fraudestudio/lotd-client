using Assets.Scripts.ProceduralGeneration;
using Assets.Scripts.ProceduralGeneration.Objets;
using Assets.Scripts.ProceduralGeneration.Salles;
using JetBrains.Annotations;
using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
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


    // Start is called before the first frame update
    void Start()
    {
    }

    public void GenerateMap(Carte carte)
    {
        this.carte = carte;

        Bounds mapBounds = new Bounds();

        for (int ligne = 0; ligne < Carte.Taille; ligne++)
        {
            for (int colonne = 0; colonne < Carte.Taille; colonne++)
            {
                if (carte.Salles[ligne, colonne].Ligne == ligne && carte.Salles[ligne, colonne].Colonne == colonne)
                {
                    GameObject salle = GetObjectSalle(carte.Salles[ligne, colonne].Type);


                    salle.transform.position = new Vector2(colonne, ligne);
                    salle.GetComponent<SalleObjectScript>().Salle = carte.Salles[ligne, colonne];
                    if (carte.Salles[ligne, colonne].Type == TypeSalle.START)
                    {
                        firstRoom = salle;
                        arrow.transform.position = new Vector3(colonne, ligne,-8);
                    }

                    if (carte.Salles[ligne, colonne].Type != TypeSalle.VIDE)
                    {
                        mapBounds.Encapsulate(salle.transform.position);
                    }

                }
            }
        }
        arrow.transform.SetAsLastSibling();
        camera.transform.position = new Vector3(mapBounds.center.x, mapBounds.center.y,-10);
        camera.orthographicSize = mapBounds.size.x / 1.2f;
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
}
