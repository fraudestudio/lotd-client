using Assets.Scripts.ProceduralGeneration;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class SalleObjectScript : MonoBehaviour
{
    // the room object of the room 
    private Salle salle;
    public Salle Salle { get => salle; set => salle = value; }

    [SerializeField]
    // the seed of the room 
    private int seed;
    public int Seed { get => seed; set => seed = value; }

    [SerializeField]
    // the right neighboor of the room 
    private GameObject right = null;
    public GameObject Right { get => right; set => right = value; }

    [SerializeField]
    // the left neighboor of the room 
    private GameObject left = null;
    public GameObject Left { get => left; set => left = value; }

    [SerializeField]
    // the top neighboor of the room 
    private GameObject top = null;
    public GameObject Top { get => top; set => top = value; }

    [SerializeField]
    // the bottom neighboor of the room 
    private GameObject bottom = null;
    public GameObject Bottom { get => bottom; set => bottom = value; }

}
