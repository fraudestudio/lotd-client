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

        /// <summary>
        /// The time max for the healing
        /// </summary>
        public static int TimeMaxHealing { get => timeMaxHealing; }

        /// <summary>
        /// Create the healerhut
        /// </summary>
        /// <param name="level">the level of the healerhut</param>
        /// <param name="baseWood">the base wood for upgrade</param>
        /// <param name="baseStone">the base stone for upgrade</param>
        /// <param name="baseGold">the base gold for upgrade</param>
        /// <param name="woodAugmentation">the wood augmentation for upgrade</param>
        /// <param name="stoneAugmentation">the stone augmentation for upgrade</param>
        /// <param name="goldAugmentation">the gold augmentation for upgrade</param>
        /// <param name="inConstruction">is the building in construction</param>
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
