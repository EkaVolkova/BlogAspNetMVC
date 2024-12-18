using BlogAspNetMVC.BusinessLogic.Requests.RoleRequest;
using BlogAspNetMVC.BusinessLogic.Requests.UserRequests;
using BlogAspNetMVC.BusinessLogic.Services;
using BlogAspNetMVC.BusinessLogic.Validation.UserRequests;
using BlogAspNetMVC.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAspNetMVC.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        [Route("ChangeUser")]
        public IActionResult ChangeUser()
        {
            return View();
        }

        /// <summary>
        /// Обновить пользователя
        /// </summary>
        /// <param name="request">Запрос на обновление пользователя</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("ChangeUser")]
        public async Task<IActionResult> ChangeUser(ChangeUserRequest request)
        {
            try
            {
                var userName = HttpContext.User.Claims.ToList()[0].Value;

                var user = await _userService.GetByUserName(userName);

                request.OldName = user.UserName;

                var validator = new ChangeUserRequestValidation();
                var validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    _logger.LogError($"Ошибка валидации. {validationResult.Errors}");
                    ModelState.AddModelError(string.Empty, validationResult.Errors.ToString());

                    return View();
                }
                var result = await _userService.ChangeUser(request);
                // После успешного обновления пользователя, выполните вход
                return RedirectToAction("SignIn", "Auth");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка изменения пользователя. {ex}");
                ModelState.AddModelError(string.Empty, ex.Message);
                return BadRequest(ModelState);

            }
        }


        [Authorize]
        [HttpGet]
        [Route("ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        /// Изменить пароль
        /// </summary>
        /// <param name="request">Модель запроса на изменение пароля</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(
            ChangePasswordRequest request // Объект запроса
)
        {
            try
            {

                var userName = HttpContext.User.Claims.ToList()[0].Value;

                var user = await _userService.GetByUserName(userName);

                request.UserName = user.UserName;

                var validator = new ChangePasswordRequestValidation();
                var validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    _logger.LogError($"Ошибка валидации. {validationResult.Errors}");
                    ModelState.AddModelError(string.Empty, validationResult.Errors.ToString());
                    return View();
                }
                var result = await _userService.ChangePassword(request);

                //Здесь нужен повторный вход
                return RedirectToAction("SignIn", "Auth");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка изменения пользователя. {ex}");
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();

        }

        [Authorize]
        [HttpGet]
        [Route("ChangeUserName")]
        public IActionResult ChangeUserName()
        {
            return View();
        }

        /// <summary>
        /// Изменить UserName
        /// </summary>
        /// <param name="request">Модель запроса на изменение UserName</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("ChangeUserName")]
        public async Task<IActionResult> ChangeUserName(
            ChangeUserNameRequest request // Объект запроса
)
        {
            try
            {
                var userName = HttpContext.User.Claims.ToList()[0].Value;

                var user = await _userService.GetByUserName(userName);

                request.OldName = user.UserName;

                var validator = new ChangeUserNameRequestValidation();
                var validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    _logger.LogError($"Ошибка валидации. {validationResult.Errors}");
                    ModelState.AddModelError(string.Empty, validationResult.Errors.ToString());

                    return View();
                }
                var result = await _userService.ChangeUserName(request);
                //Здесь нужен повторный вход
                return RedirectToAction("SignIn", "Auth");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка изменения пользователя. {ex}");
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }

        /// <summary>
        /// Получить список всех пользователей
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var result = await _userService.GetAll();

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }
        }

        /// <summary>
        /// Получить пользователя
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("GetUserById{id}")]
        public async Task<IActionResult> GetUserById(
            [FromRoute] Guid id)
        {
            try
            {
                var result = await _userService.GetById(id);

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }
        }
    }
}
