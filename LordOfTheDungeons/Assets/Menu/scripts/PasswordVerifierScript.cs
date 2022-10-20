using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PasswordVerifierScript : MonoBehaviour, IPointerClickHandler
{
    public TMP_InputField inputField;
    private GameObject currentButton;

    public GameObject CurrentButton { get => currentButton; set => currentButton = value; }

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
            verify(inputField.text);
        }
    }

    public void verify(string s)
    {
        CurrentButton.GetComponent<UniverseButtonScript>().VerifyPassword(s);
    }
}
