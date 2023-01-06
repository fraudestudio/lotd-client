using Assets.Scripts.ProceduralGeneration;
using Assets.Scripts.ProceduralGeneration.Objets;
using Assets.Scripts.ProceduralGeneration.Salles;
using Assets.Scripts.RoomDungeon;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor.Experimental.GraphView;
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
    [SerializeField]
    private GameObject border;


    private Carte carte;

    private List<GameObject> tiles = new List<GameObject>();
    private List<GameObject> fullTiles = new List<GameObject>();
    private List<GameObject> borders = new List<GameObject>();

    public void GenerateRoom(Carte carte)
    {

        this.carte = carte; 

        CreateBorders();

        ClearTiles();


        for (int ligne = 0; ligne < Carte.Taille; ligne++)
        {
            for (int colonne = 0; colonne < Carte.Taille; colonne++)
            {
                if (carte.Salles[ligne, colonne].Ligne == ligne && carte.Salles[ligne, colonne].Colonne == colonne)
                {
                    GameObject salle = GetObjectSalle(carte.Salles[ligne, colonne].Type);
                    salle.name = "Tile" + ligne + "_" + colonne + "_" + carte.Salles[ligne, colonne].Type.ToString();
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


        SetSkin();


        mainCamera.transform.position = new Vector3(mapBounds.center.x + 5, mapBounds.center.y, -10);
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

    private void ClearTiles()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            Destroy(tiles[i]);
        }
        tiles.Clear();
        fullTiles.Clear();
    }

    private void SetSkin()
    {
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
    }

    private void CreateBorders()
    {
        for (int i = 0; i < borders.Count; i++)
        {
            Destroy(borders[i]);
        }
        borders.Clear();

        for (int i = 0; i < Carte.Taille; i++)
        {
            GameObject borderObject = Instantiate(border);
            borderObject.transform.position = new Vector3(50 + i, 49);
            borderObject.GetComponent<SpriteRenderer>().sprite = mapSkinLoader.GetComponent<SkinRoomDictionnary>().GetSprite("BIG_MIDDLE_UP");
            borders.Add(borderObject);
            GameObject borderObject2 = Instantiate(border);
            borderObject2.transform.position = new Vector3(50 + i, 50 + Carte.Taille);
            borderObject2.GetComponent<SpriteRenderer>().sprite = mapSkinLoader.GetComponent<SkinRoomDictionnary>().GetSprite("BIG_MIDDLE_DOWN");
            borders.Add(borderObject2);
        }
        for (int i = 0; i < Carte.Taille; i++)
        {
            GameObject borderObject = Instantiate(border);
            borderObject.transform.position = new Vector3(49, 50 + i);
            borderObject.GetComponent<SpriteRenderer>().sprite = mapSkinLoader.GetComponent<SkinRoomDictionnary>().GetSprite("BIG_RIGHT_MIDDLE");
            borders.Add(borderObject);
            GameObject borderObject2 = Instantiate(border);
            borderObject2.transform.position = new Vector3(50 + Carte.Taille, 50 + i);
            borderObject2.GetComponent<SpriteRenderer>().sprite = mapSkinLoader.GetComponent<SkinRoomDictionnary>().GetSprite("BIG_LEFT_MIDDLE");
            borders.Add(borderObject2);
        }

        GameObject borderObject3 = Instantiate(border);
        borderObject3.transform.position = new Vector3(49, 49);
        borderObject3.GetComponent<SpriteRenderer>().sprite = mapSkinLoader.GetComponent<SkinRoomDictionnary>().GetSprite("BIG_CORNER_LEFT_DOWN");
        borders.Add(borderObject3);
        GameObject borderObject4 = Instantiate(border);
        borderObject4.transform.position = new Vector3(50 + Carte.Taille, 49);
        borderObject4.GetComponent<SpriteRenderer>().sprite = mapSkinLoader.GetComponent<SkinRoomDictionnary>().GetSprite("BIG_CORNER_RIGHT_DOWN");
        borders.Add(borderObject4);
        GameObject borderObject5 = Instantiate(border);
        borderObject5.transform.position = new Vector3(49, 50 + Carte.Taille);
        borderObject5.GetComponent<SpriteRenderer>().sprite = mapSkinLoader.GetComponent<SkinRoomDictionnary>().GetSprite("BIG_RIGHT_MIDDLE");
        borders.Add(borderObject5);
        GameObject borderObject6 = Instantiate(border);
        borderObject6.transform.position = new Vector3(50 + Carte.Taille, 50 + Carte.Taille);
        borderObject6.GetComponent<SpriteRenderer>().sprite = mapSkinLoader.GetComponent<SkinRoomDictionnary>().GetSprite("BIG_LEFT_MIDDLE");
        borders.Add(borderObject6);
    }
}
