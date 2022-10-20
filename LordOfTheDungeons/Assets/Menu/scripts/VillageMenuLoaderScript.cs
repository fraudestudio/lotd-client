using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VillageMenuLoaderScript : MonoBehaviour
{

    private bool stop = false;
    public GameObject buttonPrefab;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator GoBackToUniverse()
    {
        GameObject universeCanvas = GameObject.Find("UniversesMenu");
        Animator universeAnimator = universeCanvas.GetComponent<Animator>();
        GameObject searchCanvas = GameObject.Find("UniversesMenuSearch");
        Animator searchAnimator = searchCanvas.GetComponent<Animator>();
        GameObject villageCanvas = GameObject.Find("VillageMenu");
        Animator villageAnimator = villageCanvas.GetComponent<Animator>();

        villageCanvas.GetComponent<CanvasGroup>().interactable = false;
        villageCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
        searchCanvas.GetComponent<CanvasGroup>().alpha = 1;
        universeCanvas.GetComponent<CanvasGroup>().alpha = 1;

        universeAnimator.SetTrigger("ShowMenu");
        searchAnimator.SetTrigger("ShowMenu");
        villageAnimator.SetTrigger("HideMenu");

        yield return new WaitForSeconds(0.3f);
        villageCanvas.GetComponent<CanvasGroup>().alpha = 0;
        universeAnimator.SetTrigger("GoBackIdle");
        searchAnimator.SetTrigger("GoBackIdle");
        villageAnimator.SetTrigger("GoBackIdle");
        universeCanvas.GetComponent<CanvasGroup>().interactable = true;
        universeCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
        searchCanvas.GetComponent<CanvasGroup>().interactable = true;
        searchCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void ChangeMenu()
    {
        StartCoroutine(GoBackToUniverse());
    }

    public void CreateVillageButtons()
    {
        int i = 0;
        int y = 0;
        while (!stop)
        {
            GameObject b = Instantiate(buttonPrefab);
            b.transform.localScale = new Vector2(2, 2);
            b.name = "Village de" + b.GetComponent<VillageButtonScript>().UserName;
            b.transform.SetParent(GameObject.Find("VillageMenu").transform);
            b.GetComponentInChildren<TMP_Text>().text = "Village de " + b.GetComponent<VillageButtonScript>().UserName;
            b.transform.position = new Vector3(170 + ((i % 3) * 300), 750 - (y * 270), 0);
            i++;

            if (i % 3 == 0)
            {
                y++;
            }


            if (i == 9)
            {
                stop = true;
            }
        }

        transform.Find("bubblespeach").SetSiblingIndex(30);
    }
}
