using Assets.Scripts.Village;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Tavern : Building
{


    private static int timeMaxBeforeNewRecruit = 86400;
    public static int TimeMaxBeforeNewRecruit { get => timeMaxBeforeNewRecruit; }

    private int timeBeforeNewRecruit;
    public int TimeBeforeNewRecruit { get => timeBeforeNewRecruit; set => timeBeforeNewRecruit = value; }

    public Tavern(int level, int baseWood, int baseStone, int baseGold, int woodAugmentation, int stoneAugmentation, int goldAugmentation, bool inConstruction, int timeBeforeNewRecruit)
    {
        this.Level = level;
        this.BaseWoodNeeded = baseWood;
        this.BaseStoneNeeded = baseStone;
        this.BaseGoldNeeded = baseGold;
        this.WoodModification = woodAugmentation;
        this.StoneModification = stoneAugmentation;
        this.GoldModification = goldAugmentation;
        this.InConstruction = inConstruction;
        this.TimeBeforeNewRecruit = timeBeforeNewRecruit;
    }


}
