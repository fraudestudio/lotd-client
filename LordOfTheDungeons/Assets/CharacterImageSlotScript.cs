using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterImageSlotScript : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    private Transform parentAfterDrag;
    private bool isEngaged = false;

    public Transform ParentAfterDrag { get => parentAfterDrag; set => parentAfterDrag = value; }
    public bool IsEngaged { get => isEngaged; set => isEngaged = value; }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponent<Image>().raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        Debug.Log("Begin");
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        Debug.Log("Drag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(ParentAfterDrag);
        transform.position = parentAfterDrag.position;
        GetComponent<Image>().raycastTarget = true;
        Debug.Log("End");
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
