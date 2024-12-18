using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAspNetMVC.BusinessLogic.Requests.UserRequests
{
    public class ChangeUserRequest
    {
        public string OldName { get; set; }

        public string NewName { get; set; }

        public string NewPassword { get; set; }

        public string NewEmail { get; set; }

    }
}
