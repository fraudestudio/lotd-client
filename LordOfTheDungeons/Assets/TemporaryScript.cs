using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TemporaryScript
{

    public static Dictionary<string, string> users;

    public static string currentUser;

    private static Dictionary<string, string> universes;

    public static Dictionary<string, string> Universes { get => universes; }

    private static Dictionary<string, List<string>> universeOwner;

    public static Dictionary<string, List<string>> UniverseOwner { get => universeOwner; }


    public static void Init()
    {
        users = new Dictionary<string, string>();
        universes = new Dictionary<string, string>();
        universeOwner = new Dictionary<string, List<string>>();

        users.Add("bob", "123");
        users.Add("alice", "567");

        universes.Add("UniverBob", "123");
        universes.Add("UniverAlice", "");

        List<string> b = new List<string>();
        b.Add("UniverBob");
        universeOwner.Add("bob", b);
        
        List<string> s = new List<string>();
        s.Add("UniverAlice");
        universeOwner.Add("alice", s);

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
}
