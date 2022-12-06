using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Village
{
    public class CanHealScript : MonoBehaviour
    {
        public GameObject button;

        public void NotifySlotIsNotEmpty(bool value)
        {
            button.SetActive(value);
        }
    }
}
