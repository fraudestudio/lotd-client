using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerHutScript : MonoBehaviour
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
        Debug.Log("Mouse is over Healer Hut");
    }

    private void OnMouseExit()
    {
        Debug.Log("Mouse is no longer over Healer Hut");
    }

    private void OnMouseDown()
    {
        Debug.Log("Healer Hut has been clicked");
    }
}
