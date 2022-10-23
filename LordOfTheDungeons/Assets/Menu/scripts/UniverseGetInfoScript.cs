using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Xml;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Assertions.Must;
using static UnityEngine.EventSystems.EventTrigger;

public class UniverseGetInfoScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject buttonPreFab;

    private Transform contentMyUniverse;
    private Transform contentGlobalUniverse;


    List<GameObject> buttons = new List<GameObject>();
    List<GameObject> myUniverseButton = new List<GameObject>();

    void Start()
    {
        contentMyUniverse = GameObject.Find("MyUniverse").transform.Find("Viewport").transform.Find("Content");
        contentGlobalUniverse = GameObject.Find("GlobalUniverse").transform.Find("Viewport").transform.Find("Content");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GetUniverses()
    {
        bool stop = false;
        
        foreach (GameObject button in buttons)
        {
            Destroy(button);
        }

        buttons.Clear();

        foreach (GameObject button in myUniverseButton)
        {
            Destroy(button);
        }

        buttons.Clear();

        while (!stop)
        {
            foreach (KeyValuePair<string, string> entry in TemporaryScript.Universes)
            {
                GameObject button = Instantiate(buttonPreFab);
                button.transform.Find("T").GetComponent<TMP_Text>().text = entry.Key;
                button.transform.SetParent(contentGlobalUniverse);
                button.GetComponent<UniverseButtonScript>().VillageName = entry.Key;

                foreach (KeyValuePair<string, List<string>> entry2 in TemporaryScript.UniverseOwner)
                {
                    if (entry2.Value.Contains(entry.Key))
                    {
                        button.GetComponent<UniverseButtonScript>().Owner = entry2.Key;
                    }
                }

                if (entry.Value != "")
                {
                    button.GetComponent<UniverseButtonScript>().Password = true;
                    button.GetComponent<UniverseButtonScript>().PasswordString = entry.Value;
                }
                else
                {
                    button.GetComponent<UniverseButtonScript>().Password = false;
                }
                buttons.Add(button);
            }



            foreach(GameObject button in buttons)
            {
                if (button.GetComponent<UniverseButtonScript>().Owner == TemporaryScript.currentUser)
                {
                    GameObject b = Instantiate(button);
                    b.transform.SetParent(contentMyUniverse);
                    b.transform.localScale = new Vector2(0.8333334f, 0.8333334f);
                    myUniverseButton.Add(b);
                }
            }

            stop = true;
        }

        GameObject.Find("WaitingServer").GetComponent<WaitingForServerScript>().StopAnim();

    }

    public void SearchUniverses(string search)
    {
        if (search != "")
        {
            foreach (GameObject button in buttons)
            {
                if (!button.GetComponent<UniverseButtonScript>().VillageName.Contains(search))
                {
                    button.SetActive(false);
                }
            }
        }
        else
        {
            foreach (GameObject button in buttons)
            {
                button.SetActive(true);
            }
        }

    }
}
