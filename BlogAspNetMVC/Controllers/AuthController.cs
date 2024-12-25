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
            _logger.LogTrace("Запущен процесс аутентификации");
            //Инициализируем список утверждений проверки подлинности
            var claims = new List<Claim>()
            {
                //Добавляем одно утверждение с типом по умолчанию и логином пользователя
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                //Добавляем одно утверждение с типом роли по умолчанию и ролью
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
            };

            //Создали объект класса ClaimsIdentity, который реализует интерфейс IIdentity и представляет текущего пользователя
            ClaimsIdentity claimsIdentity = new(
                claims,
                "AppCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            //Добавляем в контекст ClaimsPrincipal для работы с авторизацией, который содержит ClaimsIdentity
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            _logger.LogTrace("Завершен процесс аутентификации");

        }

        /// <summary>
        /// Функция отмены аутентификации
        /// </summary>
        /// <returns></returns>
        private async Task AuthentCanselAsync()
        {
            _logger.LogTrace("Запущен процесс сброса аутентификации");
            await HttpContext.SignOutAsync();
            _logger.LogTrace("Завершен процесс сброса аутентификации");

        }

        /// <summary>
        /// Вход пользователя
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("SignIn")]
        public async Task<IActionResult> SignIn()
        {
            _logger.LogTrace("Открыта вкладка входа в систему");
            //Выходим, если до этого был совершен вход
            await AuthentCanselAsync();
            return View();
        }

        /// <summary>
        /// Вход пользователя
        /// </summary>
        /// <param name="request">Запрос на вход</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("SignIn")]
        public async Task<IActionResult> SignInAsync(SignInRequest request)
        {
            try
            {
                _logger.LogInformation("Попытка входа в систему");
                var validator = new SignInRequestValidation();
                var validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    _logger.LogError($"Ошибка валидации. {validationResult.Errors}");
                    ModelState.AddModelError(string.Empty, "Некорректное имя пользователя или пароль.");
                    return View();
                }
                var result = await _userService.SignIn(request);
                await AuthentAsync(result);
                _logger.LogInformation($"Пользователь {result.Id} вошел в систему");
                return RedirectToAction("Index", "User");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка входа. {ex}");
                ModelState.AddModelError(string.Empty, ex.Message);

            }
            return View(request);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("SignUp")]
        public IActionResult SignUp()
        {
            _logger.LogTrace("Открыта вкладка регистрации");
            return View();
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
            SignUpRequest request // Объект запроса
            )
        {
            try
            {
                _logger.LogInformation("Попытка зарегестрироваться в системе");
                var validator = new SignUpRequestValidation();
                var validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    _logger.LogError($"Ошибка валидации. {validationResult.Errors}");
                    ModelState.AddModelError(string.Empty, "Некорректное имя пользователя или пароль.");
                    return View();
                }
                var result = await _userService.SignUp(request);
                await AuthentAsync(result);
                _logger.LogInformation($"Пользователь {result.Id} зарегестрирован в системе");
                return RedirectToAction("Index", "User");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка входа. {ex}");
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();

        }

    }
}
