using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
    // bool of if the player can do everything on screen or no
    [SerializeField]
    private bool canDoAnything = true;
    public bool CanDoAnything { get => canDoAnything; set => canDoAnything = value; }
    
    [SerializeField]
    private GameObject buttonAttack;
    [SerializeField]
    private GameObject buttonMove;
    
    /// <summary>
    /// Show the attack button on the ATH
    /// </summary>
    /// <param name="show"></param>
    public void ShowAttackATH(bool show)
    {
        buttonAttack.SetActive(show);
    }

    /// <summary>
    /// Show the move button on the ATH
    /// </summary>
    /// <param name="show"></param>
    public void ShowMoveATH(bool show)
    {
        buttonMove.SetActive(show);
    }

    /// <summary>
    /// Start with the attack butotn
    /// </summary>
    private void Start()
    {
        ShowAttackATH(false);
    }
}
