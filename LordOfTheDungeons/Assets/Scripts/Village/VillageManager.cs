using Assets.Scripts.Village;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static CharacterFactory;

public class VillageManager : MonoBehaviour
{

    public GameObject villageCenterBlockPreFab;

    private static int countCharacterTrainingCamp = 0;
    private static int countCharacterTavern = 0;
    private static int countCharacterHealer = 0;

    private void Awake()
    {
        
        Village v = new Village("la coulante", "Hobbit",666) ;

        GameObject.Find("BuildingObjects").transform.Find("Gunsmith").Find("CanvasGunsmith").Find("GunsmithText").GetComponent<TMP_Text>().text = "Armurier\nNiveau " + Village.Gunsmith.Level;
        GameObject.Find("BuildingObjects").transform.Find("Tavern").Find("CanvasTavern").Find("TavernText").GetComponent<TMP_Text>().text = "Taverne\nNiveau " + Village.Tavern.Level;
        GameObject.Find("BuildingObjects").transform.Find("HealerHut").Find("CanvasHealerHut").Find("HealerHutText").GetComponent<TMP_Text>().text = "Hutte du guérisseur\nNiveau " + Village.HealerHut.Level;
        GameObject.Find("BuildingObjects").transform.Find("Warehouse").Find("CanvasWarehouse").Find("WarehouseText").GetComponent<TMP_Text>().text = "Entrepôt\nNiveau " + Village.Warehouse.Level;
        GameObject.Find("BuildingObjects").transform.Find("TrainingCamp").Find("CanvasTrainingCamp").Find("TrainingCampText").GetComponent<TMP_Text>().text = "Camp d'entraînement\nNiveau " + Village.TrainingCamp.Level;


        Transform villageCenterMenu = GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects");

        #region Tavern
        GameObject tavernButton = Instantiate(villageCenterBlockPreFab);
        tavernButton.name = "TavernButton";
        tavernButton.transform.SetParent(villageCenterMenu);
        tavernButton.transform.localScale = new Vector2(1, 1);
        tavernButton.transform.Find("BuildingTitle").GetComponent<TMP_Text>().text = "Taverne";

        if (Village.Tavern.InConstruction)
        {
            tavernButton.transform.Find("NotInConstructionTitle").gameObject.SetActive(false);
            tavernButton.transform.Find("TimeSliderCenter").gameObject.SetActive(true);
        }



        #endregion
        #region Gunsmith
        GameObject gunsmith = Instantiate(villageCenterBlockPreFab);
        gunsmith.name = "GunsmithButton";
        gunsmith.transform.SetParent(villageCenterMenu);
        gunsmith.transform.localScale = new Vector2(1, 1);
        gunsmith.transform.Find("BuildingTitle").GetComponent<TMP_Text>().text = "Armurier";

        if (Village.Gunsmith.InConstruction)
        {
            gunsmith.transform.Find("NotInConstructionTitle").gameObject.SetActive(false);
            gunsmith.transform.Find("TimeSliderCenter").gameObject.SetActive(true);
        }
        #endregion
        #region Warehouse
        GameObject warehouse = Instantiate(villageCenterBlockPreFab);
        warehouse.name = "WarehouseButton";
        warehouse.transform.SetParent(villageCenterMenu);
        warehouse.transform.localScale = new Vector2(1, 1);
        warehouse.transform.Find("BuildingTitle").GetComponent<TMP_Text>().text = "Entrepôt";
        warehouse.transform.Find("PeopleIcon").gameObject.SetActive(false);

        if (Village.Warehouse.InConstruction)
        {
            warehouse.transform.Find("NotInConstructionTitle").gameObject.SetActive(false);
            warehouse.transform.Find("TimeSliderCenter").gameObject.SetActive(true);
        }
        #endregion
        #region TrainingCamp
        GameObject trainingcamp = Instantiate(villageCenterBlockPreFab);
        trainingcamp.name = "TrainingCampButton";
        trainingcamp.transform.SetParent(villageCenterMenu);
        trainingcamp.transform.localScale = new Vector2(1, 1);
        trainingcamp.transform.Find("BuildingTitle").GetComponent<TMP_Text>().text = "Camp d'entraînement";
        trainingcamp.transform.Find("BuildingTitle").GetComponent<TMP_Text>().fontSize = 13;

        if (Village.TrainingCamp.InConstruction)
        {
            trainingcamp.transform.Find("NotInConstructionTitle").gameObject.SetActive(false);
            trainingcamp.transform.Find("TimeSliderCenter").gameObject.SetActive(true);
        }
        #endregion
        #region HealerHut
        GameObject healerhutButton = Instantiate(villageCenterBlockPreFab);
        healerhutButton.name = "HealerHutButton";
        healerhutButton.transform.SetParent(villageCenterMenu);
        healerhutButton.transform.localScale = new Vector2(1, 1);
        healerhutButton.transform.Find("BuildingTitle").GetComponent<TMP_Text>().text = "Hutte du guérisseur";
        healerhutButton.transform.Find("BuildingTitle").GetComponent<TMP_Text>().fontSize = 13;

        if (Village.HealerHut.InConstruction)
        {
            healerhutButton.transform.Find("NotInConstructionTitle").gameObject.SetActive(false);
            healerhutButton.transform.Find("TimeSliderCenter").gameObject.SetActive(true);
        }
        #endregion
    }

    private void Start()
    {
        #region Tavern

        Transform tavernButton = GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("TavernButton");

        if (Village.Tavern.InConstruction)
        {
           tavernButton.Find("TimeSliderCenter").gameObject.GetComponent<TimeLeftSliderScript>().Init(10, 86400);
        }


        countCharacterTavern = 0;

        for (int i = 0; i < GameObject.Find("TavernMenu").transform.Find("HeroesAvaiable").childCount; i++)
        {
            if (!GameObject.Find("TavernMenu").transform.Find("HeroesAvaiable").GetChild(i).GetComponent<CharacterSlotScript>().slotIsEmpty)
            {
                countCharacterTavern++;
            }
        }

        tavernButton.transform.Find("PeopleIcon").Find("CharacterCount").GetComponent<TMP_Text>().text = "x" + countCharacterTavern;
        tavernButton.transform.Find("PeopleIcon").Find("TimeSliderSpecific").gameObject.SetActive(true);
        tavernButton.transform.Find("PeopleIcon").Find("TimeSliderSpecific").GetComponent<TimeLeftSliderScript>().Init(Convert.ToInt32(GameObject.Find("TavernMenu").transform.Find("TimeSliderTavern").GetComponent<Slider>().value), Convert.ToInt32(GameObject.Find("TavernMenu").transform.Find("TimeSliderTavern").GetComponent<Slider>().maxValue));
        #endregion
        #region Gunsmith
        Transform gunsmith = GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("GunsmithButton");

        if (Village.Gunsmith.InConstruction)
        {
            gunsmith.Find("TimeSliderCenter").gameObject.GetComponent<TimeLeftSliderScript>().Init(10, 86400);
        }


        int countChar = 0;

        if (!GameObject.Find("GunsmithMenu").transform.Find("CharacterSlot").GetComponent<CharacterSlotScript>().SlotIsEmpty)
        {
            countChar = 1;
        }

        gunsmith.transform.Find("PeopleIcon").Find("CharacterCount").GetComponent<TMP_Text>().text = "x" + countChar;
        #endregion
        #region Warehouse
        Transform warehouse = GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("WarehouseButton");

        if (Village.Warehouse.InConstruction)
        {
            warehouse.Find("TimeSliderCenter").gameObject.GetComponent<TimeLeftSliderScript>().Init(10, 86400);
        }

        #endregion
        #region TrainingCamp
        Transform trainingCamp = GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("TrainingCampButton");

        if (Village.TrainingCamp.InConstruction)
        {
            trainingCamp.Find("TimeSliderCenter").gameObject.GetComponent<TimeLeftSliderScript>().Init(30, 86400);
        }


        for (int i = 0; i < GameObject.Find("TrainingCampMenu").transform.Find("TraineeTitle").Find("TraineesLayout").childCount; i++)
        {
            if (!GameObject.Find("TrainingCampMenu").transform.Find("TraineeTitle").Find("TraineesLayout").GetChild(i).GetComponent<CharacterSlotScript>().slotIsEmpty)
            {
                countCharacterTrainingCamp++;
            }
        }

        if (!GameObject.Find("TrainingCampMenu").transform.Find("InstructorTitle").Find("CharacterSlot").GetComponent<CharacterSlotScript>().slotIsEmpty)
        {
            countCharacterTrainingCamp++;
        }

        trainingCamp.transform.Find("PeopleIcon").Find("CharacterCount").GetComponent<TMP_Text>().text = "x" + countCharacterTrainingCamp;

        if (Village.TrainingCamp.InFormation)
        {
            trainingCamp.transform.Find("PeopleIcon").Find("TimeSliderSpecific").gameObject.SetActive(true);
            trainingCamp.transform.Find("PeopleIcon").Find("TimeSliderSpecific").GetComponent<TimeLeftSliderScript>().Init(Convert.ToInt32(GameObject.Find("TrainingCampMenu").transform.Find("TimeSliderTrainingCamp").GetComponent<Slider>().value), Convert.ToInt32(GameObject.Find("TrainingCampMenu").transform.Find("TimeSliderTrainingCamp").GetComponent<Slider>().maxValue));
        }

        #endregion
        #region HealerHut
        Transform healerHut = GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("HealerHutButton");

        if (Village.HealerHut.InConstruction)
        {
            healerHut.Find("TimeSliderCenter").gameObject.GetComponent<TimeLeftSliderScript>().Init(20, 86400);
        }


        for (int i = 0; i < GameObject.Find("HealerHutMenu").transform.Find("SlotsHealer").childCount; i++)
        {
            if (!GameObject.Find("HealerHutMenu").transform.Find("SlotsHealer").GetChild(i).Find("CharacterSlot").GetComponent<CharacterSlotScript>().SlotIsEmpty)
            {
                countCharacterHealer++;
            }
        }

        healerHut.transform.Find("PeopleIcon").Find("CharacterCount").GetComponent<TMP_Text>().text = "x" + countCharacterHealer;



        #endregion
    }


    public static void DeleteRessources(int wood, int stone, int gold)
    {
        GameObject.Find("ATHVillage").transform.Find("Background").Find("GoldSlider").GetComponent<MaterialSliderScript>().DeleteValue(gold);
        GameObject.Find("ATHVillage").transform.Find("Background").Find("StoneSlider").GetComponent<MaterialSliderScript>().DeleteValue(stone);
        GameObject.Find("ATHVillage").transform.Find("Background").Find("WoodSlider").GetComponent<MaterialSliderScript>().DeleteValue(wood);
    }

    private static void StopTimerConstructInMenu(string building)
    {
        GameObject tavernButton = GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find(building + "Button").gameObject;
        tavernButton.transform.Find("NotInConstructionTitle").gameObject.SetActive(true);
        tavernButton.transform.Find("TimeSliderCenter").gameObject.SetActive(false);
    }


    #region Observeurs


    public static void NotifyConstructStarted(string building)
    {
        switch (building)
        {
            case "Tavern":
                {
                    Village.Tavern.InConstruction = true;

                    GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("TavernButton").transform.Find("NotInConstructionTitle").gameObject.SetActive(false);
                    GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("TavernButton").transform.Find("TimeSliderCenter").gameObject.SetActive(true);
                    GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("TavernButton").transform.Find("TimeSliderCenter").GetComponent<TimeLeftSliderScript>().Init(86400, 86400);
                    GameObject.Find("BuildingMenu").GetComponent<ModifyMenuScript>().RefreshMenu("Tavern");
                }    
                break;
            case "Gunsmith":
                {
                    Village.Gunsmith.InConstruction = true;

                    GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("GunsmithButton").transform.Find("NotInConstructionTitle").gameObject.SetActive(false);
                    GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("GunsmithButton").transform.Find("TimeSliderCenter").gameObject.SetActive(true);
                    GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("GunsmithButton").transform.Find("TimeSliderCenter").GetComponent<TimeLeftSliderScript>().Init(86400, 86400);
                    GameObject.Find("BuildingMenu").GetComponent<ModifyMenuScript>().RefreshMenu("Gunsmith");
                }
                break;
            case "Warehouse":
                {
                    Village.Warehouse.InConstruction = true;

                    GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("WarehouseButton").transform.Find("NotInConstructionTitle").gameObject.SetActive(false);
                    GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("WarehouseButton").transform.Find("TimeSliderCenter").gameObject.SetActive(true);
                    GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("WarehouseButton").transform.Find("TimeSliderCenter").GetComponent<TimeLeftSliderScript>().Init(86400, 86400);
                    GameObject.Find("BuildingMenu").GetComponent<ModifyMenuScript>().RefreshMenu("Warehouse");
                }
                break;
            case "TrainingCamp":
                {
                    Village.TrainingCamp.InConstruction = true;

                    GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("TrainingCampButton").transform.Find("NotInConstructionTitle").gameObject.SetActive(false);
                    GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("TrainingCampButton").transform.Find("TimeSliderCenter").gameObject.SetActive(true);
                    GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("TrainingCampButton").transform.Find("TimeSliderCenter").GetComponent<TimeLeftSliderScript>().Init(86400, 86400);
                    GameObject.Find("BuildingMenu").GetComponent<ModifyMenuScript>().RefreshMenu("TrainingCamp");
                }
                break;
            case "HealerHut":
                {
                    Village.HealerHut.InConstruction = true;

                    GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("HealerHutButton").transform.Find("NotInConstructionTitle").gameObject.SetActive(false);
                    GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("HealerHutButton").transform.Find("TimeSliderCenter").gameObject.SetActive(true);
                    GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("HealerHutButton").transform.Find("TimeSliderCenter").GetComponent<TimeLeftSliderScript>().Init(86400, 86400);
                    GameObject.Find("BuildingMenu").GetComponent<ModifyMenuScript>().RefreshMenu("HealerHut");
                }
                break;
        }
    }

    public static void NotifyConstructFinished(string building)
    {
        switch (building)
        {
            case "TavernButton":
                {
                    Village.Tavern.InConstruction = false;
                    Village.Tavern.Level += 1;
                    StopTimerConstructInMenu("Tavern");
                    GameObject.Find("BuildingMenu").transform.Find("TimeSlider").gameObject.SetActive(false);
                    GameObject.Find("BuildingMenu").GetComponent<ModifyMenuScript>().RefreshMenu("Tavern");
                    GameObject.Find("BuildingObjects").transform.Find("Tavern").GetComponent<BuildingBehaviourScript>().RefreshBuilding("Tavern");
                    GameObject.Find("BuildingObjects").transform.Find("Tavern").Find("CanvasTavern").Find("TavernText").GetComponent<TMP_Text>().text = "Taverne\nNiveau " + Village.Tavern.Level;
                }
                break;
            case "GunsmithButton":
                {
                    Village.Gunsmith.InConstruction = false;
                    Village.Gunsmith.Level += 1;
                    StopTimerConstructInMenu("Gunsmith");
                    GameObject.Find("BuildingMenu").transform.Find("TimeSlider").gameObject.SetActive(false);
                    GameObject.Find("BuildingMenu").GetComponent<ModifyMenuScript>().RefreshMenu("Gunsmith");
                    GameObject.Find("BuildingObjects").transform.Find("Gunsmith").GetComponent<BuildingBehaviourScript>().RefreshBuilding("Gunsmith");
                    GameObject.Find("BuildingObjects").transform.Find("Gunsmith").Find("CanvasGunsmith").Find("GunsmithText").GetComponent<TMP_Text>().text = "Armurier\nNiveau " + Village.Gunsmith.Level;
                }
                break;
            case "WarehouseButton":
                {
                    Village.Warehouse.InConstruction = false;
                    Village.Warehouse.Level += 1;
                    StopTimerConstructInMenu("Warehouse");
                    GameObject.Find("BuildingMenu").transform.Find("TimeSlider").gameObject.SetActive(false);
                    GameObject.Find("BuildingMenu").GetComponent<ModifyMenuScript>().RefreshMenu("Warehouse");
                    GameObject.Find("BuildingObjects").transform.Find("Warehouse").GetComponent<BuildingBehaviourScript>().RefreshBuilding("Warehouse");
                    GameObject.Find("BuildingObjects").transform.Find("Warehouse").Find("CanvasWarehouse").Find("WarehouseText").GetComponent<TMP_Text>().text = "Entrepôt\nNiveau " + Village.Warehouse.Level;
                }
                break;
            case "TrainingCampButton":
                {

                    Village.TrainingCamp.InConstruction = false;
                    Village.TrainingCamp.Level += 1;
                    StopTimerConstructInMenu("TrainingCamp");
                    GameObject.Find("BuildingMenu").transform.Find("TimeSlider").gameObject.SetActive(false);
                    GameObject.Find("BuildingMenu").GetComponent<ModifyMenuScript>().RefreshMenu("TrainingCamp");
                    GameObject.Find("BuildingObjects").transform.Find("TrainingCamp").GetComponent<BuildingBehaviourScript>().RefreshBuilding("TrainingCamp");
                    GameObject.Find("BuildingObjects").transform.Find("TrainingCamp").Find("CanvasTrainingCamp").Find("TrainingCampText").GetComponent<TMP_Text>().text = "Camp d'entraînement\nNiveau " + Village.TrainingCamp.Level;
                }
                break;
            case "HealerHutButton":
                {
                    Village.HealerHut.InConstruction = false;
                    Village.HealerHut.Level += 1;
                    StopTimerConstructInMenu("HealerHut");
                    GameObject.Find("BuildingMenu").transform.Find("TimeSlider").gameObject.SetActive(false);
                    GameObject.Find("BuildingMenu").GetComponent<ModifyMenuScript>().RefreshMenu("HealerHut");
                    GameObject.Find("BuildingObjects").transform.Find("HealerHut").GetComponent<BuildingBehaviourScript>().RefreshBuilding("HealerHut");
                    GameObject.Find("BuildingObjects").transform.Find("HealerHut").Find("CanvasHealerHut").Find("HealerHutText").GetComponent<TMP_Text>().text = "Hutte du guérisseur\nNiveau " + Village.HealerHut.Level;
                }
                break;
        }
    }


    public static int GetConstructionTime(string building)
    {
        float time = 0;
        switch (building)
        {
            case "Tavern":
                {
                    time = GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("TavernButton").Find("TimeSliderCenter").GetComponent<Slider>().value;
                }
                break;
            case "Gunsmith":
                {
                    time = GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("GunsmithButton").Find("TimeSliderCenter").GetComponent<Slider>().value;
                }
                break;
            case "Warehouse":
                {
                    time = GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("WarehouseButton").Find("TimeSliderCenter").GetComponent<Slider>().value;
                }
                break;
            case "TrainingCamp":
                {
                    time = GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("TrainingCampButton").Find("TimeSliderCenter").GetComponent<Slider>().value;
                }
                break;
            case "HealerHut":
                {
                    time = GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("HealerHutButton").Find("TimeSliderCenter").GetComponent<Slider>().value;
                }
                break;
        }


        return (int)time;
    }


    public static void CharAddedTavern()
    {
        countCharacterTavern++;
        GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("TavernButton").Find("PeopleIcon").Find("CharacterCount").GetComponent<TMP_Text>().text = "x" + countCharacterTavern;
    }

    public static void CharRemovedTavern()
    {
        countCharacterTavern--;
        GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("TavernButton").Find("PeopleIcon").Find("CharacterCount").GetComponent<TMP_Text>().text = "x" + countCharacterTavern;
    }

    public static void TrainingStarted()
    {
        GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("TrainingCampButton").Find("PeopleIcon").Find("TimeSliderSpecific").gameObject.SetActive(true);
        GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("TrainingCampButton").Find("PeopleIcon").Find("TimeSliderSpecific").GetComponent<TimeLeftSliderScript>().Init(Convert.ToInt32(GameObject.Find("TrainingCampMenu").transform.Find("TimeSliderTrainingCamp").GetComponent<Slider>().maxValue), Convert.ToInt32(GameObject.Find("TrainingCampMenu").transform.Find("TimeSliderTrainingCamp").GetComponent<Slider>().maxValue));
    }

    public static void CharAddedTrainingCamp()
    {
        countCharacterTrainingCamp++;
        GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("TrainingCampButton").Find("PeopleIcon").Find("CharacterCount").GetComponent<TMP_Text>().text = "x" + countCharacterTrainingCamp;
    }

    public static void CharRemovedTrainingCamp()
    {
        countCharacterTrainingCamp--;
        GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("TrainingCampButton").Find("PeopleIcon").Find("CharacterCount").GetComponent<TMP_Text>().text = "x" + countCharacterTrainingCamp;
    }

    public static void CharAddedGunsmith()
    {
        GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("GunsmithButton").transform.Find("PeopleIcon").Find("CharacterCount").GetComponent<TMP_Text>().text = "x" + 1;
    }

    public static void CharRemovedGunsmith()
    {
        GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("GunsmithButton").transform.Find("PeopleIcon").Find("CharacterCount").GetComponent<TMP_Text>().text = "x" + 0;
    }

    public static void CharAddedHealer()
    {
        countCharacterHealer++;
        GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("HealerHutButton").Find("PeopleIcon").Find("CharacterCount").GetComponent<TMP_Text>().text = "x" + countCharacterHealer;
    }

    public static void CharRemovedHealer()
    {
        countCharacterHealer--;
        GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("HealerHutButton").Find("PeopleIcon").Find("CharacterCount").GetComponent<TMP_Text>().text = "x" + countCharacterHealer;
    }

    #endregion
}
