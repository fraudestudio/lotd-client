using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class searchUniverseScript : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    // The input field of the search button 
    private TMP_InputField searchButton;

    /// <summary>
    /// When clicked, it search the 
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GameObject.Find("UniversesMenu").GetComponent<UniverseGetInfoScript>().SearchUniverses(searchButton.text);
        }
    }
}
