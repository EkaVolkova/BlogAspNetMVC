using BlogAspNetMVC.BusinessLogic.Requests.RoleRequest;
using BlogAspNetMVC.BusinessLogic.ViewModels;
using BlogAspNetMVC.Data.Models;
using BlogAspNetMVC.Data.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogAspNetMVC.BusinessLogic.Services
{
    public interface IRoleService
    {
        /// <summary>
        /// Добавить роль пользователя
        /// </summary>
        /// <param name="addNewRoleRequest">Запрос на добавление роли</param>
        /// <returns></returns>
        Task<RoleViewModel> AddRole(AddNewRoleRequest addNewRoleRequest);

        /// <summary>
        /// Изменить роль пользователя
        /// </summary>
        /// <param name="changeRoleRequest">Запрос на изменение роли пользователя</param>
        /// <returns></returns>
        Task<RoleViewModel> ChangeRole(ChangeRoleRequest changeRoleRequest);

        /// <summary>
        /// Удалить роль пользователя
        /// </summary>
        /// <param name="guid">Идентификатор роли пользователя</param>
        /// <returns></returns>
        Task DeleteRole(Guid guid);


        /// <summary>
        /// Получить список всех ролей
        /// </summary>
        /// <returns></returns>
        Task<List<RoleViewModel>> GetAllRoles();


        /// <summary>
        /// Получить роль по идентификатору
        /// </summary>
        /// <param name="guid">Идентификатор роли пользователя</param>
        /// <returns></returns>
        Task<RoleViewModel> GetRoleById(Guid guid);

        /// <summary>
        /// Получить роль по названию
        /// </summary>
        /// <param name="articleName">Название роли пользователя</param>
        /// <returns></returns>
        Task<RoleViewModel> GetRoleByName(string name);

        /// <summary>
        /// Получить список всех пользователей с данной ролью
        /// </summary>
        /// <param name="guid">Идентификатор роли пользователя</param>
        /// <returns></returns>
        Task<List<UserViewModel>> GetAllUsersByRoleId(Guid guid);
    }
}
