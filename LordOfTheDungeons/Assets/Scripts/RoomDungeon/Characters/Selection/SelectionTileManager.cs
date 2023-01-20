using Assets.Scripts.ProceduralGeneration;
using Assets.Scripts.RoomDungeon.Characters.Selection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Runtime.CompilerServices;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

public class SelectionTileManager : MonoBehaviour
{

    [SerializeField]
    private GameObject characterManager;

    [SerializeField]
    private GameObject selectionTilePreFab;

    [SerializeField]
    private GameObject playerActionManager;


    // the current path that has been selected
    private List<GameObject> currentPath = new List<GameObject>();


    // All the current selection tiles
    private List<GameObject> selectionTiles = new List<GameObject>();


    // Dictionnary that contains all the distances to calcul the path
    private Dictionary<GameObject, int> distances = new Dictionary<GameObject, int>();

    // Map of the room
    private Carte carte;
    public Carte Carte { get => carte; set => carte = value; }

    /// <summary>
    /// We calculate the path with the selected tile and turn them all to red
    /// </summary>
    /// <param name="arrivalSelectionTile"></param>
    public void CreateSelectionTrail(GameObject arrivalSelectionTile)
    {
        Debug.Log(arrivalSelectionTile);
        CalculDistanceSelection(arrivalSelectionTile);
        List<GameObject> result = CalculCheminSelection(selectionTiles[0]);
        currentPath = result;
        TurnListSelectionTileRed(result);
    }

    /// <summary>
    /// Delete all the information about the current trail
    /// </summary>
    public void ResetTrail()
    {
        currentPath = null;
        TurnAllSelectionTileWhite();
    }


    /// <summary>
    /// Turn a list of selection tile to red
    /// </summary>
    /// <param name="listSelectionTile"></param>
    private void TurnListSelectionTileRed(List<GameObject> listSelectionTile)
    {
        foreach (GameObject tile in listSelectionTile)
        {
            tile.GetComponent<SelectionTileScript>().TurnRed();
        }
    }

    /// <summary>
    /// Turn all the selection tiles to white
    /// </summary>
    private void TurnAllSelectionTileWhite()
    {
        foreach (GameObject tile in selectionTiles)
        {
            tile.GetComponent<SelectionTileScript>().TurnWhite();
        }
    }

    /// <summary>
    /// Turn all the selection tiles to white
    /// </summary>
    public void HideAllSelectionTile()
    {
        foreach (GameObject tile in selectionTiles)
        {
            tile.GetComponent<SelectionTileScript>().Hide();
        }
    }



    /// <summary>
    /// Create the tiles that allows the playable to do actions 
    /// </summary>
    /// <param name="movement"></param>
    /// <param name="playable"></param>
    public void CreateSelectionTiles(int actionCount, TypeSelection type, GameObject playable)
    {
        DeleteSelectionTiles();

        if (type == TypeSelection.Deplacement)
        {
            SetCurrentPlayabe(playable);
        }


        int colonne = Convert.ToInt32(characterManager.GetComponent<CharacterManager>().CurrentSelectedPlayable.transform.position.x) - GameManager.roomPosition;
        int ligne = Convert.ToInt32(characterManager.GetComponent<CharacterManager>().CurrentSelectedPlayable.transform.position.y) - GameManager.roomPosition;
        int movement = actionCount;


        // We create all the tiles needed
        GenerateCreationTile(movement, ligne, colonne,type);


        // On récupére les voisins
        DefineSelectionTileVoisins();





        // On supprime les chemins qui ne sont pas accessible ou qui ont plus de mouvement que movement 

        List<GameObject> tileToDestroy = new List<GameObject>();


        GameObject startTile = selectionTiles[0];


        // We destroy all the tiles that are too far from the start
        foreach (GameObject Arrivee in selectionTiles)
        {

            // We calcul the distance
            CalculDistanceSelection(Arrivee);

            if (Arrivee.GetComponent<SelectionTileScript>().CanUse)
            {
                // We calcul the path 
                List<GameObject> chemin = CalculCheminSelection(startTile);

                // If it is superior to the movement, we delete it
                if (chemin.Count > movement)
                {
                    tileToDestroy.Add(Arrivee);
                }

                if (chemin.Count == 0)
                {
                    tileToDestroy.Add(Arrivee);
                }
            }
        }

        // If the center is in the remove list, we delete it from it 
        if (tileToDestroy.Contains(selectionTiles[0]))
        {
            tileToDestroy.Remove(selectionTiles[0]);
        }

        // Destruction des tiles inutiles
        for (int i = 0; i < tileToDestroy.Count; i++)
        {
            foreach (GameObject t in selectionTiles)
            {
                t.GetComponent<SelectionTileScript>().RemoveVoisins(tileToDestroy[i]);
            }
            Destroy(tileToDestroy[i]);
            selectionTiles.Remove(tileToDestroy[i]);
        }
        tileToDestroy.Clear();


        // We set the tile to the asked type
        SetType(selectionTiles, type);
    }


    /// <summary>
    /// Define the neighbor of all selection tiles
    /// </summary>
    private void DefineSelectionTileVoisins()
    {
        foreach (GameObject currentTile in selectionTiles)
        {
            foreach (GameObject tile in selectionTiles)
            {
                if (tile.GetComponent<SelectionTileScript>().Colonne == currentTile.GetComponent<SelectionTileScript>().Colonne + 1 &&
                    tile.GetComponent<SelectionTileScript>().Ligne == currentTile.GetComponent<SelectionTileScript>().Ligne ||
                    tile.GetComponent<SelectionTileScript>().Ligne == currentTile.GetComponent<SelectionTileScript>().Ligne + 1 &&
                    tile.GetComponent<SelectionTileScript>().Colonne == currentTile.GetComponent<SelectionTileScript>().Colonne ||
                    tile.GetComponent<SelectionTileScript>().Colonne == currentTile.GetComponent<SelectionTileScript>().Colonne - 1 &&
                    tile.GetComponent<SelectionTileScript>().Ligne == currentTile.GetComponent<SelectionTileScript>().Ligne ||
                    tile.GetComponent<SelectionTileScript>().Ligne == currentTile.GetComponent<SelectionTileScript>().Ligne - 1 &&
                    tile.GetComponent<SelectionTileScript>().Colonne == currentTile.GetComponent<SelectionTileScript>().Colonne)
                {
                    currentTile.GetComponent<SelectionTileScript>().AddVoisins(tile);
                }
            }
        }
    }

    private void GenerateCreationTile(int movement, int ligne, int colonne, TypeSelection type)
    {

        // We create the point 0
        GameObject selectionTile = CreateSelectionTile(ligne, colonne);
        selectionTile.GetComponent<SelectionTileScript>().CanUse = false;

        for (int i = 0; i < movement; i++)
        {
            CreateTileCondition(ligne + i + 1, colonne , type);
        }


        for (int i = 0; i < movement; i++)
        {
            CreateTileCondition(ligne - i - 1, colonne, type);
        }


        for (int i = 0; i < movement; i++)
        {
            //Horizontal Haut
            for (int j = 0; j < movement - i; j++)
            {
                CreateTileCondition(ligne + i, colonne + j + 1, type);
            }

            for (int j = 0; j < movement - i; j++)
            {
                CreateTileCondition(ligne + i ,colonne - j - 1, type);
            }

            // Horizontal Bas
            for (int j = 0; j < movement - i - 1; j++)
            {
                CreateTileCondition(ligne - i - 1,colonne + j + 1, type);
            }

            for (int j = 0; j < movement - i - 1; j++)
            {
                CreateTileCondition(ligne - i - 1, colonne - j - 1, type);
            }
        }
    }

    /// <summary>
    /// Create the tile with the given line and column condition for the type given
    /// </summary>
    /// <param name="ligneCondition">the line condition</param>
    /// <param name="colonneCondition">the column condition</param>
    /// <param name="type">the type we want to create</param>
    private void CreateTileCondition(int ligneCondition, int colonneCondition, TypeSelection type)
    {
        switch (type)
        {
            case TypeSelection.Deplacement:
                {
                    if (colonneCondition <= Carte.Taille - 1 && colonneCondition >= 0 && ligneCondition <= Carte.Taille - 1 && ligneCondition >= 0)
                    {
                        if (carte.Salles[ligneCondition, colonneCondition].Type != TypeSalle.TILEFULL && !carte.Salles[ligneCondition, colonneCondition].HasPlayer)
                        {
                            CreateSelectionTile(ligneCondition, colonneCondition);
                        }
                    }
                }
                break;
            case TypeSelection.Attack:
                {
                    if (colonneCondition <= Carte.Taille - 1 && colonneCondition >= 0 && ligneCondition <= Carte.Taille - 1 && ligneCondition >= 0)
                    {
                        if (carte.Salles[ligneCondition, colonneCondition].Type != TypeSalle.TILEFULL)
                        {
                            CreateSelectionTile(ligneCondition, colonneCondition);
                        }
                    }
                }
                break;
        }
    }


    /// <summary>
    /// Calcul a distance from a selection tile
    /// </summary>
    /// <param name="tile"></param>
    private void CalculDistanceSelection(GameObject tile)
    {
        List<GameObject> aTraiter = new List<GameObject>();
        ResetDistance();
        aTraiter.Add(tile);
        SetDistance(tile, 0);

        while (aTraiter.Count > 0)
        {
            GameObject tileEnCours = aTraiter[0];
            aTraiter.RemoveAt(0);

            foreach (GameObject t in tileEnCours.GetComponent<SelectionTileScript>().Voisins)
            {
                if (GetDistance(t) == -1)
                {
                    SetDistance(t, GetDistance(tileEnCours) + 1);
                    aTraiter.Add(t);
                }
            }
        }
    }

    /// <summary>
    /// Calculate the path for the selection tiles
    /// </summary>
    /// <param name="StartTile"></param>
    /// <returns></returns>
    private List<GameObject> CalculCheminSelection(GameObject StartTile)
    {
        GameObject tileProcessing = StartTile;

        List<GameObject> resultat = new List<GameObject>();

        if (GetDistance(tileProcessing) != 0)
        {
            while (GetDistance(tileProcessing) > 0)
            {
                GameObject PreviousTile = null;
                foreach (GameObject t in tileProcessing.GetComponent<SelectionTileScript>().Voisins)
                {
                    if (GetDistance(t) == GetDistance(tileProcessing) - 1)
                    {
                        PreviousTile = t;
                    }
                }
                resultat.Add(PreviousTile);
                tileProcessing = PreviousTile;
            }
        }

        return resultat;
    }

    /// <summary>
    /// Set the distance in the distance dicitonnary 
    /// </summary>
    /// <param name="Tile"></param>
    /// <param name="distance"></param>
    private void SetDistance(GameObject Tile, int distance)
    {
        distances.Add(Tile, distance);
    }

    /// <summary>
    /// Get the distance of a tile
    /// </summary>
    /// <param name="Tile"></param>
    /// <returns></returns>
    private int GetDistance(GameObject Tile)
    {
        if (!distances.ContainsKey(Tile))
        {
            return -1;
        }
        else
        {
            return distances[Tile];
        }
    }

    /// <summary>
    /// Clear the distance dictionnary
    /// </summary>
    private void ResetDistance()
    {
        distances.Clear();
    }

    /// <summary>
    /// Delete all the selection tiles
    /// </summary>
    public void DeleteSelectionTiles()
    {
        ResetTrail();
        for (int i = 0; i < selectionTiles.Count; i++)
        {
            Destroy(selectionTiles[i]);
        }
        selectionTiles.Clear();
    }

    /// <summary>
    /// Create a selection tile gameobject
    /// </summary>
    /// <param name="colonne"></param>
    /// <param name="ligne"></param>
    /// <returns></returns>
    private GameObject CreateSelectionTile(int colonne, int ligne)
    {
        GameObject selectionTile = Instantiate(selectionTilePreFab);
        selectionTile.transform.position = new Vector3(ligne + GameManager.roomPosition, colonne + GameManager.roomPosition, -2);
        selectionTile.name = "SelectionTile " + ligne + "_" + colonne;
        selectionTile.GetComponent<SelectionTileScript>().Ligne = ligne;
        selectionTile.GetComponent<SelectionTileScript>().Colonne = colonne;
        selectionTiles.Add(selectionTile);
        return selectionTile;
    }

    /// <summary>
    /// If the player pressed left mouse button, 
    /// we reset and delete all tiles
    /// </summary>
    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            SetCurrentPlayabe(null);
            DeleteSelectionTiles();
            playerActionManager.GetComponent<PlayerActionManager>().ShowAttackATH(false);
        }
    }

    /// <summary>
    /// Set the current playable for the character manager
    /// </summary>
    /// <param name="playable"></param>
    private void SetCurrentPlayabe(GameObject playable)
    {
        characterManager.GetComponent<CharacterManager>().CurrentSelectedPlayable = playable;
    }

    /// <summary>
    /// Tells the character manager to move the playable with the given path
    /// </summary>
    public void MovePlayable()
    {
        HideAllSelectionTile();
        characterManager.GetComponent<CharacterManager>().MoveCurrentPlayer(currentPath);
    }

    /// <summary>
    /// Tells the character manager to attack the playable
    /// </summary>
    public void AttackPlayable(int line, int column)
    {
        HideAllSelectionTile();
        characterManager.GetComponent<CharacterManager>().AttackEnemy(line, column);
    }

    /// <summary>
    /// Set the type the list of tiles given
    /// </summary>
    /// <param name="tiles">the list of tiles we want to change</param>
    /// <param name="type">the type we want to give</param>
    private void SetType(List<GameObject> tiles,TypeSelection type)
    {
        foreach(GameObject selectionTile in tiles)
        {
            selectionTile.GetComponent<SelectionTileScript>().SetType(type);
        }
    }

    /// <summary>
    /// Send the selection tile in the given 
    /// </summary>
    /// <param name="line">line of </param>
    /// <param name="column"></param>
    /// <returns></returns>
    public GameObject GetSelectionTile(int line, int column) 
    {
        GameObject result = null;
         
        foreach (GameObject tile in selectionTiles)
        {
            if (tile.GetComponent<SelectionTileScript>().Ligne == line && tile.GetComponent<SelectionTileScript>().Colonne == column)
            {
                result = tile;
            }
        }

        return result;
    }
}
