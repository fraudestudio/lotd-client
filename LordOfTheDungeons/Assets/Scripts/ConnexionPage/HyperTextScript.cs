using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HyperTextScript : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    public string url;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Application.OpenURL(url);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<TMP_Text>().color = new Color(1, 0, 0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<TMP_Text>().color = new Color(1, 1, 1);
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
