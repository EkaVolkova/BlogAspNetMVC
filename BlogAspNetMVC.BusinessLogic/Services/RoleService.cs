using AutoMapper;
using BlogAspNetMVC.BusinessLogic.Requests.RoleRequest;
using BlogAspNetMVC.Data.Models;
using BlogAspNetMVC.Data.Queries;
using BlogAspNetMVC.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogAspNetMVC.BusinessLogic.Services
{
    public class RoleService : IRoleService
    {
        IRoleRepository _roleRepository;
        IMapper _mapper;

        public RoleService(
                                IRoleRepository roleRepository,
                                IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Добавить роль пользователя
        /// </summary>
        /// <param name="addNewRoleRequest">Запрос на добавление роли</param>
        /// <returns></returns>
        public async Task<IActionResult> AddRole(AddNewRoleRequest addNewRoleRequest)
        {
            var role = await _roleRepository.GetById(addNewRoleRequest.Guid);
            if (!(role is null))
                return new ObjectResult($"Роль с Id \"{addNewRoleRequest.Guid}\" не найдена") { StatusCode = 400 };



            role = _mapper.Map<AddNewRoleRequest, Role>(addNewRoleRequest);

            await _roleRepository.Create(role);

            return new ObjectResult($"Роль {role.Name} создана") { StatusCode = 200 };
        }

        /// <summary>
        /// Изменить роль пользователя
        /// </summary>
        /// <param name="changeRoleRequest">Запрос на изменение роли пользователя</param>
        /// <returns></returns>
        public async Task<IActionResult> ChangeRole(ChangeRoleRequest changeRoleRequest)
        {
            var role = await _roleRepository.GetById(changeRoleRequest.Id);
            if (role is null)
                return new ObjectResult($"Роль с Id \"{changeRoleRequest.Id}\" не найдена") { StatusCode = 400 };


            var query = _mapper.Map<ChangeRoleRequest, UpdateRoleQuery>(changeRoleRequest);
            await _roleRepository.UpdateRole(role, query);
            return new ObjectResult($"Роль с Id {changeRoleRequest.Id} обновлена") { StatusCode = 200 };

        }

        /// <summary>
        /// Удалить роль пользователя
        /// </summary>
        /// <param name="guid">Идентификатор роли пользователя</param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteRole(Guid guid)
        {
            var role = await _roleRepository.GetById(guid);
            if (role is null)
                return new ObjectResult($"Роль с Id \"{guid}\" не найдена") { StatusCode = 400 };

            await _roleRepository.DeleteRole(role);
            return new ObjectResult($"Роль с Id {guid} удалена") { StatusCode = 200 };
        }

        /// <summary>
        /// Получить список всех ролей
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleRepository.GetAllRoles();
            if (roles is null || roles.Count == 0)
                return new ObjectResult("Нет ролей") { StatusCode = 400 };

            return new ObjectResult(roles) { StatusCode = 200 };
        }

        /// <summary>
        /// Получить роль по идентификатору
        /// </summary>
        /// <param name="guid">Идентификатор роли пользователя</param>
        /// <returns></returns>
        public async Task<IActionResult> GetRoleById(Guid guid)
        {
            var role = await _roleRepository.GetById(guid);
            if (role is null)
                return new ObjectResult($"Роль с Id \"{guid}\" не найдена") { StatusCode = 400 };

            return new ObjectResult(role) { StatusCode = 200 };
        }

        /// <summary>
        /// Получить роль по названию
        /// </summary>
        /// <param name="articleName">Название роли пользователя</param>
        /// <returns></returns>
        public async Task<IActionResult> GetRoleByName(string name)
        {
            var role = await _roleRepository.GetByName(name);
            if (role is null)
                return new ObjectResult($"Роль с названием \"{name}\" не найдена") { StatusCode = 400 };

            return new ObjectResult(role) { StatusCode = 200 };
        }

        /// <summary>
        /// Получить список всех пользователей с данной ролью
        /// </summary>
        /// <param name="guid">Идентификатор роли пользователя</param>
        /// <returns></returns>
        public async Task<IActionResult> GetAllUsersByRoleId(Guid guid)
        {
            var role = await _roleRepository.GetById(guid);
            if (role is null)
                return new ObjectResult($"Роль с Id \"{guid}\" не найдена") { StatusCode = 400 };

            if (role.Users is null || role.Users.Count == 0)
                return new ObjectResult($"Нет пользователей роли {guid}") { StatusCode = 400 };


            return new ObjectResult(role.Users) { StatusCode = 200 };
        }

    }
}
