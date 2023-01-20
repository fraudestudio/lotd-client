using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.LoadingScreen
{
    public class ProgressSingleton
    {


        private float progress;
        /// <summary>
        /// Current progress of the loading bar
        /// </summary>
        public float Progress { get => progress; set => progress = value; }



        private static ProgressSingleton instance;

        /// <summary>
        /// Instance of the loading bar saver
        /// </summary>
        public static ProgressSingleton Instance {
            get
            {
                if (instance == null)
                {
                    instance = new ProgressSingleton();
                }
                return instance;
            }
        }


    }
}
