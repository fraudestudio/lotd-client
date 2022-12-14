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

        // Ici requête BDD pour la taverne
        Tavern = new Tavern(2, 5, 4, 1, 20, 30, 4, false, 40);
        Gunsmith = new Gunsmith(1, 4, 6, 4,10,10,30, false);
        Warehouse = new Warehouse(2,1,1,5,20,20,20,true,300,300,300);
        TrainingCamp = new TrainingCamp(1, 3, 5, 20, 10, 20, 30, false, false, 0);
        HealerHut = new HealerHut(1,2,8,5,5,10,20, false);

    }

    public string Name { get => name; set => name = value; }
    public string Faction { get => faction; set => faction = value; }
    public int Owner { get => owner; set => owner = value; }
}
