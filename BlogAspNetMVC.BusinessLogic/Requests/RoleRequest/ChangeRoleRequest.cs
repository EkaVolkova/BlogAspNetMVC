using System;

namespace BlogAspNetMVC.BusinessLogic.Requests.RoleRequest
{
    public class ChangeRoleRequest
    {
        public Guid Id { get; set; }
        public string NewName { get; set; }
    }
}
