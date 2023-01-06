using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.RoomDungeon
{

    public class SkinRoomDictionnary : MonoBehaviour
    {

        private Dictionary<string, Sprite> skins = new Dictionary<string,Sprite>();

        [Serializable]
        private struct SkinRoom
        {
            public string name;
            public Sprite sprite;
        }

        [SerializeField]
        private SkinRoom[] sprites;


        private void Awake()
        {
            foreach (SkinRoom sk in sprites)
            {
                skins.Add(sk.name, sk.sprite);
            }
        }

        public Sprite GetSprite(string name)
        {
            try
            {
                return skins[name];
            }
            catch
            {
                return null;
            }

        }
    }
}
