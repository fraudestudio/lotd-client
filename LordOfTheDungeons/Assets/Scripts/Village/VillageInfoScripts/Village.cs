using Assets.Scripts.Village;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village
{
    private string name;
    private string faction;
    private int owner;

    public static HealerHut HealerHut;
    public static TrainingCamp TrainingCamp;
    public static Tavern Tavern;
    public static Warehouse Warehouse;
    public static Gunsmith Gunsmith;

    public Village(string villageName, string villageFaction, int villageOwner)
    {
        name = villageName;
        faction = villageFaction;
        owner = villageOwner;

        Dictionary<string,int> bat = Server.GetLevelVillage();

        // Ici requête BDD pour la taverne
        Tavern = new Tavern(bat["TAVERN"], 5, 4, 1, 20, 30, 4, Server.GetInConstructionVillage("TAVERN"), 40);
        Gunsmith = new Gunsmith(bat["GUNSMITH"], 4, 6, 4,10,10,30, Server.GetInConstructionVillage("GUNSMITH"));
        Warehouse = new Warehouse(bat["WAREHOUSE"], 1,1,5,20,20,20, Server.GetInConstructionVillage("WAREHOUSE"), 300,300,300);
        TrainingCamp = new TrainingCamp(bat["TRAINING_CAMP"], 3, 5, 20, 10, 20, 30, Server.GetInConstructionVillage("TRAINING_CAMP"), false, 0);
        HealerHut = new HealerHut(bat["HEALER_HUT"], 2,8,5,5,10,20, Server.GetInConstructionVillage("HEALER_HUT"));

    }

    public string Name { get => name; set => name = value; }
    public string Faction { get => faction; set => faction = value; }
    public int Owner { get => owner; set => owner = value; }
}
