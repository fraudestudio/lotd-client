using Assets.Scripts.Village;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterImageSlotScript : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
{



    // The old layer of the character
    private int oldLayer;
    private bool isEngaged = false;

    private bool canDrag = true;
    private Character character;
    private Transform parentAfterDrag;

    /// <summary>
    /// Tells if we can drag a character into int
    /// </summary>
    public bool CanDrag { get => canDrag; }
    /// <summary>
    /// remember the parent after a drag
    /// </summary>
    public Transform ParentAfterDrag { get => parentAfterDrag; set => parentAfterDrag = value; }
    /// <summary>
    /// Save if the character has been engaged
    /// </summary>
    public bool IsEngaged { get => isEngaged; set => isEngaged = value; }
    /// <summary>
    ///the character in it
    /// </summary>
    public Character Character { get => character; set => character = value; }

    // If the character is being drag or not
    private bool onDrag = false; 

    /// <summary>
    /// When the character is being drag
    /// </summary>
    /// <param name="eventData"></param>
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

    /// <summary>
    /// When we drag
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if (canDrag)
        {
            transform.position = eventData.position;
        }
    }

    /// <summary>
    /// When we stop the drag
    /// </summary>
    /// <param name="eventData"></param>
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

    /// <summary>
    /// Open the menu of the character
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            BuildingBehaviourScript.CanBeClicked = false;
            GameObject.Find("CharacterInfoMenu").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("CharacterInfoMenu").GetComponent<CanvasGroup>().interactable = true;
            GameObject.Find("CharacterInfoMenu").GetComponent<CanvasGroup>().blocksRaycasts = true;
            GameObject.Find("CharacterInfoMenu").GetComponent<ChangeInfoMenuScript>().ChangeInfoMenu(character);
        }
    }
    /// <summary>
    /// Set if the playable can be drag
    /// </summary>
    /// <param name="drag">bool of the drag</param>
    public void SetDrag(bool drag)
    {
        canDrag = drag;

        switch (drag)
        {
            case true: GetComponent<Image>().color = new Color(1,1,1,1); break;
            case false: GetComponent<Image>().color = new Color(1, 1, 1, 0.5f); break;
        }
    }

    /// <summary>
    /// Set the character
    /// </summary>
    /// <param name="c"></param>
    public void SetCharacter(Character c)
    {
        character = c;
    }
    
    /// <summary>
    /// Change the sprite of the image
    /// </summary>
    /// <param name="sprite"></param>
    public void ChangeSprite(Sprite sprite)
    {
        GetComponent<Image>().sprite = sprite;
    }
}
