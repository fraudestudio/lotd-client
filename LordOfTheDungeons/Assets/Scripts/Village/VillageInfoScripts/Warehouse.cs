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

        public int BaseMaxWood { get => baseMaxWood; set => baseMaxWood = value; }
        public int BaseMaxIron { get => baseMaxIron; set => baseMaxIron = value; }
        public int BaseMaxStone { get => baseMaxStone; set => baseMaxStone = value; }

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
