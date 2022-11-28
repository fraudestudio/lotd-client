using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace Assets.Scripts.Village
{
    public class TrainingCamp : Building
    {


        private bool inFormation;
        private int timeBeforeTrainingIsFinished;
        private static int timeMaxTraining = 86400;

        public bool InFormation { get => inFormation; set => inFormation = value; }
        public int TimeBeforeTrainingIsFinished { get => timeBeforeTrainingIsFinished; set => timeBeforeTrainingIsFinished = value; }
        public static int TimeMaxTraining { get => timeMaxTraining; }
        public TrainingCamp(int level, int baseWood, int baseStone, int baseIron, int woodAugmentation, int stoneAugmentation, int ironAugmentation, bool inConstruction, bool inFormation, int timeBeforeTrainingIsFinished)
        {
            this.Level = level;
            this.BaseWoodNeeded = baseWood;
            this.BaseStoneNeeded = baseStone;
            this.BaseIronNeeded = baseIron;
            this.WoodModification = woodAugmentation;
            this.StoneModification = stoneAugmentation;
            this.IronModification = ironAugmentation;
            this.InConstruction = inConstruction;
            this.InFormation = inFormation;
            this.TimeBeforeTrainingIsFinished = timeBeforeTrainingIsFinished;
        }


    }
}
