using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterSlotScript : MonoBehaviour, IDropHandler
{

    public GameObject SlotPreFab;
    private bool slotIsEmpty = true;

    public bool SlotIsEmpty { get => slotIsEmpty; set => slotIsEmpty = value; }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject drop = eventData.pointerDrag;
        if (SlotIsEmpty)
        {
            if (!drop.GetComponent<CharacterImageSlotScript>().IsEngaged)
            {
                SlotIsEmpty = false;
                transform.Find("PlusImage").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                GameObject d = Instantiate(SlotPreFab);
                d.transform.SetParent(transform.parent);
                d.transform.localScale = new Vector3(1, 1, 1);
                drop.GetComponent<CharacterImageSlotScript>().IsEngaged = true;
                drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag = transform;
            }
        }
        else
        {
            drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag = transform;
        }
    }

    public void Awake()
    {
        if (transform.childCount > 2)
        {
            SlotIsEmpty = false;
        }
        else
        {
            slotIsEmpty = true;
        }

        if (SlotIsEmpty)
        {
            transform.Find("PlusImage").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
