using Assets.Scripts.Server;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.EventSystems;

public class Expedition : MonoBehaviour, IPointerClickHandler
{

    private int port;

    public int Port { get => port; set => port = value; }

    public TcpClient tcpClient;

    public void OnPointerClick(PointerEventData eventData)
    {
        // If clicked, go to the expedition waiting
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GameServer.Instance.ConnectTcpClient();
        }
    }
}
