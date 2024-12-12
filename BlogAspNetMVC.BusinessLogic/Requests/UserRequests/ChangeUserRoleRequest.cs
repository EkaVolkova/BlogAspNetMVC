using System;

namespace BlogAspNetMVC.BusinessLogic.Requests.UserRequests
{
    public class ChangeUserRoleRequest
    {
        public string UserName { get; set; }
        public string NewRoleName { get; set; }
    }
}
