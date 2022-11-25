using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CharacterSlotNotAllowedScript : MonoBehaviour
{

    private static List<GameObject> slots = new List<GameObject>();

    private static List<GameObject> images = new List<GameObject>();

    private static Transform thisTransform;

    public GameObject NotAllowedPreFAb;

    public static GameObject preFab;



    public static void AddSlot(GameObject g)
    {
        slots.Add(g);
    }

    public static void RemoveSlot(GameObject g)
    {
        slots.Remove(g);
    }

    public static void ShowNotAllowedSlot()
    {
        foreach (GameObject g in slots)
        {
            GameObject p = Instantiate(preFab);
            p.transform.parent = thisTransform;
            p.transform.position = g.transform.position;
            
            images.Add(p);
        }
    }
    public static void HideNotAllowedSlot()
    {
        foreach(GameObject p in images)
        {
            Destroy(p);
        }
        images.Clear();
    }

    public void Start()
    {
        thisTransform = transform;
        preFab = NotAllowedPreFAb;
    }
}
