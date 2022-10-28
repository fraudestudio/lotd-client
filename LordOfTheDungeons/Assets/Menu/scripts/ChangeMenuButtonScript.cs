using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeMenuButtonScript : MonoBehaviour, IPointerClickHandler
{

    public Animator transitionMenu;
    public Animator transitionNewMenu;
    public Animator transitionMenuOptional;
    public Animator scdMenuOptional;
    public Canvas menuCanvas;
    public Canvas newMenuCanvas;
    public Canvas newMenuCanvasOptional;
    public bool go = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (menuCanvas.GetComponent<CanvasGroup>().interactable == true)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (go)
                {
                    StartCoroutine(LoadUniverseMenu());
                }
            }
        }
    }

    private IEnumerator LoadUniverseMenu()
    {
        transitionMenu.SetTrigger("HideMenu");

        if (scdMenuOptional != null)
        {
            scdMenuOptional.SetTrigger("HideMenu");
            newMenuCanvasOptional.GetComponent<CanvasGroup>().interactable = false;
            newMenuCanvasOptional.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        menuCanvas.GetComponent<CanvasGroup>().interactable = false;
        menuCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
        yield return new WaitForSeconds(0.5f);
        menuCanvas.GetComponent<CanvasGroup>().alpha = 0;

        if (newMenuCanvasOptional != null)
        {
            newMenuCanvasOptional.GetComponent<CanvasGroup>().alpha = 0;
        }

        if (scdMenuOptional != null)
        {
            scdMenuOptional.SetTrigger("GoBackIdle");
        }

        transitionMenu.SetTrigger("GoBackIdle");

        transitionNewMenu.SetTrigger("ShowMenu");
        if (transitionMenuOptional != null)
        {
            transitionMenuOptional.SetTrigger("ShowMenu");
        }

        newMenuCanvas.GetComponent<CanvasGroup>().alpha = 1;

        if (newMenuCanvasOptional != null)
        {
            if (scdMenuOptional == null)
            {
                newMenuCanvasOptional.GetComponent<CanvasGroup>().alpha = 1;
            }

        }


        yield return new WaitForSeconds(0.4f);
        if (transitionMenuOptional != null)
        {
            transitionMenuOptional.SetTrigger("GoBackIdle");
        }




        transitionNewMenu.SetTrigger("GoBackIdle");

        if (newMenuCanvas.name == "UniversesMenu")
        {
            newMenuCanvas.GetComponent<UniverseGetInfoScript>().GetUniverses();
        }


        if (newMenuCanvasOptional != null)
        {
            if (scdMenuOptional == null)
            {
                newMenuCanvasOptional.GetComponent<CanvasGroup>().interactable = true;
                newMenuCanvasOptional.GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
        }
        newMenuCanvas.GetComponent<CanvasGroup>().interactable = true;
        newMenuCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;



    }
}
