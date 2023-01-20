using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseVillageCenterScipt : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// Close the center village menu
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GameObject.Find("VillageCenterMenu").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("VillageCenterMenu").GetComponent<CanvasGroup>().blocksRaycasts = false;
            GameObject.Find("VillageCenterMenu").GetComponent<CanvasGroup>().interactable = false;
            BuildingBehaviourScript.CanBeClicked = true;
        }
    }
}
