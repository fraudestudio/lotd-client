using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeButtonScript : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// If clicked, start to upgrade a building
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GameObject building = GameObject.Find("BuildingMenu");
            if (building.GetComponent<ModifyMenuScript>().CanBuy())
            {
                VillageManager.DeleteRessources(building.GetComponent<ModifyMenuScript>().GetWoodBuyValue(), building.GetComponent<ModifyMenuScript>().GetStoneBuyValue(), building.GetComponent<ModifyMenuScript>().GetGoldBuyValue());
                VillageManager.NotifyConstructStarted(building.GetComponent<ModifyMenuScript>().GetCurrentUsedBuilding());
            }

        }
    }
}
