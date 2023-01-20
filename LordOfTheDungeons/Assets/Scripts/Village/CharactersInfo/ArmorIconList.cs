using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Village.CharactersInfo
{
    [Serializable]
    // Force unity to serialize a list
    public class ArmorIconList<T>
    {
        public List<T> ArmorIcons;
    }
}
