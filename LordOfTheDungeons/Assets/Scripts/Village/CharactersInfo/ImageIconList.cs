using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Village.CharactersInfo
{
    [Serializable]
    // Force unity to serialize a list
    public class ImageIconList<T>
    {
        public List<T> CharacterIcon;
    }
}
