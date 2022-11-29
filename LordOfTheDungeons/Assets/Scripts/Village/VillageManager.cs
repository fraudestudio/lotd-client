using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VillageManager : MonoBehaviour
{

    public GameObject villageCenterBlockPreFab;
    private void Awake()
    {
        
        Village v = new Village("dsqfqesf", "qsdfqsd","ezaf") ;
        GameObject.Find("BuildingText").transform.Find("CanvasGunsmith").Find("GunsmithName").Find("GunsmithLevel").GetComponent<TMP_Text>().text = "Niveau " + Village.Gunsmith.Level;
        GameObject.Find("BuildingText").transform.Find("CanvasTrainingCamp").Find("TrainingCampName").Find("TrainingCampLevel").GetComponent<TMP_Text>().text = "Niveau " + Village.Gunsmith.Level;
        GameObject.Find("BuildingText").transform.Find("CanvasHealerHut").Find("HutName").Find("HutLevel").GetComponent<TMP_Text>().text = "Niveau " + Village.Gunsmith.Level;
        GameObject.Find("BuildingText").transform.Find("CanvasTavern").Find("TavernName").Find("TavernLevel").GetComponent<TMP_Text>().text = "Niveau " + Village.Tavern.Level;
        GameObject.Find("BuildingText").transform.Find("CanvasWarehouse").Find("WarehouseName").Find("WareHouseLevel").GetComponent<TMP_Text>().text = "Niveau " + Village.Warehouse.Level;


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
            tavernButton.transform.Find("NotInConstructionTitle").gameObject.SetActive(false);
            tavernButton.transform.Find("TimeSliderCenter").gameObject.SetActive(true);
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
    }

    private void Start()
    {
        #region Tavern

        Transform tavernButton = GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("TavernButton");

        if (Village.Tavern.InConstruction)
        {
           tavernButton.Find("TimeSliderCenter").gameObject.GetComponent<TimeLeftSliderScript>().Init(300, 500);
        }


        int countChar = 0;

        for (int i = 0; i < GameObject.Find("TavernMenu").transform.Find("HeroesAvaiable").childCount; i++)
        {
            if (!GameObject.Find("TavernMenu").transform.Find("HeroesAvaiable").GetChild(i).GetComponent<CharacterSlotScript>().slotIsEmpty)
            {
                countChar++;
            }
        }

        tavernButton.transform.Find("PeopleIcon").Find("CharacterCount").GetComponent<TMP_Text>().text = "x" + countChar;
        tavernButton.transform.Find("PeopleIcon").Find("TimeSliderSpecific").gameObject.SetActive(true);
        tavernButton.transform.Find("PeopleIcon").Find("TimeSliderSpecific").GetComponent<TimeLeftSliderScript>().Init(Convert.ToInt32(GameObject.Find("TavernMenu").transform.Find("TimeSliderTavern").GetComponent<Slider>().value), Convert.ToInt32(GameObject.Find("TavernMenu").transform.Find("TimeSliderTavern").GetComponent<Slider>().maxValue));
        #endregion
        #region Gunsmith
        Transform gunsmith = GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("GunsmithButton");

        if (Village.Tavern.InConstruction)
        {
            tavernButton.Find("TimeSliderCenter").gameObject.GetComponent<TimeLeftSliderScript>().Init(300, 500);
        }


        countChar = 0;

        if (!GameObject.Find("GunsmithMenu").transform.Find("CharacterSlot").GetComponent<CharacterSlotScript>().SlotIsEmpty)
        {
            countChar = 1;
        }

        gunsmith.transform.Find("PeopleIcon").Find("CharacterCount").GetComponent<TMP_Text>().text = "x" + countChar;
        #endregion
        #region Warehouse
        Transform warehouse = GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("GunsmithButton");

        if (Village.Warehouse.InConstruction)
        {
            warehouse.Find("TimeSliderCenter").gameObject.GetComponent<TimeLeftSliderScript>().Init(300, 500);
        }

        #endregion
        #region TrainingCamp
        Transform trainingCamp = GameObject.Find("VillageCenterMenu").transform.Find("VillageCenterObjects").Find("TrainingCampButton");

        if (Village.Warehouse.InConstruction)
        {
            trainingCamp.Find("TimeSliderCenter").gameObject.GetComponent<TimeLeftSliderScript>().Init(300, 500);
        }


        countChar = 0;

        for (int i = 0; i < GameObject.Find("TrainingCampMenu").transform.Find("TraineeTitle").Find("TraineesLayout").childCount; i++)
        {
            if (!GameObject.Find("TrainingCampMenu").transform.Find("TraineeTitle").Find("TraineesLayout").GetChild(i).GetComponent<CharacterSlotScript>().slotIsEmpty)
            {
                countChar++;
            }
        }

        if (!GameObject.Find("TrainingCampMenu").transform.Find("InstructorTitle").Find("CharacterSlot").GetComponent<CharacterSlotScript>().slotIsEmpty)
        {
            countChar++;
        }

        trainingCamp.transform.Find("PeopleIcon").Find("CharacterCount").GetComponent<TMP_Text>().text = "x" + countChar;

        if (Village.TrainingCamp.InFormation)
        {
            trainingCamp.transform.Find("PeopleIcon").Find("TimeSliderSpecific").gameObject.SetActive(true);
            trainingCamp.transform.Find("PeopleIcon").Find("TimeSliderSpecific").GetComponent<TimeLeftSliderScript>().Init(Convert.ToInt32(GameObject.Find("TrainingCampMenu").transform.Find("TimeSliderTrainingCamp").GetComponent<Slider>().value), Convert.ToInt32(GameObject.Find("TrainingCampMenu").transform.Find("TimeSliderTrainingCamp").GetComponent<Slider>().maxValue));
        }

        #endregion
    }
}
