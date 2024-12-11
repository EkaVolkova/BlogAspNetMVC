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

        [HttpGet]
        [Route("SignIn")]
        public async Task<IActionResult> SignIn(
            [FromBody] // Атрибут, указывающий, откуда брать значение объекта
            SignInRequest request // Объект запроса
            )
        {
            var result = await _userService.SignIn(request);
            var status = (ObjectResult)result;
            return StatusCode((int)(status.StatusCode), status.Value);

        }

        [HttpPost]
        [Route("SignUp")]
        public async Task<IActionResult> SignUp(
            [FromBody] // Атрибут, указывающий, откуда брать значение объекта
            SignUpRequest request // Объект запроса
            )
        {
            var result = await _userService.SignUp(request);
            var status = (ObjectResult)result;
            return StatusCode((int)(status.StatusCode), status.Value);

        }

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

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAll();
            var status = (ObjectResult)result;
            return StatusCode((int)(status.StatusCode), status.Value);
        }

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
