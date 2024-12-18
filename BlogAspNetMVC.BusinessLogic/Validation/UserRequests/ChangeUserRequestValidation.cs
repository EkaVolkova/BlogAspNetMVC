using BlogAspNetMVC.BusinessLogic.Requests.UserRequests;
using FluentValidation;

namespace BlogAspNetMVC.BusinessLogic.Validation.UserRequests
{
    public class ChangeUserRequestValidation : AbstractValidator<ChangeUserRequest>
    {
        public ChangeUserRequestValidation()
        {
            RuleFor(x => x.OldName).NotEmpty();
            RuleFor(x => x.NewName).NotEmpty();
            RuleFor(x => x.NewPassword).NotEmpty();
            RuleFor(x => x.NewEmail).NotEmpty().EmailAddress();
        }
    }
}
