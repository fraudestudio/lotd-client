using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Village
{
    public class Character
    {

        private Sprite image; 

        private Sprite iconImage; 

        private string race;

        private string name;

        private int level;

        private int strengh;

        private int life;

        private Weapon weapon;

        private Armor armor;

        public Character(Sprite image, Sprite icon, string name, string race, int level, int strengh, int life)
        {
            this.image = image;
            this.iconImage = icon;
            this.name = name;
            this.race = race;
            this.level = level;
            this.strengh = strengh;
            this.life = life;
        }
    }
}
