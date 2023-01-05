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
    void Start()
    {

        System.Random r = new System.Random(123);
        for (int i = 0; i < 10; i++)
        {
            Debug.Log(r.Next(100));
        }


        mapGenerator.GetComponent<MapGenerator>().GenerateMap(new AlgorithmeEliminationSimple().Generer(123));
        roomGenerator.GetComponent<RoomGenerator>().GenerateRoom(new AlgorithmeRoomGeneration().Generer(123));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
