using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Server.Model.Connexion
{
    public class SignInSuccess
    {
        public bool Validated { get; set; }
        public string SessionToken { get; set; }
    }
}
