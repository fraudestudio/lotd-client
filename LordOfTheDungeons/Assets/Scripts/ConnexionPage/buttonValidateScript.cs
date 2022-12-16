using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class buttonValidateScript : MonoBehaviour, IPointerClickHandler
{
    public Canvas c;
    public TMP_InputField id;
    public TMP_InputField mdp;
    public TMP_Text error;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GameObject.Find("WaitingServer").GetComponent<WaitingForServerScript>().StartAnim();
            if (VerifyResponse(Server.VerifyUser(id.text, mdp.text)))
            {
                c.GetComponent<loaderScript>().Level("Menu");
            }
            GameObject.Find("WaitingServer").GetComponent<WaitingForServerScript>().StopAnim();
        }
    }


    public bool VerifyResponse(string response)
    {
        bool res = false;

        Debug.Log(response);

        if (response == "Error")
        {
            error.color = new Color(1, 0, 0, 1);
            error.text = "INFORMATIONS INCORRECTES\r\nRÉESSAYER";
        }
        else if (response == "NotValid")
        {
            error.color = new Color(1, 0, 0, 1);
            error.text = "VOTRE COMPTE N'EST\nPAS VALIDE";
        }
        else
        {
            res = true;
        }

        return res;
    }
}
