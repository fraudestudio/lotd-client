using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;

public class UniverseButtonScript : MonoBehaviour, IPointerClickHandler
{

    private string universeName;
    public string UniverseName { get => universeName; set => universeName = value; }

    private int id;
    public int Id { get => id; set => id = value; }


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
        universeCanvas = GameObject.Find("UniversesMenu");
        universeSearchCanvas = GameObject.Find("UniversesMenuSearch");
        villageCanvas = GameObject.Find("VillageMenu");
        universeAnimator = universeCanvas.GetComponent<Animator>();
        universeSearchAnimator = universeSearchCanvas.GetComponent<Animator>();
        passwordCanvas = GameObject.Find("UniversePassword");
        passwordAnimator = passwordCanvas.GetComponentInChildren<Animator>();
        villageAnimator = villageCanvas.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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


    private void ShowVillage()
    {
        StartCoroutine(HidePassword());
        StartCoroutine(ChangeMenu());
    }

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

    /*public bool VerifyPassword(string p)
    {
        bool b = false;
        if (p == PasswordString)
        {
            ShowVillage();
            b = true;
        }

        return b;
    }*/
}
