using BlogAspNetMVC.Data.Models;
using BlogAspNetMVC.Data.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BlogAspNetMVC.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        DataContext _context;
        public RoleRepository(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Создать роль
        /// </summary>
        /// <param name="role">модель роли</param>
        /// <returns></returns>
        public async Task Create(Role role)
        {
            
            //Добавляем роль в асинхронном режиме
            var entry = _context.Entry(role);
            if (entry.State == EntityState.Detached)
                await _context.Roles.AddAsync(role);

            // Сохраняем изменения в базе 
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Получить роль по идентификатору
        /// </summary>
        /// <param name="guid">Идентификатор роли</param>
        /// <returns></returns>
        public async Task<Role> GetById(Guid guid)
        {
            //Получаем роль вместе со всеми связанными сущностями
            return await _context.Roles.Include(r => r.Users)
                                              .Where(r => r.Id == guid)
                                              .FirstOrDefaultAsync();
        }


        /// <summary>
        /// Получить роль по названию
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Role> GetByName(string name)
        {
            //Получаем роль вместе со всеми связанными сущностями
            return await _context.Roles.Include(r => r.Users)
                                              .Where(r => r.Name == name)
                                              .FirstOrDefaultAsync();

        }

        /// <summary>
        /// Получить список всех ролей
        /// </summary>
        /// <returns></returns>
        public async Task<List<Role>> GetAllRoles()
        {
            //Получаем роль вместе со всеми связанными сущностями
            return await _context.Roles.Include(r => r.Users)
                                              .ToListAsync();

        }

        /// <summary>
        /// Обновить роль
        /// </summary>
        /// <param name="role">Модель роли</param>
        /// <param name="updateTagQuery">Модель запроса для обновления роли</param>
        /// <returns></returns>
        public async Task UpdateRole(Role role, UpdateRoleQuery updateRoleQuery)
        {
            //Проверяем, есть ли новое название, если да, то записываем новое
            if (!string.IsNullOrEmpty(updateRoleQuery.NewName))
                role.Name = updateRoleQuery.NewName;


            //Добавляем тег в асинхронном режиме
            var entry = _context.Entry(role);
            if (entry.State == EntityState.Detached)
                _context.Roles.Update(role);

            // Сохраняем изменения в базе 
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаление роли
        /// </summary>
        /// <param name="role">Модель роли</param>
        /// <returns></returns>
        public async Task DeleteRole(Role role)
        {
            // Удаление 
            _context.Roles.Remove(role);

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }
    }
}
