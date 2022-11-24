using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WareHouseScript : MonoBehaviour
{
    public Canvas CanvasWarehouse;

    // Start is called before the first frame update
    void Start()
    {
        CanvasWarehouse = GameObject.Find("CanvasWarehouse").GetComponent<Canvas>();
        CanvasWarehouse.enabled = false;
    }

    public void OnMouseDown()
    {
        Debug.Log("WareHouse Clicked");
        
    }

    public void OnMouseEnter()
    {
        Debug.Log("WareHouse Mouse Enter");
        CanvasWarehouse.enabled = true;
    }

    public void OnMouseExit()
    {
        Debug.Log("WareHouse Mouse Exit");
        CanvasWarehouse.enabled = false;
    }
}
