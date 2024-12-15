using BlogAspNetMVC.BusinessLogic.Requests.UserRequests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAspNetMVC.BusinessLogic.Validation.UserRequests
{
    public class SignInRequestValidation : AbstractValidator<SignInRequest>
    {
        public SignInRequestValidation()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }

    }
}
