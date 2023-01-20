using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Village
{
    public class Warehouse : Building
    {

        private int baseMaxWood;
        private int baseMaxIron;
        private int baseMaxStone;

        /// <summary>
        /// The base max wood
        /// </summary>
        public int BaseMaxWood { get => baseMaxWood; set => baseMaxWood = value; }
        /// <summary>
        /// The base max gold
        /// </summary>
        public int BaseMaxIron { get => baseMaxIron; set => baseMaxIron = value; }
        /// <summary>
        /// The base max stone
        /// </summary>
        public int BaseMaxStone { get => baseMaxStone; set => baseMaxStone = value; }


        /// <summary>
        /// Create the warehouse
        /// </summary>
        /// <param name="level">the level of the warehouse</param>
        /// <param name="baseWood">the base wood for upgrade</param>
        /// <param name="baseStone">the base stone for upgrade</param>
        /// <param name="baseGold">the base gold for upgrade</param>
        /// <param name="woodAugmentation">the wood augmentation for upgrade</param>
        /// <param name="stoneAugmentation">the stone augmentation for upgrade</param>
        /// <param name="goldAugmentation">the gold augmentation for upgrade</param>
        /// <param name="inConstruction">is the building in construction</param>
        /// <param name="maxIron">the max capacity of the gold</param>
        /// <param name="maxStone">the max capacity of the stone</param>
        /// <param name="maxWood">the max capacity of the wood</param>
        public Warehouse(int level, int baseWood, int baseStone, int baseGold, int woodAugmentation, int stoneAugmentation, int goldAugmentation, bool inConstruction, int maxWood, int maxStone, int maxIron)
        {
            this.Level = Level;
            this.Level = level;
            this.BaseWoodNeeded = baseWood;
            this.BaseStoneNeeded = baseStone;
            this.BaseGoldNeeded = baseGold;
            this.WoodModification = woodAugmentation;
            this.StoneModification = stoneAugmentation;
            this.GoldModification = goldAugmentation;
            this.InConstruction = inConstruction;
            this.baseMaxWood = maxWood;
            this.baseMaxStone = maxStone;
            this.baseMaxIron = maxIron;
        }
    }
}
