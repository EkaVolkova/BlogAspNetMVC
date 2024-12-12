using BlogAspNetMVC.Data.Models;
using BlogAspNetMVC.Data.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAspNetMVC.Data.Repositories
{
    public interface IRoleRepository
    {
        /// <summary>
        /// Создать роль
        /// </summary>
        /// <param name="role">модель роли</param>
        /// <returns></returns>
        Task Create(Role role);

        /// <summary>
        /// Получить роль по идентификатору
        /// </summary>
        /// <param name="guid">Идентификатор роли</param>
        /// <returns></returns>
        Task<Role> GetById(Guid guid);

        /// <summary>
        /// Получить роль по названию
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<Role> GetByName(string name);

        /// <summary>
        /// Получить список всех ролей
        /// </summary>
        /// <returns></returns>
        Task<List<Role>> GetAllRoles();

        /// <summary>
        /// Обновить роль
        /// </summary>
        /// <param name="role">Модель роли</param>
        /// <param name="updateTagQuery">Модель запроса для обновления роли</param>
        /// <returns></returns>
        Task UpdateRole(Role role, UpdateRoleQuery updateRoleQuery);

        /// <summary>
        /// Удаление роли
        /// </summary>
        /// <param name="role">Модель роли</param>
        /// <returns></returns>
        Task DeleteRole(Role role);
    }
}
