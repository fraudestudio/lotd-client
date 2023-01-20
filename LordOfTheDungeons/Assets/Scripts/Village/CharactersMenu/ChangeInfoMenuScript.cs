using Assets.Scripts.Village;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeInfoMenuScript : MonoBehaviour
{
    [SerializeField]
    private Image Image;
    [SerializeField]
    private TMP_Text Name;
    [SerializeField]
    private TMP_Text Race;
    [SerializeField]
    private TMP_Text Niveau;
    [SerializeField]
    private TMP_Text Class;
    [SerializeField]
    private TMP_Text PA;
    [SerializeField]
    private TMP_Text PM;
    [SerializeField]
    private TMP_Text Life;


    [SerializeField]
    public Image WeaponImage;
    [SerializeField]
    public TMP_Text WeaponName;
    [SerializeField]
    public TMP_Text WeaponLevel;
    [SerializeField]
    public TMP_Text WeaponPower;


    private Image ArmorImage;
    private TMP_Text ArmorName;
    private TMP_Text ArmorLevel;
    private TMP_Text ArmorResistance;

    /// <summary>
    /// Change the info on the character menu
    /// </summary>
    /// <param name="c">the given character</param>
    public void ChangeInfoMenu (Character c)
    {
        Image.sprite = c.Image;
        Name.text = c.Name;
        Race.text = "Race : " + c.Race;
        Niveau.text = "Niveau : " + c.Level;
        PA.text = "PA MAX : " + c.PA_MAX;
        PM.text = "PM MAX : " + c.PM_MAX;
        Life.text = "Life : " + c.Life +"/"+c.MaxLife;
        Class.text = "Classe : " + c.Classe;

        WeaponImage.sprite = c.Weapon.Image;
        WeaponName.text = c.Weapon.Name;
        WeaponLevel.text = "Level : " + c.Weapon.Level;
        WeaponPower.text = "Puissance : " + c.Weapon.GetTotalPower();


        ArmorImage.sprite = c.Armor.Image;
        ArmorName.text = c.Armor.Name;
        ArmorLevel.text = "Level : " + c.Armor.Level;
        ArmorResistance.text = "Résistance  : " + c.Armor.GetTotalResistance();
    }
}
