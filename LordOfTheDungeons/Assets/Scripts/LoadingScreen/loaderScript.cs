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
    [SerializeField]
    // Animator of the loading screen 
    private Animator transition;
    [SerializeField]
    private TMP_Text progressText;

    // Start is called before the first frame updatesm
    void Start()
    {
        progressText.text = ProgressBarScript.Progress * 100 + "%";
    }

    /// <summary>
    /// Change the scene to the given scene
    /// </summary>
    /// <param name="name">the scene you want to change to</param>
    public void Level(string name)
    {
        StartCoroutine(LoadLevel(name));
    }


    /// <summary>
    /// Load the given level
    /// </summary>
    /// <param name="name">the given scene</param>
    /// <returns></returns>
    private IEnumerator LoadLevel(string name)
    {
        // Trigger the animation start
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);

        // Load the current scene 
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);

        // Show the progression
        while (!operation.isDone)
        {
            ProgressBarScript.Progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressText.text = ProgressBarScript.Progress * 100 + "%";

            yield return null;
        }
        ProgressBarScript.Progress = 0;
    }
}
