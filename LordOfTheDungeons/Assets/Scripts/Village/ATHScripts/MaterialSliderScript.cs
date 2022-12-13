using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaterialSliderScript : MonoBehaviour
{

    public Slider currentSlider;
    public TMP_Text currentValue;



    public int GetValue()
    {
        return (int)currentSlider.value;
    }

    public void SetValue(int value)
    {
        currentSlider.value = value;
    }

    public void AddValue(int value)
    {
        currentSlider.value += value;
    }

    public void DeleteValue(int value)
    {
        currentSlider.value -= value;
    }

    public void ChangeMax(int value)
    {
        currentSlider.maxValue = value;
    }

    public void OnValueChanged()
    {
        currentValue.text = currentSlider.value.ToString();
    }
}
