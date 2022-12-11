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
    public class ImageChar : ImageCharList<Sprite> { }
    public Icon Icons;
    public ImageChar ImageCharacter;


    public GameObject PreFabCharacter;


    public GameObject CreateCharacter(int image, string name, string race, int level, int strengh, int life)
    {
        GameObject character = Instantiate(PreFabCharacter);

        character.GetComponent<CharacterImageSlotScript>().Character = new Character(ImageCharacter.CharacterImage[image], Icons.CharacterIcon[image],name,race,level,strengh,life);

        return null;
    }

}
