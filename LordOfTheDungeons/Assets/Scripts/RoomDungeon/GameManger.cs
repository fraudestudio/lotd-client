using Assets.Scripts.ProceduralGeneration.Algorithme;
using Assets.Scripts.ProceduralGeneration.Salles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    [SerializeField]
    private GameObject mapGenerator;
    [SerializeField]
    private GameObject roomGenerator;

    private GameObject currentRoom;

    void Start()
    {
        mapGenerator.GetComponent<MapGenerator>().GenerateMap(new AlgorithmeEliminationSimple().Generer(123));
        roomGenerator.GetComponent<RoomGenerator>().GenerateRoom(new AlgorithmeRoomGeneration().Generer(mapGenerator.GetComponent<MapGenerator>().FirstRoom.GetComponent<SalleObjectScript>().Seed));
        currentRoom = mapGenerator.GetComponent<MapGenerator>().FirstRoom;
    }


    public void GenerateRoom()
    {
        roomGenerator.GetComponent<RoomGenerator>().GenerateRoom(new AlgorithmeRoomGeneration().Generer(new System.Random().Next()));
    }


    public void ChangeRoom()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
