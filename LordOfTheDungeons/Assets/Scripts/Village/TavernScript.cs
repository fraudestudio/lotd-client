using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavernScript : MonoBehaviour
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
        Debug.Log("Mouse is over Tavern");
    }

    private void OnMouseExit()
    {
        Debug.Log("Mouse is no longer over Tavern");
    }

    private void OnMouseDown()
    {
        Debug.Log("Tavern has been clicked");
    }
}
