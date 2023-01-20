using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowEquipementScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// Show the equipement stats
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        switch (name)
        {
            case "Weapon": transform.Find("WeaponInfo").GetComponent<CanvasGroup>().alpha = 1; break;
            case "Armor": transform.Find("ArmorInfo").GetComponent<CanvasGroup>().alpha = 1; break;
        }
    }

    /// <summary>
    /// Hide the equipement stats
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        switch (name)
        {
            case "Weapon": transform.Find("WeaponInfo").GetComponent<CanvasGroup>().alpha = 0; break;
            case "Armor": transform.Find("ArmorInfo").GetComponent<CanvasGroup>().alpha = 0; break;
        }
    }
}
