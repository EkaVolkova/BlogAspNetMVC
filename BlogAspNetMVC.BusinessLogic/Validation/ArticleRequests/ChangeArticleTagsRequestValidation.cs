using BlogAspNetMVC.BusinessLogic.Requests.ArticleRequests;
using FluentValidation;

namespace BlogAspNetMVC.BusinessLogic.Validation.ArticleRequests
{
    public class ChangeArticleTagsRequestValidation : AbstractValidator<ChangeArticleTagsRequest>
    {
        public ChangeArticleTagsRequestValidation()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Tags).NotEmpty().WithMessage("Должен быть добавлен хотя бы один тег");
        }

    }
}
