using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class loaderScript : MonoBehaviour
{

    public Animator transition;
    public TMP_Text progressText;

    // Start is called before the first frame updates
    void Start()
    {
        progressText.text = ProgressBarScript.Progress * 100 + "%";
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Level(string name)
    {
        StartCoroutine(LoadLevel(name));
    }

    IEnumerator LoadLevel(string name)
    {
        transition.SetTrigger("Start");
        ProgressBarScript.Progress = 0;
        yield return new WaitForSeconds(1);

        AsyncOperation operation = SceneManager.LoadSceneAsync(name);



        while (!operation.isDone)
        {
            ProgressBarScript.Progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressText.text = ProgressBarScript.Progress * 100 + "%";

            yield return null;
        }

        transition.SetTrigger("End");

        yield return new WaitForSeconds(1);
    }
}
