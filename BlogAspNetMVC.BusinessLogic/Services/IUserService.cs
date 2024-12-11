using BlogAspNetMVC.BusinessLogic.Requests.UserRequests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogAspNetMVC.BusinessLogic.Services
{
    public interface IUserService
    {
        Task<IActionResult> SignIn(SignInRequest signInRequest);

        Task<IActionResult> SignUp(SignUpRequest signUpRequest);

        Task<IActionResult> ChangePassword(ChangePasswordRequest changePasswordRequest);

        Task<IActionResult> ChangeUserName(ChangeUserNameRequest changeUserNameRequest);
        Task<IActionResult> GetAll();
        Task<IActionResult> GetById(Guid guid);
    }
}
