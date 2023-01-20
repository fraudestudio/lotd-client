using Assets.Scripts.Village;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Tavern : Building
{


    private static int timeMaxBeforeNewRecruit = 86400;
    /// <summary>
    /// The times max before the new recruits
    /// </summary>
    public static int TimeMaxBeforeNewRecruit { get => timeMaxBeforeNewRecruit; }

    private int timeBeforeNewRecruit;
    /// <summary>
    /// The time before the new recruits 
    /// </summary>
    public int TimeBeforeNewRecruit { get => timeBeforeNewRecruit; set => timeBeforeNewRecruit = value; }

    /// <summary>
    /// Create the Tavern
    /// </summary>
    /// <param name="level">the level of the tavern</param>
    /// <param name="baseWood">the base wood for upgrade</param>
    /// <param name="baseStone">the base stone for upgrade</param>
    /// <param name="baseGold">the base gold for upgrade</param>
    /// <param name="woodAugmentation">the wood augmentation for upgrade</param>
    /// <param name="stoneAugmentation">the stone augmentation for upgrade</param>
    /// <param name="goldAugmentation">the gold augmentation for upgrade</param>
    /// <param name="inConstruction">is the building in construction</param>
    /// <param name="timeBeforeNewRecruit">the time max before new recruits</param>
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
