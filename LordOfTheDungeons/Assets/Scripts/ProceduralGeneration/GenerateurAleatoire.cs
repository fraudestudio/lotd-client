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
    // Singleton
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

    // the global seed
    private int seedGlobale;
    // the local seed
    private int seedLocale;

    private GenerateurAleatoire()
    {

    }

    /// <summary>
    /// Set the global seed of the generator
    /// </summary>
    /// <param name="seed">the seed</param>
    public void SetSeedGlobal(int seed)
    {
        Instance.seedGlobale = seed;
        Instance.random = new Random(Instance.seedGlobale);
        Instance.seedLocale = 0;
    }

    /// <summary>
    /// Set the local seed of the generator
    /// </summary>
    /// <param name="seed">the local seed</param>
    public void SetSeedLocale(int seed)
    {
        Instance.random = new Random(Instance.seedGlobale + seed);
    }

    /// <summary>
    /// Generate a number
    /// </summary>
    /// <returns>the number generated</returns>
    public int Next()
    {
        return Instance.random.Next();
       
    }

    /// <summary>
    /// Generate a number with max 
    /// </summary>
    /// <param name="borneMax">the max number</param>
    /// <returns>the generated number</returns>
    public int Next(int borneMax)
    {
        return Instance.random.Next(borneMax);
    }

    /// <summary>
    /// Generate a new coordinates base on the map length
    /// </summary>
    /// <returns>the generated coordinates</returns>
    public Coordonnees NextCoordonnees()
    {
        return new Coordonnees(Next(Carte.Taille), Next(Carte.Taille));
    }
}
