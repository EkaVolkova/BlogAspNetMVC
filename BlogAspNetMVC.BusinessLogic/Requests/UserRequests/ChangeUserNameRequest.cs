using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAspNetMVC.BusinessLogic.Requests.UserRequests
{
    public class ChangeUserNameRequest
    {
        public string OldUserName { get; set; }
        public string NewUserName { get; set; }
    }
}
