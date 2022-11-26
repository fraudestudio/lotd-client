using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace Assets.Scripts.Village
{
    public class Gunsmith : Building
    {
        
        public Gunsmith(int level, int baseWood, int baseStone, int baseIron, int woodAugmentation, int stoneAugmentation, int ironAugmentation, bool inConstruction)
        {
            this.Level = Level;
            this.Level = level;
            this.BaseWoodNeeded = baseWood;
            this.BaseStoneNeeded = baseStone;
            this.BaseIronNeeded = baseIron;
            this.WoodModification = woodAugmentation;
            this.StoneModification = stoneAugmentation;
            this.IronModification = ironAugmentation;
            this.InConstruction = inConstruction;
        }

    }
}
