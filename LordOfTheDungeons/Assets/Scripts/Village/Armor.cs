using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Village
{
    public class Armor
    {
        private string name;

        private Image image;

        private int level;

        private int armorPoint;

        public Armor(Image image, string name, int level, int armorPoint)
        {
            this.image = image;
            this.name = name;
            this.level = level;
            this.armorPoint = armorPoint;
        }

        public string Name { get => name; set => name = value; }
        public Image Image { get => image; set => image = value; }
        public int Level { get => level; set => level = value; }
        public int ArmorPoint { get => armorPoint; set => armorPoint = value; }
    }
}
