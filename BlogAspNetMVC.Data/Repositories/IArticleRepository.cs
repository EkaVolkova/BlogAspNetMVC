using BlogAspNetMVC.Data.Models;
using BlogAspNetMVC.Data.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAspNetMVC.Data.Repositories
{
    public interface IArticleRepository
    {
        /// <summary>
        /// Создание статьи 
        /// </summary>
        /// <param name="article">Модель статьи</param>
        /// <param name="author">Модель автора</param>
        /// <param name="tags">Список тегов</param>
        /// <param name="comments">Список комментариев</param>
        /// <returns></returns>
        Task Create(Article article, User author, List<Tag> tags);

        /// <summary>
        /// Получить статью по идентификатору
        /// </summary>
        /// <param name="guid">Идентификатор статьи</param>
        /// <returns>Модель статьи</returns>
        Task<Article> GetById(Guid guid);

        /// <summary>
        /// Получить статью по названию
        /// </summary>
        /// <param name="name">название статьи</param>
        /// <returns>Модель статьи</returns>
        Task<Article> GetByName(string name);

        /// <summary>
        /// Получить все статьи в БД
        /// </summary>
        /// <returns>Список всех статей</returns>
        Task<List<Article>> GetAllArticles();

        /// <summary>
        /// Обновить статью в базе данных
        /// </summary>
        /// <param name="article">Модель статьи</param>
        /// <param name="updateArticleQuery">Модель запроса обновления</param>
        /// <returns></returns>
        Task UpdateArticle(Article article, UpdateArticleQuery updateArticleQuery);

        /// <summary>
        /// Удалить статью
        /// </summary>
        /// <param name="article">Модель статьи</param>
        /// <returns></returns>
        Task DeleteArticle(Article article);

    }
}
