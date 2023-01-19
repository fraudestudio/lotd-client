     using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEditor.Compilation;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameServer
{
    private TcpClient tcpClient;
    private StreamReader streamReader;
    private StreamWriter streamWriter;

    private static GameServer instance;

    private int port;

    private string state = "Connexion...";

    private string token;

    public string Token { get => token; set => token = value; }

    public static GameServer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameServer();
            }
            return instance;
        }
    }

    public string State { get => state; set => state = value; }



    /// <summary>
    /// Connect to the tcp client
    /// </summary>
    public void ConnectTcpClient()
    {
        port = Server.GoExpedition(Server.GetCurrentVillage());
        tcpClient = new TcpClient("10.128.120.128", port);
        tcpClient.NoDelay = true;
        streamReader = new StreamReader(tcpClient.GetStream());
        streamWriter = new StreamWriter(tcpClient.GetStream());
        Debug.Log(streamReader.ReadLine());
        Debug.Log(port);
        streamWriter.WriteLine("AUTH " + token);
        streamWriter.Flush();
        Task<string> response = streamReader.ReadLineAsync();
        response.ContinueWith(IsOk);
    }

    /// <summary>
    /// Tells if the server is good for searching a player 
    /// </summary>
    /// <param name="response"></param>
    private void IsOk(Task<string> response)
    {
        if (response.Result == "OK")
        {
            ChangeState("En recherche d'un joueur...");
            Task<string> startGame = streamReader.ReadLineAsync();
            startGame.ContinueWith(StartTheGame);
        }
        else
        {
            throw new Exception();
        }
    }

    /// <summary>
    /// When the server tells to start the game
    /// </summary>
    /// <param name="response"></param>
    private void StartTheGame(Task<string> response)
    {
        if (response.Result == "START")
        {
            ChangeState("Joueur trouver ! Votre partie va bientôt commencer !");
        }
    }

    public void ChangeState(string stateText)
    {
        State = stateText;
    }
}
