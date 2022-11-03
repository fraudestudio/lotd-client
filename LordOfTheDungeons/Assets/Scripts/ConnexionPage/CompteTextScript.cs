using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CompteTextScript : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = transform.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var linkIndex = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, null);

        string linkId = text.textInfo.linkInfo[linkIndex].GetLinkID();


        switch (linkId)
        {
            case "id": Application.OpenURL("google.com"); break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var linkIndex = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, null);

        string linkId = text.textInfo.linkInfo[linkIndex].GetLinkID();


        switch (linkId)
        {
            case "id": text.color = new Color(1,0,0); break;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = new Color(1, 1, 1);
    }
}
