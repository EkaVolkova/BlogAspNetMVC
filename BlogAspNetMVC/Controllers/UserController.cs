using BlogAspNetMVC.BusinessLogic.Requests.RoleRequest;
using BlogAspNetMVC.BusinessLogic.Requests.UserRequests;
using BlogAspNetMVC.BusinessLogic.Services;
using BlogAspNetMVC.BusinessLogic.Validation.UserRequests;
using BlogAspNetMVC.BusinessLogic.ViewModels;
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
        public IActionResult ChangeUser(
            [FromRoute]
            Guid id)

        {
            _logger.LogTrace("Открыта вкладка обновления пользователя");
            var user = _userService.GetById(id);
            return View(user);
        }

        /// <summary>
        /// Обновить пользователя
        /// </summary>
        /// <param name="request">Запрос на обновление пользователя</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("ChangeUser")]
        public async Task<IActionResult> ChangeUser(UserViewModel request)
        {
            try
            {
                _logger.LogInformation("Попытка обновления пользователя");

                var result = await _userService.ChangeUser(request);

                _logger.LogInformation($"Пользователь {result.Id} успешно обновлен");

                // После успешного обновления пользователя, выполните вход
                return RedirectToAction("SignIn", "Auth");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка изменения пользователя. {ex}");
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(request);

            }
        }


        [Authorize(Roles = "admin, moderator")]
        [HttpGet]
        [Route("ChangeRole")]
        public IActionResult ChangeRole(
            [FromRoute]
            Guid id)
        {
            _logger.LogTrace("Открыта вкладка обновления роли пользователя администратором или модератором");
            var user = _userService.GetById(id);
            return View(user);
        }

        /// <summary>
        /// Изменить UserName
        /// </summary>
        /// <param name="userViewModel">Модель запроса на изменение UserName</param>
        /// <returns></returns>
        [Authorize(Roles = "admin, moderator")]
        [HttpPost]
        [Route("ChangeRole")]
        public async Task<IActionResult> ChangeRole(
            UserViewModel userViewModel // Объект запроса
)
        {
            try
            {
                var userName = HttpContext.User.Claims.ToList()[0].Value;

                var user = await _userService.GetByUserName(userName);

                _logger.LogInformation($"Попытка обновления роли пользователя для {userViewModel.UserName} пользователем с ролью {user.Role.Name}");

                var result = await _userService.ChangeRole(userViewModel);

                _logger.LogInformation($"Роль пользователя {result.UserName} успешно обновлена");

                //Здесь нужен повторный вход
                return RedirectToAction("Home", "Index");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка изменения пользователя. {ex}");
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(userViewModel);
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
                _logger.LogTrace("Открыта вкладка получения списка пользователей");
                _logger.LogInformation($"Попытка получения списка пользователей");

                var result = await _userService.GetAll();

                _logger.LogInformation($"Список пользователей получен");

                return View(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка получения списка пользователей. {ex}");
                ModelState.AddModelError(string.Empty, ex.Message);

                return View();
            }
        }

        /// <summary>
        /// Получить пользователя
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                _logger.LogTrace("Открыта вкладка получения пользователя по Id");
                _logger.LogInformation($"Попытка получения пользователя по Id");

                var result = await _userService.GetById(id);

                return View(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка получения пользователя по Id. {ex}");
                ModelState.AddModelError(string.Empty, ex.Message);

                return View();
            }
        }
    }
}
