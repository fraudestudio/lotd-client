using Assets.Scripts.Village;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HealButtonBehaviour : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    // The slot
    private GameObject slot;
    [SerializeField]
    // The slider of the slot
    public GameObject slider;

    /// <summary>
    /// When clicked, start to heal
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!slot.GetComponent<CharacterSlotScript>().SlotIsEmpty)
        {
            slot.GetComponent<CharacterSlotScript>().CanDrop = false;
            slot.GetComponent<CharacterSlotScript>().CurrentCharacter.GetComponent<CharacterImageSlotScript>().SetDrag(false);
            slider.SetActive(true);
            slider.GetComponent<TimeLeftSliderScript>().Init(HealerHut.TimeMaxHealing, HealerHut.TimeMaxHealing);
            transform.gameObject.SetActive(false);
        }
    }
}
