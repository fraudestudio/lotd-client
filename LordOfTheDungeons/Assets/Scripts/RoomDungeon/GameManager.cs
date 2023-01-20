using Assets.Scripts.ProceduralGeneration.Algorithme;
using Assets.Scripts.ProceduralGeneration.Salles;
using Assets.Scripts.RoomDungeon.TurnManagement;
using Assets.Scripts.Server.ServerGame;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mapGenerator;
    [SerializeField]
    private GameObject roomGenerator;
    [SerializeField]
    private GameObject characterManager;
    [SerializeField]
    private GameObject playerActionManager;
    [SerializeField]
    private GameObject turnManager;

    private GameObject currentRoom;

    public static int roomPosition => 50;

    void Start()
    {
        mapGenerator.GetComponent<MapGenerator>().GenerateMap(new AlgorithmeEliminationSimple().Generer(GameServer.Instance.Seed));
        roomGenerator.GetComponent<RoomGenerator>().GenerateRoom(new AlgorithmeRoomGeneration().Generer(mapGenerator.GetComponent<MapGenerator>().FirstRoom.GetComponent<SalleObjectScript>().Seed));
        currentRoom = mapGenerator.GetComponent<MapGenerator>().FirstRoom;
        SetCurrentPlayer(GameServer.Instance.Order);

        InitPlayers();

        turnManager.GetComponent<TurnManager>().StartManage(GameServer.Instance.AskTurn());
        VerifyState();
    }


    public void VerifyState()
    {
        GameServer.Instance.AskGameState();
        if (GameServer.Instance.GameState == GameState.WAITING)
        {
            GameServer.Instance.WaitForServerResponse();
            playerActionManager.GetComponent<PlayerActionManager>().CanDoAnything = false;
        }
        else
        {
            playerActionManager.GetComponent<PlayerActionManager>().CanDoAnything = true;
        }
    }


    public void Update()
    {
        if (GameServer.Instance.GetCurrentRequest() != "")
        {
            string[] arrayResponse = GameServer.Instance.GetCurrentRequest().Split(' ');

            if (arrayResponse[0] == "MOVE")
            {
                if (arrayResponse[1] == "CHARACTER")
                {
                    characterManager.GetComponent<CharacterManager>().MovePlayable(int.Parse(arrayResponse[2]), int.Parse(arrayResponse[3]), int.Parse(arrayResponse[4]));
                }
            }

            else if (arrayResponse[0] == "AskForState")
            {
                turnManager.GetComponent<TurnManager>().StartManage(GameServer.Instance.AskTurn());
                VerifyState();
                GameServer.Instance.ResetRequest();
            }
        }
    }

    private void SetCurrentPlayer(int order)
    {
        switch (order)
        {
            case 0: turnManager.GetComponent<TurnManager>().CurrentPlayer = TypeTurn.Player_1; break;
            case 1: turnManager.GetComponent<TurnManager>().CurrentPlayer = TypeTurn.Player_2; break;
        }
    }

    /// <summary>
    /// Create the player in the room 
    /// </summary>
    private void InitPlayers()
    {
        characterManager.GetComponent<CharacterManager>().CreatePlayableCharacters(roomGenerator.GetComponent<RoomGenerator>().Carte);
    }


    /// <summary>
    /// Generate a random room 
    /// </summary>
    public void GenerateRoom()
    {
        roomGenerator.GetComponent<RoomGenerator>().GenerateRoom(new AlgorithmeRoomGeneration().Generer(new System.Random().Next()));
        InitPlayers();
    }

    /// <summary>
    /// Generate a room with the seed of the given room
    /// </summary>
    /// <param name="room">Room we want to generate</param>
    public void GenerateRoom(GameObject room)
    {
        roomGenerator.GetComponent<RoomGenerator>().GenerateRoom(new AlgorithmeRoomGeneration().Generer(room.GetComponent<SalleObjectScript>().Seed));
        InitPlayers();
    }

    /// <summary>
    /// Change the room
    /// </summary>
    public void ChangeRoom()
    {
        StartCoroutine(FadeStartAnim());
    }

    /// <summary>
    /// Change wich to generate room in function of the arrow that has been clicked
    /// Then it stores the generated room in the current room
    /// </summary>
    /// <param name="direction"></param>
    public void GetArrow(string direction)
    {
        switch (direction)
        {
            case "Right": 
                {
                    GenerateRoom(currentRoom.GetComponent<SalleObjectScript>().Right);
                    currentRoom = currentRoom.GetComponent<SalleObjectScript>().Right;
                } 
                break;
            case "Left":
                {
                    GenerateRoom(currentRoom.GetComponent<SalleObjectScript>().Left);
                    currentRoom = currentRoom.GetComponent<SalleObjectScript>().Left;
                }
                break;
            case "Up":
                {
                    GenerateRoom(currentRoom.GetComponent<SalleObjectScript>().Top);
                    currentRoom = currentRoom.GetComponent<SalleObjectScript>().Top;
                }
                break;
            case "Down":
                {
                    GenerateRoom(currentRoom.GetComponent<SalleObjectScript>().Bottom);
                    currentRoom = currentRoom.GetComponent<SalleObjectScript>().Bottom;
                }
                break;
        }

        // Set the arrow position on the right room 
        mapGenerator.GetComponent<MapGenerator>().SetArrowPosition(currentRoom.GetComponent<SalleObjectScript>().Salle.Ligne, currentRoom.GetComponent<SalleObjectScript>().Salle.Colonne);
        StartCoroutine(FadeStopAnim());
    }


    /// <summary>
    /// Stop the animations and goes back to the normal game
    /// </summary>
    /// <returns></returns>
    public IEnumerator FadeStopAnim()
    {

        GameObject.Find("ChangeRoom").transform.Find("Buttons").GetComponent<Animator>().SetTrigger("UnZoom");
        GameObject.Find("ChangeRoom").transform.Find("Buttons").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("ChangeRoom").transform.Find("Buttons").GetComponent<CanvasGroup>().interactable = false;
        GameObject.Find("ChangeRoom").transform.Find("Buttons").GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameObject.Find("ChangeRoom").transform.Find("MiniMap").GetComponent<Animator>().SetTrigger("UnZoom");
        yield return new WaitForSeconds(1f);
        GameObject.Find("ChangeRoom").transform.Find("Door").GetComponent<Animator>().SetTrigger("StartAnimZoom");
        yield return new WaitForSeconds(2f);
        GameObject.Find("ChangeRoom").transform.Find("Door").GetComponent<Animator>().SetTrigger("StopAnimZoom");
        GameObject.Find("ChangeRoom").transform.Find("Door").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("ChangeRoom").GetComponent<Animator>().SetTrigger("StartAnimFadeOut");
        yield return new WaitForSeconds(1f);
        GameObject.Find("ChangeRoom").GetComponent<Animator>().SetTrigger("StopAnim");
        GameObject.Find("ChangeRoom").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("ChangeRoom").GetComponent<CanvasGroup>().interactable = false;
        GameObject.Find("ChangeRoom").GetComponent<CanvasGroup>().blocksRaycasts = false;

    }


    /// <summary>
    /// Start the fade animation with the door
    /// It stops when the buttons to select a room shows up
    /// </summary>
    /// <returns></returns>
    public IEnumerator FadeStartAnim()
    {
        GameObject.Find("ChangeRoom").GetComponent<Animator>().SetTrigger("StartAnimFade");
        yield return new WaitForSeconds(1.5f);
        GameObject.Find("ChangeRoom").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("ChangeRoom").GetComponent<CanvasGroup>().interactable = true;
        GameObject.Find("ChangeRoom").GetComponent<CanvasGroup>().blocksRaycasts = true;
        GameObject.Find("ChangeRoom").transform.Find("Door").GetComponent<Animator>().SetTrigger("StartAnimDoor");
        GameObject.Find("ChangeRoom").transform.Find("Door").GetComponent<CanvasGroup>().alpha = 1;
        yield return new WaitForSeconds(2f);


        GameObject.Find("ChangeRoom").transform.Find("MiniMap").GetComponent<CanvasGroup>().alpha = 1;

        GameObject.Find("ChangeRoom").transform.Find("MiniMap").GetComponent<Animator>().SetTrigger("Zoom");
        GameObject.Find("ChangeRoom").transform.Find("Buttons").GetComponent<Animator>().SetTrigger("Zoom");

        yield return new WaitForSeconds(0.3f);
        GameObject.Find("ChangeRoom").transform.Find("Door").GetComponent<Animator>().SetTrigger("StopAnimDoor");



        // We check wich arrow to select
        if (currentRoom.GetComponent<SalleObjectScript>().Left == null)
        {
            GameObject.Find("ChangeRoom").transform.Find("Buttons").transform.Find("LeftButton").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("ChangeRoom").transform.Find("Buttons").transform.Find("LeftButton").GetComponent<CanvasGroup>().interactable = false;
            GameObject.Find("ChangeRoom").transform.Find("Buttons").transform.Find("LeftButton").GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        else
        {
            GameObject.Find("ChangeRoom").transform.Find("Buttons").transform.Find("LeftButton").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("ChangeRoom").transform.Find("Buttons").transform.Find("LeftButton").GetComponent<CanvasGroup>().interactable = true;
            GameObject.Find("ChangeRoom").transform.Find("Buttons").transform.Find("LeftButton").GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

        if (currentRoom.GetComponent<SalleObjectScript>().Right == null)
        {
            GameObject.Find("ChangeRoom").transform.Find("Buttons").transform.Find("RightButton").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("ChangeRoom").transform.Find("Buttons").transform.Find("RightButton").GetComponent<CanvasGroup>().interactable = false;
            GameObject.Find("ChangeRoom").transform.Find("Buttons").transform.Find("RightButton").GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        else
        {
            GameObject.Find("ChangeRoom").transform.Find("Buttons").transform.Find("RightButton").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("ChangeRoom").transform.Find("Buttons").transform.Find("RightButton").GetComponent<CanvasGroup>().interactable = true;
            GameObject.Find("ChangeRoom").transform.Find("Buttons").transform.Find("RightButton").GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

        if (currentRoom.GetComponent<SalleObjectScript>().Top == null)
        {
            GameObject.Find("ChangeRoom").transform.Find("Buttons").transform.Find("UpButton").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("ChangeRoom").transform.Find("Buttons").transform.Find("UpButton").GetComponent<CanvasGroup>().interactable = false;
            GameObject.Find("ChangeRoom").transform.Find("Buttons").transform.Find("UpButton").GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        else
        {
            GameObject.Find("ChangeRoom").transform.Find("Buttons").transform.Find("UpButton").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("ChangeRoom").transform.Find("Buttons").transform.Find("UpButton").GetComponent<CanvasGroup>().interactable = true;
            GameObject.Find("ChangeRoom").transform.Find("Buttons").transform.Find("UpButton").GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

        if (currentRoom.GetComponent<SalleObjectScript>().Bottom == null)
        {
            GameObject.Find("ChangeRoom").transform.Find("Buttons").transform.Find("DownButton").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("ChangeRoom").transform.Find("Buttons").transform.Find("DownButton").GetComponent<CanvasGroup>().interactable = false;
            GameObject.Find("ChangeRoom").transform.Find("Buttons").transform.Find("DownButton").GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        else
        {
            GameObject.Find("ChangeRoom").transform.Find("Buttons").transform.Find("DownButton").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("ChangeRoom").transform.Find("Buttons").transform.Find("DownButton").GetComponent<CanvasGroup>().interactable = true;
            GameObject.Find("ChangeRoom").transform.Find("Buttons").transform.Find("DownButton").GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

        GameObject.Find("ChangeRoom").transform.Find("Buttons").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("ChangeRoom").transform.Find("Buttons").GetComponent<CanvasGroup>().interactable = true;
        GameObject.Find("ChangeRoom").transform.Find("Buttons").GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    /// <summary>
    /// Enemy attack a player
    /// </summary>
    /// <param name="linePlayable">line position of the playable</param>
    /// <param name="columnPlayable">column position of the playable</param>
    /// <param name="lineEnemy">line position of the enemy</param>
    /// <param name="columnEnemy">column position of the enemy</param>
    public void EnemyAttackPlayer(int linePlayable, int columnPlayable, int lineEnemy, int columnEnemy)
    {
        characterManager.GetComponent<CharacterManager>().EnemyAttackPlayable(linePlayable,columnPlayable,lineEnemy,columnEnemy);
    }

    /// <summary>
    /// Move an enemy to wanted coordinates
    /// </summary>
    /// <param name="lineEnemy">the line coordinates of the enemy</param>
    /// <param name="columnEnemy">the column coordinates of the enemy</param>
    /// <param name="lineObjective"></param>
    /// <param name="columnObjective"></param>
    public void EnemyMoveTo(int lineEnemy, int columnEnemy, int lineObjective, int columnObjective)
    {
        characterManager.GetComponent<CharacterManager>().MoveEnemy(lineEnemy, columnEnemy, lineObjective, columnObjective);
    }

}
