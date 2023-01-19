using Assets.Scripts.RoomDungeon.Characters.Selection;
using Assets.Scripts.RoomDungeon.TurnManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacterScript : MonoBehaviour
{
    // The movement of the player 
    private int movement = 5;
    public int Movement { get => movement; set => movement = value; }
    
    // Action point of the player
    private int action = 2;
    public int Action { get => action; set => action = value; }
    
    // Power of the player
    private int power = 15;
    public int Power { get => power; }


    // Team of the player
    private TypeTurn team;

    // The base color of the playable
    private Color baseColor;

    // The hover color of the playable
    private Color hoverColor;

    
    private GameObject turnManager;
    private GameObject selectionTileManager;
    private GameObject playerActionManager;

    public void Start()
    {
        turnManager = GameObject.Find("TurnManager");
        playerActionManager = GameObject.Find("PlayerActionManager");
        selectionTileManager = GameObject.Find("SelectionTileManager");

    }

    /// <summary>
    /// When the mouse enter, we turn the player in a gray tint 
    /// </summary>
    private void OnMouseEnter()
    {
        if (playerActionManager.GetComponent<PlayerActionManager>().CanDoAnything)
        {
            gameObject.GetComponent<SpriteRenderer>().color = hoverColor;
        }
    }

    /// <summary>
    /// When the mouse the mouse exit, we turn the player in its normal color
    /// </summary>
    private void OnMouseExit()
    {
        if (playerActionManager.GetComponent<PlayerActionManager>().CanDoAnything)
        {
            gameObject.GetComponent<SpriteRenderer>().color = baseColor;
        }
    }

    /// <summary>
    /// On click, we create the selection tile 
    /// </summary>
    private void OnMouseUp()
    {
        if (VerifyCanUse() && playerActionManager.GetComponent<PlayerActionManager>().CanDoAnything)
        {
            playerActionManager.GetComponent<PlayerActionManager>().ShowAttackATH(true);
            selectionTileManager.GetComponent<SelectionTileManager>().CreateSelectionTiles(movement,TypeSelection.Deplacement, gameObject);
        }
    }

    /// <summary>
    /// Change the team of the playable
    /// </summary>
    /// <param name="teamId">the team id</param>
    public void ChangeTeam(int teamId)
    {
        switch (teamId)
        {
            case 0:
                {
                    team = TypeTurn.Player_1;
                    ChangeColorTeam();
                } 
                break;
            case 1:
                {
                    team = TypeTurn.Player_2;
                    ChangeColorTeam();
                } 
                break;
        }
    }

    /// <summary>
    /// Change the color of the playable in function of his team
    /// </summary>
    /// <param name="teamId"></param>
    private void ChangeColorTeam()
    {
        switch (team)
        {
            case TypeTurn.Player_1:
                {
                    baseColor = new Color(0.241545f, 0.5954974f, 0.8679245f, 1f);
                    hoverColor = new Color(0.4008989f, 0.659913f, 0.8584906f, 1f);
                    SetColor();
                }
                break;
            case TypeTurn.Player_2:
                {
                    baseColor = new Color(0.8727888f, 0.8679245f, 0.006289184f,1f);
                    hoverColor = new Color(0.8047742f, 0.8584906f, 0.3034887f, 1f);
                    SetColor();
                }
                break;
        }
    }

    /// <summary>
    /// Set the normal color of the playable
    /// </summary>
    private void SetColor()
    {
        gameObject.GetComponent<SpriteRenderer>().color = baseColor;
    }

    /// <summary>
    /// Verify if the current player can use the playable
    /// </summary>
    /// <returns>return the result in a boolean</returns>
    private bool VerifyCanUse()
    {
        return (turnManager.GetComponent<TurnManager>().CurrentPlayer == team);
    }
}
