using BlogAspNetMVC.BusinessLogic.Requests.RoleRequest;
using BlogAspNetMVC.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BlogAspNetMVC.Controllers
{
    /// <summary>
    /// Контроллер для работы с ролью
    /// </summary>
    [Route("[controller]")]
    public class RoleController : Controller
    {
        IRoleService _roleService;
        ILogger<RoleController> _logger;

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
        [HttpPost]
        [Route("CreateNewRole")]
        public async Task<IActionResult> CreateNewRole(
            [FromBody]
            AddNewRoleRequest addNewRoleRequest)
        {
            try
            {
                var result = await _roleService.AddRole(addNewRoleRequest);

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }
        }

        /// <summary>
        /// Изменить роль
        /// </summary>
        /// <param name="changeRoleRequest">Модель запроса на обновление роли</param>
        /// <returns></returns>
        [HttpPut]
        [Route("ChangeRole")]
        public async Task<IActionResult> ChangeRole(
            [FromBody]
            ChangeRoleRequest changeRoleRequest)
        {
            try
            {
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
                var result = await _roleService.GetAllRoles();

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }
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
