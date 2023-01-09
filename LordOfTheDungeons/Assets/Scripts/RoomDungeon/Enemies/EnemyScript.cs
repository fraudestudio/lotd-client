using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Life of the enemy
    private int life = 100;

    /// <summary>
    /// Substract the enemy life
    /// </summary>
    /// <param name="value">life the enemy has to loose</param>
    public void Hurt(int value)
    {
        life -= value;
        if (life > 0)
        {
            life = 0;
        }

    }
}
