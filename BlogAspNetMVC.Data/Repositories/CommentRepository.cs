using BlogAspNetMVC.Data.Models;
using BlogAspNetMVC.Data.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAspNetMVC.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        readonly DataContext _context;
        public CommentRepository(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Создать комментарий
        /// </summary>
        /// <param name="comment">Модель комментария</param>
        /// <param name="author">Модель автора</param>
        /// <returns></returns>
        public async Task Create(Comment comment, Article article, User author, Comment parentComment)
        {
            comment.UpdateDate = DateTime.UtcNow;
            comment.Author = author;
            comment.Article = article;
            comment.ArticleId = article.Id;

            //Добавляем комментарий в асинхронном режиме
            var entry = _context.Entry(comment);
            if (entry.State == EntityState.Detached)
                await _context.Comments.AddAsync(comment);

            // Сохраняем изменения в базе 
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Получить комментарий по идентификатору
        /// </summary>
        /// <param name="guid">Идентификатор комментария</param>
        /// <returns>Модель комментария</returns>
        public async Task<Comment> GetById(Guid guid)
        {
            //Получаем комментарий вместе со всеми связанными сущностями
            return await _context.Comments.Include(a => a.Author)
                                              .Include(a => a.Article)
                                              .Include(a => a.ParentComment)
                                              .Where(a => a.Id == guid)
                                              .FirstOrDefaultAsync();

        }

        /// <summary>
        /// Получить список комментариев
        /// </summary>
        /// <returns>Список комментариев</returns>
        public async Task<List<Comment>> GetAllComments()
        {
            //Получаем комментарии вместе со всеми связанными сущностями
            return await _context.Comments.Include(a => a.Author)
                                          .Include(a => a.Article)
                                          .Include(a => a.ParentComment)
                                          .ToListAsync();

        }

        /// <summary>
        /// Обновить комментарий
        /// </summary>
        /// <param name="comment">Модель комментария</param>
        /// <param name="updateCommentQuery">Модель запроса обновления комментария</param>
        /// <returns></returns>
        public async Task UpdateComment(Comment comment, UpdateCommentQuery updateCommentQuery)
        {
            //Меняем время обновления
            comment.UpdateDate = DateTime.UtcNow;

            //Проверяем, есть ли новый текст, если да, то записываем новый
            if (!string.IsNullOrEmpty(updateCommentQuery.NewText))
                comment.Text = updateCommentQuery.NewText;


            //Добавляем комментарий в асинхронном режиме
            var entry = _context.Entry(comment);
            if (entry.State == EntityState.Detached)
                _context.Comments.Update(comment);

            // Сохраняем изменения в базе 
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удалить комментарий
        /// </summary>
        /// <param name="comment">Моедль комментария</param>
        /// <returns></returns>
        public async Task DeleteComment(Comment comment)
        {
            // Удаление 
            _context.Comments.Remove(comment);

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }

    }
}
