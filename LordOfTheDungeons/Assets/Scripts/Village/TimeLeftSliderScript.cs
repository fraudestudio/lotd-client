using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Slider;

public class TimeLeftSliderScript : MonoBehaviour
{

    public TMP_Text timeRemainingText;
    public Slider timerSlider;

    // Start is called before the first frame update
    void Start()
    {
        timerSlider.maxValue = 330000;
        timerSlider.value = 294390;
        timeRemainingText.text = calculateDate();
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void changeText()
    {
        timeRemainingText.text = calculateDate();
    }


    private string calculateDate()
    {
        Debug.Log("caca");
        TimeSpan t = TimeSpan.FromSeconds(timerSlider.value);
        return t.Days + " jours " + t.Hours + " heures " + t.Minutes + " minutes " + t.Seconds + " secondes";
    }
}
