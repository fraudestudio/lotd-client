using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TemporaryScript
{

    public static Dictionary<string, string> users;

    public static string currentUser;

    private static List<Universe> universes;

    public static List<Universe> Universes { get => universes; }


    public static void Init()
    {
        users = new Dictionary<string, string>();
        universes = new List<Universe>();


        users.Add("bob", "123");
        users.Add("alice", "567");
        users.Add("toto", "tata");


        universes.Add(new Universe("universbob", "123", "bob"));
        AddVillage(universes[0], "VILLAGE DE BOB", "human", "bob");
        universes.Add(new Universe("universalice", "", "alice"));
        AddVillage(universes[1], "VILLAGE DE ALICE", "dwarf", "alice");
        universes.Add(new Universe("test", "456", "alice"));
        AddVillage(universes[2], "test", "elve", "alice");


        /*universes.Add(new Universe("zear", "", "shinka"));
        universes.Add(new Universe("qdsf", "", "shinka"));
        universes.Add(new Universe("sq", "", "shinka"));
        universes.Add(new Universe("azeraf", "", "shinka"));
        universes.Add(new Universe("aze", "", "shinka"));*/
    }

    public static bool VerifyUser(string id, string mdp)
    {
        bool b = false;
        if (users.ContainsKey(id))
        {
            if (users[id] == mdp)
            {
                b = true;
            }
        }

        return b;
    }

    public static Universe GetUniverse(string name)
    {
        Universe universe = null;
        foreach (Universe u in Universes)
        {
            if (u.UniverseName == name)
            {
                universe = u;
                break;
            }
        }
        return universe;
    }


    public static void AddVillage(Universe u, string name, string faction, string owner)
    {
        u.Villages.Add(new Village(name, faction, owner));
        u.Users.Add(owner);
    }
}
