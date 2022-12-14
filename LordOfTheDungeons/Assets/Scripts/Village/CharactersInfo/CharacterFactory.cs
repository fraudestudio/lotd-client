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
    public class Icon : ImageIconList<Sprite> { }

    [Serializable]
    public class ImageChar : ImageCharList<Sprite> { }
    public Icon Icons;
    public ImageChar ImageCharacter;


    public GameObject PreFabCharacter;


    public GameObject CreateCharacter(int image, string name, string race, int level, int life, int maxLife, int PA_MAX, int PM_MAX, string classe, Weapon weapon)
    {
        GameObject character = Instantiate(PreFabCharacter);

        character.GetComponent<CharacterImageSlotScript>().Character = new Character(ImageCharacter.CharacterImage[image], Icons.CharacterIcon[image],name,race,level,life,maxLife,PA_MAX,PM_MAX,classe, weapon);

        character.GetComponent<CharacterImageSlotScript>().ChangeSprite(Icons.CharacterIcon[image]);


        return character;
    }

}
