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


    private void IsOk(Task<string> response)
    {
        if (response.Result == "OK")
        {
            ChangeState("En recherche d'un joueur...");
        }
    }

    public void ChangeState(string stateText)
    {
        State = stateText;
    }
}
