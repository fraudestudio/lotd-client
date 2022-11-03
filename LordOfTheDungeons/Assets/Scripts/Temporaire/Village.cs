using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village
{
    private string name;
    private string faction;
    private string owner;

    public Village(string villageName, string villageFaction, string villageOwner)
    {
        name = villageName;
        faction = villageFaction;
        owner = villageOwner;
    }

    public string Name { get => name; set => name = value; }
    public string Faction { get => faction; set => faction = value; }
    public string Owner { get => owner; set => owner = value; }
}
