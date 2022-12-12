using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeButtonScript : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (GameObject.Find("BuildingMenu").GetComponent<ModifyMenuScript>().CanBuy(GameObject.Find("BuildingMenu").GetComponent<ModifyMenuScript>().GetCurrentUsedBuilding()))
            {
                VillageManager.NotifyConstructStarted(GameObject.Find("BuildingMenu").GetComponent<ModifyMenuScript>().GetCurrentUsedBuilding());
            }

        }
    }
}
