using BlogAspNetMVC.BusinessLogic.Requests.ArticleRequests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAspNetMVC.BusinessLogic.Validation.ArticleRequests
{
    public class AddNewArticleRequestValidation : AbstractValidator<AddNewArticleRequest>
    {
        public AddNewArticleRequestValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Поле \"Название\" должно быть заполнено");
            RuleFor(x => x.Text).NotEmpty().WithMessage("Поле \"Текст\" должно быть заполнено");
            RuleFor(x => x.Tags).NotNull().NotEmpty().WithMessage("Добавьте хотя бы один тег");
            RuleFor(x => x.AuthorId).Must(BeNotDefault).WithMessage("Поле AuthorId должно быть заполнено");
        }

        private bool BeNotDefault(Guid guid)
        {
            return guid != Guid.Empty;
        }
    }
}
