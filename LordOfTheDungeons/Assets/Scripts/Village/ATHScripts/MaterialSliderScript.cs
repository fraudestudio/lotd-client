using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaterialSliderScript : MonoBehaviour
{

    public Slider currentSlider;
    public TMP_Text currentValue;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentValue.text = currentSlider.value.ToString();
    }
}
