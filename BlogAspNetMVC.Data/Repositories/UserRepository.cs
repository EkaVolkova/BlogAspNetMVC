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
        DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
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
