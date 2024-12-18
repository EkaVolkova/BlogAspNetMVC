using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAspNetMVC.BusinessLogic.Requests.UserRequests
{
    public class ChangeUserNameRequest
    {
        public string OldName { get; set; }
        public string NewName { get; set; }
    }
}
