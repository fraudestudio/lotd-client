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

    private void VerifyCond()
    {
        if (instructor == null || trainees.Count == 0)
        {
            GameObject.Find("TrainingCampMenu").transform.Find("ButtonTrain").gameObject.SetActive(false);
        }
        else
        {
            GameObject.Find("TrainingCampMenu").transform.Find("ButtonTrain").gameObject.SetActive(true);
        }
    }
}
