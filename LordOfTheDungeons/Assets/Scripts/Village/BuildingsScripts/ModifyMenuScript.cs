using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModifyMenuScript : MonoBehaviour
{
    [SerializeField]
    private TMP_Text TitleText;
    [SerializeField]
    private TMP_Text DescriptionText;
    [SerializeField]
    private Button UpgradeButton;
    [SerializeField]
    private GameObject constructionTimer;

    private string currentUsedBuilding = "";

    /// <summary>
    /// Permet de savoir quel est le bâtiment qui est utilisé par l'utilisateur
    /// </summary>
    /// <returns></returns>
    public string GetCurrentUsedBuilding()
    {
        return currentUsedBuilding;
    }

    /// <summary>
    /// Permet de rénitialiser le bâtiment utilisé par l'utilisateur;
    /// </summary>
    public void ResetCurrentUsedBuilding()
    {
        currentUsedBuilding = "";
    }

    /// <summary>
    /// Arrête le timer de construction
    /// </summary>
    public void StopConstructionTimer()
    {
        constructionTimer.GetComponent<TimeLeftSliderScript>().Stop();
    }



    /// <summary>
    /// Initialise correctement le menu en fonction de ce que l'on lui donne
    /// </summary>
    /// <param name="building"></param>
    /// <param name="description"></param>
    /// <param name="isInConstruction"></param>
    /// <param name="timeRemaining"></param>
    public void InitMenu(string building, string description)
    {

        switch (building)
        {
            case "Tavern":
                {
                    #region Tavern
                    currentUsedBuilding = building;
                    TitleText.text = "Taverne";
                    DescriptionText.text = description;
                    
                    /// On regarde si la taverne est améliorable et si la taverne n'est pas en construction
                    if (Village.Tavern.Level < 5 && !Village.Tavern.InConstruction)
                    {
                        SetActiveUpgrade(true);
                        UpgradeButton.transform.Find("WoodCountLogo").transform.Find("WoodCount").GetComponent<TMP_Text>().text = (Village.Tavern.BaseWoodNeeded * (Village.Tavern.WoodModification * Village.Tavern.Level)).ToString();
                        UpgradeButton.transform.Find("StoneCountLogo").transform.Find("StoneCount").GetComponent<TMP_Text>().text = (Village.Tavern.BaseStoneNeeded * (Village.Tavern.StoneModification * Village.Tavern.Level)).ToString();
                        UpgradeButton.transform.Find("GoldCountLogo").transform.Find("GoldCount").GetComponent<TMP_Text>().text = (Village.Tavern.BaseGoldNeeded * (Village.Tavern.GoldModification * Village.Tavern.Level)).ToString();

                        InteractableTest();
                    }
                    else
                    {
                        SetActiveUpgrade(false);
                    }

                    /// On vérifie si la tavern est en construction
                    /// Si elle l'est pas on désactive le timer.
                    if (Village.Tavern.InConstruction)
                    {
                        constructionTimer.SetActive(true);
                        constructionTimer.GetComponent<TimeLeftSliderScript>().Init(VillageManager.GetConstructionTime("Tavern"), 86400);
                    }
                    else
                    {
                        constructionTimer.SetActive(false);
                    }
                    #endregion
                }
                break;
            case "Gunsmith":
                {
                    #region Gunsmith
                    currentUsedBuilding = building;
                    TitleText.text = "Armurier";
                    DescriptionText.text = description;

                    /// On regarde si l'armurier est améliorable
                    if (Village.Gunsmith.Level < 5 && !Village.Gunsmith.InConstruction)
                    {
                        SetActiveUpgrade(true);
                        UpgradeButton.transform.Find("WoodCountLogo").transform.Find("WoodCount").GetComponent<TMP_Text>().text = (Village.Gunsmith.BaseWoodNeeded * (Village.Gunsmith.WoodModification * Village.Gunsmith.Level)).ToString();
                        UpgradeButton.transform.Find("StoneCountLogo").transform.Find("StoneCount").GetComponent<TMP_Text>().text = (Village.Gunsmith.BaseStoneNeeded * (Village.Gunsmith.StoneModification * Village.Gunsmith.Level)).ToString();
                        UpgradeButton.transform.Find("GoldCountLogo").transform.Find("GoldCount").GetComponent<TMP_Text>().text = (Village.Gunsmith.BaseGoldNeeded * (Village.Gunsmith.GoldModification * Village.Gunsmith.Level)).ToString();
                        InteractableTest();

                    }
                    else
                    {
                        SetActiveUpgrade(false);
                    }

                    /// On vérifie si la tavern est en construction
                    /// Si elle l'est pas on désactive le timer.
                    if (Village.Gunsmith.InConstruction)
                    {
                        constructionTimer.SetActive(true);
                        constructionTimer.GetComponent<TimeLeftSliderScript>().Init(VillageManager.GetConstructionTime("Gunsmith"), 86400);
                    }
                    else
                    {
                        constructionTimer.SetActive(false);
                    }
                    #endregion
                }
                break;
            case "Warehouse": 
                {
                    #region Warehouse
                    currentUsedBuilding = building;
                    TitleText.text = "Entrepôt";
                    DescriptionText.text = description;

                    /// On regarde si l'armurier est améliorable
                    if (Village.Warehouse.Level < 5 && !Village.Warehouse.InConstruction)
                    {
                        SetActiveUpgrade(true);
                        UpgradeButton.transform.Find("WoodCountLogo").transform.Find("WoodCount").GetComponent<TMP_Text>().text = (Village.Warehouse.BaseWoodNeeded * (Village.Warehouse.WoodModification * Village.Warehouse.Level)).ToString();
                        UpgradeButton.transform.Find("StoneCountLogo").transform.Find("StoneCount").GetComponent<TMP_Text>().text = (Village.Warehouse.BaseStoneNeeded * (Village.Warehouse.StoneModification * Village.Warehouse.Level)).ToString();
                        UpgradeButton.transform.Find("GoldCountLogo").transform.Find("GoldCount").GetComponent<TMP_Text>().text = (Village.Warehouse.BaseGoldNeeded * (Village.Warehouse.GoldModification * Village.Warehouse.Level)).ToString();

                        InteractableTest();
                    }
                    else
                    {
                        SetActiveUpgrade(false);
                    }

                    /// On vérifie si la tavern est en construction
                    /// Si elle l'est pas on désactive le timer.
                    if (Village.Warehouse.InConstruction)
                    {
                        constructionTimer.SetActive(true);
                        constructionTimer.GetComponent<TimeLeftSliderScript>().Init(VillageManager.GetConstructionTime("Warehouse"), 86400);
                    }
                    else
                    {
                        constructionTimer.SetActive(false);
                    }
                    #endregion
                };
                break;
            case "TrainingCamp":
                {
                    #region TrainingCamp
                    currentUsedBuilding = building;
                    TitleText.text = "Camp d'entraînement";
                    DescriptionText.text = description;

                    if (Village.TrainingCamp.Level < 5 && !Village.TrainingCamp.InConstruction)
                    {
                        SetActiveUpgrade(true);
                        UpgradeButton.transform.Find("WoodCountLogo").transform.Find("WoodCount").GetComponent<TMP_Text>().text = (Village.TrainingCamp.BaseWoodNeeded * (Village.TrainingCamp.WoodModification * Village.TrainingCamp.Level)).ToString();
                        UpgradeButton.transform.Find("StoneCountLogo").transform.Find("StoneCount").GetComponent<TMP_Text>().text = (Village.TrainingCamp.BaseStoneNeeded * (Village.TrainingCamp.StoneModification * Village.TrainingCamp.Level)).ToString();
                        UpgradeButton.transform.Find("GoldCountLogo").transform.Find("GoldCount").GetComponent<TMP_Text>().text = (Village.TrainingCamp.BaseGoldNeeded * (Village.TrainingCamp.GoldModification * Village.TrainingCamp.Level)).ToString();

                        InteractableTest();
                    }
                    else
                    {
                        SetActiveUpgrade(false);
                    }

                    /// On vérifie si la tavern est en construction
                    /// Si elle l'est pas on désactive le timer.
                    if (Village.TrainingCamp.InConstruction)
                    {
                        constructionTimer.SetActive(true);
                        constructionTimer.GetComponent<TimeLeftSliderScript>().Init(VillageManager.GetConstructionTime("TrainingCamp"), 86400);
                    }
                    else
                    {
                        constructionTimer.SetActive(false);
                    }
                    #endregion
                }
                break;
            case "HealerHut":
                {
                    #region HealerHut
                    currentUsedBuilding = building;
                    TitleText.text = "Hutte du guérisseur";
                    DescriptionText.text = description;


                    if (Village.HealerHut.Level < 5 && !Village.HealerHut.InConstruction)
                    {
                        SetActiveUpgrade(true);
                        UpgradeButton.transform.Find("WoodCountLogo").transform.Find("WoodCount").GetComponent<TMP_Text>().text = (Village.TrainingCamp.BaseWoodNeeded * (Village.TrainingCamp.WoodModification * Village.TrainingCamp.Level)).ToString();
                        UpgradeButton.transform.Find("StoneCountLogo").transform.Find("StoneCount").GetComponent<TMP_Text>().text = (Village.TrainingCamp.BaseStoneNeeded * (Village.TrainingCamp.StoneModification * Village.TrainingCamp.Level)).ToString();
                        UpgradeButton.transform.Find("GoldCountLogo").transform.Find("GoldCount").GetComponent<TMP_Text>().text = (Village.TrainingCamp.BaseGoldNeeded * (Village.TrainingCamp.GoldModification * Village.Warehouse.Level)).ToString();

                        InteractableTest();
                    }
                    else
                    {
                        SetActiveUpgrade(false);
                    }

                    /// On vérifie si la tavern est en construction
                    /// Si elle l'est pas on désactive le timer.
                    if (Village.HealerHut.InConstruction)
                    {
                        constructionTimer.SetActive(true);
                        constructionTimer.GetComponent<TimeLeftSliderScript>().Init(VillageManager.GetConstructionTime("HealerHut"), 86400);
                    }
                    else
                    {
                        constructionTimer.SetActive(false);
                    }
                    #endregion
                }
                break;
        }
    }


    public void RefreshMenu(string building)
    {
        switch (building)
        {
            case "Tavern":
                {
                    if (currentUsedBuilding == "Tavern")
                    {
                        if (Village.Tavern.Level < 5 && !Village.Tavern.InConstruction)
                        {
                            SetActiveUpgrade(true);
                            UpgradeButton.transform.Find("WoodCountLogo").transform.Find("WoodCount").GetComponent<TMP_Text>().text = (Village.Tavern.BaseWoodNeeded * (Village.Tavern.WoodModification * Village.Tavern.Level)).ToString();
                            UpgradeButton.transform.Find("StoneCountLogo").transform.Find("StoneCount").GetComponent<TMP_Text>().text = (Village.Tavern.BaseStoneNeeded * (Village.Tavern.StoneModification * Village.Tavern.Level)).ToString();
                            UpgradeButton.transform.Find("GoldCountLogo").transform.Find("GoldCount").GetComponent<TMP_Text>().text = (Village.Tavern.BaseGoldNeeded * (Village.Tavern.GoldModification * Village.Tavern.Level)).ToString();
                            InteractableTest();
                        }

                        if (Village.Tavern.InConstruction)
                        {
                            constructionTimer.SetActive(true);
                            constructionTimer.GetComponent<TimeLeftSliderScript>().Init(VillageManager.GetConstructionTime("Tavern"), 86400);
                            SetActiveUpgrade(false);
                        }
                    }

                }
                break;
            case "Gunsmith":
                {
                    if (currentUsedBuilding == "Gunsmith")
                    {
                        if (Village.Gunsmith.Level < 5 && !Village.Gunsmith.InConstruction)
                        {
                            SetActiveUpgrade(true);
                            UpgradeButton.transform.Find("WoodCountLogo").transform.Find("WoodCount").GetComponent<TMP_Text>().text = (Village.Gunsmith.BaseWoodNeeded * (Village.Gunsmith.WoodModification * Village.Gunsmith.Level)).ToString();
                            UpgradeButton.transform.Find("StoneCountLogo").transform.Find("StoneCount").GetComponent<TMP_Text>().text = (Village.Gunsmith.BaseStoneNeeded * (Village.Gunsmith.StoneModification * Village.Gunsmith.Level)).ToString();
                            UpgradeButton.transform.Find("GoldCountLogo").transform.Find("GoldCount").GetComponent<TMP_Text>().text = (Village.Gunsmith.BaseGoldNeeded * (Village.Gunsmith.GoldModification * Village.Gunsmith.Level)).ToString();
                            InteractableTest();
                        }


                        if (Village.Gunsmith.InConstruction)
                        {
                            constructionTimer.SetActive(true);
                            constructionTimer.GetComponent<TimeLeftSliderScript>().Init(VillageManager.GetConstructionTime("Gunsmith"), 86400);
                            SetActiveUpgrade(false);
                        }
                    }
                }
                break;
            case "Warehouse":
                {
                    if (currentUsedBuilding == "Warehouse")
                    {
                        if (Village.Warehouse.Level < 5 && !Village.Warehouse.InConstruction)
                        {
                            SetActiveUpgrade(true);
                            UpgradeButton.transform.Find("WoodCountLogo").transform.Find("WoodCount").GetComponent<TMP_Text>().text = (Village.Warehouse.BaseWoodNeeded * (Village.Warehouse.WoodModification * Village.Warehouse.Level)).ToString();
                            UpgradeButton.transform.Find("StoneCountLogo").transform.Find("StoneCount").GetComponent<TMP_Text>().text = (Village.Warehouse.BaseStoneNeeded * (Village.Warehouse.StoneModification * Village.Warehouse.Level)).ToString();
                            UpgradeButton.transform.Find("GoldCountLogo").transform.Find("GoldCount").GetComponent<TMP_Text>().text = (Village.Warehouse.BaseGoldNeeded * (Village.Warehouse.GoldModification * Village.Warehouse.Level)).ToString();
                            InteractableTest();

                        }

                        if (Village.Warehouse.InConstruction)
                        {
                            constructionTimer.SetActive(true);
                            constructionTimer.GetComponent<TimeLeftSliderScript>().Init(VillageManager.GetConstructionTime("Warehouse"), 86400);
                            SetActiveUpgrade(false);
                        }
                    }
                }
                break;
            case "TrainingCamp":
                {
                    if (currentUsedBuilding == "TrainingCamp")
                    {
                        if (Village.TrainingCamp.Level < 5 && !Village.TrainingCamp.InConstruction)
                        {
                            SetActiveUpgrade(true);
                            UpgradeButton.transform.Find("WoodCountLogo").transform.Find("WoodCount").GetComponent<TMP_Text>().text = (Village.TrainingCamp.BaseWoodNeeded * (Village.TrainingCamp.WoodModification * Village.TrainingCamp.Level)).ToString();
                            UpgradeButton.transform.Find("StoneCountLogo").transform.Find("StoneCount").GetComponent<TMP_Text>().text = (Village.TrainingCamp.BaseStoneNeeded * (Village.TrainingCamp.StoneModification * Village.TrainingCamp.Level)).ToString();
                            UpgradeButton.transform.Find("GoldCountLogo").transform.Find("GoldCount").GetComponent<TMP_Text>().text = (Village.TrainingCamp.BaseGoldNeeded * (Village.TrainingCamp.GoldModification * Village.TrainingCamp.Level)).ToString();
                            InteractableTest();
                        }

                        if (Village.TrainingCamp.InConstruction)
                        {
                            constructionTimer.SetActive(true);
                            constructionTimer.GetComponent<TimeLeftSliderScript>().Init(VillageManager.GetConstructionTime("TrainingCamp"), 86400);
                            SetActiveUpgrade(false);
                        }
                    }
                }
                break;
            case "HealerHut":
                {
                    if (currentUsedBuilding == "HealerHut")
                    {
                        if (Village.HealerHut.Level < 5 && !Village.HealerHut.InConstruction)
                        {
                            SetActiveUpgrade(true);
                            UpgradeButton.transform.Find("WoodCountLogo").transform.Find("WoodCount").GetComponent<TMP_Text>().text = (Village.HealerHut.BaseWoodNeeded * (Village.HealerHut.WoodModification * Village.HealerHut.Level)).ToString();
                            UpgradeButton.transform.Find("StoneCountLogo").transform.Find("StoneCount").GetComponent<TMP_Text>().text = (Village.HealerHut.BaseStoneNeeded * (Village.HealerHut.StoneModification * Village.HealerHut.Level)).ToString();
                            UpgradeButton.transform.Find("GoldCountLogo").transform.Find("GoldCount").GetComponent<TMP_Text>().text = (Village.HealerHut.BaseGoldNeeded * (Village.HealerHut.GoldModification * Village.HealerHut.Level)).ToString();
                            InteractableTest();

                        }

                        if (Village.HealerHut.InConstruction)
                        {
                            constructionTimer.SetActive(true);
                            constructionTimer.GetComponent<TimeLeftSliderScript>().Init(VillageManager.GetConstructionTime("HealerHut"), 86400);
                            SetActiveUpgrade(false);
                        }
                    }
                }
                break;
        }
    }

    /// <summary>
    /// Verify if the player can buy in the gunsmith
    /// </summary>
    /// <returns></returns>
    public bool CanBuy()
    {
        bool result = false;

        int currentGold = GameObject.Find("ATHVillage").transform.Find("Background").Find("GoldSlider").GetComponent<MaterialSliderScript>().GetValue();
        int currentWood = GameObject.Find("ATHVillage").transform.Find("Background").Find("WoodSlider").GetComponent<MaterialSliderScript>().GetValue();
        int currentStone = GameObject.Find("ATHVillage").transform.Find("Background").Find("StoneSlider").GetComponent<MaterialSliderScript>().GetValue();

        int neededWood = int.Parse(UpgradeButton.transform.Find("WoodCountLogo").transform.Find("WoodCount").GetComponent<TMP_Text>().text);
        int neededGold = int.Parse(UpgradeButton.transform.Find("GoldCountLogo").transform.Find("GoldCount").GetComponent<TMP_Text>().text);
        int neededStone = int.Parse(UpgradeButton.transform.Find("StoneCountLogo").transform.Find("StoneCount").GetComponent<TMP_Text>().text);

        if (currentGold >= neededGold &&  currentWood >= neededWood &&  currentStone >= neededStone)
        {
            result = true;
        }


        return result;
    }

    /// <summary>
    /// Get the wood cost of the building
    /// </summary>
    /// <returns>the cost of the wood</returns>
    public int GetWoodBuyValue()
    {
        return int.Parse(UpgradeButton.transform.Find("WoodCountLogo").transform.Find("WoodCount").GetComponent<TMP_Text>().text);
    }

    /// <summary>
    /// Get the gold cost of the building
    /// </summary>
    /// <returns>the cost of the gold</returns>
    public int GetGoldBuyValue()
    {
        return int.Parse(UpgradeButton.transform.Find("GoldCountLogo").transform.Find("GoldCount").GetComponent<TMP_Text>().text);
    }

    /// <summary>
    /// Get the stone cost of the building
    /// </summary>
    /// <returns>the cost of the stone</returns>
    public int GetStoneBuyValue()
    {
        return int.Parse(UpgradeButton.transform.Find("StoneCountLogo").transform.Find("StoneCount").GetComponent<TMP_Text>().text);
    }


    /// <summary>
    /// Active the upgrade button
    /// </summary>
    /// <param name="active"></param>
    private void SetActiveUpgrade(bool active)
    {
        UpgradeButton.gameObject.SetActive(active);
        UpgradeButton.transform.Find("WoodCountLogo").transform.Find("WoodCount").gameObject.SetActive(active);
        UpgradeButton.transform.Find("StoneCountLogo").transform.Find("StoneCount").gameObject.SetActive(active);
        UpgradeButton.transform.Find("GoldCountLogo").transform.Find("GoldCount").gameObject.SetActive(active);
        UpgradeButton.transform.Find("WoodCountLogo").gameObject.SetActive(active);
        UpgradeButton.transform.Find("StoneCountLogo").gameObject.SetActive(active);
        UpgradeButton.transform.Find("GoldCountLogo").gameObject.SetActive(active);
    }


    /// <summary>
    /// Test if the player can click on the upgrade button
    /// </summary>
    private void InteractableTest()
    {
        if (CanBuy())
        {
            SetInteractableUpgrade(true);
        }
        else
        {
            SetInteractableUpgrade(false);
        }
    }

    /// <summary>
    /// Set the interactibility of the upgrade button
    /// </summary>
    /// <param name="interactable">the bool of the interaction</param>
    private void SetInteractableUpgrade(bool interactable)
    {
        UpgradeButton.GetComponent<Button>().interactable = interactable;
    }
}
