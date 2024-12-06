using BlogAspNetMVC.Data.Models;
using BlogAspNetMVC.Data.Queries;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BlogAspNetMVC.Data.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Создание пользователя 
        /// </summary>
        /// <param name="user">Модель пользователя</param>
        /// <param name="articles">Список статей</param>
        /// <param name="comments">Список комментариев</param>
        /// <returns></returns>
        Task Create(User user, List<Article> articles, List<Comment> comments);

        /// <summary>
        /// Получить пользователя по идентификатору
        /// </summary>
        /// <param name="guid">Идентификатор пользователя</param>
        /// <returns>Модель пользователя</returns>
        Task<User> GetById(Guid guid);

        /// <summary>
        /// Получить пользователя по UserName
        /// </summary>
        /// <param name="name">UserName пользователя</param>
        /// <returns>Модель пользователей</returns>
        Task<User> GetByUserName(string userName);

        /// <summary>
        /// Получить пользователя по Email
        /// </summary>
        /// <param name="email">Email пользователя</param>
        /// <returns>Модель пользователей</returns>
        Task<User> GetByUserEmail(string email);

        /// <summary>
        /// Получить все пользователей в БД
        /// </summary>
        /// <returns>Список всех пользователей</returns>
        Task<List<User>> GetAllUsers();

        /// <summary>
        /// Обновить пользователя в базе данных
        /// </summary>
        /// <param name="user">Модель пользователей</param>
        /// <param name="updateUserQuery">Модель запроса обновления</param>
        /// <returns></returns>
        Task UpdateUser(User user, UpdateUserQuery updateUserQuery);

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        /// <param name="user">Модель пользователя</param>
        /// <returns></returns>
        Task DeleteUser(User user);
    }
}
