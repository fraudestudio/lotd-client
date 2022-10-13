using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UniverseGetInfoScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject buttonPreFab;

    private Transform contentMyUniverse;

    void Start()
    {
        contentMyUniverse = GameObject.Find("MyUniverse").transform.Find("Viewport").transform.Find("Content");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GetUniverses()
    {
        bool stop = false;
        while (!stop)
        {
            GameObject button = Instantiate(buttonPreFab);
            button.transform.Find("T").GetComponent<TMP_Text>().text = "FEURFEUR";
            button.transform.SetParent(contentMyUniverse);
            button.GetComponent<UniverseButtonScript>().VillageName = "FEURFEUR";
            button.GetComponent<UniverseButtonScript>().Password = false;

            stop = true;
        }

        GameObject.Find("WaitingServer").GetComponent<WaitingForServerScript>().StopAnim();

    }

    public void SearchUniverses(string search)
    {

    }
}
