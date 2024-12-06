using BlogAspNetMVC.Data.Models;
using BlogAspNetMVC.Data.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogAspNetMVC.Data.Repositories
{
    public interface ITagRepository
    {
        /// <summary>
        /// Создание тега 
        /// </summary>
        /// <param name="tag">Модель тега</param>
        /// <param name="articles">Список статей</param>
        /// <returns></returns>
        Task Create(Tag tag, List<Article> articles);

        /// <summary>
        /// Получить тег по идентификатору
        /// </summary>
        /// <param name="guid">Идентификатор тега</param>
        /// <returns>Модель тега</returns>
        Task<Tag> GetById(Guid guid);

        /// <summary>
        /// Получить тег по названию
        /// </summary>
        /// <param name="name">название тега</param>
        /// <returns>Модель тега</returns>
        Task<Tag> GetByName(string name);

        /// <summary>
        /// Получить все тега в БД
        /// </summary>
        /// <returns>Список всех статей</returns>
        Task<List<Tag>> GetAllTags();

        /// <summary>
        /// Обновить тег в базе данных
        /// </summary>
        /// <param name="tag">Модель тега</param>
        /// <param name="updateTagQuery">Модель запроса обновления</param>
        /// <returns></returns>
        Task UpdateTag(Tag tag, UpdateTagQuery updateTagQuery);

        /// <summary>
        /// Удалить тег
        /// </summary>
        /// <param name="tag">Модель тега</param>
        /// <returns></returns>
        Task DeleteTag(Tag tag);
    }

}
