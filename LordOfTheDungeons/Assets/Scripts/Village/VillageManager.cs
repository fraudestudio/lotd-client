using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VillageManager : MonoBehaviour
{
    private void Awake()
    {
        Village v = new Village("dsqfqesf", "qsdfqsd","ezaf") ;
        GameObject.Find("BuildingText").transform.Find("CanvasGunsmith").Find("GunsmithName").Find("GunsmithLevel").GetComponent<TMP_Text>().text = "Niveau " + Village.Gunsmith.Level;
        GameObject.Find("BuildingText").transform.Find("CanvasTrainingCamp").Find("TrainingCampName").Find("TrainingCampLevel").GetComponent<TMP_Text>().text = "Niveau " + Village.Gunsmith.Level;
        GameObject.Find("BuildingText").transform.Find("CanvasHealerHut").Find("HutName").Find("HutLevel").GetComponent<TMP_Text>().text = "Niveau " + Village.Gunsmith.Level;
        GameObject.Find("BuildingText").transform.Find("CanvasTavern").Find("TavernName").Find("TavernLevel").GetComponent<TMP_Text>().text = "Niveau " + Village.Tavern.Level;
        GameObject.Find("BuildingText").transform.Find("CanvasWarehouse").Find("WarehouseName").Find("WareHouseLevel").GetComponent<TMP_Text>().text = "Niveau " + Village.Warehouse.Level;
    }
}
