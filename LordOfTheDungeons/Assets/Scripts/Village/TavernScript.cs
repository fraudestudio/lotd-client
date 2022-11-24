using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavernScript : MonoBehaviour
{
    public Canvas CanvasTavern;
    // Start is called before the first frame update
    void Start()
    {
        CanvasTavern = GameObject.Find("CanvasTavern").GetComponent<Canvas>();
        CanvasTavern.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        Debug.Log("Mouse is over Tavern");
        CanvasTavern.enabled = true;
    }

    private void OnMouseExit()
    {
        Debug.Log("Mouse is no longer over Tavern");
        CanvasTavern.enabled = false;
    }

    private void OnMouseDown()
    {
        Debug.Log("Tavern has been clicked");
    }
}
