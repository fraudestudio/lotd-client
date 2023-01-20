using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static CharacterFactory;

namespace Assets.Scripts.Village.CharactersInfo
{
    public class ArmorFactory : MonoBehaviour
    {

        // Force unity to display a list 
        [Serializable]
        public class Images : ArmorIconList<Sprite> { }
        public Images Icons;


        /// <summary>
        /// Fabric of armor
        /// </summary>
        /// <param name="image">the id of the image</param>
        /// <param name="name">the name of the armor</param>
        /// <param name="level">the level of the armor</param>
        /// <param name="resistance">the resistance of the armor</param>
        /// <returns></returns>
        public Armor CreateArmor(int image, string name, int level, int resistance)
        {
            Armor a = new Armor(Icons.ArmorIcons[image], name, level, resistance);
            return a;
        }
    }
}
