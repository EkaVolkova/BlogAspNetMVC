using BlogAspNetMVC.BusinessLogic.Requests.UserRequests;
using FluentValidation;

namespace BlogAspNetMVC.BusinessLogic.Validation.UserRequests
{
    public class ChangeUserNameRequestValidation : AbstractValidator<ChangeUserNameRequest>
    {
        public ChangeUserNameRequestValidation()
        {
            RuleFor(x => x.OldName).NotEmpty();
            RuleFor(x => x.NewName).NotEmpty();
            RuleFor(x => x.NewName).NotEqual(x => x.OldName).WithMessage("Имена пользователей должны отличаться");
        }
    }
}
