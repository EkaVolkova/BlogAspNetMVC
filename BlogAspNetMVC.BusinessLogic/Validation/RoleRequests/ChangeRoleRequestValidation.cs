using BlogAspNetMVC.BusinessLogic.Requests.RoleRequest;
using FluentValidation;

namespace BlogAspNetMVC.BusinessLogic.Validation.RoleRequests
{
    public class ChangeRoleRequestValidation : AbstractValidator<ChangeRoleRequest>
    {
        public ChangeRoleRequestValidation()
        {
            RuleFor(x => x.OldName).NotEmpty();
            RuleFor(x => x.NewName).NotEmpty();
        }

    }
}
