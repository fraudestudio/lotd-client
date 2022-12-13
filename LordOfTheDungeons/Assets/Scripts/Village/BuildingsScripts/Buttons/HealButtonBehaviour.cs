using Assets.Scripts.Village;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HealButtonBehaviour : MonoBehaviour, IPointerClickHandler
{
    public GameObject slot;
    public GameObject slider;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!slot.GetComponent<CharacterSlotScript>().SlotIsEmpty)
        {
            slot.GetComponent<CharacterSlotScript>().CanDrop = false;
            slot.GetComponent<CharacterSlotScript>().CurrentCharacter.GetComponent<CharacterImageSlotScript>().SetDrag(false);
            slider.SetActive(true);
            slider.GetComponent<TimeLeftSliderScript>().Init(10, 10);//HealerHut.TimeMaxHealing, HealerHut.TimeMaxHealing);
            transform.gameObject.SetActive(false);
        }
    }
}
