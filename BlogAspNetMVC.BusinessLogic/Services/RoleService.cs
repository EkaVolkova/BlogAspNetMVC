using AutoMapper;
using BlogAspNetMVC.BusinessLogic.Exceptions.RoleExceptions;
using BlogAspNetMVC.BusinessLogic.Requests.RoleRequest;
using BlogAspNetMVC.BusinessLogic.ViewModels;
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
        public async Task<RoleViewModel> AddRole(AddNewRoleRequest addNewRoleRequest)
        {
            var role = await _roleRepository.GetByName(addNewRoleRequest.Name);
            //если роли уже существует
            if (!(role is null))
                throw new RoleAlreadyExistException($"Роль с названием \"{addNewRoleRequest.Name}\" уже существует");


            role = _mapper.Map<AddNewRoleRequest, Role>(addNewRoleRequest);

            await _roleRepository.Create(role);

            role = await _roleRepository.GetByName(addNewRoleRequest.Name);

            var roleView = _mapper.Map<Role, RoleViewModel>(role);

            return roleView;
        }

        /// <summary>
        /// Изменить роль пользователя
        /// </summary>
        /// <param name="changeRoleRequest">Запрос на изменение роли пользователя</param>
        /// <returns></returns>
        public async Task<RoleViewModel> ChangeRole(ChangeRoleRequest changeRoleRequest)
        {
            var role = await _roleRepository.GetByName(changeRoleRequest.OldName);
            //Если роли не существует
            if (role is null)
                throw new RoleNotFoundException($"Роль с названием \"{changeRoleRequest.OldName}\" не найдена");


            var query = _mapper.Map<ChangeRoleRequest, UpdateRoleQuery>(changeRoleRequest);
            await _roleRepository.UpdateRole(role, query);

            role = await _roleRepository.GetByName(changeRoleRequest.NewName);

            var roleView = _mapper.Map<Role, RoleViewModel>(role);

            return roleView;
        }

        /// <summary>
        /// Удалить роль пользователя
        /// </summary>
        /// <param name="guid">Идентификатор роли пользователя</param>
        /// <returns></returns>
        public async Task DeleteRole(Guid guid)
        {
            var role = await _roleRepository.GetById(guid);
            //Если роли не существует
            if (role is null)
                throw new RoleNotFoundException($"Роль с Id \"{guid}\" не найдена");


            await _roleRepository.DeleteRole(role);
        }

        /// <summary>
        /// Получить список всех ролей
        /// </summary>
        /// <returns></returns>
        public async Task<List<RoleViewModel>> GetAllRoles()
        {
            var roles = await _roleRepository.GetAllRoles();
            if (roles is null || roles.Count == 0)
                return new List<RoleViewModel>();

            var rolesView = _mapper.Map<List<Role>, List<RoleViewModel>>(roles);

            return rolesView;
        }

        /// <summary>
        /// Получить роль по идентификатору
        /// </summary>
        /// <param name="guid">Идентификатор роли пользователя</param>
        /// <returns></returns>
        public async Task<RoleViewModel> GetRoleById(Guid guid)
        {
            var role = await _roleRepository.GetById(guid);
            //Если роли не существует
            if (role is null)
                throw new RoleNotFoundException($"Роль с Id \"{guid}\" не найдена");

            var roleView = _mapper.Map<Role, RoleViewModel>(role);

            return roleView;
        }

        /// <summary>
        /// Получить роль по названию
        /// </summary>
        /// <param name="articleName">Название роли пользователя</param>
        /// <returns></returns>
        public async Task<RoleViewModel> GetRoleByName(string name)
        {
            var role = await _roleRepository.GetByName(name);
            if (role is null)
                throw new RoleNotFoundException($"Роль с названием \"{name}\" не найдена");

            var roleView = _mapper.Map<Role, RoleViewModel>(role);

            return roleView;
        }

        /// <summary>
        /// Получить список всех пользователей с данной ролью
        /// </summary>
        /// <param name="guid">Идентификатор роли пользователя</param>
        /// <returns></returns>
        public async Task<List<UserViewModel>> GetAllUsersByRoleId(Guid guid)
        {
            var role = await _roleRepository.GetById(guid);
            //Если роли не существует
            if (role is null)
                throw new RoleNotFoundException($"Роль с Id \"{guid}\" не найдена");

            if (role.Users is null)
                return new List<UserViewModel>();

            var usersView = _mapper.Map<List<User>, List<UserViewModel>>(role.Users);

            return usersView;
        }

    }
}
