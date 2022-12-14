using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Village
{
    public class Armor
    {
        private string name;

        private Sprite image;

        private int level;

        private int resistance;

        public Armor(Sprite image, string name, int level, int resistance)
        {
            this.image = image;
            this.name = name;
            this.level = level;
            this.resistance = resistance;
        }


        public void AddLevel()
        {
            level++;
        }

        public int GetTotalResistance()
        {
            return level * resistance;
        }

        public string Name { get => name; set => name = value; }
        public Sprite Image { get => image; set => image = value; }
        public int Level { get => level; set => level = value; }
        public int Resistance { get => resistance; set => resistance = value; }
    }
}
