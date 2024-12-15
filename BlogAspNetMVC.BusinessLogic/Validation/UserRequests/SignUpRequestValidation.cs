using BlogAspNetMVC.BusinessLogic.Requests.UserRequests;
using FluentValidation;

namespace BlogAspNetMVC.BusinessLogic.Validation.UserRequests
{
    public class SignUpRequestValidation : AbstractValidator<SignUpRequest>
    {
        public SignUpRequestValidation()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Email).EmailAddress();
        }
    }
}
