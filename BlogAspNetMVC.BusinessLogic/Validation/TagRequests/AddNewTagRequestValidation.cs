using BlogAspNetMVC.BusinessLogic.Requests.TagRequest;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAspNetMVC.BusinessLogic.Validation.TagRequests
{
    public class AddNewTagRequestValidation : AbstractValidator<AddNewTagRequest>
    {
        public AddNewTagRequestValidation()
        {
            RuleFor(x => x.Name).NotEmpty();
        }

    }
}
