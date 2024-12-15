using BlogAspNetMVC.BusinessLogic.Requests.UserRequests;
using FluentValidation;

namespace BlogAspNetMVC.BusinessLogic.Validation.UserRequests
{
    public class ChangePasswordRequestValidation : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordRequestValidation()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.OldPassword).NotEmpty();
            RuleFor(x => x.NewPasssword).NotEmpty();
            RuleFor(x => x.NewPasssword).NotEqual(x => x.OldPassword);
        }
    }
}
