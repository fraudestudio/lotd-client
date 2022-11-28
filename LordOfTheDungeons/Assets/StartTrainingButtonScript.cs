using Assets.Scripts.Village;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartTrainingButtonScript : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            for (int i = 0; i < GameObject.Find("TrainingCampMenu").transform.Find("TraineeTitle").Find("TraineesLayout").childCount; i++)
            {
                if (!GameObject.Find("TrainingCampMenu").transform.Find("TraineeTitle").Find("TraineesLayout").GetChild(i).GetComponent<CharacterSlotScript>().SlotIsEmpty)
                {
                    GameObject.Find("TrainingCampMenu").transform.Find("TraineeTitle").Find("TraineesLayout").GetChild(i).GetComponent<CharacterSlotScript>().CurrentCharacter.GetComponent<CharacterImageSlotScript>().SetDrag(false);
                }
            }
            GameObject.Find("TrainingCampMenu").transform.Find("InstructorTitle").Find("CharacterSlot").GetComponent<CharacterSlotScript>().CurrentCharacter.GetComponent<CharacterImageSlotScript>().SetDrag(false);
            GameObject.Find("ButtonTrain").SetActive(false);
            GameObject.Find("TrainingCampMenu").transform.Find("TimeSliderTrainingCamp").gameObject.SetActive(true);
            GameObject.Find("TrainingCampMenu").transform.Find("TimeSliderTrainingCamp").GetComponent<TimeLeftSliderScript>().Init(TrainingCamp.TimeMaxTraining, TrainingCamp.TimeMaxTraining);
            
        
        }
    }
}
