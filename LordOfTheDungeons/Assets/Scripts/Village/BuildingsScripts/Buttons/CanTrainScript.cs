using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanTrainScript : MonoBehaviour
{

    // The trainees in the building
    private List<GameObject> trainees = new List<GameObject>();
    // The instructor
    private GameObject instructor;

    #region Observateur instructeur
    /// <summary>
    /// Register the instructor
    /// </summary>
    /// <param name="instructor">the wanted instructor</param>
    public void AddInstructor(GameObject instructor)
    {
        this.instructor = instructor;
        VerifyCond();
    }
    /// <summary>
    /// Remove the instructor
    /// </summary>
    public void RemoveInstructor()
    {
        this.instructor = null;
        VerifyCond();
    }
    #endregion

    #region Observateur Trainee
    /// <summary>
    /// Add trainee to the trainee list
    /// </summary>
    /// <param name="trainee">the wanted trainee</param>
    public void AddTrainee(GameObject trainee)
    {
        this.trainees.Add(trainee);
        VerifyCond();
    }
    /// <summary>
    /// Remove trainee of the trainee list
    /// </summary>
    /// <param name="trainee">the wanted trainee</param>
    public void RemoveTrainee(GameObject trainee)
    {
        this.trainees.Remove(trainee);
        VerifyCond();
    }
    #endregion

    /// <summary>
    /// Verify if we can train
    /// </summary>
    public void VerifyCond()
    {
        if (instructor == null && trainees.Count == 0)
        {
            GameObject.Find("TrainingCampMenu").transform.Find("ButtonTrain").gameObject.SetActive(false);
        }
        else
        {
            if (instructor != null && trainees.Count != 0)
            {
                if (VerifyLevel())
                {
                    GameObject.Find("TrainingCampMenu").transform.Find("ErrorLevel").gameObject.SetActive(false);
                    GameObject.Find("TrainingCampMenu").transform.Find("ButtonTrain").gameObject.SetActive(true);
                }
                else
                {
                    GameObject.Find("TrainingCampMenu").transform.Find("ButtonTrain").gameObject.SetActive(false);
                    GameObject.Find("TrainingCampMenu").transform.Find("ErrorLevel").gameObject.SetActive(true);
                }
            }
            else
            {
                GameObject.Find("TrainingCampMenu").transform.Find("ErrorLevel").gameObject.SetActive(false);
                GameObject.Find("TrainingCampMenu").transform.Find("ButtonTrain").gameObject.SetActive(false);
            }

        }
    }

    /// <summary>
    /// Verify the level condition.
    /// If the level of the trainees is inferior to the instructor, it pass
    /// </summary>
    /// <returns>the result of the condition</returns>
    private bool VerifyLevel()
    {
        bool result = true;
        foreach (GameObject t in trainees)
        {
            if (t.GetComponent<CharacterSlotScript>().CurrentCharacter.GetComponent<CharacterImageSlotScript>().Character.Level >= instructor.GetComponent<CharacterSlotScript>().CurrentCharacter.GetComponent<CharacterImageSlotScript>().Character.Level)
            {
                result = false;
            }
        }

        return result;
    }

}
