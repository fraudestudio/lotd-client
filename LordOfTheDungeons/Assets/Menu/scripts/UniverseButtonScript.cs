using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.EventSystems;

public class UniverseButtonScript : MonoBehaviour, IPointerClickHandler
{

    private string villageName;

    private bool password;
    public string VillageName { get => villageName; set => villageName = value; }
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
                Debug.Log("feur");
            }
        }
    }

    private IEnumerator ShowPassword()
    {
        passwordAnimator.SetTrigger("Zoom");
        yield return new WaitForSeconds(.5f);
        passwordCanvas.GetComponent<CanvasGroup>().interactable = true;
        passwordCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
        passwordAnimator.SetTrigger("GoBackIdle");
    }

    private IEnumerator HidePassword()
    {
        passwordCanvas.GetComponent<CanvasGroup>().interactable = false;
        passwordCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
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
        yield return new WaitForSeconds(0.3f);
        universeAnimator.SetTrigger("GoBackIdle");
        universeCanvas.GetComponent<CanvasGroup>().alpha = 0;
        universeSearchAnimator.SetTrigger("GoBackIdle");
        universeSearchCanvas.GetComponent<CanvasGroup>().alpha = 0;
        villageAnimator.SetTrigger("GoBackIdle");
        GameObject.Find("VillageMenu").GetComponent<VillageMenuLoaderScript>().CreateVillageButtons();
        GameObject.Find("WaitingServer").GetComponent<WaitingForServerScript>().StopAnim();
        villageCanvas.GetComponent<CanvasGroup>().interactable = true;
        villageCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void VerifyPassword(string p)
    {
        Debug.Log(p);
        ShowVillage();
    }
}
