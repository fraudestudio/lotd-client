using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    // Life of the enemy
    private int life = 100;

    [SerializeField]
    // The hp bar of the enemy
    private GameObject hpBar;

    // slider of the health bar
    private Slider slider;

    // Movement of the enemy:
    private int movement = 5;
    public int Movement { get => movement; set => movement = value; }

    private GameObject characterManager;

    private void Start()
    {
        hpBar.transform.Find("background").GetComponent<Slider>().maxValue = life;
        hpBar.transform.Find("background").GetComponent<Slider>().value = life;
        slider = hpBar.transform.Find("background").GetComponent<Slider>();
        characterManager = GameObject.Find("CharacterManager");
    }

    /// <summary>
    /// Substract the enemy life
    /// </summary>
    /// <param name="value">life the enemy has to loose</param>
    public void Hurt(int value)
    {

        hpBar.GetComponent<Animator>().SetTrigger("HurtAnim");
        life -= value;
        if (life < 0)
        {
            life = 0;
        }
        StartCoroutine(GetSliderValueToLifeValue());


    }

    /// <summary>
    /// Get the slider to the life value
    /// </summary>
    /// <returns></returns>
    private IEnumerator GetSliderValueToLifeValue()
    {
        while (slider.value > life)
        {
            yield return new WaitForSeconds(0.05f);
            slider.value -= 1;
        }

        if (slider.value == 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Make the enemy die
    /// </summary>
    private void Die()
    {
        characterManager.GetComponent<CharacterManager>().KillCharacter(gameObject);
    }
}
