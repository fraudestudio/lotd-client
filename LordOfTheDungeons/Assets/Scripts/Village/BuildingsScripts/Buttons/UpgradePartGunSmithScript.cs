using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePartGunSmithScript : MonoBehaviour
{

    public GameObject upgradePart;
    public void CanUpgradeObserver(bool value)
    {
        upgradePart.SetActive(value);
    }

}
