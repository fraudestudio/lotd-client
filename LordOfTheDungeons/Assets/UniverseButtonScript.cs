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
    private Animator passwordAnimator;


    // Start is called before the first frame update
    void Start()
    {
        passwordCanvas = GameObject.Find("UniversePassword");
        passwordAnimator = passwordCanvas.GetComponentInChildren<Animator>();
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
    }

    public void VerifyPassword(string p)
    {
        Debug.Log(p);
        ShowVillage();
    }
}
