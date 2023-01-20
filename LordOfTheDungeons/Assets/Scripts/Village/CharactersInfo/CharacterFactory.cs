using Assets.Scripts.Village;
using Assets.Scripts.Village.CharactersInfo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class CharacterFactory : MonoBehaviour
{
    [Serializable]
    // Force unity to serialize a list
    public class Icon : ImageIconList<Sprite> { }

    [Serializable]
    // Force unity to serialize a list
    public class ImageChar : ImageCharList<Sprite> { }
    public Icon Icons;
    public ImageChar ImageCharacter;

    [SerializeField]
    private GameObject PreFabCharacter;

    /// <summary>
    /// Fabric of characters
    /// </summary>
    /// <param name="image">id of the image of the character</param>
    /// <param name="name">the name of the character</param>
    /// <param name="race">the race of the character</param>
    /// <param name="level">the level of the character</param>
    /// <param name="life">the life of the character</param>
    /// <param name="maxLife">the max life of the character</param>
    /// <param name="PA_MAX">the point action max of the character</param>
    /// <param name="PM_MAX">the movement point of the character</param>
    /// <param name="classe">the class of the character</param>
    /// <param name="weapon">the weapon of the character</param>
    /// <param name="armor">the armor of the character</param>
    /// <returns>the created character</returns>
    public GameObject CreateCharacter(int image, string name, string race, int level, int life, int maxLife, int PA_MAX, int PM_MAX, string classe, Weapon weapon, Armor armor)
    {
        GameObject character = Instantiate(PreFabCharacter);

        character.GetComponent<CharacterImageSlotScript>().Character = new Character(ImageCharacter.CharacterImage[image], Icons.CharacterIcon[image],name,race,level,life,maxLife,PA_MAX,PM_MAX,classe, weapon,armor);

        character.GetComponent<CharacterImageSlotScript>().ChangeSprite(Icons.CharacterIcon[image]);


        return character;
    }

}
