using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreateUniverseScript : MonoBehaviour, IPointerClickHandler
{

    public TMP_InputField nameFeild;
    public TMP_InputField passwordFeild;
    public TMP_Text error;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (nameFeild.text.ToUpper() != nameFeild.text.ToLower())
            {
                error.color = new Color(1, 1, 1, 0);
                GameObject.Find("WaitingServer").GetComponent<WaitingForServerScript>().StartAnim();
                GetComponent<ChangeMenuButtonScript>().go = true;
                TemporaryScript.Universes.Add(new Universe(nameFeild.text, passwordFeild.text, TemporaryScript.currentUser));
            }
            else
            {
                error.color = new Color(1, 1, 1, 1);
            }
        }
    }
}
