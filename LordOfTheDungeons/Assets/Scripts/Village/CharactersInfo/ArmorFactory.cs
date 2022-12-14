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
        [Serializable]
        public class Images : ArmorIconList<Sprite> { }
        public Images Icons;


        public Armor CreateArmor(int image, string name, int level, int resistance)
        {
            Armor a = new Armor(Icons.ArmorIcons[image], name, level, resistance);
            return a;
        }
    }
}
