using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class connexionTextScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TMP_Text>().text += TemporaryScript.currentUser;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
