using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

public class TrainingCampScript : MonoBehaviour
{
    public Canvas CanvasTrainingCamp;
    // Start is called before the first frame update
    void Start()
    {
        CanvasTrainingCamp = GameObject.Find("CanvasTrainingCamp").GetComponent<Canvas>();
        CanvasTrainingCamp.enabled = false;
    }

    public void OnMouseEnter()
    {
        Debug.Log("Mouse is over Training Camp");
        CanvasTrainingCamp.enabled = true;


    }

    public void OnMouseExit()
    {
        Debug.Log("Mouse is no longer over Training Camp");
        CanvasTrainingCamp.enabled = false;

    }

    public void OnMouseDown()
    {
        Debug.Log("Training Camp has been clicked");
    }
}
