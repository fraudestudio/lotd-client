using Assets.Scripts.Village;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tavern : Building
{


    private static int timeMaxBeforeNewRecruit = 86400;
    public static int TimeMaxBeforeNewRecruit { get => timeMaxBeforeNewRecruit; }

    private int timeBeforeNewRecruit;
    public int TimeBeforeNewRecruit { get => timeBeforeNewRecruit; set => timeBeforeNewRecruit = value; }

    public Tavern(int level, int baseWood, int baseStone, int baseIron, int woodAugmentation, int stoneAugmentation, int ironAugmentation, bool inConstruction, int timeBeforeNewRecruit)
    {
        this.Level = level;
        this.BaseWoodNeeded = baseWood;
        this.BaseStoneNeeded = baseStone;
        this.BaseIronNeeded = baseIron;
        this.WoodModification = woodAugmentation;
        this.StoneModification = stoneAugmentation;
        this.IronModification = ironAugmentation;
        this.InConstruction = inConstruction;
        this.TimeBeforeNewRecruit = timeBeforeNewRecruit;
    }


}
