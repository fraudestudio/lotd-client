using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI.Table;

public class CharacterSlotScript : MonoBehaviour, IDropHandler
{

    public SlotType Type = SlotType.RECRUIT;


    public GameObject SlotPreFab;
    public bool slotIsEmpty = true;

    public bool SlotIsEmpty { get => slotIsEmpty; set => slotIsEmpty = value; }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject drop = eventData.pointerDrag;
        if (SlotIsEmpty)
        {
            if (Type == SlotType.BUILDING)
            {
                drop.GetComponent<CharacterImageSlotScript>().IsEngaged = false;
                SlotIsEmpty = false;
                drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag.GetComponent<CharacterSlotScript>().SlotIsEmpty = true;
                if (drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag.GetComponent<CharacterSlotScript>().Type == SlotType.RECRUIT)
                {
                    Destroy(drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag.gameObject);
                }
                CharacterSlotNotAllowedScript.AddSlot(transform.gameObject);
                CharacterSlotNotAllowedScript.RemoveSlot(drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag.gameObject);
                drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag = transform;
            }
            else if (Type == SlotType.RECRUIT)
            {
                if (!drop.GetComponent<CharacterImageSlotScript>().IsEngaged)
                {
                    SlotIsEmpty = false;
                    transform.Find("PlusImage").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    GameObject d = Instantiate(SlotPreFab);
                    d.transform.SetParent(transform.parent);
                    d.transform.localScale = new Vector3(1, 1, 1);
                    drop.GetComponent<CharacterImageSlotScript>().IsEngaged = true;
                    drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag.GetComponent<CharacterSlotScript>().SlotIsEmpty = true;
                    drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag = transform;
                    CharacterSlotNotAllowedScript.RemoveSlot(drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag.gameObject);
                    CharacterSlotNotAllowedScript.AddSlot(transform.gameObject);
                }
            }

        }
        else
        {
            CharacterSlotNotAllowedScript.AddSlot(drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag.gameObject);
        }
    }

    public void Awake()
    {
        if (transform.childCount > 2)
        {
            SlotIsEmpty = false;
            CharacterSlotNotAllowedScript.AddSlot(transform.gameObject);
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
