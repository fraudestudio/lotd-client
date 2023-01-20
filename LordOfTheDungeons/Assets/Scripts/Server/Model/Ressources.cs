using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Server.Model
{
    public class Ressources
    {
        private int bois;
        private int pierre;
        private int or;

        /// <summary>
        /// Stock of wood
        /// </summary>
        public int Bois { get => bois; set => bois = value; }
        /// <summary>
        /// Stock of stone
        /// </summary>
        public int Pierre { get => pierre; set => pierre = value; }
        /// <summary>
        /// Stock of gold
        /// </summary>
        public int Or { get => or; set => or = value; }
    }
}
