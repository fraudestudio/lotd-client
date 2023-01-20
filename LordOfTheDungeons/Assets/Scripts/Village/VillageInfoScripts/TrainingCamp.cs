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

        /// <summary>
        /// Create the training camp
        /// </summary>
        /// <param name="level">the level of the training camp</param>
        /// <param name="baseWood">the base wood for upgrade</param>
        /// <param name="baseStone">the base stone for upgrade</param>
        /// <param name="baseGold">the base gold for upgrade</param>
        /// <param name="woodAugmentation">the wood augmentation for upgrade</param>
        /// <param name="stoneAugmentation">the stone augmentation for upgrade</param>
        /// <param name="goldAugmentation">the gold augmentation for upgrade</param>
        /// <param name="inConstruction">is the building in construction</param>
        /// <param name="inFormation">is the building in formation</param>
        /// <param name="timeBeforeTrainingIsFinished">time before the training is finished</param>
        public TrainingCamp(int level, int baseWood, int baseStone, int baseGold, int woodAugmentation, int stoneAugmentation, int goldAugmentation, bool inConstruction, bool inFormation, int timeBeforeTrainingIsFinished)
        {
            this.Level = level;
            this.BaseWoodNeeded = baseWood;
            this.BaseStoneNeeded = baseStone;
            this.BaseGoldNeeded = baseGold;
            this.WoodModification = woodAugmentation;
            this.StoneModification = stoneAugmentation;
            this.GoldModification = goldAugmentation;
            this.InConstruction = inConstruction;
            this.InFormation = inFormation;
            this.TimeBeforeTrainingIsFinished = timeBeforeTrainingIsFinished;
        }


    }
}
