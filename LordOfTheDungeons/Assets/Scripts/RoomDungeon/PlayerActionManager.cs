using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
    // bool of if the player can do everything on screen or no
    [SerializeField]
    private bool canDoAnything = true;
    public bool CanDoAnything { get => canDoAnything; set => canDoAnything = value; }
}
