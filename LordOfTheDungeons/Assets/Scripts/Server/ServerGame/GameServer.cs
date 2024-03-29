﻿using Assets.Scripts.Server.ServerGame;
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;

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

    private string currentRequest = "";

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
        Debug.Log(streamReader.ReadLine());
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
        Debug.Log(response.Result);
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
        if (response.Result == "STARTED")
        {
            GameState = GameState.STARTING;
            ChangeState("Joueur trouver ! Votre partie va bientôt commencer !");
            response.Dispose();
        }
    }

    /// <summary>
    /// Ask the turn to the server
    /// </summary>
    /// <returns>return the response</returns>
    public string AskTurn()
    {
        string response = streamReader.ReadLine();
        Debug.Log(response);
        return response;
    }

    /// <summary>
    /// Ask the game state to the server and change the "GameState" in function of it
    /// </summary>
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

    /// <summary>
    /// Ask the server if he can move a playable
    /// </summary>
    /// <param name="id">id of the playable</param>
    /// <param name="ligne">line of the destination</param>
    /// <param name="colonne">column of the destination</param>
    /// <returns>the response of the server</returns>
    public bool MovePlayable(int id, int ligne, int colonne)
    {
        bool result = false;

        streamWriter.WriteLine("MOVE " + id + " " + colonne + " " + ligne);
        streamWriter.Flush();

        // If not "NOK", the server accepts 
        if (streamReader.ReadLine() != "NOK")
        {
            result = true;
        }

        return result;
    }

    /// <summary>
    /// Ask for the state
    /// </summary>
    public void AskForState()
    {
        currentRequest = "AskForState";
    }

    /// <summary>
    /// Listen to the server and wait for a response
    /// </summary>
    public void WaitForServerResponse()
    {
        Task<string> whatToDo = streamReader.ReadLineAsync();
        whatToDo.ContinueWith(ReponseToWhatToDo);
    }

    /// <summary>
    /// Get the response when the player sent an instruction
    /// </summary>
    /// <param name="response">the response of the server</param>
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
    /// Reset the resquest
    /// </summary>
    public void ResetRequest()
    {
        currentRequest = "";
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
