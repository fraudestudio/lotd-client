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
        [SerializeField]
        // the heal button
        private GameObject button;

        /// <summary>
        /// Notify if the healslot is empty
        /// </summary>
        /// <param name="value"></param>
        public void NotifySlotIsNotEmpty(bool value)
        {
            button.SetActive(value);
        }
    }
}
