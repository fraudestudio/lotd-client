using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetNameScript : MonoBehaviour
{
    /// <summary>
    /// Get the name of the player
    /// </summary>
    void Start()
    {
        GameObject.Find("Menu").transform.Find("connected").GetComponent<TMP_Text>().text = "Connexion : " + Server.Name;
    }
}
