using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBehaviourScript : MonoBehaviour
{

    private void OnMouseDown()
    {
        Debug.Log(transform.name + "has been click");       
    }

    private void OnMouseEnter()
    {
        Debug.Log(transform.name + " entered");
    }

    private void OnMouseExit()
    {
        Debug.Log(transform.name + " exited");
    }
}
