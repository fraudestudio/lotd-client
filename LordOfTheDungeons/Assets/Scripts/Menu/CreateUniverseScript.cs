using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreateUniverseScript : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private TMP_InputField nameFeild;
    [SerializeField]
    private  TMP_InputField passwordFeild;
    [SerializeField]
    private TMP_Text error;


    /// <summary>
    /// When the object is clicked, it open the create universe menu
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (nameFeild.text.ToUpper() != nameFeild.text.ToLower() || !string.IsNullOrEmpty(nameFeild.text))
            {
                error.color = new Color(1, 1, 1, 0);
                GameObject.Find("WaitingServer").GetComponent<WaitingForServerScript>().StartAnim();
                GetComponent<ChangeMenuButtonScript>().Go = true;

                if (string.IsNullOrEmpty(passwordFeild.text))
                {
                    Server.CreateUniverse(nameFeild.text, null);
                }
                else
                {
                    Server.CreateUniverse(nameFeild.text, passwordFeild.text);
                }


            }
            else
            {
                error.color = new Color(1, 1, 1, 1);
            }
        }
    }
}
