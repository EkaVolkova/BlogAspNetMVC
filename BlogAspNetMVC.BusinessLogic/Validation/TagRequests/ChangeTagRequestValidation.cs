using BlogAspNetMVC.BusinessLogic.Requests.TagRequest;
using FluentValidation;

namespace BlogAspNetMVC.BusinessLogic.Validation.TagRequests
{
    public class ChangeTagRequestValidation : AbstractValidator<ChangeTagRequest>
    {
        public ChangeTagRequestValidation()
        {
            RuleFor(x => x.OldName).NotEmpty();
            RuleFor(x => x.NewName).NotEmpty();
            RuleFor(x => x.NewName).NotEqual(x => x.OldName);

        }

    }
}
