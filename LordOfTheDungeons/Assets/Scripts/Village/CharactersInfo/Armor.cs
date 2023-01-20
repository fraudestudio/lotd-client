using UnityEngine;

namespace Assets.Scripts.Village
{
    public class Armor
    {
        private string name;

        private Sprite image;

        private int level;

        private int resistance;


        /// <summary>
        /// Name of the armor
        /// </summary>
        public string Name { get => name; set => name = value; }
        /// <summary>
        /// Image of the armor
        /// </summary>
        public Sprite Image { get => image; set => image = value; }
        /// <summary>
        /// Level of the armor
        /// </summary>
        public int Level { get => level; set => level = value; }
        /// <summary>
        /// Resistance of the armor
        /// </summary>
        public int Resistance { get => resistance; set => resistance = value; }

        /// <summary>
        /// Construct the armor object
        /// </summary>
        /// <param name="image">image of the armor</param>
        /// <param name="name">name of the armor</param>
        /// <param name="level">level of the armor</param>
        /// <param name="resistance">resistance of the armor</param>
        public Armor(Sprite image, string name, int level, int resistance)
        {
            this.image = image;
            this.name = name;
            this.level = level;
            this.resistance = resistance;
        }

        /// <summary>
        /// Add a level to the armor
        /// </summary>
        public void AddLevel()
        {
            level++;
        }

        /// <summary>
        /// Get the resistance of the armor
        /// </summary>
        /// <returns>the value of the resistance</returns>
        public int GetTotalResistance()
        {
            return level * resistance;
        }

    }
}
