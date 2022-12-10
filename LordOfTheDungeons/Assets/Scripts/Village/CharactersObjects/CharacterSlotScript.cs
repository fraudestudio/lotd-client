using Assets.Scripts.Village;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI.Table;

public class CharacterSlotScript : MonoBehaviour, IDropHandler
{

    public SlotType Type = SlotType.RECRUIT;


    public GameObject SlotPreFab;

    private bool canDrop = true; 

    public bool slotIsEmpty = true;

    public bool SlotIsEmpty { get => slotIsEmpty; set => slotIsEmpty = value; }
    public bool CanDrop { get => canDrop; set => canDrop = value; }


    private GameObject currentCharacter;

    public GameObject CurrentCharacter { get => currentCharacter; }


    public void OnDrop(PointerEventData eventData)
    {
        GameObject drop = eventData.pointerDrag;

        if (canDrop)
        {
            if (drop.TryGetComponent(out CharacterImageSlotScript dropTest))
            {
                if (dropTest.CanDrag)
                {
                    if (SlotIsEmpty)
                    {

                        if (Type != SlotType.RECRUIT)
                        {
                            drop.GetComponent<CharacterImageSlotScript>().IsEngaged = false;
                            SlotIsEmpty = false;
                            drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag.GetComponent<CharacterSlotScript>().SlotIsEmpty = true;

                            if (drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag.GetComponent<CharacterSlotScript>().Type == SlotType.RECRUIT)
                            {
                                Destroy(drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag.gameObject);
                            }
                            #region Observateur healer (Remove)
                            else if (drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag.GetComponent<CharacterSlotScript>().Type == SlotType.HEALER)
                            {
                                transform.GetComponentInParent<CanHealScript>().NotifySlotIsNotEmpty(false);
                                VillageManager.CharRemovedHealer();
                            }
                            #endregion
                            #region Observateur camp d'entrainement (Remove)
                            else if (drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag.GetComponent<CharacterSlotScript>().Type == SlotType.INSTRUCTOR)
                            {
                                GameObject.Find("TrainingCampMenu").GetComponent<CanTrainScript>().RemoveInstructor();
                                VillageManager.CharRemovedTrainingCamp();
                            }
                            else if (drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag.GetComponent<CharacterSlotScript>().Type == SlotType.TRAINEE)
                            {
                                GameObject.Find("TrainingCampMenu").GetComponent<CanTrainScript>().RemoveTrainee(drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag.gameObject);
                                VillageManager.CharRemovedTrainingCamp();
                            }
                            #endregion
                            #region Observateur armurier (Remove)
                            else if (drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag.GetComponent<CharacterSlotScript>().Type == SlotType.GUNSMITH)
                            {
                                VillageManager.CharRemovedGunsmith();
                                transform.GetComponentInParent<UpgradePartGunSmithScript>().CanUpgradeObserver(false);
                            }

                            #endregion
                            #region Observateur taverne (Remove)
                            else if (drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag.GetComponent<CharacterSlotScript>().Type == SlotType.TAVERN)
                            {
                                VillageManager.CharRemovedTavern();
                            }
                            #endregion

                            CharacterSlotNotAllowedScript.AddSlot(transform.gameObject);
                            CharacterSlotNotAllowedScript.RemoveSlot(drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag.gameObject);


                            #region Observateur camp d'entrainement (Add)
                            if (transform.GetComponent<CharacterSlotScript>().Type == SlotType.INSTRUCTOR)
                            {
                                GameObject.Find("TrainingCampMenu").GetComponent<CanTrainScript>().AddInstructor(transform.gameObject);
                                VillageManager.CharAddedTrainingCamp();
                            }
                            else if (transform.GetComponent<CharacterSlotScript>().Type == SlotType.TRAINEE)
                            {
                                GameObject.Find("TrainingCampMenu").GetComponent<CanTrainScript>().AddTrainee(transform.gameObject);
                                VillageManager.CharAddedTrainingCamp();
                            }
                            #endregion
                            #region Observateur amurier (Add)
                            else if (transform.GetComponent<CharacterSlotScript>().Type == SlotType.GUNSMITH)
                            {
                                VillageManager.CharAddedGunsmith();
                                transform.GetComponentInParent<UpgradePartGunSmithScript>().CanUpgradeObserver(true);
                            }
                            #endregion
                            #region Observateur taverne (Add)
                            else if (transform.GetComponent<CharacterSlotScript>().Type == SlotType.TAVERN)
                            {
                                VillageManager.CharAddedTavern();
                            }
                            #endregion
                            #region Observateur Healer (add)
                            else if (transform.GetComponent<CharacterSlotScript>().Type == SlotType.HEALER)
                            {
                                transform.GetComponentInParent<CanHealScript>().NotifySlotIsNotEmpty(true);
                                VillageManager.CharAddedHealer();
                            }
                            #endregion

                            drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag = transform;
                            currentCharacter = drop;

                        }
                        else if (Type == SlotType.RECRUIT)
                        {
                            if (!drop.GetComponent<CharacterImageSlotScript>().IsEngaged)
                            {
                                #region Observateur camp d'entrainement (Remove)
                                if (drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag.GetComponent<CharacterSlotScript>().Type == SlotType.INSTRUCTOR)
                                {
                                    GameObject.Find("TrainingCampMenu").GetComponent<CanTrainScript>().RemoveInstructor();
                                    VillageManager.CharRemovedTrainingCamp();
                                }
                                else if (drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag.GetComponent<CharacterSlotScript>().Type == SlotType.TRAINEE)
                                {
                                    GameObject.Find("TrainingCampMenu").GetComponent<CanTrainScript>().RemoveTrainee(drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag.gameObject);
                                    VillageManager.CharRemovedTrainingCamp();
                                }
                                #endregion
                                #region Observateur armurier (Remove)
                                else if (drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag.GetComponent<CharacterSlotScript>().Type == SlotType.GUNSMITH)
                                {
                                    VillageManager.CharRemovedGunsmith();
                                    drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag.GetComponentInParent<UpgradePartGunSmithScript>().CanUpgradeObserver(false);
                                }
                                #endregion
                                #region Observateur taverne (Remove)
                                else if (drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag.GetComponent<CharacterSlotScript>().Type == SlotType.TAVERN)
                                {
                                    VillageManager.CharRemovedTavern();
                                }
                                #endregion
                                #region Observateur healer (Remove)
                                else if (drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag.GetComponent<CharacterSlotScript>().Type == SlotType.HEALER)
                                {
                                    drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag.GetComponentInParent<CanHealScript>().NotifySlotIsNotEmpty(false);
                                    VillageManager.CharRemovedHealer();
                                }
                                #endregion

                                SlotIsEmpty = false;
                                transform.Find("PlusImage").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                                GameObject d = Instantiate(SlotPreFab);
                                d.transform.SetParent(transform.parent);
                                d.transform.localScale = new Vector3(1, 1, 1);
                                drop.GetComponent<CharacterImageSlotScript>().IsEngaged = true;
                                drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag.GetComponent<CharacterSlotScript>().SlotIsEmpty = true;
                                drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag = transform;
                                CharacterSlotNotAllowedScript.RemoveSlot(drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag.gameObject);
                                CharacterSlotNotAllowedScript.AddSlot(transform.gameObject);
                                currentCharacter = drop;
                            }
                        }
                    }
                    else
                    {
                        CharacterSlotNotAllowedScript.AddSlot(drop.GetComponent<CharacterImageSlotScript>().ParentAfterDrag.gameObject);
                    }
                }
            }
        }

    }

    public void Awake()
    {
        if (transform.childCount > 2)
        {
            SlotIsEmpty = false;
            CharacterSlotNotAllowedScript.AddSlot(transform.gameObject);
        }
        else
        {
            slotIsEmpty = true;
        }

        if (SlotIsEmpty && Type == SlotType.RECRUIT)
        {
            transform.Find("PlusImage").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }

    public void SetType(SlotType type)
    {
        Type = type;

        if (SlotIsEmpty)
        {
            if (Type != SlotType.RECRUIT)
            {
                transform.Find("PlusImage").GetComponent<Image>().color = new Color(1, 1, 1, 0);
            }
            else
            {
                transform.Find("PlusImage").GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }

        }
    }


    public void AddChar(GameObject c)
    {
        SlotIsEmpty = false;
        transform.Find("PlusImage").GetComponent<Image>().color = new Color(1, 1, 1, 0);
        c.transform.SetParent(transform);
        c.transform.SetAsLastSibling();
        c.transform.position = new Vector2(0, 110);
        c.transform.localScale = new Vector2(1, 1);
    }
}
