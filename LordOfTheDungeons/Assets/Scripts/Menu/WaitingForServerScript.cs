using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingForServerScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    // Animator of the pop up 
    private Animator animator;
    [SerializeField]
    // Canvas of the text
    private Canvas Canvas;

    void Start()
    {
        animator.SetTrigger("UnZoom");
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// Start the animation
    /// </summary>
    public void StartAnim()
    {
        animator.SetTrigger("Zoom");
        Canvas.GetComponent<CanvasGroup>().interactable = true;
        Canvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
        Canvas.GetComponent<CanvasGroup>().alpha = 1;

    }

    /// <summary>
    /// Stop the animation
    /// </summary>
    public void StopAnim()
    {
        Canvas.GetComponent<CanvasGroup>().interactable = false;
        Canvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
        animator.SetTrigger("UnZoom");
    }
}
