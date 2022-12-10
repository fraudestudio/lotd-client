using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Village
{
    public class Character
    {

        private Image image; 

        private Image iconImage; 

        private string race;

        private string name;

        private int level;

        private int strengh;

        private int life;

        private Weapon weapon;

        private Armor armor;

        public Character(Image image, Image icon, string name, string race, int level, int strengh, int life)
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
