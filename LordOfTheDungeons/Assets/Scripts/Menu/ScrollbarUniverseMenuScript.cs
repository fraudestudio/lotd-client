using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarUniverseMenuScript : MonoBehaviour
{
    /// <summary>
    /// Set the scroobar size
    /// </summary>
    void Start()
    {
        GetComponent<Scrollbar>().size = 0.01f;
        enabled = true;
    }

    /// <summary>
    /// Set the scroolbar size
    /// </summary>
    void Update()
    {
        GetComponent<Scrollbar>().size = 0.01f;
        enabled = true;
    }
}
