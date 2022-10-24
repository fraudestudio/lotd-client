using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Universe
{
    private string universeName;
    private string universePassword;
    private string owner;

    private List<string> users = new List<string>();

    private List<Village> villages = new List<Village>();

    private bool password;

    public Universe(string name, string password, string universeOwner)
    {
        universeName = name;
        universePassword = password;
        owner = universeOwner;
        if (password == "")
        {
            this.password = false;
        }
        else
        {
            this.password = true;
        }
    }

    public bool Password
    {
        get 
        { 
            return password;
        }
        set
        {
            password = value;
        }
    }

    public string UniverseName { get => universeName; set => universeName = value; }
    public string UniversePassword { get => universePassword; set => universePassword = value; }
    public string Owner { get => owner; set => owner = value; }
    public List<string> Users { get => users; set => users = value; }
    public List<Village> Villages { get => villages; set => villages = value; }

    public string GetMajorFaction()
    {
        int ihuman = 0;
        int idwarf = 0;
        int ielve = 0;
        int ihobbit = 0;
        string major = "";
        foreach (Village v in Villages)
        {
            switch (v.Faction)
            {
                case "human": ihuman++; break;
                case "dwarf": idwarf++; break;
                case "elve": ielve++; break;
                case "hobbit": ihobbit++; break;
            }



        }

        if (ihuman > idwarf && ihuman > ielve && ihuman > ihobbit)
        {
            major = "humain";
        }
        else if (idwarf > ihuman && idwarf > ielve && idwarf > ihobbit)
        {
            major = "dwarf";
        }
        else if (ielve > idwarf && ielve > ihuman && ielve > ihobbit)
        {
            major = "elf";
        }
        else if (ihobbit > idwarf && ihobbit > ihuman && ihobbit > ielve)
        {
            major = "hobbit";
        }
        else
        {
            major = "NA";
        }
        return major;
    }

    public Village GetVillage(string user)
    {
        Village village = null;
        foreach (Village v in villages)
        {
            if (v.Owner == user)
            {
                village = v;
                Debug.Log("FEUR");
            }
        }

        return village;
    }
}
