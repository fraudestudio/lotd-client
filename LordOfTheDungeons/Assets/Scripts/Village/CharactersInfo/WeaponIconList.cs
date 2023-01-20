using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Village.CharactersInfo
{
    [Serializable]
    public class WeaponIconList<T>
    {
        // Force unity to serialize a list
        public List<T> WeaponIcons;
    }
}
