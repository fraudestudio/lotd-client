using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBehaviourScript : MonoBehaviour
{

    /// <summary>
    /// Pour savoir sur les bâtiments peuvent être cliquer ou non
    /// </summary>
    private static bool canBeClicked = true;

    public static bool CanBeClicked { get => canBeClicked; set => canBeClicked = value; }

    public GameObject slotPreFab;


    /// <summary>
    /// Qaund on clique sur un bâtiment, on démarre le menu qu'on a besoin 
    /// </summary>
    private void OnMouseDown()
    {
        if (canBeClicked)
        {
            canBeClicked = false;
            Debug.Log(transform.name + "has been click");
            switch (transform.name)
            {
                case "Tavern":
                    {
                        StartMenuTavern();
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// Arrête les éléments du menu que l'on souhaite
    /// </summary>
    /// <param name="name"></param>
    public void StopBuilding(string name)
    {
        switch (name)
        {
            case "Tavern": StopTavern(); break;
        }
    }


    /// <summary>
    /// Initialise le menu de la taverne
    /// </summary>
    private void StartMenuTavern()
    {
        GameObject m = GameObject.Find("BuildingMenu");
        GameObject t = GameObject.Find("TavernMenu");
        Init(t);
        Init(m);
        m.GetComponent<ModifyMenuScript>().InitMenu("Tavern","Endroit ou les héros peuvent être recutés");

        t.transform.Find("TimeSliderTavern").GetComponent<TimeLeftSliderScript>().Init(Village.Tavern.TimeBeforeNewRecruit, Tavern.TimeMaxBeforeNewRecruit);
        Transform heoresAvaiable = t.transform.Find("HeroesAvaiable");

        for (int i = 0; i < Village.Tavern.Level + 1; i++)
        {
            GameObject d = Instantiate(slotPreFab);
            d.name = "Slot_" + i;
            d.GetComponent<CharacterSlotScript>().Type = SlotType.BUILDING;
            d.transform.SetParent(heoresAvaiable);
            d.transform.localScale = new Vector2(1f, 1f);
        }

    }


    /// <summary>
    /// Arrête les élements de la tavern que l'on souhaite
    /// </summary>
    public void StopTavern()
    {
        GameObject.Find("TavernMenu").transform.Find("TimeSliderTavern").GetComponent<TimeLeftSliderScript>().Stop();
    }


 

    /// <summary>
    /// Allume un menu souhaité
    /// </summary>
    /// <param name="g"></param>
    private void Init(GameObject g)
    {
        g.GetComponent<CanvasGroup>().alpha = 1;
        g.GetComponent<CanvasGroup>().blocksRaycasts = true;
        g.GetComponent<CanvasGroup>().interactable = true;
    }

    /// <summary>
    /// Affiche les informations du bâtiments
    /// </summary>
    private void OnMouseEnter()
    {
        if (canBeClicked)
        {
            Debug.Log(transform.name + " entered");
            switch (transform.name)
            {
                case "Tavern": GameObject.Find("BuildingText").transform.Find("CanvasTavern").GetComponent<CanvasGroup>().alpha = 1; break;
                case "HealerHut": GameObject.Find("BuildingText").transform.Find("CanvasHealerHut").GetComponent<CanvasGroup>().alpha = 1; break;
                case "Gunsmith": GameObject.Find("BuildingText").transform.Find("CanvasGunsmith").GetComponent<CanvasGroup>().alpha = 1; break;
                case "WareHouse": GameObject.Find("BuildingText").transform.Find("CanvasWarehouse").GetComponent<CanvasGroup>().alpha = 1; break;
                case "TrainingCamp": GameObject.Find("BuildingText").transform.Find("CanvasTrainingCamp").GetComponent<CanvasGroup>().alpha = 1; break;
            }
        }

    }

    /// <summary>
    /// Enlève les informations du bâtiments
    /// </summary>
    private void OnMouseExit()
    {
        if (canBeClicked)
        {
            Debug.Log(transform.name + " exited");
            switch (transform.name)
            {
                case "Tavern": GameObject.Find("BuildingText").transform.Find("CanvasTavern").GetComponent<CanvasGroup>().alpha = 0; break;
                case "HealerHut": GameObject.Find("BuildingText").transform.Find("CanvasHealerHut").GetComponent<CanvasGroup>().alpha = 0; break;
                case "Gunsmith": GameObject.Find("BuildingText").transform.Find("CanvasGunsmith").GetComponent<CanvasGroup>().alpha = 0; break;
                case "WareHouse": GameObject.Find("BuildingText").transform.Find("CanvasWarehouse").GetComponent<CanvasGroup>().alpha = 0; break;
                case "TrainingCamp": GameObject.Find("BuildingText").transform.Find("CanvasTrainingCamp").GetComponent<CanvasGroup>().alpha = 0; break;
            }
        }

    }
}
