using Assets.Scripts.Server.ServerGame;
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

    private GameState gameState;

    private string token;

    private int seed;

    private int order;

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
    public int Seed { get => seed; set => seed = value; }
    public GameState GameState { get => gameState; set => gameState = value; }
    public int Order { get => order; set => order = value; }



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
        gameState = GameState.WAITING;
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

            seed = GetIntWithValue(streamReader.ReadLine(),1);
            order = GetIntWithValue(streamReader.ReadLine(),1);

            Task<string> startGame = streamReader.ReadLineAsync();
            startGame.ContinueWith(StartTheGame);
        }
        else
        {
            throw new Exception();
        }
    }

    private int GetIntWithValue(string response,int position)
    {
        Debug.Log(response);
        string[] result = response.Split(' ');
        return int.Parse(result[position]);
    }
    
    /// <summary>
    /// When the server tells to start the game
    /// </summary>
    /// <param name="response"></param>
    private void StartTheGame(Task<string> response)
    {
        Debug.Log(response.Result);
        if (response.Result == "STARTED")
        {
            GameState = GameState.STARTING;
            ChangeState("Joueur trouver ! Votre partie va bientôt commencer !");
        }
    }

    /// <summary>
    /// Change the state of the game server
    /// </summary>
    /// <param name="stateText"></param>
    public void ChangeState(string stateText)
    {
        State = stateText;
    }
}
