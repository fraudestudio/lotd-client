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

        private string idEquipement;

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
        public string IdEquipement { get => idEquipement; set => idEquipement = value; }

        /// <summary>
        /// Create a character
        /// </summary>
        /// <param name="image">the image of the character</param>
        /// <param name="icon">the icon of the character</param>
        /// <param name="name">the name of the character</param>
        /// <param name="race">the race of the character</param>
        /// <param name="level">the level of the character</param>
        /// <param name="life">the life of the character</param>
        /// <param name="maxLife">the max life of the character</param>
        /// <param name="PA_MAX">the point of action max of the character</param>
        /// <param name="PM_MAX">the point of movement max of the character</param>
        /// <param name="classe">the class of the character</param>
        /// <param name="weapon">the weapon of the character</param>
        /// <param name="armor">the armor of the character</param>
        public Character(Sprite image, Sprite icon, string name, string race, int level, int life, int maxLife, int PA_MAX, int PM_MAX, string classe, Weapon weapon, Armor armor)
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
            this.armor = armor;
        }

        /// <summary>
        /// Add a given value to the player level
        /// </summary>
        /// <param name="value">the wanted value</param>
        public void AddLevel(int value)
        {
            level += value;
        }

        /// <summary>
        /// Set the max health of the player
        /// </summary>
        public void SetMaxHealt()
        {
            life = maxLife;
        }

    }
}
