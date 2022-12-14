using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Village
{
    public class Weapon
    {

        private Sprite image;

        private string name;

        private int level;

        private int power;

        public Weapon(Sprite image, string name, int level, int power)
        {
            this.image = image;
            this.name = name;
            this.level = level;
            this.power = power;
        }

        public Sprite Image { get => image; }
        public string Name { get => name; set => name = value; }
        public int Level { get => level; set => level = value; }
        public int Power { get => power; set => power = value; }
    }
}
