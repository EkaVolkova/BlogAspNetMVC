using BlogAspNetMVC.BusinessLogic.Requests.ArticleRequests;
using BlogAspNetMVC.BusinessLogic.ViewModels;
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
        Task<ArticleViewModel> AddArticle(AddNewArticleRequest addNewArticleRequest);

        /// <summary>
        /// Изменить статью
        /// </summary>
        /// <param name="changeArticleRequest">Модель запроса на обновление статьи</param>
        /// <returns></returns>
        Task<ArticleViewModel> ChangeArticle(ChangeArticleRequest changeArticleRequest);

        /// <summary>
        /// Изменить теги
        /// </summary>
        /// <param name="changeArticleTagsRequest">Модель запроса на обновление тегов статьи</param>
        /// <returns></returns>
        Task<ArticleViewModel> ChangeArticleTags(ChangeArticleTagsRequest changeArticleTagsRequest);

        /// <summary>
        /// Удалить статью
        /// </summary>
        /// <param name="guid">Идентификатор статьи</param>
        /// <returns></returns>
        Task DeleteArticle(Guid guid);

        /// <summary>
        /// Удалить статью
        /// </summary>
        /// <param name="name">Название статьи</param>
        /// <returns></returns>
        Task DeleteArticle(string name);

        /// <summary>
        /// Получить список всех статей
        /// </summary>
        /// <returns></returns>
        Task<List<ArticleViewModel>> GetAllArticles();

        /// <summary>
        /// Получить статью 
        /// </summary>
        /// <param name="name">Название статьи/param>
        /// <returns></returns>
        Task<ArticleViewModel> GetArticleByName(string name);

        /// <summary>
        /// Получить статью
        /// </summary>
        /// <param name="guid">Идентификатор статьи</param>
        /// <returns></returns>
        Task<ArticleViewModel> GetArticleById(Guid guid);

        /// <summary>
        /// Получить список статей пользователя
        /// </summary>
        /// <param name="userName">UserName пользователя</param>
        /// <returns></returns>
        Task<List<ArticleViewModel>> GetArticlesByUserName(string userName);

        /// <summary>
        /// Получить список статей пользователя
        /// </summary>
        /// <param name="guid">Идентификатор пользователя</param>
        /// <returns></returns>
        Task<List<ArticleViewModel>> GetArticlesByAuthorId(Guid guid);
    }

}
