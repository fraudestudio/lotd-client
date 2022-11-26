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
    private TrainingCamp trainingCamp;
    public static Tavern Tavern;
    private Warehouse warehouse;

    public Village(string villageName, string villageFaction, string villageOwner)
    {
        name = villageName;
        faction = villageFaction;
        owner = villageOwner;

        // Ici requête BDD pour la taverne
        Tavern = new Tavern(1, 50, 40, 1, 20, 30, 4, false, 300);

    }

    public string Name { get => name; set => name = value; }
    public string Faction { get => faction; set => faction = value; }
    public string Owner { get => owner; set => owner = value; }
}
