using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Village
{
    public class HealerHut : Building
    {

        private static int timeMaxHealing = 86400;

        public static int TimeMaxHealing { get => timeMaxHealing; }

        public HealerHut(int level, int baseWood, int baseStone, int baseGold, int woodAugmentation, int stoneAugmentation, int goldAugmentation, bool inConstruction)
        {
            this.Level = level;
            this.BaseWoodNeeded = baseWood;
            this.BaseStoneNeeded = baseStone;
            this.BaseGoldNeeded = baseGold;
            this.WoodModification = woodAugmentation;
            this.StoneModification = stoneAugmentation;
            this.GoldModification = goldAugmentation;
            this.InConstruction = inConstruction;
        }
    }
}
