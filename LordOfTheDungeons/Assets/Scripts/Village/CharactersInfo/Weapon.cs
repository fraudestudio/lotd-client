using UnityEngine;

namespace Assets.Scripts.Village
{
    public class Weapon
    {
        
        private Sprite image;

        private string name;

        private int level;

        private int power;

        /// <summary>
        /// Image of the weapon
        /// </summary>
        public Sprite Image { get => image; }
        /// <summary>
        /// Name of the weapon
        /// </summary>
        public string Name { get => name; set => name = value; }
        /// <summary>
        /// Level of the weapon
        /// </summary>
        public int Level { get => level; set => level = value; }
        /// <summary>
        /// Power of the weapon
        /// </summary>
        public int Power { get => power; set => power = value; }

        /// <summary>
        /// Create a weaopon
        /// </summary>
        /// <param name="image">image of the weapon</param>
        /// <param name="name">name of the weapon</param>
        /// <param name="level">level of the weapon</param>
        /// <param name="power">power of the weapon</param>
        public Weapon(Sprite image, string name, int level, int power)
        {
            this.image = image;
            this.name = name;
            this.level = level;
            this.power = power;
        }

        /// <summary>
        /// Add a level to the weapon
        /// </summary>
        public void AddLevel()
        {
            level++;
        }

        /// <summary>
        /// Get the total power of the weapon
        /// </summary>
        /// <returns>the total power</returns>
        public int GetTotalPower()
        {
            return level * power;
        }
    }
}
