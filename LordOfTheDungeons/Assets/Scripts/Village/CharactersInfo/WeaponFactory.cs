using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static CharacterFactory;

namespace Assets.Scripts.Village.CharactersInfo
{
    public class WeaponFactory : MonoBehaviour
    {
        [Serializable]
        public class Images : WeaponIconList<Sprite> { }
        public Images Icons;


        public Weapon CreateWeapon(int image, string name, int level, int power)
        {
            Weapon w = new Weapon(Icons.WeaponIcons[image], name, level, power);
            return w;
        }
    }
}
