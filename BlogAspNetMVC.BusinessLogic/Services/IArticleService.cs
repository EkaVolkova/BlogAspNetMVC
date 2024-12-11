using BlogAspNetMVC.BusinessLogic.Requests.ArticleRequests;
using BlogAspNetMVC.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogAspNetMVC.BusinessLogic.Services
{
    public interface IArticleService
    {
        /// <summary>
        /// Добавить статью
        /// </summary>
        /// <param name="addNewArticleRequest">Модель запроса на добавление статьи</param>
        /// <returns></returns>
        Task<IActionResult> AddArticle(AddNewArticleRequest addNewArticleRequest);

        /// <summary>
        /// Изменить статью
        /// </summary>
        /// <param name="changeArticleRequest">Модель запроса на обновление статьи</param>
        /// <returns></returns>
        Task<IActionResult> ChangeArticle(ChangeArticleRequest changeArticleRequest);

        /// <summary>
        /// Изменить теги
        /// </summary>
        /// <param name="changeArticleTagsRequest">Модель запроса на обновление тегов статьи</param>
        /// <returns></returns>
        Task<IActionResult> ChangeArticleTags(ChangeArticleTagsRequest changeArticleTagsRequest);

        /// <summary>
        /// Удалить статью
        /// </summary>
        /// <param name="guid">Идентификатор статьи</param>
        /// <returns></returns>
        Task<IActionResult> DeleteArticle(Guid guid);

        /// <summary>
        /// Удалить статью
        /// </summary>
        /// <param name="name">Название статьи</param>
        /// <returns></returns>
        Task<IActionResult> DeleteArticle(string name);

        /// <summary>
        /// Получить список всех статей
        /// </summary>
        /// <returns></returns>
        Task<IActionResult> GetAllArticles();

        /// <summary>
        /// Получить статью 
        /// </summary>
        /// <param name="name">Название статьи/param>
        /// <returns></returns>
        Task<IActionResult> GetArticleByName(string name);

        /// <summary>
        /// Получить статью
        /// </summary>
        /// <param name="guid">Идентификатор статьи</param>
        /// <returns></returns>
        Task<IActionResult> GetArticleById(Guid guid);

        /// <summary>
        /// Получить список статей пользователя
        /// </summary>
        /// <param name="userName">UserName пользователя</param>
        /// <returns></returns>
        Task<IActionResult> GetArticlesByUserName(string userName);

        /// <summary>
        /// Получить список статей пользователя
        /// </summary>
        /// <param name="guid">Идентификатор пользователя</param>
        /// <returns></returns>
        Task<IActionResult> GetArticlesByAuthorId(Guid guid);
    }

}
