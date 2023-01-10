using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackButtonScript : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GameObject.Find("PlayerActionManager").GetComponent<PlayerActionManager>().ShowAttackATH(false);
            GameObject.Find("PlayerActionManager").GetComponent<PlayerActionManager>().ShowMoveATH(true);
            GameObject.Find("CharacterManager").GetComponent<CharacterManager>().ChangeTypeSelection(Assets.Scripts.RoomDungeon.Characters.Selection.TypeSelection.Attack);
        }
    }
}
