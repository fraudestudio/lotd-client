using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UniverseButtonScript : MonoBehaviour, IPointerClickHandler
{

    private string villageName;

    private bool password;
    public string VillageName { get => villageName; set => villageName = value; }
    public bool Password { get => password; set => password = value; }

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
        if (!password)
        {

        }
    }
}
