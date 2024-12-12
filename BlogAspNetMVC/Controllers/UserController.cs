using BlogAspNetMVC.BusinessLogic.Requests.RoleRequest;
using BlogAspNetMVC.BusinessLogic.Requests.UserRequests;
using BlogAspNetMVC.BusinessLogic.Services;
using BlogAspNetMVC.Data.Repositories;
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
        IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        /// Изменить пароль
        /// </summary>
        /// <param name="request">Модель запроса на изменение пароля</param>
        /// <returns></returns>
        [HttpPut]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(
            [FromBody] // Атрибут, указывающий, откуда брать значение объекта
            ChangePasswordRequest request // Объект запроса
)
        {
            var result = await _userService.ChangePassword(request);

            var status = (ObjectResult)result;
            return StatusCode((int)(status.StatusCode), status.Value);

        }

        /// <summary>
        /// Изменить UserName
        /// </summary>
        /// <param name="request">Модель запроса на изменение UserName</param>
        /// <returns></returns>
        [HttpPut]
        [Route("ChangeUserName")]
        public async Task<IActionResult> ChangeUserName(
           [FromBody] // Атрибут, указывающий, откуда брать значение объекта
            ChangeUserNameRequest request // Объект запроса
)
        {
            var result = await _userService.ChangeUserName(request);

            var status = (ObjectResult)result;
            return StatusCode((int)(status.StatusCode), status.Value);

        }

        /// <summary>
        /// Получить список всех пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAll();
            var status = (ObjectResult)result;
            return StatusCode((int)(status.StatusCode), status.Value);
        }

        /// <summary>
        /// Получить пользователя
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserById{id}")]
        public async Task<IActionResult> GetUserById(
            [FromRoute] Guid id)
        {

            var result = await _userService.GetById(id);
            var status = (ObjectResult)result;
            return StatusCode((int)(status.StatusCode), status.Value);
        }
    }
}
