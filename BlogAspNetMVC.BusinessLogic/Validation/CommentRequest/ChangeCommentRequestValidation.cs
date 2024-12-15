using BlogAspNetMVC.BusinessLogic.Requests.CommentRequest;
using FluentValidation;
using System;

namespace BlogAspNetMVC.BusinessLogic.Validation.CommentRequest
{
    public class ChangeCommentRequestValidation : AbstractValidator<ChangeCommentRequest>
    {
        public ChangeCommentRequestValidation()
        {
            RuleFor(x => x.NewText).NotEmpty();
            RuleFor(x => x.Id).Must(BeNotDefault).WithMessage("Поле Id должно быть заполнено");
        }

        private bool BeNotDefault(Guid guid)
        {
            return guid != Guid.Empty;
        }
    }
}
