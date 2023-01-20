using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseCharacterInfoMenuScript : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// Close the character menu
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (GameObject.Find("BuildingMenu").GetComponent<ModifyMenuScript>().GetCurrentUsedBuilding() == "")
            {
                BuildingBehaviourScript.CanBeClicked = true;
            }

            GameObject.Find("CharacterInfoMenu").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("CharacterInfoMenu").GetComponent<CanvasGroup>().interactable = false;
            GameObject.Find("CharacterInfoMenu").GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }
}
