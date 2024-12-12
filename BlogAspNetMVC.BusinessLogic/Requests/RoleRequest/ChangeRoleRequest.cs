using System;

namespace BlogAspNetMVC.BusinessLogic.Requests.RoleRequest
{
    public class ChangeRoleRequest
    {
        public string OldName { get; set; }
        public string NewName { get; set; }
    }
}
