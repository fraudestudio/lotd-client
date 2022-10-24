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

    public GameObject createUniverseButton;


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
            foreach (Universe u in TemporaryScript.Universes)
            {
                GameObject button = Instantiate(buttonPreFab);
                button.transform.Find("T").GetComponent<TMP_Text>().text = u.UniverseName;
                button.transform.SetParent(contentGlobalUniverse);
                button.GetComponent<UniverseButtonScript>().UniverseName = u.UniverseName ;
                button.GetComponent<UniverseButtonScript>().Owner = u.Owner;
                button.GetComponent<UniverseButtonScript>().Password = u.Password;
                button.GetComponent<UniverseButtonScript>().PasswordString = u.UniversePassword;
                button.GetComponent<UniverseButtonScript>().Users = u.Users;
                buttons.Add(button);
            }




            foreach(GameObject button in buttons)
            {
                if (button.GetComponent<UniverseButtonScript>().Owner == TemporaryScript.currentUser || button.GetComponent<UniverseButtonScript>().Users.Contains(TemporaryScript.currentUser))
                {
                    GameObject b = Instantiate(button);
                    b.GetComponent<UniverseButtonScript>().UniverseName = button.GetComponent<UniverseButtonScript>().UniverseName;
                    b.GetComponent<UniverseButtonScript>().Owner = button.GetComponent<UniverseButtonScript>().Owner;
                    b.GetComponent<UniverseButtonScript>().PasswordString = button.GetComponent<UniverseButtonScript>().PasswordString;
                    b.GetComponent<UniverseButtonScript>().Users = button.GetComponent<UniverseButtonScript>().Users;
                    b.transform.SetParent(contentMyUniverse);
                    b.transform.localScale = new Vector2(0.8333334f, 0.8333334f);
                    b.GetComponent<UniverseButtonScript>().Password = false;
                    myUniverseButton.Add(b);
                }
            }

            stop = true;
        }

        foreach (GameObject button in myUniverseButton)
        {
            Debug.Log(button.GetComponent<UniverseButtonScript>().Owner);
            if (button.GetComponent<UniverseButtonScript>().Owner == TemporaryScript.currentUser)
            {
                createUniverseButton.SetActive(false);
            }
        }

        GameObject.Find("WaitingServer").GetComponent<WaitingForServerScript>().StopAnim();

    }

    public void SearchUniverses(string search)
    {
        if (search != "")
        {
            foreach (GameObject button in buttons)
            {
                if (!button.GetComponent<UniverseButtonScript>().UniverseName.Contains(search))
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
