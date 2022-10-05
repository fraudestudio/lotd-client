using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CompteTextScript : MonoBehaviour
{

    TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = transform.GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseClick()
    {
        var linkIndex = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, null);

        string linkId = text.textInfo.linkInfo[linkIndex].GetLinkID();


        switch (linkId)
        {
            case "id": Application.OpenURL("google.com"); break;
        }

    }
}
