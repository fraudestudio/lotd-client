using Assets.Scripts.RoomDungeon.Characters.Selection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionTileScript : MonoBehaviour
{

    [SerializeField]
    // Line of the selection tile
    private int ligne;
    public int Ligne { get => ligne; set => ligne = value; }
    [SerializeField]
    // Column of the selection tile
    private int colonne;
    public int Colonne { get => colonne; set => colonne = value; }

    // The type of the selection
    private TypeSelection selectionType;

    private GameObject selectionTileManager;

    // Tells if the player can use the tile
    private bool canUse = true;
    public bool CanUse { get => canUse; set => canUse = value; }

    // List of the neigbor of the selection tile
    List<GameObject> voisins = new List<GameObject>();
    public List<GameObject> Voisins { get => voisins; }


    /// <summary>
    /// Add a neigbor to the selection tile
    /// </summary>
    /// <param name="selectionTile"></param>
    public void AddVoisins(GameObject selectionTile)
    {
        voisins.Add(selectionTile);
    }

    /// <summary>
    /// Remove a neigbor to the selection tile
    /// </summary>
    /// <param name="selectionTile"></param>
    public void RemoveVoisins(GameObject selectionTile)
    {
        voisins.Remove(selectionTile);
    }


    public void Start()
    {
        selectionTileManager = GameObject.Find("SelectionTileManager");
    }

    /// <summary>
    /// When the mouse enter the object, it create the selection trail 
    /// </summary>
    public void OnMouseEnter()
    {
        if (canUse)
        {
            switch (selectionType)
            {
                case TypeSelection.Deplacement:
                    {
                        selectionTileManager.GetComponent<SelectionTileManager>().CreateSelectionTrail(gameObject);
                    }
                    break;                
                case TypeSelection.Attack:
                    {
                        TurnWhite();
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// When the mouse exit, it makes all the selection tile white
    /// </summary>
    public void OnMouseExit()
    {
        if (canUse)
        {
            switch (selectionType)
            {
                case TypeSelection.Deplacement:
                    {
                        selectionTileManager.GetComponent<SelectionTileManager>().ResetTrail();
                    }
                    break;
                case TypeSelection.Attack:
                    {
                        TurnRed();
                    }
                    break;
            }
        }

    }

    /// <summary>
    /// Tells the selection manager that the player want to move his playable here
    /// </summary>
    public void OnMouseDown()
    {
        if (canUse)
        {

            switch (selectionType)
            {
                case TypeSelection.Deplacement:
                    {
                        selectionTileManager.GetComponent<SelectionTileManager>().MovePlayable();
                    }
                    break;                
                case TypeSelection.Attack:
                    {
                        selectionTileManager.GetComponent<SelectionTileManager>().AttackPlayable(ligne,colonne);
                    }
                    break;
            }
        }
    }


    /// <summary>
    /// Turn the tile red
    /// </summary>
    public void TurnRed()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1,0,0,0.3f);
    }

    /// <summary>
    /// Turn the tile white
    /// </summary>
    public void TurnWhite()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.3f);
    }

    /// <summary>
    /// Hide selection tile
    /// </summary>
    public void Hide()
    {
        canUse = false;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0f);
    }

    /// <summary>
    /// Set the type of the selection tile
    /// </summary>
    /// <param name="type">the type wanted</param>
    public void SetType(TypeSelection type)
    {
        switch (type)
        {
            case TypeSelection.Deplacement:
                {
                    selectionType = TypeSelection.Deplacement;
                    TurnWhite();
                }
                break;
            case TypeSelection.Attack:
                {
                    selectionType = TypeSelection.Attack;
                    TurnRed();
                }
                break;
        }
    }
}
