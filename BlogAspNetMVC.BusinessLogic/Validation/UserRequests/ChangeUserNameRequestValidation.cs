using BlogAspNetMVC.BusinessLogic.Requests.UserRequests;
using FluentValidation;

namespace BlogAspNetMVC.BusinessLogic.Validation.UserRequests
{
    public class ChangeUserNameRequestValidation : AbstractValidator<ChangeUserNameRequest>
    {
        public ChangeUserNameRequestValidation()
        {
            RuleFor(x => x.OldUserName).NotEmpty();
            RuleFor(x => x.NewUserName).NotEmpty();
            RuleFor(x => x.NewUserName).NotEqual(x => x.OldUserName).WithMessage("Имена пользователей должны отличаться");
        }
    }
}
