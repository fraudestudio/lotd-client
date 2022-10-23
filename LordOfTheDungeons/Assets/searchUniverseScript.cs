using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class searchUniverseScript : MonoBehaviour, IPointerClickHandler
{

    public TMP_InputField searchButton;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GameObject.Find("UniversesMenu").GetComponent<UniverseGetInfoScript>().SearchUniverses(searchButton.text);
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
