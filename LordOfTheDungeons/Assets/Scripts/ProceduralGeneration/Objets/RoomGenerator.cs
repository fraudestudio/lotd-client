using Assets.Scripts.ProceduralGeneration;
using Assets.Scripts.ProceduralGeneration.Objets;
using Assets.Scripts.ProceduralGeneration.Salles;
using Assets.Scripts.RoomDungeon;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class RoomGenerator : MonoBehaviour, IGeneratorAlgo
{
    [SerializeField]
    private GameObject tileNormale;
    [SerializeField]
    private GameObject tileFull;
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private GameObject mapSkinLoader;

    private List<GameObject> tiles = new List<GameObject>();
    private List<GameObject> fullTiles = new List<GameObject>();

    public void GenerateRoom(Carte carte)
    {


        for (int i = 0; i < tiles.Count - 1; i++)
        {
            Destroy(tiles[i]);
        }
        tiles.Clear();
        fullTiles.Clear();

        for (int ligne = 0; ligne < Carte.Taille; ligne++)
        {
            for (int colonne = 0; colonne < Carte.Taille; colonne++)
            {
                if (carte.Salles[ligne, colonne].Ligne == ligne && carte.Salles[ligne, colonne].Colonne == colonne)
                {
                    GameObject salle = GetObjectSalle(carte.Salles[ligne, colonne].Type);
                    salle.name = "Tile" + ligne + "_" + colonne;
                    salle.GetComponent<TilesScript>().Salle = carte.Salles[ligne, colonne];
                    salle.transform.position = new Vector2(colonne + 50, ligne + 50);
                    tiles.Add(salle);
                    if (carte.Salles[ligne, colonne].Type == TypeSalle.TILEFULL)
                    {
                        fullTiles.Add(salle);
                    }
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



        foreach (GameObject tile in fullTiles)
        {

            string Larger = "BIG";
            string PositionColonne = "MIDDLE";
            string PositionLigne = "MIDDLE";
            int currColonne = tile.GetComponent<TilesScript>().Salle.Colonne;
            int currLigne = tile.GetComponent<TilesScript>().Salle.Ligne;


            if (currLigne + 1 <= Carte.Taille - 1 && currLigne - 1 >= 0)
            {
                if (carte.Salles[currLigne + 1, currColonne].Type == TypeSalle.TILEFULL && carte.Salles[currLigne - 1, currColonne].Type != TypeSalle.TILEFULL)
                {
                    PositionColonne = "DOWN";
                }
                else if (carte.Salles[currLigne - 1, currColonne].Type == TypeSalle.TILEFULL && carte.Salles[currLigne + 1, currColonne].Type != TypeSalle.TILEFULL)
                {
                    PositionColonne = "UP";
                }
                else
                {
                    PositionColonne = "MIDDLE";
                }
            }



            if (currColonne + 1 <= Carte.Taille - 1 && currColonne - 1 >= 0)
            {
                if (carte.Salles[currLigne, currColonne + 1].Type == TypeSalle.TILEFULL && carte.Salles[currLigne, currColonne - 1].Type != TypeSalle.TILEFULL)
                {
                    PositionLigne = "LEFT";
                }
                else if (carte.Salles[currLigne, currColonne - 1].Type == TypeSalle.TILEFULL && carte.Salles[currLigne, currColonne + 1].Type != TypeSalle.TILEFULL)
                {
                    PositionLigne = "RIGHT";
                }
                else
                {
                    PositionLigne = "MIDDLE";
                }
            }





            tile.GetComponent<SpriteRenderer>().sprite = mapSkinLoader.GetComponent<SkinRoomDictionnary>().GetSprite(Larger + "_" + PositionLigne + "_" + PositionColonne);

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
