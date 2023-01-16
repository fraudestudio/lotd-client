using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PasswordVerifierScript : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    // the inputfeild of the object
    private TMP_InputField inputField;
    [SerializeField]
    // the error message of the object
    private TMP_Text error;

    // the current unvierse selected
    private GameObject currentButton;

    public GameObject CurrentButton { get => currentButton; set => currentButton = value; }


    /// <summary>
    /// Hide the password object
    /// </summary>
    public void HidePassword()
    {
        StartCoroutine(CurrentButton.GetComponent<UniverseButtonScript>().HidePassword());
        error.color = new Color(1, 1, 1, 0);
    }

    /// <summary>
    /// We clicked start to verify the password 
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            verify(inputField.text);
        }
    }

    /// <summary>
    /// Verify the current password
    /// Show a error message if it false
    /// </summary>
    /// <param name="s"></param>
    public void verify(string s)
    {
        if (!CurrentButton.GetComponent<UniverseButtonScript>().VerifyPassword(s))
        {
            error.color = new Color(1, 1, 1, 1);
        }
        else
        {
            error.color = new Color(1, 1, 1, 0);
        }
        inputField.text = "";
    }
}
