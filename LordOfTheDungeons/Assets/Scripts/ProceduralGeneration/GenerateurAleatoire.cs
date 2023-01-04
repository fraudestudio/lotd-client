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
    private static GenerateurAleatoire instance = new GenerateurAleatoire();
    public static GenerateurAleatoire Instance { get => instance; set => instance = value; }

    private Random random;

    private int seedGlobale;
    private int seedLocale;

    private GenerateurAleatoire()
    {

    }


    public static void SetSeedGlobal(string seed)
    {
        MD5 md5Hasher = MD5.Create();
        var hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(seed));
        Instance.seedGlobale = BitConverter.ToInt32(hashed, 0);
        Instance.random = new Random(Instance.seedGlobale);
        Instance.seedLocale = 0;
    }

    public static void SetSeedLocale(object o)
    {
        Instance.seedLocale = o.GetHashCode();
        Instance.random = new Random(Instance.seedGlobale + Instance.seedLocale);
    }

    public static int Next()
    {
        return Instance.random.Next();
    }

    public static int Next(int borneMax)
    {
        return Instance.random.Next(borneMax);
    }

    public static Coordonnees NextCoordonnees()
    {
        return new Coordonnees(Next(Carte.Taille), Next(Carte.Taille));
    }
}
