using Assets.Scripts.Village;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village
{
    private string name;
    private string faction;
    private string owner;

    private Hut hut;
    public static TrainingCamp TrainingCamp;
    public static Tavern Tavern;
    public static Warehouse Warehouse;
    public static Gunsmith Gunsmith;

    public Village(string villageName, string villageFaction, string villageOwner)
    {
        name = villageName;
        faction = villageFaction;
        owner = villageOwner;

        // Ici requête BDD pour la taverne
        Tavern = new Tavern(1, 50, 40, 1, 20, 30, 4, false, 300);
        Gunsmith = new Gunsmith(1, 45, 60, 40,10,10,30, false);
        Warehouse = new Warehouse(1,10,10,5,20,20,20,false,300,300,300);
        TrainingCamp = new TrainingCamp(1, 30, 50, 20, 10, 20, 30, false, false, 0);

    }

    public string Name { get => name; set => name = value; }
    public string Faction { get => faction; set => faction = value; }
    public string Owner { get => owner; set => owner = value; }
}
