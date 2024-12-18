using BlogAspNetMVC.BusinessLogic.Requests.RoleRequest;
using BlogAspNetMVC.BusinessLogic.Services;
using BlogAspNetMVC.BusinessLogic.Validation.RoleRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BlogAspNetMVC.Controllers
{
    /// <summary>
    /// Контроллер для работы с ролью
    /// </summary>
    [Authorize(Roles = "admin")]
    [Route("[controller]")]
    public class RoleController : Controller
    {
        readonly IRoleService _roleService;
        readonly ILogger<RoleController> _logger;

        public RoleController(ILogger<RoleController> logger, IRoleService roleService)
        {
            _roleService = roleService;
            _logger = logger;
        }


        /// <summary>
        /// Создать новую роль
        /// </summary>
        /// <param name="addNewRoleRequest">запрос на создание роли</param>
        /// <returns></returns>
        [HttpGet]
        [Route("CreateNewRole")]
        public IActionResult CreateNewRole()
        {
            _logger.LogTrace($"Открыта вкладка добавления роли");
            return View();
        }

        /// <summary>
        /// Создать новую роль
        /// </summary>
        /// <param name="addNewRoleRequest">запрос на создание роли</param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateNewRole")]
        public async Task<IActionResult> CreateNewRole(
        [FromBody]
            AddNewRoleRequest addNewRoleRequest)
        {
            try
            {
                _logger.LogInformation($"Попытка добавить роль");
                var validator = new AddNewRoleRequestValidation();
                var validationResult = validator.Validate(addNewRoleRequest);

                if (!validationResult.IsValid)
                {
                    _logger.LogError($"Ошибка валидации {validationResult.Errors}");
                    ModelState.AddModelError(string.Empty, validationResult.Errors.ToString());
                    return View();
                }
                var result = await _roleService.AddRole(addNewRoleRequest);

                return View(addNewRoleRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка добавления роли {ex}");
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }

        /// <summary>
        /// Изменить роль
        /// </summary>
        /// <param name="changeRoleRequest">Модель запроса на обновление роли</param>
        /// <returns></returns>
        [HttpPost]
        [Route("ChangeRole")]
        public async Task<IActionResult> ChangeRole(
            [FromBody]
            ChangeRoleRequest changeRoleRequest)
        {
            try
            {
                var validator = new ChangeRoleRequestValidation();
                var validationResult = validator.Validate(changeRoleRequest);

                if (!validationResult.IsValid)
                {
                    _logger.LogError($"Ошибка валидации {validationResult.Errors}");
                    ModelState.AddModelError(string.Empty, validationResult.Errors.ToString());
                    return View();
                }
                var result = await _roleService.ChangeRole(changeRoleRequest);

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }
        }

        /// <summary>
        /// Удалить роль
        /// </summary>
        /// <param name="guid">Идентификатор роли</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteRole{guid}")]
        public async Task<IActionResult> DeleteRole(
            [FromRoute] Guid guid)
        {
            try
            {
                await _roleService.DeleteRole(guid);

                return StatusCode(200, "Роль удалена");
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }
        }

        /// <summary>
        /// Получить список всех ролей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                _logger.LogTrace($"Открыта вкладка получения ролей");
                _logger.LogInformation($"Попытка получения ролей");
                var result = await _roleService.GetAllRoles();

                return View(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка получения ролей {ex}");
                ModelState.AddModelError(string.Empty, "получения ролей");
            }
            return View();
        }


        /// <summary>
        /// Получить роль
        /// </summary>
        /// <param name="guid">Идентификатор роли</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetRoleById{guid}")]
        public async Task<IActionResult> GetRoleById(
            [FromRoute] Guid guid)
        {
            try
            {
                var result = await _roleService.GetRoleById(guid);

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }
        }


        /// <summary>
        /// Получить список пользователь с выбранной ролью
        /// </summary>
        /// <param name="guid">Идентификатор роли</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllUsersByRoleId")]
        public async Task<IActionResult> GetAllUsersByRoleId(
            [FromRoute] Guid guid)
        {
            try
            {
                var result = await _roleService.GetAllUsersByRoleId(guid);

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }
        }

    }
}
