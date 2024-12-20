﻿using BlogAspNetMVC.BusinessLogic.Requests.RoleRequest;
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

        /// <summary>
        /// Изменить пароль
        /// </summary>
        /// <param name="request">Модель запроса на изменение пароля</param>
        /// <returns></returns>
        [Authorize(Roles = "admin, moderator, user")]
        [HttpPut]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(
            [FromBody] // Атрибут, указывающий, откуда брать значение объекта
            ChangePasswordRequest request // Объект запроса
)
        {
            try
            {
                var validator = new ChangePasswordRequestValidation();
                var validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                var result = await _userService.ChangePassword(request);

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }

        }

        /// <summary>
        /// Изменить UserName
        /// </summary>
        /// <param name="request">Модель запроса на изменение UserName</param>
        /// <returns></returns>
        [Authorize(Roles = "admin, moderator, user")]
        [HttpPut]
        [Route("ChangeUserName")]
        public async Task<IActionResult> ChangeUserName(
           [FromBody] // Атрибут, указывающий, откуда брать значение объекта
            ChangeUserNameRequest request // Объект запроса
)
        {
            try
            {
                var validator = new ChangeUserNameRequestValidation();
                var validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                var result = await _userService.ChangeUserName(request);

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }

        }

        /// <summary>
        /// Получить список всех пользователей
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "admin, moderator, user")]
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
        [Authorize(Roles = "admin, moderator, user")]
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
