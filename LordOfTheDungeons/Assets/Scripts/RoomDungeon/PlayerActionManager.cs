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
    public void ShowAttackATH(bool show)
    {
        buttonAttack.SetActive(show);
    }

    public void ShowMoveATH(bool show)
    {
        buttonMove.SetActive(show);
    }

    private void Start()
    {
        ShowAttackATH(false);
    }
}
