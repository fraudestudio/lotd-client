using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptTrainingCamp : MonoBehaviour
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
        Debug.Log("Mouse is no longer on Training Camp");
    }
}
