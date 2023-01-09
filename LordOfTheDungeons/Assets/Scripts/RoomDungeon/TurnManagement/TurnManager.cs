using Assets.Scripts.RoomDungeon.TurnManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnManager : MonoBehaviour
{

    [SerializeField]
    // The team of the current player
    private TypeTurn currentPlayer;
    public TypeTurn CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }



    // Current turn of the game
    [SerializeField]
    private TypeTurn currentTurn;
    public TypeTurn CurrentTurn { get { DisplayIsYourTurn(); return currentTurn; } set => currentTurn = value; }


    // Name of the player one
    private string playerOneName = "PlayerOne";
    // Name of the player two
    private string playerTwoName = "PlayerTwo";

    // Tells the time the player has to play
    private int timeRemaining = 90;

    private bool canCountTime = false;

    // If the application is paused, we saved the current time
    private DateTime pausedDateTime;

    [SerializeField]
    private GameObject turnDisplayText;

    [SerializeField]
    private TMP_Text timeRemainingText;

    [SerializeField]
    private GameObject yourTurnText;

    /// <summary>
    /// Start to manage who has the turn
    /// </summary>
    public void StartManage()
    {

        DisplayTurn();
    }

    /// <summary>
    /// Display to the player screen wich turn it is
    /// </summary>
    public void DisplayTurn()
    {
        DisplayIsYourTurn();
        string baseTurnText = "AU TOUR DE";
        switch (currentTurn)
        {
            case TypeTurn.Player_1:
                {
                    turnDisplayText.GetComponent<TMP_Text>().color = Color.blue;
                    turnDisplayText.GetComponent<TMP_Text>().text = baseTurnText + " " + playerOneName + " !";
                }
                break;
            case TypeTurn.Player_2:
                {
                    turnDisplayText.GetComponent<TMP_Text>().color = Color.yellow;
                    turnDisplayText.GetComponent<TMP_Text>().text = baseTurnText + " " + playerTwoName + " !";
                }
                break;
            case TypeTurn.Enemy:
                {
                    turnDisplayText.GetComponent<TMP_Text>().color = Color.red;
                    turnDisplayText.GetComponent<TMP_Text>().text = baseTurnText + "S ADVERSAIRES!";
                }
                break;
        }
        turnDisplayText.GetComponent<Animator>().SetTrigger("StartAnim");
        StartTimer();
    }


    /// <summary>
    /// Start to count the time remaining for the player in his turn
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartCountTurn()
    {
        while (canCountTime)
        {
            // Every second we add one
            yield return new WaitForSeconds(1f);
            timeRemaining -= 1;
            ChangeTimeText();
            // We stop the timer if the time is 0
            if (timeRemaining <= 0)
            {
                canCountTime = false;
            }
        }
    }

    /// <summary>
    /// Start the timer of the player
    /// </summary>
    private void StartTimer()
    {
        timeRemaining = 90;
        canCountTime = true;
        ChangeTimeText();
        StartCoroutine(StartCountTurn());
    }


    /// <summary>
    /// When the application is stopped, we saved the time
    /// When it is up, it rewrite the time that has been passed
    /// </summary>
    /// <param name="pause"></param>
    private void OnApplicationPause(bool pause)
    {
        if (canCountTime)
        {
            if (pause)
            {
                pausedDateTime = DateTime.Now;
            }
            else
            {
                timeRemaining -= Convert.ToInt32((DateTime.Now - pausedDateTime).TotalSeconds);
                ChangeTimeText();
            }
        }

    }


    /// <summary>
    /// Set the players name
    /// </summary>
    /// <param name="playerOneName"></param>
    /// <param name="playerTwoName"></param>
    public void SetNames(string playerOneName, string playerTwoName)
    {
        this.playerOneName = playerOneName;
        this.playerTwoName = playerTwoName;
    }

    /// <summary>
    /// Change the text of the remaining time
    /// </summary>
    private void ChangeTimeText()
    {
        timeRemainingText.text = "Temps restant : " + timeRemaining + " secondes";
    }

    /// <summary>
    /// Display if it is your turn
    /// </summary>
    private void DisplayIsYourTurn()
    {
        if (currentPlayer == currentTurn)
        {
            yourTurnText.GetComponent<TMP_Text>().text = "C'est votre tour !";
            yourTurnText.GetComponent<TMP_Text>().color = new Color(1, 1, 0, 1);
        }
        else
        {
            yourTurnText.GetComponent<TMP_Text>().text = "Tour de " + currentTurn.ToString();
            yourTurnText.GetComponent<TMP_Text>().color = new Color(1, 1, 0, 1);
        }
    }
}
