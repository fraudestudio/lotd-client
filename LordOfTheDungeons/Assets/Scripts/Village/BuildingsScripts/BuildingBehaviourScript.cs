using Assets.Scripts.Server.Model;
using Assets.Scripts.Village;
using Assets.Scripts.Village.CharactersInfo;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingBehaviourScript : MonoBehaviour
{

    /// <summary>
    /// Pour savoir sur les b�timents peuvent �tre cliquer ou non
    /// </summary>
    private static bool canBeClicked = true;

    public static bool CanBeClicked { get => canBeClicked; set => canBeClicked = value; }

    [SerializeField]
    private GameObject slotPreFab;
    [SerializeField]
    private GameObject testCharacterPreFab;


    private void Start()
    {
        switch (transform.name)
        {
            case "Tavern": InitTavern(); break;
            case "Gunsmith": InitGunsmith(); break;
            case "Warehouse": InitWarehouse(); break;
            case "TrainingCamp": InitTrainingCamp(); break;
            case "HealerHut": InitHealerHut(); break;
        }
    }



    #region Start and stop building

    /// <summary>
    /// Qaund on clique sur un b�timent, on d�marre le menu qu'on a besoin 
    /// </summary>
    private void OnMouseDown()
    {
        if (canBeClicked)
        {
            canBeClicked = false;
            switch (transform.name)
            {
                case "Tavern": StartMenuTavern(); break;
                case "Gunsmith": StartGunsmith(); break;
                case "Warehouse": StartWarehouse(); break;
                case "TrainingCamp": StartTrainingCamp(); break;
                case "VillageCenter": StartVillageCenter(); break;
                case "HealerHut": StartHealerHut(); break;
            }
        }
    }

    /// <summary>
    /// Arr�te les �l�ments du menu que l'on souhaite
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
            case "HealerHut": StopHealerHut(); break;
        }
    }

    #endregion

    #region HealerHut

    /// <summary>
    /// Initiate the healer hut
    /// </summary>
    private void InitHealerHut()
    {
        for (int i = 0; i < Village.HealerHut.Level + 1; i++)
        {
            if (i % 2 == 0)
            {
                GameObject d = Instantiate(slotPreFab);
                d.name = "SlotMed";
                d.transform.Find("CharacterSlot").GetComponent<CharacterSlotScript>().SetType(SlotType.HEALER);
                d.transform.SetParent(GameObject.Find("HealerHutMenu").transform.Find("SlotsHealer"));
                d.transform.localScale = new Vector2(1f, 1f);
            }
        }
    }

    /// <summary>
    /// Start the healer hut
    /// </summary>
    private void StartHealerHut()
    {
        GameObject.Find("BuildingObjects").transform.Find("HealerHut").Find("CanvasHealerHut").GetComponent<CanvasGroup>().alpha = 0;
        GameObject m = GameObject.Find("BuildingMenu");
        GameObject w = GameObject.Find("HealerHutMenu");
        Init(m);
        Init(w);
        m.GetComponent<ModifyMenuScript>().InitMenu("HealerHut", "Endroit o� soigner les h�ros");

        for (int i = 0; i < GameObject.Find("HealerHutMenu").transform.Find("SlotsHealer").childCount; i++)
        {
            if (!GameObject.Find("HealerHutMenu").transform.Find("SlotsHealer").GetChild(i).Find("CharacterSlot").GetComponent<CharacterSlotScript>().SlotIsEmpty)
            {
                CharacterSlotNotAllowedScript.AddSlot(GameObject.Find("HealerHutMenu").transform.Find("SlotsHealer").GetChild(i).Find("CharacterSlot").gameObject);
            }
        }
    }

    /// <summary>
    /// Stop the healer hut
    /// </summary>
    private void StopHealerHut()
    {
        for (int i = 0; i < GameObject.Find("HealerHutMenu").transform.Find("SlotsHealer").childCount; i++)
        {
            CharacterSlotNotAllowedScript.RemoveSlot(GameObject.Find("HealerHutMenu").transform.Find("SlotsHealer").GetChild(i).Find("CharacterSlot").gameObject);
        }
    }

    #endregion

    #region VillageCenter

    /// <summary>
    /// Start the village center
    /// </summary>
    private void StartVillageCenter()
    {
        GameObject.Find("BuildingObjects").transform.Find("VillageCenter").Find("CanvasVillageCenter").GetComponent<CanvasGroup>().alpha = 0;
        Init(GameObject.Find("VillageCenterMenu"));
    }

    #endregion

    #region TrainingCamp

    /// <summary>
    /// Initiate the training camp
    /// </summary>
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

    /// <summary>
    /// Start the training camp 
    /// </summary>
    private void StartTrainingCamp()
    {
        GameObject.Find("BuildingObjects").transform.Find("TrainingCamp").Find("CanvasTrainingCamp").GetComponent<CanvasGroup>().alpha = 0;
        GameObject m = GameObject.Find("BuildingMenu");
        GameObject w = GameObject.Find("TrainingCampMenu");
        Init(m);
        Init(w);
        m.GetComponent<ModifyMenuScript>().InitMenu("TrainingCamp", "Endroit o� entrainez les aventuriers");

        for (int i = 0; i < w.transform.Find("TraineeTitle").Find("TraineesLayout").childCount; i++)
        {
            w.transform.Find("TraineeTitle").Find("TraineesLayout").GetChild(i).gameObject.SetActive(true);

            if (!w.transform.Find("TraineeTitle").Find("TraineesLayout").GetChild(i).GetComponent<CharacterSlotScript>().SlotIsEmpty || !w.transform.Find("TraineeTitle").Find("TraineesLayout").GetChild(i).GetComponent<CharacterSlotScript>().CanDrop)
            {
                CharacterSlotNotAllowedScript.AddSlot(w.transform.Find("TraineeTitle").Find("TraineesLayout").GetChild(i).gameObject);
            }
        }
        if (!GameObject.Find("InstructorTitle").transform.Find("CharacterSlot").GetComponent<CharacterSlotScript>().SlotIsEmpty)
        {
            CharacterSlotNotAllowedScript.AddSlot(w.transform.Find("InstructorTitle").Find("CharacterSlot").gameObject);
        }
    }
    
    /// <summary>
    /// Stop the training camp
    /// </summary>
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


    public void TrainingIsFinished()
    {
        GameObject t = GameObject.Find("TrainingCampMenu");

        t.transform.Find("TimeSliderTrainingCamp").GetComponent<TimeLeftSliderScript>().Stop();
        t.transform.Find("TimeSliderTrainingCamp").gameObject.SetActive(false);


        for (int i = 0; i < t.transform.Find("TraineeTitle").Find("TraineesLayout").childCount; i++)
        {
            t.transform.Find("TraineeTitle").Find("TraineesLayout").GetChild(i).gameObject.SetActive(true);

            t.transform.Find("TraineeTitle").Find("TraineesLayout").GetChild(i).GetComponent<CharacterSlotScript>().CanDrop = true;

            if (!t.transform.Find("TraineeTitle").Find("TraineesLayout").GetChild(i).GetComponent<CharacterSlotScript>().SlotIsEmpty)
            {
                t.transform.Find("TraineeTitle").Find("TraineesLayout").GetChild(i).GetComponent<CharacterSlotScript>().CurrentCharacter.GetComponent<CharacterImageSlotScript>().SetDrag(true);
                t.transform.Find("TraineeTitle").Find("TraineesLayout").GetChild(i).GetComponent<CharacterSlotScript>().CurrentCharacter.GetComponent<CharacterImageSlotScript>().Character.AddLevel(1);
            }
            else
            {
                CharacterSlotNotAllowedScript.RemoveSlot(t.transform.Find("TraineeTitle").Find("TraineesLayout").GetChild(i).gameObject);
            }

            t.transform.Find("InstructorTitle").transform.Find("CharacterSlot").GetComponent<CharacterSlotScript>().CanDrop = true;
            t.transform.Find("InstructorTitle").transform.Find("CharacterSlot").GetComponent<CharacterSlotScript>().CurrentCharacter.GetComponent<CharacterImageSlotScript>().SetDrag(true);
        }

        t.GetComponent<CanTrainScript>().VerifyCond();

    }

    #endregion

    #region Warehouse
    /// <summary>
    /// Initiate the warehouse
    /// </summary>
    private void InitWarehouse()
    {
        GameObject.Find("WarehouseMenu").transform.Find("Icon&Capacity").Find("GoldIcon").Find("MaxGoldCapacity").GetComponent<TMP_Text>().text = (Village.Warehouse.BaseMaxIron * Village.Warehouse.Level).ToString();
        GameObject.Find("WarehouseMenu").transform.Find("Icon&Capacity").Find("WoodIcon").Find("MaxWoodCapacity").GetComponent<TMP_Text>().text = (Village.Warehouse.BaseMaxWood * Village.Warehouse.Level).ToString();
        GameObject.Find("WarehouseMenu").transform.Find("Icon&Capacity").Find("StoneIcon").Find("MaxStoneCapacity").GetComponent<TMP_Text>().text = (Village.Warehouse.BaseMaxStone * Village.Warehouse.Level).ToString();

        GameObject.Find("ATHVillage").transform.Find("Background").Find("GoldSlider").GetComponent<MaterialSliderScript>().ChangeMax(Village.Warehouse.BaseMaxIron * Village.Warehouse.Level);
        GameObject.Find("ATHVillage").transform.Find("Background").Find("WoodSlider").GetComponent<MaterialSliderScript>().ChangeMax(Village.Warehouse.BaseMaxWood * Village.Warehouse.Level);
        GameObject.Find("ATHVillage").transform.Find("Background").Find("StoneSlider").GetComponent<MaterialSliderScript>().ChangeMax(Village.Warehouse.BaseMaxStone * Village.Warehouse.Level);

        Ressources r = Server.GetRessources();

        GameObject.Find("ATHVillage").transform.Find("Background").Find("GoldSlider").GetComponent<MaterialSliderScript>().SetValue(r.Or);
        GameObject.Find("ATHVillage").transform.Find("Background").Find("WoodSlider").GetComponent<MaterialSliderScript>().SetValue(r.Bois);
        GameObject.Find("ATHVillage").transform.Find("Background").Find("StoneSlider").GetComponent<MaterialSliderScript>().SetValue(r.Pierre);

    }
    /// <summary>
    /// Start the warehouse
    /// </summary>
    private void StartWarehouse()
    {
        GameObject.Find("BuildingObjects").transform.Find("Warehouse").Find("CanvasWarehouse").GetComponent<CanvasGroup>().alpha = 0;
        GameObject m = GameObject.Find("BuildingMenu");
        GameObject w = GameObject.Find("WarehouseMenu");
        Init(m);
        Init(w);
        m.GetComponent<ModifyMenuScript>().InitMenu("Warehouse", "Endroit o� les ressources sont stock�s");
    }
    /// <summary>
    /// Stop the warehouse
    /// </summary>
    private void StopWarehouse()
    {

    }
    #endregion

    #region Gunsmith
    /// <summary>
    /// Initialise les �l�ments de l'armurier
    /// </summary>
    private void InitGunsmith()
    {
        
    }

    private void StartGunsmith()
    {
        GameObject.Find("BuildingObjects").transform.Find("Gunsmith").Find("CanvasGunsmith").GetComponent<CanvasGroup>().alpha = 0;
        GameObject m = GameObject.Find("BuildingMenu");
        GameObject g = GameObject.Find("GunsmithMenu");
        Init(m);
        Init(g);
        m.GetComponent<ModifyMenuScript>().InitMenu("Gunsmith", "Endroit o� am�liorer l'�quipement des h�ros");
        if (!GameObject.Find("GunsmithMenu").transform.Find("CharacterSlot").GetComponent<CharacterSlotScript>().SlotIsEmpty)
        {
            CharacterSlotNotAllowedScript.AddSlot(GameObject.Find("GunsmithMenu").transform.Find("CharacterSlot").gameObject);
        }
    }

    /// <summary>
    /// Arr�te les �l�ments de l'Armurier
    /// </summary>
    private void StopGunsmith()
    {
        if (!GameObject.Find("GunsmithMenu").transform.Find("CharacterSlot").GetComponent<CharacterSlotScript>().SlotIsEmpty)
        {
            CharacterSlotNotAllowedScript.RemoveSlot(GameObject.Find("GunsmithMenu").transform.Find("CharacterSlot").gameObject);
        }
    }
    #endregion

    #region Tavern
    /// <summary>
    /// Initialise les �l�ments de la taverne 
    /// </summary>
    private void InitTavern()
    {
        Debug.Log("caca");
        GameObject t = GameObject.Find("TavernMenu");
        t.transform.Find("TimeSliderTavern").GetComponent<TimeLeftSliderScript>().Init(Village.Tavern.TimeBeforeNewRecruit, Tavern.TimeMaxBeforeNewRecruit);
        Transform heoresAvaiable = t.transform.Find("HeroesAvaiable");
        

        for (int i = 0; i < Village.Tavern.Level + 1; i++)
        {
            if (i % 2 == 0)
            {
                //creation case
                GameObject d = Instantiate(slotPreFab);
                d.name = "Slot_" + i;
                d.GetComponent<CharacterSlotScript>().SetType(SlotType.TAVERN);
                d.transform.SetParent(heoresAvaiable);
                d.transform.localScale = new Vector2(1f, 1f);
            }
        }

        Debug.Log(Server.GetCurrentVillage());
        if (Server.GetStartTimeTaverne(Server.GetCurrentVillage()) <= 0)
        {
            Server.SetStartTimeTaverneNow(Server.GetCurrentVillage());
            Server.DeleteAllCharacterFromTaverne(Server.GetCurrentVillage());
            for (int i = 0; i < Village.Tavern.Level + 1; i++)
            {
                if (i % 2 == 0)
                {
                    int idNewPerso = 0;
                    idNewPerso = Server.InitCharacter(Server.GetCurrentVillage());
                    Server.SetCharacterInBatiment(Server.GetCurrentVillage(), idNewPerso, "TAVERN");
                }
            }
        }
        Debug.Log(Server.GetCurrentVillage());
        List<int> characters = Server.GetCharacterInBatiment(Server.GetCurrentVillage(), "TAVERN");
        CharacterModel characterTemp = new CharacterModel();
        Equipement equipementTemp = new Equipement();
        for (int i = 0; i < Village.Tavern.Level + 1; i++)
        {
            if (i % 2 == 0)
            {
                characterTemp = Server.GetCharacterByID(characters[i]);
                Debug.Log(characterTemp.Name);
                equipementTemp = Server.getEquipement(characters[i]);
                Debug.Log(equipementTemp.NIVEAU_ARMURE);
                Armor a = GameObject.Find("CharacterFactory").GetComponent<ArmorFactory>().CreateArmor(0, "Armure en cuir", equipementTemp.NIVEAU_ARMURE, equipementTemp.BONUS_ARME);
                Weapon w = GameObject.Find("CharacterFactory").GetComponent<WeaponFactory>().CreateWeapon(0, "Fronde", equipementTemp.NIVEAU_ARME, equipementTemp.NIVEAU_ARMURE);
                GameObject c = GameObject.Find("CharacterFactory").GetComponent<CharacterFactory>().CreateCharacter(0, characterTemp.Name, characterTemp.RACE, characterTemp.Level, characterTemp.PV, characterTemp.PV_MAX, characterTemp.PA_MAX, characterTemp.PM_MAX, characterTemp.CLASSE, w, a);
                heoresAvaiable.GetChild(i).GetComponent<CharacterSlotScript>().AddChar(c);
            }
        }
        Debug.Log("sucess");
    }


    /// <summary>
    /// Initialise le menu de la taverne
    /// </summary>
    private void StartMenuTavern()
    {

        GameObject.Find("BuildingObjects").transform.Find("Tavern").Find("CanvasTavern").GetComponent<CanvasGroup>().alpha = 0;
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
        m.GetComponent<ModifyMenuScript>().InitMenu("Tavern","Endroit ou les h�ros peuvent �tre recut�s");
    }


    /// <summary>
    /// Arr�te les �lements de la tavern que l'on souhaite
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

    public void NewArrival()
    {

        GameObject.Find("TavernMenu").transform.Find("TimeSliderTavern").GetComponent<TimeLeftSliderScript>().Stop();
        GameObject.Find("TavernMenu").transform.Find("TimeSliderTavern").GetComponent<TimeLeftSliderScript>().Init(Tavern.TimeMaxBeforeNewRecruit, Tavern.TimeMaxBeforeNewRecruit);
    }

    #endregion



    /// <summary>
    /// Allume un menu souhait�
    /// </summary>
    /// <param name="g"></param>
    private void Init(GameObject g)
    {
        g.GetComponent<CanvasGroup>().alpha = 1;
        g.GetComponent<CanvasGroup>().blocksRaycasts = true;
        g.GetComponent<CanvasGroup>().interactable = true;
    }

    /// <summary>
    /// Affiche les informations du b�timents
    /// </summary>
    private void OnMouseEnter()
    {
        if (canBeClicked)
        {
            switch (transform.name)
            {
                case "Tavern": GameObject.Find("BuildingObjects").transform.Find("Tavern").Find("CanvasTavern").GetComponent<CanvasGroup>().alpha = 1; break;
                case "HealerHut": GameObject.Find("BuildingObjects").transform.Find("HealerHut").Find("CanvasHealerHut").GetComponent<CanvasGroup>().alpha = 1; break;
                case "Gunsmith": GameObject.Find("BuildingObjects").transform.Find("Gunsmith").Find("CanvasGunsmith").GetComponent<CanvasGroup>().alpha = 1; break;
                case "Warehouse": GameObject.Find("BuildingObjects").transform.Find("Warehouse").Find("CanvasWarehouse").GetComponent<CanvasGroup>().alpha = 1; break;
                case "TrainingCamp": GameObject.Find("BuildingObjects").transform.Find("TrainingCamp").Find("CanvasTrainingCamp").GetComponent<CanvasGroup>().alpha = 1; break;
                case "VillageCenter": GameObject.Find("BuildingObjects").transform.Find("VillageCenter").Find("CanvasVillageCenter").GetComponent<CanvasGroup>().alpha = 1; break;
            }
        }

    }

    /// <summary>
    /// Enl�ve les informations du b�timents
    /// </summary>
    private void OnMouseExit()
    {
        if (canBeClicked)
        {
            switch (transform.name)
            {
                case "Tavern": GameObject.Find("BuildingObjects").transform.Find("Tavern").Find("CanvasTavern").GetComponent<CanvasGroup>().alpha = 0; break;
                case "HealerHut": GameObject.Find("BuildingObjects").transform.Find("HealerHut").Find("CanvasHealerHut").GetComponent<CanvasGroup>().alpha = 0; break;
                case "Gunsmith": GameObject.Find("BuildingObjects").transform.Find("Gunsmith").Find("CanvasGunsmith").GetComponent<CanvasGroup>().alpha = 0; break;
                case "Warehouse": GameObject.Find("BuildingObjects").transform.Find("Warehouse").Find("CanvasWarehouse").GetComponent<CanvasGroup>().alpha = 0; break;
                case "TrainingCamp": GameObject.Find("BuildingObjects").transform.Find("TrainingCamp").Find("CanvasTrainingCamp").GetComponent<CanvasGroup>().alpha = 0; break;
                case "VillageCenter": GameObject.Find("BuildingObjects").transform.Find("VillageCenter").Find("CanvasVillageCenter").GetComponent<CanvasGroup>().alpha = 0; break;
            }
        }
    }


    /// <summary>
    /// Refresh the building when needed
    /// </summary>
    /// <param name="building">the wanted building</param>
    public void RefreshBuilding(string building)
    {
        switch (building)
        {
            case "Tavern":
                {
                    if (Village.Tavern.Level % 2 == 0)
                    {
                        GameObject Slot = Instantiate(slotPreFab);
                        Slot.GetComponent<CharacterSlotScript>().SetType(SlotType.TAVERN);
                        Slot.transform.SetParent(GameObject.Find("TavernMenu").transform.Find("HeroesAvaiable").transform);
                        Slot.transform.localScale = new Vector2(1, 1);
                    }

                } break;
            case "Gunsmith":
                {
                    if (!GameObject.Find("GunsmithMenu").transform.Find("CharacterSlot").GetComponent<CharacterSlotScript>().SlotIsEmpty)
                    {
                        // do things...
                    }
                }
                break;
            case "Warehouse":
                {
                    GameObject.Find("WarehouseMenu").transform.Find("Icon&Capacity").Find("GoldIcon").Find("MaxGoldCapacity").GetComponent<TMP_Text>().text = (Village.Warehouse.BaseMaxIron * Village.Warehouse.Level).ToString();
                    GameObject.Find("WarehouseMenu").transform.Find("Icon&Capacity").Find("WoodIcon").Find("MaxWoodCapacity").GetComponent<TMP_Text>().text = (Village.Warehouse.BaseMaxWood * Village.Warehouse.Level).ToString();
                    GameObject.Find("WarehouseMenu").transform.Find("Icon&Capacity").Find("StoneIcon").Find("MaxStoneCapacity").GetComponent<TMP_Text>().text = (Village.Warehouse.BaseMaxStone * Village.Warehouse.Level).ToString();

                    GameObject.Find("ATHVillage").transform.Find("Background").Find("GoldSlider").GetComponent<MaterialSliderScript>().ChangeMax(Village.Warehouse.BaseMaxIron * Village.Warehouse.Level);
                    GameObject.Find("ATHVillage").transform.Find("Background").Find("WoodSlider").GetComponent<MaterialSliderScript>().ChangeMax(Village.Warehouse.BaseMaxWood * Village.Warehouse.Level);
                    GameObject.Find("ATHVillage").transform.Find("Background").Find("StoneSlider").GetComponent<MaterialSliderScript>().ChangeMax(Village.Warehouse.BaseMaxStone * Village.Warehouse.Level);
                }
                break;
            case "TrainingCamp":
                {
                    if (Village.TrainingCamp.Level % 2 == 0)
                    {
                        GameObject Slot = Instantiate(slotPreFab);

                        if (Village.TrainingCamp.InFormation)
                        {
                            Slot.GetComponent<CharacterSlotScript>().CanDrop = false;
                            CharacterSlotNotAllowedScript.AddSlot(Slot);
                        }
                        Slot.name = "SlotMed";
                        Slot.GetComponent<CharacterSlotScript>().SetType(SlotType.TRAINEE);
                        Slot.transform.SetParent(GameObject.Find("TrainingCampMenu").transform.Find("TraineeTitle").Find("TraineesLayout"));
                        Slot.transform.localScale = new Vector2(1, 1);
                    }
                }
                break;
            case "HealerHut":
                {
                    if (Village.HealerHut.Level % 2 == 0)
                    {
                        GameObject Slot = Instantiate(slotPreFab);
                        Slot.transform.Find("CharacterSlot").GetComponent<CharacterSlotScript>().SetType(SlotType.HEALER);
                        Slot.transform.SetParent(GameObject.Find("HealerHutMenu").transform.Find("SlotsHealer").transform);
                        Slot.transform.localScale = new Vector2(1, 1);
                    }

                }
                break;
        }
    }
}
