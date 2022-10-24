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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            TemporaryScript.Universes.Add(new Universe(nameFeild.text, passwordFeild.text, TemporaryScript.currentUser));
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
