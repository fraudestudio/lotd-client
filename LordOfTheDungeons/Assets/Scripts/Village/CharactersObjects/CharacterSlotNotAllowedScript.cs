using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CharacterSlotNotAllowedScript : MonoBehaviour
{
    /// <summary>
    /// List of the slots
    /// </summary>
    private static List<GameObject> slots = new List<GameObject>();


    /// <summary>
    /// List of the images
    /// </summary>
    private static List<GameObject> images = new List<GameObject>();

    private static Transform thisTransform;

    [SerializeField]
    private GameObject NotAllowedPreFAb;

    [SerializeField]
    private static GameObject preFab;


    /// <summary>
    /// Add a slot to the not allowed
    /// </summary>
    /// <param name="g"></param>
    public static void AddSlot(GameObject g)
    {
        slots.Add(g);
    }

    /// <summary>
    /// Remove a slot to the not allowed slots
    /// </summary>
    /// <param name="g"></param>
    public static void RemoveSlot(GameObject g)
    {
        slots.Remove(g);
    }

    /// <summary>
    /// Show all the slots that are not allowed
    /// </summary>
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
    
    /// <summary>
    /// Hide the not allowed slots
    /// </summary>
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
