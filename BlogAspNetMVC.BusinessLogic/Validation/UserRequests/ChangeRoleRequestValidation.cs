using BlogAspNetMVC.BusinessLogic.Requests.UserRequests;
using FluentValidation;

namespace BlogAspNetMVC.BusinessLogic.Validation.UserRequests
{
    public class ChangeRoleRequestValidation : AbstractValidator<ChangeUserRoleRequest>
    {
        public ChangeRoleRequestValidation()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.NewRoleName).NotEmpty();
        }
    }
}
