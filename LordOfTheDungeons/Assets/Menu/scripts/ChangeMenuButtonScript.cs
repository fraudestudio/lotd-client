using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeMenuButtonScript : MonoBehaviour, IPointerClickHandler
{

    public Animator transitionMenu;
    public Canvas menuCanvas;
    public Canvas newMenuCanvas;


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
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            StartCoroutine(LoadUniverseMenu());
        }
    }

    private IEnumerator LoadUniverseMenu()
    {
        transitionMenu.SetTrigger("HideMenu");
        menuCanvas.GetComponent<CanvasGroup>().interactable = false;
        yield return new WaitForSeconds(0.8f);
        menuCanvas.GetComponent<CanvasGroup>().alpha = 0;
        transitionMenu.SetTrigger("GoBackIdle");
    }
}
