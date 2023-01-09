using Assets.Scripts.ProceduralGeneration;
using Assets.Scripts.ProceduralGeneration.Salles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterManager : MonoBehaviour
{

    // Map of the room
    private Carte carte;

    // the current selected playable by the player
    private GameObject currentSelectedPlayable;
    public GameObject CurrentSelectedPlayable { get => currentSelectedPlayable; set => currentSelectedPlayable = value; }


    // Given path to move the current playable
    private List<GameObject> pathToMovePlayable;

    // Say if we can move the current playable on it path
    private bool canMoveCurrentPlayable;


    // Speed deplacement of the playable when moving
    [SerializeField]
    private int playableDeplacementSpeed = 2;

    // the current where the playable is when moving it
    private int currentNodePath;

    // the current position of the node
    private Vector3 currentPosition;

    // Timer for the movement of the playable
    private float timer;


    [SerializeField]
    private GameObject playablePreFab;

    [SerializeField]
    private GameObject cameraManager;

    [SerializeField]
    private GameObject selectionTileManager;
    
    [SerializeField]
    private GameObject playerActionManager;

    // List of thep playable in the room
    private List<GameObject> playables = new List<GameObject>();

    


    /// <summary>
    /// Create the playables on the room
    /// </summary>
    /// <param name="carte">the room</param>
    public void CreatePlayableCharacters(Carte carte)
    {

        DeletePlayable();
        selectionTileManager.GetComponent<SelectionTileManager>().DeleteSelectionTiles();

        this.carte = carte;

        for (int i = 0; i < 6; i++)
        {
            int ligne = GenerateurAleatoire.Instance.Next(3) + 1;
            int colonne = GenerateurAleatoire.Instance.Next(3) + 1;


            // If we can't put the playable, we retry with another coordinates
            while (!PlayableCanBePlaced(ligne,colonne))
            {
                ligne = GenerateurAleatoire.Instance.Next(Carte.Taille - 1) + 1;
                colonne = GenerateurAleatoire.Instance.Next(3) + 1;
            }


            carte.Salles[ligne, colonne].HasPlayer = true;
            GameObject playable = Instantiate(playablePreFab);
            playable.transform.position = new Vector3(GameManager.roomPosition + colonne, GameManager.roomPosition + ligne, - 1);
            playable.GetComponent<PlayableCharacterScript>().ChangeTeam(i % 2);
            playables.Add(playable);


            GiveSelectionManagerMap();
        }
    }

    /// <summary>
    /// Give the map to the selection manager
    /// </summary>
    private void GiveSelectionManagerMap()
    {
        selectionTileManager.GetComponent<SelectionTileManager>().Carte = carte;
    }


    /// <summary>
    /// Verify if a playable can be place in a room
    /// </summary>
    /// <returns>the boolean of the response</returns>
    private bool PlayableCanBePlaced(int ligne, int colonne)
    {
        bool result = true;

        if (carte.Salles[ligne,colonne].Type == TypeSalle.TILEFULL || carte.Salles[ligne, colonne].HasPlayer)
        {
            result = false;
        }

        return result;
    }

    /// <summary>
    /// Delete all the playable on the terrain
    /// </summary>
    public void DeletePlayable()
    {
        for (int i = 0; i < playables.Count; i++)
        {
            Destroy(playables[i]);
        }
        playables.Clear();
    }

    /// <summary>
    /// Move the player on the path that has been given
    /// </summary>
    /// <param name="path"></param>
    public void MoveCurrentPlayer(List<GameObject> path)
    {
        ModifyRoomPlayer(currentSelectedPlayable, false);
        currentNodePath = 0;
        pathToMovePlayable = path;
        CheckNode();
        cameraManager.GetComponent<CameraManager>().CenterOnObjects(path, 3f, 0.2f);
        playerActionManager.GetComponent<PlayerActionManager>().CanDoAnything = false;
        StartCoroutine(WaitForAmountsSeconds(0.3f));
    }

    /// <summary>
    /// Modify the hasPlayer of the tile of a playable
    /// </summary>
    /// <param name="hasPlayer"></param>
    private void ModifyRoomPlayer(GameObject playable,bool hasPlayer)
    {
        carte.Salles[Convert.ToInt32(playable.transform.position.y) - GameManager.roomPosition, Convert.ToInt32(playable.transform.position.x) - GameManager.roomPosition].HasPlayer = hasPlayer;
    }


    /// <summary>
    /// Check if a node can be selected an pick the next one in consequence
    /// </summary>
    private void CheckNode()
    {
        if (currentNodePath < pathToMovePlayable.Count)
        {
            timer = 0;
            currentPosition = pathToMovePlayable[currentNodePath].transform.position;
        }
        else
        {
            // When it is done, we calcule the map and goes back to normal
            GiveSelectionManagerMap();
            currentSelectedPlayable.transform.position = currentPosition;
            canMoveCurrentPlayable = false;
            ModifyRoomPlayer(currentSelectedPlayable, true);
            selectionTileManager.GetComponent<SelectionTileManager>().DeleteSelectionTiles();
            StartCoroutine(WaitForCamera(0.8f, 0.2f));
        }

    }
    
    /// <summary>
    /// Wait before calling the room camera
    /// </summary>
    /// <param name="time">amount of time needed</param>
    /// <param name="cameraSpeed">speed of the camera</param>
    /// <returns></returns>
    private IEnumerator WaitForCamera(float time, float cameraSpeed)
    {
        yield return new WaitForSeconds(time);
        playerActionManager.GetComponent<PlayerActionManager>().CanDoAnything = true;
        cameraManager.GetComponent<CameraManager>().CenterOnRoom(cameraSpeed);
    }


    /// <summary>
    /// Move the player on the path that has been given
    /// </summary>
    private void Update()
    {
        if (canMoveCurrentPlayable)
        {

            if (currentSelectedPlayable.transform.position != currentPosition)
            {
                timer += Time.deltaTime * playableDeplacementSpeed;
                currentSelectedPlayable.transform.position = Vector3.Lerp(currentSelectedPlayable.transform.position, currentPosition, timer);
            }
            else
            {
                currentNodePath++;
                CheckNode();
            }
        }
    }

    /// <summary>
    /// Wait for the amount of time given
    /// </summary>
    /// <param name="time">time in seconds</param>
    /// <returns></returns>
    private IEnumerator WaitForAmountsSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        canMoveCurrentPlayable = true;
    }

}
