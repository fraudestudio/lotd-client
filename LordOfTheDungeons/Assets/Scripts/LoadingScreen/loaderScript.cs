using Assets.Scripts.LoadingScreen;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
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
        ChangeText();
        StartCoroutine(ResetProgressValue());
    }


    /// <summary>
    /// Reset the progress value on the scene
    /// </summary>
    /// <returns></returns>
    private IEnumerator ResetProgressValue()
    {
        yield return new WaitForSeconds(0.8f);
        ProgressSingleton.Instance.Progress = 0f;
        ChangeText();
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
            ProgressSingleton.Instance.Progress = Mathf.Clamp01(operation.progress / 0.9f);
            ChangeText();
            yield return null;
        }
    }

    /// <summary>
    /// Change the progress bar text
    /// </summary>
    private void ChangeText()
    {
        progressText.text = Math.Round(ProgressSingleton.Instance.Progress * 100, 2).ToString() + "%";
    }
}
