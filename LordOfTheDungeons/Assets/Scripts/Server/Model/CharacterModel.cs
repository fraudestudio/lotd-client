using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel : MonoBehaviour
{
    private int id;
    private int level;
    private int pV;
    private bool possede;
    private int pV_MAX;
    private string nameChar = "";
    private int pA_MAX;
    private int pM_MAX;
    private int iMG;
    private int iD_VILLAGE;
    private string cLASSE = "";
    private string rACE = "";
    private int iD_EQUIPEMENT;

    /// <summary>
    /// Id of the character
    /// </summary>
    public int Id { get => id; set => id = value; }
    /// <summary>
    /// Level of the character
    /// </summary>
    public int Level { get => level; set => level = value; }
    /// <summary>
    /// PV of the character
    /// </summary>
    public int PV { get => pV; set => pV = value; }
    /// <summary>
    /// Tells if the character is owned
    /// </summary>
    public bool Possede { get => possede; set => possede = value; }
    /// <summary>
    /// PV MAX of the character
    /// </summary>
    public int PV_MAX { get => pV_MAX; set => pV_MAX = value; }
    /// <summary>
    /// Name of the character
    /// </summary>
    public string Name { get => nameChar; set => nameChar = value; }
    /// <summary>
    /// Point of action max
    /// </summary>
    public int PA_MAX { get => pA_MAX; set => pA_MAX = value; }
    /// <summary>
    /// Point of movement max
    /// </summary>
    public int PM_MAX { get => pM_MAX; set => pM_MAX = value; }
    /// <summary>
    /// Id of the image of the character
    /// </summary>
    public int IMG { get => iMG; set => iMG = value; }
    /// <summary>
    /// Village id of the character
    /// </summary>
    public int ID_VILLAGE { get => iD_VILLAGE; set => iD_VILLAGE = value; }
    /// <summary>
    /// Class of the character
    /// </summary>
    public string CLASSE { get => cLASSE; set => cLASSE = value; }
    /// <summary>
    /// Race of the character
    /// </summary>
    public string RACE { get => rACE; set => rACE = value; }
    /// <summary>
    /// Id of the equipement of the character
    /// </summary>
    public int ID_EQUIPEMENT { get => iD_EQUIPEMENT; set => iD_EQUIPEMENT = value; }
}
