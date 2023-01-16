using Assets.Scripts.Server;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VillageMenuLoaderScript : MonoBehaviour
{

    [SerializeField]
    // game object of the join button
    private GameObject joinButton;
    [SerializeField]
    // gme object of the create button
    private GameObject createButton;

    [SerializeField]
    // text state of the village
    private TMP_Text villageState;
    [SerializeField]
    // text of the number of player in the village
    private TMP_Text numberPlayer;
    [SerializeField]
    // text of the number of player connected
    private TMP_Text numberOnline;
    [SerializeField]
    // text of the major faction
    private TMP_Text majorFaction;


    /// <summary>
    /// Change the menu to the universe menu
    /// </summary>
    /// <returns></returns>
    public IEnumerator GoBackToUniverse()
    {
        GameObject universeCanvas = GameObject.Find("UniversesMenu");
        Animator universeAnimator = universeCanvas.GetComponent<Animator>();
        GameObject searchCanvas = GameObject.Find("UniversesMenuSearch");
        Animator searchAnimator = searchCanvas.GetComponent<Animator>();
        GameObject villageCanvas = GameObject.Find("VillageMenu");
        Animator villageAnimator = villageCanvas.GetComponent<Animator>();

        villageCanvas.GetComponent<CanvasGroup>().interactable = false;
        villageCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
        searchCanvas.GetComponent<CanvasGroup>().alpha = 1;
        universeCanvas.GetComponent<CanvasGroup>().alpha = 1;

        universeAnimator.SetTrigger("ShowMenu");
        searchAnimator.SetTrigger("ShowMenu");
        villageAnimator.SetTrigger("HideMenu");

        yield return new WaitForSeconds(0.3f);
        villageCanvas.GetComponent<CanvasGroup>().alpha = 0;
        universeAnimator.SetTrigger("GoBackIdle");
        searchAnimator.SetTrigger("GoBackIdle");
        villageAnimator.SetTrigger("GoBackIdle");
        universeCanvas.GetComponent<CanvasGroup>().interactable = true;
        universeCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
        searchCanvas.GetComponent<CanvasGroup>().interactable = true;
        searchCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    /// <summary>
    /// Change the menu to the universe menu
    /// </summary>
    public void ChangeMenu()
    {
        StartCoroutine(GoBackToUniverse());
    }

    /// <summary>
    /// Update all of the village buttons
    /// </summary>
    /// <param name="idUniverse">id of the universe</param>
    public void UpdateVillageButtons(int idUniverse)
    {
        // The the id for the server
        Server.SaveIdUniverse(idUniverse);


        // Check if the player has a village
        if (Server.UserHasVillageInUniverse(idUniverse))
        {
            joinButton.GetComponent<Button>().interactable = true;
            createButton.GetComponent<Button>().interactable = false;

            villageState.text = "Village : " + Server.UserGetVillageName(Server.UserGetVillageID(idUniverse).Town).Name;
            villageState.fontSize = 20;
            villageState.color = new Color(0, 0.9f, 0);

        }
        else
        {
            joinButton.GetComponent<Button>().interactable = false;
            createButton.GetComponent<Button>().interactable = true;
            villageState.color = new Color(1, 0, 0);

            villageState.text = "Vous n'avez pas de village.";
            villageState.fontSize = 20;
        }


        numberPlayer.text = "Nombre de villages : " + Server.UniverseCountVillage(idUniverse);
        numberPlayer.fontSize = 24;

        numberOnline.text = "Nombre de chef présent : " + 0;

        numberOnline.fontSize = 24;

        majorFaction.text = "Faction majoritaire : " + Server.UniverseGetMajorFaction(idUniverse);
        majorFaction.fontSize = 24;
        majorFaction.color = new Color(1, 1, 0);
    }
}
