using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoinVillageButtonScript : MonoBehaviour, IPointerClickHandler
{

    private int currentUniverse;

    public int CurrentUniverse { get => currentUniverse; set => currentUniverse = value; }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            Server.SetCurrentVillage(Server.UserGetVillageID(currentUniverse).Town);
            GameObject.Find("loadscreen").GetComponent<loaderScript>().Level("Village");
        }
    }
}
