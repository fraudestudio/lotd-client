using Assets.Scripts.Server;
using System;
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
    // Prefab of the button
    [SerializeField]
    private GameObject buttonPreFab;

    // Content of the scroll view my universe
    private Transform contentMyUniverse;
    // Content of the scool view my global universe
    private Transform contentGlobalUniverse;

    // Button prefab
    [SerializeField]
    private GameObject createUniverseButton;


    // List of the buttons 
    List<GameObject> buttons = new List<GameObject>();
    // List of the buttons in the myUniverse feild
    List<GameObject> myUniverseButton = new List<GameObject>();

    void Start()
    {
        contentMyUniverse = GameObject.Find("MyUniverse").transform.Find("Viewport").transform.Find("Content");
        contentGlobalUniverse = GameObject.Find("GlobalUniverse").transform.Find("Viewport").transform.Find("Content");
    }



    private void DestroyMyUniverseButton()
    {
        foreach (GameObject button in myUniverseButton)
        {
            Destroy(button);
        }

        myUniverseButton.Clear();
    }

    private void DestroyUniverseButton()
    {
        foreach (GameObject button in buttons)
        {
            Destroy(button);
        }
        buttons.Clear();
    }


    /// <summary>
    /// Get the universes and displayed it 
    /// </summary>
    public void GetUniverses()
    {
        bool stop = false;

        DestroyUniverseButton();

        DestroyMyUniverseButton();

        if (Server.UserHasUniverse())
        {
            createUniverseButton.SetActive(false);
        }
        else
        {
            createUniverseButton.SetActive(true);
        }

        while (!stop)
        {
            foreach (UniverseInfo u in Server.GetAllUniverses())
            {
                GameObject button = Instantiate(buttonPreFab);
                button.transform.Find("T").GetComponent<TMP_Text>().text = u.Name;
                button.transform.SetParent(contentGlobalUniverse);
                button.GetComponent<UniverseButtonScript>().UniverseName = u.Name;
                button.GetComponent<UniverseButtonScript>().Id = Convert.ToInt32(u.Id);
                button.GetComponent<UniverseButtonScript>().Password = u.HasPassword;

                if (u.HasPassword)
                {
                    button.transform.Find("Padlock").gameObject.SetActive(true);
                }

                buttons.Add(button);
            }

            UniverseInfo userUniverse = Server.GetUserUniverse();

            if (userUniverse.Id != null)
            {
                GameObject button = Instantiate(buttonPreFab);
                button.transform.Find("T").GetComponent<TMP_Text>().text = userUniverse.Name;
                button.transform.SetParent(contentMyUniverse);
                button.GetComponent<UniverseButtonScript>().UniverseName = userUniverse.Name;
                button.GetComponent<UniverseButtonScript>().Id = Convert.ToInt32(userUniverse.Id);
                button.GetComponent<UniverseButtonScript>().Password = false;
                myUniverseButton.Add(button);
            }


            foreach (UniverseInfo u in Server.GetUserJoinedUniverses())
            {
                GameObject button = Instantiate(buttonPreFab);
                button.transform.Find("T").GetComponent<TMP_Text>().text = u.Name;
                button.transform.SetParent(contentMyUniverse);
                button.GetComponent<UniverseButtonScript>().UniverseName = u.Name;
                button.GetComponent<UniverseButtonScript>().Id = Convert.ToInt32(u.Id);
                button.GetComponent<UniverseButtonScript>().Password = false;
                myUniverseButton.Add(button);
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
