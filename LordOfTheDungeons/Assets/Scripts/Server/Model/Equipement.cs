using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipement : MonoBehaviour
{
    // Start is called before the first frame update
    private int iD_EQUIPEMENT;
    private int nIVEAU_ARME;
    private int nIVEAU_ARMURE;
    private int iMG_ARME;
    private int iMG_ARMURE;
    private int bONUS_ARME;
    private int bONUS_ARMURE;

    /// <summary>
    /// Id of the equipement
    /// </summary>
    public int ID_EQUIPEMENT { get => iD_EQUIPEMENT; set => iD_EQUIPEMENT = value; }
    /// <summary>
    /// Level of the weapon
    /// </summary>
    public int NIVEAU_ARME { get => nIVEAU_ARME; set => nIVEAU_ARME = value; }
    /// <summary>
    /// Level of the armor
    /// </summary>
    public int NIVEAU_ARMURE { get => nIVEAU_ARMURE; set => nIVEAU_ARMURE = value; }
    /// <summary>
    /// Id of the image of the weapon
    /// </summary>
    public int IMG_ARME { get => iMG_ARME; set => iMG_ARME = value; }
    /// <summary>
    /// Id of the image of the armor
    /// </summary>
    public int IMG_ARMURE { get => iMG_ARMURE; set => iMG_ARMURE = value; }
    /// <summary>
    /// Bonus of the weapon 
    /// </summary>
    public int BONUS_ARME { get => bONUS_ARME; set => bONUS_ARME = value; }
    /// <summary>
    /// Bonus of the armor
    /// </summary>
    public int BONUS_ARMURE { get => bONUS_ARMURE; set => bONUS_ARMURE = value; }
}
