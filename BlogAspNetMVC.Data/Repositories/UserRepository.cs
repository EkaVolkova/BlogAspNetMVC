using BlogAspNetMVC.Data.Models;
using BlogAspNetMVC.Data.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BlogAspNetMVC.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        readonly DataContext _context;
        readonly IRoleRepository _roleRepository;
        public UserRepository(DataContext context, IRoleRepository roleRepository)
        {
            _context = context;
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// Создание пользователя 
        /// </summary>
        /// <param name="user">Модель пользователя</param>
        /// <param name="articles">Список статей</param>
        /// <param name="comments">Список комментариев</param>
        /// <returns></returns>
        public async Task Create(User user, List<Article> articles, List<Comment> comments)
        {
            //Добавляем дату регистрации
            user.RegistrationDate = DateTime.UtcNow;

            //Добавляем ссылки на статьи и комментарии
            user.Articles = articles;
            user.Comments = comments;

            //Получаем модель роли пользователя
            //Все пользователи создаются с доступом "user"
            var role = await _roleRepository.GetByName("user");

            if (role is null)
            {
                //Если роли user еще нет в базе, создадим ее
                user.Role = new Role { Name = "user" };
                await _roleRepository.Create(user.Role);
            }
            else
                user.Role = role;

            //Добавляем статью в асинхронном режиме
            var entry = _context.Entry(user);
            if (entry.State == EntityState.Detached)
                await _context.Users.AddAsync(user);

            // Сохраняем изменения в базе 
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Получить пользователя по идентификатору
        /// </summary>
        /// <param name="guid">Идентификатор пользователя</param>
        /// <returns>Модель пользователя</returns>
        public async Task<User> GetById(Guid guid)
        {
            //Получаем пользователя вместе со всеми связанными сущностями
            return await _context.Users.Include(a => a.Articles)
                                          .Include(a => a.Comments)
                                          .Include(a => a.Role)
                                          .Where(a => a.Id == guid)
                                          .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Получить пользователя по UserName
        /// </summary>
        /// <param name="name">UserName пользователя</param>
        /// <returns>Модель пользователей</returns>
        public async Task<User> GetByUserName(string userName)
        {
            //Получаем пользователя вместе со всеми связанными сущностями
            return await _context.Users.Include(a => a.Articles)
                                          .Include(a => a.Comments)
                                          .Include(a => a.Role)
                                          .Where(a => a.UserName == userName)
                                          .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Получить пользователя по Email
        /// </summary>
        /// <param name="email">Email пользователя</param>
        /// <returns>Модель пользователей</returns>
        public async Task<User> GetByUserEmail(string email)
        {
            //Получаем пользователя вместе со всеми связанными сущностями
            return await _context.Users.Include(a => a.Articles)
                                          .Include(a => a.Comments)
                                          .Include(a => a.Role)
                                          .Where(a => a.Email == email)
                                          .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Получить все пользователей в БД
        /// </summary>
        /// <returns>Список всех пользователей</returns>
        public async Task<List<User>> GetAllUsers()
        {
            //Получаем пользователей вместе со всеми связанными сущностями
            return await _context.Users.Include(a => a.Articles)
                                          .Include(a => a.Comments)
                                          .Include(a => a.Role)
                                          .ToListAsync();
        }

        /// <summary>
        /// Обновить пользователя в базе данных
        /// </summary>
        /// <param name="user">Модель пользователей</param>
        /// <param name="updateUserQuery">Модель запроса обновления</param>
        /// <returns></returns>
        public async Task UpdateUser(User user, UpdateUserQuery updateUserQuery)
        {

            //Проверяем, есть ли новый UserName, если да, то записываем новый
            if (!string.IsNullOrEmpty(updateUserQuery.NewName))
                user.UserName = updateUserQuery.NewName;

            //Проверяем, есть ли новый пароль, если да, то записываем новый
            if (!string.IsNullOrEmpty(updateUserQuery.NewPassword))
                user.Password = updateUserQuery.NewPassword;

            //Проверяем, есть ли новый Email, если да, то записываем новый
            if (!(updateUserQuery.NewEmail is null))
                user.Email = updateUserQuery.NewEmail;

            //Проверяем, есть ли новая роль, если да, то записываем новую
            if (!(updateUserQuery.NewRoleName is null))
            {
                
                var role = await _roleRepository.GetByName(updateUserQuery.NewRoleName);
                if (!(role is null))
                {
                    user.Role = role;
                    user.RoleId = role.Id;
                }
            }
            //Добавляем пользователя в асинхронном режиме
            var entry = _context.Entry(user);
            if (entry.State == EntityState.Detached)
                _context.Users.Update(user);

            // Сохраняем изменения в базе 
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        /// <param name="user">Модель пользователя</param>
        /// <returns></returns>
        public async Task DeleteUser(User user)
        {
            // Удаление 
            _context.Users.Remove(user);

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }
    }
}
