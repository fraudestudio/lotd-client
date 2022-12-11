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

        private int life;

        private int maxLife;

        private int PA_MAX;

        private int PM_MAX;

        private Weapon weapon;

        private Armor armor;
        public Weapon Weapon { get => weapon; set => weapon = value; }
        public Armor Armor { get => armor; set => armor = value; }

        public Character(Sprite image, Sprite icon, string name, string race, int level, int life, int maxLife, int PA_MAX, int PM_MAX)
        {
            this.image = image;
            this.iconImage = icon;
            this.name = name;
            this.race = race;
            this.level = level;
            this.life = life;
            this.maxLife = maxLife;
            this.PA_MAX = PA_MAX;
            this.PM_MAX = PM_MAX;
        }


    }
}
