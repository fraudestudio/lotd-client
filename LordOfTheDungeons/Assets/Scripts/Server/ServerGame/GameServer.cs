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
using UnityEngine.Animations;
using UnityEngine.Rendering;

public class GameServer
{

    // The TCPclient connexion
    private TcpClient tcpClient;
    // The streamReader
    private StreamReader streamReader;
    // the streamWriter
    private StreamWriter streamWriter;

    private static GameServer instance;

    private int port;

    private string state = "Connexion...";

    private GameState gameState;

    private string token;

    private int seed;

    private int order;

    private string currentRequest;

    /// <summary>
    /// The token of the player
    /// </summary>
    public string Token { get => token; set => token = value; }
    /// <summary>
    /// The string state of the gameserver
    /// </summary>
    public string State { get => state; set => state = value; }
    /// <summary>
    /// The seed sent by the server
    /// </summary>
    public int Seed { get => seed; set => seed = value; }
    /// <summary>
    /// The enum gamestate of the server
    /// </summary>
    public GameState GameState { get => gameState; set => gameState = value; }
    /// <summary>
    /// The order of the player sent by the server
    /// </summary>
    public int Order { get => order; set => order = value; }


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

    /// <summary>
    /// When the server send a value with a name and an int
    /// EX : 'seed 123' 
    /// </summary>
    /// <param name="response"></param>
    /// <param name="position"></param>
    /// <returns></returns>
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

    public string AskTurn()
    {
        return streamReader.ReadLine();
    }

    public void AskGameState()
    {
        if (streamReader.ReadLine() == "PLAY")
        {
            GameState = GameState.PLAYING;
        }
        else
        {
            GameState = GameState.WAITING;
        }
    }


    public bool MovePlayable(int id, int ligne, int colonne)
    {
        bool result = false;

        streamWriter.WriteLine("MOVE " + id + " " + colonne + " " + ligne);
        if (streamReader.ReadLine() != "NOK")
        {
            result = true;
        }

        return result;
    }

    public void WaitForServerResponse()
    {
        Task<string> whatToDo = streamReader.ReadLineAsync();
        whatToDo.ContinueWith(ReponseToWhatToDo);
        currentRequest = "NULL";
    }

    private void ReponseToWhatToDo(Task<string> response)
    {
        currentRequest = response.Result;
    }

    /// <summary>
    /// Return the current resquest of the server
    /// </summary>
    /// <returns>the current server request</returns>
    public string GetCurrentRequest()
    {
        return currentRequest;
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
