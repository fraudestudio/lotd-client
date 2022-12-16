using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetNameScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Menu").transform.Find("connected").GetComponent<TMP_Text>().text = "Connexion : " + Server.Name;
    }
}
