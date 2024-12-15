using BlogAspNetMVC.BusinessLogic.Requests.ArticleRequests;
using FluentValidation;
using System;

namespace BlogAspNetMVC.BusinessLogic.Validation.ArticleRequests
{
    public class ChangeArticleRequestValidation : AbstractValidator<ChangeArticleRequest>
    {
        public ChangeArticleRequestValidation()
        {
            RuleFor(x => x.OldName).NotEmpty();
            RuleFor(x => x.NewName).NotEmpty();
            RuleFor(x => x.NewText).NotEmpty();
        }

    }
}
