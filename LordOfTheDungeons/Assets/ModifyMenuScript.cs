using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class ModifyMenuScript : MonoBehaviour
{

    private Tavern tavern;

    public TMP_Text TitleText;
    public TMP_Text DescriptionText;
    public Button UpgradeButton;
    public GameObject constructionTimer;

    private string currentUsedBuilding = "";

    void Start()
    {
        tavern = Village.Tavern;
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
    public void InitMenu(string building, string description,bool isInConstruction = false, int? timeRemaining = null)
    {
        switch (building)
        {
            case "Tavern":
                {
                    currentUsedBuilding = building;
                    TitleText.text = "Tavern";
                    DescriptionText.text = description;
                    
                    /// On regarde si la taverne est améliorable
                    if (tavern.Level < 5)
                    {
                        UpgradeButton.gameObject.SetActive(true);
                        UpgradeButton.transform.Find("WoodCountLogo").transform.Find("WoodCount").gameObject.SetActive(true);
                        UpgradeButton.transform.Find("StoneCountLogo").transform.Find("StoneCount").gameObject.SetActive(true);
                        UpgradeButton.transform.Find("IronCountLogo").transform.Find("IronCount").gameObject.SetActive(true);
                        UpgradeButton.transform.Find("WoodCountLogo").gameObject.SetActive(true);
                        UpgradeButton.transform.Find("StoneCountLogo").gameObject.SetActive(true);
                        UpgradeButton.transform.Find("IronCountLogo").gameObject.SetActive(true);
                        UpgradeButton.transform.Find("WoodCountLogo").transform.Find("WoodCount").GetComponent<TMP_Text>().text = (tavern.BaseWoodNeeded * (tavern.WoodModification * tavern.Level)).ToString();
                        UpgradeButton.transform.Find("StoneCountLogo").transform.Find("StoneCount").GetComponent<TMP_Text>().text = (tavern.BaseStoneNeeded * (tavern.StoneModification * tavern.Level)).ToString();
                        UpgradeButton.transform.Find("IronCountLogo").transform.Find("IronCount").GetComponent<TMP_Text>().text = (tavern.BaseIronNeeded * (tavern.IronModification * tavern.Level)).ToString();

                    }
                    else
                    {
                        UpgradeButton.gameObject.SetActive(false);
                        UpgradeButton.transform.Find("WoodCountLogo").transform.Find("WoodCount").gameObject.SetActive(false);
                        UpgradeButton.transform.Find("StoneCountLogo").transform.Find("StoneCount").gameObject.SetActive(false);
                        UpgradeButton.transform.Find("IronCountLogo").transform.Find("IronCount").gameObject.SetActive(false);
                        UpgradeButton.transform.Find("WoodCountLogo").gameObject.SetActive(false);
                        UpgradeButton.transform.Find("StoneCountLogo").gameObject.SetActive(false);
                        UpgradeButton.transform.Find("IronCountLogo").gameObject.SetActive(false);
                    }

                    /// On vérifie si la tavern est en construction
                    /// Si elle l'est pas on désactive le timer.
                    if (tavern.InConstruction)
                    {
                        constructionTimer.SetActive(true);
                        constructionTimer.GetComponent<TimeLeftSliderScript>().Init(200, 300);
                    }
                    else
                    {
                        constructionTimer.SetActive(false);
                    }



                } break;
        }
    }
}
