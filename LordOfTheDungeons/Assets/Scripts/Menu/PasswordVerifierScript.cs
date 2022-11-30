using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PasswordVerifierScript : MonoBehaviour, IPointerClickHandler
{
    public TMP_InputField inputField;
    public TMP_Text error;
    private GameObject currentButton;

    public GameObject CurrentButton { get => currentButton; set => currentButton = value; }


    public void HidePassword()
    {
        StartCoroutine(CurrentButton.GetComponent<UniverseButtonScript>().HidePassword());
        error.color = new Color(1, 1, 1, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            verify(inputField.text);
        }
    }

    public void verify(string s)
    {
        /*if (!CurrentButton.GetComponent<UniverseButtonScript>().VerifyPassword(s))
        {
            error.color = new Color(1, 1, 1, 1);
        }
        else
        {
            error.color = new Color(1, 1, 1, 0);
        }*/
    }
}
