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
        TemporaryScript.Init();
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
            if (Server.VerifyUser(id.text, mdp.text))
            {
                TemporaryScript.currentUser = id.text;
                c.GetComponent<loaderScript>().Level("Menu");
            }
            else
            {
                error.color = new Color(1, 0, 0, 1);
            }
            GameObject.Find("WaitingServer").GetComponent<WaitingForServerScript>().StopAnim();
        }
    }
}
