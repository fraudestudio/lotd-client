using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerHutScript : MonoBehaviour
{
    public Canvas CanvasHealerHut;
    // Start is called before the first frame update
    void Start()
    {
        CanvasHealerHut = GameObject.Find("CanvasHealerHut").GetComponent<Canvas>();
        CanvasHealerHut.enabled = false;
    }

    private void OnMouseEnter()
    {
        Debug.Log("Mouse is over Healer Hut");
        CanvasHealerHut.enabled = true;
    }

    private void OnMouseExit()
    {
        Debug.Log("Mouse is no longer over Healer Hut");
        CanvasHealerHut.enabled = false;
    }

    private void OnMouseDown()
    {
        Debug.Log("Healer Hut has been clicked");
    }
}
