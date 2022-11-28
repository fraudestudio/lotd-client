using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterImageSlotScript : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{


    private Transform parentAfterDrag;
    private int oldLayer;
    private bool isEngaged = false;

    private bool canDrag = true;
    public bool CanDrag { get => canDrag; }

    public Transform ParentAfterDrag { get => parentAfterDrag; set => parentAfterDrag = value; }
    public bool IsEngaged { get => isEngaged; set => isEngaged = value; }


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
        }
    }


    public void SetDrag(bool drag)
    {
        canDrag = drag;
    }
}
