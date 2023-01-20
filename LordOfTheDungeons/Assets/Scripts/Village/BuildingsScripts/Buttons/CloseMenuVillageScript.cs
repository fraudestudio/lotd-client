using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseMenuVillageScript : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// When clicked, close the menu
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // On rend invisible le menu
            transform.parent.gameObject.GetComponent<CanvasGroup>().alpha = 0;
            transform.parent.gameObject.GetComponent<CanvasGroup>().interactable = false;
            transform.parent.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;

            // On rend invisible le menu du b�timent
            GameObject b = GameObject.Find(transform.parent.gameObject.GetComponent<ModifyMenuScript>().GetCurrentUsedBuilding() + "Menu");
            b.GetComponent<CanvasGroup>().interactable = false;
            b.GetComponent<CanvasGroup>().blocksRaycasts = false;
            b.GetComponent<CanvasGroup>().alpha = 0;

            // On arr�te les �l�ments du menu du b�timent
            GameObject.Find(transform.parent.gameObject.GetComponent<ModifyMenuScript>().GetCurrentUsedBuilding()).GetComponent<BuildingBehaviourScript>().StopBuilding(transform.parent.gameObject.GetComponent<ModifyMenuScript>().GetCurrentUsedBuilding());
            transform.parent.gameObject.GetComponent<ModifyMenuScript>().ResetCurrentUsedBuilding();


            // On arr�te le timer de construction
            transform.parent.gameObject.GetComponent<ModifyMenuScript>().StopConstructionTimer();
            
            // On repermet de cliquer sur les b�timents
            BuildingBehaviourScript.CanBeClicked = true;
        }
    }
}
