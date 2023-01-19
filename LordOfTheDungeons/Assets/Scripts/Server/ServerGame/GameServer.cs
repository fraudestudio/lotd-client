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


    public void IsOk(Task<string> response)
    {
        if (response.Result == "OK")
        {
            GameObject.Find("WaitingRoom").transform.Find("waitingtext").GetComponent<TMP_Text>().text = "Partie trouvé\n En attente de votre coéquipier...";
        }
    }
}
