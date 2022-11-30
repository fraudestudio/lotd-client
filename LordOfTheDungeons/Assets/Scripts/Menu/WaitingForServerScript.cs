using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingForServerScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator animator;
    public Canvas Canvas;

    private void Awake()
    {
        TemporaryScript.currentUser = "Test";
    }

    void Start()
    {
        animator.SetTrigger("UnZoom");
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void StartAnim()
    {
        animator.SetTrigger("Zoom");
        Canvas.GetComponent<CanvasGroup>().interactable = true;
        Canvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
        Canvas.GetComponent<CanvasGroup>().alpha = 1;

    }


    public void StopAnim()
    {
        Canvas.GetComponent<CanvasGroup>().interactable = false;
        Canvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
        animator.SetTrigger("UnZoom");
    }
}
