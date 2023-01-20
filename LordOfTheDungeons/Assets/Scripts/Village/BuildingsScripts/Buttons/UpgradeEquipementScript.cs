using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeEquipementScript : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// Upgrade the equipement
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (GetComponent<Button>().interactable)
            {
                transform.parent.parent.parent.Find("CharacterSlot").GetComponent<UpgradePartGunSmithScript>().HasBought(name);
            }
        }
    }
}
