using Assets.Scripts.ProceduralGeneration;
using Assets.Scripts.ProceduralGeneration.Salles;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
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
    private Camera mainCamera;
    [SerializeField]
    private GameObject arrow;

    private Carte carte;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        GenerateMap(new AlgorithmeEliminationSimple().Generer(new System.Random().Next().ToString()));
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
                        arrow.transform.position = new Vector2(colonne, ligne);
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


    private GameObject GetObjectSalle(TypeSalle salle)
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
