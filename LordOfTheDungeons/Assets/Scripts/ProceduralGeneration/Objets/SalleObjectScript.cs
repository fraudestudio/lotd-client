using Assets.Scripts.ProceduralGeneration;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class SalleObjectScript : MonoBehaviour
{
    private Salle salle;
    public Salle Salle { get => salle; set => salle = value; }

    [SerializeField]
    private int seed;
    public int Seed { get => seed; set => seed = value; }

    [SerializeField]
    private GameObject right = null;
    public GameObject Right { get => right; set => right = value; }

    [SerializeField]
    private GameObject left = null;
    public GameObject Left { get => left; set => left = value; }

    [SerializeField]
    private GameObject top = null;
    public GameObject Top { get => top; set => top = value; }

    [SerializeField]
    private GameObject bottom = null;
    public GameObject Bottom { get => bottom; set => bottom = value; }

}
