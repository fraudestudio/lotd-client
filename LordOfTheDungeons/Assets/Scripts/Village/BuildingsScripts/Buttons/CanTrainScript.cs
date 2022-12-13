using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanTrainScript : MonoBehaviour
{

   
    private List<GameObject> trainees = new List<GameObject>();
    private GameObject instructor;

    #region Observateur instructeur
    public void AddInstructor(GameObject instructor)
    {
        this.instructor = instructor;
        VerifyCond();
    }

    public void RemoveInstructor()
    {
        this.instructor = null;
        VerifyCond();
    }
    #endregion

    #region Observateur Trainee
    public void AddTrainee(GameObject trainee)
    {
        this.trainees.Add(trainee);
        VerifyCond();
    }

    public void RemoveTrainee(GameObject trainee)
    {
        this.trainees.Remove(trainee);
        VerifyCond();
    }
    #endregion

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
