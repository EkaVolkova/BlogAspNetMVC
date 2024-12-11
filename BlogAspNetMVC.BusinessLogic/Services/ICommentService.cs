using BlogAspNetMVC.BusinessLogic.Requests.ArticleRequests;
using BlogAspNetMVC.BusinessLogic.Requests.CommentRequest;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogAspNetMVC.BusinessLogic.Services
{
    public interface ICommentService
    {
        /// <summary>
        /// Добавить комментарий
        /// </summary>
        /// <param name="addNewCommentRequest">Модель запроса добавления комментария</param>
        /// <returns></returns>
        Task<IActionResult> AddComment(AddNewCommentRequest addNewCommentRequest);

        /// <summary>
        /// Изменить комментарий
        /// </summary>
        /// <param name="changeCommentRequest">Модель запроса на изменение комментария</param>
        /// <returns></returns>
        Task<IActionResult> ChangeComment(ChangeCommentRequest changeCommentRequest);

        /// <summary>
        /// Удалить комментарий
        /// </summary>
        /// <param name="guid">Идентификатор комментария</param>
        /// <returns></returns>
        Task<IActionResult> DeleteComment(Guid guid);

        /// <summary>
        /// Получить список всех комментариев
        /// </summary>
        /// <returns></returns>
        Task<IActionResult> GetAllComments();

        /// <summary>
        /// Получить комментарий по идентификатору
        /// </summary>
        /// <param name="guid">Идентификатор комментария</param>
        /// <returns></returns>
        Task<IActionResult> GetCommentById(Guid guid);

        /// <summary>
        /// Получить все комментарии пользователя
        /// </summary>
        /// <param name="userName">UserName автора комментариев</param>
        /// <returns></returns>
        Task<IActionResult> GetCommentsByUserName(string userName);

        /// <summary>
        /// Получить все комментарии пользователя
        /// </summary>
        /// <param name="guid">Идентификатор автора комментариев</param>
        /// <returns></returns>
        Task<IActionResult> GetCommentsByAuthorId(Guid guid);

        /// <summary>
        /// Получить все комментарии к статье
        /// </summary>
        /// <param name="articleName">Название статьи</param>
        /// <returns></returns>
        Task<IActionResult> GetCommentsByArticleName(string articleName);

        /// <summary>
        /// Получить все комментарии к статье
        /// </summary>
        /// <param name="guid">Идентификатор статьи</param>
        /// <returns></returns>
        Task<IActionResult> GetCommentsByArticleId(Guid guid);

    }
}
