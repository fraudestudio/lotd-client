using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuitButtonScript : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// When clicked, quit the application
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Application.Quit();
        }
    }
}
