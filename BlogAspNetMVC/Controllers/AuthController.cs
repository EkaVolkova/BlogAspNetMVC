using AutoMapper;
using BlogAspNetMVC.BusinessLogic.Requests.RoleRequest;
using BlogAspNetMVC.BusinessLogic.Requests.UserRequests;
using BlogAspNetMVC.BusinessLogic.Services;
using BlogAspNetMVC.BusinessLogic.Validation.UserRequests;
using BlogAspNetMVC.BusinessLogic.ViewModels;
using BlogAspNetMVC.Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogAspNetMVC.Controllers
{
    /// <summary>
    /// Контроллер для работы с аутентификацией
    /// </summary>
    [Route("[controller]")]
    public class AuthController : Controller
    {
        readonly IUserService _userService;
        readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger, IUserService authService)
        {
            _userService = authService;
            _logger = logger;
        }


        /// <summary>
        /// Функция аутентификации
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task AuthentAsync(UserViewModel user)
        {
            
            //Инициализируем список утверждений проверки подлинности
            var claims = new List<Claim>()
            {
                //Добавляем одно утверждение с типом по умолчанию и логином пользователя
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                //Добавляем одно утверждение с типом роли по умолчанию и ролью
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleName)
            };

            //Создали объект класса ClaimsIdentity, который реализует интерфейс IIdentity и представляет текущего пользователя
            ClaimsIdentity claimsIdentity = new(
                claims,
                "AppCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            //Добавляем в контекст ClaimsPrincipal для работы с авторизацией, который содержит ClaimsIdentity
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        }

        /// <summary>
        /// Вход пользователя
        /// </summary>
        /// <param name="request">Запрос на вход</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("SignIn")]
        public async Task<IActionResult> SignIn(
            [FromBody] // Атрибут, указывающий, откуда брать значение объекта
            SignInRequest request // Объект запроса
            )
        {
            try
            {
                var validator = new SignInRequestValidation();
                var validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                var result = await _userService.SignIn(request);
                await AuthentAsync(result);

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }

        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="request">Запрос на регистрацию</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("SignUp")]
        public async Task<IActionResult> SignUp(
            [FromBody] // Атрибут, указывающий, откуда брать значение объекта
            SignUpRequest request // Объект запроса
            )
        {
            try
            {
                var validator = new SignUpRequestValidation();
                var validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                var result = await _userService.SignUp(request);
                await AuthentAsync(result);

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }

        }

    }
}
