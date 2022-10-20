using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VillageButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private string userName = "CHI";

    public string UserName { get => userName; }

    private Transform bubble;


    // Start is called before the first frame update
    void Start()
    {
        bubble = GameObject.Find("VillageMenu").transform.Find("bubblespeach");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<RawImage>().color = new Color(1, 0.5151579f, 0.2688679f);
        bubble.position = new Vector2(transform.position.x, transform.position.y - 200);
        bubble.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<RawImage>().color = new Color(1, 1, 1);
        bubble.GetComponent<CanvasGroup>().alpha = 0;
    }
}
