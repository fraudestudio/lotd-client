using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeMenuButtonScript : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    // transition menu animator 
    private Animator transitionMenu;
    [SerializeField]
    // the new menu transition 
    private Animator transitionNewMenu;
    [SerializeField]
    // transition menu optional 
    private Animator transitionMenuOptional;   
    [SerializeField]
    // second old menu (optional)
    private Animator scdMenuOptional;
    [SerializeField]
    // the old menu canvas
    private Canvas menuCanvas;
    [SerializeField]
    // the new menu canvas 
    private Canvas newMenuCanvas;
    [SerializeField]
    // the second new menu canvas (optional change)
    private Canvas newMenuCanvasOptional;
    [SerializeField]
    // bool that tell if the menu can change
    private bool go = true;

    public bool Go { get => go; set => go = value; }

    /// <summary>
    /// When you click on the button, it change of menu 
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (menuCanvas.GetComponent<CanvasGroup>().interactable == true)
        {
            // When it is clicked
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                // if the menu can change
                if (go)
                {
                    StartCoroutine(LoadUniverseMenu());
                }
            }
        }
    }

    /// <summary>
    /// Change to the universe menu 
    /// </summary>
    /// <returns></returns>
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
