using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VillageNameValidation : MonoBehaviour, IPointerClickHandler
{

    public Canvas nameCanvas;
    public Canvas factionCanvas;
    public GameObject factionObjects;
    public Animator animator;

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
            StartCoroutine(changeChoice());
        }
    }


    private IEnumerator changeChoice()
    {
        animator.SetTrigger("transition");
        yield return new WaitForSeconds(0.5f);
        nameCanvas.GetComponent<CanvasGroup>().alpha = 0;
        nameCanvas.GetComponent<CanvasGroup>().interactable = false;
        yield return new WaitForSeconds(0.5f);
        factionCanvas.GetComponent<CanvasGroup>().alpha = 1;
        factionObjects.SetActive(true);
    }

}
