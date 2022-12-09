using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Server
{
    public class UniverseInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool HasPassword { get; set; }

        public int Town { get; set; }

        public string Faction { get; set; }
    }
}
