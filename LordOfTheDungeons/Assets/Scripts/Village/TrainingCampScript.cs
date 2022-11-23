using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingCampScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        Debug.Log("Mouse is over Training Camp");
    }

    private void OnMouseExit()
    {
        Debug.Log("Mouse is no longer over Training Camp");
    }

    private void OnMouseDown()
    {
        Debug.Log("Training Camp has been clicked");
    }
}
