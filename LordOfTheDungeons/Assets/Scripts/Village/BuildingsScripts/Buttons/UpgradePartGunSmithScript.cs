using Assets.Scripts.Village;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePartGunSmithScript : MonoBehaviour
{
    [SerializeField]
    private Image WeaponIcon;
    [SerializeField]
    private Image ArmorIcon;

    [SerializeField]
    private TMP_Text WeaponGold;
    [SerializeField]
    private TMP_Text WeaponStone;
    [SerializeField]
    private TMP_Text WeaponWood;

    [SerializeField]
    private TMP_Text ArmorGold;
    [SerializeField]
    private TMP_Text ArmorStone;
    [SerializeField]
    private TMP_Text ArmorWood;


    private int weapongold;
    private int weaponwood;
    private int weaponstone;
    private int armorgold;
    private int armorwood;
    private int armorstone;

    [SerializeField]
    private GameObject upgradePart;

    /// <summary>
    /// Check if we can upgrade
    /// </summary>
    /// <param name="value"></param>
    public void CanUpgradeObserver(bool value)
    {
        upgradePart.SetActive(value);


        upgradePart.transform.Find("WeaponIcon").Find("ErrorRessources").gameObject.SetActive(false);
        upgradePart.transform.Find("WeaponIcon").Find("ErrorLevel").gameObject.SetActive(false);
        upgradePart.transform.Find("ArmorIcon").Find("ErrorRessources").gameObject.SetActive(false);
        upgradePart.transform.Find("ArmorIcon").Find("ErrorLevel").gameObject.SetActive(false);

        Weapon charWeapon = transform.GetComponent<CharacterSlotScript>().CurrentCharacter.GetComponent<CharacterImageSlotScript>().Character.Weapon;
        Armor charArmor = transform.GetComponent<CharacterSlotScript>().CurrentCharacter.GetComponent<CharacterImageSlotScript>().Character.Armor;

        WeaponIcon.sprite = charWeapon.Image;
        ArmorIcon.sprite = charArmor.Image;

        weapongold = charWeapon.Level * 34;
        weaponstone = charWeapon.Level * 4;
        weaponwood = charWeapon.Level * 8;

        armorgold = charArmor.Level * 34;
        armorstone = charArmor.Level * 4;
        armorwood = charArmor.Level * 8;

        WeaponGold.text = weapongold.ToString();
        WeaponStone.text = weaponstone.ToString();
        WeaponWood.text = weaponwood.ToString() ;

        ArmorGold.text = armorgold.ToString();
        ArmorStone.text = armorstone.ToString();
        ArmorWood.text = armorwood.ToString();
        
        upgradePart.transform.Find("WeaponIcon").Find("UpgradeWeaponButton").GetComponent<Button>().interactable = CanBuy("Weapon");
        upgradePart.transform.Find("ArmorIcon").Find("UpgradeArmorButton").GetComponent<Button>().interactable = CanBuy("Armor");

    }

    /// <summary>
    /// Verify if can the player can buy the given equipement
    /// </summary>
    /// <param name="equipement">the wanted equipement</param>
    /// <returns>the result of the condition</returns>
    private bool CanBuy(string equipement)
    {

        bool result = false;

        int currentGold = GameObject.Find("ATHVillage").transform.Find("Background").Find("GoldSlider").GetComponent<MaterialSliderScript>().GetValue();
        int currentWood = GameObject.Find("ATHVillage").transform.Find("Background").Find("WoodSlider").GetComponent<MaterialSliderScript>().GetValue();
        int currentStone = GameObject.Find("ATHVillage").transform.Find("Background").Find("StoneSlider").GetComponent<MaterialSliderScript>().GetValue();


        switch (equipement)
        {
            case "Weapon": 
                {
                    if (transform.GetComponent<CharacterSlotScript>().CurrentCharacter.GetComponent<CharacterImageSlotScript>().Character.Weapon.Level < (Village.Gunsmith.Level * 5))
                    {
                        if (currentGold >= weapongold && currentStone >= weaponstone && currentWood >= weaponwood)
                        {
                            result = true;
                        }
                        else
                        {
                            upgradePart.transform.Find("WeaponIcon").Find("ErrorRessources").gameObject.SetActive(true);
                        }
                    }
                    else
                    {
                        upgradePart.transform.Find("WeaponIcon").Find("ErrorRessources").gameObject.SetActive(false);
                        upgradePart.transform.Find("WeaponIcon").Find("ErrorLevel").gameObject.SetActive(true);
                    }

                }
                break;
            case "Armor":
                {
                    if (transform.GetComponent<CharacterSlotScript>().CurrentCharacter.GetComponent<CharacterImageSlotScript>().Character.Armor.Level < (Village.Gunsmith.Level * 5))
                    {
                        if (currentGold >= armorgold && currentStone >= armorstone && currentWood >= armorwood)
                        {
                            result = true;
                        }
                        else
                        {
                            upgradePart.transform.Find("ArmorIcon").Find("ErrorRessources").gameObject.SetActive(true);
                        }
                    }
                    else
                    {
                        upgradePart.transform.Find("ArmorIcon").Find("ErrorRessources").gameObject.SetActive(false);
                        upgradePart.transform.Find("ArmorIcon").Find("ErrorLevel").gameObject.SetActive(true);
                    }

                }
                break;

        }
        return result;

    }

    /// <summary>
    /// Buy an upgrade
    /// </summary>
    /// <param name="equipement">the wanted equipement</param>
    private void Buy(string equipement)
    {
        switch (equipement)
        {
            case "Weapon": VillageManager.DeleteRessources(weaponwood, weaponstone, weapongold); break;
            case "Armor": VillageManager.DeleteRessources(armorwood, armorstone, armorgold); break;
        }
    }


    /// <summary>
    /// Upgrade a weapon or armor
    /// </summary>
    /// <param name="buttonName">the button name</param>
    public void HasBought(string buttonName)
    {
        switch (buttonName)
        {
            case "UpgradeWeaponButton":
                {
                    Buy("Weapon");
                    transform.GetComponent<CharacterSlotScript>().CurrentCharacter.GetComponent<CharacterImageSlotScript>().Character.Weapon.AddLevel();
                    CanUpgradeObserver(true);
                }
                break;
            case "UpgradeArmorButton": 
                {
                    Buy("Armor");
                    transform.GetComponent<CharacterSlotScript>().CurrentCharacter.GetComponent<CharacterImageSlotScript>().Character.Armor.AddLevel();
                    CanUpgradeObserver(true);
                } 
                break;
        }
    }

}
