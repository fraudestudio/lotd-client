using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Village
{
    public class Building
    {
        private int level;
        private int baseWoodNeeded;
        private int baseStoneNeeded;
        private int baseGoldNeeded;
        private int woodModification;
        private int stoneModification;
        private int goldModification;
        private bool inConstruction;

        /// <summary>
        /// The level of the building
        /// </summary>
        public int Level { get => level; set => level = value; }
        /// <summary>
        /// The base wood needed for upgrade
        /// </summary>
        public int BaseWoodNeeded { get => baseWoodNeeded; set => baseWoodNeeded = value; }
        /// <summary>
        /// The base stone needed for upgrade
        /// </summary>
        public int BaseStoneNeeded { get => baseStoneNeeded; set => baseStoneNeeded = value; }
        /// <summary>
        /// The base gold needed for upgrade
        /// </summary>
        public int BaseGoldNeeded { get => baseGoldNeeded; set => baseGoldNeeded = value; }
        /// <summary>
        /// The wood modification for the upgrade
        /// </summary>
        public int WoodModification { get => woodModification; set => woodModification = value; }
        /// <summary>
        /// The stone modification for upgrade
        /// </summary>
        public int StoneModification { get => stoneModification; set => stoneModification = value; }
        /// <summary>
        /// The gold mofification for upgrade
        /// </summary>
        public int GoldModification { get => goldModification; set => goldModification = value; }
        /// <summary>
        /// Check if the building is in construction
        /// </summary>
        public bool InConstruction { get => inConstruction; set => inConstruction = value; }
    }
}
