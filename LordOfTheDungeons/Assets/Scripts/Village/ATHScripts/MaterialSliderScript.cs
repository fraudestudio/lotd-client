using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaterialSliderScript : MonoBehaviour
{

    [SerializeField]
    // The slider used for the material
    private Slider currentSlider;

    [SerializeField]
    // the current value of the slider
    private TMP_Text currentValue;


    /// <summary>
    /// Get the value of the slider
    /// </summary>
    /// <returns>the slider value</returns>
    public int GetValue()
    {
        return (int)currentSlider.value;
    }

    /// <summary>
    /// Set the value of the slider
    /// </summary>
    /// <param name="value">the value wanted</param>
    public void SetValue(int value)
    {
        currentSlider.value = value;
    }

    /// <summary>
    /// Add value to the slider
    /// </summary>
    /// <param name="value">the value wanted</param>
    public void AddValue(int value)
    {
        currentSlider.value += value;
    }

    /// <summary>
    /// Delete value to the slider
    /// </summary>
    /// <param name="value">the wanted value</param>
    public void DeleteValue(int value)
    {
        currentSlider.value -= value;
    }

    /// <summary>
    /// Change the maximum of the slider
    /// </summary>
    /// <param name="value"></param>
    public void ChangeMax(int value)
    {
        currentSlider.maxValue = value;
    }

    public void OnValueChanged()
    {
        currentValue.text = currentSlider.value.ToString();
    }
}
