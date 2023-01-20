using Assets.Scripts.Server;
using Assets.Scripts.Server.ServerGame;
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

    /// <summary>
    /// When the player click it start the connexion the with the game server
    /// </summary>
    /// <param name="eventData"></param>
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

    /// <summary>
    /// Show the state of the server and when the game start, ask to load the dungeon
    /// </summary>
    private void Update()
    {
        waitScreen.transform.Find("waitingtext").GetComponent<TMP_Text>().text = GameServer.Instance.State;

        if (GameServer.Instance.GameState == GameState.STARTING)
        {
            StartCoroutine(GoToDungeon());
            GameServer.Instance.GameState = GameState.WAITING;

        }
    }

    /// <summary>
    /// Wait 5 seconds and go to the dungeon
    /// </summary>
    /// <returns></returns>
    private IEnumerator GoToDungeon()
    {
        yield return new WaitForSeconds(5);
        GameObject.Find("loadscreen").GetComponent<loaderScript>().Level("RoomDungeon");

    }
}
