using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Server.Model.Connexion
{
    public class SignInSuccess
    {

        private bool validated;

        /// <summary>
        /// Tells if the signin is validated
        /// </summary>
        public bool Validated { get => validated; set => validated = value; }

        private string sessionToken;
        /// <summary>
        /// The session token sent by the server
        /// </summary>
        public string SessionToken { get => sessionToken; set => sessionToken = value; }
    }
}
