using BlogAspNetMVC.BusinessLogic.Requests.CommentRequest;
using BlogAspNetMVC.BusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        Task<CommentViewModel> AddComment(AddNewCommentRequest addNewCommentRequest);

        /// <summary>
        /// Изменить комментарий
        /// </summary>
        /// <param name="commentViewModel">Модель запроса на изменение комментария</param>
        /// <returns></returns>
        Task<CommentViewModel> ChangeComment(CommentViewModel commentViewModel);

        /// <summary>
        /// Удалить комментарий
        /// </summary>
        /// <param name="guid">Идентификатор комментария</param>
        /// <returns></returns>
        Task DeleteComment(Guid guid);

        /// <summary>
        /// Получить список всех комментариев
        /// </summary>
        /// <returns></returns>
        Task<List<CommentViewModel>> GetAllComments();

        /// <summary>
        /// Получить комментарий по идентификатору
        /// </summary>
        /// <param name="guid">Идентификатор комментария</param>
        /// <returns></returns>
        Task<CommentViewModel> GetCommentById(Guid guid);

        /// <summary>
        /// Получить все комментарии пользователя
        /// </summary>
        /// <param name="userName">UserName автора комментариев</param>
        /// <returns></returns>
        Task<List<CommentViewModel>> GetCommentsByUserName(string userName);

        /// <summary>
        /// Получить все комментарии пользователя
        /// </summary>
        /// <param name="guid">Идентификатор автора комментариев</param>
        /// <returns></returns>
        Task<List<CommentViewModel>> GetCommentsByAuthorId(Guid guid);

        /// <summary>
        /// Получить все комментарии к статье
        /// </summary>
        /// <param name="articleName">Название статьи</param>
        /// <returns></returns>
        Task<List<CommentViewModel>> GetCommentsByArticleName(string articleName);

        /// <summary>
        /// Получить все комментарии к статье
        /// </summary>
        /// <param name="guid">Идентификатор статьи</param>
        /// <returns></returns>
        Task<List<CommentViewModel>> GetCommentsByArticleId(Guid guid);

    }
}
