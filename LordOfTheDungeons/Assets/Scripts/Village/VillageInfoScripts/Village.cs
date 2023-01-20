using Assets.Scripts.Village;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village
{
    private string name;
    private string faction;
    private int owner;

    private static HealerHut healerHut;
    private static TrainingCamp trainingCamp;
    private static Tavern tavern;
    private static Warehouse warehouse;
    private static Gunsmith gunsmith;

    /// <summary>
    /// Name of the village
    /// </summary>
    public string Name { get => name; set => name = value; }
    /// <summary>
    /// Faction of the village
    /// </summary>
    public string Faction { get => faction; set => faction = value; }
    /// <summary>
    /// Owner of the village
    /// </summary>
    public int Owner { get => owner; set => owner = value; }
    /// <summary>
    /// Healerhtu of the village
    /// </summary>
    public static HealerHut HealerHut { get => healerHut; set => healerHut = value; }
    /// <summary>
    /// Training camp of the village
    /// </summary>
    public static TrainingCamp TrainingCamp { get => trainingCamp; set => trainingCamp = value; }
    /// <summary>
    /// Tavern of the village
    /// </summary>
    public static Tavern Tavern { get => tavern; set => tavern = value; }
    /// <summary>
    /// Warehouse of the village
    /// </summary>
    public static Warehouse Warehouse { get => warehouse; set => warehouse = value; }
    /// <summary>
    /// Gunsmith of the village
    /// </summary>
    public static Gunsmith Gunsmith { get => gunsmith; set => gunsmith = value; }

    /// <summary>
    /// Create the village
    /// </summary>
    /// <param name="villageName">the village name</param>
    /// <param name="villageFaction">the village faction</param>
    /// <param name="villageOwner">the village owner</param>
    public Village(string villageName, string villageFaction, int villageOwner)
    {
        name = villageName;
        faction = villageFaction;
        owner = villageOwner;

        Dictionary<string,int> bat = Server.GetLevelVillage();

        Debug.Log(Server.GetCurrentVillage());
        int starttime = Server.GetStartTimeTaverne(Server.GetCurrentVillage());
        Debug.Log(starttime);
        Tavern = new Tavern(bat["TAVERN"], 5, 4, 1, 20, 30, 4, Server.GetInConstructionVillage("TAVERN"),starttime);
        Gunsmith = new Gunsmith(bat["GUNSMITH"], 4, 6, 4,10,10,30, Server.GetInConstructionVillage("GUNSMITH"));
        Warehouse = new Warehouse(bat["WAREHOUSE"], 1,1,5,20,20,20, Server.GetInConstructionVillage("WAREHOUSE"), 300,300,300);
        TrainingCamp = new TrainingCamp(bat["TRAINING_CAMP"], 3, 5, 20, 10, 20, 30, Server.GetInConstructionVillage("TRAINING_CAMP"), false, 0);
        HealerHut = new HealerHut(bat["HEALER_HUT"], 2,8,5,5,10,20, Server.GetInConstructionVillage("HEALER_HUT"));

    }


}
