using Assets.Scripts.ProceduralGeneration;
using Assets.Scripts.ProceduralGeneration.Objets;
using Assets.Scripts.ProceduralGeneration.Salles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour, IGeneratorAlgo
{
    [SerializeField]
    private GameObject tileNormale;
    [SerializeField]
    private GameObject tileFull;
    [SerializeField]
    private Camera mainCamera;


    private List<GameObject> tiles = new List<GameObject>();

    public void GenerateRoom(Carte carte)
    {


        for (int ligne = 0; ligne < Carte.Taille; ligne++)
        {
            for (int colonne = 0; colonne < Carte.Taille; colonne++)
            {
                if (carte.Salles[ligne, colonne].Ligne == ligne && carte.Salles[ligne, colonne].Colonne == colonne)
                {
                    GameObject salle = GetObjectSalle(carte.Salles[ligne, colonne].Type);
                    salle.transform.position = new Vector2(colonne + 50, ligne + 50);
                    tiles.Add(salle);
                }
            }
        }

        Bounds mapBounds = new Bounds(tiles[0].transform.position, Vector3.zero);

        foreach (GameObject tile in tiles)
        {
            if (!mapBounds.Contains(tile.transform.position))
            {
                mapBounds.Encapsulate(tile.transform.position);
            }
        }

        mainCamera.transform.position = new Vector3(mapBounds.center.x, mapBounds.center.y, -10);
        mainCamera.orthographicSize = mapBounds.size.x / 1.6f;
    }

    public GameObject GetObjectSalle(TypeSalle salle)
    {
        GameObject result;

        switch (salle)
        {
            case TypeSalle.TILEFULL: result = Instantiate(tileFull); break;
            default: result = Instantiate(tileNormale); break;
        }

        return result;
    }
}
