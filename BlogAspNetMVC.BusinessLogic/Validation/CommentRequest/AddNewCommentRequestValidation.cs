using BlogAspNetMVC.BusinessLogic.Requests.CommentRequest;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAspNetMVC.BusinessLogic.Validation.CommentRequest
{
    public class AddNewCommentRequestValidation : AbstractValidator<AddNewCommentRequest>
    {
        public AddNewCommentRequestValidation()
        {
            RuleFor(x => x.Text).NotEmpty();
            RuleFor(x => x.AuthorId).Must(BeNotDefault).WithMessage("Поле AuthorId должно быть заполнено");
            RuleFor(x => x.ArticleId).Must(BeNotDefault).WithMessage("Поле ArtcleId должно быть заполнено");
        }

        private bool BeNotDefault(Guid guid)
        {
            return guid != Guid.Empty;
        }
    }
}
