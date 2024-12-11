using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAspNetMVC.BusinessLogic.Requests.UserRequests
{
    public class SignInRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }

    }
}
