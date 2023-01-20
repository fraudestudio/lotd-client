using Assets.Scripts.Village;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartTrainingButtonScript : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// When clicked, start to train and desactivate all the slots
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            for (int i = 0; i < GameObject.Find("TrainingCampMenu").transform.Find("TraineeTitle").Find("TraineesLayout").childCount; i++)
            {
                GameObject.Find("TrainingCampMenu").transform.Find("TraineeTitle").Find("TraineesLayout").GetChild(i).GetComponent<CharacterSlotScript>().CanDrop = false;
                if (!GameObject.Find("TrainingCampMenu").transform.Find("TraineeTitle").Find("TraineesLayout").GetChild(i).GetComponent<CharacterSlotScript>().SlotIsEmpty)
                {
                    GameObject.Find("TrainingCampMenu").transform.Find("TraineeTitle").Find("TraineesLayout").GetChild(i).GetComponent<CharacterSlotScript>().CurrentCharacter.GetComponent<CharacterImageSlotScript>().SetDrag(false);
                }
                else
                {
                    CharacterSlotNotAllowedScript.AddSlot(GameObject.Find("TrainingCampMenu").transform.Find("TraineeTitle").Find("TraineesLayout").GetChild(i).gameObject);
                }
            }
            GameObject.Find("TrainingCampMenu").transform.Find("InstructorTitle").Find("CharacterSlot").GetComponent<CharacterSlotScript>().CurrentCharacter.GetComponent<CharacterImageSlotScript>().SetDrag(false);
            GameObject.Find("ButtonTrain").SetActive(false);
            GameObject.Find("TrainingCampMenu").transform.Find("TimeSliderTrainingCamp").gameObject.SetActive(true);
            GameObject.Find("TrainingCampMenu").transform.Find("TimeSliderTrainingCamp").GetComponent<TimeLeftSliderScript>().Init(TrainingCamp.TimeMaxTraining, TrainingCamp.TimeMaxTraining);

            VillageManager.TrainingStarted();
            Village.TrainingCamp.InFormation = true;
            
        }
    }
}
