using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarUniverseMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Scrollbar>().size = 0.01f;
        enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Scrollbar>().size = 0.01f;
        enabled = true;
    }
}
