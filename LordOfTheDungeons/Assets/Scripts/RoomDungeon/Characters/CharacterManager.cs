using Assets.Scripts.ProceduralGeneration;
using Assets.Scripts.ProceduralGeneration.Salles;
using Assets.Scripts.RoomDungeon.Characters.Selection;
using Assets.Scripts.RoomDungeon.TurnManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using UnityEngine;
using UnityEngine.Playables;
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
    private GameObject enemiesPreFab;

    [SerializeField]
    private GameObject cameraManager;

    [SerializeField]
    private GameObject selectionTileManager;
    
    [SerializeField]
    private GameObject playerActionManager;

    // List of thep playable in the room
    private List<GameObject> playables = new List<GameObject>();

    private List<GameObject> enemies = new List<GameObject>();
    




    /// <summary>
    /// Create the playables on the room
    /// </summary>
    /// <param name="carte">the room</param>
    public void CreatePlayableCharacters(Carte carte)
    {

        DeletePlayable();
        DeleteEnemies();
        selectionTileManager.GetComponent<SelectionTileManager>().DeleteSelectionTiles();

        this.carte = carte;

        for (int i = 0; i < 6; i++)
        {
            int ligne = GenerateurAleatoire.Instance.Next(Carte.Taille - 1) + 1;
            int colonne = GenerateurAleatoire.Instance.Next(3) + 1;


            // If we can't put the playable, we retry with another coordinates
            while (!PlayableCanBePlaced(ligne,colonne))
            {
                ligne = GenerateurAleatoire.Instance.Next(Carte.Taille - 1) + 1;
                colonne = GenerateurAleatoire.Instance.Next(3) + 1;
            }

            GameObject playable = Instantiate(playablePreFab);
            playable.transform.position = new Vector3(GameManager.roomPosition + colonne, GameManager.roomPosition + ligne, - 1);
            ModifyRoomPlayer(playable, true);
            playable.GetComponent<PlayableCharacterScript>().ChangeTeam(i % 2);
            playables.Add(playable);


            GiveSelectionManagerMap();
        }


        for (int i = 0; i < 6; i++)
        {
            int ligne = GenerateurAleatoire.Instance.Next(Carte.Taille - 1) + 1;
            int colonne = Carte.Taille - 5 + GenerateurAleatoire.Instance.Next(3) + 1;


            // If we can't put the enemy, we retry with another coordinates
            while (!PlayableCanBePlaced(ligne, colonne))
            {
                ligne = GenerateurAleatoire.Instance.Next(Carte.Taille - 1) + 1;
                colonne = Carte.Taille - 5 + GenerateurAleatoire.Instance.Next(3) + 1;
            }


            GameObject enemy = Instantiate(enemiesPreFab);
            enemy.transform.position = new Vector3(GameManager.roomPosition + colonne, GameManager.roomPosition + ligne, -1);
            ModifyRoomPlayer(enemy, true);
            enemies.Add(enemy);


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
    /// Delete all the playable on the terrain
    /// </summary>
    public void DeleteEnemies()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            Destroy(enemies[i]);
        }
        enemies.Clear();
    }

    /// <summary>
    /// Move the player on the path that has been given
    /// </summary>
    /// <param name="path"></param>
    public void MoveCurrentPlayer(List<GameObject> path)
    {
        playerActionManager.GetComponent<PlayerActionManager>().ShowAttackATH(false);
        ModifyRoomPlayer(currentSelectedPlayable, false);
        currentNodePath = 0;
        pathToMovePlayable = path;
        CheckNode();
        cameraManager.GetComponent<CameraManager>().CenterOnObjects(path, 5f, 0.2f);
        playerActionManager.GetComponent<PlayerActionManager>().CanDoAnything = false;
        StartCoroutine(WaitForAmountsSeconds(0.3f));
    }

    /// <summary>
    /// Attack the given enemy
    /// </summary>
    public void AttackEnemy(int line, int column)
    {

        int playerPower = currentSelectedPlayable.GetComponent<PlayableCharacterScript>().Power;

        playerActionManager.GetComponent<PlayerActionManager>().ShowMoveATH(false);
        GameObject enemy = GetEnemy(line, column);
        List<GameObject> center = new List<GameObject>();
        center.Add(enemy);
        center.Add(currentSelectedPlayable);
        cameraManager.GetComponent<CameraManager>().CenterOnObjects(center, 5f, 0.2f);
        playerActionManager.GetComponent<PlayerActionManager>().CanDoAnything = false;
        StartCoroutine(WaitForAmountsSeconds(0.5f));
        enemy.GetComponent<EnemyScript>().Hurt(playerPower);
        selectionTileManager.GetComponent<SelectionTileManager>().DeleteSelectionTiles();
        playerActionManager.GetComponent<PlayerActionManager>().CanDoAnything = false;
        StartCoroutine(WaitForCamera(1f, 0.2f));

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



    /// <summary>
    /// Change the type of selction of the playable
    /// </summary>
    /// <param name="type"></param>
    public void ChangeTypeSelection(TypeSelection type)
    {
        switch (type)
        {
            case TypeSelection.Deplacement: selectionTileManager.GetComponent<SelectionTileManager>().CreateSelectionTiles(currentSelectedPlayable.GetComponent<PlayableCharacterScript>().Movement, type, currentSelectedPlayable); break;
            case TypeSelection.Attack: selectionTileManager.GetComponent<SelectionTileManager>().CreateSelectionTiles(currentSelectedPlayable.GetComponent<PlayableCharacterScript>().Action, type, currentSelectedPlayable); break;
        }

    }

    /// <summary>
    /// Return the enemy with the coordinates given
    /// </summary>
    /// <param name="line">it line</param>
    /// <param name="column">it column</param>
    /// <returns>the enemy wanted if found, null otherwise</returns>
    public GameObject GetEnemy(int line, int column)
    {

        GameObject result = null;

        foreach (GameObject enemy in enemies)
        {
            if (enemy.transform.position.x == line + GameManager.roomPosition && enemy.transform.position.y == column + GameManager.roomPosition)
            {
                result = enemy;
            }
        }

        return result;
    }

    /// <summary>
    /// Kill the given character
    /// </summary>
    /// <param name="enemy"></param>
    public void KillCharacter(GameObject character)
    {

        ModifyRoomPlayer(character, false);
        playables.Remove(character);
        enemies.Remove(character);
        Destroy(character);
    }

    public void EnemyAttackPlayable(int linePlayable, int columnPlayable, int lineEnemy, int columnEnemy)
    {

    }

}
