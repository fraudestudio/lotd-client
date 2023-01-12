using Assets.Scripts.ProceduralGeneration;
using Assets.Scripts.ProceduralGeneration.Algorithme;
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
    private GameObject mapSkinLoader;
    [SerializeField]
    private GameObject border;
    [SerializeField]
    private GameObject torchPreFab;
    [SerializeField]
    // The size of the room
    private int roomSize = 18;

    [SerializeField]
    private GameObject cameraManager;

    // the room bounds for the camera
    private Bounds roomCameraBounds;

    // the map of the room
    private Carte carte;
    public Carte Carte { get => carte; set => carte = value; }

    // all the tiles of the room
    private List<GameObject> tiles = new List<GameObject>();
    // all the tiles that have type "full"
    private List<GameObject> fullTiles = new List<GameObject>();
    // borders of the room 
    private List<GameObject> borders = new List<GameObject>();
    // torches of the room
    private List<GameObject> torches = new List<GameObject>();


    private void Awake()
    {
        AlgorithmeRoomGeneration.RoomSize = roomSize;
    }

    /// <summary>
    /// Generate the room with the given map
    /// </summary>
    /// <param name="carte">the map you want to generate</param>
    public void GenerateRoom(Carte carte)
    {
        this.carte = carte; 

        CreateBorders();

        ClearTiles();

        DestroyTorches();

        for (int ligne = 0; ligne < Carte.Taille; ligne++)
        {
            for (int colonne = 0; colonne < Carte.Taille; colonne++)
            {
                if (carte.Salles[ligne, colonne].Ligne == ligne && carte.Salles[ligne, colonne].Colonne == colonne)
                {
                    GameObject salle = GetObjectSalle(carte.Salles[ligne, colonne].Type);
                    salle.name = "Tile" + ligne + "_" + colonne + "_" + carte.Salles[ligne, colonne].Type.ToString();
                    salle.GetComponent<TilesScript>().Salle = carte.Salles[ligne, colonne];
                    salle.transform.position = new Vector2(colonne + GameManager.roomPosition, ligne + GameManager.roomPosition);
                    tiles.Add(salle);

                    if (carte.Salles[ligne, colonne].Type == TypeSalle.TILEFULL)
                    {
                        fullTiles.Add(salle);
                    }
                }
            }
        }


        roomCameraBounds = new Bounds(tiles[0].transform.position, Vector3.zero);

        foreach (GameObject tile in tiles)
        {
            if (!roomCameraBounds.Contains(tile.transform.position))
            {
                roomCameraBounds.Encapsulate(tile.transform.position);
            }
        }


        SetSkin();


        cameraManager.GetComponent<CameraManager>().RoomBounds = roomCameraBounds;
        cameraManager.GetComponent<CameraManager>().CenterOnRoom(1000);

    }

    /// <summary>
    /// Create a gameobject in function of the given room
    /// </summary>
    /// <param name="salle"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Destroy all the tiles
    /// </summary>
    private void ClearTiles()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            Destroy(tiles[i]);
        }
        tiles.Clear();
        fullTiles.Clear();
    }

    /// <summary>
    /// Set the skin on all the full tiles
    /// </summary>
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
            
            if (Larger + "_" + PositionLigne + "_" + PositionColonne == "BIG_MIDDLE_DOWN")
            {
                if (GenerateurAleatoire.Instance.Next(100) + 1 > 70)
                {
                    GameObject torch = Instantiate(torchPreFab);
                    torch.transform.position = new Vector3(currColonne + GameManager.roomPosition, currLigne + GameManager.roomPosition, -7);
                    torches.Add(torch);
                }
            }


            tile.GetComponent<SpriteRenderer>().sprite = mapSkinLoader.GetComponent<SkinRoomDictionnary>().GetSprite(Larger + "_" + PositionLigne + "_" + PositionColonne);


        }
    }

    /// <summary>
    /// Destroy all the torches
    /// </summary>
    private void DestroyTorches()
    {
        for (int i = 0; i < torches.Count; i++)
        {
            Destroy(torches[i]);
        }
        torches.Clear();
    }

    /// <summary>
    /// Create the border of the room
    /// </summary>
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
            borderObject.transform.position = new Vector3(GameManager.roomPosition + i, GameManager.roomPosition - 1);
            borderObject.GetComponent<SpriteRenderer>().sprite = mapSkinLoader.GetComponent<SkinRoomDictionnary>().GetSprite("BIG_MIDDLE_UP");
            borders.Add(borderObject);
            GameObject borderObject2 = Instantiate(border);
            borderObject2.transform.position = new Vector3(GameManager.roomPosition + i, GameManager.roomPosition + Carte.Taille);
            borderObject2.GetComponent<SpriteRenderer>().sprite = mapSkinLoader.GetComponent<SkinRoomDictionnary>().GetSprite("BIG_MIDDLE_DOWN");
            borders.Add(borderObject2);
        }
        for (int i = 0; i < Carte.Taille; i++)
        {
            GameObject borderObject = Instantiate(border);
            borderObject.transform.position = new Vector3(GameManager.roomPosition - 1, GameManager.roomPosition + i);
            borderObject.GetComponent<SpriteRenderer>().sprite = mapSkinLoader.GetComponent<SkinRoomDictionnary>().GetSprite("BIG_RIGHT_MIDDLE");
            borders.Add(borderObject);
            GameObject borderObject2 = Instantiate(border);
            borderObject2.transform.position = new Vector3(GameManager.roomPosition + Carte.Taille, GameManager.roomPosition + i);
            borderObject2.GetComponent<SpriteRenderer>().sprite = mapSkinLoader.GetComponent<SkinRoomDictionnary>().GetSprite("BIG_LEFT_MIDDLE");
            borders.Add(borderObject2);
        }

        GameObject borderObject3 = Instantiate(border);
        borderObject3.transform.position = new Vector3(GameManager.roomPosition - 1, GameManager.roomPosition - 1);
        borderObject3.GetComponent<SpriteRenderer>().sprite = mapSkinLoader.GetComponent<SkinRoomDictionnary>().GetSprite("BIG_CORNER_LEFT_DOWN");
        borders.Add(borderObject3);
        GameObject borderObject4 = Instantiate(border);
        borderObject4.transform.position = new Vector3(GameManager.roomPosition + Carte.Taille, GameManager.roomPosition - 1);
        borderObject4.GetComponent<SpriteRenderer>().sprite = mapSkinLoader.GetComponent<SkinRoomDictionnary>().GetSprite("BIG_CORNER_RIGHT_DOWN");
        borders.Add(borderObject4);
        GameObject borderObject5 = Instantiate(border);
        borderObject5.transform.position = new Vector3(GameManager.roomPosition - 1, GameManager.roomPosition + Carte.Taille);
        borderObject5.GetComponent<SpriteRenderer>().sprite = mapSkinLoader.GetComponent<SkinRoomDictionnary>().GetSprite("BIG_RIGHT_MIDDLE");
        borders.Add(borderObject5);
        GameObject borderObject6 = Instantiate(border);
        borderObject6.transform.position = new Vector3(GameManager.roomPosition + Carte.Taille, GameManager.roomPosition + Carte.Taille);
        borderObject6.GetComponent<SpriteRenderer>().sprite = mapSkinLoader.GetComponent<SkinRoomDictionnary>().GetSprite("BIG_LEFT_MIDDLE");
        borders.Add(borderObject6);
    }
}
