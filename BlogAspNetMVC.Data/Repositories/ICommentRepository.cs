using BlogAspNetMVC.Data.Models;
using BlogAspNetMVC.Data.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogAspNetMVC.Data.Repositories
{
    public interface ICommentRepository
    {
        /// <summary>
        /// Создать комментарий
        /// </summary>
        /// <param name="comment">Модель комментария</param>
        /// <param name="author">Модель автора</param>
        /// <returns></returns>
        Task Create(Comment comment, Article article, User author, Comment parentComment);

        /// <summary>
        /// Получить комментарий по идентификатору
        /// </summary>
        /// <param name="guid">Идентификатор комментария</param>
        /// <returns>Модель комментария</returns>
        Task<Comment> GetById(Guid guid);

        /// <summary>
        /// Получить список комментариев
        /// </summary>
        /// <returns>Список комментариев</returns>
        Task<List<Comment>> GetAllComments();

        /// <summary>
        /// Обновить комментарий
        /// </summary>
        /// <param name="comment">Модель комментария</param>
        /// <param name="updateCommentQuery">Модель запроса обновления комментария</param>
        /// <returns></returns>
        Task UpdateComment(Comment comment, UpdateCommentQuery updateCommentQuery);

        /// <summary>
        /// Удалить комментарий
        /// </summary>
        /// <param name="comment">Моедль комментария</param>
        /// <returns></returns>
        Task DeleteComment(Comment comment);
    }
}
