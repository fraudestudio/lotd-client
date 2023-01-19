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
        // dictionnary of the skins
        private Dictionary<string, Sprite> skins = new Dictionary<string,Sprite>();

        [Serializable]
        private struct SkinRoom
        {
            // name of the skin
            public string name;
            // sprite of the skin
            public Sprite sprite;
        }

        [SerializeField]
        private SkinRoom[] sprites;


        private void Awake()
        {
            // fill the dictionnary of sprites
            foreach (SkinRoom sk in sprites)
            {
                skins.Add(sk.name, sk.sprite);
            }
        }

        /// <summary>
        /// Send the sprite with the correct name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
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
