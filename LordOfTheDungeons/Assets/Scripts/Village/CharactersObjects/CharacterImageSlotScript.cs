using Assets.Scripts.Village;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterImageSlotScript : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
{


    private Transform parentAfterDrag;
    private int oldLayer;
    private bool isEngaged = false;

    private bool canDrag = true;

    private Character character;

    public bool CanDrag { get => canDrag; }
    public Transform ParentAfterDrag { get => parentAfterDrag; set => parentAfterDrag = value; }
    public bool IsEngaged { get => isEngaged; set => isEngaged = value; }
    public Character Character { get => character; set => character = value; }

    private bool onDrag = false; 


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (canDrag)
        {
            GetComponent<Image>().raycastTarget = false;
            parentAfterDrag = transform.parent;
            CharacterSlotNotAllowedScript.RemoveSlot(transform.parent.gameObject);
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            oldLayer = transform.parent.gameObject.GetComponent<Canvas>().sortingOrder;
            transform.parent.gameObject.GetComponent<Canvas>().sortingOrder = 100;
            CharacterSlotNotAllowedScript.ShowNotAllowedSlot();
            onDrag = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canDrag)
        {
            transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (canDrag)
        {
            transform.parent.gameObject.GetComponent<Canvas>().sortingOrder = oldLayer;
            CharacterSlotNotAllowedScript.HideNotAllowedSlot();
            transform.SetParent(ParentAfterDrag);
            transform.position = parentAfterDrag.position;
            GetComponent<Image>().raycastTarget = true;
            onDrag = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            GameObject.Find("CharacterInfoMenu").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("CharacterInfoMenu").GetComponent<CanvasGroup>().interactable = true;
            GameObject.Find("CharacterInfoMenu").GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }

    public void SetDrag(bool drag)
    {
        canDrag = drag;

        switch (drag)
        {
            case true: GetComponent<Image>().color = new Color(1,1,1,1); break;
            case false: GetComponent<Image>().color = new Color(1, 1, 1, 0.5f); break;
        }
    }


    public void SetCharacter(Character c)
    {
        character = c;
    }
}
