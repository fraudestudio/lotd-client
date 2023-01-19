using Assets.Scripts.Server;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Expedition : MonoBehaviour, IPointerClickHandler
{

    [SerializeField]
    private GameObject waitScreen;

    public void OnPointerClick(PointerEventData eventData)
    {
        // If clicked, go to the expedition waiting
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            waitScreen.GetComponent<CanvasGroup>().interactable = true;
            waitScreen.GetComponent<CanvasGroup>().blocksRaycasts = true;
            waitScreen.GetComponent<CanvasGroup>().alpha = 1;

            GameServer.Instance.ConnectTcpClient();
        }
    }

    private void Update()
    {
        waitScreen.transform.Find("waitingtext").GetComponent<TMP_Text>().text = GameServer.Instance.State;
    }
}
