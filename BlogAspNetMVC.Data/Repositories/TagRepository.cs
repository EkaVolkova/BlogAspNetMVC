using BlogAspNetMVC.Data.Models;
using BlogAspNetMVC.Data.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAspNetMVC.Data.Repositories
{
    public class TagRepository : ITagRepository
    {
        DataContext _context;
        public TagRepository(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Создание тега 
        /// </summary>
        /// <param name="tag">Модель тега</param>
        /// <param name="articles">Список статей</param>
        /// <returns></returns>
        public async Task Create(Tag tag, List<Article> articles)
        {
            tag.Articles = articles;

            //Добавляем тег в асинхронном режиме
            var entry = _context.Entry(tag);
            if (entry.State == EntityState.Detached)
                await _context.Tags.AddAsync(tag);

            // Сохраняем изменения в базе 
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Получить тег по идентификатору
        /// </summary>
        /// <param name="guid">Идентификатор тега</param>
        /// <returns>Модель тега</returns>
        public async Task<Tag> GetById(Guid guid)
        {
            //Получаем тег вместе со всеми связанными сущностями
            return await _context.Tags.Include(a => a.Articles)
                                              .Where(a => a.Id == guid)
                                              .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Получить тег по названию
        /// </summary>
        /// <param name="name">название тега</param>
        /// <returns>Модель тега</returns>
        public async Task<Tag> GetByName(string name)
        {
            //Получаем тег вместе со всеми связанными сущностями
            return await _context.Tags.Include(a => a.Articles)
                                              .Where(a => a.Name == name)
                                              .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Получить все тега в БД
        /// </summary>
        /// <returns>Список всех статей</returns>
        public async Task<List<Tag>> GetAllTags()
        {

            //Получаем теги вместе со всеми связанными сущностями
            return await _context.Tags.Include(a => a.Articles)
                                          .ToListAsync();
        }

        /// <summary>
        /// Обновить тег в базе данных
        /// </summary>
        /// <param name="tag">Модель тега</param>
        /// <param name="updateTagQuery">Модель запроса обновления</param>
        /// <returns></returns>
        public async Task UpdateTag(Tag tag, UpdateTagQuery updateTagQuery)
        {

            //Проверяем, есть ли новое название, если да, то записываем новое
            if (!string.IsNullOrEmpty(updateTagQuery.NewName))
                tag.Name = updateTagQuery.NewName;


            //Добавляем тег в асинхронном режиме
            var entry = _context.Entry(tag);
            if (entry.State == EntityState.Detached)
                _context.Tags.Update(tag);

            // Сохраняем изменения в базе 
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удалить тег
        /// </summary>
        /// <param name="tag">Модель тега</param>
        /// <returns></returns>
        public async Task DeleteTag(Tag tag)
        {
            // Удаление 
            _context.Tags.Remove(tag);

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }
    }

}
