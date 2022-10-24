using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

public class SelectRaceScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Light2D usedLight;
    public Animator transition;
    public TMP_Text text;

    public TMP_InputField inputField;

    private bool isEnter;
    private bool isExit;

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
        usedLight.intensity = 1f;
        usedLight.pointLightOuterRadius = 27;
        text.color = new Color(1, 1, 1);
    }

    private void OnMouseExit()
    {
        transition.SetTrigger("UnZoom");
        usedLight.intensity = 0.1f;
        usedLight.pointLightOuterRadius = 10;
        text.color = new Color(0.05f, 0.05f, 0.05f);
    }

    private void OnMouseUp()
    {
        switch (name)
        {
            case "humanshitbox":
                {
                    TemporaryScript.AddVillage(SaveUniverseScript.Universe, inputField.text, "human", TemporaryScript.currentUser);
                }
                break;
            case "elveshitbox":
                {
                    TemporaryScript.AddVillage(SaveUniverseScript.Universe, inputField.text, "elve", TemporaryScript.currentUser);
                }
                break;
            case "dwarfshitbox":
                {
                    TemporaryScript.AddVillage(SaveUniverseScript.Universe, inputField.text, "dwarf", TemporaryScript.currentUser);
                }
                break;
            case "hobbitshitbox":
                {
                    TemporaryScript.AddVillage(SaveUniverseScript.Universe, inputField.text, "hobbit", TemporaryScript.currentUser);
                }
                break;

        }

        GameObject.Find("loadscreen").GetComponent<loaderScript>().Level("Village");
    }
}
