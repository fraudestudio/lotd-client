using Assets.Scripts.Village;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingBehaviourScript : MonoBehaviour
{

    /// <summary>
    /// Pour savoir sur les bâtiments peuvent être cliquer ou non
    /// </summary>
    private static bool canBeClicked = true;

    public static bool CanBeClicked { get => canBeClicked; set => canBeClicked = value; }

    public GameObject slotPreFab;
    public GameObject testCharacterPreFab;


    private void Start()
    {
        switch (transform.name)
        {
            case "Tavern": InitTavern(); break;
            case "Gunsmith": InitGunsmith(); break;
            case "Warehouse": InitWarehouse(); break;
            case "TrainingCamp": InitTrainingCamp(); break;
        }
    }



    #region Start and stop building

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
                case "Tavern": StartMenuTavern(); break;
                case "Gunsmith": StartGunsmith(); break;
                case "Warehouse": StartWarehouse(); break;
                case "TrainingCamp": StartTrainingCamp(); break;
                case "VillageCenter": StartVillageCenter(); break;
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
            case "Gunsmith": StopGunsmith(); break;
            case "Warehouse": StopWarehouse(); break;
            case "TrainingCamp": StopTrainingCamp(); break;
        }
    }

    #endregion

    #region VillageCenter

    private void StartVillageCenter()
    {
        GameObject.Find("BuildingText").transform.Find("CanvasVillageCenter").GetComponent<CanvasGroup>().alpha = 0;
        Init(GameObject.Find("VillageCenterMenu"));
    }

    #endregion

    #region TrainingCamp

    private void InitTrainingCamp()
    {
        if (Village.TrainingCamp.InFormation)
        {
            GameObject t = GameObject.Find("TrainingCampMenu").transform.Find("TimeSliderTrainingCamp").gameObject;
            t.SetActive(true);
            t.GetComponent<TimeLeftSliderScript>().Init(Village.TrainingCamp.TimeBeforeTrainingIsFinished, TrainingCamp.TimeMaxTraining);
        }
        else
        {

            for (int i = 0; i < 1 + Village.TrainingCamp.Level; i++)
            {
                GameObject d = Instantiate(slotPreFab);
                d.name = "Slot_" + i;
                d.GetComponent<CharacterSlotScript>().SetType(SlotType.TRAINEE);
                d.transform.SetParent(GameObject.Find("TraineeTitle").transform.Find("TraineesLayout"));
                d.transform.localScale = new Vector2(1f, 1f);
            }

            if (!GameObject.Find("InstructorTitle").transform.Find("CharacterSlot").GetComponent<CharacterSlotScript>().SlotIsEmpty)
            {
                for (int i = 0; i < GameObject.Find("TraineeTitle").transform.Find("TraineesLayout").childCount; i++)
                {
                    if (GameObject.Find("TraineeTitle").transform.Find("TraineesLayout").GetChild(i).GetComponent<CharacterSlotScript>().SlotIsEmpty)
                    {
                        GameObject.Find("ButtonTrain").SetActive(true);
                    }
                }
            }
        }



    }

    private void StartTrainingCamp()
    {
        GameObject.Find("BuildingText").transform.Find("CanvasTrainingCamp").GetComponent<CanvasGroup>().alpha = 0;
        GameObject m = GameObject.Find("BuildingMenu");
        GameObject w = GameObject.Find("TrainingCampMenu");
        Init(m);
        Init(w);
        m.GetComponent<ModifyMenuScript>().InitMenu("TrainingCamp", "Endroit où entrainez les aventuriers");

        for (int i = 0; i < w.transform.Find("TraineeTitle").Find("TraineesLayout").childCount; i++)
        {
            w.transform.Find("TraineeTitle").Find("TraineesLayout").GetChild(i).gameObject.SetActive(true);
            if (!w.transform.Find("TraineeTitle").Find("TraineesLayout").GetChild(i).GetComponent<CharacterSlotScript>().SlotIsEmpty)
            {
                CharacterSlotNotAllowedScript.AddSlot(w.transform.Find("TraineeTitle").Find("TraineesLayout").GetChild(i).gameObject);
            }
        }
        if (!GameObject.Find("InstructorTitle").transform.Find("CharacterSlot").GetComponent<CharacterSlotScript>().SlotIsEmpty)
        {
            CharacterSlotNotAllowedScript.AddSlot(w.transform.Find("InstructorTitle").Find("CharacterSlot").gameObject);
        }
    }
    
    private void StopTrainingCamp()
    {
        for (int i = 0; i < GameObject.Find("TrainingCampMenu").transform.Find("TraineeTitle").Find("TraineesLayout").childCount; i++)
        {
            CharacterSlotNotAllowedScript.RemoveSlot(GameObject.Find("TrainingCampMenu").transform.Find("TraineeTitle").Find("TraineesLayout").GetChild(i).gameObject);
            GameObject.Find("TrainingCampMenu").transform.Find("TraineeTitle").Find("TraineesLayout").GetChild(i).gameObject.SetActive(false);
        }
        if (!GameObject.Find("InstructorTitle").transform.Find("CharacterSlot").GetComponent<CharacterSlotScript>().SlotIsEmpty)
        {
            CharacterSlotNotAllowedScript.RemoveSlot(GameObject.Find("TrainingCampMenu").transform.Find("InstructorTitle").Find("CharacterSlot").gameObject);
        }
    }

    #endregion

    #region Warehouse
    private void InitWarehouse()
    {
        GameObject.Find("WarehouseMenu").transform.Find("Icon&Capacity").Find("IronIcon").Find("MaxIronCapacity").GetComponent<TMP_Text>().text = (Village.Warehouse.BaseMaxIron * Village.Warehouse.Level).ToString();
        GameObject.Find("WarehouseMenu").transform.Find("Icon&Capacity").Find("WoodIcon").Find("MaxWoodCapacity").GetComponent<TMP_Text>().text = (Village.Warehouse.BaseMaxWood * Village.Warehouse.Level).ToString();
        GameObject.Find("WarehouseMenu").transform.Find("Icon&Capacity").Find("StoneIcon").Find("MaxStoneCapacity").GetComponent<TMP_Text>().text = (Village.Warehouse.BaseMaxStone * Village.Warehouse.Level).ToString();

        GameObject.Find("ATHVillage").transform.Find("Background").Find("IronSlider").GetComponent<Slider>().maxValue = Village.Warehouse.BaseMaxIron * Village.Warehouse.Level;
        GameObject.Find("ATHVillage").transform.Find("Background").Find("WoodSlider").GetComponent<Slider>().maxValue = Village.Warehouse.BaseMaxWood * Village.Warehouse.Level;
        GameObject.Find("ATHVillage").transform.Find("Background").Find("StoneSlider").GetComponent<Slider>().maxValue = Village.Warehouse.BaseMaxStone * Village.Warehouse.Level;
    }

    private void StartWarehouse()
    {
        GameObject.Find("BuildingText").transform.Find("CanvasWarehouse").GetComponent<CanvasGroup>().alpha = 0;
        GameObject m = GameObject.Find("BuildingMenu");
        GameObject w = GameObject.Find("WarehouseMenu");
        Init(m);
        Init(w);
        m.GetComponent<ModifyMenuScript>().InitMenu("Warehouse", "Endroit où les ressources sont stockés");
    }

    private void StopWarehouse()
    {

    }
    #endregion

    #region Gunsmith
    /// <summary>
    /// Initialise les éléments de l'armurier
    /// </summary>
    private void InitGunsmith()
    {
        
    }

    private void StartGunsmith()
    {
        GameObject.Find("BuildingText").transform.Find("CanvasGunsmith").GetComponent<CanvasGroup>().alpha = 0;
        GameObject m = GameObject.Find("BuildingMenu");
        GameObject g = GameObject.Find("GunsmithMenu");
        Init(m);
        Init(g);
        m.GetComponent<ModifyMenuScript>().InitMenu("Gunsmith", "Endroit où améliorer l'équipement des héros");
    }

    /// <summary>
    /// Arrête les éléments de l'Armurier
    /// </summary>
    private void StopGunsmith()
    {

    }
    #endregion

    #region Tavern
    /// <summary>
    /// Initialise les éléments de la taverne 
    /// </summary>
    private void InitTavern()
    {
        GameObject t = GameObject.Find("TavernMenu");
        t.transform.Find("TimeSliderTavern").GetComponent<TimeLeftSliderScript>().Init(Village.Tavern.TimeBeforeNewRecruit, Tavern.TimeMaxBeforeNewRecruit);
        Transform heoresAvaiable = t.transform.Find("HeroesAvaiable");

        for (int i = 0; i < Village.Tavern.Level + 1; i++)
        {
            GameObject d = Instantiate(slotPreFab);
            d.name = "Slot_" + i;
            d.GetComponent<CharacterSlotScript>().SetType(SlotType.BUILDING);
            d.transform.SetParent(heoresAvaiable);
            d.transform.localScale = new Vector2(1f, 1f);
            GameObject c = Instantiate(testCharacterPreFab);
            d.GetComponent<CharacterSlotScript>().AddChar(c);
        }

    }


    /// <summary>
    /// Initialise le menu de la taverne
    /// </summary>
    private void StartMenuTavern()
    {
        GameObject.Find("BuildingText").transform.Find("CanvasTavern").GetComponent<CanvasGroup>().alpha = 0;
        GameObject m = GameObject.Find("BuildingMenu");
        GameObject t = GameObject.Find("TavernMenu");
        Init(t);
        for (int i = 0; i < GameObject.Find("TavernMenu").transform.Find("HeroesAvaiable").childCount; i++)
        {
            GameObject.Find("TavernMenu").transform.Find("HeroesAvaiable").GetChild(i).gameObject.SetActive(true);
            if (!GameObject.Find("TavernMenu").transform.Find("HeroesAvaiable").GetChild(i).GetComponent<CharacterSlotScript>().SlotIsEmpty)
            {
                CharacterSlotNotAllowedScript.AddSlot(GameObject.Find("TavernMenu").transform.Find("HeroesAvaiable").GetChild(i).gameObject);
            }
        }
        Init(m);
        m.GetComponent<ModifyMenuScript>().InitMenu("Tavern","Endroit ou les héros peuvent être recutés");
    }


    /// <summary>
    /// Arrête les élements de la tavern que l'on souhaite
    /// </summary>
    public void StopTavern()
    {
        for (int i = 0; i < GameObject.Find("TavernMenu").transform.Find("HeroesAvaiable").childCount; i++)
        {
            CharacterSlotNotAllowedScript.RemoveSlot(GameObject.Find("TavernMenu").transform.Find("HeroesAvaiable").GetChild(i).gameObject);
            GameObject.Find("TavernMenu").transform.Find("HeroesAvaiable").GetChild(i).gameObject.SetActive(false);
        }
        //GameObject.Find("TavernMenu").transform.Find("TimeSliderTavern").GetComponent<TimeLeftSliderScript>().Stop();
    }

    #endregion



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
                case "Warehouse": GameObject.Find("BuildingText").transform.Find("CanvasWarehouse").GetComponent<CanvasGroup>().alpha = 1; break;
                case "TrainingCamp": GameObject.Find("BuildingText").transform.Find("CanvasTrainingCamp").GetComponent<CanvasGroup>().alpha = 1; break;
                case "VillageCenter": GameObject.Find("BuildingText").transform.Find("CanvasVillageCenter").GetComponent<CanvasGroup>().alpha = 1; break;
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
                case "Warehouse": GameObject.Find("BuildingText").transform.Find("CanvasWarehouse").GetComponent<CanvasGroup>().alpha = 0; break;
                case "TrainingCamp": GameObject.Find("BuildingText").transform.Find("CanvasTrainingCamp").GetComponent<CanvasGroup>().alpha = 0; break;
                case "VillageCenter": GameObject.Find("BuildingText").transform.Find("CanvasVillageCenter").GetComponent<CanvasGroup>().alpha = 0; break;
            }
        }

    }
}
