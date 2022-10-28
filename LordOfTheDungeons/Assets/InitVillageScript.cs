using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InitVillageScript : MonoBehaviour
{

    public TMP_Text nameVillage;
    public TMP_Text owner;
    public TMP_Text universe;
    public TMP_Text faction;


    // Start is called before the first frame update
    void Start()
    {
        Universe u = SaveUniverseScript.Universe;

        Village v = u.GetVillage(TemporaryScript.currentUser);

        nameVillage.text = "Village : " + v.Name;
        owner.text = "Proprio : " + v.Owner;
        universe.text = "Univer : " + u.UniverseName;
        faction.text = "Faction : " + v.Faction;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
