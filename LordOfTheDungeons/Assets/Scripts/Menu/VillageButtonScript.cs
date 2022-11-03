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
    private Transform factionText;
    private Transform userText;


    private int posX;
    private int posY;
    public int PosX { get => posX; set => posX = value; }
    public int PosY { get => posY; set => posY = value; }

    // Start is called before the first frame update
    void Start()
    {
        bubble = GameObject.Find("VillageMenu").transform.Find("bubblespeach");
        factionText = bubble.GetChild(0);
        userText = bubble.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<RawImage>().color = new Color(1, 0.5151579f, 0.2688679f);

        if (posY == 2)
        {
            factionText.transform.position = new Vector2(0, 16);
            userText.transform.position = new Vector2(0, 39.2f);
            bubble.GetComponent<RawImage>().transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            bubble.position = new Vector2(transform.position.x, transform.position.y + 200);
            bubble.GetComponent<CanvasGroup>().alpha = 1;
        }
        else
        {
            factionText.transform.position = new Vector2(0, -16);
            userText.transform.position = new Vector2(0, -39.2f);
            bubble.GetComponent<RawImage>().transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            bubble.position = new Vector2(transform.position.x, transform.position.y - 200);
            bubble.GetComponent<CanvasGroup>().alpha = 1;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<RawImage>().color = new Color(1, 1, 1);
        bubble.GetComponent<CanvasGroup>().alpha = 0;
    }
}
