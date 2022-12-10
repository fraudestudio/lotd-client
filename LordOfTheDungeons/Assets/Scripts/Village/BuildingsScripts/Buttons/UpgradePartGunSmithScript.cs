using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePartGunSmithScript : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject Slot;


    private bool isShowed = false;

    private void Update()
    {
        if (!Slot.GetComponent<CharacterSlotScript>().SlotIsEmpty)
        {
            if (!isShowed)
            {
                transform.Find("UpgradePart").gameObject.SetActive(true);
                isShowed = true;
            }
        }
        else
        {
            if (isShowed)
            {
                transform.Find("UpgradePart").gameObject.SetActive(false);
                isShowed = false;
            }
        }
    }
}
