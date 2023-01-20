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
        // Force unity to serialize a list
        public class Images : WeaponIconList<Sprite> { }
        public Images Icons;

        /// <summary>
        /// Fabric of weapon
        /// </summary>
        /// <param name="image">image id of the weapon</param>
        /// <param name="name">name of the weapon</param>
        /// <param name="level">level of the weapon</param>
        /// <param name="power">power of the weapon</param>
        /// <returns>the created weapon</returns>
        public Weapon CreateWeapon(int image, string name, int level, int power)
        {
            Weapon w = new Weapon(Icons.WeaponIcons[image], name, level, power);
            return w;
        }
    }
}
