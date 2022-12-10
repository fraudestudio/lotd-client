using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseCharacterInfoMenuScript : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GameObject.Find("CharacterInfoMenu").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("CharacterInfoMenu").GetComponent<CanvasGroup>().interactable = false;
            GameObject.Find("CharacterInfoMenu").GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }
}
