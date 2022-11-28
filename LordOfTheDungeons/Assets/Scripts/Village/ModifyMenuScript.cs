using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class ModifyMenuScript : MonoBehaviour
{

    public TMP_Text TitleText;
    public TMP_Text DescriptionText;
    public Button UpgradeButton;
    public GameObject constructionTimer;

    private string currentUsedBuilding = "";

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


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
                    
                    /// On regarde si la taverne est améliorable
                    if (Village.Tavern.Level < 5)
                    {
                        SetActiveUpgrade(true);
                        UpgradeButton.transform.Find("WoodCountLogo").transform.Find("WoodCount").GetComponent<TMP_Text>().text = (Village.Tavern.BaseWoodNeeded * (Village.Tavern.WoodModification * Village.Tavern.Level)).ToString();
                        UpgradeButton.transform.Find("StoneCountLogo").transform.Find("StoneCount").GetComponent<TMP_Text>().text = (Village.Tavern.BaseStoneNeeded * (Village.Tavern.StoneModification * Village.Tavern.Level)).ToString();
                        UpgradeButton.transform.Find("IronCountLogo").transform.Find("IronCount").GetComponent<TMP_Text>().text = (Village.Tavern.BaseIronNeeded * (Village.Tavern.IronModification * Village.Tavern.Level)).ToString();

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
                        constructionTimer.GetComponent<TimeLeftSliderScript>().Init(200, 300);
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
                    if (Village.Gunsmith.Level < 5)
                    {
                        SetActiveUpgrade(true);
                        UpgradeButton.transform.Find("WoodCountLogo").transform.Find("WoodCount").GetComponent<TMP_Text>().text = (Village.Gunsmith.BaseWoodNeeded * (Village.Gunsmith.WoodModification * Village.Gunsmith.Level)).ToString();
                        UpgradeButton.transform.Find("StoneCountLogo").transform.Find("StoneCount").GetComponent<TMP_Text>().text = (Village.Gunsmith.BaseStoneNeeded * (Village.Gunsmith.StoneModification * Village.Gunsmith.Level)).ToString();
                        UpgradeButton.transform.Find("IronCountLogo").transform.Find("IronCount").GetComponent<TMP_Text>().text = (Village.Gunsmith.BaseIronNeeded * (Village.Gunsmith.IronModification * Village.Gunsmith.Level)).ToString();

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
                        constructionTimer.GetComponent<TimeLeftSliderScript>().Init(200, 300);
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
                    if (Village.Warehouse.Level < 5)
                    {
                        SetActiveUpgrade(true);
                        UpgradeButton.transform.Find("WoodCountLogo").transform.Find("WoodCount").GetComponent<TMP_Text>().text = (Village.Warehouse.BaseWoodNeeded * (Village.Warehouse.WoodModification * Village.Warehouse.Level)).ToString();
                        UpgradeButton.transform.Find("StoneCountLogo").transform.Find("StoneCount").GetComponent<TMP_Text>().text = (Village.Warehouse.BaseStoneNeeded * (Village.Warehouse.StoneModification * Village.Warehouse.Level)).ToString();
                        UpgradeButton.transform.Find("IronCountLogo").transform.Find("IronCount").GetComponent<TMP_Text>().text = (Village.Warehouse.BaseIronNeeded * (Village.Warehouse.IronModification * Village.Warehouse.Level)).ToString();

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
                        constructionTimer.GetComponent<TimeLeftSliderScript>().Init(200, 300);
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

                    if (Village.TrainingCamp.Level < 5)
                    {
                        SetActiveUpgrade(true);
                        UpgradeButton.transform.Find("WoodCountLogo").transform.Find("WoodCount").GetComponent<TMP_Text>().text = (Village.TrainingCamp.BaseWoodNeeded * (Village.TrainingCamp.WoodModification * Village.TrainingCamp.Level)).ToString();
                        UpgradeButton.transform.Find("StoneCountLogo").transform.Find("StoneCount").GetComponent<TMP_Text>().text = (Village.TrainingCamp.BaseStoneNeeded * (Village.TrainingCamp.StoneModification * Village.TrainingCamp.Level)).ToString();
                        UpgradeButton.transform.Find("IronCountLogo").transform.Find("IronCount").GetComponent<TMP_Text>().text = (Village.TrainingCamp.BaseIronNeeded * (Village.TrainingCamp.IronModification * Village.Warehouse.Level)).ToString();

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
                        constructionTimer.GetComponent<TimeLeftSliderScript>().Init(200, 300);
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


    private void SetActiveUpgrade(bool active)
    {
        if (active)
        {
            UpgradeButton.gameObject.SetActive(active);
            UpgradeButton.transform.Find("WoodCountLogo").transform.Find("WoodCount").gameObject.SetActive(active);
            UpgradeButton.transform.Find("StoneCountLogo").transform.Find("StoneCount").gameObject.SetActive(active);
            UpgradeButton.transform.Find("IronCountLogo").transform.Find("IronCount").gameObject.SetActive(active);
            UpgradeButton.transform.Find("WoodCountLogo").gameObject.SetActive(active);
            UpgradeButton.transform.Find("StoneCountLogo").gameObject.SetActive(active);
            UpgradeButton.transform.Find("IronCountLogo").gameObject.SetActive(active);
        }
    }
}
