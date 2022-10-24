using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VillageMenuLoaderScript : MonoBehaviour
{

    private bool stop = false;

    private Universe currentUniverse;
    public Universe CurrentUniverse { get => currentUniverse; set => currentUniverse = value; }


    public GameObject joinButton;
    public GameObject createButton;

    public TMP_Text villageState;
    public TMP_Text numberPlayer;
    public TMP_Text numberOnline;
    public TMP_Text majorFaction;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


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

    public void ChangeMenu()
    {
        StartCoroutine(GoBackToUniverse());
    }

    public void UpdateVillageButtons()
    {

        if (currentUniverse.Users.Contains(TemporaryScript.currentUser))
        {
            joinButton.GetComponent<Button>().interactable = true;
            createButton.GetComponent<Button>().interactable = false;

            villageState.text = "Village : " + currentUniverse.GetVillage(TemporaryScript.currentUser).Name;
            villageState.fontSize = 26;
            villageState.color = new Color(0, 0.9f, 0);

        }
        else
        {
            joinButton.GetComponent<Button>().interactable = false;
            createButton.GetComponent<Button>().interactable = true;
            villageState.color = new Color(1, 0, 0);

            villageState.text = "Vous n'avez pas de village.";
            villageState.fontSize = 28;
        }


        numberPlayer.text = "Nombre de villages : " + currentUniverse.Users.Count;
        numberPlayer.fontSize = 30;

        numberOnline.text = "Nombre de chef present : " + 0;

        numberOnline.fontSize = 28;

        majorFaction.text = "Faction majoritaire : " + currentUniverse.GetMajorFaction();
        majorFaction.fontSize = 28;
        majorFaction.color = new Color(1, 1, 0);
    }
}
