using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacterScript : MonoBehaviour
{

    private int movement = 3;

    public int Movement { get => movement; set => movement = value; }


    private GameObject selectionTileManager;
    private GameObject characterManager;

    public void Start()
    {
        characterManager = GameObject.Find("CharacterManager");
        selectionTileManager = GameObject.Find("SelectionTileManager");
    }


    private void OnMouseEnter()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0.4008989f, 0.659913f, 0.8584906f, 1f);
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0.241545f, 0.5954974f, 0.8679245f, 1f);
    }

    private void OnMouseUp()
    {
        selectionTileManager.GetComponent<SelectionTileManager>().CreateSelectionTiles(movement,gameObject);
    }
}
