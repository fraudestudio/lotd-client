using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Village
{
    public class Weapon
    {

        private Image image;

        private string name;

        private int level;

        private int attackPoint;

        public Weapon(Image image, string name, int level, int attackPoint)
        {
            this.image = image;
            this.name = name;
            this.level = level;
            this.attackPoint = attackPoint;
        }

        public Image Image { get => image; set => image = value; }
        public string Name { get => name; set => name = value; }
        public int Level { get => level; set => level = value; }
        public int AttackPoint { get => attackPoint; set => attackPoint = value; }
    }
}
