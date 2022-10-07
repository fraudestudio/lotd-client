using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SelectRaceScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Light2D usedLight;
    public Animator transition;
    public TMP_Text text;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        transition.SetTrigger("Zoom");
        usedLight.intensity = 1.5f;
        usedLight.pointLightOuterRadius = 27;
        text.color = new Color(1, 1, 1);
    }

    private void OnMouseExit()
    {
        transition.SetTrigger("UnZoom");
        usedLight.intensity = 0.5f;
        usedLight.pointLightOuterRadius = 10;
        text.color = new Color(0.05f, 0.05f, 0.05f);
    }
}
