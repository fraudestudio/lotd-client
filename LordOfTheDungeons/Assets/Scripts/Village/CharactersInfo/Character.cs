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

        private int pA_MAX;

        private int pM_MAX;

        private string classe;

        private Weapon weapon;

        private Armor armor;
        public Weapon Weapon { get => weapon; set => weapon = value; }
        public Armor Armor { get => armor; set => armor = value; }
        public Sprite Image { get => image; }
        public string Race { get => race; }
        public string Name { get => name; }
        public int Level { get => level; set => level = value; }
        public int Life { get => life; set => life = value; }
        public int MaxLife { get => maxLife; }
        public int PA_MAX { get => pA_MAX; set => pA_MAX = value; }
        public int PM_MAX { get => pM_MAX; set => pM_MAX = value; }
        public string Classe { get => classe; set => classe = value; }

        public Character(Sprite image, Sprite icon, string name, string race, int level, int life, int maxLife, int PA_MAX, int PM_MAX, string classe, Weapon weapon)
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
            this.classe = classe;
            this.weapon = weapon;
        }


        public void AddLevel(int value)
        {
            level += value;
        }

        public void SetMaxHealt()
        {
            life = maxLife;
        }

    }
}
