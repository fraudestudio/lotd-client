using Assets.Scripts.ProceduralGeneration.Algorithme;
using Assets.Scripts.ProceduralGeneration.Salles;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mapGenerator;
    [SerializeField]
    private GameObject roomGenerator;
    [SerializeField]
    private GameObject characterManager;

    private GameObject currentRoom;

    public static int roomPosition => 50;

    void Start()
    {
        mapGenerator.GetComponent<MapGenerator>().GenerateMap(new AlgorithmeEliminationSimple().Generer(123));
        roomGenerator.GetComponent<RoomGenerator>().GenerateRoom(new AlgorithmeRoomGeneration().Generer(mapGenerator.GetComponent<MapGenerator>().FirstRoom.GetComponent<SalleObjectScript>().Seed));
        currentRoom = mapGenerator.GetComponent<MapGenerator>().FirstRoom;
        InitPlayers();
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

}
