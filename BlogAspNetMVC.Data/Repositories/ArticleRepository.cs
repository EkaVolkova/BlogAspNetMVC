using BlogAspNetMVC.Data.Models;
using BlogAspNetMVC.Data.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAspNetMVC.Data.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        readonly DataContext _context;
        public ArticleRepository(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Создание статьи 
        /// </summary>
        /// <param name="article">Модель статьи</param>
        /// <param name="author">Модель автора</param>
        /// <param name="tags">Список тегов</param>
        /// <param name="comments">Список комментариев</param>
        /// <returns></returns>
        public async Task Create(Article article, User author, List<Tag> tags)
        {

            article.UpdateDate = DateTime.UtcNow;
            article.Author = author;
            article.AuthorId = author.Id;
            article.Tags = tags;
            article.Comments = new List<Comment>();

            //Добавляем статью в асинхронном режиме
            var entry = _context.Entry(article);
            if (entry.State == EntityState.Detached)
                await _context.Articles.AddAsync(article);

            // Сохраняем изменения в базе 
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Получить статью по идентификатору
        /// </summary>
        /// <param name="guid">Идентификатор статьи</param>
        /// <returns>Модель статьи</returns>
        public async Task<Article> GetById(Guid guid)
        {
            //Получаем статью вместе со всеми связанными сущностями
            return await _context.Articles.Include(a => a.Author)
                                          .Include(a => a.Comments)
                                          .Include(a => a.Tags)
                                          .Where(a => a.Id == guid)
                                          .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Получить статью по названию
        /// </summary>
        /// <param name="name">название статьи</param>
        /// <returns>Модель статьи</returns>
        public async Task<Article> GetByName(string name)
        {
            //Получаем статью вместе со всеми связанными сущностями
            return await _context.Articles.Include(a => a.Author)
                                          .Include(a => a.Comments)
                                          .Include(a => a.Tags)
                                          //Ищем полное совпадение, для удобства переводим в нижний регистр и название в БД и переданное
                                          .Where(a => a.Name.ToLower().Equals(name.ToLower()))
                                          .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Получить все статьи в БД
        /// </summary>
        /// <returns>Список всех статей</returns>
        public async Task<List<Article>> GetAllArticles()
        {
            //Получаем статьи вместе со всеми связанными сущностями
            return await _context.Articles.Include(a => a.Author)
                                          .Include(a => a.Comments)
                                          .Include(a => a.Tags)
                                          .ToListAsync();

        }

        /// <summary>
        /// Обновить статью в базе данных
        /// </summary>
        /// <param name="article">Модель статьи</param>
        /// <param name="updateArticleQuery">Модель запроса обновления</param>
        /// <returns></returns>
        public async Task UpdateArticle(Article article, UpdateArticleQuery updateArticleQuery)
        {
            //Меняем время обновления
            article.UpdateDate = DateTime.UtcNow;

            //Проверяем, есть ли новое навание, если да, то записываем новое
            if(!string.IsNullOrEmpty(updateArticleQuery.NewName))
                article.Name = updateArticleQuery.NewName;

            //Проверяем, есть ли новый текст, если да, то записываем новый
            if (!string.IsNullOrEmpty(updateArticleQuery.NewText))
                article.Text = updateArticleQuery.NewText;

            //Проверяем, есть ли новый список тегов, если да, то записываем новый
            if (!(updateArticleQuery.NewTags is null) && updateArticleQuery.NewTags.Count != 0)
                article.Tags = updateArticleQuery.NewTags;

            //Добавляем статью в асинхронном режиме
            var entry = _context.Entry(article);
            if (entry.State == EntityState.Detached)
                _context.Articles.Update(article);

            // Сохраняем изменения в базе 
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удалить статью
        /// </summary>
        /// <param name="article">Модель статьи</param>
        /// <returns></returns>
        public async Task DeleteArticle(Article article)
        {
            // Удаление 
            _context.Articles.Remove(article);

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }

    }
}
