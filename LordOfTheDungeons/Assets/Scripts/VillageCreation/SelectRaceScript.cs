using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

public class SelectRaceScript : MonoBehaviour
{
    
    [SerializeField]
    private Light2D usedLight;
    [SerializeField]
    private Animator transition;
    [SerializeField]
    private TMP_Text text;

    [SerializeField]
    private TMP_InputField inputField;

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
        string faction = "";

        switch (name)
        {
            case "humanshitbox":
                {
                    faction = "Humain";
                }
                break;
            case "elveshitbox":
                {
                    faction = "Elfe";
                }
                break;
            case "dwarfshitbox":
                {
                    faction = "Nain";
                }
                break;
            case "hobbitshitbox":
                {
                    faction = "Hobbit";
                }
                break;

        }


        Server.CreateVillage(inputField.text, faction, Server.GetSavedIdUniverse());
        Server.SetCurrentVillage(Server.UserGetVillageID(Server.GetSavedIdUniverse()).Town);
        Debug.Log(Server.GetCurrentVillage());
        Server.InitVillage(Server.GetCurrentVillage());

        GameObject.Find("loadscreen").GetComponent<loaderScript>().Level("Village");
    }
}
