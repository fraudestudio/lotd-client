using System.Timers;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;
using static UnityEngine.UI.Slider;
using System;
using System.Runtime.CompilerServices;
using System.Security;

public class TimeLeftSliderScript : MonoBehaviour
{

    public TMP_Text timeRemainingText;
    public Slider timerSlider;

    private DateTime pausedDateTime;

    public bool init;


    /// <summary>
    /// Initialise le timer
    /// </summary>
    /// <param name="value"></param>
    /// <param name="maxValue"></param>
    public void Init(int value, int maxValue)
    {
        timerSlider.maxValue = maxValue;
        timerSlider.value = value;
        init = true;
        StartCoroutine("timer");
    }

    /// <summary>
    /// Arrête le timer
    /// </summary>
    public void Stop()
    {
        StopCoroutine("timer");
        init = false;
    }

    /// <summary>
    /// Quand l'application est en pause on retient la date
    /// Quand elle est remise, on soustrait le temps passé
    /// </summary>
    /// <param name="pause"></param>
    private void OnApplicationPause(bool pause)
    {
        if (init)
        {
            if (pause)
            {
                pausedDateTime = DateTime.Now;
            }
            else
            {
                timerSlider.value -= Convert.ToSingle(Math.Floor((DateTime.Now - pausedDateTime).TotalSeconds));
            }

        }
    }

    /// <summary>
    /// Enlève 1 au slider toute les secondes
    /// </summary>
    /// <returns></returns>
    private IEnumerator timer()
    {
        while (init)
        {
            yield return new WaitForSeconds(1f);
            timerSlider.value -= 1;
            if (timerSlider.value == 0)
            {
                NotifyObserver();
            }
        }
    }


    private void NotifyObserver()
    {
        switch (name)
        {
            case "TimeSliderCenter": VillageManager.NotifyConstructFinished(transform.parent.name); break;
        }
    }

    /// <summary>
    /// Change le texte en fonction de la valeur du slider
    /// </summary>
    public void changeText()
    {
        timeRemainingText.text = calculateDate();
    }


    /// <summary>
    /// Renvoie le texte pour indiquer le temps restant
    /// </summary>
    /// <returns></returns>
    private string calculateDate()
    {
        TimeSpan t = TimeSpan.FromSeconds(timerSlider.value);
        return t.Days + " jours " + t.Hours + " heures " + t.Minutes + " minutes " + t.Seconds + " secondes";
    }


    public void GetPercentage(TMP_Text t)
    {
        t.text = "" + Math.Round((((timerSlider.value / timerSlider.maxValue) * 100) - 100) * -1,2) + "%";
    }
}
