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


        public int Level { get => level; set => level = value; }
        public int BaseWoodNeeded { get => baseWoodNeeded; set => baseWoodNeeded = value; }
        public int BaseStoneNeeded { get => baseStoneNeeded; set => baseStoneNeeded = value; }
        public int BaseGoldNeeded { get => baseGoldNeeded; set => baseGoldNeeded = value; }
        public int WoodModification { get => woodModification; set => woodModification = value; }
        public int StoneModification { get => stoneModification; set => stoneModification = value; }
        public int GoldModification { get => goldModification; set => goldModification = value; }
        public bool InConstruction { get => inConstruction; set => inConstruction = value; }
    }
}
