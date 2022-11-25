using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsmithScript : MonoBehaviour
{
    public Canvas CanvasGunsmith;
    // Start is called before the first frame update
    void Start()
    {
        CanvasGunsmith = GameObject.Find("CanvasGunsmith").GetComponent<Canvas>();
        CanvasGunsmith.enabled = false;
    }

    public void OnMouseDown()
    {
        Debug.Log("Gunsmith Clicked");
    }

    public void OnMouseEnter()
    {
        Debug.Log("Gunsmith Mouse Enter");
        CanvasGunsmith.enabled = true;
    }

    public void OnMouseExit()
    {
        Debug.Log("Gunsmith Mouse Exit");
        CanvasGunsmith.enabled = false;
    }
}
