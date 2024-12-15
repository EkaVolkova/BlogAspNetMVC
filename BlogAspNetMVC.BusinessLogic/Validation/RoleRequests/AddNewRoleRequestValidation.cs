using BlogAspNetMVC.BusinessLogic.Requests.RoleRequest;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAspNetMVC.BusinessLogic.Validation.RoleRequests
{
    public class AddNewRoleRequestValidation : AbstractValidator<AddNewRoleRequest>
    {
        public AddNewRoleRequestValidation()
        {
            RuleFor(x => x.Name).NotEmpty();
        }

    }
}
