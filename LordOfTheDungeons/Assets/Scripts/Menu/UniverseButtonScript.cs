using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;

public class UniverseButtonScript : MonoBehaviour, IPointerClickHandler
{
    // name of the universe
    private string universeName;
    public string UniverseName { get => universeName; set => universeName = value; }

    // id of the universe
    private int id;
    public int Id { get => id; set => id = value; }


    // bool to know if it had a password
    private bool password;
    public bool Password { get => password; set => password = value; }



    private GameObject passwordCanvas;
    private GameObject universeCanvas;
    private GameObject universeSearchCanvas;
    private GameObject villageCanvas;
    private Animator passwordAnimator;
    private Animator universeAnimator;
    private Animator universeSearchAnimator;
    private Animator villageAnimator;


    // Start is called before the first frame update
    void Start()
    {
        // Get all the wanted game objets
        universeCanvas = GameObject.Find("UniversesMenu");
        universeSearchCanvas = GameObject.Find("UniversesMenuSearch");
        villageCanvas = GameObject.Find("VillageMenu");
        universeAnimator = universeCanvas.GetComponent<Animator>();
        universeSearchAnimator = universeSearchCanvas.GetComponent<Animator>();
        passwordCanvas = GameObject.Find("UniversePassword");
        passwordAnimator = passwordCanvas.GetComponentInChildren<Animator>();
        villageAnimator = villageCanvas.GetComponent<Animator>();
    }


    /// <summary>
    /// When clicked, check if it has a password, 
    /// if yes, show the password object
    /// if not, show the village 
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Password)
            {
                passwordCanvas.GetComponent<CanvasGroup>().alpha = 1;
                passwordCanvas.GetComponentInChildren<PasswordVerifierScript>().CurrentButton = gameObject;
                StartCoroutine(ShowPassword());
            }
            else
            {
                ShowVillage();
            }
        }
    }

    /// <summary>
    /// Show the password menu
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShowPassword()
    {
        passwordAnimator.SetTrigger("Zoom");
        yield return new WaitForSeconds(.5f);
        passwordCanvas.GetComponent<CanvasGroup>().interactable = true;
        passwordCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
        universeCanvas.GetComponent<CanvasGroup>().interactable = false;
        universeCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;        
        universeSearchCanvas.GetComponent<CanvasGroup>().interactable = false;
        universeSearchCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
        passwordAnimator.SetTrigger("GoBackIdle");
    }

    /// <summary>
    /// Hide the password menu
    /// </summary>
    /// <returns></returns>
    public IEnumerator HidePassword()
    {
        passwordCanvas.GetComponent<CanvasGroup>().interactable = false;
        passwordCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
        universeCanvas.GetComponent<CanvasGroup>().interactable = true;
        universeCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
        universeSearchCanvas.GetComponent<CanvasGroup>().interactable = true;
        universeSearchCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
        passwordAnimator.SetTrigger("UnZoom");
        yield return new WaitForSeconds(0.3f);
        passwordCanvas.GetComponent<CanvasGroup>().alpha = 0;
        passwordAnimator.SetTrigger("GoBackIdle");
    }

    /// <summary>
    /// Show the village menu 
    /// </summary>
    private void ShowVillage()
    {
        GameObject.Find("VillageMenu").transform.Find("JoinVillageButton").GetComponent<JoinVillageButtonScript>().CurrentUniverse = id;
        StartCoroutine(HidePassword());
        StartCoroutine(ChangeMenu());
    }

    /// <summary>
    /// Chnage the menu to the universe menu
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChangeMenu()
    {
        universeCanvas.GetComponent<CanvasGroup>().interactable = false;
        universeCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
        universeAnimator.SetTrigger("HideMenu");
        universeSearchCanvas.GetComponent<CanvasGroup>().interactable = false;
        universeSearchCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
        universeSearchAnimator.SetTrigger("HideMenu");
        villageCanvas.GetComponent<CanvasGroup>().alpha = 1;
        villageAnimator.SetTrigger("ShowMenu");
        GameObject.Find("WaitingServer").GetComponent<WaitingForServerScript>().StartAnim();
        yield return new WaitForSeconds(0.5f);
        universeAnimator.SetTrigger("GoBackIdle");
        villageAnimator.SetTrigger("GoBackIdle");
        universeCanvas.GetComponent<CanvasGroup>().alpha = 0;
        universeSearchAnimator.SetTrigger("GoBackIdle");
        universeSearchCanvas.GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("VillageMenu").GetComponent<VillageMenuLoaderScript>().UpdateVillageButtons(id);
        GameObject.Find("WaitingServer").GetComponent<WaitingForServerScript>().StopAnim();
        villageCanvas.GetComponent<CanvasGroup>().interactable = true;
        villageCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;

    }

    /// <summary>
    /// Verify if the password given is the good one
    /// </summary>
    /// <param name="password">the password entered</param>
    /// <returns>the result of the verification</returns>
    public bool VerifyPassword(string password)
    {
        bool b = false;

       
        if (Server.VerifyAcessUniverse(Id, password))
        {
            ShowVillage();
            b = true;
        }

        return b;
    }
}
