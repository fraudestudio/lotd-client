using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowEquipementScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        switch (name)
        {
            case "Weapon": transform.Find("WeaponInfo").GetComponent<CanvasGroup>().alpha = 1; break;
            case "Armor": transform.Find("ArmorInfo").GetComponent<CanvasGroup>().alpha = 1; break;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        switch (name)
        {
            case "Weapon": transform.Find("WeaponInfo").GetComponent<CanvasGroup>().alpha = 0; break;
            case "Armor": transform.Find("ArmorInfo").GetComponent<CanvasGroup>().alpha = 0; break;
        }
    }
}
