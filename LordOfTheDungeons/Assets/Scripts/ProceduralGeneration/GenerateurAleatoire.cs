using Assets.Scripts.ProceduralGeneration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using Random = System.Random;

public class GenerateurAleatoire
{
    private static GenerateurAleatoire instance;
    public static GenerateurAleatoire Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GenerateurAleatoire();
            }
            return instance;
        }
    }

    private Random random;

    private int seedGlobale;
    private int seedLocale;

    private GenerateurAleatoire()
    {

    }


    public void SetSeedGlobal(int seed)
    {
        //MD5 md5Hasher = MD5.Create();
        //var hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(seed));
        Instance.seedGlobale = seed;
        Instance.random = new Random(Instance.seedGlobale);
        Instance.seedLocale = 0;
    }

    public void SetSeedLocale(object o)
    {
        Instance.seedLocale = o.GetHashCode();
        Instance.random = new Random(Instance.seedGlobale + Instance.seedLocale);
    }

    public int Next()
    {
        int n = Instance.random.Next();
        UnityEngine.Debug.Log(String.Format("next: {0}", n));
        return n;
    }

    public int Next(int borneMax)
    {
        int n = Instance.random.Next(borneMax);
        UnityEngine.Debug.Log(String.Format("next (max {1}): {0}", n, borneMax));
        return n;
    }

    public Coordonnees NextCoordonnees()
    {
        return new Coordonnees(Next(Carte.Taille), Next(Carte.Taille));
    }
}
